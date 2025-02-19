using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Domain.Models
{
    public class DetailedFund
    {

        public string Id { get; set; }
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

        public override bool Equals(object obj)
        {
            return obj is DetailedFund fund &&
                   FundCode == fund.FundCode &&
                   Segment == fund.Segment &&
                   Quotation == fund.Quotation &&
                   FFOYield == fund.FFOYield &&
                   DividendYield == fund.DividendYield &&
                   PriceEquityValue == fund.PriceEquityValue &&
                   ValueOfMarket == fund.ValueOfMarket &&
                   Liquidity == fund.Liquidity &&
                   NumberOfProperties == fund.NumberOfProperties &&
                   SquareMeterPrice == fund.SquareMeterPrice &&
                   RentPerSquareMeter == fund.RentPerSquareMeter &&
                   CapRate == fund.CapRate &&
                   AverageVacancy == fund.AverageVacancy;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(FundCode);
            hash.Add(Segment);
            hash.Add(Quotation);
            hash.Add(FFOYield);
            hash.Add(DividendYield);
            hash.Add(PriceEquityValue);
            hash.Add(ValueOfMarket);
            hash.Add(Liquidity);
            hash.Add(NumberOfProperties);
            hash.Add(SquareMeterPrice);
            hash.Add(RentPerSquareMeter);
            hash.Add(CapRate);
            hash.Add(AverageVacancy);
            return hash.ToHashCode();
        }
    }
}