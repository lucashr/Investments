using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Domain.Models
{
    public class FundDividends
    {
        public string Id { get; set; }
        public string FundCode { get; set; }
        public string LastComputedDate { get; set; }
        public double Value { get; set; }
        public string DatePayment { get; set; }
        public string Type { get; set; }

    }
}