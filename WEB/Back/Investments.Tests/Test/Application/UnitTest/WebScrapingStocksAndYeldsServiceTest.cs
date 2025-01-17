using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Domain;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Investments.Tests.Test.Application.UnitTest
{
    public class WebScrapingStocksAndDividendsServiceTest
    {

        private readonly Mock<IDetailedStocksPersist> _mockDetailedStocksPersist;
        private readonly Mock<IStockDividendPersist> _mockStocksDividendsPersist;
        private readonly WebScrapingStocksAndDividendsService _service;
        private InvestmentsContext _context;
        private const string DatabasePath = @"C:\Users\lucas\Desktop\Repositorios\meus_repositorios\Book_projeto_Investments\Angular\Investments\WEB\Back\src\Investments.API\Investments.db";
        ILogger<WebScrapingStocksAndDividendsService> logger = null;
        private DbContextOptions<InvestmentsContext> options;

        public WebScrapingStocksAndDividendsServiceTest()
        {
            _mockDetailedStocksPersist = new Mock<IDetailedStocksPersist>();
            _mockStocksDividendsPersist = new Mock<IStockDividendPersist>();
            _service = new WebScrapingStocksAndDividendsService(
                _mockDetailedStocksPersist.Object,
                _mockStocksDividendsPersist.Object,
                logger
            );

            options = new DbContextOptionsBuilder<InvestmentsContext>()
            .UseSqlite($"Data Source={DatabasePath}")
            .Options;

            _context = new InvestmentsContext(options);
        }

        [Fact]
        public async Task GetStocksAsync_ShouldReturnDetailedStocks_WhenSuccessful()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _service.GetStocksAsync(cancellationTokenSource);

            _context.DetailedStocks.RemoveRange(_context.DetailedStocks.AsNoTracking());
            await _context.SaveChangesAsync();
            _context.DetailedStocks.RemoveRange(_context.DetailedStocks.AsNoTracking().ToList());
            _context.AddRange(result);
            await _context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFundsDividendsAsync_ShouldReturnStocksDividends_WhenSuccessful()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _service.GetStocksDividendsAsync(_context.DetailedStocks, cancellationTokenSource);
            
            _context.StocksDividends.RemoveRange(_context.StocksDividends);
            await _context.SaveChangesAsync();
            _context.StocksDividends.RemoveRange(_context.StocksDividends.ToList());
            _context.AddRange(result);
            await _context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}