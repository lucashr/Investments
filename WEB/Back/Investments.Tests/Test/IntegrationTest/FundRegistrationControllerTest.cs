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
    public class FundRegistrationControllerTest : IClassFixture<CustomWebApplicationFactory<Investments.API.Startup>>
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;

    
        public FundRegistrationControllerTest(CustomWebApplicationFactory<Investments.API.Startup> factory)
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
            await FundsServiceTest.MustEnterTenFunds(detailedFunds);

        }

        //Deveria retornar algum erro informando que a API não existe!
        // [Theory]
        // [InlineData("BBBB")]
        // public async void MustTryEnterAndReturnError(string fundCode)
        // {
        //     

        //     HttpClient client = _factory.CreateClient(clientOptions);
            
        //     client.DefaultRequestHeaders.Accept.Clear();
        //     client.DefaultRequestHeaders.Accept.Add( 
        //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
        //     Funds fund = new Funds(){Id = 1, FundCode = fundCode};

        //     var jsonContent = JsonConvert.SerializeObject(fund); 
        //     var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "appication/json");
            
        //     string url = $"api/FundRegistration/FundRegistration?fundCods={fundCode}";

        //     var response = await client.PostAsync(url, contentString);

        //     string result = await response.Content.ReadAsStringAsync();

        //     Funds returnFund = JsonConvert.DeserializeObject<Funds>(result);

        //     Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
        //         System.Net.HttpStatusCode.NotFound);
                
        // }        

        [Theory]
        [ConfigureTest]
        [MemberData(nameof(DummyTest.FundCodes), MemberType = typeof(DummyTest))]
        public async void MustInsertAndReturnMessageOK(List<string> fundCodes)
        {

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            var jsonContent = JsonConvert.SerializeObject(fundCodes); 
            var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json-patch+json");
            
            string url = $"api/FundRegistration/FundsRegistration?fundCodes={jsonContent}";

            var response = await client.PostAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Theory]
        [ConfigureTest]
        [InlineData("AFCR11")]
        public async void MustDeleteAndReturnTrue(string fundCode)
        {

            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            // dynamic fundCodes = DummyTest.FundCodes().ElementAt(0).ElementAt(0);

            // var jsonContent = JsonConvert.SerializeObject(fundCodes); 
            // var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json-patch+json");
            
            // string url = $"api/FundRegistration/FundsRegistration?fundCodes={jsonContent}";

            // var response = await client.PostAsync(url, contentString);
            string url = $"api/FundRegistration/DeleteFundByCode/{fundCode}";

            var response = await client.DeleteAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Theory]
        [ConfigureTest]
        [InlineData("AEFI11", "EDCBA")]
        public async void MustUpdateAndReturnModifiedFund(string oldFundCode, string newFundCode)
        {

            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
            
            var contentString  = new StringContent($"{oldFundCode}/{newFundCode}", System.Text.Encoding.UTF8, "text/plain");

            string url = $"api/FundRegistration/UpdateFundByCode/{oldFundCode}/{newFundCode}";

            var response = await client.PutAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            Funds funds = JsonConvert.DeserializeObject<Funds>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Theory]
        [ConfigureTest]
        [InlineData("ABCP11")]
        public async void MustGetFundByCode(string fundCode)
        {

            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            string url = $"api/FundRegistration/GetFundbyCode/{fundCode}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Fact]
        [ConfigureTest]
        public async void MustReturnOneListFunds()
        {

            await SeedDB();

            HttpClient client = _factory.CreateClient(clientOptions);
            
            string url = $"api/FundRegistration/AllFunds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<Funds> funds = JsonConvert.DeserializeObject<List<Funds>>(result);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/plain; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }
    }
}