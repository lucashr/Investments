using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimentos.VariablesManager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WEbSocketTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        private readonly WebScrapingSocketManager _socketManager;

        public ApiController(WebScrapingSocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        [HttpGet("report")]
        public async Task Report(double liquidTemp)
        {
            var reading = new
            {
                Date = DateTime.Now,
                LiquidTemp = liquidTemp
            };

            await _socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(reading));
        }

        // [HttpGet("Generate")]
        // public async Task Generate()
        // {
        //     var rnd = new Random();

        //     for(var i = 0; i < 100; i++)
        //     {                
        //         await Report(rnd.Next(23, 35));
        //         await Task.Delay(5000);
        //     }
        // }
    }
}