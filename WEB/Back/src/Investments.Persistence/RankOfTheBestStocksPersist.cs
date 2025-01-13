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
    public class RankOfTheBestStocksPersist : GeneralPersist, IRankOfTheBestStocksPersist
    {
        private readonly InvestmentsContext _context;

        public RankOfTheBestStocksPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<RankOfTheBestStocks> rankOfTheBestStocks)
        {
            try
            {
                _context.RankOfStocks.RemoveRange(_context.RankOfStocks.ToList());
                await _context.SaveChangesAsync();

                await _context.AddRangeAsync(rankOfTheBestStocks);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Task<IEnumerable<RankOfTheBestStocks>> GetRankOfTheBestStocksAsync(int? totalFundsInRank = null)
        {

            try
            {
                IQueryable<RankOfTheBestStocks> funds = totalFundsInRank is null ? 
                                                        _context.RankOfStocks.AsNoTracking() : 
                                                        _context.RankOfStocks.AsNoTracking().Take((int)totalFundsInRank);
                
                return Task.FromResult(funds.AsEnumerable());
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (Task<IEnumerable<RankOfTheBestStocks>>)null;
            }
            
        }
    }
}