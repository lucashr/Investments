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
using Moq;
using Xunit;

namespace Investments.Tests.Test.Application.UnitTest
{
    public class WebScrapingStocksAndYeldsServiceTest
    {

        private readonly Mock<IDetailedStocksPersist> _mockDetailedStocksPersist;
        private readonly Mock<IStocksYeldPersist> _mockStocksYeldPersist;
        private readonly WebScrapingStocksAndYeldsService _service;
        private InvestmentsContext _context;
        private const string DatabasePath = @"C:\Users\lucas\Desktop\Repositorios\meus_repositorios\Book_projeto_Investments\Angular\Investments\WEB\Back\src\Investments.API\Investments.db";

        private DbContextOptions<InvestmentsContext> options;

        public WebScrapingStocksAndYeldsServiceTest()
        {
            _mockDetailedStocksPersist = new Mock<IDetailedStocksPersist>();
            _mockStocksYeldPersist = new Mock<IStocksYeldPersist>();
            _service = new WebScrapingStocksAndYeldsService(
                _mockDetailedStocksPersist.Object,
                _mockStocksYeldPersist.Object
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
        public async Task GetYeldsFundsAsync_ShouldReturnStocksDividends_WhenSuccessful()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _service.GetYeldsStocksAsync(_context.DetailedStocks, cancellationTokenSource);
            
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