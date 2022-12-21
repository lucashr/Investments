using Xunit;

namespace Investimentos.Test.Test
{
    public class FundYeldsServiceTest : FundYeldsService
    {

        [Theory]
        [InlineData("hctr11")]
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
            // Assert.Equal(360, funds.Count());
        }
    }
}