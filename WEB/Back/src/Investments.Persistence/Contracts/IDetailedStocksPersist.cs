using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IDetailedStocksPersist
    {
        Task<DetailedStocks> GetDetailedStockByCodeAsync(string stockCode);
        Task<IEnumerable<DetailedStocks>> GetAllDetailedStocksAsync();
        Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStocks> detailedStocks);
    }
}