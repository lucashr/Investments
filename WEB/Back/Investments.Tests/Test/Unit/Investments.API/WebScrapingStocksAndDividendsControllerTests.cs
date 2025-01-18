using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Domain.Models;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Investments.Tests.Test.Unit.Investments.API
{
    public class WebScrapingStocksAndDividendsControllerTests
    {
        private readonly Mock<IWebScrapingStocksAndDividendsService> _webScrapingStocksAndDividendsServiceMock;
        private readonly Mock<IRankOfTheBestStocksService> _rankOfTheBestStocksServiceMock;
        private readonly Mock<IDetailedStockService> _detailedStockServiceMock;
        private readonly Mock<IStocksDividendService> _stocksDividendServiceMock;
        private readonly Mock<WebScrapingSocketManager> _socketManagerMock;
        private readonly WebScrapingStocksAndDividendsController _controller;

        public WebScrapingStocksAndDividendsControllerTests()
        {
            _webScrapingStocksAndDividendsServiceMock = new Mock<IWebScrapingStocksAndDividendsService>();
            _rankOfTheBestStocksServiceMock = new Mock<IRankOfTheBestStocksService>();
            _detailedStockServiceMock = new Mock<IDetailedStockService>();
            _stocksDividendServiceMock = new Mock<IStocksDividendService>();
            _socketManagerMock = new Mock<WebScrapingSocketManager>();

            _controller = new WebScrapingStocksAndDividendsController(
                _webScrapingStocksAndDividendsServiceMock.Object,
                _rankOfTheBestStocksServiceMock.Object,
                _socketManagerMock.Object,
                _detailedStockServiceMock.Object,
                _stocksDividendServiceMock.Object
            );
        }

        // Helper method to create fake stocks
        private List<DetailedStock> GenerateFakeStocks(int count)
        {
            var stockFaker = new Faker<DetailedStock>()
                .RuleFor(s => s.FundCode, f => f.Random.AlphaNumeric(6))
                .RuleFor(s => s.PriceOnWorkingCapital, f => f.Random.Double(1, 100))
                .RuleFor(s => s.ROE, f => f.Random.Double(1, 30));

            return stockFaker.Generate(count);
        }

        // Helper method to create fake dividends
        private List<StockDividend> GenerateFakeDividends(int count)
        {
            var dividendFaker = new Faker<StockDividend>()
                .RuleFor(d => d.Id, f => f.Random.Guid().ToString())
                .RuleFor(d => d.FundCode, f => f.Random.AlphaNumeric(6))
                .RuleFor(d => d.Value, f => f.Random.Double(1, 100));

            return dividendFaker.Generate(count);
        }

        // Helper method to create fake rankings
        private List<BestStockRank> GenerateFakeBestStockRanks(int count)
        {
            var rankFaker = new Faker<BestStockRank>()
                .RuleFor(r => r.Id, f => f.Random.Guid().ToString())
                .RuleFor(r => r.FundCode, f => f.Random.AlphaNumeric(6));

            return rankFaker.Generate(count);
        }

        [Fact]
        public async Task GetStocksAsyncShouldReturnOkWhenStocksExist()
        {
            // Arrange
            var stocks = GenerateFakeStocks(2); // Use Bogus to generate fake stocks

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(stocks);

            // Act
            var result = await _controller.GetStocksAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStocks = Assert.IsAssignableFrom<IEnumerable<DetailedStock>>(okResult.Value);
            returnedStocks.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetStocksAsyncShouldReturnNotFoundWhenNoStocksExist()
        {
            // Arrange
            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<DetailedStock>());

            // Act
            var result = await _controller.GetStocksAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stocks found.");
        }

        [Fact]
        public async Task GetStockDividendsAsyncShouldReturnOkWhenDividendsExist()
        {
            // Arrange
            var detailedStocks = GenerateFakeStocks(1); // Use Bogus to generate fake stocks
            var dividends = GenerateFakeDividends(1); // Use Bogus to generate fake dividends
            var ranking = GenerateFakeBestStockRanks(1); // Use Bogus to generate fake rankings

            _detailedStockServiceMock
                .Setup(d => d.GetAllDetailedStocksAsync())
                .ReturnsAsync(detailedStocks);

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksDividendsAsync(It.IsAny<IEnumerable<DetailedStock>>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(dividends);

            _rankOfTheBestStocksServiceMock
                .Setup(r => r.GetRankOfTheBestStocksAsync(1))
                .ReturnsAsync(ranking);

            // Act
            var result = await _controller.GetStockDividendsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<StockDividend>>(okResult.Value);
            returnedDividends.Count().Should().Be(dividends.Count);
        }

        [Fact]
        public async Task GetStockDividendsAsyncShouldReturnNotFoundWhenNoDividendsExist()
        {
            // Arrange
            var detailedStocks = new List<DetailedStock>();
            _detailedStockServiceMock
                .Setup(d => d.GetAllDetailedStocksAsync())
                .ReturnsAsync(detailedStocks);

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksDividendsAsync(It.IsAny<IEnumerable<DetailedStock>>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<StockDividend>());

            // Act
            var result = await _controller.GetStockDividendsAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stocks dividends found.");
        }

        [Fact]
        public void PauseShouldReturnOkWhenNoProcessIsRunning()
        {
            // Act
            var cancellationTokenSource = new CancellationTokenSource();

            _controller.GetType().GetField("_cancellationTokenSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, cancellationTokenSource);
            _controller.GetType().GetField("_isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, false);

            var result = _controller.Pause();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("No process is running.");
        }

        [Fact]
        public void PauseShouldReturnOkWhenProcessIsStopped()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();
            _controller.GetType().GetField("_cancellationTokenSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, cancellationTokenSource);
            _controller.GetType().GetField("_isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, true);

            // Act
            var result = _controller.Pause();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("Process stopped.");
        }
    }
}
