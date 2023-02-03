using System.Linq;
using Moq;
using Xunit;
using Investments.Application;
using Investments.Persistence;
using System.Collections.Generic;
using Investments.Domain.Models;
using System;

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

        public static IEnumerable<object[]> FundsYeld(){

            yield return new object[]
            {
                new List<FundsYeld>()
                {
                    new FundsYeld() { FundCode = "ABCD", DatePayment = DateTime.Now.AddDays(20), LastComputedDate = DateTime.Now, Value = 1.90, Type = "DIVIDENDO" },
                    new FundsYeld() { FundCode = "EFGH", DatePayment = DateTime.Now.AddDays(20), LastComputedDate = DateTime.Now, Value = 0.80, Type = "DIVIDENDO" },
                    new FundsYeld() { FundCode = "IJKL", DatePayment = DateTime.Now.AddDays(20), LastComputedDate = DateTime.Now, Value = 0.50, Type = "DIVIDENDO" },
                    new FundsYeld() { FundCode = "MNOP", DatePayment = DateTime.Now.AddDays(20), LastComputedDate = DateTime.Now, Value = 0.77, Type = "DIVIDENDO" },
                    new FundsYeld() { FundCode = "RSTU", DatePayment = DateTime.Now.AddDays(20), LastComputedDate = DateTime.Now, Value = 1.50, Type = "DIVIDENDO" },
                    new FundsYeld() { FundCode = "VXYZ", DatePayment = DateTime.Now.AddDays(20), LastComputedDate = DateTime.Now, Value = 1.00, Type = "DIVIDENDO" }
                }

            };
        }

        [Theory]
        [MemberData(nameof(FundsYeld))]
        [ConfigureTest]
        public async void MustEnterSixFundsYeldsAndReturnTrue(List<FundsYeld> fundsYelds)
        {

            Setup();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            Assert.True(resutl);
        }

        [Theory]
        [MemberData(nameof(FundsYeld))]
        [ConfigureTest]
        public async void MustReturnSixFundsYelds(List<FundsYeld> fundsYelds)
        {

            Setup();

            await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var resutl = await fundsYieldService.GetAllFundsYeldAsync();

            Assert.True(resutl.Count() == 6);
        }

        [Theory]
        [MemberData(nameof(FundsYeld))]
        [ConfigureTest]
        public async void MustReturnFundYeldByCode(List<FundsYeld> fundsYelds)
        {

            Setup();

            var resutl = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            var yelds = await fundsYieldService.GetFundYeldByCodeAsync("EFGH");
            
            Assert.Equal("EFGH", yelds.Select(x=>x.FundCode).FirstOrDefault());
            Assert.True(yelds.Count() > 0);

        }

    }
}