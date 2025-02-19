using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.WebSockets;
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
    public class FundsAndDividendsWebScrapingControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _client;
        private UserLoginDto loginDto;
        private RegisterResponse registerResponse;
        private WebSocket _websocket;
        Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Startup> _factory;
        

        public FundsAndDividendsWebScrapingControllerTests(CustomWebApplicationFactory<Startup> factory)
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
           _websocket = await websocketClient.ConnectAsync(new Uri("wss://localhost:5001/ws?sessionId=test-session"), CancellationToken.None);
           
           Assert.Equal(WebSocketState.Open, _websocket.State);
           
        }

        [Fact]
        public async Task GetFundsAsyncReturnsOkWhenFundsExist()
        {
            _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/GetFunds");

            await Task.Delay(3000);

            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/StopWebScraping");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetFundsAsyncReturnsNotFoundWhenNoFundsExist()
        {
            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/GetFunds");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Contain("No funds found");
        }

        [Fact]
        public async Task GetFundDividendsAsyncReturnsOkWhenDividendsExist()
        {
            _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/GetFundDividends");

            await Task.Delay(2000);

            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/StopWebScraping");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetFundDividendsAsyncReturnsNotFoundWhenNoDividendsExist()
        {
            
            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/GetFundDividends");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Contain("No funds dividends found");
        }

        [Fact]
        public async Task PauseReturnsOkWhenNoProcessIsRunning()
        {
            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/StopWebScraping");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Contain("No process is running");
        }

        [Fact]
        public async Task PauseReturnsOkWhenProcessIsStopped()
        {
            _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/GetFunds");
            
            await Task.Delay(2000);

            var response = await _client.GetAsync("/api/v1/FundsAndDividendsWebScraping/StopWebScraping");
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().Contain("Process stopped");
        }

        public void Dispose()
        {
            _websocket.Dispose();
        }
    }
}