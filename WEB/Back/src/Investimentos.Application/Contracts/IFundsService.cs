using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimentos.Application.Dtos;
using Investimentos.Domain;
using Investimentos.Domain.Models;

namespace Investimentos.Application.Contracts
{
    public interface IFundsService
    {
        Task<Funds> UpdateFundByCodeAsync(string oldFundCode, string newFundCode);
        Task<Funds> AddFundAsync(string fundCode);
        Task<Funds> GetFundByCodeAsync(string fundCode);
        Task<IEnumerable<Funds>> GetAllFundsAsync();
        Task<bool> DeleteFundByCodeAsync(string fundCode);
    }
}