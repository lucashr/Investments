using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Investments.Tests
{
    public class StockDividendsControllerTests
    {
        private readonly Mock<IStocksDividendService> _stocksDividendServiceMock;
        private readonly StockDividendsController _controller;

        public StockDividendsControllerTests()
        {
            _stocksDividendServiceMock = new Mock<IStocksDividendService>();
            _controller = new StockDividendsController(_stocksDividendServiceMock.Object);
        }

        private IEnumerable<StockDividend> GenerateFakeStockDividends(int count)
        {
            var faker = new Faker<StockDividend>()
                .RuleFor(s => s.Id, f => Guid.NewGuid().ToString())
                .RuleFor(s => s.FundCode, f => f.Finance.Account(8))
                .RuleFor(s => s.Value, f => f.Random.Double(1, 1000));

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetStockDividendsByCodeShouldReturnOkWhenDividendsExist()
        {
            var dividends = GenerateFakeStockDividends(2).ToList();
            _stocksDividendServiceMock.Setup(s => s.GetStockDividendsByCodeAsync("CXLS11"))
                .ReturnsAsync(dividends);

            var result = await _controller.GetStockDividendsByCode("CXLS11");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<StockDividend>>(okResult.Value);
            returnedDividends.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetStockDividendsByCodeShouldReturnNotFoundWhenNoDividendsExist()
        {
            _stocksDividendServiceMock.Setup(s => s.GetStockDividendsByCodeAsync("CXLS11"))
                .ReturnsAsync(new List<StockDividend>());

            var result = await _controller.GetStockDividendsByCode("CXLS11");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stock dividends found");
        }

        [Fact]
        public async Task GetAllStockDividendsShouldReturnOkWhenDividendsExist()
        {
            var dividends = GenerateFakeStockDividends(2).ToList();
            _stocksDividendServiceMock.Setup(s => s.GetAllStocksDividendsAsync())
                .ReturnsAsync(dividends);

            var result = await _controller.GetAllStockDividends();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<StockDividend>>(okResult.Value);
            returnedDividends.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetAllStockDividendsShouldReturnNotFoundWhenNoDividendsExist()
        {
            _stocksDividendServiceMock.Setup(s => s.GetAllStocksDividendsAsync())
                .ReturnsAsync(new List<StockDividend>());

            var result = await _controller.GetAllStockDividends();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stock dividends found");
        }
    }
}
