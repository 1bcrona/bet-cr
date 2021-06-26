using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class AddHasStanding : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasStanding",
                table: "Stage");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasStanding",
                table: "Stage",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        #endregion Protected Methods
    }
}