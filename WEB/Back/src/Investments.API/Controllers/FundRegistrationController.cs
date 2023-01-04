using System.Threading.Tasks;
using Investments.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("FundRegistration")]
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

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

    }
}