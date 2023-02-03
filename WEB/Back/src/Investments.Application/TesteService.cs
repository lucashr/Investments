using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class TesteService : ITesteService
    {

        private readonly ITestePersist _testeService;

        public TesteService(ITestePersist testeService)
        {
            _testeService = testeService;
        }

        public async Task<double> Soma(double a, double b)
        {
            
            var result = await _testeService.Soma(a, b);

            return result;

        }
    }
}