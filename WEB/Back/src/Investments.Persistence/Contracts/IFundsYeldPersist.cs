using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IFundsYeldPersist
    {
        Task<IEnumerable<FundsYeld>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundsYeld>> GetAllFundsYeldAsync();
        Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds);
    }
}