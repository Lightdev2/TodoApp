using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApp.DAL.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sessions",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                newName: "IX_Sessions_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserIdId",
                table: "Sessions",
                column: "UserIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserIdId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Sessions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserIdId",
                table: "Sessions",
                newName: "IX_Sessions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
