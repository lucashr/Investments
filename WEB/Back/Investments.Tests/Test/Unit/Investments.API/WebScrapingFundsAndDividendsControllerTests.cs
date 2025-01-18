using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Tests.Helpers;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Investments.Tests
{
    public class WebScrapingFundsAndDividendsControllerTests
    {
        private readonly Mock<IWebScrapingFundsAndDividendsService> _webScrapingFundsAndDividendsMock;
        private readonly Mock<IRankOfTheBestFundsService> _rankOfTheBestFundsServiceMock;
        private readonly Mock<IDetailedFundService> _detailedFundServiceMock;
        private readonly Mock<WebScrapingSocketManager> _socketManagerMock;
        private readonly WebScrapingFundsAndDividendsController _controller;

        public WebScrapingFundsAndDividendsControllerTests()
        {
            _webScrapingFundsAndDividendsMock = new Mock<IWebScrapingFundsAndDividendsService>();
            _rankOfTheBestFundsServiceMock = new Mock<IRankOfTheBestFundsService>();
            _detailedFundServiceMock = new Mock<IDetailedFundService>();
            _socketManagerMock = new Mock<WebScrapingSocketManager>();
            _controller = new WebScrapingFundsAndDividendsController(
                _webScrapingFundsAndDividendsMock.Object,
                _rankOfTheBestFundsServiceMock.Object,
                _socketManagerMock.Object,
                _detailedFundServiceMock.Object
            );
        }

        [Fact]
        public async Task GetFundsAsync_ShouldReturnOk_WhenFundsExist()
        {
            // Arrange
            var funds = new List<DetailedFund>
            {
                new DetailedFund { FundCode = "FND001" },
                new DetailedFund { FundCode = "FND002" }
            };
            _webScrapingFundsAndDividendsMock
                .Setup(w => w.GetFundsAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(funds);

            // Act
            var result = await _controller.GetFundsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFunds = Assert.IsAssignableFrom<IEnumerable<DetailedFund>>(okResult.Value);
            Assert.Equal(2, returnedFunds.Count());
        }

        [Fact]
        public async Task GetFundsAsync_ShouldReturnNotFound_WhenNoFundsExist()
        {
            // Arrange
            _webScrapingFundsAndDividendsMock
                .Setup(w => w.GetFundsAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<DetailedFund>());

            // Act
            var result = await _controller.GetFundsAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No funds found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetFundDividendsAsync_ShouldReturnOk_WhenDividendsExist()
        {
            // Arrange
            var detailedFunds = new List<DetailedFund> { new DetailedFund { FundCode = "FND001" } };
            var dividends = new List<FundDividend> { new FundDividend { Id = "1", FundCode = "FND001", Value = 100 } };
            var ranking = new List<BestFundRank> 
            { 
                new BestFundRank { FundCode = "FND001", Id = "1", RankPrice = 1 },
                new BestFundRank { FundCode = "FND002", Id = "2", RankPrice = 2 },
                new BestFundRank { FundCode = "FND003", Id = "3", RankPrice = 3 }
            };

            _detailedFundServiceMock
                .Setup(d => d.GetAllDetailedFundsAsync())
                .ReturnsAsync(detailedFunds);

            _webScrapingFundsAndDividendsMock
                .Setup(w => w.GetFundDividendsAsync(It.IsAny<IEnumerable<DetailedFund>>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(dividends);

            _rankOfTheBestFundsServiceMock
                .Setup(r => r.GetCalculationRankOfTheBestFundsAsync())
                .ReturnsAsync(ranking);

            // Act
            var result = await _controller.GetFundDividendsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<FundDividend>>(okResult.Value);
            Assert.Single(returnedDividends);
        }

        [Fact]
        public async Task GetFundDividendsAsync_ShouldReturnNotFound_WhenNoDividendsExist()
        {
            // Arrange
            var detailedFunds = new List<DetailedFund>();
            _detailedFundServiceMock
                .Setup(d => d.GetAllDetailedFundsAsync())
                .ReturnsAsync(detailedFunds);

            _webScrapingFundsAndDividendsMock
                .Setup(w => w.GetFundDividendsAsync(It.IsAny<IEnumerable<DetailedFund>>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<FundDividend>());

            // Act
            var result = await _controller.GetFundDividendsAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No funds dividends found.", notFoundResult.Value);
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