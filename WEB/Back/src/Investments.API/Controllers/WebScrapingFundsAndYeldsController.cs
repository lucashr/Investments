using System.Collections.Generic;
using System.Linq;
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

                _socketManager.GetAll();
                
                var result = await _webScrapingFundsAndYelds.GetFundsAsync();
                bool storageWentOK = await _detailedFundService.AddDetailedFundsAsync(result);


                if(storageWentOK)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(storageWentOK);
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

                var detailedFunds = await _detailedFundService.GetAllDetailedFundsAsync();
                
                var fundYelds = await _webScrapingFundsAndYelds.GetYeldsFundsAsync(detailedFunds);
                
                bool storageWentOK = await _fundsYieldService.AddFundsYieldsAsync(fundYelds);

                if(storageWentOK)
                {
                    var rankingOfTheBestFunds = await _rankOfTheBestFundsService.GetCalculationRankOfTheBestFundsAsync();
                    await _rankOfTheBestFundsService.AddRankOfTheBestFundsAsync(rankingOfTheBestFunds);
                    return Ok(fundYelds);
                }
                else
                {
                    return Ok(storageWentOK);
                }
                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter fundos. Erro: {ex.Message}");
            }
            
        }
        
    }
}