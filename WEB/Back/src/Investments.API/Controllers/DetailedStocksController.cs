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
            var stocks = await _detailedStockService.GetAllDetailedStocksAsync();
            return stocks.Any() ? Ok(stocks) : NotFound("No stocks found");
        }

    }
}