// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Investments.Domain.Models;
// using Newtonsoft.Json;
// using Xunit;

// namespace Investments.Tests.Test.IntegrationTest
// {
//     public class FundsServiceTest : IClassFixture<CustomWebApplicationFactory<Investments.API.Startup>>
//     {

//         private readonly CustomWebApplicationFactory<Investments.API.Startup> _factory;

//         public FundsServiceTest(CustomWebApplicationFactory<Investments.API.Startup> factory)
//         {
//             _factory = factory;
//         }

//         [Theory]
//         [InlineData("api/FundRegistration/AllFunds")]
//         public async void TestServer(string url)
//         {

//             var clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
//             {
//                 HandleCookies = false,
//                 BaseAddress = new Uri("https://localhost:5001"),
//                 AllowAutoRedirect = true,
//                 MaxAutomaticRedirections = 7,
//             };

//             var client = _factory.CreateClient(clientOptions);

//             var response = await client.GetAsync(url);

//             string result = await response.Content.ReadAsStringAsync();

//             List<Funds> funds = JsonConvert.DeserializeObject<List<Funds>>(result);

//             // Assert
//             response.EnsureSuccessStatusCode();
//             Assert.Equal("text/plain; charset=utf-8", 
//                 response.Content.Headers.ContentType.ToString());
//         }
//     }
// }