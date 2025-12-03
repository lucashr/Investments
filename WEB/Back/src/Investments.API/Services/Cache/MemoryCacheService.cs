using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
namespace Investments.API.Services.Cache
{
    public class MemoryCacheService : Investments.Application.Contracts.ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<T> GetRecordAsync<T>(string recordId)
        {
            if (_memoryCache.TryGetValue(recordId, out var value))
            {
                return Task.FromResult((T)value);
            }
            return Task.FromResult(default(T));
        }

        public Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (absoluteExpireTime.HasValue)
                options.SetAbsoluteExpiration(absoluteExpireTime.Value);

            _memoryCache.Set(recordId, data, options);
            return Task.CompletedTask;
        }

        public Task RemoveRecordAsync(string recordId)
        {
            _memoryCache.Remove(recordId);
            return Task.CompletedTask;
        }
    }
}
