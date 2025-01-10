using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Investments.Domain
{
    public class StocksDividends
    {
        public string Id { get; set; }
        public string FundCode { get; set; }
        public string Date { get; set; }
        public double Value { get; set; }
        public string Type { get; set; }
        public string DatePayment { get; set; }
        public int ForHowManyShares { get; set; }
    }
}