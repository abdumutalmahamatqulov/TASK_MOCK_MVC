using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TASK_MOCK_MVC.Migrations
{
    /// <inheritdoc />
    public partial class Joke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dca1f47-b34e-47f1-8b4b-8a5ce08bc074");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2098b4e0-2d76-42a9-b0f0-c9f809d1f576");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfc76249-ddea-448b-a3b5-a4aed35ca1de");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "400aa3c3-1413-4532-aa99-89a2c9bf2c9c", null, "USER", "USER" },
                    { "9b7e88ca-bad1-4687-8a15-d086de471e11", null, "MANAGER", "MANAGER" },
                    { "9fa51dcd-2ce2-4ef2-aa5b-abd243f802e6", null, "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "400aa3c3-1413-4532-aa99-89a2c9bf2c9c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b7e88ca-bad1-4687-8a15-d086de471e11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fa51dcd-2ce2-4ef2-aa5b-abd243f802e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dca1f47-b34e-47f1-8b4b-8a5ce08bc074", null, "ADMIN", "ADMIN" },
                    { "2098b4e0-2d76-42a9-b0f0-c9f809d1f576", null, "MANAGER", "MANAGER" },
                    { "bfc76249-ddea-448b-a3b5-a4aed35ca1de", null, "USER", "USER" }
                });
        }
    }
}
