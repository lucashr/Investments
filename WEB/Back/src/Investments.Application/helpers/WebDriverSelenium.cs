using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Investments.Application.helpers
{
    public class WebDriverSelenium
    {
        public static IWebDriver ConfigDriver()
        {
            
            var options = new ChromeOptions();

            options.AddArgument("--headless"); // Executar sem abrir a interface do navegador
            options.AddArgument("--disable-gpu"); // Desativar GPU para evitar erros em algumas plataformas
            options.AddArgument("--no-sandbox"); // Evitar o uso de sandbox, útil em ambientes de produção
            options.AddArguments("--incognito"); //Inicia o navegador no modo de navegação anônima.
            options.AddArguments("--disable-blink-features=AutomationControlled");
            options.AddArguments("--disable-notifications"); //Desativa notificações enviadas por sites
            options.AddArguments("--disable-web-security"); // Desabilita a política de mesma origem (same-origin policy)
            options.AddArguments("--disable-extensions"); //Desativa todas as extensões do navegador
            options.AddArguments("--disable-infobars"); // Remove a barra de informações que aparece informando "Chrome is being controlled by automated test software"
            options.AddArgument("--disable-logging"); //Desativa a maioria dos logs do navegador
            options.AddArgument("start-maximized"); //Inicia o navegador com a janela maximizada
            options.AddArgument("--disable-dev-shm-usage"); // Desativa o uso do diretório /dev/shm para armazenamento temporário

            IWebDriver driver = new ChromeDriver(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

            return driver;

        }
    }
}