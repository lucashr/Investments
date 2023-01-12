// using System.Linq;
// using Xunit;

// namespace Investments.Test.Test
// {
//     public class FundsServiceTest : FundsService
//     {

//         [Theory]
//         [InlineData("HCTR11", "DDDDD")]
//         [ConfigureTest]
//         public async void MustUpdateFundCode(string oldFundCode, string newFundCode)
//         {
//             var fund = await UpdateFundByCodeAsync(oldFundCode, newFundCode);
//             Assert.Equal("DDDDD", fund.FundCode);
//         }

//         [Theory]
//         [InlineData("BBBBB")]
//         [ConfigureTest]
//         public async void MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
//         {
//             var fund = await AddFundAsync(fundCode);
//             Assert.Equal("BBBBB", fund.FundCode);
//         }

//         [Theory]
//         [InlineData("CVBI11")]
//         [ConfigureTest]
//         public async void MustReturnFund(string fundCode)
//         {
//             var fund = await GetFundByCodeAsync(fundCode);
//             Assert.Equal("CVBI11", fund.FundCode);
//         }

//         [Fact]
//         [ConfigureTest]
//         public async void MustReturnAllFunds()
//         {
//             var fund = await GetAllFundsAsync();
//             Assert.True(fund.Count() > 300);
//         }

//         [Theory]
//         [InlineData("ABCP11")]
//         [ConfigureTest]
//         public async void MustDeleteFundEReturnTrue(string fundCode)
//         {
//             var result = await DeleteFundByCodeAsync(fundCode);
//             Assert.Equal(true, result);
//         }

//     }
// }