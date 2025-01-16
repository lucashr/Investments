using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class DetailedFundPersist : GeneralPersist, IDetailedFundPersist
    {
        private readonly InvestmentsContext _context;

        public DetailedFundPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DetailedFund> GetDetailedFundByCodeAsync(string fundCode)
        {
            IQueryable<DetailedFund> query = _context.DetailedFunds
                                                        .AsNoTracking()
                                                        .Where(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim());

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DetailedFund>> GetAllDetailedFundsAsync()
        {
            IQueryable<DetailedFund> query = _context.DetailedFunds.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFund> detailedFunds)
        {
            _context.DetailedFunds.RemoveRange(_context.DetailedFunds.ToList());
            await _context.SaveChangesAsync();
            _context.AddRange(detailedFunds);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividend> fundsDividends)
        {
            _context.Database.EnsureDeleted();
            _context.AddRange(fundsDividends);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}