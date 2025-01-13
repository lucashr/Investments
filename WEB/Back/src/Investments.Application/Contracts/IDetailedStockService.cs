using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IDetailedStockService
    {
        Task<DetailedStocks> GetDetailedStockByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedStocks>> GetAllDetailedStocksAsync();
        Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStocks> detailedStocks);
    }
}