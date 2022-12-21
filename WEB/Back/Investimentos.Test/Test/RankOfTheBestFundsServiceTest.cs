using System.Collections.Generic;
using Investimentos.Domain.Models;
using Xunit;

namespace Investimentos.Test.Test
{
    public class RankOfTheBestFundsServiceTest : RankOfTheBestFundsService
    {
        static IEnumerable<RankOfTheBestFunds> rankOfTheBestFunds;

        [Fact]
        [ConfigureTest]
        public async void CalculationRankOfTheBestFunds ()
        {
            rankOfTheBestFunds = await GetCalculationRankOfTheBestFundsAsync();
        }

        [Fact]
        [ConfigureTest]
        public async void StoreAndCalculationAndGetRankOfTheBestFunds()
        {
            var result = await AddRankOfTheBestFundsAsync(rankOfTheBestFunds);
            rankOfTheBestFunds = await GetRankOfTheBestFundsAsync();
        }
    }
}