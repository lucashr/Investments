
namespace Investments.Test.Domain.Models
{
    public class DetailedFunds
    {
        public string FundCode { get; set; }
        public string Segment { get; set; }
        public double Quotation { get; set; }
        public double FFOYield { get; set; }
        public double DividendYield { get; set; }
        public double PriceEquityValue { get; set; }
        public double ValueOfMarket { get; set; }
        public double Liquidity { get; set; }
        public double NumberOfProperties { get; set; }
        public double SquareMeterPrice { get; set; }
        public double RentPerSquareMeter { get; set; }
        public double CapRate { get; set; }
        public double AverageVacancy { get; set; }

    }
}