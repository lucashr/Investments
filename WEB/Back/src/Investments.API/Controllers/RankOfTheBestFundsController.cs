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
    public class RankOfTheBestFundsController : ControllerBase
    {
        
        private readonly IRankOfTheBestFundsService _rankOfTheBestFundsService;

        public RankOfTheBestFundsController(IRankOfTheBestFundsService rankOfTheBestFundsService)
        {
            _rankOfTheBestFundsService = rankOfTheBestFundsService;
        }

        [HttpGet("GetRank/{quantity}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetRank(int? quantity = null)
        {
            var rank = await _rankOfTheBestFundsService.GetRankOfTheBestFundsAsync(quantity);
            return rank.Any() ? Ok(rank) : NotFound("No rank found");
        }

    }
}