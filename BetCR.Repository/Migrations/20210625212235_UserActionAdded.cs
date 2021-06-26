using Microsoft.EntityFrameworkCore.Migrations;

namespace BetCR.Repository.Migrations
{
    public partial class UserActionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FromUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ToUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ActionType = table.Column<string>(type: "TEXT", nullable: true),
                    ActionObject = table.Column<string>(type: "TEXT", nullable: true),
                    ActionStatus = table.Column<string>(type: "TEXT", nullable: true),
                    ActionResult = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<int>(type: "INTEGER", nullable: false),
                    UpsertDateEpoch = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAction_User_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAction_User_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAction_FromUserId",
                table: "UserAction",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAction_ToUserId",
                table: "UserAction",
                column: "ToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAction");
        }
    }
}
