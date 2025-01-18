using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.API.Controllers;
using Investments.Application;
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

        [Fact]
        public async Task GetStocksAsync_ShouldReturnOk_WhenStocksExist()
        {
            // Arrange
            var stocks = new List<DetailedStock>
            {
                new DetailedStock { FundCode = "STK001" },
                new DetailedStock { FundCode = "STK002" }
            };

            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(stocks);

            // Act
            var result = await _controller.GetStocksAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStocks = Assert.IsAssignableFrom<IEnumerable<DetailedStock>>(okResult.Value);
            Assert.Equal(2, returnedStocks.Count());
        }

        [Fact]
        public async Task GetStocksAsync_ShouldReturnNotFound_WhenNoStocksExist()
        {
            // Arrange
            _webScrapingStocksAndDividendsServiceMock
                .Setup(w => w.GetStocksAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<DetailedStock>());

            // Act
            var result = await _controller.GetStocksAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No stocks found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetStockDividendsAsync_ShouldReturnOk_WhenDividendsExist()
        {
            // Arrange
            var detailedStocks = new List<DetailedStock> { new DetailedStock { FundCode = "STK001" } };
            var dividends = new List<StockDividend> { new StockDividend { Id = "1", FundCode = "STK001", Value = 100 } };
            var ranking = new List<BestStockRank> { new BestStockRank { Id = "1", FundCode = "STK001" } };

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
            Assert.Single(returnedDividends);
        }

        [Fact]
        public async Task GetStockDividendsAsync_ShouldReturnNotFound_WhenNoDividendsExist()
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
            Assert.Equal("No stocks dividends found.", notFoundResult.Value);
        }

        [Fact]
        public void Pause_ShouldReturnOk_WhenNoProcessIsRunning()
        {
            // Act
            var result = _controller.Pause();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("No process is running.", okResult.Value);
        }

        [Fact]
        public void Pause_ShouldReturnOk_WhenProcessIsStopped()
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
            Assert.Equal("Process stopped.", okResult.Value);
        }
    }
}