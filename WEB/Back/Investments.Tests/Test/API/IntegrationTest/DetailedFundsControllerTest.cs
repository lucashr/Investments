using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Investments.Tests.Tests.API.IntegrationTest
{
    public class DetailedFundsControllerTest
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;
        private static DetailedFundPersist detailedFundPersist = null;
        private static DetailedFundService detailedFundService = null;
        private static Mock<IGeneralPersist> iGeneralPersist = null;
        private static Mock<IMapper> iMapper = null;
        private static InvestmentsContext ctx = null;
        private static DbContextOptionsBuilder<InvestmentsContext> optionsBuilder = null;
        private static string dbName = null;

        static WebApplicationFactoryClientOptions clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
        {
            HandleCookies = false,
            BaseAddress = new Uri("https://localhost:5001"),
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 7,
        };

        public DetailedFundsControllerTest(ITestOutputHelper output)
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

            await Task.CompletedTask;
        }

        public static async Task Setup()
        {
            
            await CreateContext();

            detailedFundPersist = new DetailedFundPersist(ctx);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            iGeneralPersist = new Mock<IGeneralPersist>();
            iMapper = new Mock<IMapper>();

        }

        public async Task SeedDB()
        {

            var funds = (List<DetailedFund>)DummyTest.BestFundRank().ElementAt(0).ElementAt(0);

            using (InvestmentsContext ctx = new(optionsBuilder.Options))
            {
                await ctx.AddRangeAsync(funds.ToArray());
                ctx.SaveChanges();
            }

        }

        [Fact]
        // [ConfigureTes t]
        public async void MustReturnAllDetailedFunds ()
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            string url = $"api/DetailedFund/AllFunds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<DetailedFund> funds = JsonConvert.DeserializeObject<List<DetailedFund>>(result);

            funds.Should().HaveCountGreaterThan(0);

        }

        [Theory]
        [InlineData("AAZQ11")]
        // [ConfigureTest]
        public async void MustReturnDetailedFundByCode (string fundCode)
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            string url = $"api/DetailedFund/{fundCode}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            DetailedFund fund = JsonConvert.DeserializeObject<DetailedFund>(result);

            fund.FundCode.Should().Be(fundCode);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFund), MemberType = typeof(DummyTest))]
        public async void MustEnterFourDetailedFundsAndReturnTrue(List<DetailedFund> newfunds)
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json")
            );

            var jsonContent = JsonConvert.SerializeObject(newfunds); 
            var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json-patch+json");

            string url = $"api/DetailedFund/Registration";

            var response = await client.PostAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            var resultDb = JsonConvert.DeserializeObject<bool>(result);

            resultDb.Should().Be(true);

        }

    }

}