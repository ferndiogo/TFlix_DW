using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Migrations
{
    public partial class AddAuxPreco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInicio",
                table: "Aluguers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFim",
                table: "Aluguers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "3873db6a-e134-4396-b2e7-e7464e0ef12b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "al",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "61bb40a4-df3f-4802-9f72-562d0d3548b8", "ALUGUERES" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "f6dff349-011e-4658-8003-453e345cc431");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "s",
                column: "ConcurrencyStamp",
                value: "b1daa6fe-03d4-455e-b7f8-d0f2589a2da1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataInicio",
                table: "Aluguers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DataFim",
                table: "Aluguers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "be32b1ee-4d74-4655-b738-5c7148cc9354");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "al",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "7f96c8e9-64da-4b81-b0d0-f2cf6143158a", "ALUGUERS" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "59d4a618-6f7d-4f34-983c-46f0aa1d5ae3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "s",
                column: "ConcurrencyStamp",
                value: "b06e60e8-cbbd-4781-a92b-c6490d60e8cf");
        }
    }
}
