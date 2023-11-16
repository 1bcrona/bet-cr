using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangingStandings : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Serialized",
                "StageStanding");

            migrationBuilder.RenameColumn(
                "Standings",
                "StageStanding",
                "Data");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Data",
                "StageStanding",
                "Standings");

            migrationBuilder.AddColumn<string>(
                "Serialized",
                "StageStanding",
                "TEXT",
                nullable: true);
        }

        #endregion Protected Methods
    }
}