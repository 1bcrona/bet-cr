using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangedMatchEvent : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Events",
                "MatchEvent");

            migrationBuilder.RenameColumn(
                "MatchStat",
                "MatchEvent",
                "Lineups");

            migrationBuilder.RenameColumn(
                "MatchLineup",
                "MatchEvent",
                "Incidents");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Lineups",
                "MatchEvent",
                "MatchStat");

            migrationBuilder.RenameColumn(
                "Incidents",
                "MatchEvent",
                "MatchLineup");

            migrationBuilder.AddColumn<string>(
                "Events",
                "MatchEvent",
                "TEXT",
                nullable: true);
        }

        #endregion Protected Methods
    }
}