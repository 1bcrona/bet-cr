using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangeBase : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "UserMatchBet");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "TeamLeagueRel");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "StageStanding");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "MatchEvent");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "UpsertDateEpoch",
                table: "League");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "UserMatchBet",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "User",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Tournament",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "TeamLeagueRel",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Team",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "StageStanding",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Stage",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "MatchEvent",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Match",
                newName: "ACTIVE");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "League",
                newName: "ACTIVE");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "UserMatchBet",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "User",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "Tournament",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "TeamLeagueRel",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "Team",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "StageStanding",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "Stage",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "MatchEvent",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "Match",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ACTIVE",
                table: "League",
                newName: "Active");

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "UserMatchBet",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "Tournament",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "TeamLeagueRel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "Team",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "StageStanding",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "Stage",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "MatchEvent",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "Match",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpsertDateEpoch",
                table: "League",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        #endregion Protected Methods
    }
}