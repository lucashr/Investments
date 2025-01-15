using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                
                await _detailedStocksPersist.AddDetailedStocksAsync(detailedFunds);

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<DetailedStock>> GetAllDetailedStocksAsync()
        {
            try
            {
                var allFunds = await _detailedStocksPersist.GetAllDetailedStocksAsync();

                return allFunds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DetailedStock> GetDetailedStockByCodeAsync(string fundCode)
        {
            try
            {
                var funds = await _detailedStocksPersist.GetDetailedStockByCodeAsync(fundCode);

                return funds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}