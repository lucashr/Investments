using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimentos.Application.Contracts;
using Investimentos.Domain.Models;
using Investimentos.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Investimentos.Test
{
    public class DetailedFundService : ConfigureTest, IDetailedFundService
    {
        // private readonly InvestmentsContext _context;

        public DetailedFundService()
        {
        }

        public DetailedFundService(InvestmentsContext context)
        {
            _context = context;
        }

        public Task<bool> AddFundsYeldsAsync(IEnumerable<FundsYeld> fundsYelds)
        {
            try
            {
                // _context.Database.ExecuteSqlRaw("DELETE FROM [FundsYeld]");
                _context.AddRange(fundsYelds);

                return Task.FromResult(true);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        public async Task<DetailedFunds> GetDetailedFundByCodeAsync(string fundCode)
        {
            try
            {
                IQueryable<DetailedFunds> query = _context.DetailedFunds
                                                          .AsNoTracking()
                                                          .Where(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim());

                return await query.FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<IEnumerable<DetailedFunds>> GetAllDetailedFundsAsync()
        {
            try
            {
                IQueryable<DetailedFunds> query = _context.DetailedFunds.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddFundsYeldsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            throw new NotImplementedException();
        }
    }
}