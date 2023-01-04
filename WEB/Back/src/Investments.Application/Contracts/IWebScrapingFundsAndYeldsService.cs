using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IWebScrapingFundsAndYeldsService
    {
        // Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        // Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds);
        // Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<IEnumerable<DetailedFunds>> GetFundsAsync();
        Task<IEnumerable<FundsYeld>> GetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        
    }
}