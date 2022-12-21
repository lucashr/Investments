using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimentos.Domain.Models;
using Investimentos.Persistence.Contexts;
using Investimentos.Test.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investimentos.Test
{
    public class FundYeldsService : IFundsYeldService
    {
        private readonly InvestmentsContext _context;

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

    }
}