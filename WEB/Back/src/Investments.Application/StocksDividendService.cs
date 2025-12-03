using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class StocksDividendService : IStocksDividendService
    {
        private readonly IStockDividendPersist _stocksDividendsPersist;
        private readonly ICacheService _cache;
        private const string AllStockDividendsCacheKey = "All_Stock_Dividends";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);

        public StocksDividendService(IStockDividendPersist fundsDividendsPersist, ICacheService cache)
        {
            _stocksDividendsPersist = fundsDividendsPersist;
            _cache = cache;
        }

        public async Task<IEnumerable<StockDividend>> GetStockDividendsByCodeAsync(string fundCode)
        {
            return await _stocksDividendsPersist.GetStockDividendsByCodeAsync(fundCode);;
        }

        public async Task<IEnumerable<StockDividend>> GetAllStocksDividendsAsync()
        {
            var cached = await _cache.GetRecordAsync<IEnumerable<StockDividend>>(AllStockDividendsCacheKey);
            if (cached is not null) return cached;
            
            var dividends = await _stocksDividendsPersist.GetAllStockDividendsAsync();
            await _cache.SetRecordAsync(AllStockDividendsCacheKey, dividends, CacheExpiration);
            return dividends;
        }

        public async Task<bool> AddStocksDividendsAsync(IEnumerable<StockDividend> stocksDividends)
        {
            return await _stocksDividendsPersist.AddStockDividendsAsync(stocksDividends);;
        }
    }
}