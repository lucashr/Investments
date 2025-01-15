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
        Task<IEnumerable<DetailedFund>> GetFundsAsync(CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<DetailedFund>> DriverGetFundsAsync();
        Task<IEnumerable<FundDividend>> GetYeldsFundsAsync(IEnumerable<DetailedFund> detailedFunds, CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<FundDividend>> DriverGetYeldsFundsAsync(IEnumerable<DetailedFund> detailedFunds);
        
    }
}