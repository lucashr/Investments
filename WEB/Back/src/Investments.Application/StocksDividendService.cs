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
        private readonly IStockDividendPersist _stocksYeldPersist;

        public StocksDividendService(IStockDividendPersist fundsYeldPersist)
        {
            _stocksYeldPersist = fundsYeldPersist;
        }

        public async Task<IEnumerable<StockDividend>> GetStockYeldByCodeAsync(string fundCode)
        {
            return await _stocksYeldPersist.GetStockDividendsByCodeAsync(fundCode);;
        }

        public async Task<IEnumerable<StockDividend>> GetAllStocksYeldAsync()
        {
            return await _stocksYeldPersist.GetAllStockDividendsAsync();;
        }

        public async Task<bool> AddStocksYieldsAsync(IEnumerable<StockDividend> stocksYelds)
        {
            return await _stocksYeldPersist.AddStockDividendsAsync(stocksYelds);;
        }
    }
}