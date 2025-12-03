using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class DetailedFundService : IDetailedFundService
    {

        private readonly IDetailedFundPersist _detailedFundPersist;
        private readonly ICacheService _cache;
        private const string AllFundsCacheKey = "All_Detailed_Funds";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);

        public DetailedFundService(IDetailedFundPersist detailedFundPersist, ICacheService cache)
        {
            _detailedFundPersist = detailedFundPersist;
            _cache = cache;
        }

        public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFund> detailedFunds)
        {
            return await _detailedFundPersist.AddDetailedFundsAsync(detailedFunds);
        }

        public async Task<IEnumerable<DetailedFund>> GetAllDetailedFundsAsync()
        {
            var cached = await _cache.GetRecordAsync<IEnumerable<DetailedFund>>(AllFundsCacheKey);
            if (cached is not null) return cached;
            
            var funds = await _detailedFundPersist.GetAllDetailedFundsAsync();
            await _cache.SetRecordAsync(AllFundsCacheKey, funds, CacheExpiration);
            return funds;
        }

        public async Task<DetailedFund> GetDetailedFundByCodeAsync(string fundCode)
        {
            return await _detailedFundPersist.GetDetailedFundByCodeAsync(fundCode);
        }
        
    }
}