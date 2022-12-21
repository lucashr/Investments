using System.Threading.Tasks;
using Investimentos.Application.Dtos;

namespace Investimentos.Test.Contracts
{
    public interface IStocksService
    {
        Task<StocksDto> Get(string name);
    }
}