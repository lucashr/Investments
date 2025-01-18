using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WebScrapingStocksAndDividendsController : ControllerBase
    {
        private readonly IWebScrapingStocksAndDividendsService _webScrapingStocksAndDividendsService;
        private readonly IRankOfTheBestStocksService _rankOfTheBestStocksService;
        private readonly WebScrapingSocketManager _socketManager;
        private readonly IDetailedStockService _detailedStockService;
        private readonly IStocksDividendService _stocksDividendService;
        private static CancellationTokenSource? _cancellationTokenSource;
        private static bool _isRunning = false;

        public WebScrapingStocksAndDividendsController(IWebScrapingStocksAndDividendsService webScrapingStocksAndDividendsService,
                                                       IRankOfTheBestStocksService rankOfTheBestStocksService,
                                                       WebScrapingSocketManager socketManager,
                                                       IDetailedStockService detailedStockService,
                                                       IStocksDividendService stocksDividendService)
        {
            _webScrapingStocksAndDividendsService = webScrapingStocksAndDividendsService;
            _rankOfTheBestStocksService = rankOfTheBestStocksService;
            _socketManager = socketManager;
            _detailedStockService = detailedStockService;
            _stocksDividendService = stocksDividendService;
        }

        [HttpGet("GetStocks")]
        [Authorize(policy: "AdminOrUser")]
        public async Task<IActionResult> GetStocksAsync()
        {
            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();

            _socketManager.GetAll();

            var result = await _webScrapingStocksAndDividendsService.GetStocksAsync(_cancellationTokenSource);

            return result.Count() > 0 ? Ok(result) : NotFound("No stocks found.");

        }

        [HttpGet("GetStockDividends")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetStockDividendsAsync()
        {
            _socketManager.GetAll();
            _cancellationTokenSource = new CancellationTokenSource();

            if (_cancellationTokenSource.IsCancellationRequested)
                return Ok();

            var detailedFunds = await _detailedStockService.GetAllDetailedStocksAsync();

            var fundDividends = await _webScrapingStocksAndDividendsService.GetStocksDividendsAsync(detailedFunds, _cancellationTokenSource);
            
            if(fundDividends.Count() > 0)
            {

                var rankingOfTheBestFunds = await _rankOfTheBestStocksService.GetRankOfTheBestStocksAsync();
                await _rankOfTheBestStocksService.AddRankOfTheBestStocksAsync(rankingOfTheBestFunds);
                
                return Ok(fundDividends);

            }
            else
                return NotFound("No stocks dividends found.");

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