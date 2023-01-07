using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Test.Domain.Models;

namespace Investments.Test.Application.Contracts
{
    public interface IDetailedFundService
    {
        Task<DetailedFunds> GetDetailedFundByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedFunds>> GetAllDetailedFundsAsync();
        Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<bool> AddFundsYeldsAsync(IEnumerable<DetailedFunds> detailedFunds);
    }
}