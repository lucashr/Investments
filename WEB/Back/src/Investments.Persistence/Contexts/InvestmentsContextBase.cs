// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Investments.Domain.Identity;
// using Investments.Domain.Models;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

// namespace Investments.Persistence.Contexts
// {
//     public class InvestmentsContextBase : IdentityDbContext<User, Role, int, 
//                                                         IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
//                                                         IdentityRoleClaim<int>, IdentityUserToken<int>>
//     {
//         public InvestmentsContextBase(DbContextOptions options) : base(options)
//         {
//         }

//         public virtual DbSet<DetailedFunds> DetailedFunds { get; set; }
//         public virtual DbSet<Funds> Funds { get; set; }
//         public virtual DbSet<FundsYeld> FundsYeld { get; set; }
//         public virtual DbSet<RankOfTheBestFunds> RankFunds { get; set; }
        
//     }
// }