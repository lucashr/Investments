using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Application.Contracts;
using Investimentos.Domain.Models;
using Investimentos.Persistence.Contracts;

namespace Investimentos.Application
{
    public class DetailedFundService : IDetailedFundService
    {

        private readonly IDetailedFundPersist _detailedFundPersist;

        public DetailedFundService(IDetailedFundPersist detailedFundPersist)
        {
            _detailedFundPersist = detailedFundPersist;
        }

        public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            try
            {
                
                await _detailedFundPersist.AddDetailedFundsAsync(detailedFunds);
                // _generalPersist.AddRange(detailedFunds.ToArray());
                // await _generalPersist.SaveChangesAsync();

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Task<bool> AddFundsYeldsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailedFunds>> GetAllDetailedFundsAsync()
        {
            try
            {
                var allFunds = await _detailedFundPersist.GetAllDetailedFundsAsync();

                return allFunds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DetailedFunds> GetDetailedFundByCodeAsync(string fundCode)
        {
            try
            {
                var funds = await _detailedFundPersist.GetDetailedFundByCodeAsync(fundCode);

                return funds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
    }
}