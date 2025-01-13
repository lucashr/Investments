using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application;
using Investments.Application.Contracts;
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
    public class RankOfTheBestFundsControllerTest
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;
        private static DbContextOptionsBuilder<InvestmentsContext> optionsBuilder = null;
        private static InvestmentsContext ctx = null;
        private static RankOfTheBestFundsPersist rankOfTheBestFundsPersist = null;
        private static RankOfTheBestFundsService rankOfTheBestFundsService = null;
        private static DetailedFundPersist detailedFundPersist = null;
        private static DetailedFundService detailedFundService = null;
        private static FundDividendsPersist fundYeldsPersist = null;
        private static FundsDividendsService fundsYeldService = null;
        private static string dbName = null;

        public RankOfTheBestFundsControllerTest(ITestOutputHelper output)
        {
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            var test = (ITest)testMember.GetValue(output);
            dbName = test.TestCase.TestMethod.Method.Name;
        }

        static dynamic clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
        {
            HandleCookies = false,
            BaseAddress = new Uri("https://localhost:5001"),
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 7,
        };

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

            rankOfTheBestFundsPersist = new RankOfTheBestFundsPersist(ctx);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RankOfTheBestFunds, DetailedFunds>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();

            fundYeldsPersist = new FundDividendsPersist(ctx);
            detailedFundPersist = new DetailedFundPersist(ctx);

            detailedFundService = new DetailedFundService(detailedFundPersist);
            fundsYeldService = new FundsDividendsService(fundYeldsPersist);

            rankOfTheBestFundsService = new RankOfTheBestFundsService(rankOfTheBestFundsPersist, detailedFundService, fundsYeldService, mapper);

        }

        public async Task SeedDB()
        {

            dynamic rankFunds = DummyTest.RankOfTheBestFunds().ElementAt(0).ElementAt(0);

            using (InvestmentsContext ctx = new(optionsBuilder.Options))
            {
                await ctx.AddRangeAsync(rankFunds.ToArray());
                ctx.SaveChanges();
            }

        }

        [Theory]
        // [ConfigureTest]
        [InlineData(5)]
        public async void MustReturnRank5Funds(int? totalFundsInRank = null)
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            string url = $"api/RankOfTheBestFunds/ListBestFunds/{totalFundsInRank}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            List<RankOfTheBestFunds> fundsRank = JsonConvert.DeserializeObject<List<RankOfTheBestFunds>>(result);

            Assert.Equal(response.EnsureSuccessStatusCode().StatusCode, 
                System.Net.HttpStatusCode.OK);

        }


    }
}