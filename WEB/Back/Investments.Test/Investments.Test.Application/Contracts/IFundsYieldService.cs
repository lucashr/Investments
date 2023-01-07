using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Test.Domain.Models;

namespace Investments.Test.Application.Contracts
{
    public interface IFundsYieldService
    {
        Task<IEnumerable<FundsYeld>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundsYeld>> GetAllFundsYeldAsync();
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds);
    }
}