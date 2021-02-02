using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_rpg.Migrations
{
    public partial class UserCharacterRelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_USerId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "USerId",
                table: "Characters",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_USerId",
                table: "Characters",
                newName: "IX_Characters_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Characters",
                newName: "USerId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                newName: "IX_Characters_USerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_USerId",
                table: "Characters",
                column: "USerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
