using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BetCR.Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamLeagueRel");

            migrationBuilder.DropTable(
                name: "UserMatchBet");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "MatchEvent");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Tournament");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropTable(
                name: "StageStanding");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: true),
                    LeagueName = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchEvent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HomeTeamScore = table.Column<int>(type: "INTEGER", nullable: true),
                    AwayTeamScore = table.Column<int>(type: "INTEGER", nullable: true),
                    Incidents = table.Column<string>(type: "TEXT", nullable: true),
                    Lineups = table.Column<string>(type: "TEXT", nullable: true),
                    MatchId = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageStanding",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: true),
                    StageId = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageStanding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: true),
                    TeamLogo = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TournamentName = table.Column<string>(type: "TEXT", nullable: true),
                    TournamentStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TournamentEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: true),
                    StageName = table.Column<string>(type: "TEXT", nullable: true),
                    StageStandingId = table.Column<string>(type: "TEXT", nullable: true),
                    LeagueId = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stage_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stage_StageStanding_StageStandingId",
                        column: x => x.StageStandingId,
                        principalTable: "StageStanding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamLeagueRel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LeagueId = table.Column<string>(type: "TEXT", nullable: true),
                    TeamId = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLeagueRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamLeagueRel_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamLeagueRel_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalMatchId = table.Column<string>(type: "TEXT", nullable: true),
                    Week = table.Column<string>(type: "TEXT", nullable: true),
                    ResultState = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchDateEpoch = table.Column<long>(type: "INTEGER", nullable: false),
                    TournamentId = table.Column<string>(type: "TEXT", nullable: true),
                    StageId = table.Column<string>(type: "TEXT", nullable: true),
                    HomeTeamId = table.Column<string>(type: "TEXT", nullable: true),
                    AwayTeamId = table.Column<string>(type: "TEXT", nullable: true),
                    MatchEventId = table.Column<string>(type: "TEXT", nullable: true),
                    LeagueId = table.Column<string>(type: "TEXT", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_MatchEvent_MatchEventId",
                        column: x => x.MatchEventId,
                        principalTable: "MatchEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMatchBet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "INTEGER", nullable: true),
                    HomeTeamScore = table.Column<int>(type: "INTEGER", nullable: true),
                    MatchId = table.Column<string>(type: "TEXT", nullable: true),
                    ProcessState = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    UserBetPoint = table.Column<int>(type: "INTEGER", nullable: true),
                    ACTIVE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatchBet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMatchBet_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMatchBet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_AwayTeamId",
                table: "Match",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_HomeTeamId",
                table: "Match",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_LeagueId",
                table: "Match",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchEventId",
                table: "Match",
                column: "MatchEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_StageId",
                table: "Match",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TournamentId",
                table: "Match",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_LeagueId",
                table: "Stage",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_StageStandingId",
                table: "Stage",
                column: "StageStandingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamLeagueRel_LeagueId",
                table: "TeamLeagueRel",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLeagueRel_TeamId",
                table: "TeamLeagueRel",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatchBet_MatchId",
                table: "UserMatchBet",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatchBet_UserId",
                table: "UserMatchBet",
                column: "UserId");
        }

        #endregion Protected Methods
    }
}