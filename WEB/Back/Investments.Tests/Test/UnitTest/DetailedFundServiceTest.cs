
using System.Collections.Generic;
using AutoMapper;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;
using System.Linq;
using Investments.Tests.Helpers;
using System.Threading.Tasks;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Tests.Test
{

    public class DetailedFundServiceTest 
    {

        static DetailedFundPersist detailedFundPersist = null;
        static DetailedFundService detailedFundService = null;
        static Mock<IGeneralPersist> iGeneralPersist = null;
        static Mock<IMapper> iMapper = null;
        static Mock<FundsPersist> fundsPersist = null;
        static InvestmentsContext ctx = null;

        public static void CreateContext()
        {

            var ContextOptions = new DbContextOptionsBuilder<InvestmentsContext>()
                                .UseSqlite($"Data Source=Test_DetailedFundServiceTest.db")
                                .EnableSensitiveDataLogging().Options;
            
            ctx = new InvestmentsContext(ContextOptions);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

        }
 
        public static void Setup()
        {
            
            // var ctx = await ConfigureTest.ConfigureDatabase();
            detailedFundPersist = new DetailedFundPersist(ctx);
            detailedFundService = new DetailedFundService(detailedFundPersist);
            iGeneralPersist = new Mock<IGeneralPersist>();
            iMapper = new Mock<IMapper>();
            fundsPersist = new Mock<FundsPersist>(ctx);

        }

        public async static Task<bool> SeedDB()
        {

            dynamic funds = DummyTest.DetailedFunds().ElementAt(0).ElementAt(0);
            await detailedFundService.AddDetailedFundsAsync(funds);

            return true;

        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public static async Task MustEnterTenFunds(List<DetailedFunds> detailedFunds)
        {
            
            CreateContext();
            Setup();

            FundsService fundsService = new FundsService(iGeneralPersist.Object, fundsPersist.Object, iMapper.Object);

            await detailedFundService.AddDetailedFundsAsync(detailedFunds);

            var funds = await detailedFundService.GetAllDetailedFundsAsync();

            Assert.Equal(10, funds.Count());

        }

        [Theory]
        [InlineData("ABCP11")]
        // [ConfigureTest]
        public static async Task MustReturnOneFund(string fundCode)
        {
            
            CreateContext();
            Setup();
            await SeedDB();

            var funds = await detailedFundService.GetDetailedFundByCodeAsync(fundCode);

            Assert.Equal(fundCode, funds.FundCode);

        }

    }
}