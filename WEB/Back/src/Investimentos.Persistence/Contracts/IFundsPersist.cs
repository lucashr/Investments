using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Domain.Models;

namespace Investimentos.Persistence.Contracts
{
    public interface IFundsPersist
    {
        Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode);
        Task<Funds> AddFundAsync(string fundCode);
        Task<Funds> GetFundByCodeAsync(string fundCode);
        Task<IEnumerable<Funds>> GetAllFundsAsync();
        Task<bool> DeleteFundByCodeAsync(string fundCode);
    }
}