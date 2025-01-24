using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BestStocksRankController : ControllerBase
    {
        
        private readonly IRankOfTheBestStocksService _rankOfTheBestStocksService;

        public BestStocksRankController(IRankOfTheBestStocksService rankOfTheBestStocksService)
        {
            _rankOfTheBestStocksService = rankOfTheBestStocksService;
        }

        [HttpGet("GetRank/{quantity}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetRank(int quantity = 0)
        {
            var rank = await _rankOfTheBestStocksService.GetRankOfTheBestStocksAsync(quantity);
            return rank.Any() ? Ok(rank) : NotFound("No rank found");
        }

    }
}