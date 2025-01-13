using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Investments.Tests.Test.IntegrationTest
{
    public class FundYieldsControllerTest
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;
        private static FundDividendsPersist fundYeldsPersist = null;
        private static FundsDividendsService fundsYieldService = null;
        private static DbContextOptionsBuilder<InvestmentsContext> optionsBuilder = null;
        private static InvestmentsContext ctx = null;
        private static string dbName = null;

        public FundYieldsControllerTest(ITestOutputHelper output)
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

        public static async Task Setup()
        {

            await CreateContext();

            fundYeldsPersist = new FundDividendsPersist(ctx);
            fundsYieldService = new FundsDividendsService(fundYeldsPersist);
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
            
            dynamic fundsYeld = DummyTest.FundDividends().ElementAt(0).ElementAt(0);

            using (InvestmentsContext ctx = new(optionsBuilder.Options))
            {
                
                await ctx.AddRangeAsync(fundsYeld.ToArray());
                ctx.SaveChanges();
            }

        }

        [Theory]
        [ConfigureTest]
        [InlineData("AGRX11")]
        public async void MustGetFundYeldByCode(string fundCode)
        {
            
            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/FundYelds/{fundCode}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<FundDividends> fundsYeld = JsonConvert.DeserializeObject<List<FundDividends>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Fact]
        [ConfigureTest]
        public async void MustReturnAllFundsYelds()
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/FundYelds/AllFundsYeld";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<FundDividends> fundsYeld = JsonConvert.DeserializeObject<List<FundDividends>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundDividends), MemberType = typeof(DummyTest))]
        public async void MustEnterFundsYeldsAndReturnTrue(List<FundDividends> FundsYelds)
        {

            await Setup();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

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