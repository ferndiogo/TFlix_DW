using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Migrations
{
    public partial class AddSeedFilmesSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataCriacao",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DataCriacao",
                table: "Filmes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "7e52447e-8420-4fc3-869c-ff22b1d6d3b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "befb1ec7-661b-4f1b-9211-f7b6759a85b2");

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Classificacao", "DataCriacao", "Elenco", "Genero", "Imagem", "Sinopse", "Titulo" },
                values: new object[,]
                {
                    { 1, 4, "5 de maio de 2022", "Elizabeth Olsen, Benedict Cumberbatch", "Terror", "DoctorStrange.jpeg", "O aguardado filme trata da jornada do Doutor Estranho rumo ao desconhecido.", "Doctor Strange" },
                    { 2, 4, "26 de maio de 2022", "Elsa Pataky, Luke Bracey", "Ação", "Interceptor.jpg", "Um grupo de amigos se envolve em uma série de eventos sobrenaturais na pacata cidade de Hawkins.", "Interceptor" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Classificacao", "DataCriacao", "Elenco", "Episodio", "Genero", "Imagem", "Sinopse", "Temporada", "Titulo" },
                values: new object[,]
                {
                    { 1, 4, "18 de Setembro de 2020", "Sarah Paulson, Finn Wittrock", 8, "Drama", "Ratched.jpeg", "Mildred Ratched começa a trabalhar como enfermeira em um hospital psiquiátrico.", 1, "Ratched" },
                    { 2, 3, "15 de julho de 2016", "Millie Bobby Brown, Finn Wolfhard", 32, "Terror", "StrangerThings.jpg", "Um grupo de amigos se envolve em uma série de eventos sobrenaturais na pacata cidade de Hawkins.", 4, "Stranger Things" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Filmes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "55c52d57-f2fe-42b0-8393-df8f117cd7f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "cb75884c-e6e9-4917-b0aa-cd4e1cc29dcc");
        }
    }
}
