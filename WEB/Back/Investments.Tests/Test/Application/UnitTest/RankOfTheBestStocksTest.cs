using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.Application.helpers;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Investments.Tests.Test.Application.UnitTest
{
    public class RankOfTheBestStocksTest
    {

        private const string DatabasePath = @"C:\Users\lucas\Desktop\Repositorios\meus_repositorios\Book_projeto_Investments\Angular\Investments\WEB\Back\src\Investments.API\Investments.db";
        private InvestmentsContext _context;

        public RankOfTheBestStocksTest()
        {

            var options = new DbContextOptionsBuilder<InvestmentsContext>()
            .UseSqlite($"Data Source={DatabasePath}")
            .Options;

            _context = new InvestmentsContext(options);
        }

        [Fact]
        public async void XXXXXXXX()
        {

            var mockRankPersist = new RankOfTheBestStocksPersist(_context);
            var mockDetailedStockService = new DetailedStockService(new DetailedStocksPersist(_context));
            var mockStocksYieldService = new StocksDividendService(new StocksDividendsPersist(_context));
            
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new InvestmentsProfile());
            });
            
            var mapper = new Mapper(config);
            var rank = new RankOfTheBestStocksService(mockRankPersist, mockDetailedStockService, mockStocksYieldService, mapper);
            
            var result = await rank.GetRankOfTheBestStocksAsync(10);

            foreach (var stock in result)
            {
                Debug.WriteLine(stock.FundCode);
            }
        }
    }
}