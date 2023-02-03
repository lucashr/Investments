
using System.Collections.Generic;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;
using System.Linq;

namespace Investments.Tests.Test
{

    public class DetailedFundServiceTest
    {
        
        static DetailedFundPersist detailedFundPersist = null;
        static DetailedFundService detailedFundService = null;
        static Mock<IGeneralPersist> iGeneralPersist = null;
        static Mock<IMapper> iMapper = null;
        static Mock<FundsPersist> fundsPersist = null;

        public void Setup()
        {

            detailedFundPersist = new DetailedFundPersist(ConfigureTest._context);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            iGeneralPersist = new Mock<IGeneralPersist>();
            iMapper = new Mock<IMapper>();
            fundsPersist = new Mock<FundsPersist>(ConfigureTest._context);

        }

        public static IEnumerable<object[]> DetailedFunds(){

            yield return new object[]
            {
                new List<DetailedFunds>()
                {
                    new DetailedFunds() { FundCode = "ABCD", Liquidity = 1000000, DividendYield = 1 },
                    new DetailedFunds() { FundCode = "EFGH", Liquidity = 1000000, DividendYield = 2 },
                    new DetailedFunds() { FundCode = "IJKL", Liquidity = 1000000, DividendYield = 3 },
                    new DetailedFunds() { FundCode = "MNOP", Liquidity = 1000000, DividendYield = 4 },
                    new DetailedFunds() { FundCode = "RSTU", Liquidity = 1000000, DividendYield = 5 },
                    new DetailedFunds() { FundCode = "VXYZ", Liquidity = 1000000, DividendYield = 6 }
                },

            };

        }

        [Theory]
        [MemberData(nameof(DetailedFunds))]
        [ConfigureTest]
        public async void MustEnterSixFunds(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            await detailedFundService.AddDetailedFundsAsync(detailedFunds);

            FundsService fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);

            await fundsService.AddFundsAsync(detailedFunds);

            var funds = await detailedFundService.GetAllDetailedFundsAsync();

            Assert.True(funds.Count() == 6);
            
        }

        [Theory]
        [MemberData(nameof(DetailedFunds))]
        [ConfigureTest]
        public async void MustReturnOneFund(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            await detailedFundService.AddDetailedFundsAsync(detailedFunds);
            
            var funds = await detailedFundService.GetDetailedFundByCodeAsync("MNOP");
            
            Assert.Equal("MNOP", funds.FundCode);
            
        }

    }
}