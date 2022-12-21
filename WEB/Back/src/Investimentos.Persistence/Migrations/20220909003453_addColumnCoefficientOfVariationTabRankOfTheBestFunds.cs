using Microsoft.EntityFrameworkCore.Migrations;

namespace Investimentos.Persistence.Migrations
{
    public partial class addColumnCoefficientOfVariationTabRankOfTheBestFunds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RankFunds",
                columns: table => new
                {
                    Papel = table.Column<string>(type: "TEXT", nullable: false),
                    Segmento = table.Column<string>(type: "TEXT", nullable: true),
                    RankMultiplicador = table.Column<int>(type: "INTEGER", nullable: false),
                    CoefficientOfVariation = table.Column<double>(type: "REAL", nullable: false),
                    FFOYield = table.Column<double>(type: "REAL", nullable: false),
                    DividendYield = table.Column<double>(type: "REAL", nullable: false),
                    RankDivYeld = table.Column<int>(type: "INTEGER", nullable: false),
                    PVP = table.Column<double>(type: "REAL", nullable: false),
                    RankPreco = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorDeMercado = table.Column<double>(type: "REAL", nullable: false),
                    Liquidez = table.Column<double>(type: "REAL", nullable: false),
                    VacanciaMedia = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankFunds", x => x.Papel);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankFunds");
        }
    }
}
