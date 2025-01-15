using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class RankOfTheBestFundsService : IRankOfTheBestFundsService
    {

        private readonly IRankOfTheBestFundsPersist _rankOfTheBestFundsPersist;
        private readonly IDetailedFundService _detailedFundService;
        private readonly IFundDividendsService _fundsYeldService;
        private readonly IMapper _mapper;

        public RankOfTheBestFundsService(IRankOfTheBestFundsPersist rankOfTheBestFundsPersist,
                                         IDetailedFundService detailedFundService,
                                         IFundDividendsService fundsYeldService,
                                         IMapper mapper)
        {
            _rankOfTheBestFundsPersist = rankOfTheBestFundsPersist;
            _detailedFundService = detailedFundService;
            _fundsYeldService = fundsYeldService;
            _mapper = mapper;
        }

        public async Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<BestFundRank> rankOfTheBestFunds)
        {
            try
            {
                await _rankOfTheBestFundsPersist.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
                
                return true; 
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /*
            The better FIIs

            1) filter: "Liquidity" lower than 1,000,000.00 is then removed from the analysis.
            2) filter: Sort "Dividend Yield" column from  highest to lowest.
            3) After sorting create a column named "Rank Dividend Yeld" and enter a count for each row generated in this new column.
            4) Then add a column named "Rank Preco" and sort column "PriceEquityValue" from cheapest to most expensive and enter a count for each row generated in this new column.
            5) Finally add a column called "Rank Multiplier". Add the column "Rank Dividend Yeld" and the column "Rank Price". Then order column "Rank Multiplier"from smallest to largest.

            Calculation coefficient of variation

            6) Take the fund's dividends from the last 12 months. Calculate standard deviation of dividends for the last 12 months
            7) Calculate the average of dividends for the last 12 months
            8) Calculate the coefficient of variation CV (maximum 35%)
                CV = standard deviation / average
            9) Create a column named "Coefficient of variation".
            10) Add the result of the CV calculation for each fund and sort the column "Coefficient of variation" from lowest to highest.

        */

        public async Task<IEnumerable<BestFundRank>> GetCalculationRankOfTheBestFundsAsync()
        {

            try
            {

                var funds = await _detailedFundService.GetAllDetailedFundsAsync();

                var bestFunds = _mapper.Map<IEnumerable<BestFundRank>>(funds);

                // 1)
                bestFunds = bestFunds.Where(x => x.Liquidity >= 1000000);

                // 2)
                bestFunds = bestFunds.OrderByDescending(x => x.DividendYield);

                // 3)

                for (int i = 0; i < bestFunds.Count(); i++)
                {
                    bestFunds.ElementAt(i).DividendYieldRanking = i+1;
                }

                // 4)

                bestFunds = bestFunds.OrderBy(x => x.PriceEquityValue);

                for (int i = 0; i < bestFunds.Count(); i++)
                {
                    bestFunds.ElementAt(i).RankPrice = i+1;
                }

                // 5)

                for (int i = 0; i < bestFunds.Count(); i++)
                {
                    bestFunds.ElementAt(i).MultiplierRanking = 
                              bestFunds.ElementAt(i).DividendYieldRanking + bestFunds.ElementAt(i).RankPrice;
                }

                bestFunds = bestFunds.OrderBy(x => x.MultiplierRanking);

                // 6)

                for (int i = 0; i < bestFunds.Count(); i++)
                {

                    var result = await _fundsYeldService.GetFundYeldByCodeAsync(bestFunds.ElementAt(i).FundCode);

                    result = result.OrderByDescending(x => x.LastComputedDate).Take(12);

                    var standardDeviation = result.Select(x => x.Value).StandardDeviation();

                    var average = result.Select(x => x.Value).Average();

                    var CoefficienOfVariation = (standardDeviation / average) * 100;

                    bestFunds.ElementAt(i).CoefficientOfVariation = CoefficienOfVariation;
                }

                bestFunds = bestFunds.Where(x => x.CoefficientOfVariation <= 35);

                return bestFunds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BestFundRank>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null)
        {
            try
            {
                
                var bestFunds = await _rankOfTheBestFundsPersist.GetRankOfTheBestFundsAsync(totalFundsInRank);
                
                return bestFunds;

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}