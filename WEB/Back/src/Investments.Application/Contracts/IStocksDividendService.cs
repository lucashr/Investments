using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IStocksDividendService
    {
        Task<IEnumerable<StockDividend>> GetStockDividendsByCodeAsync(string fundCode);
        Task<IEnumerable<StockDividend>> GetAllStocksDividendsAsync();
        Task<bool> AddStocksDividendsAsync(IEnumerable<StockDividend> stocksDividends);
    }
}