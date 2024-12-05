using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Investments.Domain.Identity
{
    public class Role : IdentityRole<string>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}