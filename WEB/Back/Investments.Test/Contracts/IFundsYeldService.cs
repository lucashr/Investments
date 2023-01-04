using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Test.Contracts
{
    public interface IFundsYeldService
    {
        Task<IEnumerable<FundsYeld>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundsYeld>> GetAllFundsYeldAsync();
    }
}