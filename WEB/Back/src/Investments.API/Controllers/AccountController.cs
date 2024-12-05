using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
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

            var usrDto = new UserDto{
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Username = model.Username,
                cep = model.cep,
                city = model.city,
                address = model.address,
                district = model.district,
                state = model.state
            };

            var result = await _accountService.CreateAccountAsync(usrDto);
            
            if(result.Username.IsNullOrEmpty())
                return BadRequest("Falha ao tentar criar uma conta!");

            var address = new EnderecoUsuario{
                    Id = Guid.NewGuid().ToString(),
                    UserId = usrDto.Id,
                    cep = model.cep,
                    city = model.city,
                    address = model.address,
                    district = model.district,
                    state = model.state
                };

            address.User = new(){
                Id = usrDto.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                Function = Domain.Enum.Function.Admin,
                EnderecoUsuario = address
            };

            bool bSaveAddressOk = await _enderecoUsuarioService.SaveAddressUser(address);

            if(bSaveAddressOk)
                return Ok();
            else
                return BadRequest("Erro ao tentar salvar o endereço do usuário!");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.Username);

                if(user == null) return Unauthorized("Usuário ou senha está inválido!");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);

                if(!result.Succeeded) return Unauthorized();

                return Ok(new 
                    {
                        username = user.Username,
                        PrimeiroNome = user.FirstName,
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