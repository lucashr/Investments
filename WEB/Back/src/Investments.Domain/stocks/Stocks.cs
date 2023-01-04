using System.Collections.Generic;
using Investments.Domain.stocks;
using Newtonsoft.Json;

namespace Investments.Domain.Models
{
    public partial class Stocks
    {
        // [BindProperty(Name ="Meta Data")]
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }
        // [BindProperty(Name ="Time Series (Daily)")]
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, TimeSeriesDaily> TimeSeriesDaily { get; set; }
        // public List<TimeSeriesDaily> TimeSeriesDaily { get; set; }
    }

}