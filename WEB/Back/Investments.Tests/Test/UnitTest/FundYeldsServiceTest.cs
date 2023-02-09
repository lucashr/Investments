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

namespace Investments.Tests.Test
{
    public static class FundYeldsServiceTest
    {

        static Mock<FundYeldsPersist> fundYeldsPersist = null;
        static FundsYieldService fundsYieldService = null;

        public static void Setup()
        {
            fundYeldsPersist = new Mock<FundYeldsPersist>(ConfigureTest._context);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);
        }

        public async static Task SeedDB()
        {

            dynamic detailedFunds = DummyTest.FundsYeld().ElementAt(0).ElementAt(0);
            await fundsYieldService.AddFundsYieldsAsync(detailedFunds);

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async Task MustEnterThirtyFundsYeldsAndReturnTrue(List<FundsYeld> fundsYelds)
        {

            Setup();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            Assert.True(resutl);
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async void MustReturnThirtyFundsYelds(List<FundsYeld> fundsYelds)
        {

            Setup();
            await SeedDB();

            await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var resutl = await fundsYieldService.GetAllFundsYeldAsync();

            Assert.True(resutl.Count() == 30);
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async void MustReturnFundYeldByCode(List<FundsYeld> fundsYelds)
        {

            Setup();
            await SeedDB();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var yelds = await fundsYieldService.GetFundYeldByCodeAsync("AAZQ11");
            
            Assert.Equal("AAZQ11", yelds.Select(x=>x.FundCode).FirstOrDefault());
            Assert.True(yelds.Count() > 0);

        }

    }
}