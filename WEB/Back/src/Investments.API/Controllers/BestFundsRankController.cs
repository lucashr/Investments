using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.API.Extensions;
using Investments.Application.Contracts;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BestFundsRankController : ControllerBase
    {
        
        private readonly IRankOfTheBestFundsService _rankOfTheBestFundsService;

        public BestFundsRankController(IRankOfTheBestFundsService rankOfTheBestFundsService)
        {
            _rankOfTheBestFundsService = rankOfTheBestFundsService;
        }

        [HttpGet("GetRank")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetRank([FromQuery] int quantity = 0)
        {
            IEnumerable<BestFundRank> rank; rank = await _rankOfTheBestFundsService.GetRankOfTheBestFundsAsync(quantity > 0 ? quantity : null);  
            return rank.Any() ? Ok(rank) : NotFound("No rank found");
        }

    }
}