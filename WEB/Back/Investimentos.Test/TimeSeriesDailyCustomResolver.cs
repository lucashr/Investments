using System.Collections.Generic;
using AutoMapper;
using Investimentos.Application.Dtos;
using Investimentos.Domain.Models;
using Investimentos.Domain.stocks;

namespace Investimentos.Test
{
    public class TimeSeriesDailyCustomResolver : IValueResolver<Stocks, StocksDto, Dictionary<string, TimeSeriesDailyDto>>
    {

        private readonly TimeSeriesDailyDto _stocks;
        public TimeSeriesDailyCustomResolver(TimeSeriesDailyDto stocks)
        {
            _stocks = stocks;
        }

        public Dictionary<string, TimeSeriesDailyDto> Resolve(Stocks source, StocksDto destination, Dictionary<string, TimeSeriesDailyDto> destMember, ResolutionContext context)
        {
            var result = new Dictionary<string, TimeSeriesDailyDto>();

            foreach (KeyValuePair<string, TimeSeriesDaily> item in source.TimeSeriesDaily)
            {
                var tmp = new TimeSeriesDailyDto()
                {
                    Close = source.TimeSeriesDaily[item.Key].Close,
                    High = source.TimeSeriesDaily[item.Key].High,
                    Low = source.TimeSeriesDaily[item.Key].High,
                    Open = source.TimeSeriesDaily[item.Key].Open,
                    Volume = source.TimeSeriesDaily[item.Key].Volume,
                };

                result.Add(item.Key, tmp);
            }


            return result;
        }
    }
}