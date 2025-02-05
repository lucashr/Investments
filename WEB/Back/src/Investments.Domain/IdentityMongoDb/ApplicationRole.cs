using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using Investments.Domain.Identity;

namespace Investments.Domain
{
    [CollectionName("Roles")] // Nome da coleção no MongoDB
    
    public class ApplicationRole : MongoIdentityRole<string>
    {
        public IEnumerable<ApplicationUserRole> UserRoles { get; set; }
    }
}