using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "al");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "s");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "0054d562-86ce-4ab5-a3cc-bc426ebbe70a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "742b0f72-0813-493b-bd8c-f26a430f5594");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "3873db6a-e134-4396-b2e7-e7464e0ef12b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "f6dff349-011e-4658-8003-453e345cc431");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "al", "61bb40a4-df3f-4802-9f72-562d0d3548b8", "Alugueres", "ALUGUERES" },
                    { "s", "b1daa6fe-03d4-455e-b7f8-d0f2589a2da1", "Subscritor", "SUBSCRITOR" }
                });
        }
    }
}
