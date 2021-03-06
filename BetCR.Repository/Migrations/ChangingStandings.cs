using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class ChangingStandings : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Serialized",
                table: "StageStanding");

            migrationBuilder.RenameColumn(
                name: "Standings",
                table: "StageStanding",
                newName: "Data");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "StageStanding",
                newName: "Standings");

            migrationBuilder.AddColumn<string>(
                name: "Serialized",
                table: "StageStanding",
                type: "TEXT",
                nullable: true);
        }

        #endregion Protected Methods
    }
}