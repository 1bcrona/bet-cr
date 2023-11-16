using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class AddHasStanding : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "HasStanding",
                "Stage");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "HasStanding",
                "Stage",
                "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        #endregion Protected Methods
    }
}