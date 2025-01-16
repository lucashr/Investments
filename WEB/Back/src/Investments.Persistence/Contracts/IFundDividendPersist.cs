using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IFundDividendPersist
    {
        Task<IEnumerable<FundDividend>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundDividend>> GetAllFundsYeldAsync();
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividend> fundsYelds);
    }
}