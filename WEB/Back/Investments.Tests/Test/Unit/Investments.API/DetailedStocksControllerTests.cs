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

namespace Investments.Tests.Test.Unit.Investments.API
{
    public class DetailedStocksControllerTests
    {
        private readonly Mock<IDetailedStockService> _detailedStockServiceMock;
        private readonly DetailedStocksController _controller;

        public DetailedStocksControllerTests()
        {
            _detailedStockServiceMock = new Mock<IDetailedStockService>();
            _controller = new DetailedStocksController(_detailedStockServiceMock.Object);
        }

        private List<DetailedStock> GenerateFakeStocks(int count)
        {
            var faker = new Faker<DetailedStock>()
                .RuleFor(s => s.Id, f => Guid.NewGuid().ToString("D"))
                .RuleFor(s => s.FundCode, f => f.Finance.Account(8));

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetAllStocksShouldReturnOkWhenStocksExist()
        {
            // Arrange
            var stocks = GenerateFakeStocks(3);

            _detailedStockServiceMock.Setup(s => s.GetAllDetailedStocksAsync())
                .ReturnsAsync(stocks);

            // Act
            var result = await _controller.GetAllStocks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStocks = Assert.IsAssignableFrom<IEnumerable<DetailedStock>>(okResult.Value);
            returnedStocks.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetAllStocksShouldReturnNotFoundWhenNoStocksExist()
        {
            // Arrange
            _detailedStockServiceMock.Setup(s => s.GetAllDetailedStocksAsync())
                .ReturnsAsync(new List<DetailedStock>());

            // Act
            var result = await _controller.GetAllStocks();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No stocks found");
        }
    }
}
