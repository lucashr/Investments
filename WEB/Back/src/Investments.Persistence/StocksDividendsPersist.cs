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
    public class StocksDividendsPersist : RepositoryPersist, IStockDividendPersist
    {

        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<StockDividend> _stockDividendCollection;

        public StocksDividendsPersist(InvestmentsContext context = null,
                                   IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _stockDividendCollection = database?.GetCollection<StockDividend>("StocksDividends");
        }

        public async Task<bool> AddStockDividendsAsync(IEnumerable<StockDividend> stockDividends)
        {
            if (_context != null)
            {
                _context.StocksDividends.RemoveRange(_context.StocksDividends.ToList());
                await _context.SaveChangesAsync();
                _context.AddRange(stockDividends);
                await _context.SaveChangesAsync();
                return true;
            }
            else if (_stockDividendCollection != null)
            {
                await _stockDividendCollection.DeleteManyAsync(Builders<StockDividend>.Filter.Empty);
                await _stockDividendCollection.InsertManyAsync(stockDividends);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<StockDividend>> GetAllStockDividendsAsync()
        {
            if (_context != null)
            {
                IQueryable<StockDividend> query = _context.StocksDividends.AsNoTracking();
                return await query.ToListAsync();
            }
            else if (_stockDividendCollection != null)
            {
                return await _stockDividendCollection.Find(_ => true).ToListAsync();
            }

            return Enumerable.Empty<StockDividend>();
        }

        public async Task<IEnumerable<StockDividend>> GetStockDividendsByCodeAsync(string stockCode)
        {
            if (_context != null)
            {
                IQueryable<StockDividend> query = _context.StocksDividends
                                                    .FromSqlRaw(@"SELECT * FROM StocksDividends 
                                                                WHERE UPPER(FundCode) = UPPER({0}) 
                                                                ORDER BY strftime('%Y-%m-%d', substr(date, 7, 4) || '-' || substr(date, 4, 2) || '-' || substr(date, 1, 2)) DESC", stockCode);
                return await query.ToListAsync();
            }
            else if (_stockDividendCollection != null)
            {
                return await _stockDividendCollection.Find(f => f.FundCode.ToUpper() == stockCode.ToUpper().Trim())
                                                    .SortByDescending(f => f.Date)
                                                    .ToListAsync();
            }

            return Enumerable.Empty<StockDividend>();

        }
    }
}