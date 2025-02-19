using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Application.helpers;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Investments.Application.Factory
{
    public class AccountServiceMongoDbFactory : IAccountServiceFactory
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountServiceMongoDbFactory(UserManager<ApplicationUser> userManager,
                                           SignInManager<ApplicationUser> signInManager,
                                           RoleManager<ApplicationRole> roleManager,
                                           IMapper mapper,
                                           IUserPersist userPersist)
        {
            _mapper = mapper;
            _userPersist = userPersist;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            Task<SignInResult> signInResult = null;
            var user = await _userManager.FindByNameAsync(userUpdateDto.UserName);
            signInResult = _signInManager.CheckPasswordSignInAsync(user, password, false);

            return await signInResult;
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            UserDto userToReturn = null;

            // string erro = string.Empty;
            // var config = new MapperConfiguration(cfg =>
            // {
            //     cfg.AddProfile<InvestmentsProfile>();
            // });

            // try
            // {
            //     config.AssertConfigurationIsValid();
            // }
            // catch (AutoMapperConfigurationException ex)
            // {
            //     foreach (var error in ex.Errors)
            //     {

            //         Debug.WriteLine($"Erro: {error.TypeMap.Types} -> ");
            //         erro = string.Empty;
            //         foreach (var item in error.UnmappedPropertyNames)
            //         {
            //             if(erro == "")
            //                 erro = item;
            //             else
            //                 erro = string.Concat(erro, ", ", item);

            //             Debug.WriteLine($"Erro: {error.TypeMap} -> ");
            //         }
            //     }
            //     throw;
            // }

            var user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                userToReturn = _mapper.Map<UserDto>(user);
            }

            return userToReturn;
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            UserUpdateDto userUpdateDto = null;

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return null;
            userUpdateDto = new UserUpdateDto { Id = user.Id, UserName = user.UserName, Email = user.Email };

            return userUpdateDto;
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            UserUpdateDto userUpdateDto1 = null;

            var user = await _userManager.FindByNameAsync(userUpdateDto.UserName);
            if (user == null) return null;

            _mapper.Map(userUpdateDto, user);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

            var ret = await _userManager.UpdateAsync(user);

            if (ret.Succeeded)
            {
                var userReturn = await _userManager.FindByNameAsync(user.UserName);
                userUpdateDto = _mapper.Map<UserUpdateDto>(userReturn);
            }

            return userUpdateDto;
        }

        public async Task<bool> UpdateUserRoleAsync(string userId, string newRole)
        {
            bool result = false;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(newRole);

            if (!roleExists)
            {
                var createRoleResult = await _roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = newRole });
                if (!createRoleResult.Succeeded)
                {
                    throw new Exception("Erro ao criar a nova role.");
                }
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                throw new Exception("Erro ao remover as roles atuais do usuário.");
            }

            var addResult = await _userManager.AddToRoleAsync(user, newRole);

            if (!addResult.Succeeded)
            {
                throw new Exception("Erro ao adicionar a nova role ao usuário.");
            }

            result = true;

            return result;
        }

        public async Task<bool> UserExists(string userName)
        {
            bool result = false;

                result = await _userManager.Users
                                         .AnyAsync(user => user.UserName == userName.ToLower());
            return result;
        }
    }
}