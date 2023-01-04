using System.Linq;
using Xunit;
using XUnit.Project.Attributes;

namespace Investments.Test.Test
{
    [TestCaseOrderer("Investments.Test.Orderers.PriorityOrderer", "Investments.Test")]
    public class FundsServiceTest : FundsService
    {

        [Theory, TestPriority(7)]
        [InlineData("hctr11", "dddd")]
        [ConfigureTest]
        public async void MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            var fund = await UpdateFundByCodeAsync(oldFundCode, newFundCode);
            Assert.Equal("dddd", fund.FundCode);
        }

        [Theory, TestPriority(8)]
        [InlineData("zzzzz")]
        [ConfigureTest]
        public async void MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {
            var fund = await AddFundAsync(fundCode);
            Assert.Equal("zzzzz", fund.FundCode);
        }

        [Theory, TestPriority(9)]
        [InlineData("cvbi11")]
        [ConfigureTest]
        public async void MustReturnFund(string fundCode)
        {
            var fund = await GetFundByCodeAsync(fundCode);
            Assert.Equal("cvbi11", fund.FundCode);
        }

        [Fact, TestPriority(10)]
        [ConfigureTest]
        public async void MustReturnAllFunds()
        {
            var fund = await GetAllFundsAsync();
            Assert.True(fund.Count() > 300);
        }

        [Theory, TestPriority(11)]
        [InlineData("abcp11")]
        [ConfigureTest]
        public async void MustDeleteFundEReturnTrue(string fundCode)
        {
            var result = await DeleteFundByCodeAsync(fundCode);
            Assert.Equal(true, result);
        }

    }
}