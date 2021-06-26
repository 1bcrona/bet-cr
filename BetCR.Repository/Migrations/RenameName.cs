using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class RenameName : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalId",
                table: "Match",
                newName: "ExternalMatchId");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalMatchId",
                table: "Match",
                newName: "ExternalId");
        }

        #endregion Protected Methods
    }
}