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
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FundDividendsController : ControllerBase
    {

        private readonly IFundDividendsService _fundsYeldService;
        
        public FundDividendsController(IFundDividendsService fundsYeldService)
        {
            _fundsYeldService = fundsYeldService;
        }

        [HttpGet("GetFundDividendsByCode/{fundCode}")]
        public async Task<IActionResult> GetFundDividendsByCode(string fundCode)
        {
            var fund = await _fundsYeldService.GetFundYeldByCodeAsync(fundCode);
            return fund.Any() ? Ok(fund) : NotFound("No fund dividends found");
        }

        [HttpGet("GetAllFundsDividends")]
        public async Task<IActionResult> GetAllFundsDividends()
        {
            var dividends = await _fundsYeldService.GetAllFundsYeldAsync();
            return dividends.Any() ? Ok(dividends) : NotFound("No funds dividends found");
        }

    }
}