using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Test.Contracts
{
    public interface IDetailedFundService
    {
        Task<DetailedFunds> GetDetailedFundByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedFunds>> GetAllDetailedFundsAsync();
    }
}