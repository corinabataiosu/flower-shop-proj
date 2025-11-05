using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bloomify.Migrations
{
    /// <inheritdoc />
    public partial class Fixedprovider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "roses" },
                    { 2, "tulips" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderID", "ProviderName" },
                values: new object[,]
                {
                    { 1, "pro1" },
                    { 2, "pro2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "ImagePath", "Price", "ProductDescription", "ProductName", "ProviderID" },
                values: new object[,]
                {
                    { 1, 1, "path1", 15f, "description 1", "Trandafir Roșu", 1 },
                    { 2, 2, "path2", 10f, "description 2", "Lalea Galbenă", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderID",
                keyValue: 2);
        }
    }
}
