using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public static class RankOfTheBestFundsServiceTest
    {

        static RankOfTheBestFundsService rankOfTheBestFundsService = null;
        static Mock<RankOfTheBestFundsPersist> rankOfTheBestFundsPersist = null;
        static DetailedFundPersist detailedFundPersist = null;
        static DetailedFundService detailedFundService = null;
        static Mock<FundYeldsPersist> fundYeldsPersist = null;
        static FundsYieldService fundsYieldService = null;
        static InvestmentsContext ctx = null;

        public static async Task<bool> CreateContext()
        {

            var ContextOptions = new DbContextOptionsBuilder<InvestmentsContext>()
                                .UseSqlite($"Data Source=Test_RankOfTheBestFundsServiceTest.db")
                                .EnableSensitiveDataLogging().Options;
            
            ctx = new InvestmentsContext(ContextOptions);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            return await Task.FromResult(true);

        }

        public static void Setup()
        {   
            
            // var ctx = await ConfigureTest.ConfigureDatabase();
            rankOfTheBestFundsPersist = new Mock<RankOfTheBestFundsPersist>(ctx);
            fundYeldsPersist = new Mock<FundYeldsPersist>(ctx);
            detailedFundPersist = new DetailedFundPersist(ctx);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RankOfTheBestFunds, DetailedFunds>().ReverseMap();
            });

            var mapper = config.CreateMapper();

            rankOfTheBestFundsService = new RankOfTheBestFundsService(rankOfTheBestFundsPersist.Object, detailedFundService, 
                                                                      fundsYieldService, mapper);

        }


        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustStoreAndReturnTrue(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            await CreateContext();
            Setup();

            var result = await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
            
            Assert.True(result);

        }

        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustCalculateAndReturnNotNull(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            await CreateContext();
            Setup();
            await SeedDB();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            var result = await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
            
            Assert.NotNull(result);

        }

        public async static Task SeedDB()
        {

            dynamic funds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await detailedFundService.AddDetailedFundsAsync(funds);

            dynamic yelds = DummyTest.FundsYeld().ElementAt(0).ElementAt(0);
            await fundsYieldService.AddFundsYieldsAsync(yelds);

        }

        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustReturnRankOfTheBestFunds(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            await CreateContext();
            Setup();
            await SeedDB();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();

            var result = await rankOfTheBestFundsService.GetRankOfTheBestFundsAsync();
            
            Assert.NotNull(result);

        }
        
    }
}