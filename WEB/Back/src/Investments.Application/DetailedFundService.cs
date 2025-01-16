using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class DetailedFundService : IDetailedFundService
    {

        private readonly IDetailedFundPersist _detailedFundPersist;

        public DetailedFundService(IDetailedFundPersist detailedFundPersist)
        {
            _detailedFundPersist = detailedFundPersist;
        }

        public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFund> detailedFunds)
        {
            return await _detailedFundPersist.AddDetailedFundsAsync(detailedFunds);
        }

        public async Task<IEnumerable<DetailedFund>> GetAllDetailedFundsAsync()
        {
            return await _detailedFundPersist.GetAllDetailedFundsAsync();
        }

        public async Task<DetailedFund> GetDetailedFundByCodeAsync(string fundCode)
        {
            return await _detailedFundPersist.GetDetailedFundByCodeAsync(fundCode);
        }
        
    }
}