using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class UpdatedUser : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DateOfBirth",
                "User");

            migrationBuilder.RenameColumn(
                "Firstname",
                "User",
                "Username");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Username",
                "User",
                "Firstname");

            migrationBuilder.AddColumn<string>(
                "DateOfBirth",
                "User",
                "TEXT",
                nullable: true);
        }

        #endregion Protected Methods
    }
}