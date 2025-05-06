using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.API;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Tests.Integration;
using Xunit;

namespace InvestmentsTests.Test.Integration.Investments.API
{
    public class StockDividendsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;

        public StockDividendsControllerTests(CustomWebApplicationFactory<Startup> factory)
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
        public async Task GetStockDividendsByCode_ShouldReturnOk_WhenDividendsExist()
        {
            var stockCode = "CMIG4";
            
            var response = await _client.GetAsync($"/api/v1/StockDividends/GetstockDividendsByCode/{stockCode}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var returnedDividends = JsonSerializer.Deserialize<List<StockDividend>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            returnedDividends.Should().NotBeNull().And.HaveCount(3);
            returnedDividends.Should().AllSatisfy(d => d.FundCode.Should().Be(stockCode));
        }

        [Fact]
        public async Task GetStockDividendsByCode_ShouldReturnNotFound_WhenNoDividendsExist()
        {
            var stockCode = "CMIG4";

            var response = await _client.GetAsync($"/api/v1/StockDividends/GetstockDividendsByCode/{stockCode}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAllStockDividends_ShouldReturnOk_WhenDividendsExist()
        {
            var response = await _client.GetAsync("/api/v1/StockDividends/GetAllStockDividends");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var returnedDividends = JsonSerializer.Deserialize<List<StockDividend>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            returnedDividends.Should().NotBeNull().And.HaveCount(5);
        }

        [Fact]
        public async Task GetAllStockDividends_ShouldReturnNotFound_WhenNoDividendsExist()
        {
            var response = await _client.GetAsync("/api/v1/StockDividends/GetAllStockDividends");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}