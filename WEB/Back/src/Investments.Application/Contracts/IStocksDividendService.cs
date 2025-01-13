using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IStocksDividendService
    {
        Task<IEnumerable<StocksDividends>> GetStockYeldByCodeAsync(string fundCode);
        Task<IEnumerable<StocksDividends>> GetAllStocksYeldAsync();
        Task<bool> AddStocksYieldsAsync(IEnumerable<StocksDividends> stocksYelds);
    }
}