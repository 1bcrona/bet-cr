using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class AddUserBetPointDefault : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBetPointDefault",
                table: "UserMatchBet");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserBetPointDefault",
                table: "UserMatchBet",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        #endregion Protected Methods
    }
}