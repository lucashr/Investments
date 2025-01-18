using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Fact]
        public async Task GetAllStocks_ShouldReturnOk_WhenStocksExist()
        {
            // Arrange
            var stocks = new List<DetailedStock>
            {
                new DetailedStock { Id = "1", FundCode = "STK001" },
                new DetailedStock { Id = "2", FundCode = "STK002" }
            };

            _detailedStockServiceMock.Setup(s => s.GetAllDetailedStocksAsync())
                .ReturnsAsync(stocks);

            // Act
            var result = await _controller.GetAllStocks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStocks = Assert.IsAssignableFrom<IEnumerable<DetailedStock>>(okResult.Value);
            Assert.Equal(2, returnedStocks.Count());
        }

        [Fact]
        public async Task GetAllStocks_ShouldReturnNotFound_WhenNoStocksExist()
        {
            // Arrange
            _detailedStockServiceMock.Setup(s => s.GetAllDetailedStocksAsync())
                .ReturnsAsync(new List<DetailedStock>());

            // Act
            var result = await _controller.GetAllStocks();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No stocks found", notFoundResult.Value);
        }
    }
}