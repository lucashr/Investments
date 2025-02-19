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
    public class DetailedFundsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;

        public DetailedFundsControllerTests(CustomWebApplicationFactory<Startup> factory)
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
        public async Task GetAllFundsReturnsOkWhenFundsExist()
        {

            //TODO - Implementar inserção de dados 

            var response = await _client.GetAsync("/api/v1/DetailedFunds/GetAllFunds");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var funds = await response.Content.ReadFromJsonAsync<DetailedFund[]>();
            funds.Should().NotBeNull();
            funds.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllFundsReturnsNotFoundWhenNoFundsExist()
        {
            var response = await _client.GetAsync("/api/v1/DetailedFunds/GetAllFunds");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Be("No funds found");
        }
    }
}