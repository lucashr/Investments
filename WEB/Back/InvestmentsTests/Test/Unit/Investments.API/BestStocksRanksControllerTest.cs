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
    public class BestStocksRanksControllerTest
    {
        private readonly Mock<IRankOfTheBestStocksService> _rankOfTheBestFundsServiceMock;
        private readonly BestStocksRankController _controller;

        public BestStocksRanksControllerTest()
        {
            _rankOfTheBestFundsServiceMock = new Mock<IRankOfTheBestStocksService>();
            _controller = new BestStocksRankController(_rankOfTheBestFundsServiceMock.Object);
        }

        private IEnumerable<BestStockRank> GenerateFakeBestStockRank(int count)
        {
            var rankFaker = new Faker<BestStockRank>()
                .RuleFor(r => r.FundCode, f => f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"));

            return rankFaker.Generate(count);
        }

        [Fact]
        public async Task GetRankShouldReturnOkWhenRanksExist()
        {
            // Arrange
            var ranks = GenerateFakeBestStockRank(25);

            _rankOfTheBestFundsServiceMock
                .Setup(s => s.GetRankOfTheBestStocksAsync(25))
                .ReturnsAsync(ranks);

            // Act
            var result = await _controller.GetRank(25);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRanks = Assert.IsAssignableFrom<IEnumerable<BestStockRank>>(okResult.Value);
            returnedRanks.Count().Should().Be(25);
        }

    }
}
