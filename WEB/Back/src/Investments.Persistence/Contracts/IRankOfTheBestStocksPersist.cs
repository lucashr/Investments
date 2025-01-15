using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IRankOfTheBestStocksPersist
    {
        Task<IEnumerable<BestStockRank>> GetRankOfTheBestStocksAsync(int? totalFundsInRank = null);
        Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<BestStockRank> rankOfTheBestFunds);
    }
}