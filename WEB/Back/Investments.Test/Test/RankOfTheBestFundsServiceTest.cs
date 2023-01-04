using System.Collections.Generic;
using Investments.Domain.Models;
using Xunit;
using XUnit.Project.Attributes;

namespace Investments.Test.Test
{
    [TestCaseOrderer("Investments.Test.Orderers.PriorityOrderer", "Investments.Test")]
    public class RankOfTheBestFundsServiceTest : RankOfTheBestFundsService
    {

        [Fact, TestPriority(3)]
        [ConfigureTest]
        public async void CalculationRankOfTheBestFunds ()
        {
            var rankOfTheBestFunds = await GetCalculationRankOfTheBestFundsAsync();
            Assert.NotNull(rankOfTheBestFunds);
        }

        [Fact, TestPriority(4)]
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