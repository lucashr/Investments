using System;
using Newtonsoft.Json;

namespace Investments.Domain.stocks
{
    public partial class MetaData
    {

        // [BindProperty(Name ="1. Information")]
        [JsonProperty("1. Information")]
        public string Information { get; set; }
        // [BindProperty(Name ="2. Symbol")]
         [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }
        // [BindProperty(Name ="3. Last Refreshed")]
         [JsonProperty("3. Last Refreshed")]
        public DateTimeOffset LastRefreshed { get; set; }
        // [BindProperty(Name ="4. Output Size")]
         [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; }
        // [BindProperty(Name ="5. Time Zone")]
         [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }
    }
}