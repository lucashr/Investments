using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IStockDividendPersist : IRepositoryPersist
    {
        Task<IEnumerable<StockDividend>> GetStockDividendsByCodeAsync(string stockCode);
        Task<IEnumerable<StockDividend>> GetAllStockDividendsAsync();
        Task<bool> AddStockDividendsAsync(IEnumerable<StockDividend> stockDividends);
    }
}