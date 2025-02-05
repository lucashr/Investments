using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IRankOfTheBestFundsPersist : IRepositoryPersist
    {
        Task<IEnumerable<BestFundRank>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<BestFundRank> rankOfTheBestFunds);
    }
}