using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Domain.Models;

namespace Investimentos.Application.Contracts
{
    public interface IRankOfTheBestFundsService
    {
        Task<IEnumerable<RankOfTheBestFunds>> GetCalculationRankOfTheBestFundsAsync();
        Task<IEnumerable<RankOfTheBestFunds>> GetRankOfTheBestFundsAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestFundsAsync(IEnumerable<RankOfTheBestFunds> rankOfTheBestFunds);
    }
}