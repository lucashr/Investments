using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IFundsService
    {
        Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode);
        Task<Funds> AddFundAsync(string fundCode);
        Task<bool> AddFundsAsync(IEnumerable<DetailedFunds> detailedFunds);
        Task<Funds> GetFundByCodeAsync(string fundCode);
        Task<IEnumerable<Funds>> GetAllFundsAsync();
        Task<bool> DeleteFundByCodeAsync(string fundCode);
    }
}