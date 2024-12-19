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

namespace Investments.Application
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountService(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              RoleManager<Role> roleManager,
                              IMapper mapper,
                              IUserPersist userPersist)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersist = userPersist;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users
                                             .SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                
                if(result.Succeeded)
                {

                    var userToReturn = _mapper.Map<UserDto>(user);

                    return userToReturn;

                }

                return null;

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar criar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {

                var user = await _userPersist.GetUserByUserNameAsync(userName);

                if(user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);

                return userUpdateDto;
                    
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar pegar usuário por nome. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {

                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName);

                if(user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update<User>(user);

                if(await _userPersist.SaveChangesAsync())
                {

                    var userReturn = await _userPersist.GetUserByUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userReturn);

                }

                return null;

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UpdateUserRoleAsync(string userId, string newRole)
        {
            try
            {
                // Encontra o usuário pelo ID
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                // Verifica se a nova role existe, caso contrário, cria a role
                var roleExists = await _roleManager.RoleExistsAsync(newRole);
                if (!roleExists)
                {
                    var createRoleResult = await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid().ToString(), Name = newRole});
                    if (!createRoleResult.Succeeded)
                    {
                        throw new Exception("Erro ao criar a nova role.");
                    }
                }

                // Obtém as roles atuais do usuário
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Remove as roles atuais
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                
                if (!removeResult.Succeeded)
                {
                    throw new Exception("Erro ao remover as roles atuais do usuário.");
                }

                // // Verifica se a nova role existe
                // var roleExists = await _userManager.IsInRoleAsync(user, newRole);
                // if (!roleExists)
                // {
                //     throw new Exception("A role especificada não existe.");
                // }

                // Adiciona a nova role ao usuário
                var addResult = await _userManager.AddToRoleAsync(user, newRole);
                if (!addResult.Succeeded)
                {
                    throw new Exception("Erro ao adicionar a nova role ao usuário.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar a role do usuário. Erro: {ex.Message}");
            }
        }


        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users
                                         .AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar se o usuário existe. Erro: {ex.Message}");
            }
        }
    }
}