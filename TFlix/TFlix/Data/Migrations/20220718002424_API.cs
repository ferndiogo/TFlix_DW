using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Migrations
{
    public partial class API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "8a614acc-a16a-41d2-82cc-2ae508a885c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "d38840cb-0000-4f90-af22-2b3b176f8259");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "222ae499-9c55-4a46-8c37-e06542df7cc6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "76f55b5a-0d50-4b68-905e-764fc7aa6d20");
        }
    }
}
