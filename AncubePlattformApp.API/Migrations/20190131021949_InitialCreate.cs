using Microsoft.EntityFrameworkCore.Migrations;

namespace AncubePlattformApp.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        // Up keeps the information for CREATE the Table
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });
        }


        // Down keeps the information for shutdown the table
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");
        }
    }
}
