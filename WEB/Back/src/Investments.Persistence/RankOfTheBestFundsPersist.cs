using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Persistence.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class RankOfTheBestFundsPersist : GeneralPersist, IRankOfTheBestFundsPersist
    {
        private readonly InvestmentsContext _context;

        public RankOfTheBestFundsPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

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

        public Task<IEnumerable<RankOfTheBestFunds>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null)
        {

            try
            {
                IQueryable<RankOfTheBestFunds> funds = _context.RankFunds.AsNoTracking();
                
                if(totalFundsInRank != null)
                {
                    funds = funds.OrderBy(x => x.MultiplierRanking)
                                 .Take(totalFundsInRank ?? funds.Count());
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