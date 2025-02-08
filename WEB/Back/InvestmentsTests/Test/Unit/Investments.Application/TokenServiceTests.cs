using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Investments.Application;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;
using Investments.Domain.Enum;

namespace InvestmentsTests.Test.Unit.Investments.Application
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUserPersistMongoDb> _mockUserPersistMongoDb;
        private readonly TokenService _tokenService;
        private const string tokenKey = "supersecretkey123456789012345678901234567890123456789012345678901234567890";

        public TokenServiceTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(x => x["TokenKey"]).Returns(tokenKey);

            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _mockMapper = new Mock<IMapper>();
            _mockUserPersistMongoDb = new Mock<IUserPersistMongoDb>();

            _tokenService = new TokenService(_mockConfig.Object, _mockUserManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateToken_ShouldReturnValidJwtToken()
        {

            var user = new Bogus.Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Function, f => Function.Admin )
            .RuleFor(u => u.UserRoles, f => new List<UserRole>())
            .RuleFor(u => u.EnderecoUsuario, f => new UserAddress { Address = f.Address.StreetAddress(), City = f.Address.City(), State = f.Address.State() })
            .Generate();

            var userUpdateDto = new Bogus.Faker<UserUpdateDto>()
            .RuleFor(u => u.Id, user.Id)
            .RuleFor(u => u.UserName, user.UserName)
            .RuleFor(u => u.FirstName, user.FirstName)
            .RuleFor(u => u.LastName, user.LastName)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Function, f => f.Name.JobTitle())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Token, f => f.Random.Guid().ToString())
            .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.District, f => f.Address.County())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.State, f => f.Address.State())
            .RuleFor(u => u.UserRoles, f => 
                new List<UserRole>(){new UserRole{
                    User = user,
                    Role = new Role{Id = Guid.NewGuid().ToString(), Name = "Admin"}
                }}
            )
            .Generate();
            
            _mockMapper.Setup(m => m.Map<User>(userUpdateDto)).Returns(user);
            _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Admin" });

            var token = await _tokenService.CreateToken(userUpdateDto);

            token.Should().NotBeNullOrEmpty();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            jwtToken.Claims.Should().Contain(c => c.Type == "role" && c.Value == "Admin");
            jwtToken.Claims.Should().Contain(c => c.Type == "unique_name" && c.Value == user.UserName);
        }

        [Fact]
        public void GetUsernameFromToken_ShouldReturnCorrectUsername()
        {
            
            var claims = new List<Claim>
            {
                new Claim("username", "testuser")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var username = _tokenService.GetUsernameFromToken(tokenString);

            username.Should().Be("testuser");
        }
    }

}