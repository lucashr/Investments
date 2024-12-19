using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Investments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IEnderecoUsuarioService _enderecoUsuarioService;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                 IEnderecoUsuarioService enderecoUsuarioService,
                                 ITokenService tokenService)
        {
            _accountService = accountService;
            _enderecoUsuarioService = enderecoUsuarioService;
            _tokenService = tokenService;
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([Required]string username, [Required]string role)
        {
            
            var user = await _accountService.GetUserByUserNameAsync(username);
            
            if(user == null)
                return BadRequest("Falha ao tentar obter usuario pelo username!");

            var result = await _accountService.UpdateUserRoleAsync(user.Id, role);

            if(user == null)
                return BadRequest("Falha ao tentar atualizar a role do usuario!");
            
            return Ok("Role atualizada com sucesso!");
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {

            var usrDto = new UserDto
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                ZipCode = model.ZipCode,
                City = model.City,
                Address = model.Address,
                District = model.District,
                State = model.State,
                Function = model.Function
            };

            var result = await _accountService.CreateAccountAsync(usrDto);

            if (string.IsNullOrEmpty(result.UserName))
                return BadRequest("Falha ao tentar criar uma conta!");

            var address = new EnderecoUsuario
            {
                Id = usrDto.Id,
                UserId = usrDto.Id,
                ZipCode = model.ZipCode,
                City = model.City,
                Address = model.Address,
                District = model.District,
                State = model.State
            };

            bool bSaveAddressOk = await _enderecoUsuarioService.SaveAddressUser(address);

            if (bSaveAddressOk == false)
                return BadRequest("Erro ao tentar salvar o endereço do usuário!");

            var user = await _accountService.GetUserByUserNameAsync(usrDto.UserName);

            return Ok(new
            {
                username = user.UserName,
                firstName = user.FirstName,
                token = _tokenService.CreateToken(user).Result
            }
            );

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.UserName);

                if(user == null) return Unauthorized("Usuário ou senha está inválido!");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);

                if(!result.Succeeded) return Unauthorized();

                return Ok(new 
                    {
                        username = user.UserName,
                        firstName = user.FirstName,
                        token = _tokenService.CreateToken(user).Result
                    }
                );
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar login. Erro: {ex.Message}");
            }
        }
    }

}