using Microsoft.EntityFrameworkCore.Migrations;

namespace BookListMVC.Migrations
{
    public partial class addedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Department = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 1, 1, "asd@asdf.de", "Tom H" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 2, 2, "asd2@asdf.de", "Tom 2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 3, 1, "asd3@asdf.de", "Tom 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
