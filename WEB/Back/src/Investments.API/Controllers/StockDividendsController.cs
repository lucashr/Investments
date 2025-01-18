using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StockDividendsController : ControllerBase
    {

        private readonly IStocksDividendService _stocksDividendsService;
        
        public StockDividendsController(IStocksDividendService stockDividendsService)
        {
            _stocksDividendsService = stockDividendsService;
        }

        [HttpGet("GetstockDividendsByCode/{stockCode}")]
        public async Task<IActionResult> GetStockDividendsByCode(string StockCode)
        {
            var dividends = await _stocksDividendsService.GetStockDividendsByCodeAsync(StockCode);
            return dividends.Any() ? Ok(dividends) : NotFound("No stock dividends found");
        }

        [HttpGet("GetAllStockDividends")]
        public async Task<IActionResult> GetAllStockDividends()
        {
            var dividends = await _stocksDividendsService.GetAllStocksDividendsAsync();
            return dividends.Any() ? Ok(dividends) : NotFound("No stock dividends found");
        }

    }
}