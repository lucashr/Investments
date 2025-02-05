using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.Application;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Persistence.Contracts;
using Moq;
using Xunit;

namespace InvestmentsTests.Test.Unit.Investments.Application
{
    public class EnderecoUsuarioServiceTests
    {
        private readonly Mock<IUserAddressPersist> _mockUserAddressPersist;
        private readonly EnderecoUsuarioService _enderecoUsuarioService;

        public EnderecoUsuarioServiceTests()
        {
            _mockUserAddressPersist = new Mock<IUserAddressPersist>();
            _enderecoUsuarioService = new EnderecoUsuarioService(_mockUserAddressPersist.Object);
        }

        private UserAddress CreateFakeUserAddress()
        {

            var user = new Faker<User>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PasswordHash, f => f.Internet.Password());

            var faker = new Faker<UserAddress>()
                .RuleFor(a => a.UserId, f => f.Random.AlphaNumeric(5))
                .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
                .RuleFor(a => a.State, f => f.Address.StreetName())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.User, f => user);

            return faker.Generate();
            
        }

        [Fact]
        public async Task GetAddressUser_ShouldReturnUserAddress_WhenExists()
        {
            var userAddress = CreateFakeUserAddress();

            _mockUserAddressPersist.Setup(m => m.GetAddressUser(userAddress.User.UserName)).ReturnsAsync(userAddress);

            var result = await _enderecoUsuarioService.GetAddressUser(userAddress.User.UserName);

            result.Should().Be(userAddress);
        }

        [Fact]
        public async Task SaveAddressUser_ShouldReturnTrue_WhenSavedSuccessfully()
        {
            var userAddress = CreateFakeUserAddress();

            _mockUserAddressPersist.Setup(m => m.SaveAddressUser(userAddress)).ReturnsAsync(true);

            var result = await _enderecoUsuarioService.SaveAddressUser(userAddress);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAddressUser_ShouldReturnTrue_WhenUpdatedSuccessfully()
        {
            var userAddress = CreateFakeUserAddress();

            _mockUserAddressPersist.Setup(m => m.UpdateAddressUser(userAddress)).ReturnsAsync(true);

            var result = await _enderecoUsuarioService.UpdateAddressUser(userAddress);

            result.Should().BeTrue();
        }
    }
}