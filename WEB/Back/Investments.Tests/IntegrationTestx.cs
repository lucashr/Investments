using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Investments.API;
using Investments.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Investments.Tests
{
    public class IntegrationTestx
    {

        // protected readonly HttpClient TestClient;
        
        // protected IntegrationTestx()
        // {
            
        //     var appFactory = new WebApplicationFactory<Startup>()
        //         .WithWebHostBuilder(builder =>
        //             {
                        
        //                 builder.ConfigureServices(services =>
        //                 {
        //                     var descriptor = services.SingleOrDefault(
        //                     d => d.ServiceType ==
        //                         typeof(DbContextOptions<InvestmentsContext>));

        //                     if (descriptor != null)
        //                     {
        //                         services.Remove(descriptor);
        //                     }

        //                     services.AddDbContext<InvestmentsContext>(options => { options.UseInMemoryDatabase("Investments"); });
        //                 });
        //             });

        //     var clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
        //     {
        //         HandleCookies = false,
        //         BaseAddress = new Uri("https://localhost:5001"),
        //         AllowAutoRedirect = true,
        //         MaxAutomaticRedirections = 7,
        //     };
            
        //     TestClient = appFactory.CreateClient(clientOptions);
            
        // }

    }
}