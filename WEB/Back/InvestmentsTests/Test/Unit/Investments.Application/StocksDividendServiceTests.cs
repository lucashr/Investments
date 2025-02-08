using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.Application;
using Investments.Domain;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;
using Bogus;

namespace InvestmentsTests.Test.Unit.Investments.Application
{
    public class StocksDividendServiceTests
    {
        private readonly Mock<IStockDividendPersist> _stocksDividendsPersistMock;
        private readonly StocksDividendService _service;

        public StocksDividendServiceTests()
        {
            _stocksDividendsPersistMock = new Mock<IStockDividendPersist>();
            _service = new StocksDividendService(_stocksDividendsPersistMock.Object);
        }

        private List<StockDividend> GenerateStockDividend(int count, string fundCode = null)
        {
            var faker = new Faker<StockDividend>()
                            .RuleFor(d => d.FundCode, f => fundCode ?? f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "11")
                            .RuleFor(d => d.Value, f => f.Random.Double(1, 1000))
                            .RuleFor(d => d.DatePayment, f => f.Date.Past(1).ToShortDateString());

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetStockDividendsByCodeAsync_ShouldReturnStockDividends()
        {
            var fundCode = "AAPL";
            var expectedDividends = GenerateStockDividend(5, fundCode);
            _stocksDividendsPersistMock.Setup(p => p.GetStockDividendsByCodeAsync(fundCode))
                .ReturnsAsync(expectedDividends);
            
            var result = await _service.GetStockDividendsByCodeAsync(fundCode);
            
            result.Should().BeEquivalentTo(expectedDividends);
            _stocksDividendsPersistMock.Verify(p => p.GetStockDividendsByCodeAsync(fundCode), Times.Once);
        }

        [Fact]
        public async Task GetAllStocksDividendsAsync_ShouldReturnAllStockDividends()
        {
            var expectedDividends = GenerateStockDividend(5);
            _stocksDividendsPersistMock.Setup(p => p.GetAllStockDividendsAsync())
                .ReturnsAsync(expectedDividends);
            
            var result = await _service.GetAllStocksDividendsAsync();
            
            result.Should().BeEquivalentTo(expectedDividends);
            _stocksDividendsPersistMock.Verify(p => p.GetAllStockDividendsAsync(), Times.Once);
        }

        [Fact]
        public async Task AddStocksDividendsAsync_ShouldReturnTrue_WhenSuccessfullyAdded()
        {
            var dividendsToAdd = GenerateStockDividend(5);
            _stocksDividendsPersistMock.Setup(p => p.AddStockDividendsAsync(dividendsToAdd))
                .ReturnsAsync(true);
            
            var result = await _service.AddStocksDividendsAsync(dividendsToAdd);
            
            result.Should().BeTrue();
            _stocksDividendsPersistMock.Verify(p => p.AddStockDividendsAsync(dividendsToAdd), Times.Once);
        }
    }
}