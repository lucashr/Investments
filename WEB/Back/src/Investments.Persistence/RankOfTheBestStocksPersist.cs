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

        public async Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<BestStockRank> rankOfTheBestStocks)
        {
            _context.BestStockRanks.RemoveRange(_context.BestStockRanks.ToList());
            await _context.SaveChangesAsync();
            await _context.AddRangeAsync(rankOfTheBestStocks);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<IEnumerable<BestStockRank>> GetRankOfTheBestStocksAsync(int? totalFundsInRank = null)
        {
            IQueryable<BestStockRank> funds = totalFundsInRank is null ? 
                                                    _context.BestStockRanks.AsNoTracking() : 
                                                    _context.BestStockRanks.AsNoTracking().Take((int)totalFundsInRank);
            
            return Task.FromResult(funds.AsEnumerable());
        }
    }
}