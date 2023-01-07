using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Test.Application.Contracts;
using Investments.Test.Application.Dtos;
using Investments.Test.Domain.Models;
using Newtonsoft.Json;

namespace Investments.Test.Application
{
    public class StocksService : IStocksService
    {

        private IMapper _mapper;

        public StocksService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<StocksDto> Get(string name)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={name}.SA&apikey=QXCJRQTC6J5C74GS";
            // string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);
            Stocks stocks;
            StocksDto stocksDto = new StocksDto();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = queryUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await client.GetAsync(QUERY_URL);
                string data = await response.Content.ReadAsStringAsync();

                 stocks = JsonConvert.DeserializeObject<Stocks>(data);
               
               try
               {
                   var kkkk =  _mapper.Map<StocksDto>(stocks);
               }
               catch (System.Exception ex)
               {
                    Console.Write(ex.Message);
               }
            }

            return stocksDto;
        }
    }
}