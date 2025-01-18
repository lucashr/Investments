using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Investments.Tests
{
    public class FundDividendsControllerTest
    {
    public class FundDividendsControllerTests
    {
            private readonly Mock<IFundDividendsService> _fundDividendsServiceMock;
            private readonly FundDividendsController _controller;

            public FundDividendsControllerTests()
            {
                _fundDividendsServiceMock = new Mock<IFundDividendsService>();
                _controller = new FundDividendsController(_fundDividendsServiceMock.Object);
            }

            [Fact]
            public async Task GetFundDividendsByCode_ShouldReturnOk_WhenDividendsExist()
            {
                // Arrange
                var fundCode = "ABCD11";
                var mockDividends = new List<FundDividend>
                {
                    new FundDividend { FundCode = fundCode, Value = 10.5, DatePayment = System.DateTime.UtcNow.ToShortDateString() }
                };
                _fundDividendsServiceMock
                    .Setup(service => service.GetFundDividendsByCodeAsync(fundCode))
                    .ReturnsAsync(mockDividends);

                // Act
                var result = await _controller.GetFundDividendsByCode(fundCode);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<FundDividend>>(okResult.Value);
                Assert.Single(returnValue);
                Assert.Equal(fundCode, returnValue.First().FundCode);
            }

            [Fact]
            public async Task GetFundDividendsByCode_ShouldReturnNotFound_WhenNoDividendsExist()
            {
                // Arrange
                var fundCode = "ABCD11";
                _fundDividendsServiceMock
                    .Setup(service => service.GetFundDividendsByCodeAsync(fundCode))
                    .ReturnsAsync(new List<FundDividend>());

                // Act
                var result = await _controller.GetFundDividendsByCode(fundCode);

                // Assert
                var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal("No fund dividends found", notFoundResult.Value);
            }

            [Fact]
            public async Task GetAllFundsDividends_ShouldReturnOk_WhenDividendsExist()
            {
                // Arrange
                var mockDividends = new List<FundDividend>
                {
                    new FundDividend { FundCode = "ABCD11", Value = 10.5, DatePayment = System.DateTime.UtcNow.ToShortDateString() },
                    new FundDividend { FundCode = "EFGH11", Value = 15.2, DatePayment = System.DateTime.UtcNow.ToShortDateString() }
                };

                _fundDividendsServiceMock
                    .Setup(service => service.GetAllFundsDividendsAsync())
                    .ReturnsAsync(mockDividends);

                // Act
                var result = await _controller.GetAllFundsDividends();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<FundDividend>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }

            [Fact]
            public async Task GetAllFundsDividends_ShouldReturnNotFound_WhenNoDividendsExist()
            {
                // Arrange
                _fundDividendsServiceMock
                    .Setup(service => service.GetAllFundsDividendsAsync())
                    .ReturnsAsync(new List<FundDividend>());

                // Act
                var result = await _controller.GetAllFundsDividends();

                // Assert
                var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal("No funds dividends found", notFoundResult.Value);
            }
        }

    }
}