using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Investments.Tests.Test.IntegrationTest
{
    public class WebScrapingFundsAndYeldsControllerTest
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;
        private static DbContextOptionsBuilder<InvestmentsContext> optionsBuilder = null;
        private static InvestmentsContext ctx = null;
        private static string dbName = null;

        public WebScrapingFundsAndYeldsControllerTest(ITestOutputHelper output)
        {
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            var test = (ITest)testMember.GetValue(output);
            dbName = test.TestCase.TestMethod.Method.Name;
        }

        public static async Task CreateContext()
        {

            optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(dbName);

            ctx = new InvestmentsContext(optionsBuilder.Options);
            ctx.Database.EnsureCreated();

            await Task.CompletedTask;

        }

        public async Task Setup()
        {
            
            await CreateContext();

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
            
            var detailedFunds = (List<DetailedFund>)DummyTest.DetailedFund().ElementAt(0).ElementAt(0);

            using (InvestmentsContext ctx = new(optionsBuilder.Options))
            {
                await ctx.AddRangeAsync(detailedFunds.ToArray());
                ctx.SaveChanges();
            }

        }


        [Fact]
        public async void MustCaptureTenfunds()
        {

            await Setup();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/WebScrapingFundsAndYelds/Funds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<DetailedFund> fundsYeld = JsonConvert.DeserializeObject<List<DetailedFund>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Fact]
        // [ConfigureTest]
        public async void MustCaptureFundsYields()
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/WebScrapingFundsAndYelds/Yelds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<FundDividend> fundsYeld = JsonConvert.DeserializeObject<List<FundDividend>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        
    }
}