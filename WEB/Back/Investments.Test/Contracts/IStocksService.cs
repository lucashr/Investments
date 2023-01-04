using System.Threading.Tasks;
using Investments.Application.Dtos;

namespace Investments.Test.Contracts
{
    public interface IStocksService
    {
        Task<StocksDto> Get(string name);
    }
}