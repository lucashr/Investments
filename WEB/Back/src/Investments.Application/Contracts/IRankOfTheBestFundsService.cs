using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IRankOfTheBestFundsService
    {
        Task<IEnumerable<RankOfTheBestFunds>> GetCalculationRankOfTheBestFundsAsync();
        Task<IEnumerable<RankOfTheBestFunds>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<RankOfTheBestFunds> rankOfTheBestFunds);
    }
}