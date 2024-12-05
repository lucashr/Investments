using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public class WebScrapingFundsAndYeldsServiceTest
    {

        static Mock<IGeneralPersist> generalPersist = null;
        static Mock<FundsYieldService> fundsYieldService = null;
        static Mock<IFundsYeldPersist> mockFundsYeldPersist = null;
        static Mock<IDetailedFundPersist> detailedFundPersist = null;
        static List<FundsYeld> dummyFundsYieldService = null;
        static List<DetailedFunds> memoryDetailedFunds = null;
        static List<FundsYeld> memoryFundsYeld = null;
        static IEnumerable<DetailedFunds> dummyDetailedFunds = null;
        
        [Fact]
        public void Setup()
        {
            
            generalPersist = new Mock<IGeneralPersist>();
            mockFundsYeldPersist = new Mock<IFundsYeldPersist>();
            detailedFundPersist = new Mock<IDetailedFundPersist>();
            memoryDetailedFunds = new List<DetailedFunds>();
            memoryFundsYeld = new List<FundsYeld>();
            dummyDetailedFunds = (IEnumerable<DetailedFunds>)DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);

            detailedFundPersist.Setup(x => x.AddDetailedFundsAsync(It.IsAny<IEnumerable<DetailedFunds>>())).Returns(Task.FromResult(true));

            detailedFundPersist.Setup(x => x.GetAllDetailedFundsAsync()).Returns(Task.FromResult(dummyDetailedFunds));
            
            detailedFundPersist.Setup(x => x.GetDetailedFundByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var fund = dummyDetailedFunds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                return Task.FromResult(fund);
            });

            mockFundsYeldPersist.Setup(x => x.AddFundsYieldsAsync(It.IsAny<IEnumerable<FundsYeld>>())).Returns(Task.FromResult(true));
            
            mockFundsYeldPersist.Setup(x => x.GetAllFundsYeldAsync()).Returns(() => {
                        return Task.FromResult((IEnumerable<FundsYeld>)dummyFundsYieldService);
            });

            mockFundsYeldPersist.Setup(x => x.GetFundYeldByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var result = dummyFundsYieldService.Where(x => x.FundCode == fundCode);
                return Task.FromResult(result);
            });

            generalPersist = new Mock<IGeneralPersist>();

            generalPersist.Setup(x => x.AddRange<DetailedFunds>(It.IsAny<DetailedFunds[]>())).Callback((DetailedFunds[] detailedFunds) => {
                memoryDetailedFunds.AddRange(detailedFunds);
                Console.WriteLine($"Add::Add OK");
            });

            generalPersist.Setup(x => x.AddRange<FundsYeld>(It.IsAny<FundsYeld[]>())).Callback((FundsYeld[] fundsYelds) => {
                memoryFundsYeld.AddRange(fundsYelds);
                Console.WriteLine($"Add::Add OK");
            });

            generalPersist.Setup(x => x.SaveChangesAsync()).Returns(() => {
                Console.WriteLine($"SaveChangesAsync::SaveChangesAsync OK");
                return Task.FromResult(true);
            });

            fundsYieldService = new Mock<FundsYieldService>(mockFundsYeldPersist.Object);

        }

        [Fact]
        // [ConfigureTest]
        public async Task MustGetTenFunds()
        {

            Setup();

            var detailedFunds = new List<DetailedFunds>();
            var ct = new CancellationTokenSource();

            using(WebScrapingFundsAndYeldsService webScraping = new WebScrapingFundsAndYeldsService(detailedFundPersist.Object, mockFundsYeldPersist.Object))
            {
                detailedFunds = (List<DetailedFunds>)await webScraping.GetFundsAsync(ct);
            }

            detailedFunds.Should().HaveCount(10);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustGetYeldsFundsAndReturnNotNull(List<DetailedFunds> detailedFunds)
        {

            Setup();

            var yelds = new List<FundsYeld>();
            var ct = new CancellationTokenSource();

            using(WebScrapingFundsAndYeldsService webScraping = new WebScrapingFundsAndYeldsService(detailedFundPersist.Object, mockFundsYeldPersist.Object))
            {
                yelds = (List<FundsYeld>)await webScraping.GetYeldsFundsAsync(detailedFunds, ct);
            }

            yelds.Should().HaveCountGreaterThan(0);

        }

    }
}