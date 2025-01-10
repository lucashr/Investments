using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class DetailedStocksPersist : GeneralPersist, IDetailedStocksPersist
    {

        private readonly InvestmentsContext _context;

        public DetailedStocksPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStocks> detailedStocks)
        {
            try
            {
                _context.DetailedStocks.RemoveRange(_context.DetailedStocks.ToList());
                await _context.SaveChangesAsync();
                _context.AddRange(detailedStocks);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<DetailedStocks>> GetAllDetailedStocksAsync()
        {
            try
            {
                IQueryable<DetailedStocks> query = _context.DetailedStocks.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<DetailedStocks> GetDetailedStockByCodeAsync(string stockCode)
        {
            try
            {
                IQueryable<DetailedStocks> query = _context.DetailedStocks
                                                          .AsNoTracking()
                                                          .Where(f => f.FundCode.ToUpper() == stockCode.ToUpper().Trim());

                return await query.FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}