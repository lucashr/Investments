using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class DetailedStockService : IDetailedStockService
    {
        private readonly IDetailedStocksPersist _detailedStocksPersist;
        private readonly ICacheService _cache;
        private const string AllStocksCacheKey = "All_Detailed_Stocks";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);

        public DetailedStockService(IDetailedStocksPersist detailedStocksPersist, ICacheService cache)
        {
            _detailedStocksPersist = detailedStocksPersist;
            _cache = cache;
        }

        public async Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStock> detailedFunds)
        {
            return await _detailedStocksPersist.AddDetailedStocksAsync(detailedFunds);
        }

        public async Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync()
        {
            var cached = await _cache.GetRecordAsync<IEnumerable<DetailedStock>>(AllStocksCacheKey);
            if (cached is not null) return cached;
            
            var stocks = await _detailedStocksPersist.GetAllDetailedStocksAsync();
            await _cache.SetRecordAsync(AllStocksCacheKey, stocks, CacheExpiration);
            return stocks;
        }

        public async Task<DetailedStock> GetDetailedStockByCodeAsync(string fundCode)
        {
            return await _detailedStocksPersist.GetDetailedStockByCodeAsync(fundCode);;
        }
    }
}