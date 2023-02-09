
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
using System.Threading.Tasks;

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

        public async static Task SeedDB()
        {

            dynamic funds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await detailedFundService.AddDetailedFundsAsync(funds);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public static async Task MustEnterTenFunds(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            FundsService fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);

            await fundsService.AddFundsAsync(detailedFunds);

            var funds = await detailedFundService.GetAllDetailedFundsAsync();

            Assert.True(funds.Count() == 10);

        }

        [Theory]
        [InlineData("ABCP11")]
        [ConfigureTest]
        public static async Task MustReturnOneFund(string fundCode)
        {
            
            Setup();
            await SeedDB();

            var funds = await detailedFundService.GetDetailedFundByCodeAsync(fundCode);
            
            Assert.Equal(fundCode, funds.FundCode);

        }

    }
}