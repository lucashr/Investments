using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public static class FundsServiceTest
    {

        static Mock<FundsPersist> fundsPersist = null;
        static Mock<GeneralPersist> iGeneralPersist = null;
        static Mock<IMapper> iMapper = null;
        static FundsService fundsService = null;
        static InvestmentsContext ctx = null;

        public static async Task<bool> CreateContext()
        {

            var ContextOptions = new DbContextOptionsBuilder<InvestmentsContext>()
                                .UseSqlite($"Data Source=Test_FundsServiceTest.db")
                                .EnableSensitiveDataLogging().Options;
            
            ctx = new InvestmentsContext(ContextOptions);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            return await Task.FromResult(true);

        }
        
        public static void Setup()
        {

            // var ctx = await ConfigureTest.ConfigureDatabase();
            fundsPersist = new Mock<FundsPersist>(ctx);
            iGeneralPersist = new Mock<GeneralPersist>(ctx);
            iMapper = new Mock<IMapper>();
            fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);
            
        }

        public async static Task SeedDB()
        {

            dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await fundsService.AddFundsAsync(detailedFunds);

        }
        
        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustEnterTenFunds(List<DetailedFunds> detailedFunds)
        {
            
            await CreateContext();
            Setup();

            var result = await fundsService.AddFundsAsync(detailedFunds);

            Assert.True(result);

        }

        [Theory]
        [InlineData("AIEC11", "TESTE")]
        // [ConfigureTest]
        public static async Task MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            
            await CreateContext();
            Setup();
            await SeedDB();

            Funds fund = await fundsService.UpdateFundByCodeAsync(oldFundCode, newFundCode);

            Assert.Equal("TESTE", fund.FundCode);

        }

        [Theory]
        [InlineData("ZZZZ")]
        // [ConfigureTest]
        public static async Task MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {

            await CreateContext();
            Setup();
            await SeedDB();

            var fund = await fundsService.AddFundAsync(fundCode);

            Assert.Equal("ZZZZ", fund.FundCode);
        }

        [Theory]
        [InlineData("AFOF11")]
        // [ConfigureTest]
        public static async Task MustReturnOneFund(string fundCode)
        {

            await CreateContext();
            Setup();
            await SeedDB();

            var fund = await fundsService.GetFundByCodeAsync(fundCode);

            Assert.Equal("AFOF11", fund.FundCode);
        }

        [Fact]
        // [ConfigureTest]
        public static async Task MustReturnAllFunds()
        {

            await CreateContext();
            Setup();
            await SeedDB();

            var fund = await fundsService.GetAllFundsAsync();

            Assert.True(fund.Count() == 10);
        }

        [Theory]
        [InlineData("AFHI11")]
        // [ConfigureTest]
        public static async Task MustDeleteFundEReturnTrue(string fundCode)
        {

            await CreateContext();
            Setup();
            await SeedDB();

            var result = await fundsService.DeleteFundByCodeAsync(fundCode);

            Assert.Equal(true, result);
        }

    }
}