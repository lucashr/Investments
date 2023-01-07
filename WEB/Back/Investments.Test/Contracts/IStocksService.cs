using System.Threading.Tasks;
using Investments.Test.Application.Dtos;

namespace Investments.Test.Contracts
{
    public interface IStocksService
    {
        Task<StocksDto> Get(string name);
    }
}