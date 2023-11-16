using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class AddUserBetPointDefault : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "UserBetPointDefault",
                "UserMatchBet");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "UserBetPointDefault",
                "UserMatchBet",
                "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        #endregion Protected Methods
    }
}