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

namespace Investments.Tests
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
                .RuleFor(f => f.FundCode, f => f.Finance.Account(8));

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetAllFundsShouldReturnOkWhenFundsExist()
        {
            var funds = GenerateFakeFunds(5);

            _detailedFundServiceMock.Setup(s => s.GetAllDetailedFundsAsync())
                .ReturnsAsync(funds);

            var result = await _controller.GetAllFunds();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFunds = Assert.IsAssignableFrom<IEnumerable<DetailedFund>>(okResult.Value);
            returnedFunds.Count().Should().Be(5);

        }

        [Fact]
        public async Task GetAllFundsShouldReturnNotFoundWhenNoFundsExist()
        {
            _detailedFundServiceMock.Setup(s => s.GetAllDetailedFundsAsync())
                .ReturnsAsync(new List<DetailedFund>());

            var result = await _controller.GetAllFunds();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No funds found");
        }
    }
}
