using System.Collections;
using System.Collections.Generic;
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
    public class DetailedFundsController : ControllerBase
    {
        private readonly IDetailedFundService _detailedFundService;

        public DetailedFundsController(IDetailedFundService detailedFundService)
        {
            _detailedFundService = detailedFundService;
        }

        [HttpGet("AllFunds")]
        public async Task<IActionResult> GetAllFunds()
        {

            try
            {
                var allFunds = await _detailedFundService.GetAllDetailedFundsAsync();

                return Ok(allFunds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpGet("{fundCode}")]
        public async Task<IActionResult> GetFundsByCode(string fundCode)
        {

            try
            {
                var funds = await _detailedFundService.GetDetailedFundByCodeAsync(fundCode);

                return Ok(funds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> AddDetailedFunds(IEnumerable<DetailedFunds> detailedFunds)
        {

            try
            {
                var funds = await _detailedFundService.AddDetailedFundsAsync(detailedFunds);

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