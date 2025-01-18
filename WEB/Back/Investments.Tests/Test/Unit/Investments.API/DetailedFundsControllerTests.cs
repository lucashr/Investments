using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
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

        private List<DetailedFund> GenerateFakeFunds(int count)
        {
            var faker = new Faker<DetailedFund>()
                .RuleFor(f => f.Id, f => Guid.NewGuid().ToString("D"))
                .RuleFor(f => f.FundCode, f => f.Finance.Account(8)); // Gera códigos simulando contas financeiras

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetAllFundsShouldReturnOkWhenFundsExist()
        {
            // Arrange
            var funds = GenerateFakeFunds(5);

            _detailedFundServiceMock.Setup(s => s.GetAllDetailedFundsAsync())
                .ReturnsAsync(funds);

            // Act
            var result = await _controller.GetAllFunds();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFunds = Assert.IsAssignableFrom<IEnumerable<DetailedFund>>(okResult.Value);
            returnedFunds.Count().Should().Be(5);

        }

        [Fact]
        public async Task GetAllFundsShouldReturnNotFoundWhenNoFundsExist()
        {
            // Arrange
            _detailedFundServiceMock.Setup(s => s.GetAllDetailedFundsAsync())
                .ReturnsAsync(new List<DetailedFund>());

            // Act
            var result = await _controller.GetAllFunds();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No funds found");
        }
    }
}
