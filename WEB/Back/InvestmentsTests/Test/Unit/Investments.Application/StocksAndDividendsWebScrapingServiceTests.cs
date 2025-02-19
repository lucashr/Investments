using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.Application;
using Investments.Domain;
using Investments.Persistence.Contracts;
using Investments.VariablesManager;
using Microsoft.Extensions.Logging;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace InvestmentsTests.Test.Unit.Investments.Application
{
    public class StocksAndDividendsWebScrapingServiceTests
    {
        private readonly Mock<IDetailedStocksPersist> _mockDetailedStocksPersist;
        private readonly Mock<IStockDividendPersist> _mockStockDividendPersist;
        private readonly Mock<ILogger<StocksAndDividendsWebScrapingService>> _mockLogger;
        private readonly SessionContext _sessionContext;
        private readonly StocksAndDividendsWebScrapingService _service;
        private readonly Mock<IWebDriver> _mockDriver;

        public StocksAndDividendsWebScrapingServiceTests()
        {
            _mockDetailedStocksPersist = new Mock<IDetailedStocksPersist>();
            _mockStockDividendPersist = new Mock<IStockDividendPersist>();
            _mockLogger = new Mock<ILogger<StocksAndDividendsWebScrapingService>>();
            _sessionContext = new SessionContext{ SessionId = "abcd-derfg-achg-1234" };
            _mockDriver = new Mock<IWebDriver>();
            _service = new StocksAndDividendsWebScrapingService(
                _mockDetailedStocksPersist.Object,
                _mockStockDividendPersist.Object,
                _mockLogger.Object,
                _sessionContext
            );
            _service.IsRunningTests = true;
        }

        [Fact]
        public async Task GetFundsAsyncShouldReturnListOfDetailedFundsWhenScrapingIsSuccessful()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            _mockDriver.Setup(d => d.FindElements(It.IsAny<By>())).Returns(new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(
                new List<IWebElement> { Mock.Of<IWebElement>() }
            ));
            _mockDetailedStocksPersist.Setup(d => d.AddDetailedStocksAsync(It.IsAny<IEnumerable<DetailedStock>>())).ReturnsAsync(true);

            var result = await _service.GetStocksAsync(cancellationTokenSource);

            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetFundDividendsAsyncShouldReturnListOfFundDividendsWhenScrapingIsSuccessful()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var detailedFunds = new List<DetailedStock>
            {
                new DetailedStock { FundCode = "CMIG4" },
                new DetailedStock { FundCode = "PINE4" }
            };

            _mockDriver.Setup(d => d.FindElements(It.IsAny<By>())).Returns(new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            _mockStockDividendPersist.Setup(d => d.AddStockDividendsAsync(It.IsAny<List<StockDividend>>())).ReturnsAsync(true);

            var result = await _service.GetStocksDividendsAsync(detailedFunds, cancellationTokenSource);

            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task DriverGetFundsDividendsAsyncShouldReturnEmptyListWhenStockNotFound()
        {

            var cancellationTokenSource = new CancellationTokenSource();
            var detailedFunds = new List<DetailedStock> { new DetailedStock { FundCode = "XPTO" } };

            var result = await _service.GetStocksDividendsAsync(detailedFunds, cancellationTokenSource);

            result.Should().BeEmpty();
        }
    }
}