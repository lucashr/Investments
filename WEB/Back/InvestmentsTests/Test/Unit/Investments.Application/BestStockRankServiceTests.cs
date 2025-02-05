using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;

namespace InvestmentsTests.Test
{
    public class BestStockRankServiceTests
    {
        private readonly Mock<IRankOfTheBestStocksPersist> _mockRankPersist;
        private readonly Mock<IDetailedStockService> _mockDetailedStockService;
        private readonly Mock<IStocksDividendService> _mockStocksDividendService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BestStockRankService _service;

        public BestStockRankServiceTests()
        {
            _mockRankPersist = new Mock<IRankOfTheBestStocksPersist>();
            _mockDetailedStockService = new Mock<IDetailedStockService>();
            _mockStocksDividendService = new Mock<IStocksDividendService>();
            _mockMapper = new Mock<IMapper>();

            _service = new BestStockRankService(
                _mockRankPersist.Object,
                _mockDetailedStockService.Object,
                _mockStocksDividendService.Object,
                _mockMapper.Object
            );
            
        }

        private IEnumerable<BestStockRank> CreateBestStockRank(int totalItems)
        {
            var faker = new Faker<BestStockRank>()
                .RuleFor(f => f.FundCode, f => f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "11");

            return faker.Generate(totalItems);
        } 

        private IEnumerable<DetailedStock> GetDetailedStocks(int totalItems)
        {

            var faker = new Faker<DetailedStock>()
                .RuleFor(s => s.FundCode, f => f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + "11")
                .RuleFor(s => s.DivYield, f => f.Random.Double(3, 15))
                .RuleFor(s => s.LiquidityTwoMonths, f => f.Random.Double(1000000, 10000000))
                .RuleFor(s => s.PL, f => f.Random.Double(1, 20))
                .RuleFor(s => s.ROE, f => f.Random.Double(1, 30))
                .RuleFor(s => s.ROIC, f => f.Random.Double(1, 25))
                .RuleFor(s => s.RevenueGrowthFiveYears, f => f.Random.Double(0, 20))
                .RuleFor(s => s.EbitMargin, f => f.Random.Double(1, 40))
                .RuleFor(s => s.NetWorth, f => f.Random.Double(1, 100))
                .RuleFor(s => s.GrossEquityDebt, f => f.Random.Double(1, 50))
                .RuleFor(s => s.EVEBITDA, f => f.Random.Double(1, 20))
                .RuleFor(s => s.LiquidityMargin, f => f.Random.Double(1, 5))
                .RuleFor(s => s.PVP, f => f.Random.Double(1, 4))
                .RuleFor(s => s.Quotation, f => f.Random.Double(1, 100));

            return faker.Generate(totalItems);
        }

        [Fact]
        public async Task AddRankOfTheBestStocksAsyncShouldReturnTrue()
        {
            var detailedStocks = GetDetailedStocks(5);
            var rankedStocks = detailedStocks.OrderByDescending(s => s.DivYield);

            _mockRankPersist.Setup(s => s.AddRankOfTheBestStocksAsync(It.IsAny<IEnumerable<BestStockRank>>())).ReturnsAsync(true);
            _mockRankPersist.Setup(s => s.GetRankOfTheBestStocksAsync(15));
            _mockMapper.Setup(m => m.Map<IEnumerable<BestStockRank>>(It.IsAny<IEnumerable<DetailedStock>>()))
                    .Returns((IEnumerable<DetailedStock> stocks) => stocks.Select(s => new BestStockRank { FundCode = s.FundCode }));

            var result = await _service.AddRankOfTheBestStocksAsync(CreateBestStockRank(10));

            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetRankOfTheBestStocksAsyncShouldReturnRankedStocks()
        {
            var detailedStocks = GetDetailedStocks(5);
            var rankedStocks = detailedStocks.OrderByDescending(s => s.DivYield); // Simplificado para o teste

            _mockDetailedStockService.Setup(s => s.GetAllDetailedStocksAsync()).ReturnsAsync(detailedStocks);
            _mockMapper.Setup(m => m.Map<IEnumerable<BestStockRank>>(It.IsAny<IEnumerable<DetailedStock>>()))
                    .Returns((IEnumerable<DetailedStock> stocks) => stocks.Select(s => new BestStockRank { FundCode = s.FundCode }));

            var result = await _service.GetRankOfTheBestStocksAsync();

            result.Count().Should().BeGreaterThan(0);
        }

    }

}