using System.Linq;
using Xunit;

namespace Investimentos.Test.Test
{
    public class FundsServiceTest : FundsService
    {

        [Theory]
        [InlineData("XXXXX", "hctr11")]
        [ConfigureTest]
        public async void MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            var fund = await UpdateFundByCodeAsync(oldFundCode, newFundCode);
            Assert.Equal("hctr11", fund.FundCode);
        }

        [Theory]
        [InlineData("RRTT11")]
        [ConfigureTest]
        public async void MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {
            var fund = await AddFundAsync(fundCode);
            Assert.Equal("RRTT11", fund.FundCode);
        }

        [Theory]
        [InlineData("hctr11")]
        [ConfigureTest]
        public async void MustReturnFund(string fundCode)
        {
            var fund = await GetFundByCodeAsync(fundCode);
            Assert.Equal("hctr11", fund.FundCode);
        }

        [Fact]
        [ConfigureTest]
        public async void MustReturnAllFunds()
        {
            var fund = await GetAllFundsAsync();
            // Assert.Equal("hctr11", fund.Papel);
        }

        [Theory]
        [InlineData("RRTT11")]
        [ConfigureTest]
        public async void MustDeleteFundEReturnTrue(string fundCode)
        {
            var result = await DeleteFundByCodeAsync(fundCode);
            Assert.Equal(true, result);
        }

    }
}