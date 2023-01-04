using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Test.Contracts
{
    public interface IFundsService
    {
        Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode);
        Task<Funds> AddFundAsync(string fundCode);
        Task<Funds> GetFundByCodeAsync(string fundCode);
        Task<IEnumerable<Funds>> GetAllFundsAsync();
        Task<bool> DeleteFundByCodeAsync(string fundCode);
    }
}