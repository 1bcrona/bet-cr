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
                "TeamLeagueRel");

            migrationBuilder.DropTable(
                "UserMatchBet");

            migrationBuilder.DropTable(
                "Match");

            migrationBuilder.DropTable(
                "User");

            migrationBuilder.DropTable(
                "MatchEvent");

            migrationBuilder.DropTable(
                "Stage");

            migrationBuilder.DropTable(
                "Team");

            migrationBuilder.DropTable(
                "Tournament");

            migrationBuilder.DropTable(
                "League");

            migrationBuilder.DropTable(
                "StageStanding");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "League",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    ExternalId = table.Column<string>("TEXT", nullable: true),
                    LeagueName = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_League", x => x.Id); });

            migrationBuilder.CreateTable(
                "MatchEvent",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    HomeTeamScore = table.Column<int>("INTEGER", nullable: true),
                    AwayTeamScore = table.Column<int>("INTEGER", nullable: true),
                    Incidents = table.Column<string>("TEXT", nullable: true),
                    Lineups = table.Column<string>("TEXT", nullable: true),
                    MatchId = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_MatchEvent", x => x.Id); });

            migrationBuilder.CreateTable(
                "StageStanding",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    Data = table.Column<string>("TEXT", nullable: true),
                    StageId = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_StageStanding", x => x.Id); });

            migrationBuilder.CreateTable(
                "Team",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    ExternalId = table.Column<string>("TEXT", nullable: true),
                    TeamLogo = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Team", x => x.Id); });

            migrationBuilder.CreateTable(
                "Tournament",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    TournamentName = table.Column<string>("TEXT", nullable: true),
                    TournamentStartDate = table.Column<DateTime>("TEXT", nullable: false),
                    TournamentEndDate = table.Column<DateTime>("TEXT", nullable: false),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Tournament", x => x.Id); });

            migrationBuilder.CreateTable(
                "User",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    Email = table.Column<string>("TEXT", nullable: true),
                    Password = table.Column<string>("TEXT", nullable: true),
                    Surname = table.Column<string>("TEXT", nullable: true),
                    Username = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

            migrationBuilder.CreateTable(
                "Stage",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    ExternalId = table.Column<string>("TEXT", nullable: true),
                    StageName = table.Column<string>("TEXT", nullable: true),
                    StageStandingId = table.Column<string>("TEXT", nullable: true),
                    LeagueId = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.ForeignKey(
                        "FK_Stage_League_LeagueId",
                        x => x.LeagueId,
                        "League",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Stage_StageStanding_StageStandingId",
                        x => x.StageStandingId,
                        "StageStanding",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "TeamLeagueRel",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    LeagueId = table.Column<string>("TEXT", nullable: true),
                    TeamId = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLeagueRel", x => x.Id);
                    table.ForeignKey(
                        "FK_TeamLeagueRel_League_LeagueId",
                        x => x.LeagueId,
                        "League",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_TeamLeagueRel_Team_TeamId",
                        x => x.TeamId,
                        "Team",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Match",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    ExternalMatchId = table.Column<string>("TEXT", nullable: true),
                    Week = table.Column<string>("TEXT", nullable: true),
                    ResultState = table.Column<int>("INTEGER", nullable: false),
                    MatchDateEpoch = table.Column<long>("INTEGER", nullable: false),
                    TournamentId = table.Column<string>("TEXT", nullable: true),
                    StageId = table.Column<string>("TEXT", nullable: true),
                    HomeTeamId = table.Column<string>("TEXT", nullable: true),
                    AwayTeamId = table.Column<string>("TEXT", nullable: true),
                    MatchEventId = table.Column<string>("TEXT", nullable: true),
                    LeagueId = table.Column<string>("TEXT", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        "FK_Match_League_LeagueId",
                        x => x.LeagueId,
                        "League",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Match_MatchEvent_MatchEventId",
                        x => x.MatchEventId,
                        "MatchEvent",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Match_Stage_StageId",
                        x => x.StageId,
                        "Stage",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Match_Team_AwayTeamId",
                        x => x.AwayTeamId,
                        "Team",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Match_Team_HomeTeamId",
                        x => x.HomeTeamId,
                        "Team",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Match_Tournament_TournamentId",
                        x => x.TournamentId,
                        "Tournament",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "UserMatchBet",
                table => new
                {
                    Id = table.Column<string>("TEXT", nullable: false),
                    AwayTeamScore = table.Column<int>("INTEGER", nullable: true),
                    HomeTeamScore = table.Column<int>("INTEGER", nullable: true),
                    MatchId = table.Column<string>("TEXT", nullable: true),
                    ProcessState = table.Column<int>("INTEGER", nullable: true),
                    UserId = table.Column<string>("TEXT", nullable: true),
                    UserBetPoint = table.Column<int>("INTEGER", nullable: true),
                    ACTIVE = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatchBet", x => x.Id);
                    table.ForeignKey(
                        "FK_UserMatchBet_Match_MatchId",
                        x => x.MatchId,
                        "Match",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_UserMatchBet_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Match_AwayTeamId",
                "Match",
                "AwayTeamId");

            migrationBuilder.CreateIndex(
                "IX_Match_HomeTeamId",
                "Match",
                "HomeTeamId");

            migrationBuilder.CreateIndex(
                "IX_Match_LeagueId",
                "Match",
                "LeagueId");

            migrationBuilder.CreateIndex(
                "IX_Match_MatchEventId",
                "Match",
                "MatchEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Match_StageId",
                "Match",
                "StageId");

            migrationBuilder.CreateIndex(
                "IX_Match_TournamentId",
                "Match",
                "TournamentId");

            migrationBuilder.CreateIndex(
                "IX_Stage_LeagueId",
                "Stage",
                "LeagueId");

            migrationBuilder.CreateIndex(
                "IX_Stage_StageStandingId",
                "Stage",
                "StageStandingId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_TeamLeagueRel_LeagueId",
                "TeamLeagueRel",
                "LeagueId");

            migrationBuilder.CreateIndex(
                "IX_TeamLeagueRel_TeamId",
                "TeamLeagueRel",
                "TeamId");

            migrationBuilder.CreateIndex(
                "IX_UserMatchBet_MatchId",
                "UserMatchBet",
                "MatchId");

            migrationBuilder.CreateIndex(
                "IX_UserMatchBet_UserId",
                "UserMatchBet",
                "UserId");
        }

        #endregion Protected Methods
    }
}