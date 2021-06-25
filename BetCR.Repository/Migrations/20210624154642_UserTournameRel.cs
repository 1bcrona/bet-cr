using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class UserTournameRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentEndDate",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "TournamentStartDate",
                table: "Tournament");

       

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Tournament",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TournameStartDateEpoch",
                table: "Tournament",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TournamentEndDateEpoch",
                table: "Tournament",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UserTournameRel",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTournameRel");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "TournameStartDateEpoch",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "TournamentEndDateEpoch",
                table: "Tournament");

 

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentEndDate",
                table: "Tournament",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentStartDate",
                table: "Tournament",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
