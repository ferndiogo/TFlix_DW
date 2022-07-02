using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Migrations
{
    public partial class AlteracaoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Subscricoes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Imagem",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Filmes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Imagem",
                table: "Filmes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Aluguers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "944eca38-03ee-4782-ba19-4a3e38598933");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "e8ae9ad0-93dc-474d-8e36-07fb0f3a7416");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "s",
                column: "ConcurrencyStamp",
                value: "a6963fa2-6d83-4cc7-b4cf-34b4b523aefb");

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Classificacao",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Classificacao",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1,
                column: "Classificacao",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2,
                column: "Classificacao",
                value: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Subscricoes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Imagem",
                table: "Series",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Filmes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Imagem",
                table: "Filmes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Aluguers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "85b785b4-045a-48e0-9c1b-37c3241ddeff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "73d25faf-4c27-4c14-9285-1623313cc688");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "s",
                column: "ConcurrencyStamp",
                value: "3cd290db-561c-4ee5-bcad-c5e288ec383e");

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Classificacao",
                value: 83);

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Classificacao",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1,
                column: "Classificacao",
                value: 90);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2,
                column: "Classificacao",
                value: 96);
        }
    }
}
