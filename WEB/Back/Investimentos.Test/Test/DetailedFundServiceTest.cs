
using System.Linq;
using Xunit;

namespace Investimentos.Test.Test
{
    public class DetailedFundServiceTest : DetailedFundService
    {
        [Fact]
        [ConfigureTest]
        public async void MustReturn360Funds()
        {
            var funds = await GetAllDetailedFundsAsync();
            Assert.Equal(360, funds.Count());
        }

        [Theory]
        [InlineData("hctr11")]
        [ConfigureTest]
        public async void MustReturnOneFunds(string fundCode)
        {
            var funds = await GetDetailedFundByCodeAsync(fundCode);
            Assert.NotNull(funds);
        }
    }
}