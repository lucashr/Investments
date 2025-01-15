using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IFundDividendsService
    {
        Task<IEnumerable<FundDividend>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundDividend>> GetAllFundsYeldAsync();
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividend> fundsYelds);
    }
}