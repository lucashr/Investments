using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Investments.Tests.Test.Unit.Investments.API
{
    public class DetailedFundsControllerTests
    {
        private readonly Mock<IDetailedFundService> _detailedFundServiceMock;
        private readonly DetailedFundsController _controller;

        public DetailedFundsControllerTests()
        {
            _detailedFundServiceMock = new Mock<IDetailedFundService>();
            _controller = new DetailedFundsController(_detailedFundServiceMock.Object);
        }

        [Fact]
        public async Task GetAllFunds_ShouldReturnOk_WhenFundsExist()
        {
            // Arrange
            var funds = new List<DetailedFund>
            {
                new DetailedFund { Id = "1", FundCode = "FND001" },
                new DetailedFund { Id = "2", FundCode = "FFND002" }
            };

            _detailedFundServiceMock.Setup(s => s.GetAllDetailedFundsAsync())
                .ReturnsAsync(funds);

            // Act
            var result = await _controller.GetAllFunds();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFunds = Assert.IsAssignableFrom<IEnumerable<DetailedFund>>(okResult.Value);
            Assert.Equal(2, returnedFunds.Count());
        }

        [Fact]
        public async Task GetAllFunds_ShouldReturnNotFound_WhenNoFundsExist()
        {
            // Arrange
            _detailedFundServiceMock.Setup(s => s.GetAllDetailedFundsAsync())
                .ReturnsAsync(new List<DetailedFund>());

            // Act
            var result = await _controller.GetAllFunds();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No funds found", notFoundResult.Value);
        }
    }
}