using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Identity;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Investments.Tests
{
    public class ContextTest : DbContext, IDbContext 
    {

        public virtual DbSet<DetailedFunds> DetailedFunds { get; set; }
        public virtual DbSet<Funds> Funds { get; set; }
        public virtual DbSet<FundsYeld> FundsYeld { get; set; }
        public virtual DbSet<RankOfTheBestFunds> RankFunds { get; set; }

        public ContextTest(DbContextOptions<ContextTest> options): base(options){
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        // public Task<int> SaveChangesAsync()
        // {
        //     throw new NotImplementedException();
        // }

        // public override DbSet<TEntity> Set<TEntity>() where TEntity : class
        // {
        //     Mock<DbSet<TEntity>> dbSetMock = new Mock<DbSet<TEntity>>();

        //     // dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(queryableList.Provider);
        //     // dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(queryableList.Expression);
        //     // dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
        //     // dbSetMock.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

        //     return dbSetMock.Object;
        // }



        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        //     {

        //         base.OnModelCreating(modelBuilder);

        //         modelBuilder.Entity<UserRole>(userRole => {

        //             userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

        //             userRole.HasOne(ur => ur.Role)
        //                     .WithMany(r => r.UserRoles)
        //                     .HasForeignKey(ur => ur.RoleId)
        //                     .IsRequired();

        //             userRole.HasOne(ur => ur.User)
        //                     .WithMany(r => r.UserRoles)
        //                     .HasForeignKey(ur => ur.UserId)
        //                     .IsRequired();

        //         });

        //         modelBuilder.Entity<DetailedFunds>(
        //             funds => funds.HasKey(x => x.FundCode)
        //         );

        //         base.OnModelCreating(modelBuilder);
        //         modelBuilder.Entity<Funds>( funds => {
        //             funds.HasKey(x => x.Id);
        //             funds.Property(x => x.Id).ValueGeneratedOnAdd();
        //         });

        //         base.OnModelCreating(modelBuilder);
        //         modelBuilder.Entity<FundsYeld>(funds => {
        //              funds.HasKey(x => x.Id);
        //              funds.Property(x => x.Id).ValueGeneratedOnAdd();
        //         });

        //         base.OnModelCreating(modelBuilder);
        //         modelBuilder.Entity<RankOfTheBestFunds>(Fund => {
        //             Fund.HasKey(x=> x.FundCode);
        //         });

        //     }

    }
}