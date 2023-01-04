using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Investments.Test.Contracts;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.IO;
using System.Linq;
using Investments.Persistence;
using System.Net.WebSockets;
using System.Threading;
using System.Text;

namespace Investments.Test
{
    public class WebScrapingFundsAndYeldsService : ConfigureTest, IWebScrapingFundsAndYeldsService
    {
        IWebDriver driver;

        FundsPersist _fundsYeldPersist;

        public Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM [DetailedFunds]");
                
                _context.AddRange(detailedFunds);
                _context.SaveChanges();

                return Task.FromResult(true);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        public Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM [FundsYeld]");
                _context.AddRange(fundsYelds);
                _context.SaveChanges();

                return Task.FromResult(true);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<DetailedFunds>> GetFundsAsync()
        {
            
            GoToPage("https://www.fundamentus.com.br/fii_resultado.php");

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

                    var wait60 = new WebDriverWait(driver, new TimeSpan(0, 0, 60, 0));
                    var delay4 = new TimeSpan(0, 0, 0, 7, 0);
                    var timestamp4 = DateTime.Now;
                    
                    wait60.Until(driver => (DateTime.Now - timestamp4) > delay4);

                    var rows = driver.FindElements(By.XPath("//*[@id='tabelaResultado']/tbody/tr"));
                    int numberOfLines = rows.Count;
                    
                    Console.WriteLine($"Total de linhas {numberOfLines}");
                    
                    var columns = driver.FindElements(By.XPath("//*[@id='tabelaResultado']/thead/tr/th"));
                    int numberOfColumn = columns.Count;
                    
                    Console.WriteLine($"Total de colunas {numberOfColumn}");

                    if(totalOfColumnExpected != numberOfColumn)
                    {
                        Console.WriteLine($"Total of columns expected invalid {numberOfColumn}");
                    }

                    for (int j = 1; j <= numberOfColumn - 1; j++)
                    {
                        string campo = driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/thead/tr[{1}]/th[{j}]")).Text;
                        
                        if(orderColumnTableOfFunds[j-1] != campo)
                        {
                            Console.Write($"Ordem inválida! {orderColumnTableOfFunds[j-1]} esperado {campo}");
                            return await Task.FromResult<IEnumerable<DetailedFunds>>(detailedFunds);
                        }

                        Console.Write(driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                    }

                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        string[] obj = new string[14];
                        obj[0] = i.ToString();

                        for (int j = 1; j <= numberOfColumn - 1; j++)
                        {
                            var e = driver.FindElement(By.XPath($"//*[@id='tabelaResultado']/tbody/tr[{i}]/td[{j}]")).Text;
                            obj[j] = e;
                        }

                        var fund = new DetailedFunds(){
                            FundCode = obj[1], Segment = obj[2], Quotation = Convert.ToDouble(obj[3]),
                            FFOYield = Convert.ToDouble(obj[4].Replace("%", "")), DividendYield = Convert.ToDouble(obj[5].Replace("%", "")), 
                            PriceEquityValue = Convert.ToDouble(obj[6]), ValueOfMarket = Convert.ToDouble(obj[7]),
                            Liquidity = Convert.ToDouble(obj[8]), NumberOfProperties = Convert.ToDouble(obj[9]), 
                            SquareMeterPrice = Convert.ToDouble(obj[10]), RentPerSquareMeter = Convert.ToDouble(obj[11]),
                            CapRate = Convert.ToDouble(obj[12].Replace("%", "")), AverageVacancy = Convert.ToDouble(obj[13].Replace("%", ""))
                        };
                        
                        detailedFunds.Add(fund);
                    }

                driver.Close();

                return await Task.FromResult<IEnumerable<DetailedFunds>>(detailedFunds);  

            }
            catch (System.Exception ex)
            {
                driver.Close();
                Console.WriteLine(ex.Message);
                return await Task.FromResult<IEnumerable<DetailedFunds>>(detailedFunds);
            }

        }

        public async Task<IEnumerable<FundsYeld>> GetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            
            var fundsYelds = new List<FundsYeld>();
            var fundsYeldsTmp = new List<FundsYeld>();
            var totalFundYeldsDb = new List<FundsYeld>();
            
            dynamic lastDateDB = null;
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

