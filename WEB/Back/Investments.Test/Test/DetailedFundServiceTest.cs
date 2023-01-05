
using System.Linq;
using Xunit;
using XUnit.Project.Attributes;

namespace Investments.Test.Test
{   
    [TestCaseOrderer("Investments.Test.Orderers.PriorityOrderer", "Investments.Test.Test")]
    public class DetailedFundServiceTest : DetailedFundService
    {

        [Fact, TestPriority(12)]
        [ConfigureTest]
        public async void MustReturnMore300Funds()
        {
            
            var webScraping = new WebScrapingFundsAndYeldsService();

            webScraping.ConfigDriver();
            
            var detailedFunds = await webScraping.GetFundsAsync();

            await AddDetailedFundsAsync(detailedFunds);

            var funds = await GetAllDetailedFundsAsync();

            Assert.True(funds.Count() > 300);
        }

        [Theory, TestPriority(13)]
        [InlineData("MXRF11")]
        [ConfigureTest]
        public async void MustReturnOneFunds(string fundCode)
        {

            var webScraping = new WebScrapingFundsAndYeldsService();

            webScraping.ConfigDriver();

            var detailedFunds = await webScraping.GetFundsAsync();

            await AddDetailedFundsAsync(detailedFunds);

            var funds = await GetDetailedFundByCodeAsync(fundCode);

            Assert.NotNull(funds);
        }
    }
}