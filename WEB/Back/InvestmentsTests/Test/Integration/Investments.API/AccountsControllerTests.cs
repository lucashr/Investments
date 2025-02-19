using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.API;
using Investments.Application.Dtos;
using Investments.Tests.Integration;
using Xunit;

namespace InvestmentsTests.Test.Integration
{
    public class AccountsControllerTests
    {
        public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;

            public AccountControllerTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
            }

            private UserDto GenerateRandomUserDto()
            {
                var faker = new Bogus.Faker<UserDto>()
                    .RuleFor(u => u.Id, f => f.Internet.Random.GetHashCode().ToString())
                    .RuleFor(u => u.UserName, f => f.Internet.UserName())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.Password, f => f.Internet.Password())
                    .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
                    .RuleFor(u => u.City, f => f.Address.City())
                    .RuleFor(u => u.Address, f => f.Address.StreetAddress())
                    .RuleFor(u => u.District, f => f.Address.County())
                    .RuleFor(u => u.State, f => f.Address.State())
                    .RuleFor(u => u.Function, f => f.Name.JobTitle());

                return faker.Generate();
            }

            [Fact]
            public async Task RegisterShouldReturnOkWhenUserIsCreated()
            {
                var userDto = GenerateRandomUserDto();

                var response = await _client.PostAsJsonAsync("api/v1/Account/Register", userDto);

                response.EnsureSuccessStatusCode();
            }

            [Fact]
            public async Task LoginShouldReturnOkWhenCredentialsAreValid()
            {
                var loginDto = new UserLoginDto{ UserName = "user", Password = "user" };

                var response = await _client.PostAsJsonAsync("api/v1/Account/Login", loginDto);
                var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();

                result.Username.Should().Be(loginDto.UserName);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}