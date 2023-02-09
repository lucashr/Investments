
using System.Collections.Generic;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;
using System.Linq;
using Investments.Tests.Helpers;

namespace Investments.Tests.Test
{

    public static class DetailedFundServiceTest
    {
        
        static DetailedFundPersist detailedFundPersist = null;
        static DetailedFundService detailedFundService = null;
        static Mock<IGeneralPersist> iGeneralPersist = null;
        static Mock<IMapper> iMapper = null;
        static Mock<FundsPersist> fundsPersist = null;

        public static void Setup()
        {

            detailedFundPersist = new DetailedFundPersist(ConfigureTest._context);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            iGeneralPersist = new Mock<IGeneralPersist>();
            iMapper = new Mock<IMapper>();
            fundsPersist = new Mock<FundsPersist>(ConfigureTest._context);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async void MustEnterTenFunds(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            await detailedFundService.AddDetailedFundsAsync(detailedFunds);

            FundsService fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);

            await fundsService.AddFundsAsync(detailedFunds);

            var funds = await detailedFundService.GetAllDetailedFundsAsync();

            Assert.True(funds.Count() == 10);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async void MustReturnOneFund(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            await detailedFundService.AddDetailedFundsAsync(detailedFunds);
            
            var funds = await detailedFundService.GetDetailedFundByCodeAsync("MNOP");
            
            Assert.Equal("MNOP", funds.FundCode);

        }

    }
}