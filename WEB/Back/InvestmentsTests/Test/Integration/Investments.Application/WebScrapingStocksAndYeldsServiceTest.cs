using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Investments.Tests
{
    public class WebScrapingStocksAndDividendsServiceTest
    {

        private readonly Mock<IDetailedStocksPersist> _mockDetailedStocksPersist;
        private readonly Mock<IStockDividendPersist> _mockStocksDividendsPersist;
        private readonly WebScrapingStocksAndDividendsService _service;
        private InvestmentsContext _context;
        private string DatabasePath = $"{AppDomain.CurrentDomain.BaseDirectory}/Investments.db";
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