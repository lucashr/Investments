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
    public class RankOfTheBestFundsPersist : RepositoryPersist, IRankOfTheBestFundsPersist
    {
        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<BestFundRank> _bestFundRankCollection;

        public RankOfTheBestFundsPersist(InvestmentsContext context = null,
                                         IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _bestFundRankCollection = database?.GetCollection<BestFundRank>("BestFundRanks");
        }

        public async Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<BestFundRank> rankOfTheBestFunds)
        {
            if (_context != null)
            {
                _context.BestFundRanks.RemoveRange(_context.BestFundRanks.ToList());
                await _context.SaveChangesAsync();
                await _context.AddRangeAsync(rankOfTheBestFunds);
                await _context.SaveChangesAsync();
                return true;
            }
            else if (_bestFundRankCollection != null)
            {
                await _bestFundRankCollection.DeleteManyAsync(Builders<BestFundRank>.Filter.Empty);
                await _bestFundRankCollection.InsertManyAsync(rankOfTheBestFunds);
                return true;
            }
            
            return false;

        }

        public Task<IEnumerable<BestFundRank>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null)
        {

            if (_context != null)
            {
                
                IQueryable<BestFundRank> funds = _context.BestFundRanks.AsNoTracking();

                if (totalFundsInRank != null)
                {
                    funds = funds.OrderBy(x => x.MultiplierRanking)
                                    .Take(totalFundsInRank ?? funds.Count());
                }

                return Task.FromResult(funds.AsEnumerable());

            } else if (_bestFundRankCollection != null)
            {
                var funds = _bestFundRankCollection.Find(_ => true).ToList();
                if (totalFundsInRank != null)
                {
                    funds = funds.OrderBy(x => x.MultiplierRanking)
                                    .Take(totalFundsInRank ?? funds.Count())
                                    .ToList();
                }

                return Task.FromResult(funds.AsEnumerable());

            }


            return Task.FromResult(Enumerable.Empty<BestFundRank>());

        }
    }
}