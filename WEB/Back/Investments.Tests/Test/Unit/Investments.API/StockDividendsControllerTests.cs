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
    public class StockDividendsControllerTests
    {
        private readonly Mock<IStocksDividendService> _stocksDividendServiceMock;
        private readonly StockDividendsController _controller;

        public StockDividendsControllerTests()
        {
            _stocksDividendServiceMock = new Mock<IStocksDividendService>();
            _controller = new StockDividendsController(_stocksDividendServiceMock.Object);
        }

        [Fact]
        public async Task GetStockDividendsByCode_ShouldReturnOk_WhenDividendsExist()
        {
            // Arrange
            var dividends = new List<StockDividend>
            {
                new StockDividend { Id = "1", FundCode = "STK001", Value = 100.50 },
                new StockDividend { Id = "2", FundCode = "STK002", Value = 200.75 }
            };

            _stocksDividendServiceMock.Setup(s => s.GetStockDividendsByCodeAsync("STK001"))
                .ReturnsAsync(dividends);

            // Act
            var result = await _controller.GetStockDividendsByCode("STK001");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<StockDividend>>(okResult.Value);
            Assert.Equal(2, returnedDividends.Count());
        }

        [Fact]
        public async Task GetStockDividendsByCode_ShouldReturnNotFound_WhenNoDividendsExist()
        {
            // Arrange
            _stocksDividendServiceMock.Setup(s => s.GetStockDividendsByCodeAsync("STK002"))
                .ReturnsAsync(new List<StockDividend>());

            // Act
            var result = await _controller.GetStockDividendsByCode("STK002");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No stock dividends found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllStockDividends_ShouldReturnOk_WhenDividendsExist()
        {
            // Arrange
            var dividends = new List<StockDividend>
            {
                new StockDividend { Id = "1", FundCode = "STK001", Value = 100.50 },
                new StockDividend { Id = "2", FundCode = "STK002", Value = 200.75 }
            };

            _stocksDividendServiceMock.Setup(s => s.GetAllStocksDividendsAsync())
                .ReturnsAsync(dividends);

            // Act
            var result = await _controller.GetAllStockDividends();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDividends = Assert.IsAssignableFrom<IEnumerable<StockDividend>>(okResult.Value);
            Assert.Equal(2, returnedDividends.Count());
        }

        [Fact]
        public async Task GetAllStockDividends_ShouldReturnNotFound_WhenNoDividendsExist()
        {
            // Arrange
            _stocksDividendServiceMock.Setup(s => s.GetAllStocksDividendsAsync())
                .ReturnsAsync(new List<StockDividend>());

            // Act
            var result = await _controller.GetAllStockDividends();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No stock dividends found", notFoundResult.Value);
        }
    }
}