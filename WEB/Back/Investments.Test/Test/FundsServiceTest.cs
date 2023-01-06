using System.Linq;
using Xunit;

namespace Investments.Test.Test
{
    public class FundsServiceTest : FundsService
    {

        [Theory]
        [InlineData("hctr11", "dddd")]
        [ConfigureTest]
        public async void MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            var fund = await UpdateFundByCodeAsync(oldFundCode, newFundCode);
            Assert.Equal("dddd", fund.FundCode);
        }

        [Theory]
        [InlineData("bbbbbb")]
        [ConfigureTest]
        public async void MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {
            var fund = await AddFundAsync(fundCode);
            Assert.Equal("bbbbbb", fund.FundCode);
        }

        [Theory]
        [InlineData("cvbi11")]
        [ConfigureTest]
        public async void MustReturnFund(string fundCode)
        {
            var fund = await GetFundByCodeAsync(fundCode);
            Assert.Equal("cvbi11", fund.FundCode);
        }

        [Fact]
        [ConfigureTest]
        public async void MustReturnAllFunds()
        {
            var fund = await GetAllFundsAsync();
            Assert.True(fund.Count() > 300);
        }

        [Theory]
        [InlineData("abcp11")]
        [ConfigureTest]
        public async void MustDeleteFundEReturnTrue(string fundCode)
        {
            var result = await DeleteFundByCodeAsync(fundCode);
            Assert.Equal(true, result);
        }

    }
}