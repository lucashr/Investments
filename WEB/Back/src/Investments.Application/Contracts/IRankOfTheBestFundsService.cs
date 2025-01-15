using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IRankOfTheBestFundsService
    {
        Task<IEnumerable<BestFundRank>> GetCalculationRankOfTheBestFundsAsync();
        Task<IEnumerable<BestFundRank>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<BestFundRank> rankOfTheBestFunds);
    }
}