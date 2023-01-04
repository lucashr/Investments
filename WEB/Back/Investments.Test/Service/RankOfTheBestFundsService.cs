using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Test.Contracts;
using Investments.Test.FunctionsOfCalculationExtend;
using Microsoft.EntityFrameworkCore;

namespace Investments.Test
{
    public class RankOfTheBestFundsService : ConfigureTest, IRankOfTheBestFundsService
    {

        public async Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM [RankFunds]");
                await _context.AddRangeAsync(rankOfTheBestFunds);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<RankOfTheBestFunds>> GetCalculationRankOfTheBestFundsAsync()
        {
            try
            {

                var config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DetailedFunds, RankOfTheBestFunds>();
                });

                var _mapper = config.CreateMapper();

                // var gggggg = new DetailedFundService(_context).GetAllDetailedFundsAsync();

                var _detailedFundService = new DetailedFundService(_context);
                var _fundsYeldService = new FundsService(_context);

                var kkk = await _context.DetailedFunds.ToListAsync();

                var funds = await _detailedFundService.GetAllDetailedFundsAsync();

                var bestFunds = _mapper.Map<IEnumerable<RankOfTheBestFunds>>(funds);

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

        public Task<IEnumerable<RankOfTheBestFunds>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null)
        {

            try
            {
                IQueryable<RankOfTheBestFunds> funds = _context.RankFunds.AsNoTracking();
                
                funds = funds.OrderBy(x => x.MultiplierRanking);

                if(totalFundsInRank != null)
                {
                    funds = funds.Take(totalFundsInRank ?? funds.Count());
                }

                return Task.FromResult(funds.AsEnumerable());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
    }
}