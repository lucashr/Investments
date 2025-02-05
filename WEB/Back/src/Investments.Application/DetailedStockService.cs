using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class DetailedStockService : IDetailedStockService
    {
        private readonly IDetailedStocksPersist _detailedStocksPersist;

        public DetailedStockService(IDetailedStocksPersist detailedStocksPersist)
        {
            _detailedStocksPersist = detailedStocksPersist;
        }

        public async Task<bool> AddDetailedStocksAsync(IEnumerable<DetailedStock> detailedFunds)
        {
            return await _detailedStocksPersist.AddDetailedStocksAsync(detailedFunds);
        }

        public async Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync()
        {
            return await _detailedStocksPersist.GetAllDetailedStocksAsync();
        }

        public async Task<DetailedStock> GetDetailedStockByCodeAsync(string fundCode)
        {
            return await _detailedStocksPersist.GetDetailedStockByCodeAsync(fundCode);;
        }
    }
}