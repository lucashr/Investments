using System.Linq;
using Xunit;

namespace Investments.Test.Test
{
    public class FundYeldsServiceTest : FundYeldsService
    {

        [Theory]
        [InlineData("MXRF11")]
        [ConfigureTest]
        public async void MustReturnFundYeldByCode(string fundCode)
        {
            var yelds = await GetFundYeldByCodeAsync(fundCode);
            Assert.NotNull(yelds);
        }

        [Fact]
        [ConfigureTest]
        public async void MustReturnAllFundsYeld()
        {
            var funds = await GetAllFundsYeldAsync();
            Assert.True(funds.Count() > 1000);
        }
    }
}