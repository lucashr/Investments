using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Test.Domain.Models;

namespace Investments.Test.Application.Contracts
{
    public interface IRankOfTheBestFundsService
    {
        Task<IEnumerable<RankOfTheBestFunds>> GetCalculationRankOfTheBestFundsAsync();
        Task<IEnumerable<RankOfTheBestFunds>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<RankOfTheBestFunds> rankOfTheBestFunds);
    }
}