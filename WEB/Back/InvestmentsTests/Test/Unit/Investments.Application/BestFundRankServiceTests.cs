using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using Xunit;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Tests.Application
{
    public class BestFundRankServiceTests
    {
        private readonly Mock<IRankOfTheBestFundsPersist> _mockRankOfTheBestFundsPersist;
        private readonly Mock<IDetailedFundService> _mockDetailedFundService;
        private readonly Mock<IFundDividendsService> _mockFundsDividendsService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BestFundRankService _service;

        public BestFundRankServiceTests()
        {
            _mockRankOfTheBestFundsPersist = new Mock<IRankOfTheBestFundsPersist>();
            _mockDetailedFundService = new Mock<IDetailedFundService>();
            _mockFundsDividendsService = new Mock<IFundDividendsService>();
            _mockMapper = new Mock<IMapper>();

            _service = new BestFundRankService(
                _mockRankOfTheBestFundsPersist.Object,
                _mockDetailedFundService.Object,
                _mockFundsDividendsService.Object,
                _mockMapper.Object
            );
        }

        private IEnumerable<DetailedFund> GenerateRandomFunds(int count)
        {

            var faker = new Faker<DetailedFund>()
                .RuleFor(f => f.FundCode, f => f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "11")
                .RuleFor(f => f.Liquidity, f => f.Random.Double(500000, 5000000))
                .RuleFor(f => f.DividendYield, f => f.Random.Double(3, 5))
                .RuleFor(f => f.PriceEquityValue, f => f.Random.Double(5, 10))
                .RuleFor(f => f.AverageVacancy, f => f.Random.Double(1, 5));

            return faker.Generate(count);

        }

        private IEnumerable<FundDividend> GenerateRandomDividends(string fundCode, int count)
        {
            var faker = new Faker<FundDividend>()
                .RuleFor(d => d.FundCode, f => fundCode)
                .RuleFor(d => d.Value, f => f.Random.Double(5, 10))
                .RuleFor(d => d.LastComputedDate, f => f.Date.Past(1).ToString());

            return faker.Generate(count);
        }

        [Fact]
        public async Task GetCalculationRankOfTheBestFundsAsyncShouldReturnFilteredAndRankedFunds()
        {
            var funds = GenerateRandomFunds(25).ToList();
            _mockDetailedFundService.Setup(s => s.GetAllDetailedFundsAsync()).ReturnsAsync(funds);

            var bestFundRanks = funds.Select(f => new BestFundRank
            {
                FundCode = f.FundCode,
                Liquidity = f.Liquidity,
                DividendYield = f.DividendYield,
                PriceEquityValue = f.PriceEquityValue
            }).ToList();

            _mockMapper.Setup(m => m.Map<IEnumerable<BestFundRank>>(funds)).Returns(bestFundRanks);

            foreach (var fund in bestFundRanks)
            {
                var dividends = GenerateRandomDividends(fund.FundCode, 12);
                _mockFundsDividendsService
                    .Setup(s => s.GetFundDividendsByCodeAsync(fund.FundCode))
                    .ReturnsAsync(dividends);
            }

            var result = await _service.GetCalculationRankOfTheBestFundsAsync();

            result.Should().NotBeNullOrEmpty();
            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task AddRankOfTheBestFundsAsyncShouldCallPersistLayer()
        {
            var randomFunds = GenerateRandomFunds(5)
                .Select(f => new BestFundRank
                {
                    FundCode = f.FundCode,
                    Liquidity = f.Liquidity,
                    DividendYield = f.DividendYield,
                    PriceEquityValue = f.PriceEquityValue
                }).ToList();

            _mockRankOfTheBestFundsPersist
                .Setup(p => p.AddRankOfTheBestFundsAsync(It.IsAny<IEnumerable<BestFundRank>>()))
                .ReturnsAsync(true);

            var result = await _service.AddRankOfTheBestFundsAsync(randomFunds);

            result.Should().BeTrue();
        }
    }
}
