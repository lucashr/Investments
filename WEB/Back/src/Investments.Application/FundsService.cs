using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class FundsService : IFundsService
    {

        private readonly IGeneralPersist _generalPersist;
        private readonly IFundsPersist _fundsPersist;
        private readonly IMapper _mapper;

        public FundsService(IGeneralPersist generalPersist, 
                            IFundsPersist fundsPersist,
                            IMapper mapper)
        {
            _generalPersist = generalPersist;
            _fundsPersist = fundsPersist;
            _mapper = mapper;
        }

        public async Task<Funds> AddFundAsync(string fund)
        {
            try
            {
                var retFund = await _fundsPersist.GetFundByCodeAsync(fund);

                if(retFund != null) return null;

                var newFund = new Funds() {FundCode = fund};

                _generalPersist.Add<Funds>(newFund);
                
                await _generalPersist.SaveChangesAsync();

                retFund = await _fundsPersist.GetFundByCodeAsync(newFund.FundCode);

                return retFund;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Task<Funds> GetFundByCodeAsync(string fundCode)
        {
            try
            {
                var fund = _fundsPersist.GetFundByCodeAsync(fundCode);

                return fund;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Task<IEnumerable<Funds>> GetAllFundsAsync()
        {
            try
            {
                var fund = _fundsPersist.GetAllFundsAsync();

                return fund;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFundByCodeAsync(string fundCode)
        {
            try
            {
                var fund = await _fundsPersist.GetFundByCodeAsync(fundCode);
                
                if(fund == null) throw new Exception("Fundo não encontrado par deleção");

                _generalPersist.DetachLocal<Funds>(x=>x.Id == fund.Id);

                _generalPersist.Delete<Funds>(fund);

                return true;
                    
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode)
        {
            try
            {
                var fund = await _fundsPersist.GetFundByCodeAsync(oldFundCode);
                
                if(fund == null) throw new Exception("Fundo não encontrado par atualização");

                fund.FundCode = newFundCode;

                _generalPersist.DetachLocal<Funds>(x=>x.Id == fund.Id);
                
                _generalPersist.Update<Funds>(fund);

                await _generalPersist.SaveChangesAsync();

                fund = await _fundsPersist.GetFundByCodeAsync(newFundCode);

                return fund;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        {
            try
            {
                var result = await _fundsPersist.AddFundsAsync(detailedFunds);

                return result;

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}