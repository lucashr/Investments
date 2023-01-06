using System.Collections.Generic;
using Investments.Domain.Models;
using Xunit;

namespace Investments.Test.Test
{
    public class RankOfTheBestFundsServiceTest : RankOfTheBestFundsService
    {

        [Fact]
        [ConfigureTest]
        public async void CalculationRankOfTheBestFunds ()
        {
            var rankOfTheBestFunds = await GetCalculationRankOfTheBestFundsAsync();
            Assert.NotNull(rankOfTheBestFunds);
        }

        [Fact]
        [ConfigureTest]
        public async void StoreAndCalculationAndGetRankOfTheBestFunds()
        {

            var rankOfTheBestFunds = await GetCalculationRankOfTheBestFundsAsync();
            var result = await AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
            
            rankOfTheBestFunds = await GetRankOfTheBestFundsAsync();

            Assert.NotNull(rankOfTheBestFunds);

        }
    }
}