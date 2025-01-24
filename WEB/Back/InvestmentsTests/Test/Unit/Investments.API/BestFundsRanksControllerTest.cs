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
    public class BestFundsRanksControllerTest
    {
        private readonly Mock<IRankOfTheBestFundsService> _rankOfTheBestFundsServiceMock;
        private readonly BestFundsRankController _controller;

        public BestFundsRanksControllerTest()
        {
            _rankOfTheBestFundsServiceMock = new Mock<IRankOfTheBestFundsService>();
            _controller = new BestFundsRankController(_rankOfTheBestFundsServiceMock.Object);
        }

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
            var ranks = GenerateFakeBestFundRanks(5);

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(ranks);

            // Act
            var result = await _controller.GetRank(5);

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
                .ReturnsAsync(new List<BestFundRank>());

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
            var ranks = GenerateFakeBestFundRanks(3);

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(ranks);

            // Act
            var result = await _controller.GetRank(null);

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
                .ReturnsAsync(new List<BestFundRank>());

            // Act
            var result = await _controller.GetRank(null);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No rank found");
        }
    }
}
