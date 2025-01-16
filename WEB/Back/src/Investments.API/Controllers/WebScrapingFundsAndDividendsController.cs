using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WebScrapingFundsAndDividendsController : ControllerBase
    {
        private readonly IWebScrapingFundsAndDividendsService _webScrapingFundsAndYelds;
        private readonly IRankOfTheBestFundsService _rankOfTheBestFundsService;
        private readonly WebScrapingSocketManager _socketManager;
        private readonly IDetailedFundService _detailedFundService;
        private readonly IFundDividendsService _fundsYieldService;
        private static CancellationTokenSource? _cancellationTokenSource;
        private static bool _isRunning = false;

        public WebScrapingFundsAndDividendsController(IWebScrapingFundsAndDividendsService webScrapingFundsAndYelds,
                                                  IRankOfTheBestFundsService rankOfTheBestFundsService,
                                                  WebScrapingSocketManager socketManager,
                                                  IDetailedFundService detailedFundService,
                                                  IFundDividendsService fundsYieldService)
        {
            _webScrapingFundsAndYelds = webScrapingFundsAndYelds;
            _rankOfTheBestFundsService = rankOfTheBestFundsService;
            _socketManager = socketManager;
            _detailedFundService = detailedFundService;
            _fundsYieldService = fundsYieldService;
        }

        [HttpGet("GetFunds")]
        [Authorize(policy: "AdminOrUser")]
        public async Task<IActionResult> GetFundsAsync()
        {
            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();

            _socketManager.GetAll();

            var result = await _webScrapingFundsAndYelds.GetFundsAsync(_cancellationTokenSource);

            return result.Count() > 0 ? Ok(result) : NotFound("No funds found.");

        }

        [HttpGet("GetFundDividends")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetFundDividendsAsync()
        {
            _socketManager.GetAll();

            if (_cancellationTokenSource.IsCancellationRequested)
                return Ok();

            var detailedFunds = await _detailedFundService.GetAllDetailedFundsAsync();

            var fundYelds = await _webScrapingFundsAndYelds.GetYeldsFundsAsync(detailedFunds, _cancellationTokenSource);
            
            if(fundYelds.Count() > 0)
            {
                var rankingOfTheBestFunds = await _rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
                await _rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankingOfTheBestFunds);
                return Ok(fundYelds);
            }
            else
                return NotFound("No funds dividends found.");

        }

        [HttpGet("StopWebScraping")]
        [Authorize(policy: "Admin")]
        public IActionResult Pause()
        {
            if (!_isRunning)
                return Ok("No process is running.");

            _cancellationTokenSource?.Cancel();
            return Ok("Process paused.");
        }
        
    }
}