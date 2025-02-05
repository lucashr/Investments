using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Persistence.Contracts
{
    public interface IFundDividendPersist : IRepositoryPersist
    {
        Task<IEnumerable<FundDividend>> GetFundDividendsByCodeAsync(string fundCode);
        Task<IEnumerable<FundDividend>> GetAllFundsDividendsAsync();
        Task<bool> AddFundsDividendsAsync(IEnumerable<FundDividend> fundsDividends);
    }
}