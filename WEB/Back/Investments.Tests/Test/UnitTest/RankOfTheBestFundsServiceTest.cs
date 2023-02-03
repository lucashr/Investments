using System.Collections.Generic;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public class RankOfTheBestFundsServiceTest
    {

        static RankOfTheBestFundsService rankOfTheBestFundsService = null;
        static Mock<RankOfTheBestFundsPersist> rankOfTheBestFundsPersist = null;
        static DetailedFundPersist detailedFundPersist = null;
        static DetailedFundService detailedFundService = null;
        static Mock<FundYeldsPersist> fundYeldsPersist = null;
        static FundsYieldService fundsYieldService = null;

        public void Setup()
        {   

            rankOfTheBestFundsPersist = new Mock<RankOfTheBestFundsPersist>(ConfigureTest._context);
            fundYeldsPersist = new Mock<FundYeldsPersist>(ConfigureTest._context);
            detailedFundPersist = new DetailedFundPersist(ConfigureTest._context);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RankOfTheBestFunds, DetailedFunds>().ReverseMap();
            });

            var mapper = config.CreateMapper();
            
            rankOfTheBestFundsService = new RankOfTheBestFundsService(rankOfTheBestFundsPersist.Object, detailedFundService, 
                                                                      fundsYieldService, mapper);
        }

        public static IEnumerable<object[]> RankOfTheBestFunds(){

            yield return new object[]
            {
                new List<RankOfTheBestFunds>()
                {
                    new RankOfTheBestFunds() { FundCode = "ABCD", AverageVacancy = 1, CoefficientOfVariation= 7, DividendYield = 13, DividendYieldRanking = 19, FFOYield = 25, Liquidity = 31, MultiplierRanking = 37, PriceEquityValue = 43, RankPrice = 49, Segment = "TESTE", ValueOfMarket = 55 },
                    new RankOfTheBestFunds() { FundCode = "EFGH", AverageVacancy = 2, CoefficientOfVariation= 8, DividendYield = 14, DividendYieldRanking = 20, FFOYield = 26, Liquidity = 32, MultiplierRanking = 38, PriceEquityValue = 44, RankPrice = 50, Segment = "TESTE2", ValueOfMarket = 56 },
                    new RankOfTheBestFunds() { FundCode = "IJKL", AverageVacancy = 3, CoefficientOfVariation= 9, DividendYield = 15, DividendYieldRanking = 21, FFOYield = 27, Liquidity = 33, MultiplierRanking = 39, PriceEquityValue = 45, RankPrice = 51, Segment = "TESTE3", ValueOfMarket = 57 },
                    new RankOfTheBestFunds() { FundCode = "MNOP", AverageVacancy = 4, CoefficientOfVariation= 10, DividendYield = 16, DividendYieldRanking = 22, FFOYield = 28, Liquidity = 34, MultiplierRanking = 40, PriceEquityValue = 46, RankPrice = 52, Segment = "TESTE4", ValueOfMarket = 58 },
                    new RankOfTheBestFunds() { FundCode = "RSTU", AverageVacancy = 5, CoefficientOfVariation= 11, DividendYield = 17, DividendYieldRanking = 23, FFOYield = 29, Liquidity = 35, MultiplierRanking = 41, PriceEquityValue = 47, RankPrice = 53, Segment = "TESTE5", ValueOfMarket = 59 },
                    new RankOfTheBestFunds() { FundCode = "VXYZ", AverageVacancy = 6, CoefficientOfVariation= 12, DividendYield = 18, DividendYieldRanking = 24, FFOYield = 30, Liquidity = 36, MultiplierRanking = 42, PriceEquityValue = 48, RankPrice = 54, Segment = "TESTE6", ValueOfMarket = 60 }
                }

            };
        }

        [Theory]
        [MemberData(nameof(RankOfTheBestFunds))]
        [ConfigureTest]
        public async void MustStoreAndReturnTrue(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            var result = await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
            
            Assert.True(result);

        }

        [Theory]
        [MemberData(nameof(RankOfTheBestFunds))]
        [ConfigureTest]
        public async void MustCalculateAndReturnNotNull(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            var result = await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
            
            Assert.NotNull(result);

        }

        [Theory]
        [MemberData(nameof(RankOfTheBestFunds))]
        [ConfigureTest]
        public async void MustReturnRankOfTheBestFunds(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();

            var result = await rankOfTheBestFundsService.GetRankOfTheBestFundsAsync();
            
            Assert.NotNull(result);

        }
        
    }
}