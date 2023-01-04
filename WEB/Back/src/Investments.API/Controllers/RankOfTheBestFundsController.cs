using System.Threading.Tasks;
using Investments.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankOfTheBestFundsController : ControllerBase
    {
        private readonly IRankOfTheBestFundsService _rankOfTheBestFundsService;

        public RankOfTheBestFundsController(IRankOfTheBestFundsService rankOfTheBestFundsService)
        {
            _rankOfTheBestFundsService = rankOfTheBestFundsService;
        }

        [HttpGet("ListBestFunds/{totalFundsInRank}")]
        public async Task<IActionResult> ListBestFunds(int? totalFundsInRank = null)
        {

            try
            {
                var allFunds = await _rankOfTheBestFundsService.GetRankOfTheBestFundsAsync(totalFundsInRank);

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