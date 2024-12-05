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
    [Route("api/[controller]")]
    public class FundYeldsController : ControllerBase
    {

        private readonly IFundsYieldService _fundsYeldService;
        
        public FundYeldsController(IFundsYieldService fundsYeldService)
        {
            _fundsYeldService = fundsYeldService;
        }

        [HttpGet("{fundCode}")]
        public async Task<IActionResult> GetFundsYeldByCode(string fundCode)
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

        [HttpGet("AllFundsYeld")]
        public async Task<IActionResult> GetAllFundsYeld()
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

        [HttpPost("Registration")]
        public async Task<IActionResult> AddFundsYields(IEnumerable<FundsYeld> fundsYelds)
        {

            try
            {
                var funds = await _fundsYeldService.AddFundsYieldsAsync(fundsYelds);

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