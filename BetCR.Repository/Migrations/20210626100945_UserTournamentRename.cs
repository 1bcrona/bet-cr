using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class UserTournamentRename : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTournament");

            migrationBuilder.CreateTable(
                name: "UserTournameRel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<int>(type: "INTEGER", nullable: false),
                    TournamentId = table.Column<string>(type: "TEXT", nullable: true),
                    UpsertDateEpoch = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTournameRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTournameRel_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTournameRel_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTournameRel_TournamentId",
                table: "UserTournameRel",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTournameRel_UserId",
                table: "UserTournameRel",
                column: "UserId");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTournameRel");

            migrationBuilder.CreateTable(
                name: "UserTournament",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TournamentId = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<int>(type: "INTEGER", nullable: false),
                    UpsertDateEpoch = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTournament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTournament_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTournament_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTournament_TournamentId",
                table: "UserTournament",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTournament_UserId",
                table: "UserTournament",
                column: "UserId");
        }

        #endregion Protected Methods
    }
}