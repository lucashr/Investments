using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
namespace Investments.API.Services.Cache
{
    public class DistributedCacheService : Investments.Application.Contracts.ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetRecordAsync<T>(string recordId)
        {
            var json = await _distributedCache.GetStringAsync(recordId);
            if (string.IsNullOrEmpty(json)) return default;
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null)
        {
            var json = JsonSerializer.Serialize(data);
            var options = new DistributedCacheEntryOptions();
            if (absoluteExpireTime.HasValue)
                options.SetAbsoluteExpiration(absoluteExpireTime.Value);
            await _distributedCache.SetStringAsync(recordId, json, options);
        }

        public async Task RemoveRecordAsync(string recordId)
        {
            await _distributedCache.RemoveAsync(recordId);
        }
    }
}
