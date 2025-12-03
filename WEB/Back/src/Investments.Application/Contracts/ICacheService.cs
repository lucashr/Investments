using System;
using System.Threading.Tasks;

namespace Investments.Application.Contracts
{
    public interface ICacheService
    {
        Task<T> GetRecordAsync<T>(string recordId);
        Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null);
        Task RemoveRecordAsync(string recordId);
    }
}
