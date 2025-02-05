using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Persistence.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class RankOfTheBestStocksPersist : RepositoryPersist, IRankOfTheBestStocksPersist
    {
        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<BestStockRank> _bestStockRankCollection;

        public RankOfTheBestStocksPersist(InvestmentsContext context = null,
                                   IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _bestStockRankCollection = database?.GetCollection<BestStockRank>("BestStockRanks");
        }

        public async Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<BestStockRank> rankOfTheBestStocks)
        {
            if (_context != null)
            {
                _context.BestStockRanks.RemoveRange(_context.BestStockRanks.ToList());
                await _context.SaveChangesAsync();
                await _context.AddRangeAsync(rankOfTheBestStocks);
                await _context.SaveChangesAsync();
                return true;
            }
            else if (_bestStockRankCollection != null)
            {
                await _bestStockRankCollection.DeleteManyAsync(Builders<BestStockRank>.Filter.Empty);
                await _bestStockRankCollection.InsertManyAsync(rankOfTheBestStocks);
                return true;
            }

            return false;
        }

        public Task<IEnumerable<BestStockRank>> GetRankOfTheBestStocksAsync(int? totalFundsInRank = null)
        {
            IEnumerable<BestStockRank> funds = null;

            if (_context != null)
            {
                funds = totalFundsInRank is null ?
                            _context.BestStockRanks.AsNoTracking() :
                            _context.BestStockRanks.AsNoTracking().Take((int)totalFundsInRank);

                return Task.FromResult(funds.AsEnumerable());
            }
            else if (_bestStockRankCollection != null)
            {
                funds = totalFundsInRank is null ?
                            _bestStockRankCollection.Find(_ => true).ToList() :
                            _bestStockRankCollection.Find(_ => true).Limit((int)totalFundsInRank).ToList();
            }
            
            return Task.FromResult(funds.AsEnumerable());
        }
    }
}