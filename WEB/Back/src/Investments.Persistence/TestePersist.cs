using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Persistence.Contracts
{
    public class Teste : ITestePersist
    {
        public Task<double> Soma(double a, double b)
        {
            double result = a + b;

            return Task.FromResult<double>(result);
        }
    }
}