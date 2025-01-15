using System;
using System.Collections.Generic;
using Investments.Domain.Models;

namespace Investments.Tests.Helpers
{
    public static class DummyTest
    {

        public static IEnumerable<object[]> FundDividend(){

            yield return new object[]
            {
                new List<FundDividend>()
                {
                    new FundDividend() { FundCode = "AAZQ11", DatePayment = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.90, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "ABCP11", DatePayment = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.90, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AEFI11", DatePayment = DateTime.Now.AddDays(22).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.90, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFCR11", DatePayment = DateTime.Now.AddDays(23).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.90, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFHI11", DatePayment = DateTime.Now.AddDays(24).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.90, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFOF11", DatePayment = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.80, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AGCX11", DatePayment = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.80, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AGRX11", DatePayment = DateTime.Now.AddDays(22).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.80, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AIEC11", DatePayment = DateTime.Now.AddDays(23).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.80, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "ALMI11", DatePayment = DateTime.Now.AddDays(24).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.80, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AAZQ11", DatePayment = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "ABCP11", DatePayment = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AEFI11", DatePayment = DateTime.Now.AddDays(22).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFCR11", DatePayment = DateTime.Now.AddDays(23).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFHI11", DatePayment = DateTime.Now.AddDays(24).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFOF11", DatePayment = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.77, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AGCX11", DatePayment = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.77, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AGRX11", DatePayment = DateTime.Now.AddDays(22).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.77, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AIEC11", DatePayment = DateTime.Now.AddDays(23).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.77, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "ALMI11", DatePayment = DateTime.Now.AddDays(24).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 0.77, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AAZQ11", DatePayment = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "ABCP11", DatePayment = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AEFI11", DatePayment = DateTime.Now.AddDays(22).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFCR11", DatePayment = DateTime.Now.AddDays(23).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFHI11", DatePayment = DateTime.Now.AddDays(24).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.50, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AFOF11", DatePayment = DateTime.Now.AddDays(20).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.00, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AGCX11", DatePayment = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.00, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AGRX11", DatePayment = DateTime.Now.AddDays(22).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.00, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "AIEC11", DatePayment = DateTime.Now.AddDays(23).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.00, Type = "DIVIDENDO" },
                    new FundDividend() { FundCode = "ALMI11", DatePayment = DateTime.Now.AddDays(24).ToString("dd/MM/yyyy"), LastComputedDate = DateTime.Now.ToString("dd/MM/yyyy"), Value = 1.00, Type = "DIVIDENDO" }
                }

            };
        }

        public static IEnumerable<object[]> FundCodes(){

            yield return new object[]
            {
                new List<string>()
                {
                   "AAZQ11",
                   "ABCP11",
                   "AEFI11",
                   "AFCR11",
                   "AFHI11",
                   "AFOF11",
                   "AGCX11",
                   "AGRX11",
                   "AIEC11",
                   "ALMI11"
                },

            };

        }

        public static IEnumerable<object[]> DetailedFund(){

            yield return new object[]
            {
                new List<DetailedFund>()
                {
                    new DetailedFund() { FundCode = "AAZQ11", Liquidity = 1000000, DividendYield = 1 },
                    new DetailedFund() { FundCode = "ABCP11", Liquidity = 1000000, DividendYield = 2 },
                    new DetailedFund() { FundCode = "AEFI11", Liquidity = 1000000, DividendYield = 3 },
                    new DetailedFund() { FundCode = "AFCR11", Liquidity = 1000000, DividendYield = 4 },
                    new DetailedFund() { FundCode = "AFHI11", Liquidity = 1000000, DividendYield = 5 },
                    new DetailedFund() { FundCode = "AFOF11", Liquidity = 1000000, DividendYield = 6 },
                    new DetailedFund() { FundCode = "AGCX11", Liquidity = 1000000, DividendYield = 6 },
                    new DetailedFund() { FundCode = "AGRX11", Liquidity = 1000000, DividendYield = 6 },
                    new DetailedFund() { FundCode = "AIEC11", Liquidity = 1000000, DividendYield = 6 },
                    new DetailedFund() { FundCode = "ALMI11", Liquidity = 1000000, DividendYield = 6 }
                },

            };

        }

