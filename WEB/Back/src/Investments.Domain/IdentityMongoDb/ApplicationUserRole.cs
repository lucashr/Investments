using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace Investments.Domain
{
    public class ApplicationUserRole
    {
        public string Id { get; set; }
        public string UserId { get; set; }  // ID do usuário
        public string RoleId { get; set; }  // ID da role
        //public ApplicationUser User { get; set; }
        //public ApplicationRole Role { get; set; }
    }
}