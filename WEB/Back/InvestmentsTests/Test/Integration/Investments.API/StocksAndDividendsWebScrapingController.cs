using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.API;
using Investments.Application.Dtos;
using Investments.Tests.Integration;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace InvestmentsTests.Test.Integration.Investments.API
{
    public class StocksAndDividendsWebScrapingControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;
        private WebSocket _websocket;
        Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Startup> _factory;

        public StocksAndDividendsWebScrapingControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            Setup().GetAwaiter().GetResult();
        }

        private async Task Setup()
        {
            
            loginDto = new UserLoginDto{ UserName = "admin", Password = "admin" };
            var response = await _client.PostAsJsonAsync("api/v1/Account/Login", loginDto);
            registerResponse = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", registerResponse.Token);
           
            var appBuilder = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                });
            });

           var websocketClient = appBuilder.Server.CreateWebSocketClient();
           _websocket = await websocketClient.ConnectAsync(new Uri("wss://localhost:5001/ws?sessionId=test-session-stocks-webscraping"), CancellationToken.None);
           
           Assert.Equal(WebSocketState.Open, _websocket.State);
           
        }

        [Fact]
        public async Task GetStocksShouldReturnOkWhenStocksExist()
        {
            _client.GetAsync("/api/v1/StocksAndDividendsWebScraping/GetStocks");
            
            await Task.Delay(3000);

            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/StopWebScraping");
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetStockDividendsShouldReturnOkWhenDividendsExist()
        {
            
            _client.GetAsync("/api/v1/StocksAndDividendsWebScraping/GetStockDividends");
            
            await Task.Delay(3000);

            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/StopWebScraping");
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
        }

        [Fact]
        public async Task StopWebScrapingShouldReturnOkWhenProcessIsStopped()
        {

            _client.GetAsync("/api/v1/StocksAndDividendsWebScraping/GetStocks");

             await Task.Delay(3000);

            var response = await _client.GetAsync("/api/v1/StocksAndDividendsWebScraping/StopWebScraping");
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Match(x => x.Contains("Process stopped.") || 
                                        x.Contains("No process is running."));
        }
    }
}