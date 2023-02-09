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
    public class DetailedFundsControllerTest : IClassFixture<CustomWebApplicationFactory<Investments.API.Startup>>
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;

        public DetailedFundsControllerTest(CustomWebApplicationFactory<Investments.API.Startup> factory)
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


        public async Task SeedDB()
        {
            
            dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await DetailedFundServiceTest.MustEnterTenFunds(detailedFunds);

        }

        [Fact]
        [ConfigureTest]
        public async void MustReturnAllDetailedFunds ()
        {
            
            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            string url = $"api/DetailedFunds/AllFunds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<DetailedFunds> funds = JsonConvert.DeserializeObject<List<DetailedFunds>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);


        }

        [Theory]
        [InlineData("AAZQ11")]
        [ConfigureTest]
        public async void MustReturnDetailedFundByCode (string fundCode)
        {

            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            string url = $"api/DetailedFunds/{fundCode}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            DetailedFunds fund = JsonConvert.DeserializeObject<DetailedFunds>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        public async void MustEnterFourDetailedFundsAndReturnTrue(List<DetailedFunds> newfunds)
        {

            HttpClient client = _factory.CreateClient(clientOptions);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json")
            );

            var jsonContent = JsonConvert.SerializeObject(newfunds); 
            var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json-patch+json");

            string url = $"api/DetailedFunds/Registration";

            var response = await client.PostAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            var resultDb = JsonConvert.DeserializeObject<bool>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

    }

}