using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IDetailedFundPersist
    {
        Task<DetailedFund> GetDetailedFundByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedFund>> GetAllDetailedFundsAsync();
        Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFund> detailedFunds);
    }
}