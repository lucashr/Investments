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
using Investments.Domain;
using Investments.Persistence.Contracts;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Investments.Application
{
    public class WebScrapingStocksAndDividendsService : IWebScrapingStocksAndDividendsService
    {

        IWebDriver driver;
        IDetailedStocksPersist _detailedStocksPersist;
        IStocksYeldPersist _stocksYeldPersist;

        const string WEBPAGE_STOCKS = "https://www.fundamentus.com.br/resultado.php";
        const string WEBPAGE_YELDS = "https://www.fundamentus.com.br/proventos.php?papel";
        
        CancellationTokenSource _cancellationTokenSource;

        public WebScrapingStocksAndDividendsService(IDetailedStocksPersist detailedStocksPersist,
                                                IStocksYeldPersist stocksYeldPersist)
        {
            _detailedStocksPersist = detailedStocksPersist;
            _stocksYeldPersist = stocksYeldPersist;
            ConfigDriver();
        }

        public async Task<IEnumerable<DetailedStocks>> GetStocksAsync(CancellationTokenSource cancellationTokenSource)
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
                new Exception("GetFundsAsync::Error SaveChangesAsync");
            }

            return result;

        }

        public async Task<IEnumerable<StocksDividends>> GetStocksDividendsAsync(IEnumerable<DetailedStocks> detailedStocks, CancellationTokenSource cancellationTokenSource)
        {

            _cancellationTokenSource = cancellationTokenSource;
            // detailedStocks = await _stocksYeldPersist.GetAllStockDividendsAsync();
            var result = await DriverGetStocksDividendsAsync(detailedStocks);

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                return result;
            }

            var bIsOk = await _stocksYeldPersist.AddStockDividendsAsync(result.ToList());

            if (bIsOk == false)
            {
                // throw new Exception("GetYeldsFundsAsync::Error SaveChangesAsync");
            }

            return result;

        }

        public async Task<IEnumerable<StocksDividends>> DriverGetStocksDividendsAsync(IEnumerable<DetailedStocks> detailedStocks)
        {

            var clock = new Stopwatch();
            var stocksDividends = new List<StocksDividends>();
            var stocksDividendsTmp = new List<StocksDividends>();
            dynamic rows;
            dynamic columns;

            int numberOfLines = 0;
            int numberOfColumn = 0;

            clock.Start();

            List<string> orderColumnTableOfFunds = new List<string>
            {
                "Data", "Valor", "Tipo", "Data de Pagamento", "Por quantas ações"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;

            try
            {

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Started: Capture Dividends Stocks"));

                foreach (var fund in detailedStocks)
                {
                    
                    if(AbortarProcesso())
                        return Enumerable.Empty<StocksDividends>();
                    
                    GoToPage($"{WEBPAGE_YELDS}={fund.FundCode}&tipo=2");

                    try
                    {

                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                        
                        // wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='resultado']/tbody/tr")));
                        var tableResultadoExists = driver.FindElements(By.Id("resultado"));
                        if(tableResultadoExists == null || tableResultadoExists.Count == 0)
                            continue;
                        

                        rows = driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr"));

                        numberOfLines = rows.Count;

                        Console.WriteLine($"Total de linhas {numberOfLines}");

                        columns = driver.FindElements(By.XPath("//*[@id='resultado']/thead/tr/th"));

                        numberOfColumn = columns.Count;

                        Console.WriteLine($"Total de colunas {numberOfColumn}");

                        if (totalOfColumnExpected != numberOfColumn)
                        {
                            Console.WriteLine($"Total of columns expected invalid {numberOfColumn}");
                        }

                        Console.WriteLine(driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/p/strong")).Text);
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }

                    //Limitador de historico de curto prazo
                    // if (numberOfLines > 12)
                    // {
                    //     numberOfLines = 12;
                    // }

                    for (int j = 1; j <= numberOfColumn; j++)
                    {
                        string campo = driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text;

                        if (orderColumnTableOfFunds[j - 1] != campo)
                        {
                            Console.Write($"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}");
                            return await Task.FromResult<IEnumerable<StocksDividends>>(stocksDividends);
                        }

                        Console.Write(driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                    }

                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        
                        if(AbortarProcesso())
                            return Enumerable.Empty<StocksDividends>();

                        string[] obj = new string[5];

                        for (int j = 1; j <= numberOfColumn; j++)
                        {
                            var e = driver.FindElement(By.XPath($"//*[@id='resultado']/tbody/tr[{i}]/td[{j}]")).Text;
                            obj[j - 1] = e;
                        }
                        
                        var datePayment = new DateTime();
                        DateTime.TryParse(obj[3].ToString(), out datePayment);

                        var stockDividends = new StocksDividends()
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            FundCode = fund.FundCode,
                            Date = Convert.ToDateTime(obj[0]).ToString("dd/MM/yyyy"),
                            Value = Convert.ToDouble(obj[1]),
                            Type = obj[2],
                            DatePayment =  datePayment.ToString("dd/MM/yyyy"),
                            ForHowManyShares = Convert.ToInt16(obj[4]),
                        };

                        await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(stockDividends));
                        Console.WriteLine($"{JsonConvert.SerializeObject(stockDividends)}");
                        Debug.WriteLine($"{JsonConvert.SerializeObject(stockDividends)}");
                        
                        stocksDividendsTmp.Add(stockDividends);

                    }

                }

                if (stocksDividendsTmp.Count() == 0)
                {
                    return await Task.FromResult<IEnumerable<StocksDividends>>(stocksDividends);
                }

                stocksDividends.AddRange(stocksDividendsTmp);

                stocksDividendsTmp.Clear();

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Completed: Capture of yelds"));

                return await Task.FromResult<IEnumerable<StocksDividends>>(stocksDividends);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Error: Capture of yelds"));
                return await Task.FromResult<IEnumerable<StocksDividends>>(stocksDividends);
            }
            finally{
                driver.Close();

                var elapsed = clock.Elapsed;
                var elapsedTime = string.Format("Tempo decorrido: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(elapsedTime));
                Console.WriteLine($"{JsonConvert.SerializeObject(elapsedTime)}");
                Debug.WriteLine($"{JsonConvert.SerializeObject(elapsedTime)}");
            }
        }

        private async Task<IEnumerable<DetailedStocks>> GetStocksAsync()
        {

            var result = new List<DetailedStocks>();

            try
            {

                var stockList = await DriverGetStocksAsync();

                result.AddRange(stockList);

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Finished: Capture of Stocks"));

            }
            catch (Exception ex)
            {
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Error: Capture of Stocks"));
                throw new Exception("DriverGetFundsAsync::Error", ex);
            }

            return result;
        }
        
        public void ConfigDriver()
        {

            var options = new ChromeOptions();
            options.AddArgument("--headless"); // Executar sem abrir a interface do navegador
            options.AddArgument("--disable-gpu"); // Desativar GPU para evitar erros em algumas plataformas
            options.AddArgument("--no-sandbox"); // Evitar o uso de sandbox, útil em ambientes de produção

            driver = new ChromeDriver(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        public void Dispose()
        {
            driver.Close();
            driver.Quit();
        }

        public void GoToPage(string linkPage)
        {
            try
            {
                driver.Navigate().GoToUrl(linkPage);
            }
            catch (System.Exception)
            {
                GoToPage(linkPage);
            }
        }

        public async Task<IEnumerable<DetailedStocks>> DriverGetStocksAsync()
        {
            
            var clock = new Stopwatch();

            clock.Start();

            await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Started: Capture of Stocks"));
            
            var totalRows = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            
            
            GoToPage(WEBPAGE_STOCKS);

            List<string> orderColumnTableOfFunds = new List<string>
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
            var stockList = new List<DetailedStocks>();

            try
            {

                if(AbortarProcesso())
                        return Enumerable.Empty<DetailedStocks>();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='resultado']/tbody/tr")));
                
                // #if DEBUG
                //     totalRows = new ReadOnlyCollection<IWebElement>(driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr")).Take(10).ToList());
                // #else
                    totalRows = new ReadOnlyCollection<IWebElement>(driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr")).ToList());
                // #endif
                
                Console.WriteLine($"Total de linhas {totalRows.Count}");

                var table = driver.FindElement(By.Id("resultado"));

                var header = table.FindElement(By.TagName("thead"));

                var headerColumns = header.FindElements(By.TagName("th"));
                Console.WriteLine($"Total de colunas {headerColumns.Count}");

                if (headerColumns.Count != totalOfColumnExpected)
                {
                    throw new Exception("ScrapeStockData::Error: The number of columns in the table is different from the expected number of columns");
                }

                for (int i = 0; i < headerColumns.Count; i++)
                {
                    if (headerColumns[i].Text.Trim() != orderColumnTableOfFunds[i])
                    {
                        throw new Exception("ScrapeStockData::Error: The order of the columns in the table is different from the expected order");
                    }
                }

                

                foreach (var row in totalRows)
                {
                    var cells = row.FindElements(By.TagName("td"));
                    if (cells.Count > 0)
                    {
                        var stock = new DetailedStocks
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

                        await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(stock));
                        Console.WriteLine($"{JsonConvert.SerializeObject(stock)}");
                        Debug.WriteLine($"{JsonConvert.SerializeObject(stock)}");

                        stockList.Add(stock);
                    }
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Error: Capture of stocks"));
                return await Task.FromResult<IEnumerable<DetailedStocks>>(stockList);
            }
            finally{

                driver.Close();
                clock.Stop();

                var elapsed = clock.Elapsed;
                var elapsedTime = string.Format("Tempo decorrido: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(elapsedTime));
                Console.WriteLine($"{JsonConvert.SerializeObject(elapsedTime)}");
                Debug.WriteLine($"{JsonConvert.SerializeObject(elapsedTime)}");
            }

            await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Completed: Capture of stocks"));

            return stockList;

        }

        private bool AbortarProcesso(){

            if (_cancellationTokenSource.IsCancellationRequested)
                return true;
            else
                return false;
        }

    }
}