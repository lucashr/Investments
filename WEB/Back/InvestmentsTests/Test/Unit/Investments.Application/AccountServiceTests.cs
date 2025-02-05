using System;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain.Identity;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Investments.Tests
{
    public class AccountServiceTests
    {
        private readonly FakeUserManager _userManagerMock;
        private readonly FakeSignInManager _signInManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserPersist> _userPersistMock;
        private readonly AccountServiceFactory _accountService;
        private readonly IAccountServiceFactory _accountServiceFactory;

        public AccountServiceTests()
        {
            _userManagerMock = new FakeUserManager();

            _signInManagerMock = new FakeSignInManager();
            
            _roleManagerMock = new Mock<RoleManager<Role>>(
                Mock.Of<IRoleStore<Role>>(), 
                null, null, null, null
            );
            
            _accountServiceFactory = new Mock<IAccountServiceFactory>().Object;
            _mapperMock = new Mock<IMapper>();
            _userPersistMock = new Mock<IUserPersist>();

            _accountService = new AccountServiceFactory(
                _accountServiceFactory
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
            var userDto = GenerateFakeUserDto();

            var result = await _accountService.CreateAccountAsync(userDto);

            result.Should().NotBeNull();
            result.UserName.Should().Be(userDto.UserName);
            result.Email.Should().Be(userDto.Email);
        }

        [Fact]
        public async Task CheckUserPasswordAsyncShouldReturnSignInResultWhenPasswordIsCorrect()
        {
            var userUpdateDto = GenerateFakeUserUpdateDto();
            var password = "Password123!";

            var result = await _accountService.CheckUserPasswordAsync(userUpdateDto, password);

            result.Should().Be(SignInResult.Success);
        }

        [Fact]
        public async Task GetUserByUserNameAsyncShouldReturnUserUpdateDtoWhenUserExists()
        {
            var userName = "testuser";
            var user = new User { UserName = userName };
            var userUpdateDto = GenerateFakeUserUpdateDto();
            _userPersistMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserUpdateDto>(user)).Returns(userUpdateDto);

            var result = await _accountService.GetUserByUserNameAsync(userName);

            result.Should().NotBeNull();
            result.UserName.Should().Be(userName);
        }

        [Fact]
        public async Task UpdateAccountShouldReturnUserUpdateDtoWhenAccountIsUpdated()
        {
            var userUpdateDto = GenerateFakeUserUpdateDto();
            var user = new User { UserName = userUpdateDto.UserName };
            
            _userPersistMock.Setup(u => u.GetUserByUserNameAsync(userUpdateDto.UserName)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map(userUpdateDto, user));
            
            _userPersistMock.Setup(u => u.Update<User>(user));
            _userPersistMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);

            _mapperMock.Setup(m => m.Map<UserUpdateDto>(user)).Returns(userUpdateDto);

            var result = await _accountService.UpdateAccount(userUpdateDto);

            result.Should().NotBeNull();
            result.UserName.Should().Be(userUpdateDto.UserName);
        }

        [Fact]
        public async Task UpdateUserRoleAsyncShouldReturnTrueWhenRoleIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newRole = "Admin";

            // Act
            var result = await _accountService.UpdateUserRoleAsync(userId, newRole);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UserExistsShouldReturnTrueWhenUserExists()
        {
            var userName = "existinguser";

            var result = await _accountService.UserExists(userName);

            result.Should().BeTrue();
        }
    }
}
