using System.Linq;
using Moq;
using Xunit;
using Investments.Application;
using Investments.Persistence;
using System.Collections.Generic;
using Investments.Domain.Models;
using System;
using Investments.Tests.Helpers;

namespace Investments.Tests.Test
{
    public class FundYeldsServiceTest
    {

        static Mock<FundYeldsPersist> fundYeldsPersist = null;
        static FundsYieldService fundsYieldService = null;

        public void Setup()
        {
            fundYeldsPersist = new Mock<FundYeldsPersist>(ConfigureTest._context);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public async void MustEnterSixFundsYeldsAndReturnTrue(List<FundsYeld> fundsYelds)
        {

            Setup();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            Assert.True(resutl);
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public async void MustReturnSixFundsYelds(List<FundsYeld> fundsYelds)
        {

            Setup();

            await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var resutl = await fundsYieldService.GetAllFundsYeldAsync();

            Assert.True(resutl.Count() == 30);
        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public async void MustReturnFundYeldByCode(List<FundsYeld> fundsYelds)
        {

            Setup();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var yelds = await fundsYieldService.GetFundYeldByCodeAsync("AAZQ11");
            
            Assert.Equal("AAZQ11", yelds.Select(x=>x.FundCode).FirstOrDefault());
            Assert.True(yelds.Count() > 0);

        }

    }
}