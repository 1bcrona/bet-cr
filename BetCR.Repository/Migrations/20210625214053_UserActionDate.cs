using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class UserActionDate : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDateEpoch",
                table: "UserAction");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActionDateEpoch",
                table: "UserAction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        #endregion Protected Methods
    }
}