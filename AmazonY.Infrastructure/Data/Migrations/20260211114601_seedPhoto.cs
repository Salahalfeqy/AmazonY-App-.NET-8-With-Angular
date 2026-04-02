using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonY.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "id", "ImageName", "ProductId" },
                values: new object[] { 1, "https://m.media-amazon.com/images/I/71w+q9Zt2sL._AC_SX679_.jpg", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
