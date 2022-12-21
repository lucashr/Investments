using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Domain.Models;

namespace Investimentos.Test.Contracts
{
    public interface IFundsYeldService
    {
        Task<IEnumerable<FundsYeld>> GetFundYeldByCodeAsync(string fundCode);
        Task<IEnumerable<FundsYeld>> GetAllFundsYeldAsync();
    }
}