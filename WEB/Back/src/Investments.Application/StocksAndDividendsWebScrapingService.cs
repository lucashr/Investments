using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Application.helpers;
using Investments.Application.Utils;
using Investments.Domain;
using Investments.Domain.Enum;
using Investments.Persistence.Contracts;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Investments.Application
{
    public class StocksAndDividendsWebScrapingService : IWebScrapingStocksAndDividendsService
    {

        const string WEBPAGE_STOCKS = "https://www.fundamentus.com.br/resultado.php";
        const string WEBPAGE_DIVIDENDS = "https://www.fundamentus.com.br/proventos.php?papel";
        IWebDriver _driver;
        IDetailedStocksPersist _detailedStocksPersist;
        IStockDividendPersist _stockDividendsPersist;
        ILogger<StocksAndDividendsWebScrapingService> _logger;
        CancellationTokenSource _cancellationTokenSource;
        IHttpContextAccessor _httpContextAccessor;
        SessionContext _sessionContext;
        string _sessionId;

        public StocksAndDividendsWebScrapingService(IDetailedStocksPersist detailedStocksPersist,
                                                    IStockDividendPersist stocksDividendsPersist,
                                                    ILogger<StocksAndDividendsWebScrapingService> logger,
                                                    SessionContext sessionContext)
        {
            _sessionContext = sessionContext;
            _detailedStocksPersist = detailedStocksPersist;
            _stockDividendsPersist = stocksDividendsPersist;
            _logger = logger;
            _driver = WebDriverSelenium.ConfigDriver();
            _sessionId = _sessionContext.SessionId;
        }

        public async Task<IEnumerable<DetailedStock>> GetStocksAsync(CancellationTokenSource cancellationTokenSource)
        {

            _cancellationTokenSource = cancellationTokenSource;

            var result = await DriverGetStocksAsync();

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                return result;
            }

            var bIsOk = await _detailedStocksPersist.AddDetailedStocksAsync(result);

            if (bIsOk == false)
            {
                _logger.LogError("GetStocksAsync::Error SaveChangesAsync");
            }

            return result;

        }

        public async Task<IEnumerable<StockDividend>> GetStocksDividendsAsync(IEnumerable<DetailedStock> detailedStocks, 
                                                                              CancellationTokenSource cancellationTokenSource)
        {

            _cancellationTokenSource = cancellationTokenSource;

            var result = await DriverGetStocksDividendsAsync(detailedStocks);

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                return result;
            }

            var bIsOk = await _stockDividendsPersist.AddStockDividendsAsync(result.ToList());

            if (bIsOk == false)
            {
                _logger.LogError("GetStocksDividendsAsync::Error SaveChangesAsync");
            }

            return result;

        }

        public async Task<IEnumerable<StockDividend>> DriverGetStocksDividendsAsync(IEnumerable<DetailedStock> detailedStocks)
        {

            var clock = new Stopwatch();

            clock.Start();

            List<string> orderColumnTableOfFunds = new List<string>
            {
                "Data", "Valor", "Tipo", "Data de Pagamento", "Por quantas ações"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;

            try
            {

                await LogUtils.LogActions(PageIdentification.StocksAndDividends, "Started: Capture Dividends Stocks", _sessionId);

                var stocksDividends = new List<StockDividend>();
                var stocksDividendsTmp = new List<StockDividend>();

                foreach (var fund in detailedStocks)
                {
                    
                    if(AbortarProcesso())
                        return Enumerable.Empty<StockDividend>();
                    
                    bool navigateOK = await GoToPage($"{WEBPAGE_DIVIDENDS}={fund.FundCode}&tipo=2");

                    if (!navigateOK)
                        return Enumerable.Empty<StockDividend>();
                        
                    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                    
                    var tableResultadoExists = _driver.FindElements(By.Id("resultado"));
                    
                    if(tableResultadoExists == null || tableResultadoExists.Count == 0)
                        continue;

                    var rows = _driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr"));
                    int numberOfLines = rows.Count;

                    Console.WriteLine($"Total de linhas {numberOfLines}");

                    var columns = _driver.FindElements(By.XPath("//*[@id='resultado']/thead/tr/th"));
                    int numberOfColumn = columns.Count;

                    Console.WriteLine($"Total de colunas {numberOfColumn}");

                    if (totalOfColumnExpected != numberOfColumn)
                    {
                        await LogUtils.LogActions(PageIdentification.StocksAndDividends, $"Total of columns expected invalid {numberOfColumn}", _sessionId);
                        _logger.LogError($"Total of columns expected invalid {numberOfColumn}");
                        return await Task.FromResult<IEnumerable<StockDividend>>(Enumerable.Empty<StockDividend>());
                    }

                    Console.WriteLine(_driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/p/strong")).Text);

                    for (int j = 1; j <= numberOfColumn; j++)
                    {
                        string campo = _driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text;

                        if (orderColumnTableOfFunds[j - 1] != campo)
                        {
                            await LogUtils.LogActions(PageIdentification.StocksAndDividends, $"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}", _sessionId);
                            _logger.LogError($"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}");
                            return await Task.FromResult<IEnumerable<StockDividend>>(Enumerable.Empty<StockDividend>());
                        }

                        Console.Write(_driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                    }

                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        
                        if(AbortarProcesso())
                            return Enumerable.Empty<StockDividend>();

                        string[] obj = new string[5];

                        for (int j = 1; j <= numberOfColumn; j++)
                        {
                            var e = _driver.FindElement(By.XPath($"//*[@id='resultado']/tbody/tr[{i}]/td[{j}]")).Text;
                            obj[j - 1] = e;
                        }
                        
                        var datePayment = new DateTime();
                        DateTime.TryParse(obj[3].ToString(), out datePayment);

                        var stockDividends = new StockDividend()
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            FundCode = fund.FundCode,
                            Date = Convert.ToDateTime(obj[0]).ToString("dd/MM/yyyy"),
                            Value = Convert.ToDouble(obj[1]),
                            Type = obj[2],
                            DatePayment =  datePayment.ToString("dd/MM/yyyy"),
                            ForHowManyShares = Convert.ToInt16(obj[4]),
                        };

                        await LogUtils.LogActions(PageIdentification.StocksAndDividends, stockDividends, _sessionId);

                        stocksDividendsTmp.Add(stockDividends);

                    }

                }

                if (stocksDividendsTmp.Count() == 0)
                    return await Task.FromResult<IEnumerable<StockDividend>>(stocksDividends);

                stocksDividends.AddRange(stocksDividendsTmp);
                stocksDividendsTmp.Clear();

                await LogUtils.LogActions(PageIdentification.StocksAndDividends, "Completed: Capture of stocks dividends", _sessionId);

                return await Task.FromResult<IEnumerable<StockDividend>>(stocksDividends);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await LogUtils.LogActions(PageIdentification.StocksAndDividends, $"Error: Capture of stocks dividends -> {ex.Message}", _sessionId);
                _logger.LogError($"Error: Capture of stocks dividends -> {ex.Message}");
                return await Task.FromResult<IEnumerable<StockDividend>>(Enumerable.Empty<StockDividend>());
            }
            finally{

                _driver.Close();

                var elapsed = clock.Elapsed;
                var elapsedTime = string.Format("Tempo decorrido: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);

                await LogUtils.LogActions(PageIdentification.StocksAndDividends, elapsedTime, _sessionId);

            }
        }

        public async Task<IEnumerable<DetailedStock>> DriverGetStocksAsync()
        {
            
            var clock = new Stopwatch();

            clock.Start();

            await VariablesManager.ConectionsWebSocket.socketManager.SendMessageAsync(_sessionId, JsonConvert.SerializeObject("Started: Capture of Stocks"));
            
            var totalRows = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            
            bool navigateOK = await GoToPage(WEBPAGE_STOCKS);

            if (!navigateOK)
                        return Enumerable.Empty<DetailedStock>();
            
            var orderColumnTableOfFunds = new List<string>
            {
                "Papel", "Cotação", "P/L", "P/VP",
                "PSR", "Div.Yield", "P/Ativo",
                "P/Cap.Giro", "P/EBIT", "P/Ativ Circ.Liq",
                "EV/EBIT", "EV/EBITDA", "Mrg Ebit",
                "Mrg. Líq.", "Liq. Corr.", "ROIC", "ROE", 
                "Liq.2meses", "Patrim. Líq", "Dív.Brut/ Patrim.",
                "Cresc. Rec.5a"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;
            var stockList = new List<DetailedStock>();

            try
            {

                if(AbortarProcesso())
                        return Enumerable.Empty<DetailedStock>();

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='resultado']/tbody/tr")));
                
                // #if DEBUG
                //     totalRows = new ReadOnlyCollection<IWebElement>(driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr")).Take(10).ToList());
                // #else
                    totalRows = new ReadOnlyCollection<IWebElement>(_driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr")).ToList());
                // #endif
                
                Console.WriteLine($"Total de linhas {totalRows.Count}");

                var table = _driver.FindElement(By.Id("resultado"));

                var header = table.FindElement(By.TagName("thead"));

                var headerColumns = header.FindElements(By.TagName("th"));
                Console.WriteLine($"Total de colunas {headerColumns.Count}");

                if (headerColumns.Count != totalOfColumnExpected)
                {
                    await LogUtils.LogActions(PageIdentification.StocksAndDividends, "ScrapeStockData::Error: The number of columns in the table is different from the expected number of columns", _sessionId);
                    _logger.LogError("ScrapeStockData::Error: The number of columns in the table is different from the expected number of columns");
                    return Enumerable.Empty<DetailedStock>();
                }

                for (int i = 0; i < headerColumns.Count; i++)
                {
                    if (headerColumns[i].Text.Trim() != orderColumnTableOfFunds[i])
                    {
                        await LogUtils.LogActions(PageIdentification.StocksAndDividends, "ScrapeStockData::Error: The order of the columns in the table is different from the expected order", _sessionId);
                        _logger.LogError("ScrapeStockData::Error: The order of the columns in the table is different from the expected order");
                        return Enumerable.Empty<DetailedStock>();
                    }
                }

                foreach (var row in totalRows)
                {

                    if(AbortarProcesso())
                        return Enumerable.Empty<DetailedStock>();

                    var cells = row.FindElements(By.TagName("td"));

                    if (cells.Count > 0)
                    {
                        var stock = new DetailedStock
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            FundCode = cells[0].Text.Trim(),
                            Quotation = double.TryParse(cells[1].Text.Trim(), out double cotacao) ? cotacao : 0,
                            PL = double.TryParse(cells[2].Text.Trim(), out double pl) ? pl : 0,
                            PVP = double.TryParse(cells[3].Text.Trim(), out double pvp) ? pvp : 0,
                            PSR = double.TryParse(cells[4].Text.Trim(), out double psr) ? psr : 0,
                            DivYield = double.TryParse(cells[5].Text.Replace("%", "").Trim(), out double divYield) ? divYield : 0,
                            PriceOverAsset = double.TryParse(cells[6].Text.Trim(), out double pAtivo) ? pAtivo : 0,
                            PriceOnWorkingCapital = double.TryParse(cells[7].Text.Trim(), out double pCapGiro) ? pCapGiro : 0,
                            PEBIT = double.TryParse(cells[8].Text.Trim(), out double pebit) ? pebit : 0,
                            PriceOverNetCurrentAssets = double.TryParse(cells[9].Text.Trim(), out double pAtivCircLiq) ? pAtivCircLiq : 0,
                            EVEBIT = double.TryParse(cells[10].Text.Trim(), out double evEbit) ? evEbit : 0,
                            EVEBITDA = double.TryParse(cells[11].Text.Trim(), out double evEbitda) ? evEbitda : 0,
                            EbitMargin = double.TryParse(cells[12].Text.Replace("%", "").Trim(), out double mrgEbit) ? mrgEbit : 0,
                            LiquidityMargin = double.TryParse(cells[13].Text.Trim(), out double mrgLiq) ? mrgLiq : 0,
                            LiquidityCurrent = double.TryParse(cells[14].Text.Trim(), out double liqCorr) ? liqCorr : 0,
                            ROIC = double.TryParse(cells[15].Text.Replace("%", "").Trim(), out double roic) ? roic : 0,
                            ROE = double.TryParse(cells[16].Text.Replace("%", "").Trim(), out double roe) ? roe : 0,
                            LiquidityTwoMonths = double.TryParse(cells[17].Text.Trim(), out double liq2meses) ? liq2meses : 0,
                            NetWorth = double.TryParse(cells[18].Text.Trim(), out double patrimLiq) ? patrimLiq : 0,
                            GrossEquityDebt = double.TryParse(cells[19].Text.Trim(), out double divBrutPatrim) ? divBrutPatrim : 0,
                            RevenueGrowthFiveYears = double.TryParse(cells[20].Text.Replace("%", "").Trim(), out double crescRec5a) ? crescRec5a : 0                        
                        };

                        await LogUtils.LogActions(PageIdentification.StocksAndDividends, stock, _sessionId);

                        stockList.Add(stock);

                    }
                }

                 await LogUtils.LogActions(PageIdentification.StocksAndDividends, "Completed: Capture of stocks", _sessionId);

                 return stockList.DistinctBy(x=> x.FundCode).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Capture of stocks -> {ex.Message}");
                await LogUtils.LogActions(PageIdentification.StocksAndDividends, $"Error: Capture of stocks -> {ex.Message}", _sessionId);
                return await Task.FromResult<IEnumerable<DetailedStock>>(stockList);
            }
            finally{

                _driver.Quit();

                var elapsed = clock.Elapsed;
                var elapsedTime = string.Format("Tempo decorrido: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);

                await LogUtils.LogActions(PageIdentification.StocksAndDividends, elapsedTime, _sessionId);
                
            }

        }
        
        public async Task<bool> GoToPage(string linkPage)
        {

            int attempts = 0;

            try
            {
                _driver.Navigate().GoToUrl(linkPage);
                return true;
            }
            catch (Exception ex)
            {

                if (attempts > 10)
                {
                    await LogUtils.LogActions(PageIdentification.StocksAndDividends, $"Error: GoToPage {ex.Message}", _sessionId);
                    _logger.LogError($"Error: GoToPage {ex.Message}");
                    return false;
                }
                    
                attempts++;

                return await GoToPage(linkPage);
                
            }
        }
        private bool AbortarProcesso(){

            if (_cancellationTokenSource.IsCancellationRequested)
                return true;
            else
                return false;
        }

    }
}