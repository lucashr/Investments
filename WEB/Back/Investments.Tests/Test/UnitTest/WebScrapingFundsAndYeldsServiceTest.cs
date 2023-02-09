using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public class WebScrapingFundsAndYeldsServiceTest
    {

        static RankOfTheBestFundsService rankOfTheBestFundsService = null;
        static DetailedFundPersist detailedFundPersist = null;
        static DetailedFundService detailedFundService = null;
        static FundsYieldService fundsYieldService = null;
        static Mock<RankOfTheBestFundsPersist> rankOfTheBestFundsPersist = null;
        static Mock<FundYeldsPersist> fundYeldsPersist = null;
        static Mock<IGeneralPersist> iGeneralPersist = null;
        static Mock<FundsPersist> fundsPersist = null;
        static Mock<IWebScrapingFundsAndYeldsPersist> iWebScrapingFundsAndYeldsPersist = null;

        public void Setup()
        {
            detailedFundPersist = new DetailedFundPersist(ConfigureTest._context);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            Mock<IFundsYeldPersist> fundsYeldPersist = new Mock<IFundsYeldPersist>();
            iGeneralPersist = new Mock<IGeneralPersist>();
            iWebScrapingFundsAndYeldsPersist = new Mock<IWebScrapingFundsAndYeldsPersist>();
            fundsPersist = new Mock<FundsPersist>(ConfigureTest._context);
            fundYeldsPersist = new Mock<FundYeldsPersist>(ConfigureTest._context);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);
        }

        [Fact]
        [ConfigureTest]
        public async Task MustGetTenFunds()
        {

            Setup();

            var detailedFunds = new List<DetailedFunds>();

            using(WebScrapingFundsAndYeldsService webScraping = new WebScrapingFundsAndYeldsService(iGeneralPersist.Object , fundsPersist.Object,
                                                                                                    fundYeldsPersist.Object, iWebScrapingFundsAndYeldsPersist.Object))
            {
                detailedFunds = (List<DetailedFunds>)await webScraping.GetFundsAsync();
            }

            Assert.NotNull(detailedFunds.Count() == 10);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        [ConfigureTest]
        public async Task MustGetYeldsFundsAndReturnNotNull(List<DetailedFunds> detailedFunds)
        {

            Setup();

            var yelds = new List<FundsYeld>();

            using(WebScrapingFundsAndYeldsService webScraping = new WebScrapingFundsAndYeldsService(iGeneralPersist.Object , fundsPersist.Object,
                                                                                                    fundYeldsPersist.Object, iWebScrapingFundsAndYeldsPersist.Object))
            {
                yelds = (List<FundsYeld>)await webScraping.GetYeldsFundsAsync(detailedFunds);
            }

            Assert.NotNull(yelds);

        }

    }
}