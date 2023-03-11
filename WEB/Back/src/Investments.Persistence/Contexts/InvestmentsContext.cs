using Investments.Domain.Identity;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence.Contexts
{
    public class InvestmentsContext : IdentityDbContext<User, Role, int, 
                                                        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public InvestmentsContext(DbContextOptions<InvestmentsContext> options): base(options){
            ChangeTracker.QueryTrackingBehavior =
            QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

            public DbSet<DetailedFunds> DetailedFunds { get; set; }
            public DbSet<Funds> Funds { get; set; }
            public DbSet<FundsYeld> FundsYeld { get; set; }
            public DbSet<RankOfTheBestFunds> RankFunds { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<UserRole>(userRole => {
                    
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                    userRole.HasOne(ur => ur.Role)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.RoleId)
                            .IsRequired();
                    
                    userRole.HasOne(ur => ur.User)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.UserId)
                            .IsRequired();

                });

                modelBuilder.Entity<DetailedFunds>(
                    funds => funds.HasKey(x => x.FundCode)
                );

                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Funds>( funds => {
                    funds.HasKey(x => x.Id);
                    funds.Property(x => x.Id).ValueGeneratedOnAdd();
                });

                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<FundsYeld>(funds => {
                     funds.HasKey(x => x.Id);
                     funds.Property(x => x.Id).ValueGeneratedOnAdd();
                });

                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<RankOfTheBestFunds>(Fund => {
                    Fund.HasKey(x=> x.FundCode);
                });

            }
            
    }
}