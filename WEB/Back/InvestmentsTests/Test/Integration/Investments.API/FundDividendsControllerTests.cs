using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.API;
using Investments.Application.Dtos;
using Investments.Domain.Models;
using Investments.Tests.Integration;
using Xunit;

namespace InvestmentsTests.Test.Integration.Investments.API
{
    public class FundDividendsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;

        public FundDividendsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            Setup().GetAwaiter().GetResult();
        }

        private async Task Setup()
        {
            loginDto = new UserLoginDto{ UserName = "admin", Password = "admin" };
            var response = await _client.PostAsJsonAsync("api/v1/Account/Login", loginDto);
            registerResponse = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", registerResponse.Token);
        }

        [Fact]
        public async Task GetFundDividendsByCodeReturnsOkWhenDividendsExist()
        {

            //TODO - Implementar inserção de dados 

            var fundCode = "ABC123";
            var response = await _client.GetAsync($"/api/v1/FundDividends/GetFundDividendsByCode/{fundCode}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var dividends = await response.Content.ReadFromJsonAsync<FundDividend[]>();
            dividends.Should().NotBeNull();
            dividends.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetFundDividendsByCodeReturnsNotFoundWhenNoDividendsExist()
        {

            //TODO - Implementar inserção de dados

            var fundCode = "XYZ789";
            var response = await _client.GetAsync($"/api/v1/FundDividends/GetFundDividendsByCode/{fundCode}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Be("No fund dividends found");
        }

        [Fact]
        public async Task GetAllFundsDividendsReturnsOkWhenDividendsExist()
        {

            //TODO - Implementar inserção de dados
            
            var response = await _client.GetAsync("/api/v1/FundDividends/GetAllFundsDividends");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var dividends = await response.Content.ReadFromJsonAsync<FundDividend[]>();
            dividends.Should().NotBeNull();
            dividends.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllFundsDividendsReturnsNotFoundWhenNoDividendsExist()
        {
            var response = await _client.GetAsync("/api/v1/FundDividends/GetAllFundsDividends");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Be("No funds dividends found");
        }
    }
}