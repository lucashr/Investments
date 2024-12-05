using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Moq;
using Xunit;

namespace Investments.Tests.Test
{
    public class FundsServiceTest
    {

        static FundsService service = null;
        static Mock<IFundsPersist> mockPersist = null;
        static Mock<IMapper> mapper = null;
        static Mock<IGeneralPersist> generalPersist = null;
        static List<Funds> dummyFunds = null;
        static Funds fundChange = null;
        
        public static void Setup()
        {

            mockPersist = new Mock<IFundsPersist>();
            mapper = new Mock<IMapper>();
            dummyFunds = new List<Funds>();
            fundChange = new Funds();

            dummyFunds = ((List<Funds>)DummyTest.Funds().ElementAt(0).ElementAt(0));

            generalPersist = new Mock<IGeneralPersist>();

            mockPersist.Setup(x => x.AddFundAsync(It.IsAny<string>())).Returns((string fundCode) => {
                        
                        var funds = (IEnumerable<Funds>)DummyTest.FundCodes().ElementAt(0).ElementAt(0);
                        var result = funds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                        result.Id = 1;

                        return Task.FromResult(result);
            });

            mockPersist.Setup(x => x.AddFundsAsync(It.IsAny<IEnumerable<DetailedFunds>>())).Returns((IEnumerable<DetailedFunds> fundCode) => {
                        
                        var funds = new List<Funds>();

                        foreach (var item in fundCode)
                        {
                            funds.Add(new Funds(){ FundCode = item.FundCode });
                            Console.WriteLine($"Fundo adicionado: {item.FundCode}");
                        }

                        return Task.FromResult(true);
            });

            mockPersist.Setup(x => x.DeleteFundByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                        
                        var funds = (List<Funds>)DummyTest.Funds().ElementAt(0).ElementAt(0);
                        var fund = funds.Where(x => x.FundCode == fundCode).FirstOrDefault();

                        if(funds.Remove(fund))
                        {
                            Console.WriteLine($"Fundo removido: {fund.FundCode}");
                            return Task.FromResult(true);
                        }
                        else
                        {
                            Console.WriteLine($"Fundo não encontrado: {fund.FundCode}");
                            return Task.FromResult(false);
                        }
                        
            });

            mockPersist.Setup(x => x.GetAllFundsAsync()).Returns(() => {
                        var funds = (IEnumerable<Funds>)DummyTest.Funds().ElementAt(0).ElementAt(0);
                        return Task.FromResult(funds);
            });

            mockPersist.Setup(x => x.GetFundByCodeAsync(It.IsAny<string>())).Returns((string fundCode) => {
                        var fund = dummyFunds.Where(x => x.FundCode == fundCode).FirstOrDefault();
                        return Task.FromResult(fund);
            });

            mockPersist.Setup(x => x.UpdateFundByCodeAsync(It.IsAny<string>(), It.IsAny<string>())).Returns((string oldFundCode, string newFundCode) => {
                        
                        var funds = (List<Funds>)DummyTest.FundCodes().ElementAt(0).ElementAt(0);
                        var fund = funds.Where(x => x.FundCode == oldFundCode).FirstOrDefault();

                        if(fund == null)
                        {
                            Console.WriteLine($"UpdateFundByCodeAsync::Fundo não encontrado: {fund.FundCode}");
                            Task.FromResult(fund);
                        }
                        else
                        {
                            funds.Remove(fund);
                            fund.FundCode = newFundCode;
                        }

                        funds.Add(fund);

                        return Task.FromResult(fund);
            });

            generalPersist.Setup(x => x.Update<Funds>(It.IsAny<Funds>())).Callback((Funds fund) => {
                Funds oldFund = dummyFunds.Where(x => x.Id == fund.Id).FirstOrDefault();
                int idx = dummyFunds.IndexOf(oldFund);
                fundChange = fund;
                dummyFunds[idx].Id = fund.Id;
                dummyFunds[idx].FundCode = fund.FundCode;
                Console.WriteLine($"Update::Update OK: {fund.FundCode}");
            });

            generalPersist.Setup(x => x.Add<Funds>(It.IsAny<Funds>())).Callback((Funds fund) => {
                int id = dummyFunds.Select(x=>x.Id).Max() + 1;
                fund.Id = id;
                fundChange = fund;
                dummyFunds.Add(fund);
                Console.WriteLine($"Add::Add OK: {fund.FundCode}");
            });

            generalPersist.Setup(x => x.SaveChangesAsync()).Returns(() => {
                if(dummyFunds.Where(x => x.FundCode == fundChange.FundCode).Count() > 0)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            });

            generalPersist.Setup(x => x.Delete<Funds>(It.IsAny<Funds>())).Callback((Funds fund) => {
                
                fundChange = fund;
                dummyFunds.Remove(fund);
                Console.WriteLine($"Delete::Delete OK: {fund.FundCode}");

            });

            service = new FundsService(generalPersist.Object, mockPersist.Object, mapper.Object);
            
        }

        [Theory]
        [MemberData(nameof(DummyTest.DetailedFunds), MemberType = typeof(DummyTest))]
        // [ConfigureTest]
        public async Task MustEnterTenFunds(List<DetailedFunds> detailedFunds)
        {
            
            Setup();

            var result = await service.AddFundsAsync(detailedFunds);

            result.Should().Be(true);

        }

        [Theory]
        [InlineData("AIEC11", "TESTE")]
        // [ConfigureTest]
        public async Task MustUpdateFundCode(string oldFundCode, string newFundCode)
        {
            
            Setup();

            Funds fund = await service.UpdateFundByCodeAsync(oldFundCode, newFundCode);

            fund.FundCode.Should().Be("TESTE", "Must return FundCode = TESTE");

        }

        [Theory]
        [InlineData("ZZZZ")]
        // [ConfigureTest]
        public async Task MustVerifyIfFundExistsAndCreateNewFund(string fundCode)
        {

            Setup();

            var fund = await service.AddFundAsync(fundCode);

            fund.FundCode.Should().Be("ZZZZ", "Must return FundCode = ZZZZ");
        }

        [Theory]
        [InlineData("AFOF11")]
        // [ConfigureTest]
        public async Task MustReturnOneFund(string fundCode)
        {

            Setup();

            var fund = await service.GetFundByCodeAsync(fundCode);

            fund.FundCode.Should().Be("AFOF11", "Must Return One Fund");
        }

        [Fact]
        // [ConfigureTest]
        public async Task MustReturnAllFunds()
        {

            Setup();

            var fund = await service.GetAllFundsAsync();

            fund.Should().HaveCount(10, "Must Return 10 records");

        }

        [Theory]
        [InlineData("AFHI11")]
        // [ConfigureTest]
        public async Task MustDeleteFundEReturnTrue(string fundCode)
        {

            Setup();

            var result = await service.DeleteFundByCodeAsync(fundCode);

            result.Should().Be(true);

        }

    }
}