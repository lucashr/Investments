using System.Collections.Generic;
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
    public class FundDividendsControllerTests
    {
        private readonly Mock<IFundDividendsService> _fundDividendsServiceMock;
        private readonly FundDividendsController _controller;

        public FundDividendsControllerTests()
        {
            _fundDividendsServiceMock = new Mock<IFundDividendsService>();
            _controller = new FundDividendsController(_fundDividendsServiceMock.Object);
        }

        private List<FundDividend> GenerateFakeDividends(int count, string fundCode = null)
        {
            var faker = new Faker<FundDividend>()
                .RuleFor(d => d.FundCode, f => fundCode ?? f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "11")
                .RuleFor(d => d.Value, f => f.Random.Double(1, 1000))
                .RuleFor(d => d.DatePayment, f => f.Date.Past(1).ToShortDateString());

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetFundDividendsByCodeShouldReturnOkWhenDividendsExist()
        {
            // Arrange
            var fundCode = "ZWBC11";
            var mockDividends = GenerateFakeDividends(3, fundCode);

            _fundDividendsServiceMock
                .Setup(service => service.GetFundDividendsByCodeAsync(fundCode))
                .ReturnsAsync(mockDividends);

            // Act
            var result = await _controller.GetFundDividendsByCode(fundCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<FundDividend>>(okResult.Value);

            returnValue.Count.Should().Be(3);
            returnValue.Should().OnlyContain(d => d.FundCode == fundCode);

        }

        [Fact]
        public async Task GetFundDividendsByCodeShouldReturnNotFoundWhenNoDividendsExist()
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
            notFoundResult.Value.Should().Be("No fund dividends found");
        }

        [Fact]
        public async Task GetAllFundsDividendsShouldReturnOkWhenDividendsExist()
        {
            // Arrange
            var mockDividends = GenerateFakeDividends(5);

            _fundDividendsServiceMock
                .Setup(service => service.GetAllFundsDividendsAsync())
                .ReturnsAsync(mockDividends);

            // Act
            var result = await _controller.GetAllFundsDividends();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<FundDividend>>(okResult.Value);
            returnValue.Count.Should().Be(5);
        }

        [Fact]
        public async Task GetAllFundsDividendsShouldReturnNotFoundWhenNoDividendsExist()
        {
            // Arrange
            _fundDividendsServiceMock
                .Setup(service => service.GetAllFundsDividendsAsync())
                .ReturnsAsync(new List<FundDividend>());

            // Act
            var result = await _controller.GetAllFundsDividends();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No funds dividends found");
        }
    }
}
