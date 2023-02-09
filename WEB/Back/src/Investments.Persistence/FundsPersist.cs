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
    public class FundsPersist : GeneralPersist, IFundsPersist, IDetachLocal
    {
        private readonly InvestmentsContext _context;

        public FundsPersist(InvestmentsContext context) : base(context)
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
                IQueryable<DetailedFunds> query = _context.DetailedFunds
                                                          .AsNoTracking();
                
                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
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

        public async Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode)
        {
            try
            {
                IQueryable<Funds> query = _context.Funds
                                                  .AsNoTracking().Where(x=> x.FundCode == oldFundCode);

                if(query.FirstOrDefault() == null) throw new Exception("Fundo não encontrado para atualização");

                var fund = await query.FirstOrDefaultAsync();

                fund.FundCode = newFundCode;

                _context.Update<Funds>(fund);

                await _context.SaveChangesAsync();

                fund = await _context.Funds.Where(x=> x.FundCode == newFundCode).FirstOrDefaultAsync();

                return fund;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {

            var funds = new List<Funds>();

            try
            {

                _context.Database.ExecuteSqlRaw("DELETE FROM [Funds]");

                foreach (var fund in detailedFunds)
                {

                    funds.Add(new Funds() {FundCode = fund.FundCode});
                }

                AddRange<Funds>(funds.ToArray());

                await _context.SaveChangesAsync();

                return true;

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Funds> AddFundAsync(string fundCode)
        {
            
            try
            {
                IQueryable<Funds> query = _context.Funds.Where(x=> x.FundCode == fundCode);

                if(query.FirstOrDefault() is not null) throw new Exception("Fundo já cadastrado na base de dados");

                var newFund = new Funds() {FundCode = fundCode};

                _context.Add<Funds>(newFund);

                await _context.SaveChangesAsync();

                newFund =  await _context.Funds
                                         .Where(x=> x.FundCode == fundCode).FirstOrDefaultAsync();

                return newFund;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<Funds> GetFundByCodeAsync(string fundCode)
        {
            try
            {
                IQueryable<Funds> query = _context.Funds
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

        public async Task<IEnumerable<Funds>> GetAllFundsAsync()
        {
            try
            {
                IQueryable<Funds> query = _context.Funds.AsNoTracking();

                return await query.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<bool> DeleteFundByCodeAsync(string fundCode)
        {
            try
            {
                IQueryable<Funds> query = _context.Funds.AsNoTracking().Where(x=> x.FundCode == fundCode);
                
                if(query.FirstOrDefault() == null) throw new Exception("Fundo não encontrado na base de dados");
                
                _context.Remove<Funds>(query.FirstOrDefault());

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