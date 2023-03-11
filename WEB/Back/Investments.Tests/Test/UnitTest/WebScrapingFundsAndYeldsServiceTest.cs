using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
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
        static InvestmentsContext ctx = null;

        public static async Task<bool> CreateContext()
        {

            var ContextOptions = new DbContextOptionsBuilder<InvestmentsContext>()
                                .UseSqlite($"Data Source=Test_WebScrapingFundsAndYeldsServiceTest.db")
                                .EnableSensitiveDataLogging().Options;
            
            ctx = new InvestmentsContext(ContextOptions);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            return await Task.FromResult(true);

        }

        public static void Setup()
        {
            
            // var ctx = await ConfigureTest.ConfigureDatabase();
            detailedFundPersist = new DetailedFundPersist(ctx);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            Mock<IFundsYeldPersist> fundsYeldPersist = new Mock<IFundsYeldPersist>();
            iGeneralPersist = new Mock<IGeneralPersist>();
            iWebScrapingFundsAndYeldsPersist = new Mock<IWebScrapingFundsAndYeldsPersist>();
            fundsPersist = new Mock<FundsPersist>(ctx);
            fundYeldsPersist = new Mock<FundYeldsPersist>(ctx);
            fundsYieldService = new FundsYieldService(fundYeldsPersist.Object);

        }

        [Fact]
        // [ConfigureTest]
        public async Task MustGetTenFunds()
        {

            await CreateContext();
            Setup();

            var detailedFunds = new List<DetailedFunds>();

            using(WebScrapingFundsAndYeldsService webScraping = new WebScrapingFundsAndYeldsService(iGeneralPersist.Object , fundsPersist.Object,
                                                                                                    fundYeldsPersist.Object, iWebScrapingFundsAndYeldsPersist.Object))
            {
                detailedFunds = (List<DetailedFunds>)await webScraping.GetFundsAsync();
            }

            Assert.Equal(10, detailedFunds.Count());

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustGetYeldsFundsAndReturnNotNull(List<DetailedFunds> detailedFunds)
        {

            await CreateContext();
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