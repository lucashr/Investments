using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public class FundsServiceTest
    {

        static Mock<FundsPersist> fundsPersist = null;
        static Mock<GeneralPersist> iGeneralPersist = null;
        static Mock<IMapper> iMapper = null;
        static FundsService fundsService = null;

        public void Setup()
        {
            fundsPersist = new Mock<FundsPersist>(ConfigureTest._context);
            iGeneralPersist = new Mock<GeneralPersist>(ConfigureTest._context);
            iMapper = new Mock<IMapper>();
            fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);
        }
        
        public static IEnumerable<object[]> DetailedFunds(){

            yield return new object[]
            {
                new List<DetailedFunds>()
                {
                    new DetailedFunds() { FundCode = "ABCD"},
                    new DetailedFunds() { FundCode = "EFGH" },
                    new DetailedFunds() { FundCode = "IJKL" },
                    new DetailedFunds() { FundCode = "MNOP" },
                    new DetailedFunds() { FundCode = "RSTU" },
                    new DetailedFunds() { FundCode = "VXYZ" }
                }
            };

        }

        [Theory]
        [MemberData(nameof(DetailedFunds))]
        [ConfigureTest]
        public async void MustEnterSixFunds(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            var result = await fundsService.AddFundsAsync(detailedFunds);

            Assert.True(result);

        }

        [Theory]
        [InlineData("ABCD", "TESTE")]
        [ConfigureTest]
        public async void MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            
            Setup();

            dynamic detailedFunds = DetailedFunds().ElementAt(0).ElementAt(0);

            await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.UpdateFundByCodeAsync(oldFundCode, newFundCode);

            Assert.Equal("TESTE", fund.FundCode);

        }

        [Theory]
        [InlineData("BBBBB")]
        [ConfigureTest]
        public async void MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {

            Setup();

            dynamic detailedFunds = DetailedFunds().ElementAt(0).ElementAt(0);

            await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.AddFundAsync(fundCode);

            Assert.Equal("BBBBB", fund.FundCode);
        }

        [Theory]
        [InlineData("VXYZ")]
        [ConfigureTest]
        public async void MustReturnOneFund(string fundCode)
        {

            Setup();

            dynamic detailedFunds = DetailedFunds().ElementAt(0).ElementAt(0);

            await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.GetFundByCodeAsync(fundCode);

            Assert.Equal("VXYZ", fund.FundCode);
        }

        [Theory]
        [MemberData(nameof(DetailedFunds))]
        [ConfigureTest]
        public async void MustReturnAllFunds(List<DetailedFunds> detailedFunds)
        {

            Setup();

            var result = await fundsService.AddFundsAsync(detailedFunds);

            var fund = await fundsService.GetAllFundsAsync();

            Assert.True(fund.Count() == 6);
        }

        [Theory]
        [InlineData("RSTU")]
        [ConfigureTest]
        public async void MustDeleteFundEReturnTrue(string fundCode)
        {

            Setup();

            dynamic detailedFunds = DetailedFunds().ElementAt(0).ElementAt(0);

            await fundsService.AddFundsAsync(detailedFunds);

            var result = await fundsService.DeleteFundByCodeAsync(fundCode);

            Assert.Equal(true, result);
        }

    }
}