using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangeBase : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "UserMatchBet");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "User");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "Tournament");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "TeamLeagueRel");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "Team");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "StageStanding");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "Stage");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "MatchEvent");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "Match");

            migrationBuilder.DropColumn(
                "UpsertDateEpoch",
                "League");

            migrationBuilder.RenameColumn(
                "Active",
                "UserMatchBet",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "User",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "Tournament",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "TeamLeagueRel",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "Team",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "StageStanding",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "Stage",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "MatchEvent",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "Match",
                "ACTIVE");

            migrationBuilder.RenameColumn(
                "Active",
                "League",
                "ACTIVE");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ACTIVE",
                "UserMatchBet",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "User",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "Tournament",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "TeamLeagueRel",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "Team",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "StageStanding",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "Stage",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "MatchEvent",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "Match",
                "Active");

            migrationBuilder.RenameColumn(
                "ACTIVE",
                "League",
                "Active");

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "UserMatchBet",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "User",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "Tournament",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "TeamLeagueRel",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "Team",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "StageStanding",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "Stage",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "MatchEvent",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "Match",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "UpsertDateEpoch",
                "League",
                "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        #endregion Protected Methods
    }
}