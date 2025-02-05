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
            var ranks = GenerateFakeBestFundRanks(5);

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(ranks);

            var result = await _controller.GetRank(5);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRanks = Assert.IsAssignableFrom<IEnumerable<BestFundRank>>(okResult.Value);
            returnedRanks.Count().Should().Be(5);
        }

        [Fact]
        public async Task GetRankShouldReturnNotFoundWhenNoRanksExist()
        {
            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(new List<BestFundRank>());

            var result = await _controller.GetRank(5);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No rank found");
        }

        [Fact]
        public async Task GetRankShouldReturnOkWhenRankExistsForDefaultQuantity()
        {
            var ranks = GenerateFakeBestFundRanks(3);

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(ranks);

            var result = await _controller.GetRank(null);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRanks = Assert.IsAssignableFrom<IEnumerable<BestFundRank>>(okResult.Value);
            returnedRanks.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetRankShouldReturnNotFoundWhenNoRanksExistForDefaultQuantity()
        {
            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestFundsAsync(It.IsAny<int?>()))
                .ReturnsAsync(new List<BestFundRank>());

            var result = await _controller.GetRank(null);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            notFoundResult.Value.Should().Be("No rank found");
        }
    }
}
