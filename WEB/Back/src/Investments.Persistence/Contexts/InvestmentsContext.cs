using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence.Contexts
{
    public class InvestmentsContext : IdentityDbContext<User, Role, string, 
                                                        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, 
                                                        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        
        public InvestmentsContext()
        {
        }

        public InvestmentsContext(DbContextOptions<InvestmentsContext> options): base(options){
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

            public virtual DbSet<DetailedFunds> DetailedFunds { get; set; }
            public virtual DbSet<Funds> Funds { get; set; }
            public virtual DbSet<FundsYeld> FundsYeld { get; set; }
            public virtual DbSet<RankOfTheBestFunds> RankFunds { get; set; }
            public virtual DbSet<EnderecoUsuario> EnderecoUsuarios { get; set; }

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

                modelBuilder.Entity<User>()
                            .HasOne(u => u.EnderecoUsuario)
                            .WithOne(e => e.User)
                            .HasForeignKey<EnderecoUsuario>(e => e.UserId)
                            .OnDelete(DeleteBehavior.Cascade);

            }
            
    }
}