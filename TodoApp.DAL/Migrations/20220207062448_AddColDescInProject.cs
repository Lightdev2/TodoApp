using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApp.DAL.Migrations
{
    public partial class AddColDescInProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Projects",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Projects");
        }
    }
}
