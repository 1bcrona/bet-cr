using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangedMatchEventElapsedAdd : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CurrentElapsed",
                "MatchEvent");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "CurrentElapsed",
                "MatchEvent",
                "TEXT",
                nullable: true);
        }

        #endregion Protected Methods
    }
}