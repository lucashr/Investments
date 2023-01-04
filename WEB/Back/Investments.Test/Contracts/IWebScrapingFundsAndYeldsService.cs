using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Test.Contracts
{
    public interface IWebScrapingFundsAndYeldsService
    {
        Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds);
        Task<IEnumerable<DetailedFunds>> GetFundsAsync();
        Task<IEnumerable<FundsYeld>> GetYeldsFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
    }
}