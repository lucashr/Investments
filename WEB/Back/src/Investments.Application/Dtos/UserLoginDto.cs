using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Application.Dtos
{
    public record UserLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}