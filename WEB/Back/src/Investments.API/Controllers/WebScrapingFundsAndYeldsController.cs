using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebScrapingFundsAndYeldsController : ControllerBase
    {
        private readonly IWebScrapingFundsAndYeldsService _webScrapingFundsAndYelds;
        private readonly IRankOfTheBestFundsService _rankOfTheBestFundsService;
        private readonly WebScrapingSocketManager _socketManager;
        private readonly IDetailedFundService _detailedFundService;
        private readonly IFundsService _fundsService;
        private readonly IFundsYieldService _fundsYieldService;
        private static CancellationTokenSource? _cancellationTokenSource;
        private static bool _isRunning = false;

        public WebScrapingFundsAndYeldsController(IWebScrapingFundsAndYeldsService webScrapingFundsAndYelds,
                                                  IRankOfTheBestFundsService rankOfTheBestFundsService,
                                                  WebScrapingSocketManager socketManager,
                                                  IDetailedFundService detailedFundService,
                                                  IFundsService fundsService,
                                                  IFundsYieldService fundsYieldService)
        {
            _webScrapingFundsAndYelds = webScrapingFundsAndYelds;
            _rankOfTheBestFundsService = rankOfTheBestFundsService;
            _socketManager = socketManager;
            _detailedFundService = detailedFundService;
            _fundsService = fundsService;
            _fundsYieldService = fundsYieldService;
        }

        [HttpGet("Funds")]
        public async Task<IActionResult> GetFundsAsync()
        {
            try
            {
                
                _isRunning = true;
                _cancellationTokenSource = new CancellationTokenSource();

                _socketManager.GetAll();

                var result = await _webScrapingFundsAndYelds.GetFundsAsync(_cancellationTokenSource);

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

        [HttpGet("Yelds")]
        public async Task<IActionResult> GetYeldsFundsAsync()
        {

            try
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