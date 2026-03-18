using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IndiaWalks.APi.Migrations
{
    /// <inheritdoc />
    public partial class WalksDataSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "DifficultyId", "Length", "Name", "RegionId" },
                values: new object[,]
                {
                    { 2, 1, 3.6000000000000001, "Marine drive stroll", 2 },
                    { 3, 1, 1.5, "Golden Temple Circuit", 3 },
                    { 4, 2, 4.2000000000000002, "Lalbagh Garden Walk", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
