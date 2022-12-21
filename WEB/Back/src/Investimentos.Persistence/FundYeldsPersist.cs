using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimentos.Domain.Models;
using Investimentos.Persistence.Contexts;
using Investimentos.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investimentos.Persistence
{
    public class FundYeldsPersist : GeneralPersist, IFundsYeldPersist
    {
        private readonly InvestmentsContext _context;

        public FundYeldsPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FundsYeld>> GetFundYeldByCodeAsync(string fundCode)
        {
            try
            {
                IQueryable<FundsYeld> query = _context.FundsYeld
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

        public async Task<IEnumerable<FundsYeld>> GetAllFundsYeldAsync()
        {
            try
            {
                IQueryable<FundsYeld> query = _context.FundsYeld.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds)
        {
            try
            {

                _context.Database.ExecuteSqlRaw("DELETE FROM [FundsYeld]");
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