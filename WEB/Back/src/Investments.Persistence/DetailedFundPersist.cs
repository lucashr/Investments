using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using Investments.Persistence.Contexts;

namespace Investments.Persistence
{
    public class DetailedFundPersist : RepositoryPersist, IDetailedFundPersist
    {
        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<DetailedFund> _detailedFundsCollection;

        public DetailedFundPersist(InvestmentsContext context = null, 
                                   IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _detailedFundsCollection = database?.GetCollection<DetailedFund>("DetailedFunds");
        }

        public async Task<DetailedFund> GetDetailedFundByCodeAsync(string fundCode)
        {
            if (_context != null) // SQLite
            {
                IQueryable<DetailedFund> query = _context.DetailedFunds
                    .AsNoTracking()
                    .Where(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim());

                return await query.FirstOrDefaultAsync();
            }
            else if (_detailedFundsCollection != null) // MongoDB
            {
                return await _detailedFundsCollection
                    .Find(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim())
                    .FirstOrDefaultAsync();
            }

            throw new InvalidOperationException("No database context available.");
        }

        public async Task<IEnumerable<DetailedFund>> GetAllDetailedFundsAsync()
        {
            if (_context != null) // SQLite
            {
                IQueryable<DetailedFund> query = _context.DetailedFunds.AsNoTracking();
                return await query.ToListAsync();
            }
            else if (_detailedFundsCollection != null) // MongoDB
            {
                return await _detailedFundsCollection.Find(_ => true).ToListAsync();
            }

            throw new InvalidOperationException("No database context available.");
        }

        public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFund> detailedFunds)
        {

            if (_context != null) // SQLite
            {
                _context.DetailedFunds.RemoveRange(_context.DetailedFunds.ToList());
                await _context.SaveChangesAsync();
                _context.AddRange(detailedFunds);
                await _context.SaveChangesAsync();

                return true;
            }
            else if (_detailedFundsCollection != null) // MongoDB
            {
                await _detailedFundsCollection.DeleteManyAsync(_ => true);
                await _detailedFundsCollection.InsertManyAsync(detailedFunds);

                return true;
            }

            throw new InvalidOperationException("No database context available.");
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividend> fundsDividends)
        {
            if (_context != null) // SQLite
            {
                _context.Database.EnsureDeleted();
                _context.AddRange(fundsDividends);
                await _context.SaveChangesAsync();

                return true;
            }
            else if (_detailedFundsCollection != null) // MongoDB
            {
                var fundDividendsCollection = _detailedFundsCollection.Database.GetCollection<FundDividend>("FundDividends");
                await fundDividendsCollection.DeleteManyAsync(_ => true);
                await fundDividendsCollection.InsertManyAsync(fundsDividends);

                return true;
            }

            throw new InvalidOperationException("No database context available.");
        }
    }
}
