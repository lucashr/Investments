using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Investments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FundRegistrationController : ControllerBase
    {

        private readonly IFundsService _fundsService;
        
        public FundRegistrationController(IFundsService fundsService)
        {
            _fundsService = fundsService;
        }

        [HttpPost("FundRegistration")]
        public async Task<IActionResult> FundRegistration(string fundCode)
        {

            try
            {
                var funds = await _fundsService.AddFundAsync(fundCode);

                if(funds == null)
                {
                    return Ok("Fundo já cadastrado na base de dados");
                }

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpPost("FundsRegistration")]
        public async Task<IActionResult> FundsRegistration(List<string> fundCodes)
        {

            try
            {

                var detailedFunds = new List<DetailedFunds>();

                foreach (string fund in fundCodes)
                {
                    detailedFunds.Add(new DetailedFunds(){FundCode = fund});
                }

                var funds = await _fundsService.AddFundsAsync(detailedFunds);

                if(funds)
                    return Ok("Operação efetuada com sucesso!");

                return Ok("Falha ao tentar executar operação!");
                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpDelete("DeleteFundByCode/{fundCode}")]
        public async Task<IActionResult> DeleteFundByCode(string fundCode)
        {

            try
            {
                var funds = await _fundsService.DeleteFundByCodeAsync(fundCode);

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpPut("UpdateFundByCode/{oldFundCode}/{newFundCode}")]
        public async Task<IActionResult> UpdateFundByCode(string oldFundCode, string newFundCode)
        {

            try
            {
                var funds = await _fundsService.UpdateFundByCodeAsync(oldFundCode, newFundCode);

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpGet("GetFundbyCode/{fundCode}")]
        public async Task<IActionResult> GetFundbyCode(string fundCode)
        {

            try
            {
                var funds = await _fundsService.GetFundByCodeAsync(fundCode);

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpGet("AllFunds")]
        public async Task<IActionResult> ListAllFunds()
        {

            try
            {
                var funds = await _fundsService.GetAllFundsAsync();
                var result = JsonConvert.SerializeObject(funds);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

    }
}