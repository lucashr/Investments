using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Investments.VariablesManager;
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
        private readonly StocksAndDividendsWebScrapingService _service;
        private InvestmentsContext _context;
        private string DatabasePath = $"{AppDomain.CurrentDomain.BaseDirectory}/Investments.db";
        ILogger<StocksAndDividendsWebScrapingService> logger = null;
        private DbContextOptions<InvestmentsContext> options;
        private Mock<SessionContext> _mockSessionContext;

        public WebScrapingStocksAndDividendsServiceTest()
        {
            _mockDetailedStocksPersist = new Mock<IDetailedStocksPersist>();
            _mockStocksDividendsPersist = new Mock<IStockDividendPersist>();
            _mockSessionContext = new Mock<SessionContext>();
            _mockSessionContext.Object.SessionId = "abcd-derfg-achg-1234";

            _service = new StocksAndDividendsWebScrapingService(
                _mockDetailedStocksPersist.Object,
                _mockStocksDividendsPersist.Object,
                logger,
                _mockSessionContext.Object
            );

            options = new DbContextOptionsBuilder<InvestmentsContext>()
            .UseSqlite($"Data Source={DatabasePath}")
            .Options;

            _context = new InvestmentsContext(options);
        }

        [Fact]
        public async Task GetStocksAsync_ShouldReturnDetailedStocks_WhenSuccessful()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _service.GetStocksAsync(cancellationTokenSource);

            _context.DetailedStocks.RemoveRange(_context.DetailedStocks.AsNoTracking());
            await _context.SaveChangesAsync();
            _context.DetailedStocks.RemoveRange(_context.DetailedStocks.AsNoTracking().ToList());
            _context.AddRange(result);
            await _context.SaveChangesAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFundsDividendsAsync_ShouldReturnStocksDividends_WhenSuccessful()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _service.GetStocksDividendsAsync(_context.DetailedStocks, cancellationTokenSource);
            
            _context.StocksDividends.RemoveRange(_context.StocksDividends);
            await _context.SaveChangesAsync();
            _context.StocksDividends.RemoveRange(_context.StocksDividends.ToList());
            _context.AddRange(result);
            await _context.SaveChangesAsync();

            Assert.NotNull(result);
        }
    }
}