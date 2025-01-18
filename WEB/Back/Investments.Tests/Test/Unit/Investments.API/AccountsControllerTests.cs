using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.API.Controllers;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
using Microsoft.AspNetCore.Identity;
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

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenRoleIsUpdated()
        {
            // Arrange
            var username = "testuser";
            var role = "Admin";
            var user = new UserDto { Id = "1", UserName = username };

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(username))
                .ReturnsAsync(new UserUpdateDto { UserName = user.UserName });
            _accountServiceMock.Setup(s => s.UpdateUserRoleAsync(user.Id, role))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateUser(username, role);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Role atualizada com sucesso!", okResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnBadRequest_WhenUserNotFound()
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
            Assert.Equal("Falha ao tentar obter usuario pelo username!", badRequestResult.Value);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsCreated()
        {
            // Arrange
            var model = new UserDto
            {
                FirstName = "Teste",
                LastName = "Super",
                Email = "Teste@Teste.com",
                Password = "password",
                UserName = "Teste",
                ZipCode = "12345",
                City = "City",
                Address = "Address",
                District = "District",
                State = "State",
                Function = "User"
            };

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
                .ReturnsAsync("token123");

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal(model.UserName, response.username);
            Assert.Equal(model.FirstName, response.firstName);
            Assert.Equal("token123", response.token);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenUserCreationFails()
        {
            // Arrange
            var model = new UserDto { UserName = "johndoe" };

            _accountServiceMock.Setup(s => s.CreateAccountAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(new UserDto { UserName = null });

            // Act
            var result = await _controller.Register(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Falha ao tentar criar uma conta!", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new UserLoginDto { UserName = "johndoe", Password = "password" };
            var user = new UserUpdateDto { UserName = loginDto.UserName, FirstName = "John" };

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(loginDto.UserName))
                .ReturnsAsync(user);
            _accountServiceMock.Setup(s => s.CheckUserPasswordAsync(user, loginDto.Password))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _tokenServiceMock.Setup(s => s.CreateToken(It.IsAny<UserUpdateDto>()))
                .ReturnsAsync("token123");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal(user.UserName, response.username);
            Assert.Equal(user.FirstName, response.firstName);
            Assert.Equal("token123", response.token);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new UserLoginDto { UserName = "johndoe", Password = "wrongpassword" };

            _accountServiceMock.Setup(s => s.GetUserByUserNameAsync(loginDto.UserName))
                .ReturnsAsync((UserUpdateDto)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Usuário ou senha está inválido!", unauthorizedResult.Value);
        }
    }
}