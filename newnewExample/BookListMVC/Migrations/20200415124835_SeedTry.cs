using Microsoft.EntityFrameworkCore.Migrations;

namespace BookListMVC.Migrations
{
    public partial class SeedTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "ISBN", "Name" },
                values: new object[] { 1, "Max Mustermann", null, "The Great Book" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "ISBN", "Name" },
                values: new object[] { 2, "Max MusterMustermann", null, "Another Great Book" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
