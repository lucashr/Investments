using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IWebScrapingStocksAndDividendsService
    {
        Task<IEnumerable<DetailedStocks>> GetStocksAsync(CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<StocksDividends>> GetStocksDividendsAsync(IEnumerable<DetailedStocks> detailedFunds, CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<DetailedStocks>> DriverGetStocksAsync();
        Task<IEnumerable<StocksDividends>> DriverGetStocksDividendsAsync(IEnumerable<DetailedStocks> detailedFunds);
        
    }
}