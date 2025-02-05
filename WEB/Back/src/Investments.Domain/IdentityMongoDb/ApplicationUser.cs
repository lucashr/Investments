using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using Investments.Domain.Enum;
using Investments.Domain.Identity;
using MongoDbGenericRepository.Attributes;

namespace Investments.Domain
{
    [CollectionName("Users")] // Nome da coleção no MongoDB
    public class ApplicationUser : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Function Function { get; set; }
        public IEnumerable<ApplicationUserRole> UserRoles { get; set; }
        public UserAddress EnderecoUsuario { get; set; }
    }
}