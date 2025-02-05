using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;

namespace InvestmentsTests.Test
{
    public class DetailedFundServiceTests
    {
        private readonly Mock<IDetailedFundPersist> _mockDetailedFundPersist;
        private readonly DetailedFundService _detailedFundService;

        public DetailedFundServiceTests()
        {
            _mockDetailedFundPersist = new Mock<IDetailedFundPersist>();
            _detailedFundService = new DetailedFundService(_mockDetailedFundPersist.Object);
        }

        private IEnumerable<DetailedFund> CreateFakeDetailedFunds(int count)
        {
            var faker = new Faker<DetailedFund>()
                .RuleFor(f => f.FundCode, f => f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "11");

            return faker.Generate(count);
        }

        [Fact]
        public async Task AddDetailedFundsAsyncShouldReturnTrueWhenFundsAreAddedSuccessfully()
        {
            var detailedFunds = CreateFakeDetailedFunds(2);

            _mockDetailedFundPersist.Setup(m => m.AddDetailedFundsAsync(detailedFunds)).ReturnsAsync(true);

            var result = await _detailedFundService.AddDetailedFundsAsync(detailedFunds);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllDetailedFundsAsyncShouldReturnListOfDetailedFunds()
        {
            var detailedFunds = CreateFakeDetailedFunds(2);

            _mockDetailedFundPersist.Setup(m => m.GetAllDetailedFundsAsync()).ReturnsAsync(detailedFunds);

            var result = await _detailedFundService.GetAllDetailedFundsAsync();

            result.Equals(detailedFunds);
            result.Should().HaveCount(detailedFunds.Count());
        }

        [Fact]
        public async Task GetDetailedFundByCodeAsyncShouldReturnFundWhenFundExists()
        {
            var detailedFund = CreateFakeDetailedFunds(1).First();

            _mockDetailedFundPersist.Setup(m => m.GetDetailedFundByCodeAsync(detailedFund.FundCode)).ReturnsAsync(detailedFund);

            var result = await _detailedFundService.GetDetailedFundByCodeAsync(detailedFund.FundCode);

            result.Should().Be(detailedFund);
        }
    }
}