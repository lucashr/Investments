using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Investments.Application;
using Investments.Application.Dtos;
using Investments.Domain.Identity;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Investments.Tests.Unit.Application
{
    public class AccountServiceTests
    {
        private readonly FakeUserManager _userManagerMock;
        private readonly FakeSignInManager _signInManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserPersist> _userPersistMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _userManagerMock = new FakeUserManager();

            _signInManagerMock = new FakeSignInManager();
            
            _roleManagerMock = new Mock<RoleManager<Role>>(
                Mock.Of<IRoleStore<Role>>(), 
                null, null, null, null
            );

            _mapperMock = new Mock<IMapper>();
            _userPersistMock = new Mock<IUserPersist>();

            _accountService = new AccountService(
                _userManagerMock, 
                _signInManagerMock, 
                _roleManagerMock.Object, 
                _mapperMock.Object, 
                _userPersistMock.Object
            );

        }

        private UserDto GenerateFakeUserDto()
        {
            var userFaker = new Faker<UserDto>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Email, f => f.Internet.Email());

            return userFaker.Generate();
        }

        private UserUpdateDto GenerateFakeUserUpdateDto()
        {
            var userUpdateFaker = new Faker<UserUpdateDto>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password());

            return userUpdateFaker.Generate();
        }

        [Fact]
        public async Task CreateAccountAsyncShouldReturnUserDtoWhenAccountIsCreated()
        {
            // Arrange
            var userDto = GenerateFakeUserDto();
            // var user = new User { UserName = userDto.UserName, Email = userDto.Email };
            // _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);
            // // _userManagerMock.Setup(u => u.CreateAsync(user, userDto.Password)).ReturnsAsync(IdentityResult.Success);
            // _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _accountService.CreateAccountAsync(userDto);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be(userDto.UserName);
            result.Email.Should().Be(userDto.Email);
        }

        [Fact]
        public async Task CheckUserPasswordAsyncShouldReturnSignInResultWhenPasswordIsCorrect()
        {
            // Arrange
            var userUpdateDto = GenerateFakeUserUpdateDto();
            var password = "Password123!";
            var user = new User { UserName = userUpdateDto.UserName };
            
            // _userManagerMock.Setup(u => u.Users).Returns(new List<User> { user }.AsQueryable().BuildMock());
            // _signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(user, password, false))
            //                   .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _accountService.CheckUserPasswordAsync(userUpdateDto, password);

            // Assert
            result.Should().Be(SignInResult.Success);
        }

        [Fact]
        public async Task GetUserByUserNameAsyncShouldReturnUserUpdateDtoWhenUserExists()
        {
            // Arrange
            var userName = "testuser";
            var user = new User { UserName = userName };
            var userUpdateDto = GenerateFakeUserUpdateDto();
            _userPersistMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserUpdateDto>(user)).Returns(userUpdateDto);

            // Act
            var result = await _accountService.GetUserByUserNameAsync(userName);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be(userName);
        }

        [Fact]
        public async Task UpdateAccountShouldReturnUserUpdateDtoWhenAccountIsUpdated()
        {
            // Arrange
            var userUpdateDto = GenerateFakeUserUpdateDto();
            var user = new User { UserName = userUpdateDto.UserName };
            var token = "reset-token";
            
            _userPersistMock.Setup(u => u.GetUserByUserNameAsync(userUpdateDto.UserName)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map(userUpdateDto, user));
            
            // _userManagerMock.Setup(u => u.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
            // _userManagerMock.Setup(u => u.ResetPasswordAsync(user, token, userUpdateDto.Password))
            //                 .ReturnsAsync(IdentityResult.Success);

            _userPersistMock.Setup(u => u.Update<User>(user));
            _userPersistMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);

            _mapperMock.Setup(m => m.Map<UserUpdateDto>(user)).Returns(userUpdateDto);

            // Act
            var result = await _accountService.UpdateAccount(userUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be(userUpdateDto.UserName);
        }

        [Fact]
        public async Task UpdateUserRoleAsyncShouldReturnTrueWhenRoleIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newRole = "Admin";
            var user = new User { Id = userId };
            // _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
            // _roleManagerMock.Setup(r => r.RoleExistsAsync(newRole)).ReturnsAsync(true);
            // _userManagerMock.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string>());
            // _userManagerMock.Setup(u => u.AddToRoleAsync(user, newRole)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _accountService.UpdateUserRoleAsync(userId, newRole);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UserExistsShouldReturnTrueWhenUserExists()
        {
            // Arrange
            var userName = "existinguser";
            // _userManagerMock.Setup(u => u.FindByNameAsync(userName)).ReturnsAsync(new User { UserName = userName });

            // Act
            var result = await _accountService.UserExists(userName);

            // Assert
            result.Should().BeTrue();
        }
    }
}
