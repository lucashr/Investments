using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class FundsDividendsService : IFundDividendsService
    {

        private readonly IFundsYeldPersist _fundsYeldPersist;

        public FundsDividendsService(IFundsYeldPersist fundsYeldPersist)
        {
            _fundsYeldPersist = fundsYeldPersist;
        }

        public async Task<IEnumerable<FundDividend>> GetAllFundsYeldAsync()
        {
            return await _fundsYeldPersist.GetAllFundsYeldAsync();
        }

        public async Task<IEnumerable<FundDividend>> GetFundYeldByCodeAsync(string fundCode)
        {
            return await _fundsYeldPersist.GetFundYeldByCodeAsync(fundCode);
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividend> fundsYelds)
        {
            return await _fundsYeldPersist.AddFundsYieldsAsync(fundsYelds);
        }

    }
}