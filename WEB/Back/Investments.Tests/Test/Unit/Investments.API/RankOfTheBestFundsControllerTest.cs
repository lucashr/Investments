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
    public class RankOfTheBestFundsControllerTests
    {
        private readonly Mock<IRankOfTheBestFundsService> _rankOfTheBestFundsServiceMock;
        private readonly RankOfTheBestFundsController _controller;

        public RankOfTheBestFundsControllerTests()
        {
            _rankOfTheBestFundsServiceMock = new Mock<IRankOfTheBestFundsService>();
            _controller = new RankOfTheBestFundsController(_rankOfTheBestFundsServiceMock.Object);
        }

        // Helper method to generate fake rankings
        private List<BestFundRank> GenerateFakeBestFundRanks(int count)
        {
            var rankFaker = new Faker<BestFundRank>()
                .RuleFor(r => r.FundCode, f => f.Random.AlphaNumeric(6))
                .RuleFor(r => r.RankPrice, f => f.Random.Int(1, 100));

            return rankFaker.Generate(count);
        }

        [Fact]
        public async Task GetRankShouldReturnOkWhenRanksExist()
        {
            // Arrange
            var ranks = GenerateFakeBestFundRanks(5); // Generate 5 fake rankings

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(ranks);

            // Act
            var result = await _controller.GetRank(5); // Pass a quantity to the controller

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRanks = Assert.IsAssignableFrom<IEnumerable<BestFundRank>>(okResult.Value);
            returnedRanks.Count().Should().Be(5);
        }

        [Fact]
        public async Task GetRankShouldReturnNotFoundWhenNoRanksExist()
        {
            // Arrange
            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(new List<BestFundRank>()); // Empty list

            // Act
            var result = await _controller.GetRank(5);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No rank found");
        }

        [Fact]
        public async Task GetRankShouldReturnOkWhenRankExistsForDefaultQuantity()
        {
            // Arrange
            var ranks = GenerateFakeBestFundRanks(3); // Generate 3 fake rankings

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(ranks);

            // Act
            var result = await _controller.GetRank(null); // Pass null for default quantity

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRanks = Assert.IsAssignableFrom<IEnumerable<BestFundRank>>(okResult.Value);
            returnedRanks.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetRankShouldReturnNotFoundWhenNoRanksExistForDefaultQuantity()
        {
            // Arrange
            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(new List<BestFundRank>()); // Empty list

            // Act
            var result = await _controller.GetRank(null); // Pass null for default quantity

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No rank found");
        }
    }
}
