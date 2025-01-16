using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class StocksDividendsPersist : GeneralPersist, IStockDividendPersist
    {

        private readonly InvestmentsContext _context;

        public StocksDividendsPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddStockDividendsAsync(IEnumerable<StockDividend> stockDividends)
        {
            _context.StocksDividends.RemoveRange(_context.StocksDividends);
            await _context.SaveChangesAsync();
            _context.StocksDividends.RemoveRange(_context.StocksDividends.ToList());
            _context.AddRange(stockDividends);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<StockDividend>> GetAllStockDividendsAsync()
        {
            IQueryable<StockDividend> query = _context.StocksDividends.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<StockDividend>> GetStockDividendsByCodeAsync(string stockCode)
        {
            IQueryable<StockDividend> query = _context.StocksDividends
                                                    .FromSqlRaw(@"SELECT * FROM StocksDividends 
                                                                WHERE UPPER(FundCode) = UPPER({0}) 
                                                                ORDER BY strftime('%Y-%m-%d', substr(date, 7, 4) || '-' || substr(date, 4, 2) || '-' || substr(date, 1, 2)) DESC", stockCode);
            
            return await query.ToListAsync();
        }
    }
}