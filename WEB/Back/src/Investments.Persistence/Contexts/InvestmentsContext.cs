using System;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Domain.Models;
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

        public virtual DbSet<DetailedFund> DetailedFunds { get; set; }
        public virtual DbSet<FundDividend> FundDividends { get; set; }
        public virtual DbSet<BestFundRank> BestFundRanks { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<DetailedStock> DetailedStocks { get; set; }
        public virtual DbSet<StockDividend> StocksDividends { get; set; }
        public virtual DbSet<BestStockRank> BestStockRanks { get; set; }

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

                modelBuilder.Entity<DetailedFund>(
                    funds => funds.HasKey(x => x.Id)
                );

                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<FundDividend>(funds => {
                     funds.HasKey(x => x.Id);
                     funds.Property(x => x.Id).ValueGeneratedOnAdd();
                });

                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<BestFundRank>(Fund => {
                    Fund.HasKey(x=> x.Id);
                });

                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<BestStockRank>(Fund => {
                    Fund.HasKey(x=> x.Id);
                });

                base.OnModelCreating(modelBuilder);
                    modelBuilder.Entity<User>(o => {
                        o.HasOne(u => u.EnderecoUsuario)
                        .WithOne(e => e.User)
                        .HasForeignKey<UserAddress>(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                });


                string adminRoleId = Guid.NewGuid().ToString("D");
                string userRoleId = Guid.NewGuid().ToString("D");
                string adminUserId = Guid.NewGuid().ToString("D");

                // Inserir roles
                modelBuilder.Entity<Role>().HasData(
                    new Role { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                    new Role { Id = userRoleId, Name = "User", NormalizedName = "USER" }
                );

                // Inserir usuário com senha pré-hash
                var adminUser = new User
                {
                    Id = adminUserId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "admin"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                };

                modelBuilder.Entity<User>().HasData(adminUser);

                var userRole = new UserRole
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                };

                modelBuilder.Entity<UserRole>().HasData(userRole);

            }
            
    }
}