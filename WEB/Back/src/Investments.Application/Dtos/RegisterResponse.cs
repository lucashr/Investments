using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Application.Dtos
{
    public record RegisterResponse 
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
    }
}