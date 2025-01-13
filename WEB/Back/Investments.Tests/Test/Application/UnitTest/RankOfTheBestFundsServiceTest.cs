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

namespace Investments.Tests.Test.Application
{
    public class RankOfTheBestFundsServiceTest
    {

        static RankOfTheBestFundsService rankOfTheBestFundsService = null;
        static Mock<IRankOfTheBestFundsPersist> mockRankOfTheBestFundsPersist = null;
        static List<RankOfTheBestFunds> dummyRankOfTheBestFunds = null;
        static Mock<DetailedFundService> detailedFundService = null;
        static Mock<IDetailedFundPersist> mockDetailedFundPersist = null;
        static List<DetailedFunds> dummyDetailedFunds = null;
        static Mock<FundsDividendsService> fundsYieldService = null;
        static Mock<IFundsYeldPersist> mockFundsYeldPersist = null;
        static List<FundDividends> dummyFundsYieldService = null;

        public void Setup()
        {   
            
            mockDetailedFundPersist = new Mock<IDetailedFundPersist>();
            dummyDetailedFunds = new List<DetailedFunds>();
            dummyDetailedFunds = ((List<DetailedFunds>)DummyTest.DetailedFunds().ElementAt(0).ElementAt(0));

            mockRankOfTheBestFundsPersist = new Mock<IRankOfTheBestFundsPersist>();
            dummyRankOfTheBestFunds = new List<RankOfTheBestFunds>();
            dummyRankOfTheBestFunds = ((List<RankOfTheBestFunds>)DummyTest.RankOfTheBestFunds().ElementAt(0).ElementAt(0));
            
            mockFundsYeldPersist = new Mock<IFundsYeldPersist>();
            dummyFundsYieldService = new List<FundDividends>();
            dummyFundsYieldService = ((List<FundDividends>)DummyTest.FundDividends().ElementAt(0).ElementAt(0));

            mockDetailedFundPersist.Setup(x=> x.AddDetailedFundsAsync(It.IsAny<IEnumerable<DetailedFunds>>())).Returns(Task.FromResult(true));
            mockDetailedFundPersist.Setup(x=> x.GetAllDetailedFundsAsync()).Returns(Task.FromResult((IEnumerable<DetailedFunds>)dummyDetailedFunds));
            mockDetailedFundPersist.Setup(
                        x=> x.GetDetailedFundByCodeAsync(It.IsAny<string>()))
                             .Returns((string fundCode) => {
                                var funds = (IEnumerable<DetailedFunds>)dummyDetailedFunds;
                                var result = funds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                                return Task.FromResult(result);
            });

            mockFundsYeldPersist.Setup(x => x.AddFundsYieldsAsync(It.IsAny<IEnumerable<FundDividends>>())).Returns(Task.FromResult(true));
            
            mockFundsYeldPersist.Setup(x => x.GetAllFundsYeldAsync()).Returns(() => {
                        return Task.FromResult((IEnumerable<FundDividends>)dummyFundsYieldService);
            });

            mockFundsYeldPersist.Setup(x => x.GetFundYeldByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var result = dummyFundsYieldService.Where(x => x.FundCode == fundCode);
                return Task.FromResult(result);
            });

            mockRankOfTheBestFundsPersist.Setup(x => x.AddRankOfTheBestFundsAsync(It.IsAny<IEnumerable<RankOfTheBestFunds>>())).Returns(Task.FromResult(true));
            
            mockRankOfTheBestFundsPersist.Setup(x => x.GetRankOfTheBestFundsAsync(null)).Returns((int totalRank) => {
                return Task.FromResult(dummyRankOfTheBestFunds.Take(dummyRankOfTheBestFunds.Count()));
            });

            mockRankOfTheBestFundsPersist.Setup(x => x.GetRankOfTheBestFundsAsync(It.IsAny<int>())).Returns((int totalRank) => {
                return Task.FromResult(dummyRankOfTheBestFunds.Take(totalRank));
            });

            detailedFundService = new Mock<DetailedFundService>(mockDetailedFundPersist.Object);
            
            fundsYieldService = new Mock<FundsDividendsService>(mockFundsYeldPersist.Object);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RankOfTheBestFunds, DetailedFunds>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();

            rankOfTheBestFundsService = new RankOfTheBestFundsService(mockRankOfTheBestFundsPersist.Object, detailedFundService.Object, 
                                                                      fundsYieldService.Object, mapper);

        }


        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustStoreAndReturnTrue(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            var result = await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
            
            result.Should().Be(true);

        }

        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustCalculateAndReturnNotNull(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            var result = await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
            
            result.Should().NotBeNull();

        }

        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustReturnAllRankOfTheBestFunds(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();

            var result = await rankOfTheBestFundsService.GetRankOfTheBestFundsAsync();
            
            result.Should().NotBeNull();

        }

        [Theory]
        [MemberData(nameof(DummyTest.RankOfTheBestFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustReturnNumberOfElementsRankOfTheBestFunds(List<RankOfTheBestFunds> rankOfTheBestFunds)
        {
            
            Setup();

            await rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankOfTheBestFunds);

            await rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();

            var result = await rankOfTheBestFundsService.GetRankOfTheBestFundsAsync(5);
            
            result.Should().NotBeNull();

        }
        
    }
}