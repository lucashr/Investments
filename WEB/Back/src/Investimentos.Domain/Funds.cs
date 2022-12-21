using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Investimentos.Domain.Models
{
    public class Funds
    {
        public int Id { get; set; }
        public string FundCode { get; set; }
    }
}