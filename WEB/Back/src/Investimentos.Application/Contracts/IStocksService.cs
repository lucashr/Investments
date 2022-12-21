using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimentos.Application.Dtos;

namespace Investimentos.Application.Contracts
{
    public interface IStocksService
    {
        Task<StocksDto> Get(string name);
    }
}