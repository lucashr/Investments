using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Application.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string cep { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }
}