using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Investments.Domain.Identity
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Function Function { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public UserAddress EnderecoUsuario { get; set; }

    }
}