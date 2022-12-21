using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Investimentos.Application.Contracts;

namespace Investimentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stockService;
        
        public StocksController(IStocksService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("Stock")]
        public async Task<IActionResult> GetStockByCode(string name)
        {

            var stocks = await _stockService.Get(name);
            
            return Ok(stocks);

        }
        
    }
}