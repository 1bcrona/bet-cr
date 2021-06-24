using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangedMatchEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lineups",
                table: "MatchEvent",
                newName: "MatchStat");

            migrationBuilder.RenameColumn(
                name: "Incidents",
                table: "MatchEvent",
                newName: "MatchLineup");

            migrationBuilder.AddColumn<string>(
                name: "Events",
                table: "MatchEvent",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Events",
                table: "MatchEvent");

            migrationBuilder.RenameColumn(
                name: "MatchStat",
                table: "MatchEvent",
                newName: "Lineups");

            migrationBuilder.RenameColumn(
                name: "MatchLineup",
                table: "MatchEvent",
                newName: "Incidents");
        }
    }
}
