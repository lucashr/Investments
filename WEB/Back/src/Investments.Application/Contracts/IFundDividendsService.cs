using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IFundDividendsService
    {
        Task<IEnumerable<FundDividends>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundDividends>> GetAllFundsYeldAsync();
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividends> fundsYelds);
    }
}