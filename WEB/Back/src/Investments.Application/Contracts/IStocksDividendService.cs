using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IStocksDividendService
    {
        Task<IEnumerable<StockDividend>> GetStockYeldByCodeAsync(string fundCode);
        Task<IEnumerable<StockDividend>> GetAllStocksYeldAsync();
        Task<bool> AddStocksYieldsAsync(IEnumerable<StockDividend> stocksYelds);
    }
}