using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Domain.Models;

namespace Investimentos.Test.Contracts
{
    public interface IDetailedFundService
    {
        Task<DetailedFunds> GetDetailedFundByCodeAsync(string fundCode);
        Task<IEnumerable<DetailedFunds>> GetAllDetailedFundsAsync();
    }
}