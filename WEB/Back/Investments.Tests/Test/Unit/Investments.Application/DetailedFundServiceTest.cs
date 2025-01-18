
using System.Collections.Generic;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;
using System.Linq;
using Investments.Tests.Helpers;
using System.Threading.Tasks;
using FluentAssertions;

namespace Investments.Tests
{

    public class DetailedFundServiceTest 
    {

        static DetailedFundService service = null;
        static Mock<IDetailedFundPersist> mock = null;
        static List<DetailedFund> dummyDetailedFunds = null;

        public static void Setup()
        {
            
            
            mock = new Mock<IDetailedFundPersist>();
            dummyDetailedFunds = new List<DetailedFund>();
            dummyDetailedFunds = ((List<DetailedFund>)DummyTest.DetailedFund().ElementAt(0).ElementAt(0));

            mock.Setup(x=> x.AddDetailedFundsAsync(It.IsAny<IEnumerable<DetailedFund>>())).Returns(Task.FromResult(true));
            mock.Setup(x=> x.GetAllDetailedFundsAsync()).Returns(Task.FromResult((IEnumerable<DetailedFund>)dummyDetailedFunds));
            mock.Setup(
                        x=> x.GetDetailedFundByCodeAsync(It.IsAny<string>()))
                             .Returns((string fundCode) => {
                                var funds = (IEnumerable<DetailedFund>)dummyDetailedFunds;
                                var result = funds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                                return Task.FromResult(result);
            });

            service = new DetailedFundService(mock.Object);

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFund), MemberType = typeof(DummyTest))]
        public async Task MustEnterTenFunds(List<DetailedFund> detailedFunds)
        {
            
            Setup();

            await service.AddDetailedFundsAsync(detailedFunds);

            var funds = await service.GetAllDetailedFundsAsync();

            funds.Should().HaveCount(10);

        }

        [Theory]
        [InlineData("ABCP11")]
        public async Task MustReturnOneFund(string fundCode)
        {
            
            Setup();

            var funds = await service.GetDetailedFundByCodeAsync(fundCode);

            funds.FundCode.Should().Be("ABCP11");

        }

    }
}