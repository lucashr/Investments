using System;

namespace Investments.Test.Domain.Models
{
    public class FundsYeld
    {
        public int Id { get; set; }
        public string FundCode { get; set; }
        public DateTime LastComputedDate { get; set; }
        public double Value { get; set; }
        public DateTime DatePayment { get; set; }
        public string Type { get; set; }

    }
}