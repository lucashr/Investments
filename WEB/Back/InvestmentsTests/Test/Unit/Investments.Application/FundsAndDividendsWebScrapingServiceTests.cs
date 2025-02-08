using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Investments.VariablesManager;

namespace InvestmentsTests.Test
{
    public class FundsAndDividendsWebScrapingServiceTests
    {
        private readonly Mock<IDetailedFundPersist> _mockDetailedFundPersist;
        private readonly Mock<IFundDividendPersist> _mockFundDividendPersist;
        private readonly Mock<ILogger<FundsAndDividendsWebScrapingService>> _mockLogger;
        private readonly Mock<IWebDriver> _mockDriver;
        private readonly Mock<SessionContext> _mockSessionContext;
        private readonly FundsAndDividendsWebScrapingService _scrapingService;

        public FundsAndDividendsWebScrapingServiceTests()
        {
            _mockDetailedFundPersist = new Mock<IDetailedFundPersist>();
            _mockFundDividendPersist = new Mock<IFundDividendPersist>();
            _mockLogger = new Mock<ILogger<FundsAndDividendsWebScrapingService>>();
            _mockDriver = new Mock<IWebDriver>();
            _mockSessionContext = new Mock<SessionContext>();
            _mockSessionContext.Object.SessionId = "abcd-derfg-achg-1234";

            _scrapingService = new FundsAndDividendsWebScrapingService(
                _mockDetailedFundPersist.Object,
                _mockFundDividendPersist.Object,
                _mockLogger.Object,
                _mockSessionContext.Object
            );
        }

        [Fact]
        public async Task GetFundsAsyncShouldReturnListOfDetailedFundsWhenScrapingIsSuccessful()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            _mockDriver.Setup(d => d.FindElements(It.IsAny<By>())).Returns(new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            _mockDetailedFundPersist.Setup(d => d.AddDetailedFundsAsync(It.IsAny<IEnumerable<DetailedFund>>())).ReturnsAsync(true);

            var result = await _scrapingService.GetFundsAsync(cancellationTokenSource);
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetFundDividendsAsyncShouldReturnListOfFundDividendsWhenScrapingIsSuccessful()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var detailedFunds = new List<DetailedFund>
            {
                new DetailedFund { FundCode = "HCTR11" },
                new DetailedFund { FundCode = "ABCP11" }
            };

            _mockDriver.Setup(d => d.FindElements(It.IsAny<By>())).Returns(new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            _mockFundDividendPersist.Setup(d => d.AddFundsDividendsAsync(It.IsAny<List<FundDividend>>())).ReturnsAsync(true);

            var result = await _scrapingService.GetFundDividendsAsync(detailedFunds, cancellationTokenSource);

            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task DriverGetFundsDividendsAsyncShouldReturnEmptyListWhenFundNotFound()
        {
            var detailedFunds = new List<DetailedFund> { new DetailedFund { FundCode = "XPTO" } };

            var result = await _scrapingService.DriverGetFundsDividendsAsync(detailedFunds);

            result.Should().BeEmpty();
        }

    }
}
