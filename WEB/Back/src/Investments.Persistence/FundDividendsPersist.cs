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
    public class FundDividendsPersist : GeneralPersist, IFundsYeldPersist
    {
        private readonly InvestmentsContext _context;

        public FundDividendsPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FundDividend>> GetFundYeldByCodeAsync(string fundCode)
        {
            try
            {
                IQueryable<FundDividend> query = _context.FundDividends
                                                      .AsNoTracking()
                                                      .Where(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim());
                
                return await query.ToListAsync();
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<IEnumerable<FundDividend>> GetAllFundsYeldAsync()
        {
            try
            {
                IQueryable<FundDividend> query = _context.FundDividends.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividend> fundsYelds)
        {
            try
            {
                _context.FundDividends.RemoveRange(_context.FundDividends);
                await _context.SaveChangesAsync();
                _context.FundDividends.RemoveRange(_context.FundDividends.ToList());
                _context.AddRange(fundsYelds);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}