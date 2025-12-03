using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class FundsDividendsService : IFundDividendsService
    {

        private readonly IFundDividendPersist _fundsDividendsPersist;
        private readonly ICacheService _cache;
        private const string AllFundDividendsCacheKey = "All_Fund_Dividends";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);

        public FundsDividendsService(IFundDividendPersist fundsDividendsPersist, ICacheService cache)
        {
            _fundsDividendsPersist = fundsDividendsPersist;
            _cache = cache;
        }

        public async Task<IEnumerable<FundDividend>> GetAllFundsDividendsAsync()
        {
            var cached = await _cache.GetRecordAsync<IEnumerable<FundDividend>>(AllFundDividendsCacheKey);
            if (cached is not null) return cached;
            
            var dividends = await _fundsDividendsPersist.GetAllFundsDividendsAsync();
            await _cache.SetRecordAsync(AllFundDividendsCacheKey, dividends, CacheExpiration);
            return dividends;
        }

        public async Task<IEnumerable<FundDividend>> GetFundDividendsByCodeAsync(string fundCode)
        {
            return await _fundsDividendsPersist.GetFundDividendsByCodeAsync(fundCode);
        }

        public async Task<bool> AddFundDividendsAsync(IEnumerable<FundDividend> fundsDividends)
        {
            return await _fundsDividendsPersist.AddFundsDividendsAsync(fundsDividends);
        }

    }
}