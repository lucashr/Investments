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
        Task<IEnumerable<DetailedStock>> GetStocksAsync(CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<StockDividend>> GetStocksDividendsAsync(IEnumerable<DetailedStock> detailedFunds, CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<DetailedStock>> DriverGetStocksAsync();
        Task<IEnumerable<StockDividend>> DriverGetStocksDividendsAsync(IEnumerable<DetailedStock> detailedFunds);
        
    }
}