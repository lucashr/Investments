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

        public async Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStock> detailedStocks)
        {
            _context.DetailedStocks.RemoveRange(_context.DetailedStocks.ToList());
            await _context.SaveChangesAsync();
            _context.AddRange(detailedStocks);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync()
        {
            IQueryable<DetailedStock> query = _context.DetailedStocks.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<DetailedStock> GetDetailedStockByCodeAsync(string stockCode)
        {
            IQueryable<DetailedStock> query = _context.DetailedStocks
                                                        .AsNoTracking()
                                                        .Where(f => f.FundCode.ToUpper() == stockCode.ToUpper().Trim());

            return await query.FirstOrDefaultAsync();
        }
    }
}