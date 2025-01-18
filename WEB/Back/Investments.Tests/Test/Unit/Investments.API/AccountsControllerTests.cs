using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Investments.Tests.Test.Unit.Investments.API
{
    public class AccountsControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<IEnderecoUsuarioService> _enderecoUsuarioServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AccountsController _controller;

        public AccountsControllerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _enderecoUsuarioServiceMock = new Mock<IEnderecoUsuarioService>();
            _tokenServiceMock = new Mock<ITokenService>();

            _controller = new AccountsController(
                _accountServiceMock.Object,
                _enderecoUsuarioServiceMock.Object,
                _tokenServiceMock.Object
            );
        }

        private UserDto GenerateRandomUserDto()
        {
            var faker = new Faker<UserDto>()
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

        private UserUpdateDto GenerateRandomUserUpdateDto()
        {
            var faker = new Faker<UserUpdateDto>()
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

        private UserLoginDto GenerateRandomUserLoginDto()
        {
            var faker = new Faker<UserLoginDto>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password());

            return faker.Generate();
        }

        [Fact]
        public async Task UpdateUserShouldReturnOkWhenRoleIsUpdated()
        {
            // Arrange
            var user = GenerateRandomUserDto();
            var username = user.UserName;
            var role = user.Function;

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(username))
                .ReturnsAsync(GenerateRandomUserUpdateDto());
            _accountServiceMock.Setup(s => s.UpdateUserRoleAsync(user.Id, role))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateUser(username, role);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("Role atualizada com sucesso!");
        }

        [Fact]
        public async Task UpdateUserShouldReturnBadRequestWhenUserNotFound()
        {
            // Arrange
            var username = "invaliduser";
            var role = "Admin";

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(username))
                .ReturnsAsync((UserUpdateDto)null);

            // Act
            var result = await _controller.UpdateUser(username, role);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().Be("Falha ao tentar obter usuario pelo username!");
        }

        [Fact]
        public async Task RegisterShouldReturnOkWhenUserIsCreated()
        {
            // Arrange
            var model = GenerateRandomUserDto();

            var createdUser = new UserDto { UserName = model.UserName };
            _accountServiceMock.Setup(s => s.CreateAccountAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(createdUser);

            _enderecoUsuarioServiceMock.Setup(s => s.SaveAddressUser(It.IsAny<UserAddress>()))
                .ReturnsAsync(true);

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(model.UserName))
                .ReturnsAsync(new UserUpdateDto
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName
                });

            _tokenServiceMock.Setup(s => s.CreateToken(It.IsAny<UserUpdateDto>()))
                .ReturnsAsync("AJHSAd545616!!@$");

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as RegisterResponse;
            
            model.UserName.Should().Be(response.Username);
            model.FirstName.Should().Be(response.FirstName);
            response.Token.Should().Be("AJHSAd545616!!@$");
        }

        [Fact]
        public async Task RegisterShouldReturnBadRequestWhenUserCreationFails()
        {
            // Arrange
            var model = GenerateRandomUserDto();

            _accountServiceMock.Setup(s => s.CreateAccountAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(new UserDto { UserName = null });

            // Act
            var result = await _controller.Register(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().Be("Falha ao tentar criar uma conta!");
        }

        [Fact]
        public async Task LoginShouldReturnOkWhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = GenerateRandomUserLoginDto();
            var user = new UserUpdateDto { UserName = loginDto.UserName, FirstName = "Karen" };

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(loginDto.UserName))
                .ReturnsAsync(user);
            _accountServiceMock.Setup(s => s.CheckUserPasswordAsync(user, loginDto.Password))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _tokenServiceMock.Setup(s => s.CreateToken(It.IsAny<UserUpdateDto>()))
                .ReturnsAsync("AJHSAd545616!!@$");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as RegisterResponse;
            
            user.UserName.Should().Be(response.Username);
            user.FirstName.Should().Be(response.FirstName);
            response.Token.Should().Be("AJHSAd545616!!@$");
        }

        [Fact]
        public async Task LoginShouldReturnUnauthorizedWhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = GenerateRandomUserLoginDto();

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(loginDto.UserName))
                .ReturnsAsync((UserUpdateDto)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            unauthorizedResult.Value.Should().Be("Usuário ou senha está inválido!");
        }
    }
}