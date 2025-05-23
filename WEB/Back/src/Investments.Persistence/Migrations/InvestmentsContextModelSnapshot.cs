﻿// <auto-generated />
using System;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Investments.Persistence.Migrations
{
    [DbContext(typeof(InvestmentsContext))]
    partial class InvestmentsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("Investments.Domain.DetailedStock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("DivYield")
                        .HasColumnType("REAL");

                    b.Property<double>("EVEBIT")
                        .HasColumnType("REAL");

                    b.Property<double>("EVEBITDA")
                        .HasColumnType("REAL");

                    b.Property<double>("EbitMargin")
                        .HasColumnType("REAL");

                    b.Property<string>("FundCode")
                        .HasColumnType("TEXT");

                    b.Property<double>("GrossEquityDebt")
                        .HasColumnType("REAL");

                    b.Property<double>("LiquidityCurrent")
                        .HasColumnType("REAL");

                    b.Property<double>("LiquidityMargin")
                        .HasColumnType("REAL");

                    b.Property<double>("LiquidityTwoMonths")
                        .HasColumnType("REAL");

                    b.Property<double>("NetWorth")
                        .HasColumnType("REAL");

                    b.Property<double>("PEBIT")
                        .HasColumnType("REAL");

                    b.Property<double>("PL")
                        .HasColumnType("REAL");

                    b.Property<double>("PSR")
                        .HasColumnType("REAL");

                    b.Property<double>("PVP")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceOnWorkingCapital")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceOverAsset")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceOverNetCurrentAssets")
                        .HasColumnType("REAL");

                    b.Property<double>("Quotation")
                        .HasColumnType("REAL");

                    b.Property<double>("ROE")
                        .HasColumnType("REAL");

                    b.Property<double>("ROIC")
                        .HasColumnType("REAL");

                    b.Property<double>("RevenueGrowthFiveYears")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("DetailedStocks");
                });

            modelBuilder.Entity("Investments.Domain.Identity.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "42103ea4-63fa-4b38-bd1b-c38160bfda2b",
                            ConcurrencyStamp = "b1d19665-6b11-4b77-9109-136a376a6276",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "6ddfe99a-6ff0-41d1-a4f3-697ccbc4c2e5",
                            ConcurrencyStamp = "36a582e0-c7e3-42ad-92e3-75bf323c66c4",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Investments.Domain.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Function")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "e1ef25ad-9080-4e51-82dd-9cfc2b9bba90",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a44ec98e-96db-4570-8839-7797e0611408",
                            Email = "admin@example.com",
                            EmailConfirmed = true,
                            Function = 0,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEFTpw+QDRXuBY9Ron1UL+Fh9FWiMrnpownK1zu9slutgA5WYQF6D7scHoaxTaXQ1ZQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d5f8f172-bffe-43a2-9775-1238940e6eaf",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Investments.Domain.Identity.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId1")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "e1ef25ad-9080-4e51-82dd-9cfc2b9bba90",
                            RoleId = "42103ea4-63fa-4b38-bd1b-c38160bfda2b"
                        });
                });

            modelBuilder.Entity("Investments.Domain.Models.BestFundRank", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("AverageVacancy")
                        .HasColumnType("REAL");

                    b.Property<double>("CoefficientOfVariation")
                        .HasColumnType("REAL");

                    b.Property<double>("DividendYield")
                        .HasColumnType("REAL");

                    b.Property<int>("DividendYieldRanking")
                        .HasColumnType("INTEGER");

                    b.Property<double>("FFOYield")
                        .HasColumnType("REAL");

                    b.Property<string>("FundCode")
                        .HasColumnType("TEXT");

                    b.Property<double>("Liquidity")
                        .HasColumnType("REAL");

                    b.Property<int>("MultiplierRanking")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PriceEquityValue")
                        .HasColumnType("REAL");

                    b.Property<int>("RankPrice")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Segment")
                        .HasColumnType("TEXT");

                    b.Property<double>("ValueOfMarket")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("BestFundRanks");
                });

            modelBuilder.Entity("Investments.Domain.Models.BestStockRank", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("DivYield")
                        .HasColumnType("REAL");

                    b.Property<double>("EVEBIT")
                        .HasColumnType("REAL");

                    b.Property<double>("EVEBITDA")
                        .HasColumnType("REAL");

                    b.Property<double>("EbitMargin")
                        .HasColumnType("REAL");

                    b.Property<string>("FundCode")
                        .HasColumnType("TEXT");

                    b.Property<double>("GrossEquityDebt")
                        .HasColumnType("REAL");

                    b.Property<double>("LiquidityCurrent")
                        .HasColumnType("REAL");

                    b.Property<double>("LiquidityMargin")
                        .HasColumnType("REAL");

                    b.Property<double>("LiquidityTwoMonths")
                        .HasColumnType("REAL");

                    b.Property<double>("NetWorth")
                        .HasColumnType("REAL");

                    b.Property<double>("PEBIT")
                        .HasColumnType("REAL");

                    b.Property<double>("PL")
                        .HasColumnType("REAL");

                    b.Property<double>("PSR")
                        .HasColumnType("REAL");

                    b.Property<double>("PVP")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceOnWorkingCapital")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceOverAsset")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceOverNetCurrentAssets")
                        .HasColumnType("REAL");

                    b.Property<double>("Quotation")
                        .HasColumnType("REAL");

                    b.Property<double>("ROE")
                        .HasColumnType("REAL");

                    b.Property<double>("ROIC")
                        .HasColumnType("REAL");

                    b.Property<double>("RevenueGrowthFiveYears")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("BestStockRanks");
                });

            modelBuilder.Entity("Investments.Domain.Models.DetailedFund", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("AverageVacancy")
                        .HasColumnType("REAL");

                    b.Property<double>("CapRate")
                        .HasColumnType("REAL");

                    b.Property<double>("DividendYield")
                        .HasColumnType("REAL");

                    b.Property<double>("FFOYield")
                        .HasColumnType("REAL");

                    b.Property<string>("FundCode")
                        .HasColumnType("TEXT");

                    b.Property<double>("Liquidity")
                        .HasColumnType("REAL");

                    b.Property<double>("NumberOfProperties")
                        .HasColumnType("REAL");

                    b.Property<double>("PriceEquityValue")
                        .HasColumnType("REAL");

                    b.Property<double>("Quotation")
                        .HasColumnType("REAL");

                    b.Property<double>("RentPerSquareMeter")
                        .HasColumnType("REAL");

                    b.Property<string>("Segment")
                        .HasColumnType("TEXT");

                    b.Property<double>("SquareMeterPrice")
                        .HasColumnType("REAL");

                    b.Property<double>("ValueOfMarket")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("DetailedFunds");
                });

            modelBuilder.Entity("Investments.Domain.Models.FundDividend", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DatePayment")
                        .HasColumnType("TEXT");

                    b.Property<string>("FundCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastComputedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("FundDividends");
                });

            modelBuilder.Entity("Investments.Domain.StockDividend", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DatePayment")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForHowManyShares")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FundCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("StocksDividends");
                });

            modelBuilder.Entity("Investments.Domain.UserAddress", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("District")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Investments.Domain.Identity.UserRole", b =>
                {
                    b.HasOne("Investments.Domain.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Investments.Domain.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("Investments.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Investments.Domain.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Investments.Domain.UserAddress", b =>
                {
                    b.HasOne("Investments.Domain.Identity.User", "User")
                        .WithOne("EnderecoUsuario")
                        .HasForeignKey("Investments.Domain.UserAddress", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Investments.Domain.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Investments.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Investments.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Investments.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Investments.Domain.Identity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Investments.Domain.Identity.User", b =>
                {
                    b.Navigation("EnderecoUsuario");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
