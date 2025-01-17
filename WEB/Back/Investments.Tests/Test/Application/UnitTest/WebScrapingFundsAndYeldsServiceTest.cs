using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public class WebScrapingFundsAndDividendsServiceTest
    {

        static Mock<IGeneralPersist> generalPersist = null;
        static Mock<FundsDividendsService> fundsYieldService = null;
        static Mock<IFundDividendPersist> mockFundsDividendsPersist = null;
        static Mock<IDetailedFundPersist> detailedFundPersist = null;
        static List<FundDividend> dummyFundsYieldService = null;
        static List<DetailedFund> memoryDetailedFunds = null;
        static List<FundDividend> memoryFundsDividends = null;
        static IEnumerable<DetailedFund> dummyDetailedFunds = null;
        ILogger<WebScrapingFundsAndDividendsService> logger = null;

        [Fact]
        public void Setup()
        {
            
            generalPersist = new Mock<IGeneralPersist>();
            mockFundsDividendsPersist = new Mock<IFundDividendPersist>();
            detailedFundPersist = new Mock<IDetailedFundPersist>();
            memoryDetailedFunds = new List<DetailedFund>();
            memoryFundsDividends = new List<FundDividend>();
            dummyDetailedFunds = (IEnumerable<DetailedFund>)DummyTest.DetailedFund().ElementAt(0).ElementAt(0);

            detailedFundPersist.Setup(x => x.AddDetailedFundsAsync(It.IsAny<IEnumerable<DetailedFund>>())).Returns(Task.FromResult(true));

            detailedFundPersist.Setup(x => x.GetAllDetailedFundsAsync()).Returns(Task.FromResult(dummyDetailedFunds));
            
            detailedFundPersist.Setup(x => x.GetDetailedFundByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var fund = dummyDetailedFunds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                return Task.FromResult(fund);
            });

            mockFundsDividendsPersist.Setup(x => x.AddFundsDividendsAsync(It.IsAny<IEnumerable<FundDividend>>())).Returns(Task.FromResult(true));
            
            mockFundsDividendsPersist.Setup(x => x.GetAllFundsDividendsAsync()).Returns(() => {
                        return Task.FromResult((IEnumerable<FundDividend>)dummyFundsYieldService);
            });

            mockFundsDividendsPersist.Setup(x => x.GetFundDividendsByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var result = dummyFundsYieldService.Where(x => x.FundCode == fundCode);
                return Task.FromResult(result);
            });

            generalPersist = new Mock<IGeneralPersist>();

            generalPersist.Setup(x => x.AddRange<DetailedFund>(It.IsAny<DetailedFund[]>())).Callback((DetailedFund[] detailedFunds) => {
                memoryDetailedFunds.AddRange(detailedFunds);
                Console.WriteLine($"Add::Add OK");
            });

            generalPersist.Setup(x => x.AddRange<FundDividend>(It.IsAny<FundDividend[]>())).Callback((FundDividend[] fundsDividends) => {
                memoryFundsDividends.AddRange(fundsDividends);
                Console.WriteLine($"Add::Add OK");
            });

            generalPersist.Setup(x => x.SaveChangesAsync()).Returns(() => {
                Console.WriteLine($"SaveChangesAsync::SaveChangesAsync OK");
                return Task.FromResult(true);
            });

            fundsYieldService = new Mock<FundsDividendsService>(mockFundsDividendsPersist.Object);

        }

        [Fact]
        // [ConfigureTest]
        public async Task MustGetTenFunds()
        {

            Setup();

            var detailedFunds = new List<DetailedFund>();
            var ct = new CancellationTokenSource();

            var webScraping = new WebScrapingFundsAndDividendsService(detailedFundPersist.Object, mockFundsDividendsPersist.Object, logger);
            detailedFunds = (List<DetailedFund>)await webScraping.GetFundsAsync(ct);

            detailedFunds.Should().HaveCount(10);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFund), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustGetFundsDividendAndShouldNotNull(List<DetailedFund> detailedFunds)
        {

            Setup();

            var dividends = new List<FundDividend>();
            var ct = new CancellationTokenSource();

            var webScraping = new WebScrapingFundsAndDividendsService(detailedFundPersist.Object, mockFundsDividendsPersist.Object, logger);
            dividends = (List<FundDividend>)await webScraping.GetFundDividendsAsync(detailedFunds, ct);

            dividends.Should().HaveCountGreaterThan(0);

        }

    }
}