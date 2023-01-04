using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Domain.Models
{
    public class RankOfTheBestFunds
    {

        public string FundCode { get; set; }
        public string Segment { get; set; }
        public int MultiplierRanking { get; set; }
        public double CoefficientOfVariation { get; set; }
        // public double Cotacao { get; set; }
        public double FFOYield { get; set; }
        public double DividendYield { get; set; }
        public int DividendYieldRanking { get; set; }
        public double PriceEquityValue { get; set; }
        public int RankPrice { get; set; }
        public double ValueOfMarket { get; set; }
        public double Liquidity { get; set; }
        // public double QtdDeImoveis { get; set; }
        // public double PrecoDoM2 { get; set; }
        // public double AluguelPorM2 { get; set; }
        // public double CapRate { get; set; }
        public double AverageVacancy { get; set; }

    }
}