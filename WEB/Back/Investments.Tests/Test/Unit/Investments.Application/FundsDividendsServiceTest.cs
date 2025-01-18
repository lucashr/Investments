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
using Investments.Persistence;

namespace Investments.Tests.Test
{
    public class FundDividendsServiceTest
    {

        static FundsDividendsService fundsYieldService = null;
        static Mock<FundDividendPersist> mockPersist = null;
        static List<FundDividend> dummyFundsYield = null;

        public void Setup()
        {
            
            mockPersist = new Mock<FundDividendPersist>();
            dummyFundsYield = new List<FundDividend>();
            dummyFundsYield = ((List<FundDividend>)DummyTest.FundDividend().ElementAt(0).ElementAt(0));

            mockPersist.Setup(x => x.AddFundsDividendsAsync(It.IsAny<IEnumerable<FundDividend>>())).Returns(Task.FromResult(true));
            
            mockPersist.Setup(x => x.GetAllFundsDividendsAsync()).Returns(() => {
                        return Task.FromResult((IEnumerable<FundDividend>)dummyFundsYield);
            });

            mockPersist.Setup(x => x.GetFundDividendsByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                var result = dummyFundsYield.Where(x => x.FundCode == fundCode);
                return Task.FromResult(result);
            });
            
            fundsYieldService = new FundsDividendsService(mockPersist.Object);

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundDividend), MemberType = typeof(DummyTest))]
        public async Task MustEnterThirtyFundsDividendsAndReturnTrue(List<FundDividend> fundsDividend)
        {

            Setup();

            var resut = await fundsYieldService.AddFundDividendsAsync(fundsDividend);

            resut.Should().Be(true, "Must be True");

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundDividend), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustReturnThirtyFundsDividends(List<FundDividend> fundsDividends)
        {

            Setup();

            var result = await fundsYieldService.GetAllFundsDividendsAsync();

            result.Should().HaveCount(30, "Must be 30");

        }

        [Theory]
        [MemberData(nameof(DummyTest.FundDividend), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustReturnFundDividendsByCode(List<FundDividend> fundsDividends)
        {

            Setup();

            var dividends = await fundsYieldService.GetFundDividendsByCodeAsync("AAZQ11");
            
            dividends.ElementAt(0).FundCode.Should().Be("AAZQ11", "Must be AAZQ11");
            dividends.Should().HaveCountGreaterThan(0, "Must be greater than 0");

        }

    }

}