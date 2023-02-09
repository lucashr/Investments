using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Tests.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace Investments.Tests.Test.IntegrationTest
{
    public class WebScrapingFundsAndYeldsControllerTest : IClassFixture<CustomWebApplicationFactory<Investments.API.Startup>>
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;

        public WebScrapingFundsAndYeldsControllerTest(CustomWebApplicationFactory<Investments.API.Startup> factory)
        {
            _factory = factory;
        }

        static dynamic clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
        {
            HandleCookies = false,
            BaseAddress = new Uri("https://localhost:5001"),
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 7,
        };

        [Fact]
        public async void MustCaptureTenfunds()
        {

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/WebScrapingFundsAndYelds/Funds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<DetailedFunds> fundsYeld = JsonConvert.DeserializeObject<List<DetailedFunds>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        public async Task SeedDB()
        {
            
            dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await DetailedFundServiceTest.MustEnterTenFunds(detailedFunds);

        }

        [Fact]
        [ConfigureTest]
        public async void MustCaptureFundsYields()
        {

            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/WebScrapingFundsAndYelds/Yelds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<FundsYeld> fundsYeld = JsonConvert.DeserializeObject<List<FundsYeld>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        
    }
}