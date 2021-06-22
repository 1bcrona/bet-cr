﻿// <auto-generated />
using System;
using BetCR.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BetCR.Repository.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    [Migration("20210621213734_UpdatedUser")]
    partial class UpdatedUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("BetCR.Repository.Entity.League", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LeagueName")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("League");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Match", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AwayTeamId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HomeTeamId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LeagueId")
                        .HasColumnType("TEXT");

                    b.Property<long>("MatchDateEpoch")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MatchEventId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ResultState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TournamentId")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Week")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("MatchEventId")
                        .IsUnique();

                    b.HasIndex("StageId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.MatchEvent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AwayTeamScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrentElapsed")
                        .HasColumnType("TEXT");

                    b.Property<string>("Events")
                        .HasColumnType("TEXT");

                    b.Property<int?>("HomeTeamScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MatchId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MatchLineup")
                        .HasColumnType("TEXT");

                    b.Property<string>("MatchStat")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MatchEvent");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Stage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasStanding")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeagueId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StageName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StageStandingId")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("StageStandingId")
                        .IsUnique();

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.StageStanding", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("StageId")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("StageStanding");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DominantColors")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamLogo")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.TeamLeagueRel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeagueId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamId")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamLeagueRel");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Tournament", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TournamentEndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("TournamentName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TournamentStartDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Tournament");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.UserMatchBet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Leverage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MatchId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProcessState")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UpsertDateEpoch")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserBetPoint")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserBetPointDefault")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMatchBet");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Match", b =>
                {
                    b.HasOne("BetCR.Repository.Entity.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("AwayTeamId");

                    b.HasOne("BetCR.Repository.Entity.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamId");

                    b.HasOne("BetCR.Repository.Entity.League", null)
                        .WithMany("Matches")
                        .HasForeignKey("LeagueId");

                    b.HasOne("BetCR.Repository.Entity.MatchEvent", "MatchEvent")
                        .WithOne("Match")
                        .HasForeignKey("BetCR.Repository.Entity.Match", "MatchEventId");

                    b.HasOne("BetCR.Repository.Entity.Stage", "Stage")
                        .WithMany("Matches")
                        .HasForeignKey("StageId");

                    b.HasOne("BetCR.Repository.Entity.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("MatchEvent");

                    b.Navigation("Stage");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Stage", b =>
                {
                    b.HasOne("BetCR.Repository.Entity.League", "League")
                        .WithMany("Stages")
                        .HasForeignKey("LeagueId");

                    b.HasOne("BetCR.Repository.Entity.StageStanding", "StageStanding")
                        .WithOne("Stage")
                        .HasForeignKey("BetCR.Repository.Entity.Stage", "StageStandingId");

                    b.Navigation("League");

                    b.Navigation("StageStanding");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.TeamLeagueRel", b =>
                {
                    b.HasOne("BetCR.Repository.Entity.League", "League")
                        .WithMany("TeamLeagueRels")
                        .HasForeignKey("LeagueId");

                    b.HasOne("BetCR.Repository.Entity.Team", "Team")
                        .WithMany("TeamLeagueRels")
                        .HasForeignKey("TeamId");

                    b.Navigation("League");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.UserMatchBet", b =>
                {
                    b.HasOne("BetCR.Repository.Entity.Match", "Match")
                        .WithMany("UserMatchBets")
                        .HasForeignKey("MatchId");

                    b.HasOne("BetCR.Repository.Entity.User", "User")
                        .WithMany("UserMatchBets")
                        .HasForeignKey("UserId");

                    b.Navigation("Match");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.League", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Stages");

                    b.Navigation("TeamLeagueRels");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Match", b =>
                {
                    b.Navigation("UserMatchBets");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.MatchEvent", b =>
                {
                    b.Navigation("Match");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Stage", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.StageStanding", b =>
                {
                    b.Navigation("Stage");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Team", b =>
                {
                    b.Navigation("AwayMatches");

                    b.Navigation("HomeMatches");

                    b.Navigation("TeamLeagueRels");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.Tournament", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("BetCR.Repository.Entity.User", b =>
                {
                    b.Navigation("UserMatchBets");
                });
#pragma warning restore 612, 618
        }
    }
}