        public static IEnumerable<object[]> BestFundRank(){

            yield return new object[]
            {
                new List<BestFundRank>()
                {
                    new BestFundRank() { FundCode = "AAZQ11", AverageVacancy = 1, CoefficientOfVariation= 7, DividendYield = 13, DividendYieldRanking = 19, FFOYield = 25, Liquidity = 31, MultiplierRanking = 37, PriceEquityValue = 43, RankPrice = 49, Segment = "TESTE", ValueOfMarket = 55 },
                    new BestFundRank() { FundCode = "ABCP11", AverageVacancy = 2, CoefficientOfVariation= 8, DividendYield = 14, DividendYieldRanking = 20, FFOYield = 26, Liquidity = 32, MultiplierRanking = 38, PriceEquityValue = 44, RankPrice = 50, Segment = "TESTE2", ValueOfMarket = 56 },
                    new BestFundRank() { FundCode = "AEFI11", AverageVacancy = 3, CoefficientOfVariation= 9, DividendYield = 15, DividendYieldRanking = 21, FFOYield = 27, Liquidity = 33, MultiplierRanking = 39, PriceEquityValue = 45, RankPrice = 51, Segment = "TESTE3", ValueOfMarket = 57 },
                    new BestFundRank() { FundCode = "AFCR11", AverageVacancy = 4, CoefficientOfVariation= 10, DividendYield = 16, DividendYieldRanking = 22, FFOYield = 28, Liquidity = 34, MultiplierRanking = 40, PriceEquityValue = 46, RankPrice = 52, Segment = "TESTE4", ValueOfMarket = 58 },
                    new BestFundRank() { FundCode = "AFHI11", AverageVacancy = 5, CoefficientOfVariation= 11, DividendYield = 17, DividendYieldRanking = 23, FFOYield = 29, Liquidity = 35, MultiplierRanking = 41, PriceEquityValue = 47, RankPrice = 53, Segment = "TESTE5", ValueOfMarket = 59 },
                    new BestFundRank() { FundCode = "AFOF11", AverageVacancy = 6, CoefficientOfVariation= 12, DividendYield = 18, DividendYieldRanking = 24, FFOYield = 30, Liquidity = 36, MultiplierRanking = 42, PriceEquityValue = 48, RankPrice = 54, Segment = "TESTE6", ValueOfMarket = 60 },
                    new BestFundRank() { FundCode = "AGCX11", AverageVacancy = 6, CoefficientOfVariation= 12, DividendYield = 18, DividendYieldRanking = 24, FFOYield = 30, Liquidity = 36, MultiplierRanking = 42, PriceEquityValue = 48, RankPrice = 54, Segment = "TESTE6", ValueOfMarket = 60 },
                    new BestFundRank() { FundCode = "AGRX11", AverageVacancy = 6, CoefficientOfVariation= 12, DividendYield = 18, DividendYieldRanking = 24, FFOYield = 30, Liquidity = 36, MultiplierRanking = 42, PriceEquityValue = 48, RankPrice = 54, Segment = "TESTE6", ValueOfMarket = 60 },
                    new BestFundRank() { FundCode = "AIEC11", AverageVacancy = 6, CoefficientOfVariation= 12, DividendYield = 18, DividendYieldRanking = 24, FFOYield = 30, Liquidity = 36, MultiplierRanking = 42, PriceEquityValue = 48, RankPrice = 54, Segment = "TESTE6", ValueOfMarket = 60 },
                    new BestFundRank() { FundCode = "ALMI11", AverageVacancy = 6, CoefficientOfVariation= 12, DividendYield = 18, DividendYieldRanking = 24, FFOYield = 30, Liquidity = 36, MultiplierRanking = 42, PriceEquityValue = 48, RankPrice = 54, Segment = "TESTE6", ValueOfMarket = 60 }
                }

            };
        }

    }
}