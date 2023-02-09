using System;
using System.Collections.Generic;
using System.Net.Http;
using Investments.Domain.Models;
using Investments.Tests.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace Investments.Tests.Test.IntegrationTest
{
    public class FundYieldsControllerTest : IClassFixture<CustomWebApplicationFactory<Investments.API.Startup>>
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;

        public FundYieldsControllerTest(CustomWebApplicationFactory<Investments.API.Startup> factory)
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
        [InlineData("AGRX11")]
        public async void MustGetFundYeldByCode(string fundCode)
        {

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/FundYelds/{fundCode}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<FundsYeld> fundsYeld = JsonConvert.DeserializeObject<List<FundsYeld>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async void MustReturnAllFundsYelds()
        {

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/FundYelds/AllFundsYeld";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<FundsYeld> fundsYeld = JsonConvert.DeserializeObject<List<FundsYeld>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        public async void MustEnterFundsYeldsAndReturnTrue(List<FundsYeld> FundsYelds)
        {

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            var jsonContent = JsonConvert.SerializeObject(FundsYelds); 
            var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json-patch+json");
            
            string url = $"api/FundYelds/Registration";

            var response = await client.PostAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

    }
}