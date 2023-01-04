using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Domain.Models
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