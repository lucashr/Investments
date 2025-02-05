using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.Application;
using Investments.Domain;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;

namespace InvestmentsTests.Test
{
    public class DetailedStockServiceTests
    {
        private readonly Mock<IDetailedStocksPersist> _mockDetailedStocksPersist;
        private readonly DetailedStockService _detailedStockService;

        public DetailedStockServiceTests()
        {
            _mockDetailedStocksPersist = new Mock<IDetailedStocksPersist>();
            _detailedStockService = new DetailedStockService(_mockDetailedStocksPersist.Object);
        }

        private IEnumerable<DetailedStock> CreateFakeDetailedStocks(int count)
        {
            var faker = new Faker<DetailedStock>()
                .RuleFor(s => s.FundCode, f => f.Random.String2(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "3");

            return faker.Generate(count);
        }

        [Fact]
        public async Task AddDetailedStocksAsyncShouldReturnTrueWhenStocksAreAddedSuccessfully()
        {
            var detailedStocks = CreateFakeDetailedStocks(2);

            _mockDetailedStocksPersist.Setup(m => m.AddDetailedStocksAsync(detailedStocks)).ReturnsAsync(true);

            var result = await _detailedStockService.AddDetailedStocksAsync(detailedStocks);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllDetailedStocksAsyncShouldReturnListOfDetailedStocks()
        {
            var detailedStocks = CreateFakeDetailedStocks(2);

            _mockDetailedStocksPersist.Setup(m => m.GetAllDetailedStocksAsync()).ReturnsAsync(detailedStocks);

            var result = await _detailedStockService.GetAllDetailedStocksAsync();

            result.Should().BeEquivalentTo(detailedStocks);
            result.Should().HaveCount(detailedStocks.Count());
        }

        [Fact]
        public async Task GetDetailedStockByCodeAsyncShouldReturnStockWhenStockExists()
        {
            var detailedStock = CreateFakeDetailedStocks(1).First();

            _mockDetailedStocksPersist.Setup(m => m.GetDetailedStockByCodeAsync(detailedStock.FundCode)).ReturnsAsync(detailedStock);

            var result = await _detailedStockService.GetDetailedStockByCodeAsync(detailedStock.FundCode);

            result.Should().Be(detailedStock);
        }
    }
}
