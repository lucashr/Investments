using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Persistence.Contracts
{
    public interface ITestePersist
    {
        Task<double> Soma(double a, double b);
    }
}