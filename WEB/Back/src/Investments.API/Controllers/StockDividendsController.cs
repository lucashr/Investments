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
            var dividends = await _stocksYeldService.GetStockYeldByCodeAsync(fundCode);
            return dividends.Any() ? Ok(dividends) : NotFound("No fund dividends found");
        }

        [HttpGet("GetAllFundsDividends")]
        public async Task<IActionResult> GetAllFundsDividends()
        {
            var dividends = await _stocksYeldService.GetAllStocksYeldAsync();
            return dividends.Any() ? Ok(dividends) : NotFound("No funds dividends found");
        }

    }
}