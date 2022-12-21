using Investimentos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Investimentos.Persistence.Contexts
{
    public class InvestmentsContext : DbContext
    {
        public InvestmentsContext(DbContextOptions<InvestmentsContext> options): base(options){}

            public DbSet<DetailedFunds> DetailedFunds { get; set; }
            public DbSet<Funds> Funds { get; set; }
            public DbSet<FundsYeld> FundsYeld { get; set; }
            public DbSet<RankOfTheBestFunds> RankFunds { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                
                base.OnModelCreating(modelBuilder);

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