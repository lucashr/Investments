using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IFundsPersist
    {
        Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode);
        Task<Funds> AddFundAsync(string fundCode);
        Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<Funds> GetFundByCodeAsync(string fundCode);
        Task<IEnumerable<Funds>> GetAllFundsAsync();
        Task<bool> DeleteFundByCodeAsync(string fundCode);
    }
}