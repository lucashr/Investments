using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class DetailedStocksPersist : RepositoryPersist, IDetailedStocksPersist
    {

        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<DetailedStock> _detailedStocksCollection;

        public DetailedStocksPersist(InvestmentsContext context = null,
                                     IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _detailedStocksCollection = database?.GetCollection<DetailedStock>("DetailedStocks");
        }

        public async Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStock> detailedStocks)
        {

            bool result = false;

            if (_context != null)
            {
                _context.DetailedStocks.RemoveRange(_context.DetailedStocks.ToList());
                await _context.SaveChangesAsync();
                _context.AddRange(detailedStocks);
                await _context.SaveChangesAsync();
                result = true;
            }
            else if (_detailedStocksCollection != null)
            {
                await _detailedStocksCollection.DeleteManyAsync(Builders<DetailedStock>.Filter.Empty);
                await _detailedStocksCollection.InsertManyAsync(detailedStocks);
                result = true;
            }

            return result;

        }

        public async Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync()
        {
            if (_context != null)
            {
                IQueryable<DetailedStock> query = _context.DetailedStocks.AsNoTracking();
                return await query.ToListAsync();
            }
            else if (_detailedStocksCollection != null)
            {
                return await _detailedStocksCollection.Find(_ => true).ToListAsync();
            }

            return Enumerable.Empty<DetailedStock>();

        }

        public async Task<DetailedStock> GetDetailedStockByCodeAsync(string stockCode)
        {
            if (_context != null)
            {
                IQueryable<DetailedStock> query = _context.DetailedStocks
                                                        .AsNoTracking()
                                                        .Where(f => f.FundCode.ToUpper() == stockCode.ToUpper().Trim());
                return await query.FirstOrDefaultAsync();
            }
            else if (_detailedStocksCollection != null)
            {
                return await _detailedStocksCollection
                    .Find(f => f.FundCode.ToUpper() == stockCode.ToUpper().Trim())
                    .FirstOrDefaultAsync();
            }

            return new DetailedStock();
        }
    }
}