using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class UpdatedUser : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "User",
                newName: "Username");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "Firstname");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "User",
                type: "TEXT",
                nullable: true);
        }

        #endregion Protected Methods
    }
}