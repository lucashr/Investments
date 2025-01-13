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
        private readonly IStocksYeldPersist _stocksYeldPersist;

        public StocksDividendService(IStocksYeldPersist fundsYeldPersist)
        {
            _stocksYeldPersist = fundsYeldPersist;
        }

        public async Task<IEnumerable<StocksDividends>> GetStockYeldByCodeAsync(string fundCode)
        {
            try
            {
                var funds = await _stocksYeldPersist.GetStockDividendsByCodeAsync(fundCode);

                return funds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<StocksDividends>> GetAllStocksYeldAsync()
        {
            try
            {
                var funds = await _stocksYeldPersist.GetAllStockDividendsAsync();

                return funds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddStocksYieldsAsync(IEnumerable<StocksDividends> stocksYelds)
        {
            try
            {

                await _stocksYeldPersist.AddStockDividendsAsync(stocksYelds);

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}