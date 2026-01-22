using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonY.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "Description", "Name" },
                values: new object[] { 1, "test", "test" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 1, 1, "test", "test ", 123m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
