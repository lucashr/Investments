using System;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Persistence;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Application
{
    public class AccountServiceFactory : IAccountServiceFactory
    {

        private readonly IAccountServiceFactory _accountService;

        public AccountServiceFactory(IAccountServiceFactory accountService)
        {
            _accountService = accountService;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            return await _accountService.CheckUserPasswordAsync(userUpdateDto, password);
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            return await _accountService.CreateAccountAsync(userDto);
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            return await _accountService.GetUserByUserNameAsync(userName);
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            return await _accountService.UpdateAccount(userUpdateDto);
        }

        public async Task<bool> UpdateUserRoleAsync(string userId, string newRole)
        {
            return await _accountService.UpdateUserRoleAsync(userId, newRole);
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _accountService.UserExists(userName);
        }
    }
}
