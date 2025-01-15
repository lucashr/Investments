using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Investments.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Function = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestFundRanks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", nullable: true),
                    Segment = table.Column<string>(type: "TEXT", nullable: true),
                    MultiplierRanking = table.Column<int>(type: "INTEGER", nullable: false),
                    CoefficientOfVariation = table.Column<double>(type: "REAL", nullable: false),
                    FFOYield = table.Column<double>(type: "REAL", nullable: false),
                    DividendYield = table.Column<double>(type: "REAL", nullable: false),
                    DividendYieldRanking = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceEquityValue = table.Column<double>(type: "REAL", nullable: false),
                    RankPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    ValueOfMarket = table.Column<double>(type: "REAL", nullable: false),
                    Liquidity = table.Column<double>(type: "REAL", nullable: false),
                    AverageVacancy = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestFundRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestStockRanks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", nullable: true),
                    Quotation = table.Column<double>(type: "REAL", nullable: false),
                    PL = table.Column<double>(type: "REAL", nullable: false),
                    PVP = table.Column<double>(type: "REAL", nullable: false),
                    PSR = table.Column<double>(type: "REAL", nullable: false),
                    DivYield = table.Column<double>(type: "REAL", nullable: false),
                    PriceOverAsset = table.Column<double>(type: "REAL", nullable: false),
                    PriceOnWorkingCapital = table.Column<double>(type: "REAL", nullable: false),
                    PEBIT = table.Column<double>(type: "REAL", nullable: false),
                    PriceOverNetCurrentAssets = table.Column<double>(type: "REAL", nullable: false),
                    EVEBIT = table.Column<double>(type: "REAL", nullable: false),
                    EVEBITDA = table.Column<double>(type: "REAL", nullable: false),
                    EbitMargin = table.Column<double>(type: "REAL", nullable: false),
                    LiquidityMargin = table.Column<double>(type: "REAL", nullable: false),
                    LiquidityCurrent = table.Column<double>(type: "REAL", nullable: false),
                    ROIC = table.Column<double>(type: "REAL", nullable: false),
                    ROE = table.Column<double>(type: "REAL", nullable: false),
                    LiquidityTwoMonths = table.Column<double>(type: "REAL", nullable: false),
                    NetWorth = table.Column<double>(type: "REAL", nullable: false),
                    GrossEquityDebt = table.Column<double>(type: "REAL", nullable: false),
                    RevenueGrowthFiveYears = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestStockRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailedFunds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", nullable: true),
                    Segment = table.Column<string>(type: "TEXT", nullable: true),
                    Quotation = table.Column<double>(type: "REAL", nullable: false),
                    FFOYield = table.Column<double>(type: "REAL", nullable: false),
                    DividendYield = table.Column<double>(type: "REAL", nullable: false),
                    PriceEquityValue = table.Column<double>(type: "REAL", nullable: false),
                    ValueOfMarket = table.Column<double>(type: "REAL", nullable: false),
                    Liquidity = table.Column<double>(type: "REAL", nullable: false),
                    NumberOfProperties = table.Column<double>(type: "REAL", nullable: false),
                    SquareMeterPrice = table.Column<double>(type: "REAL", nullable: false),
                    RentPerSquareMeter = table.Column<double>(type: "REAL", nullable: false),
                    CapRate = table.Column<double>(type: "REAL", nullable: false),
                    AverageVacancy = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedFunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailedStocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", nullable: true),
                    Quotation = table.Column<double>(type: "REAL", nullable: false),
                    PL = table.Column<double>(type: "REAL", nullable: false),
                    PVP = table.Column<double>(type: "REAL", nullable: false),
                    PSR = table.Column<double>(type: "REAL", nullable: false),
                    DivYield = table.Column<double>(type: "REAL", nullable: false),
                    PriceOverAsset = table.Column<double>(type: "REAL", nullable: false),
                    PriceOnWorkingCapital = table.Column<double>(type: "REAL", nullable: false),
                    PEBIT = table.Column<double>(type: "REAL", nullable: false),
                    PriceOverNetCurrentAssets = table.Column<double>(type: "REAL", nullable: false),
                    EVEBIT = table.Column<double>(type: "REAL", nullable: false),
                    EVEBITDA = table.Column<double>(type: "REAL", nullable: false),
                    EbitMargin = table.Column<double>(type: "REAL", nullable: false),
                    LiquidityMargin = table.Column<double>(type: "REAL", nullable: false),
                    LiquidityCurrent = table.Column<double>(type: "REAL", nullable: false),
                    ROIC = table.Column<double>(type: "REAL", nullable: false),
                    ROE = table.Column<double>(type: "REAL", nullable: false),
                    LiquidityTwoMonths = table.Column<double>(type: "REAL", nullable: false),
                    NetWorth = table.Column<double>(type: "REAL", nullable: false),
                    GrossEquityDebt = table.Column<double>(type: "REAL", nullable: false),
                    RevenueGrowthFiveYears = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundDividends",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", nullable: true),
                    LastComputedDate = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    DatePayment = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundDividends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StocksDividends",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    DatePayment = table.Column<string>(type: "TEXT", nullable: true),
                    ForHowManyShares = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocksDividends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId1 = table.Column<string>(type: "TEXT", nullable: true),
                    RoleId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    District = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42103ea4-63fa-4b38-bd1b-c38160bfda2b", "b1d19665-6b11-4b77-9109-136a376a6276", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ddfe99a-6ff0-41d1-a4f3-697ccbc4c2e5", "36a582e0-c7e3-42ad-92e3-75bf323c66c4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Function", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e1ef25ad-9080-4e51-82dd-9cfc2b9bba90", 0, "a44ec98e-96db-4570-8839-7797e0611408", "admin@example.com", true, null, 0, null, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEFTpw+QDRXuBY9Ron1UL+Fh9FWiMrnpownK1zu9slutgA5WYQF6D7scHoaxTaXQ1ZQ==", null, false, "d5f8f172-bffe-43a2-9775-1238940e6eaf", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "RoleId1", "UserId1" },
                values: new object[] { "42103ea4-63fa-4b38-bd1b-c38160bfda2b", "e1ef25ad-9080-4e51-82dd-9cfc2b9bba90", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BestFundRanks");

            migrationBuilder.DropTable(
                name: "BestStockRanks");

            migrationBuilder.DropTable(
                name: "DetailedFunds");

            migrationBuilder.DropTable(
                name: "DetailedStocks");

            migrationBuilder.DropTable(
                name: "FundDividends");

            migrationBuilder.DropTable(
                name: "StocksDividends");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
