using Microsoft.EntityFrameworkCore.Migrations;

namespace Investimentos.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailedFunds",
                columns: table => new
                {
                    Papel = table.Column<string>(type: "TEXT", nullable: false),
                    Segmento = table.Column<string>(type: "TEXT", nullable: true),
                    Cotacao = table.Column<double>(type: "REAL", nullable: false),
                    FFOYield = table.Column<double>(type: "REAL", nullable: false),
                    DividendYield = table.Column<double>(type: "REAL", nullable: false),
                    PVP = table.Column<double>(type: "REAL", nullable: false),
                    ValorDeMercado = table.Column<double>(type: "REAL", nullable: false),
                    Liquidez = table.Column<double>(type: "REAL", nullable: false),
                    QtdDeImoveis = table.Column<double>(type: "REAL", nullable: false),
                    PrecoDoM2 = table.Column<double>(type: "REAL", nullable: false),
                    AluguelPorM2 = table.Column<double>(type: "REAL", nullable: false),
                    CapRate = table.Column<double>(type: "REAL", nullable: false),
                    VacanciaMedia = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedFunds", x => x.Papel);
                });

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Papel = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Papel);
                });

            migrationBuilder.CreateTable(
                name: "FundsYeld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Papel = table.Column<string>(type: "TEXT", nullable: true),
                    UltDataCom = table.Column<string>(type: "TEXT", nullable: true),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    DataPagamento = table.Column<double>(type: "REAL", nullable: false),
                    Tipo = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundsYeld", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailedFunds");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "FundsYeld");
        }
    }
}
