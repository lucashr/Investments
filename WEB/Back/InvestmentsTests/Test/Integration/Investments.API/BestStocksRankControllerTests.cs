using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.API;
using Investments.Application.Dtos;
using Investments.Domain.Models;
using Investments.Tests.Integration;
using Xunit;

namespace InvestmentsTests.Test.Integration.Investments.API
{
    public class BestStocksRankControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;

        private async Task Setup()
        {
            loginDto = new UserLoginDto{ UserName = "admin", Password = "admin" };
            var response = await _client.PostAsJsonAsync("api/v1/Account/Login", loginDto);
            registerResponse = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", registerResponse.Token);
        }

        public BestStocksRankControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            Setup().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task GetRankReturnsEmptyWhenStocksNotExist()
        {

            //TODO - Implementar inserção de dados 

            var response = await _client.GetAsync("/api/v1/BestStocksRank/GetRank/10");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetRankReturnsOkWhenStocksExist()
        {

            //TODO - Implementar inserção de dados 
            var response = await _client.GetAsync("/api/v1/BestStocksRank/GetRank/10");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            var stocks = JsonSerializer.Deserialize<List<BestStockRank>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            stocks.Should().NotBeNull().And.HaveCount(10);
        }
    }
}