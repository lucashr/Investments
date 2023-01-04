using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Dtos;

namespace Investments.Application.Contracts
{
    public interface IStocksService
    {
        Task<StocksDto> Get(string name);
    }
}