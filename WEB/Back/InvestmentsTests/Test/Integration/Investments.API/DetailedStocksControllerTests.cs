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
using Investments.Domain;
using Investments.Tests.Integration;
using Xunit;

namespace InvestmentsTests.Test.Integration.Investments.API
{
    public class DetailedStocksControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;

        public DetailedStocksControllerTests(CustomWebApplicationFactory<Startup> factory)
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
        public async Task GetAllStocksReturnsOkWhenStocksExist()
        {

            //TODO - Implementar inserção de dados 
            
            var response = await _client.GetAsync("/api/v1/DetailedStocks/GetAllStocks");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stocks = await response.Content.ReadFromJsonAsync<DetailedStock[]>();
            stocks.Should().NotBeNull();
            stocks.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllStocksReturnsNotFoundWhenNoStocksExist()
        {
            var response = await _client.GetAsync("/api/v1/DetailedStocks/GetAllStocks");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Be("No stocks found");
        }
    }
}