using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FundDividendsController : ControllerBase
    {

        private readonly IFundDividendsService _fundsYeldService;
        
        public FundDividendsController(IFundDividendsService fundsYeldService)
        {
            _fundsYeldService = fundsYeldService;
        }

        [HttpGet("GetFundDividendsByCode/{fundCode}")]
        public async Task<IActionResult> GetFundDividendsByCode(string fundCode)
        {

            try
            {
                var funds = await _fundsYeldService.GetFundYeldByCodeAsync(fundCode);

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpGet("GetAllFundsDividends")]
        public async Task<IActionResult> GetAllFundsDividends()
        {

            try
            {
                var funds = await _fundsYeldService.GetAllFundsYeldAsync();

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