using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
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

        public static void Setup()
        {
            fundsPersist = new Mock<FundsPersist>(ConfigureTest._context);
            iGeneralPersist = new Mock<GeneralPersist>(ConfigureTest._context);
            iMapper = new Mock<IMapper>();
            fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);
        }

        public async static Task SeedDB()
        {

            dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await fundsService.AddFundsAsync(detailedFunds);
            await DetailedFundServiceTest.MustEnterTenFunds(detailedFunds);

        }
        
        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async Task MustEnterTenFunds(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            var result = await fundsService.AddFundsAsync(detailedFunds);

            Assert.True(result);

        }

        [Theory]
        [InlineData("AIEC11", "TESTE")]
        [ConfigureTest]
        public static async Task MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            
            Setup();
            await SeedDB();

            // dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);

            // await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.UpdateFundByCodeAsync(oldFundCode, newFundCode);

            Assert.Equal("TESTE", fund.FundCode);

        }

        [Theory]
        [InlineData("ABCDE")]
        [ConfigureTest]
        public static async Task MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {

            Setup();
            await SeedDB();

            // dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);

            // await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.AddFundAsync(fundCode);

            Assert.Equal("ABCDE", fund.FundCode);
        }

        [Theory]
        [InlineData("AFOF11")]
        [ConfigureTest]
        public static async Task MustReturnOneFund(string fundCode)
        {

            Setup();
            await SeedDB();

            // dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);

            // await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.GetFundByCodeAsync(fundCode);

            Assert.Equal("AFOF11", fund.FundCode);
        }

        [Fact]
        [ConfigureTest]
        public static async Task MustReturnAllFunds()
        {

            Setup();
            await SeedDB();

            // var result = await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.GetAllFundsAsync();

            Assert.True(fund.Count() == 10);
        }

        [Theory]
        [InlineData("AFHI11")]
        [ConfigureTest]
        public static async Task MustDeleteFundEReturnTrue(string fundCode)
        {

            Setup();
            await SeedDB();

            // dynamic detailedFunds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);

            // await fundsService.AddFundsAsync(detailedFunds);

            var result = await fundsService.DeleteFundByCodeAsync(fundCode);

            Assert.Equal(true, result);
        }

    }
}