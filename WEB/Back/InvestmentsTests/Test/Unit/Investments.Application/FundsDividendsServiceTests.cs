using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Tests
{
    public class FundsDividendsServiceTests
    {
        private readonly Mock<IFundDividendPersist> _mockFundDividendPersist;
        private readonly FundsDividendsService _fundsDividendsService;

        public FundsDividendsServiceTests()
        {
            _mockFundDividendPersist = new Mock<IFundDividendPersist>();
            _fundsDividendsService = new FundsDividendsService(_mockFundDividendPersist.Object);
        }

        [Fact]
        public async Task GetAllFundsDividendsAsync_ShouldReturnFundsDividends()
        {
            var expectedFundsDividends = new List<FundDividend>
            {
                new FundDividend { Id = "1", FundCode = "FUND1", Value = 100 },
                new FundDividend { Id = "2", FundCode = "FUND2", Value = 150 }
            };

            _mockFundDividendPersist
                .Setup(repo => repo.GetAllFundsDividendsAsync())
                .ReturnsAsync(expectedFundsDividends);

            var result = await _fundsDividendsService.GetAllFundsDividendsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().FundCode.Should().Be("FUND1");
            result.First().Value.Should().Be(100);
        }

        [Fact]
        public async Task GetFundDividendsByCodeAsync_ShouldReturnCorrectFundDividends()
        {
            var fundCode = "FUND1";
            var expectedFundsDividends = new List<FundDividend>
            {
                new FundDividend { Id = "1", FundCode = "FUND1", Value = 100 }
            };

            _mockFundDividendPersist
                .Setup(repo => repo.GetFundDividendsByCodeAsync(fundCode))
                .ReturnsAsync(expectedFundsDividends);

            var result = await _fundsDividendsService.GetFundDividendsByCodeAsync(fundCode);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().FundCode.Should().Be("FUND1");
        }

        [Fact]
        public async Task GetFundDividendsByCodeAsync_ShouldReturnEmpty_WhenFundCodeNotFound()
        {
            var fundCode = "XPTO";
            _mockFundDividendPersist
                .Setup(repo => repo.GetFundDividendsByCodeAsync(fundCode))
                .ReturnsAsync(new List<FundDividend>());

            var result = await _fundsDividendsService.GetFundDividendsByCodeAsync(fundCode);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task AddFundDividendsAsync_ShouldReturnTrue_WhenSuccess()
        {
            var newFundsDividends = new List<FundDividend>
            {
                new FundDividend { Id = "3", FundCode = "FUND3", Value = 200 }
            };

            _mockFundDividendPersist
                .Setup(repo => repo.AddFundsDividendsAsync(newFundsDividends))
                .ReturnsAsync(true);

            var result = await _fundsDividendsService.AddFundDividendsAsync(newFundsDividends);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddFundDividendsAsync_ShouldReturnFalse_WhenFailure()
        {
            var newFundsDividends = new List<FundDividend>
            {
                new FundDividend { Id = "4", FundCode = "FUND4", Value = 250 }
            };

            _mockFundDividendPersist
                .Setup(repo => repo.AddFundsDividendsAsync(newFundsDividends))
                .ReturnsAsync(false);

            var result = await _fundsDividendsService.AddFundDividendsAsync(newFundsDividends);

            result.Should().BeFalse();
        }

    }
}
