using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Investments.Tests
{
    public class WebScrapingFundsAndDividendsControllerTests
    {
        private readonly Mock<IWebScrapingFundsAndDividendsService> _webScrapingFundsAndDividendsMock;
        private readonly Mock<IRankOfTheBestFundsService> _rankOfTheBestFundsServiceMock;
        private readonly Mock<IDetailedFundService> _detailedFundServiceMock;
        private readonly Mock<WebScrapingSocketManager> _socketManagerMock;
        private readonly FundsAndDividendsWebScrapingController _controller;

        public WebScrapingFundsAndDividendsControllerTests()
        {
            _webScrapingFundsAndDividendsMock = new Mock<IWebScrapingFundsAndDividendsService>();
            _rankOfTheBestFundsServiceMock = new Mock<IRankOfTheBestFundsService>();
            _detailedFundServiceMock = new Mock<IDetailedFundService>();
            _socketManagerMock = new Mock<WebScrapingSocketManager>();

            _controller = new FundsAndDividendsWebScrapingController(
                _webScrapingFundsAndDividendsMock.Object,
                _rankOfTheBestFundsServiceMock.Object,
                _socketManagerMock.Object,
                _detailedFundServiceMock.Object
            );

        }

        private IEnumerable<DetailedFund> GenerateFakeDetailedFunds(int count)
        {
            return new Faker<DetailedFund>()
                .RuleFor(f => f.FundCode, f => f.Finance.Account(8))
                .Generate(count);
        }

        private IEnumerable<FundDividend> GenerateFakeFundDividends(int count, string fundCode)
        {
            return new Faker<FundDividend>()
                .RuleFor(d => d.Id, f => Guid.NewGuid().ToString())
                .RuleFor(d => d.FundCode, _ => fundCode)
                .RuleFor(d => d.Value, f => f.Random.Double(10, 1000))
                .Generate(count);
        }

        private IEnumerable<BestFundRank> GenerateFakeBestFundRanks(int count)
        {
            return new Faker<BestFundRank>()
                .RuleFor(r => r.Id, f => Guid.NewGuid().ToString())
                .RuleFor(r => r.FundCode, f => f.Finance.Account(8))
                .RuleFor(r => r.RankPrice, f => f.Random.Int(1, count))
                .Generate(count);
        }

        [Fact]
        public async Task GetFundsAsyncShouldReturnOkWhenFundsExist()
        {
            // Arrange
            var funds = GenerateFakeDetailedFunds(2).ToList();
            _webScrapingFundsAndDividendsMock
                .Setup(w => w.GetFundsAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(funds);

            // Act
            var result = await _controller.GetFundsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFunds = Assert.IsAssignableFrom<IEnumerable<DetailedFund>>(okResult.Value);
            returnedFunds.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetFundsAsyncShouldReturnNotFoundWhenNoFundsExist()
        {
            // Arrange
            _webScrapingFundsAndDividendsMock
                .Setup(w => w.GetFundsAsync(It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(new List<DetailedFund>());

            // Act
            var result = await _controller.GetFundsAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Should().Be("No funds found.");
        }

        [Fact]
        public async Task GetFundDividendsAsyncShouldReturnOkWhenDividendsExist()
        {
            // Arrange
            var detailedFunds = GenerateFakeDetailedFunds(1).ToList();
            var fundCode = detailedFunds.First().FundCode;
            var dividends = GenerateFakeFundDividends(1, fundCode).ToList();
            var ranking = GenerateFakeBestFundRanks(3).ToList();

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

            returnedDividends.Should().OnlyContain(d => d.FundCode == fundCode);
        }

        [Fact]
        public async Task GetFundDividendsAsyncShouldReturnNotFoundWhenNoDividendsExist()
        {
            // Arrange
            var detailedFunds = GenerateFakeDetailedFunds(20);

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
            notFoundResult.Value.Should().Be("No funds dividends found.");
        }

        [Fact]
        public void PauseShouldReturnOkWhenNoProcessIsRunning()
        {
            _controller.GetType().GetField("_isRunning", BindingFlags.NonPublic | BindingFlags.Static)
                ?.SetValue(null, false);
            // Act
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
            _controller.GetType().GetField("_cancellationTokenSource", BindingFlags.NonPublic | BindingFlags.Static)
                ?.SetValue(null, cancellationTokenSource);
            _controller.GetType().GetField("_isRunning", BindingFlags.NonPublic | BindingFlags.Static)
                ?.SetValue(null, true);

            // Act
            var result = _controller.Pause();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("Process stopped.");
        }
    }
}
