using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Investments.API
{
    public partial class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug() // Define o n�vel m�nimo de log
                    .WriteTo.Console()    // Escreve logs no console
                    .WriteTo.Seq("http://localhost:5341") // Envia logs para o Seq
                    .Enrich.FromLogContext() // Enriquecimento autom�tico de logs
                    .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
