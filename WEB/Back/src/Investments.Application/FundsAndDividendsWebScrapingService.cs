using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Application.helpers;
using Investments.Application.Utils;
using Investments.Domain.Enum;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Investments.VariablesManager;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Investments.Application
{
    public class FundsAndDividendsWebScrapingService : IWebScrapingFundsAndDividendsService
    {

        const string WEBPAGE_FUNDS = "https://www.fundamentus.com.br/fii_resultado.php";
        const string WEBPAGE_DIVIDENDS = "https://www.fundamentus.com.br/fii_proventos.php?papel";
        IWebDriver _driver;
        IDetailedFundPersist _detailedFundPersist;
        IFundDividendPersist _fundDividendPersist;
        CancellationTokenSource _cancellationTokenSource;
        ILogger _logger;
        SessionContext _sessionContext;
        string _sessionId;

        public FundsAndDividendsWebScrapingService(IDetailedFundPersist detailedFundPersist,
                                                   IFundDividendPersist fundsDividendsPersist,
                                                   ILogger<FundsAndDividendsWebScrapingService> logger,
                                                   SessionContext sessionContext)
        {
            _sessionContext = sessionContext;
            _detailedFundPersist = detailedFundPersist;
            _fundDividendPersist = fundsDividendsPersist;
            _logger = logger;
            _driver = WebDriverSelenium.ConfigDriver();
            _sessionId = _sessionContext.SessionId;
        }

        public async Task<IEnumerable<DetailedFund>> GetFundsAsync(CancellationTokenSource cancellationTokenSource)
        {

            _cancellationTokenSource = cancellationTokenSource;

            var result = await DriverGetFundsAsync();

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                return result;
            }

            var bIsOk = await _detailedFundPersist.AddDetailedFundsAsync(result);

            if (bIsOk == false)
            {
                _logger.LogError("GetFundsAsync::Error SaveChangesAsync");
            }

            return result;

        }
        public async Task<IEnumerable<FundDividend>> GetFundDividendsAsync(IEnumerable<DetailedFund> detailedFunds, CancellationTokenSource cancellationTokenSource)
        {

            _cancellationTokenSource = cancellationTokenSource;

            var result = await DriverGetFundsDividendsAsync(detailedFunds);

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                return result;
            }

            var bIsOk = await _fundDividendPersist.AddFundsDividendsAsync(result.ToList());

            if (bIsOk == false)
            {
                _logger.LogError("GetFundDividendsAsync::Error SaveChangesAsync");
            }

            return result;

        }
        public async Task<IEnumerable<DetailedFund>> DriverGetFundsAsync()
        {

            var clock = new Stopwatch();
            clock.Start();

            await LogUtils.LogActions(PageIdentification.FundsAndDividends, "Started: Capture of FIIs", _sessionId);

            bool navigateOK = await GoToPage(WEBPAGE_FUNDS);

            if (!navigateOK)
                        return Enumerable.Empty<DetailedFund>();

            List<string> orderColumnTableOfFunds = new List<string>
            {
                "Papel", "Segmento", "Cotação", "FFO Yield",
                "Dividend Yield", "P/VP", "Valor de Mercado",
                "Liquidez", "Qtd de imóveis", "Preço do m2",
                "Aluguel por m2", "Cap Rate", "Vacância Média",
                "Endereço"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;
            var detailedFunds = new List<DetailedFund>();

            try
            {

                if(AbortarProcesso())
                        return Enumerable.Empty<DetailedFund>();
                        
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='tabelaResultado']/tbody/tr")));

                var rows = _driver.FindElements(By.XPath("//*[@id='tabelaResultado']/tbody/tr"));

                #if DEBUG
                    int numberOfLines = 10;
                #else
                int numberOfLines = rows.Count;
                #endif

                Console.WriteLine($"Total de linhas {numberOfLines}");

                var columns = _driver.FindElements(By.XPath("//*[@id='tabelaResultado']/thead/tr/th"));

                int numberOfColumn = columns.Count;

                Console.WriteLine($"Total de colunas {numberOfColumn}");

                if (totalOfColumnExpected != numberOfColumn)
                {
                    await LogUtils.LogActions(PageIdentification.FundsAndDividends, $"Total of columns expected invalid {numberOfColumn}", _sessionId);
                    _logger.LogError($"Total of columns expected invalid {numberOfColumn}");
                    return await Task.FromResult<IEnumerable<DetailedFund>>(Enumerable.Empty<DetailedFund>());
                }


                for (int j = 1; j <= numberOfColumn - 1; j++)
                {
                    string campo = _driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/thead/tr[{1}]/th[{j}]")).Text;

                    if (orderColumnTableOfFunds[j - 1] != campo)
                    {
                        await LogUtils.LogActions(PageIdentification.FundsAndDividends, $"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}", _sessionId);
                        _logger.LogError($"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}");
                        return await Task.FromResult<IEnumerable<DetailedFund>>(detailedFunds);
                    }

                    Console.Write(_driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                }

                for (int i = 1; i <= numberOfLines; i++)
                {

                    if(AbortarProcesso())
                        return Enumerable.Empty<DetailedFund>();

                    string[] obj = new string[14];
                    obj[0] = i.ToString();

                    for (int j = 1; j <= numberOfColumn - 1; j++)
                    {
                        var e = _driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/tbody/tr[{i}]/td[{j}]")).Text;
                        obj[j] = e;
                    }

                    var fund = new DetailedFund()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        FundCode = obj[1],
                        Segment = obj[2],
                        Quotation = Convert.ToDouble(obj[3]),
                        FFOYield = Convert.ToDouble(obj[4].Replace("%", "")),
                        DividendYield = Convert.ToDouble(obj[5].Replace("%", "")),
                        PriceEquityValue = Convert.ToDouble(obj[6]),
                        ValueOfMarket = Convert.ToDouble(obj[7]),
                        Liquidity = Convert.ToDouble(obj[8]),
                        NumberOfProperties = Convert.ToDouble(obj[9]),
                        SquareMeterPrice = Convert.ToDouble(obj[10]),
                        RentPerSquareMeter = Convert.ToDouble(obj[11]),
                        CapRate = Convert.ToDouble(obj[12].Replace("%", "")),
                        AverageVacancy = Convert.ToDouble(obj[13].Replace("%", ""))
                    };

                    await LogUtils.LogActions(PageIdentification.FundsAndDividends, fund, _sessionId);
                    _logger.LogInformation("{@fund}", fund);

                    detailedFunds.Add(fund);

                }

                await LogUtils.LogActions(PageIdentification.FundsAndDividends, "Completed: Capture of FIIs", _sessionId);
                return await Task.FromResult<IEnumerable<DetailedFund>>(detailedFunds);

            }
            catch (System.Exception ex)
            {
                await LogUtils.LogActions(PageIdentification.FundsAndDividends, $"Error: Capture of FIIs -> {ex.Message}", _sessionId);
                _logger.LogError($"Error: Capture of FIIs -> {ex.Message}");
                return await Task.FromResult<IEnumerable<DetailedFund>>(detailedFunds);
            }
            finally{
                _driver.Quit();

                var elapsed = clock.Elapsed;
                var elapsedTime = string.Format("Tempo decorrido: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
                await LogUtils.LogActions(PageIdentification.FundsAndDividends, elapsedTime, _sessionId);
                
            }
        }
        public async Task<IEnumerable<FundDividend>> DriverGetFundsDividendsAsync(IEnumerable<DetailedFund> detailedFunds)
        {

            
            var clock = new Stopwatch();
            clock.Start();

            await LogUtils.LogActions(PageIdentification.FundsAndDividends, "Started: Capture of funds dividends", _sessionId);

            List<string> orderColumnTableOfFunds = new List<string>
            {
                "Última Data Com", "Tipo", "Data de Pagamento", "Valor"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;

            try
            {
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageAsync(_sessionId, JsonConvert.SerializeObject("Started: Capture Dividends Funds"));

                var fundsDividendsTmp = new List<FundDividend>();
                var fundsDividends = new List<FundDividend>();

                foreach (var fund in detailedFunds)
                {
                    
                    if(AbortarProcesso())
                        return Enumerable.Empty<FundDividend>();
                    
                    bool navigateOK = await GoToPage($"{WEBPAGE_DIVIDENDS}={fund.FundCode}");

                    if (!navigateOK)
                        return Enumerable.Empty<FundDividend>();

                    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

                    var tableResultadoExists = _driver.FindElements(By.Id("resultado"));

                    if (tableResultadoExists == null || tableResultadoExists.Count == 0)
                        continue;

                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='resultado']/tbody/tr")));

                    var rows = _driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr"));

                    var numberOfLines = rows.Count;
                    Console.WriteLine($"Total de linhas {numberOfLines}");

                    var columns = _driver.FindElements(By.XPath("//*[@id='resultado']/thead/tr/th"));

                    var numberOfColumn = columns.Count;
                    Console.WriteLine($"Total de colunas {numberOfColumn}");

                    if (totalOfColumnExpected != numberOfColumn)
                    {
                        await LogUtils.LogActions(PageIdentification.FundsAndDividends, $"Total of columns expected invalid {numberOfColumn}", _sessionId);
                        _logger.LogError($"Total of columns expected invalid {numberOfColumn}");
                        return await Task.FromResult<IEnumerable<FundDividend>>(Enumerable.Empty<FundDividend>());
                    }

                    Console.WriteLine(_driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/p/strong")).Text);

                    numberOfLines = numberOfLines > 12 ? 12 : numberOfLines;

                    for (int j = 1; j <= numberOfColumn; j++)
                    {
                        string campo = _driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text;

                        if (orderColumnTableOfFunds[j - 1] != campo)
                        {
                            Console.Write($"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}");
                            return await Task.FromResult<IEnumerable<FundDividend>>(Enumerable.Empty<FundDividend>());
                        }

                        Console.Write(_driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                    }

                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        
                        if(AbortarProcesso())
                            return Enumerable.Empty<FundDividend>();

                        string[] obj = new string[4];

                        for (int j = 1; j <= numberOfColumn; j++)
                        {
                            var e = _driver.FindElement(By.XPath($"//*[@id='resultado']/tbody/tr[{i}]/td[{j}]")).Text;
                            obj[j - 1] = e;
                        }

                        var fundDividends = new FundDividend()
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            FundCode = fund.FundCode,
                            LastComputedDate = Convert.ToDateTime(obj[0]).ToString("dd/MM/yyyy"),
                            Type = obj[1],
                            DatePayment = Convert.ToDateTime(obj[2]).ToString("dd/MM/yyyy"),
                            Value = Convert.ToDouble(obj[3])
                        };

                        await LogUtils.LogActions(PageIdentification.FundsAndDividends, fundDividends, _sessionId);
                        fundsDividendsTmp.Add(fundDividends);

                    }

                }

                if (fundsDividendsTmp.Count() == 0)
                    return await Task.FromResult<IEnumerable<FundDividend>>(fundsDividends);

                fundsDividends.AddRange(fundsDividendsTmp);
                fundsDividendsTmp.Clear();

                await LogUtils.LogActions(PageIdentification.FundsAndDividends, "Completed: Capture of funds dividends", _sessionId);

                return await Task.FromResult<IEnumerable<FundDividend>>(fundsDividends);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await LogUtils.LogActions(PageIdentification.FundsAndDividends, "Error: Capture of funds dividends", _sessionId);
                _logger.LogError("Error: Capture of funds dividends");
                return await Task.FromResult<IEnumerable<FundDividend>>(Enumerable.Empty<FundDividend>());
            }
            finally{

                _driver.Quit();

                var elapsed = clock.Elapsed;
                var elapsedTime = string.Format("Tempo decorrido: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);

                await LogUtils.LogActions(PageIdentification.FundsAndDividends, elapsedTime, _sessionId);

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
                    await LogUtils.LogActions(PageIdentification.FundsAndDividends, $"Error: GoToPage {ex.Message}", _sessionId);
                    _logger.LogError($"Error: GoToPage {ex.Message}");
                    return false;
                }
                    
                attempts++;

                return await GoToPage(linkPage);
                
            }
        }

        private bool AbortarProcesso(){
            
            if (_cancellationTokenSource is CancellationTokenSource cancellation)
            {
                if (cancellation.IsCancellationRequested)
                    return true;
                else
                    return false;
            }

            return false;
            
        }
    }
}