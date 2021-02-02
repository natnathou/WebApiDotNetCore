using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_rpg.Migrations
{
    public partial class UserCharacterRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "USerId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_USerId",
                table: "Characters",
                column: "USerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_USerId",
                table: "Characters",
                column: "USerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_USerId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_USerId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "USerId",
                table: "Characters");
        }
    }
}
