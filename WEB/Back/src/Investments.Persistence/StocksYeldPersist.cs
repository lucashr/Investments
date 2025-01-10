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
    public class StocksYeldPersist : GeneralPersist, IStocksYeldPersist
    {

        private readonly InvestmentsContext _context;

        public StocksYeldPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddStockDividendsAsync(IEnumerable<StocksDividends> stockDividends)
        {
            try
            {
                _context.StocksDividends.RemoveRange(_context.StocksDividends);
                await _context.SaveChangesAsync();
                _context.StocksDividends.RemoveRange(_context.StocksDividends.ToList());
                _context.AddRange(stockDividends);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<StocksDividends>> GetAllStockDividendsAsync()
        {
            try
            {
                IQueryable<StocksDividends> query = _context.StocksDividends.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<StocksDividends>> GetStockDividendsByCodeAsync(string stockCode)
        {
            try
            {
                IQueryable<StocksDividends> query = _context.StocksDividends
                                                      .AsNoTracking()
                                                      .Where(f => f.FundCode.ToUpper() == stockCode.ToUpper().Trim());
                
                return await query.ToListAsync();
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}