using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IDetailedStockService
    {
        Task<DetailedStock> GetDetailedStockByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync();
        Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStock> detailedStocks);
    }
}