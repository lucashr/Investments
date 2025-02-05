using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IDetailedStocksPersist : IRepositoryPersist
    {
        Task<DetailedStock> GetDetailedStockByCodeAsync(string stockCode);
        Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync();
        Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStock> detailedStocks);
    }
}