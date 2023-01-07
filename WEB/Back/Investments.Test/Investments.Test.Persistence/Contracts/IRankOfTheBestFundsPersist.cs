using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Test.Domain.Models;

namespace Investments.Test.Persistence.Contracts
{
    public interface IRankOfTheBestFundsPersist
    {
        Task<IEnumerable<RankOfTheBestFunds>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<RankOfTheBestFunds> rankOfTheBestFunds);
    }
}