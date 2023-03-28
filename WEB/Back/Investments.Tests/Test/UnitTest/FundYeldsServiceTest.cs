using System.Linq;
using Moq;
using Xunit;
using Investments.Application;
using System.Collections.Generic;
using Investments.Domain.Models;
using Investments.Tests.Helpers;
using System.Threading.Tasks;
using Investments.Persistence.Contracts;
using FluentAssertions;

namespace Investments.Tests.Test
{
    public class FundYeldsServiceTest
    {

        static FundsYieldService fundsYieldService = null;
        static Mock<IFundsYeldPersist> mockPersist = null;
        static List<FundsYeld> dummyFundsYield = null;

        public void Setup()
        {
            
            mockPersist = new Mock<IFundsYeldPersist>();
            dummyFundsYield = new List<FundsYeld>();
            dummyFundsYield = ((List<FundsYeld>)DummyTest.FundsYeld().ElementAt(0).ElementAt(0));

            mockPersist.Setup(x => x.AddFundsYieldsAsync(It.IsAny<IEnumerable<FundsYeld>>())).Returns(Task.FromResult(true));
            
            mockPersist.Setup(x => x.GetAllFundsYeldAsync()).Returns(() => {
                        return Task.FromResult((IEnumerable<FundsYeld>)dummyFundsYield);
            });

            mockPersist.Setup(x => x.GetFundYeldByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var result = dummyFundsYield.Where(x => x.FundCode == fundCode);
                return Task.FromResult(result);
            });
            
            fundsYieldService = new FundsYieldService(mockPersist.Object);

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustEnterThirtyFundsYeldsAndReturnTrue(List<FundsYeld> fundsYelds)
        {

            Setup();

            var resut = await fundsYieldService.AddFundsYieldsAsync(fundsYelds);

            resut.Should().Be(true, "Must be True");

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustReturnThirtyFundsYelds(List<FundsYeld> fundsYelds)
        {

            Setup();

            var result = await fundsYieldService.GetAllFundsYeldAsync();

            result.Should().HaveCount(30, "Must be 30");

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundsYeld), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustReturnFundYeldByCode(List<FundsYeld> fundsYelds)
        {

            Setup();

            var yelds = await fundsYieldService.GetFundYeldByCodeAsync("AAZQ11");
            
            yelds.ElementAt(0).FundCode.Should().Be("AAZQ11", "Must be AAZQ11");
            yelds.Should().HaveCountGreaterThan(0, "Must be greater than 0");

        }

    }
}