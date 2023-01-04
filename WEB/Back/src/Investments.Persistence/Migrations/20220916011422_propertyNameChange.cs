using Microsoft.EntityFrameworkCore.Migrations;

namespace Investments.Persistence.Migrations
{
    public partial class propertyNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorDeMercado",
                table: "RankFunds",
                newName: "ValueOfMarket");

            migrationBuilder.RenameColumn(
                name: "VacanciaMedia",
                table: "RankFunds",
                newName: "PriceEquityValue");

            migrationBuilder.RenameColumn(
                name: "Segmento",
                table: "RankFunds",
                newName: "Segment");

            migrationBuilder.RenameColumn(
                name: "RankPreco",
                table: "RankFunds",
                newName: "RankPrice");

            migrationBuilder.RenameColumn(
                name: "RankMultiplicador",
                table: "RankFunds",
                newName: "MultiplierRanking");

            migrationBuilder.RenameColumn(
                name: "RankDivYeld",
                table: "RankFunds",
                newName: "DividendYieldRanking");

            migrationBuilder.RenameColumn(
                name: "PVP",
                table: "RankFunds",
                newName: "Liquidity");

            migrationBuilder.RenameColumn(
                name: "Liquidez",
                table: "RankFunds",
                newName: "AverageVacancy");

            migrationBuilder.RenameColumn(
                name: "Papel",
                table: "RankFunds",
                newName: "FundCode");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "FundsYeld",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "UltDataCom",
                table: "FundsYeld",
                newName: "LastComputedDate");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "FundsYeld",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Papel",
                table: "FundsYeld",
                newName: "FundCode");

            migrationBuilder.RenameColumn(
                name: "DataPagamento",
                table: "FundsYeld",
                newName: "DatePayment");

            migrationBuilder.RenameColumn(
                name: "Papel",
                table: "Funds",
                newName: "FundCode");

            migrationBuilder.RenameColumn(
                name: "ValorDeMercado",
                table: "DetailedFunds",
                newName: "ValueOfMarket");

            migrationBuilder.RenameColumn(
                name: "VacanciaMedia",
                table: "DetailedFunds",
                newName: "SquareMeterPrice");

            migrationBuilder.RenameColumn(
                name: "Segmento",
                table: "DetailedFunds",
                newName: "Segment");

            migrationBuilder.RenameColumn(
                name: "QtdDeImoveis",
                table: "DetailedFunds",
                newName: "RentPerSquareMeter");

            migrationBuilder.RenameColumn(
                name: "PrecoDoM2",
                table: "DetailedFunds",
                newName: "Quotation");

            migrationBuilder.RenameColumn(
                name: "PVP",
                table: "DetailedFunds",
                newName: "PriceEquityValue");

            migrationBuilder.RenameColumn(
                name: "Liquidez",
                table: "DetailedFunds",
                newName: "NumberOfProperties");

            migrationBuilder.RenameColumn(
                name: "Cotacao",
                table: "DetailedFunds",
                newName: "Liquidity");

            migrationBuilder.RenameColumn(
                name: "AluguelPorM2",
                table: "DetailedFunds",
                newName: "AverageVacancy");

            migrationBuilder.RenameColumn(
                name: "Papel",
                table: "DetailedFunds",
                newName: "FundCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueOfMarket",
                table: "RankFunds",
                newName: "ValorDeMercado");

            migrationBuilder.RenameColumn(
                name: "Segment",
                table: "RankFunds",
                newName: "Segmento");

            migrationBuilder.RenameColumn(
                name: "RankPrice",
                table: "RankFunds",
                newName: "RankPreco");

            migrationBuilder.RenameColumn(
                name: "PriceEquityValue",
                table: "RankFunds",
                newName: "VacanciaMedia");

            migrationBuilder.RenameColumn(
                name: "MultiplierRanking",
                table: "RankFunds",
                newName: "RankMultiplicador");

            migrationBuilder.RenameColumn(
                name: "Liquidity",
                table: "RankFunds",
                newName: "PVP");

            migrationBuilder.RenameColumn(
                name: "DividendYieldRanking",
                table: "RankFunds",
                newName: "RankDivYeld");

            migrationBuilder.RenameColumn(
                name: "AverageVacancy",
                table: "RankFunds",
                newName: "Liquidez");

            migrationBuilder.RenameColumn(
                name: "FundCode",
                table: "RankFunds",
                newName: "Papel");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "FundsYeld",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FundsYeld",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "LastComputedDate",
                table: "FundsYeld",
                newName: "UltDataCom");

            migrationBuilder.RenameColumn(
                name: "FundCode",
                table: "FundsYeld",
                newName: "Papel");

            migrationBuilder.RenameColumn(
                name: "DatePayment",
                table: "FundsYeld",
                newName: "DataPagamento");

            migrationBuilder.RenameColumn(
                name: "FundCode",
                table: "Funds",
                newName: "Papel");

            migrationBuilder.RenameColumn(
                name: "ValueOfMarket",
                table: "DetailedFunds",
                newName: "ValorDeMercado");

            migrationBuilder.RenameColumn(
                name: "SquareMeterPrice",
                table: "DetailedFunds",
                newName: "VacanciaMedia");

            migrationBuilder.RenameColumn(
                name: "Segment",
                table: "DetailedFunds",
                newName: "Segmento");

            migrationBuilder.RenameColumn(
                name: "RentPerSquareMeter",
                table: "DetailedFunds",
                newName: "QtdDeImoveis");

            migrationBuilder.RenameColumn(
                name: "Quotation",
                table: "DetailedFunds",
                newName: "PrecoDoM2");

            migrationBuilder.RenameColumn(
                name: "PriceEquityValue",
                table: "DetailedFunds",
                newName: "PVP");

            migrationBuilder.RenameColumn(
                name: "NumberOfProperties",
                table: "DetailedFunds",
                newName: "Liquidez");

            migrationBuilder.RenameColumn(
                name: "Liquidity",
                table: "DetailedFunds",
                newName: "Cotacao");

            migrationBuilder.RenameColumn(
                name: "AverageVacancy",
                table: "DetailedFunds",
                newName: "AluguelPorM2");

            migrationBuilder.RenameColumn(
                name: "FundCode",
                table: "DetailedFunds",
                newName: "Papel");
        }
    }
}
