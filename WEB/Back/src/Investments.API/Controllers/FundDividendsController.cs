using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FundDividendsController : ControllerBase
    {

        private readonly IFundDividendsService _fundDividendsService;
        
        public FundDividendsController(IFundDividendsService fundDividendsService)
        {
            _fundDividendsService = fundDividendsService;
        }

        [HttpGet("GetFundDividendsByCode/{fundCode}")]
        public async Task<IActionResult> GetFundDividendsByCode(string fundCode)
        {
            var fund = await _fundDividendsService.GetFundDividendsByCodeAsync(fundCode);
            return fund.Any() ? Ok(fund) : NotFound("No fund dividends found");
        }

        [HttpGet("GetAllFundsDividends")]
        public async Task<IActionResult> GetAllFundsDividends()
        {
            var dividends = await _fundDividendsService.GetAllFundsDividendsAsync();
            return dividends.Any() ? Ok(dividends) : NotFound("No funds dividends found");
        }

    }
}