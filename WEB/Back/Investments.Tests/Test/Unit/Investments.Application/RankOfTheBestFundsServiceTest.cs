using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Moq;
using Xunit;

namespace Investments.Tests
{
    public class RankOfTheBestFundsServiceTest
    {

        static RankOfTheBestFundsService rankOfTheBestFundsService = null;
        static Mock<IRankOfTheBestFundsPersist> mockRankOfTheBestFundsPersist = null;
        static List<BestFundRank> dummyRankOfTheBestFunds = null;
        static Mock<DetailedFundService> detailedFundService = null;
        static Mock<IDetailedFundPersist> mockDetailedFundPersist = null;
        static List<DetailedFund> dummyDetailedFunds = null;
        static Mock<FundsDividendsService> fundsYieldService = null;
        static Mock<IFundDividendPersist> mockFundsDividendsPersist = null;
        static List<FundDividend> dummyFundsYieldService = null;

        public void Setup()
        {   
            
            mockDetailedFundPersist = new Mock<IDetailedFundPersist>();
            dummyDetailedFunds = new List<DetailedFund>();
            dummyDetailedFunds = ((List<DetailedFund>)DummyTest.DetailedFund().ElementAt(0).ElementAt(0));

            mockRankOfTheBestFundsPersist = new Mock<IRankOfTheBestFundsPersist>();
            dummyRankOfTheBestFunds = new List<BestFundRank>();
            dummyRankOfTheBestFunds = ((List<BestFundRank>)DummyTest.BestFundRank().ElementAt(0).ElementAt(0));
            
            mockFundsDividendsPersist = new Mock<IFundDividendPersist>();
            dummyFundsYieldService = new List<FundDividend>();
            dummyFundsYieldService = ((List<FundDividend>)DummyTest.FundDividend().ElementAt(0).ElementAt(0));

            mockDetailedFundPersist.Setup(x=> x.AddDetailedFundsAsync(It.IsAny<IEnumerable<DetailedFund>>())).Returns(Task.FromResult(true));
            mockDetailedFundPersist.Setup(x=> x.GetAllDetailedFundsAsync()).Returns(Task.FromResult((IEnumerable<DetailedFund>)dummyDetailedFunds));
            mockDetailedFundPersist.Setup(
                        x=> x.GetDetailedFundByCodeAsync(It.IsAny<string>()))
                             .Returns((string fundCode) => {
                                var funds = (IEnumerable<DetailedFund>)dummyDetailedFunds;
                                var result = funds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                                return Task.FromResult(result);
            });

            mockFundsDividendsPersist.Setup(x => x.AddFundsDividendsAsync(It.IsAny<IEnumerable<FundDividend>>())).Returns(Task.FromResult(true));
            
            mockFundsDividendsPersist.Setup(x => x.GetAllFundsDividendsAsync()).Returns(() => {
                        return Task.FromResult((IEnumerable<FundDividend>)dummyFundsYieldService);
            });

            mockFundsDividendsPersist.Setup(x => x.GetFundDividendsByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var result = dummyFundsYieldService.Where(x => x.FundCode == fundCode);
                return Task.FromResult(result);
            });

            mockRankOfTheBestFundsPersist.Setup(x => x.AddRankOfTheBestFundsAsync(It.IsAny<IEnumerable<BestFundRank>>())).Returns(Task.FromResult(true));
            
            mockRankOfTheBestFundsPersist.Setup(x => x.GetRankOfTheBestFundsAsync(null)).Returns((int totalRank) => {
                return Task.FromResult(dummyRankOfTheBestFunds.Take(dummyRankOfTheBestFunds.Count()));
            });

            mockRankOfTheBestFundsPersist.Setup(x => x.GetRankOfTheBestFundsAsync(It.IsAny<int>())).Returns((int totalRank) => {
                return Task.FromResult(dummyRankOfTheBestFunds.Take(totalRank));
            });

            detailedFundService = new Mock<DetailedFundService>(mockDetailedFundPersist.Object);
            
            fundsYieldService = new Mock<FundsDividendsService>(mockFundsDividendsPersist.Object);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BestFundRank, DetailedFund>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();

            rankOfTheBestFundsService = new RankOfTheBestFundsService(mockRankOfTheBestFundsPersist.Object, detailedFundService.Object, 
                                                                      fundsYieldService.Object, mapper);

        }


        [Theory]
        [MemberData(nameof(DummyTest.BestFundRank), MemberType = typeof(DummyTest))]
        public async Task MustStoreAndReturnTrue(List<BestFundRank> rankOfTheBestFunds)
        {
            
            Setup();

            var result = await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
            
            result.Should().Be(true);

        }

        [Theory]
        [MemberData(nameof(DummyTest.BestFundRank), MemberType = typeof(DummyTest))]
        public async Task MustCalculateAndReturnNotNull(List<BestFundRank> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            var result = await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
            
            result.Should().NotBeNull();

        }

        [Theory]
        [MemberData(nameof(DummyTest.BestFundRank), MemberType = typeof(DummyTest))]
        public async Task MustReturnAllRankOfTheBestFunds(List<BestFundRank> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();

            var result = await rankOfTheBestFundsService.GetRankOfTheBestFundsAsync();
            
            result.Should().NotBeNull();

        }

        [Theory]
        [MemberData(nameof(DummyTest.BestFundRank), MemberType = typeof(DummyTest))]
        public async Task MustReturnNumberOfElementsRankOfTheBestFunds(List<BestFundRank> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();

            var result = await rankOfTheBestFundsService.GetRankOfTheBestFundsAsync(5);
            
            result.Should().NotBeNull();

        }
        
    }
}