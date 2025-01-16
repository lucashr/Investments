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
    public class DetailedFundsController : ControllerBase
    {
        private readonly IDetailedFundService _detailedFundService;

        public DetailedFundsController(IDetailedFundService detailedFundService)
        {
            _detailedFundService = detailedFundService;
        }
        
        [HttpGet("GetAllFunds")]
        public async Task<IActionResult> GetAllFunds()
        {
            var allFunds = await _detailedFundService.GetAllDetailedFundsAsync();
            return allFunds.Any() ? Ok(allFunds) : NotFound("No funds found");
        }

    }
}