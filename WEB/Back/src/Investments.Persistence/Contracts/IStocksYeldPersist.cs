using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IStocksYeldPersist
    {
        Task<IEnumerable<StocksDividends>> GetStockDividendsByCodeAsync(string stockCode);
        Task<IEnumerable<StocksDividends>> GetAllStockDividendsAsync();
        Task<bool> AddStockDividendsAsync(IEnumerable<StocksDividends> stockDividends);
    }
}