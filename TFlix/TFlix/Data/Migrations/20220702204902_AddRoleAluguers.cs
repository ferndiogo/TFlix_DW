using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Migrations
{
    public partial class AddRoleAluguers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "be32b1ee-4d74-4655-b738-5c7148cc9354");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "al", "7f96c8e9-64da-4b81-b0d0-f2cf6143158a", "Alugueres", "ALUGUERES" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "al");

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
        }
    }
}
