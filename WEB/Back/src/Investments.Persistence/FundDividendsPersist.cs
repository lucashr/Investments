using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class FundDividendPersist : RepositoryPersist, IFundDividendPersist
    {
        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<FundDividend> _fundDividendCollection;

        public FundDividendPersist(InvestmentsContext context = null,
                                   IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _fundDividendCollection = database?.GetCollection<FundDividend>("FundDividends");
        }

        public async Task<IEnumerable<FundDividend>> GetFundDividendsByCodeAsync(string fundCode)
        {

            if (_context != null)
            {
                IQueryable<FundDividend> query = _context.FundDividends
                    .AsNoTracking()
                    .Where(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim());
                return await query.ToListAsync();
            }
            else if (_fundDividendCollection != null)
            {
                return await _fundDividendCollection
                    .Find(f => f.FundCode.ToUpper() == fundCode.ToUpper().Trim())
                    .ToListAsync();
            }

            return Enumerable.Empty<FundDividend>();

        }

        public async Task<IEnumerable<FundDividend>> GetAllFundsDividendsAsync()
        {
            if (_context != null)
            {
                IQueryable<FundDividend> query = _context.FundDividends.AsNoTracking();
                return await query.ToListAsync();
            }
            else if (_fundDividendCollection != null)
            {
                return await _fundDividendCollection.Find(_ => true).ToListAsync();
            }

            return Enumerable.Empty<FundDividend>();

        }

        public async Task<bool> AddFundsDividendsAsync(IEnumerable<FundDividend> fundsDividends)
        {
            if (_context != null) {
                _context.FundDividends.RemoveRange(_context.FundDividends.ToList());
                await _context.SaveChangesAsync();
                _context.AddRange(fundsDividends);
                await _context.SaveChangesAsync();
                return true;
            }
            else if (_fundDividendCollection != null)
            {
                await _fundDividendCollection.DeleteManyAsync(Builders<FundDividend>.Filter.Empty);
                await _fundDividendCollection.InsertManyAsync(fundsDividends);
                return true;
            }

            return false;

        }

    }
}