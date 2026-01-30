using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stage_4.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CreatedAt", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 30, 17, 19, 16, 846, DateTimeKind.Local).AddTicks(5360), null, true, "Environment Setup and Database Integration" },
                    { 2, new DateTime(2026, 1, 30, 17, 19, 16, 847, DateTimeKind.Local).AddTicks(4466), null, false, "Implement JWT Authentication (Stage 6)" },
                    { 3, new DateTime(2026, 1, 30, 17, 19, 16, 847, DateTimeKind.Local).AddTicks(4476), null, false, "Write Unit Tests for Todo Controller" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
