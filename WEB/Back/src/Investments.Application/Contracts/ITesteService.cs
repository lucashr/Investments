using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Application.Contracts
{
    public interface ITesteService
    {
        Task<double> Soma(double a, double b);
    }
}