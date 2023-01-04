using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Persistence.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class WebScrapingFundsAndYeldsPersist : GeneralPersist, IWebScrapingFundsAndYeldsPersist
    {
        private readonly InvestmentsContext _context;

        public WebScrapingFundsAndYeldsPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        // public async Task<bool> AddDetailedFundsAsync(IEnumerable<DetailedFunds> detailedFunds)
        // {
        //     try
        //     {
        //         _context.Database.ExecuteSqlRaw("DELETE FROM [DetailedFunds]");
        //         _context.AddRange(detailedFunds);
        //         await _context.SaveChangesAsync();

        //         return true;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         return false;
        //     }
        // }

        // public async Task<bool> AddFundsYieldsAsync(IEnumerable<FundsYeld> fundsYelds)
        // {
        //     try
        //     {

        //         _context.Database.ExecuteSqlRaw("DELETE FROM [FundsYeld]");
        //         _context.AddRange(fundsYelds);
        //         await _context.SaveChangesAsync();

        //         return true;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         return false;
        //     }
        // }
    }
}