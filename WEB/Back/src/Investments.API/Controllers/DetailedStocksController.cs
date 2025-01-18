using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
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