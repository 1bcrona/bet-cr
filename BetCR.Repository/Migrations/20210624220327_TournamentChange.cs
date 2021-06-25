using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class TournamentChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserId",
                table: "Tournament",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TournamentPassword",
                table: "Tournament",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_OwnerUserId",
                table: "Tournament",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_User_OwnerUserId",
                table: "Tournament",
                column: "OwnerUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_User_OwnerUserId",
                table: "Tournament");

            migrationBuilder.DropIndex(
                name: "IX_Tournament_OwnerUserId",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "TournamentPassword",
                table: "Tournament");
        }
    }
}
