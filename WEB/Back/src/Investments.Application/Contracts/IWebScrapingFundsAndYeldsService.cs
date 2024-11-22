using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IWebScrapingFundsAndYeldsService
    {
        Task<IEnumerable<DetailedFunds>> GetFundsAsync(CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<DetailedFunds>> DriverGetFundsAsync();
        Task<IEnumerable<FundsYeld>> GetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds, CancellationTokenSource cancellationTokenSource);
        Task<IEnumerable<FundsYeld>> DriverGetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        
    }
}