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

namespace Investments.Tests
{
    public class StocksAndDividendsWebScrapingControllerTests
    {
        private readonly Mock<IWebScrapingStocksAndDividendsService> _webScrapingStocksAndDividendsServiceMock;
        private readonly Mock<IRankOfTheBestStocksService> _rankOfTheBestStocksServiceMock;
        private readonly Mock<IDetailedStockService> _detailedStockServiceMock;
        private readonly Mock<IStocksDividendService> _stocksDividendServiceMock;
        private readonly Mock<WebScrapingSocketManager> _socketManagerMock;
        private readonly StocksAndDividendsWebScrapingController _controller;

        public StocksAndDividendsWebScrapingControllerTests()
        {
            _webScrapingStocksAndDividendsServiceMock = new Mock<IWebScrapingStocksAndDividendsService>();
            _rankOfTheBestStocksServiceMock = new Mock<IRankOfTheBestStocksService>();
            _detailedStockServiceMock = new Mock<IDetailedStockService>();
            _stocksDividendServiceMock = new Mock<IStocksDividendService>();
            _socketManagerMock = new Mock<WebScrapingSocketManager>();

            _controller = new StocksAndDividendsWebScrapingController(
                _webScrapingStocksAndDividendsServiceMock.Object,
                _rankOfTheBestStocksServiceMock.Object,
                _socketManagerMock.Object,
                _detailedStockServiceMock.Object,
                _stocksDividendServiceMock.Object
            );
        }

        private List<DetailedStock> GenerateFakeStocks(int count)
        {
            var stockFaker = new Bogus.Faker<DetailedStock>()
                .RuleFor(s => s.FundCode, f => f.Random.AlphaNumeric(6))
                .RuleFor(s => s.PriceOnWorkingCapital, f => f.Random.Double(1, 100))
                .RuleFor(s => s.ROE, f => f.Random.Double(1, 30));

            return stockFaker.Generate(count);
        }

        private List<StockDividend> GenerateFakeDividends(int count)
        {
            var dividendFaker = new Bogus.Faker<StockDividend>()
                .RuleFor(d => d.Id, f => f.Random.Guid().ToString())
                .RuleFor(d => d.FundCode, f => f.Random.AlphaNumeric(6))
                .RuleFor(d => d.Value, f => f.Random.Double(1, 100));

            return dividendFaker.Generate(count);
        }

        private List<BestStockRank> GenerateFakeBestStockRanks(int count)
        {
            var rankFaker = new Bogus.Faker<BestStockRank>()
                .RuleFor(r => r.Id, f => f.Random.Guid().ToString())
                .RuleFor(r => r.FundCode, f => f.Random.AlphaNumeric(6));

            return rankFaker.Generate(count);
        }

        [Fact]
        public async Task GetStocksAsyncShouldReturnOkWhenStocksExist()
        {
            var stocks = GenerateFakeStocks(2);

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(stocks);

            var result = await _controller.GetStocksAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStocks = Assert.IsAssignableFrom<IEnumerable<DetailedStock>>(okResult.Value);
            returnedStocks.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetStocksAsyncShouldReturnNotFoundWhenNoStocksExist()
        {
            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<DetailedStock>());

            var result = await _controller.GetStocksAsync();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stocks found.");
        }

        [Fact]
        public async Task GetStockDividendsAsyncShouldReturnOkWhenDividendsExist()
        {
            var detailedStocks = GenerateFakeStocks(1);
            var dividends = GenerateFakeDividends(1);
            var ranking = GenerateFakeBestStockRanks(1);

            _detailedStockServiceMock
                .Setup(d => d.GetAllDetailedStocksAsync())
                .ReturnsAsync(detailedStocks);

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksDividendsAsync(It.IsAny<IEnumerable<DetailedStock>>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(dividends);

            _rankOfTheBestStocksServiceMock
                .Setup(r => r.GetRankOfTheBestStocksAsync(1))
                .ReturnsAsync(ranking);

            var result = await _controller.GetStockDividendsAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<StockDividend>>(okResult.Value);
            returnedDividends.Count().Should().Be(dividends.Count);
        }

        [Fact]
        public async Task GetStockDividendsAsyncShouldReturnNotFoundWhenNoDividendsExist()
        {
            var detailedStocks = new List<DetailedStock>();
            _detailedStockServiceMock
                .Setup(d => d.GetAllDetailedStocksAsync())
                .ReturnsAsync(detailedStocks);

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksDividendsAsync(It.IsAny<IEnumerable<DetailedStock>>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<StockDividend>());

            var result = await _controller.GetStockDividendsAsync();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stocks dividends found.");
        }

        [Fact]
        public void PauseShouldReturnOkWhenNoProcessIsRunning()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            _controller.GetType().GetField("_cancellationTokenSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, cancellationTokenSource);
            _controller.GetType().GetField("_isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, false);

            var result = _controller.Pause();

            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("No process is running.");
        }

        [Fact]
        public void PauseShouldReturnOkWhenProcessIsStopped()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            _controller.GetType().GetField("_cancellationTokenSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, cancellationTokenSource);
            _controller.GetType().GetField("_isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, true);

            var result = _controller.Pause();

            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("Process stopped.");
        }
    }
}
