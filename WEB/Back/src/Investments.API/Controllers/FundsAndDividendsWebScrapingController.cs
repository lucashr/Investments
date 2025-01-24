using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FundsAndDividendsWebScrapingController : ControllerBase
    {
        private readonly IWebScrapingFundsAndDividendsService _webScrapingFundsAndDividends;
        private readonly IRankOfTheBestFundsService _rankOfTheBestFundsService;
        private readonly WebScrapingSocketManager _socketManager;
        private readonly IDetailedFundService _detailedFundService;
        private static CancellationTokenSource? _cancellationTokenSource;
        private static bool _isRunning = false;

        public FundsAndDividendsWebScrapingController(IWebScrapingFundsAndDividendsService webScrapingFundsAndDividends,
                                                  IRankOfTheBestFundsService rankOfTheBestFundsService,
                                                  WebScrapingSocketManager socketManager,
                                                  IDetailedFundService detailedFundService)
        {
            _webScrapingFundsAndDividends = webScrapingFundsAndDividends;
            _rankOfTheBestFundsService = rankOfTheBestFundsService;
            _socketManager = socketManager;
            _detailedFundService = detailedFundService;
   
        }

        [HttpGet("GetFunds")]
        [Authorize(policy: "AdminOrUser")]
        public async Task<IActionResult> GetFundsAsync()
        {
            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();
  
            _socketManager.GetAll();

            var result = await _webScrapingFundsAndDividends.GetFundsAsync(_cancellationTokenSource);

            return result.Count() > 0 ? Ok(result) : NotFound("No funds found.");

        }

        [HttpGet("GetFundDividends")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetFundDividendsAsync()
        {
            _socketManager.GetAll();
            _cancellationTokenSource = new CancellationTokenSource();

            if (_cancellationTokenSource.IsCancellationRequested)
                return Ok();

            var detailedFunds = await _detailedFundService.GetAllDetailedFundsAsync();

            var dividends = await _webScrapingFundsAndDividends.GetFundDividendsAsync(detailedFunds, _cancellationTokenSource);
            
            if(dividends.Count() > 0)
            {
                var rankingOfTheBestFunds = await _rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
                await _rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankingOfTheBestFunds);
                return Ok(dividends);
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
            return Ok("Process stopped.");
        }
        
    }
}