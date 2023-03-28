using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Investments.Application;
using Investments.Domain.Models;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Investments.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Investments.Tests.Test.IntegrationTest
{
    public class FundRegistrationControllerTest
    {

        private static CustomWebApplicationFactory<Investments.API.Startup> _factory;
        private static FundsPersist fundsPersist = null;
        private static FundsService fundsService = null;
        private static Mock<IGeneralPersist> iGeneralPersist = null;
        private static Mock<IMapper> iMapper = null;
        private static InvestmentsContext ctx = null;
        private static DbContextOptionsBuilder<InvestmentsContext> optionsBuilder = null;
        private static string dbName = null;

        public FundRegistrationControllerTest(ITestOutputHelper output)
        {
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            var test = (ITest)testMember.GetValue(output);
            dbName = test.TestCase.TestMethod.Method.Name;
        }

        static dynamic clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
        {
            HandleCookies = false,
            BaseAddress = new Uri("https://localhost:5001"),
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 7,
        };

        public static async Task CreateContext()
        {

            optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(dbName);

            ctx = new InvestmentsContext(optionsBuilder.Options);

            await Task.CompletedTask;
        }

        public static async Task Setup()
        {
            
            await CreateContext();

            fundsPersist = new FundsPersist(ctx);
            iGeneralPersist = new Mock<IGeneralPersist>();
            iMapper = new Mock<IMapper>();
            fundsService = new FundsService(iGeneralPersist.Object, fundsPersist, iMapper.Object);
            
        }

        public async Task SeedDB()
        {
            
            dynamic funds = DummyTest.Funds().ElementAt(0).ElementAt(0);

            using (InvestmentsContext ctx = new(optionsBuilder.Options))
            {
                ctx.Database.EnsureCreated();
                await ctx.AddRangeAsync(funds.ToArray());
                ctx.SaveChanges();
            }

        }

        [Theory]
        [InlineData("BBBB")]
        public async void MustTryEnterAndReturnError(string fundCode)
        {
            
            await Setup();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            Funds fund = new Funds(){Id = 1, FundCode = fundCode};

            var jsonContent = JsonConvert.SerializeObject(fund); 
            var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "appication/json");
            
            string url = $"api/FundRegistration/FundRegistration?fundCods={fundCode}";

            var response = await client.PostAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode().StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().Contain("Fundo já cadastrado na base de dados");

        }        

        [Theory]
        // [ConfigureTest]
        [MemberData(nameof(DummyTest.FundCodes), MemberType = typeof(DummyTest))]
        public async void MustInsertAndReturnMessageOK(List<string> fundCodes)
        {

            await Setup();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            var jsonContent = JsonConvert.SerializeObject(fundCodes); 
            var contentString  = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json-patch+json");
            
            string url = $"api/FundRegistration/FundsRegistration?fundCodes={jsonContent}";

            var response = await client.PostAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode().StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().Contain("Operação efetuada com sucesso!");

        }

        [Theory]
        // [ConfigureTest]
        [InlineData("AFCR11")]
        public async void MustDeleteAndReturnTrue(string fundCode)
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            string url = $"api/FundRegistration/DeleteFundByCode/{fundCode}";

            var response = await client.DeleteAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode().StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().Be("true");

        }

        [Theory]
        // [ConfigureTest]
        [InlineData("AEFI11", "EDCBA")]
        public async void MustUpdateAndReturnModifiedFund(string oldFundCode, string newFundCode)
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
            
            var contentString  = new StringContent($"{oldFundCode}/{newFundCode}", System.Text.Encoding.UTF8, "text/plain");

            string url = $"api/FundRegistration/UpdateFundByCode/{oldFundCode}/{newFundCode}";

            var response = await client.PutAsync(url, contentString);

            string result = await response.Content.ReadAsStringAsync();

            var fund = JsonConvert.DeserializeObject<Funds>(result);

            response.EnsureSuccessStatusCode().StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            fund.FundCode.Should().Be(newFundCode);

        }

        [Theory]
        // [ConfigureTest]
        [InlineData("ABCP11")]
        public async void MustGetFundByCode(string fundCode)
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( 
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
            
            string url = $"api/FundRegistration/GetFundbyCode/{fundCode}";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            var fund = JsonConvert.DeserializeObject<Funds>(result);

            response.EnsureSuccessStatusCode().StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            fund.FundCode.Should().Be(fund.FundCode);

        }

        [Fact]
        // [ConfigureTest]
        public async void MustReturnOneListFunds()
        {

            await Setup();
            await SeedDB();

            _factory = new CustomWebApplicationFactory<Investments.API.Startup>(dbName);

            HttpClient client = _factory.CreateClient(clientOptions);
            
            string url = $"api/FundRegistration/AllFunds";

            var response = await client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            var funds = JsonConvert.DeserializeObject<List<Funds>>(result);

            response.EnsureSuccessStatusCode().StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("text/plain; charset=utf-8");
        
        }

    }
}