using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Domain.Models
{
    public class BestStockRank
    {
        public string Id { get; set; }
        public string FundCode { get; set; }
        public double Quotation { get; set; }
        public double PL { get; set; } //Psobre
        public double PVP { get; set; } //Psobre
        public double PSR { get; set; }
        public double DivYield { get; set; }
        public double PriceOverAsset { get; set; } //Psobre
        public double PriceOnWorkingCapital { get; set; } //Psobre
        public double PEBIT { get; set; } //Psobre
        public double PriceOverNetCurrentAssets { get; set; } //Psobre
        public double EVEBIT { get; set; } //EVsobre
        public double EVEBITDA { get; set; } //EVsobre
        public double EbitMargin { get; set; }
        public double LiquidityMargin { get; set; }
        public double LiquidityCurrent { get; set; }
        public double ROIC { get; set; }
        public double ROE { get; set; }
        public double LiquidityTwoMonths { get; set; }
        public double NetWorth { get; set; }
        public double GrossEquityDebt { get; set; } //DivBrutsobre
        public double RevenueGrowthFiveYears { get; set; }

    }
}