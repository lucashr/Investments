using System.Linq;
using Moq;
using Xunit;
using Investments.Application;
using Investments.Persistence;
using System.Collections.Generic;
using Investments.Domain.Models;
using System;
using Investments.Tests.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Investments.Persistence.Contexts;
using System.Threading;

namespace Investments.Tests.Test
{
    public static class FundYeldsServiceTest
    {

        static Mock<FundYeldsPersist> fundYeldsPersist = null;
        static FundsYieldService fundsYieldService = null;
        static InvestmentsContext ctx = null;

        public static async Task<bool> CreateContext()
        {

            var ContextOptions = new DbContextOptionsBuilder<InvestmentsContext>()
                                .UseSqlite($"Data Source=Test_FundYeldsServiceTest.db")
                                .EnableSensitiveDataLogging().Options;
            
            ctx = new InvestmentsContext(ContextOptions);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            return await Task.FromResult(true);

        }

        public static void Setup()
        {

            // var ctx = await ConfigureTest.ConfigureDatabase();
            fundYeldsPersist = new Mock<FundYeldsPersist>(ctx);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);

        }

        public async static Task SeedDB()
        {

            dynamic fundsYeld = DummyTest.FundsYeld().ElementAt(0).ElementAt(0);
            await fundsYieldService.AddFundsYieldsAsync(fundsYeld);

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustEnterThirtyFundsYeldsAndReturnTrue(List<FundsYeld> fundsYelds)
        {

            await CreateContext();
            Setup();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            Assert.True(resutl);
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustReturnThirtyFundsYelds(List<FundsYeld> fundsYelds)
        {

            await CreateContext();
            Setup();
            await SeedDB();

            // await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var result = await fundsYieldService.GetAllFundsYeldAsync();

            Assert.Equal(30, result.Count());
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustReturnFundYeldByCode(List<FundsYeld> fundsYelds)
        {

            await CreateContext();
            Setup();
            await SeedDB();

            var yelds = await fundsYieldService.GetFundYeldByCodeAsync("AAZQ11");
            
            Assert.Equal("AAZQ11", yelds.Select(x=>x.FundCode).FirstOrDefault());
            Assert.True(yelds.Count() > 0);

        }

    }
}