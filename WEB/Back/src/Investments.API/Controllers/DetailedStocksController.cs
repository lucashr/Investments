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
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DetailedStocksController : ControllerBase
    {
        private readonly IDetailedStockService _detailedStockService;

        public DetailedStocksController(IDetailedStockService detailedFundService)
        {
            _detailedStockService = detailedFundService;
        }
        
        [HttpGet("GetAllStocks")]
        public async Task<IActionResult> GetAllStocks()
        {

            try
            {
                var allFunds = await _detailedStockService.GetAllDetailedStocksAsync();

                return Ok(allFunds);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar fundos. Erro: {ex.Message}");
            }
            
        }

    }
}