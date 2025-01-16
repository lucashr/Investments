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
    public class FundDividendPersist : GeneralPersist, IFundDividendPersist
    {
        private readonly InvestmentsContext _context;

        public FundDividendPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FundDividend>> GetFundDividendsByCodeAsync(string fundCode)
        {
            IQueryable<FundDividend> query = _context.FundDividends
                                                    .AsNoTracking()
                                                    .Where(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim());
            
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<FundDividend>> GetAllFundsDividendsAsync()
        {
            IQueryable<FundDividend> query = _context.FundDividends.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<bool> AddFundsDividendsAsync(IEnumerable<FundDividend> fundsDividends)
        {
            _context.FundDividends.RemoveRange(_context.FundDividends);
            await _context.SaveChangesAsync();
            _context.FundDividends.RemoveRange(_context.FundDividends.ToList());
            _context.AddRange(fundsDividends);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}