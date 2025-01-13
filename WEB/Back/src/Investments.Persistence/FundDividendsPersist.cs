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

        public async Task<IEnumerable<FundDividends>> GetFundYeldByCodeAsync(string fundCode)
        {
            try
            {
                IQueryable<FundDividends> query = _context.FundsYeld
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

        public async Task<IEnumerable<FundDividends>> GetAllFundsYeldAsync()
        {
            try
            {
                IQueryable<FundDividends> query = _context.FundsYeld.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividends> fundsYelds)
        {
            try
            {
                _context.FundsYeld.RemoveRange(_context.FundsYeld);
                await _context.SaveChangesAsync();
                _context.FundsYeld.RemoveRange(_context.FundsYeld.ToList());
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