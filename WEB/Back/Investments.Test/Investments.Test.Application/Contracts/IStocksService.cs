using System.Threading.Tasks;
using Investments.Test.Application.Dtos;

namespace Investments.Test.Application.Contracts
{
    public interface IStocksService
    {
        Task<StocksDto> Get(string name);
    }
}