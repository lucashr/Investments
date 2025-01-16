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

        private readonly IFundDividendPersist _fundsDividendsPersist;

        public FundsDividendsService(IFundDividendPersist fundsDividendsPersist)
        {
            _fundsDividendsPersist = fundsDividendsPersist;
        }

        public async Task<IEnumerable<FundDividend>> GetAllFundsDividendsAsync()
        {
            return await _fundsDividendsPersist.GetAllFundsDividendsAsync();
        }

        public async Task<IEnumerable<FundDividend>> GetFundDividendsByCodeAsync(string fundCode)
        {
            return await _fundsDividendsPersist.GetFundDividendsByCodeAsync(fundCode);
        }

        public async Task<bool> AddFundDividendsAsync(IEnumerable<FundDividend> fundsDividends)
        {
            return await _fundsDividendsPersist.AddFundsDividendsAsync(fundsDividends);
        }

    }
}