using Newtonsoft.Json;

namespace Investments.Test.Domain.stocks
{
    public partial class TimeSeriesDaily
    {
        // [BindProperty(Name ="1. open")]
        [JsonProperty("1. open")]
        public string Open { get; set; }
        // [BindProperty(Name ="2. high")]
         [JsonProperty("2. high")]
        public string High { get; set; }
        // [BindProperty(Name ="3. low")]
         [JsonProperty("3. low")]
        public string Low { get; set; }
        // [BindProperty(Name ="4. close")]
         [JsonProperty("4. close")]
        public string Close { get; set; }
        // [BindProperty(Name ="5. volume")]
         [JsonProperty("5. volume")]
        public long Volume { get; set; }
    }
}