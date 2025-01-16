using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IFundDividendsService
    {
        Task<IEnumerable<FundDividend>> GetFundDividendsByCodeAsync(string fundCode);
        Task<IEnumerable<FundDividend>> GetAllFundsDividendsAsync();
        Task<bool> AddFundDividendsAsync(IEnumerable<FundDividend> fundDividends);
    }
}