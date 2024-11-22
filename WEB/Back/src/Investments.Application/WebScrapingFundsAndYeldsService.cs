using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Investments.Application
{
    public class WebScrapingFundsAndYeldsService : IWebScrapingFundsAndYeldsService, IDisposable
    {
        IWebDriver driver;
        IDetailedFundPersist _detailedFundPersist;
        IFundsPersist _fundsPersist;
        IFundsYeldPersist _fundsYeldPersist;
        CancellationTokenSource _cancellationTokenSource;
        

        const string WEBPAGE_FUNDS = "https://www.fundamentus.com.br/fii_resultado.php";
        const string WEBPAGE_YELDS = "https://www.fundamentus.com.br/fii_proventos.php?papel";

        public WebScrapingFundsAndYeldsService(IDetailedFundPersist detailedFundPersist,
                                               IFundsYeldPersist fundsYeldPersist)
        {
            _detailedFundPersist = detailedFundPersist;
            _fundsYeldPersist = fundsYeldPersist;
            // _fundsPersist = fundsPersist;

            ConfigDriver();
        }

        public async Task<IEnumerable<DetailedFunds>> GetFundsAsync(CancellationTokenSource cancellationTokenSource)
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
                new Exception("GetFundsAsync::Error SaveChangesAsync");
            }

            return result;

        }

        public async Task<IEnumerable<FundsYeld>> GetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds, CancellationTokenSource cancellationTokenSource)
        {

            _cancellationTokenSource = cancellationTokenSource;

            var result = await DriverGetYeldsFundsAsync(detailedFunds);

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                return result;
            }

            var bIsOk = await _fundsYeldPersist.AddFundsYieldsAsync(result.ToList());

            if (bIsOk == false)
            {
                throw new Exception("GetYeldsFundsAsync::Error SaveChangesAsync");
            }

            return result;

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

        public void ConfigDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");

            driver = new ChromeDriver(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        public void Dispose()
        {
            driver.Quit();
        }

        public async Task<IEnumerable<DetailedFunds>> DriverGetFundsAsync()
        {

            await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Started: Capture of FIIs"));

            GoToPage(WEBPAGE_FUNDS);

            List<string> orderColumnTableOfFunds = new List<string>
            {
                "Papel", "Segmento", "Cotação", "FFO Yield",
                "Dividend Yield", "P/VP", "Valor de Mercado",
                "Liquidez", "Qtd de imóveis", "Preço do m2",
                "Aluguel por m2", "Cap Rate", "Vacância Média",
                "Endereço"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;
            var detailedFunds = new List<DetailedFunds>();

            try
            {

                if(AbortarProcesso())
                        return Enumerable.Empty<DetailedFunds>();
                        
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='tabelaResultado']/tbody/tr")));

                var rows = driver.FindElements(By.XPath("//*[@id='tabelaResultado']/tbody/tr"));

                // #if DEBUG
                //     int numberOfLines = 10;
                // #else
                int numberOfLines = rows.Count;
                // #endif

                Console.WriteLine($"Total de linhas {numberOfLines}");

                var columns = driver.FindElements(By.XPath("//*[@id='tabelaResultado']/thead/tr/th"));

                int numberOfColumn = columns.Count;

                Console.WriteLine($"Total de colunas {numberOfColumn}");

                if (totalOfColumnExpected != numberOfColumn)
                {
                    Console.WriteLine($"Total of columns expected invalid {numberOfColumn}");
                }


                for (int j = 1; j <= numberOfColumn - 1; j++)
                {
                    string campo = driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/thead/tr[{1}]/th[{j}]")).Text;

                    if (orderColumnTableOfFunds[j - 1] != campo)
                    {
                        Console.Write($"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}");
                        return await Task.FromResult<IEnumerable<DetailedFunds>>(detailedFunds);
                    }

                    Console.Write(driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                }

                for (int i = 1; i <= numberOfLines; i++)
                {

                    if(AbortarProcesso())
                        return Enumerable.Empty<DetailedFunds>();

                    string[] obj = new string[14];
                    obj[0] = i.ToString();

                    for (int j = 1; j <= numberOfColumn - 1; j++)
                    {
                        var e = driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/tbody/tr[{i}]/td[{j}]")).Text;
                        obj[j] = e;
                    }

                    var fund = new DetailedFunds()
                    {
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

                    await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(fund));
                    Console.WriteLine($"{JsonConvert.SerializeObject(fund)}");

                    detailedFunds.Add(fund);
                }

                driver.Close();

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Completed: Capture of FIIs "));

                return await Task.FromResult<IEnumerable<DetailedFunds>>(detailedFunds);

            }
            catch (System.Exception ex)
            {
                driver.Close();
                Console.WriteLine(ex.Message);
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Error: Capture of FIIs"));
                return await Task.FromResult<IEnumerable<DetailedFunds>>(detailedFunds);
            }
        }

        public async Task<IEnumerable<FundsYeld>> DriverGetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {

            var fundsYelds = new List<FundsYeld>();
            var fundsYeldsTmp = new List<FundsYeld>();
            var totalFundYeldsDb = new List<FundsYeld>();

            dynamic rows;
            dynamic columns;

            int numberOfLines = 0;
            int numberOfColumn = 0;

            List<string> orderColumnTableOfFunds = new List<string>
            {
                "Última Data Com", "Tipo", "Data de Pagamento", "Valor"
            };

            int totalOfColumnExpected = orderColumnTableOfFunds.Count;

            try
            {

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Started: Capture Yelds Funds"));

                foreach (var fund in detailedFunds)
                {
                    
                    if(AbortarProcesso())
                        return Enumerable.Empty<FundsYeld>();
                    
                    GoToPage($"{WEBPAGE_YELDS}={fund.FundCode}");

                    try
                    {

                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='resultado']/tbody/tr")));

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

                    if (numberOfLines > 12)
                    {
                        numberOfLines = 12;
                    }

                    for (int j = 1; j <= numberOfColumn; j++)
                    {
                        string campo = driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text;

                        if (orderColumnTableOfFunds[j - 1] != campo)
                        {
                            Console.Write($"Ordem inválida! {orderColumnTableOfFunds[j - 1]} esperado {campo}");
                            return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
                        }

                        Console.Write(driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                    }

                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        
                        if(AbortarProcesso())
                            return Enumerable.Empty<FundsYeld>();

                        string[] obj = new string[4];

                        for (int j = 1; j <= numberOfColumn; j++)
                        {
                            var e = driver.FindElement(By.XPath($"//*[@id='resultado']/tbody/tr[{i}]/td[{j}]")).Text;
                            obj[j - 1] = e;
                        }

                        var fY = new FundsYeld()
                        {
                            FundCode = fund.FundCode,
                            LastComputedDate = Convert.ToDateTime(obj[0]).AddDays(5),
                            Type = obj[1],
                            DatePayment = Convert.ToDateTime(obj[2]),
                            Value = Convert.ToDouble(obj[3])
                        };

                        Console.WriteLine($"{JsonConvert.SerializeObject(fY)}");
                        await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(fY));

                        fundsYeldsTmp.Add(fY);

                    }

                }

                if (fundsYeldsTmp.Count() == 0)
                {
                    return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
                }

                fundsYelds.AddRange(fundsYeldsTmp);

                fundsYeldsTmp.Clear();
                totalFundYeldsDb.Clear();

                driver.Close();

                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Completed: Capture of yelds"));

                return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
            }
            catch (System.Exception ex)
            {
                driver.Close();
                Console.WriteLine(ex.Message);
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject("Error: Capture of yelds"));
                return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
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