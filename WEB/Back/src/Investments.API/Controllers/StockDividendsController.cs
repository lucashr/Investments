using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StockDividendsController : ControllerBase
    {

        private readonly IStocksDividendService _stocksYeldService;
        
        public StockDividendsController(IStocksDividendService fundsYeldService)
        {
            _stocksYeldService = fundsYeldService;
        }

        [HttpGet("GetFundDividendsByCode/{fundCode}")]
        public async Task<IActionResult> GetFundDividendsByCode(string fundCode)
        {

            try
            {
                var funds = await _stocksYeldService.GetStockYeldByCodeAsync(fundCode);

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
                var funds = await _stocksYeldService.GetAllStocksYeldAsync();

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