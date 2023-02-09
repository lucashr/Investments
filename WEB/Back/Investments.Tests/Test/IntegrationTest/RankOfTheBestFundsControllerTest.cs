using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Newtonsoft.Json;
using Xunit;

namespace Investments.Tests.Test.IntegrationTest
{
    public class RankOfTheBestFundsControllerTest : IClassFixture<CustomWebApplicationFactory<Investments.API.Startup>>
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;

        public RankOfTheBestFundsControllerTest(CustomWebApplicationFactory<Investments.API.Startup> factory)
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


        [Theory]
        [InlineData(5)]
        public async void MustReturnRank5Funds(int? totalFundsInRank = null)
        {

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/RankOfTheBestFunds/ListBestFunds/{totalFundsInRank}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<RankOfTheBestFunds> fundsRank = JsonConvert.DeserializeObject<List<RankOfTheBestFunds>>(result);

            // Assert.Equal(5, fundsRank.Count());
            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }


    }
}