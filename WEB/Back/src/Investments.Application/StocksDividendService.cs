using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class StocksDividendService : IStocksDividendService
    {
        private readonly IStockDividendPersist _stocksDividendsPersist;

        public StocksDividendService(IStockDividendPersist fundsDividendsPersist)
        {
            _stocksDividendsPersist = fundsDividendsPersist;
        }

        public async Task<IEnumerable<StockDividend>> GetStockDividendsByCodeAsync(string fundCode)
        {
            return await _stocksDividendsPersist.GetStockDividendsByCodeAsync(fundCode);;
        }

        public async Task<IEnumerable<StockDividend>> GetAllStocksDividendsAsync()
        {
            return await _stocksDividendsPersist.GetAllStockDividendsAsync();;
        }

        public async Task<bool> AddStocksDividendsAsync(IEnumerable<StockDividend> stocksDividends)
        {
            return await _stocksDividendsPersist.AddStockDividendsAsync(stocksDividends);;
        }
    }
}