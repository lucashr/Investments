using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application;
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
    public class WebScrapingStocksAndDividendsController : ControllerBase
    {
        private readonly IWebScrapingStocksAndDividendsService _webScrapingStocksAndDividendsService;
        private readonly RankOfTheBestStocksService _rankOfTheBestStocksService;
        private readonly WebScrapingSocketManager _socketManager;
        private readonly IDetailedStockService _detailedStockService;
        private readonly IStocksDividendService _stocksDividendService;
        private static CancellationTokenSource? _cancellationTokenSource;
        private static bool _isRunning = false;

        public WebScrapingStocksAndDividendsController(IWebScrapingStocksAndDividendsService webScrapingStocksAndDividendsService,
                                                       RankOfTheBestStocksService rankOfTheBestStocksService,
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
            try
            {
                
                _isRunning = true;
                _cancellationTokenSource = new CancellationTokenSource();

                _socketManager.GetAll();

                var result = await _webScrapingStocksAndDividendsService.GetStocksAsync(_cancellationTokenSource);

                if(result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }

                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpGet("GetStockDividends")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> GetStockDividendsAsync()
        {

            try
            {

                _socketManager.GetAll();

                if (_cancellationTokenSource.IsCancellationRequested)
                    return Ok();

                var detailedFunds = await _detailedStockService.GetAllDetailedStocksAsync();

                var fundYelds = await _webScrapingStocksAndDividendsService.GetStocksDividendsAsync(detailedFunds, _cancellationTokenSource);
                
                if(fundYelds.Count() > 0)
                {

                    var rankingOfTheBestFunds = await _rankOfTheBestStocksService.GetRankOfTheBestStocksAsync();
                    await _rankOfTheBestStocksService.AddRankOfTheBestStocksAsync(rankingOfTheBestFunds);
                    
                    return Ok(fundYelds);

                }
                else
                {
                    return Ok(fundYelds);
                }
                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter fundos. Erro: {ex.Message}");
            }
            
        }

        [HttpGet("StopWebScraping")]
        [Authorize(policy: "Admin")]
        public IActionResult Pause()
        {
            if (!_isRunning)
            {
                // return BadRequest("No process is running.");
                return Ok("No process is running.");
            }

            _cancellationTokenSource?.Cancel();
            return Ok("Process paused.");
        }
        
    }
}