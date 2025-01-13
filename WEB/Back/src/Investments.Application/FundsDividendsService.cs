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

        public async Task<IEnumerable<FundDividends>> GetAllFundsYeldAsync()
        {
            try
            {
                var funds = await _fundsYeldPersist.GetAllFundsYeldAsync();

                return funds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable<FundDividends>> GetFundYeldByCodeAsync(string fundCode)
        {
            try
            {
                var funds = await _fundsYeldPersist.GetFundYeldByCodeAsync(fundCode);

                return funds;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundDividends> fundsYelds)
        {
            try
            {

                 await _fundsYeldPersist.AddFundsYieldsAsync(fundsYelds);

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}