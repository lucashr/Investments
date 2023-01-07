using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Test.Domain.Models;

namespace Investments.Test.Persistence.Contracts
{
    public interface IDetailedFundPersist
    {
        Task<DetailedFunds> GetDetailedFundByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedFunds>> GetAllDetailedFundsAsync();
        Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds);
    }
}