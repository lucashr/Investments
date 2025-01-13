using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IWebScrapingFundsAndDividendsService
    {
        Task<IEnumerable<DetailedFunds>> GetFundsAsync(CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<DetailedFunds>> DriverGetFundsAsync();
        Task<IEnumerable<FundDividends>> GetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds, CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<FundDividends>> DriverGetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        
    }
}