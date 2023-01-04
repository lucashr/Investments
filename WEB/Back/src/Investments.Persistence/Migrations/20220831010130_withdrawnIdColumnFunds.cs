using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Investments.Persistence.Migrations
{
    public partial class withdrawnIdColumnFunds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Funds",
                table: "Funds");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UltDataCom",
                table: "FundsYeld",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "FundsYeld",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPagamento",
                table: "FundsYeld",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<string>(
                name: "Papel",
                table: "Funds",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Funds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funds",
                table: "Funds",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Funds",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Funds");

            migrationBuilder.AlterColumn<string>(
                name: "UltDataCom",
                table: "FundsYeld",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Tipo",
                table: "FundsYeld",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DataPagamento",
                table: "FundsYeld",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Papel",
                table: "Funds",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funds",
                table: "Funds",
                column: "Papel");
        }
    }
}
