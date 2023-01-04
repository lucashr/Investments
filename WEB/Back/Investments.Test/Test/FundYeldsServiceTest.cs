using System.Linq;
using Xunit;
using XUnit.Project.Attributes;

namespace Investments.Test.Test
{
    [TestCaseOrderer("Investments.Test.Orderers.PriorityOrderer", "Investments.Test")]
    public class FundYeldsServiceTest : FundYeldsService
    {

        [Theory, TestPriority(5)]
        [InlineData("MXRF11")]
        [ConfigureTest]
        public async void MustReturnFundYeldByCode(string fundCode)
        {
            var yelds = await GetFundYeldByCodeAsync(fundCode);
            Assert.NotNull(yelds);
        }

        [Fact, TestPriority(6)]
        [ConfigureTest]
        public async void MustReturnAllFundsYeld()
        {
            var funds = await GetAllFundsYeldAsync();
            Assert.True(funds.Count() > 1000);
        }
    }
}