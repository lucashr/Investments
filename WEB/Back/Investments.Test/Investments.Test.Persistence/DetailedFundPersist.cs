using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Test.Domain.Models;
using Investments.Test.Persistence.Contexts;
using Investments.Test.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Test.Persistence
{
    public class DetailedFundPersist : GeneralPersist, IDetailedFundPersist
    {
        private readonly InvestmentsContext _context;

        public DetailedFundPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
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

        public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM [DetailedFunds]");
                _context.AddRange(detailedFunds);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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

        Task<DetailedFunds> IDetailedFundPersist.GetDetailedFundByCodeAsync(string fundCode)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<DetailedFunds>> IDetailedFundPersist.GetAllDetailedFundsAsync()
        {
            throw new NotImplementedException();
        }

    }
}