                foreach (var fund in detailedFunds)
                {

                    GoToPage($"https://www.fundamentus.com.br/fii_proventos.php?papel={fund.FundCode}");

                    try
                    {
                    
                        var wait10 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        wait10.Until(driver => driver.FindElement(By.XPath("//*[@id='resultado']/tbody/tr")));

                        rows = driver.FindElements(By.XPath("//*[@id='resultado']/tbody/tr"));
                        numberOfLines = rows.Count;
                        
                        Console.WriteLine($"Total de linhas {numberOfLines}");

                        columns = driver.FindElements(By.XPath("//*[@id='resultado']/thead/tr/th"));
                        numberOfColumn = columns.Count;
                        
                        Console.WriteLine($"Total de colunas {numberOfColumn}");

                        if(totalOfColumnExpected != numberOfColumn)
                        {
                            Console.WriteLine($"Total of columns expected invalid {numberOfColumn}");
                        }
                        
                        Console.WriteLine(driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/p/strong")).Text);
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }

                    if(numberOfLines > 12)
                    {
                        numberOfLines = 12;
                    }

                    for (int j = 1; j <= numberOfColumn; j++)
                    {
                        string campo = driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text;

                        if(orderColumnTableOfFunds[j-1] != campo)
                        {
                            Console.Write($"Ordem inválida! {orderColumnTableOfFunds[j-1]} esperado {campo}");
                            return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
                        }

                        Console.Write(driver.FindElement(By.XPath($"//*[@id='resultado']/thead/tr[{1}]/th[{j}]")).Text + " | ");
                    }

                    var result = await _fundsYeldPersist.GetFundYeldByCodeAsync(fund.FundCode.ToUpper().Trim());

                    if(result != null)
                    {
                        totalFundYeldsDb = result.ToList();

                        lastDateDB = result.OrderByDescending(x=> x.LastComputedDate).Take(1).Select(x=>x.LastComputedDate).FirstOrDefault(); 
                    }
                    
                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        
                        string[] obj = new string[4];

                        for (int j = 1; j <= numberOfColumn; j++)
                        {
                            var e = driver.FindElement(By.XPath($"//*[@id='resultado']/tbody/tr[{i}]/td[{j}]")).Text;
                            obj[j - 1] = e;
                        }

                        var fY = new FundsYeld(){
                            FundCode = fund.FundCode,
                            LastComputedDate = Convert.ToDateTime(obj[0]).AddDays(5),
                            Type = obj[1],
                            DatePayment = Convert.ToDateTime(obj[2]),
                            Value = Convert.ToDouble(obj[3])
                        };

                        if(lastDateDB != null)
                        {
                            if(lastDateDB >= fY.LastComputedDate)
                            {
                                break;
                            }
                        }

                        fundsYeldsTmp.Add(fY);

                    }

                }

                if(fundsYeldsTmp.Count() == 0)
                {
                    return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
                }

                if(totalFundYeldsDb.Count > 0)
                {
                    fundsYeldsTmp = fundsYeldsTmp.OrderByDescending(x=>x.LastComputedDate).ToList();
                
                    int totalItemsNew = fundsYeldsTmp.Count();
                    int totalItemsDb = totalFundYeldsDb.Count();
                    int totalItems = totalItemsNew + totalItemsDb;

                    if(totalItems > 12)
                    {
                        int totalItemsToRemove = totalItems - 12;
                        var removeItems = totalFundYeldsDb.OrderBy(x => x.LastComputedDate).Take(totalItemsToRemove);
                        _context.RemoveRange(removeItems.ToArray());
                    }
                }

                fundsYelds.AddRange(fundsYeldsTmp);
                
                fundsYeldsTmp.Clear();
                totalFundYeldsDb.Clear();

                driver.Close();

                return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds); 
            }
            catch (System.Exception ex)
            {
                driver.Close();
                Console.WriteLine(ex.Message);
                return await Task.FromResult<IEnumerable<FundsYeld>>(fundsYelds);
            }
             
        }

        public async Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            try
            {

                List<Funds> funds = new List<Funds>();

                foreach (var item in detailedFunds)
                {

                    var fund = new Funds(){
                        FundCode = item.FundCode
                    };

                    funds.Add(fund);

                }

                _context.Database.ExecuteSqlRaw("DELETE FROM [Funds]");
                _context.AddRange(funds);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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
    }
}