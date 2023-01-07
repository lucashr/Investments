using System;
using System.Collections.Generic;

namespace Investments.Test.Application.Dtos
{
    public partial class StocksDto
    {
        // public StocksDto()
        // {
        // }

        // public StocksDto(MetaData metaData, Dictionary<string, TimeSeriesDailyDto> timeSeriesDaily)
        // {
        //     MetaData = metaData;
        //     TimeSeriesDaily = timeSeriesDaily;
        // }

        // [BindProperty(Name ="Meta Data")]
        // [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }
        // [BindProperty(Name ="Time Series (Daily)")]
        // [JsonProperty("Time Series (Daily)")]
        // public  List<TimeSeriesDailyDto> TimeSeriesDaily { get; set; }
        public  Dictionary<string, TimeSeriesDailyDto> TimeSeriesDaily { get; set; }
        
    }

    public partial class MetaData
    {
        // public MetaData()
        // {
        // }

        // public MetaData(string information, string symbol, DateTimeOffset lastRefreshed, string outputSize, string timeZone)
        // {
        //     Information = information;
        //     Symbol = symbol;
        //     LastRefreshed = lastRefreshed;
        //     OutputSize = outputSize;
        //     TimeZone = timeZone;
        // }

        // [BindProperty(Name ="1. Information")]
        // [JsonProperty("1. Information")]
        public string Information { get; set; }
        // [BindProperty(Name ="2. Symbol")]
        //  [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }
        // [BindProperty(Name ="3. Last Refreshed")]
        //  [JsonProperty("3. Last Refreshed")]
        public DateTimeOffset LastRefreshed { get; set; }
        // [BindProperty(Name ="4. Output Size")]
        //  [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; }
        // [BindProperty(Name ="5. Time Zone")]
        //  [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }
    }

    public partial class TimeSeriesDailyDto
    {
        // public TimeSeriesDailyDto()
        // {
        // }

        // public TimeSeriesDailyDto(string open, string high, string low, string close, long volume)
        // {
        //     Open = open;
        //     High = high;
        //     Low = low;
        //     Close = close;
        //     Volume = volume;
        // }

        // [BindProperty(Name ="1. open")]
        //  [JsonProperty("1. open")]
        public string Open { get; set; }
        // [BindProperty(Name ="2. high")]
        //  [JsonProperty("2. high")]
        public string High { get; set; }
        // [BindProperty(Name ="3. low")]
        //  [JsonProperty("3. low")]
        public string Low { get; set; }
        // [BindProperty(Name ="4. close")]
        //  [JsonProperty("4. close")]
        public string Close { get; set; }
        // [BindProperty(Name ="5. volume")]
        //  [JsonProperty("5. volume")]
        public long Volume { get; set; }
    }

}
