using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_Api_Repository.Migrations
{
    /// <inheritdoc />
    public partial class CategorySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryDesc", "CategoryName", "CreatedAt", "ModifiedAt" },
                values: new object[,]
                {
                    { 1, "Delicious pizzas", "Pizza", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2795), null },
                    { 2, "Italian pastas", "Pasta", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2798), null },
                    { 3, "Soft drinks and beverages", "Drinks", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2800), null },
                    { 4, "Tasty burgers", "Burgers", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2845), null },
                    { 5, "Healthy salads", "Salads", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2847), null },
                    { 6, "Sweet treats", "Desserts", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2849), null },
                    { 7, "Fresh seafood dishes", "Seafood", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2851), null },
                    { 8, "Hot and tasty soups", "Soups", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2853), null },
                    { 9, "Delicious sandwiches", "Sandwiches", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2854), null },
                    { 10, "Morning meals", "Breakfast", new DateTime(2025, 9, 2, 12, 4, 56, 581, DateTimeKind.Utc).AddTicks(2856), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
