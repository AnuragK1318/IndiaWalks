using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IndiaWalks.APi.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1, "Easy" },
                    { 2, "Medium" },
                    { 3, "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Area", "Code", "Lat", "Long", "Name", "Population" },
                values: new object[] { 1, 162968.0, "AP", 15.9129, 79.739999999999995, "Andhra Pradesh", 49577103L });

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "DifficultyId", "Length", "Name", "RegionId" },
                values: new object[] { 1, 1, 2.5, "Charminar Walk", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
