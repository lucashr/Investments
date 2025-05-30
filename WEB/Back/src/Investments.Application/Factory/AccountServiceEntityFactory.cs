using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain.Identity;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Investments.Application.Factory
{
    public class AccountServiceEntityFactory : IAccountServiceFactory
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountServiceEntityFactory(UserManager<User> userManager,
                                           SignInManager<User> signInManager,
                                           RoleManager<Role> roleManager,
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

            if (_userManager != null)
            {
                var user = await _userManager.Users
                                            .SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());
                signInResult = _signInManager.CheckPasswordSignInAsync(user, password, false);
            }

            return await signInResult;
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            UserDto userToReturn = null;

            if (_userManager != null)
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    userToReturn = _mapper.Map<UserDto>(user);
                }
            }

            return userToReturn;
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            UserUpdateDto userUpdateDto = null;

            if (_userPersist != null)
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if (user == null) return null;
                userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            }

            return userUpdateDto;
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            UserUpdateDto userUpdateDto1 = null;

            if (_userPersist != null)
            {
                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName);
                if (user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update<User>(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userReturn = await _userPersist.GetUserByUserNameAsync(user.UserName);
                    userUpdateDto = _mapper.Map<UserUpdateDto>(userReturn);
                }
            }

            return userUpdateDto;
        }

        public async Task<bool> UpdateUserRoleAsync(string userId, string newRole)
        {
            bool result = false;

            if (_userManager != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                var roleExists = await _roleManager.RoleExistsAsync(newRole);

                if (!roleExists)
                {
                    var createRoleResult = await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid().ToString(), Name = newRole });
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
            }

            return result;
        }

        public async Task<bool> UserExists(string userName)
        {
            bool result = false;

            if (_userManager != null)
                result = await _userManager.Users
                                     .AnyAsync(user => user.UserName == userName.ToLower());
            return result;
        }
    }
}