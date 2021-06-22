using System;
using System.Collections.Generic;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BetCR.Repository.Repository
{
    public class SQLiteDbContext : DataContext
    {
        #region Public Fields



        #endregion Public Fields

        #region Public Constructors

        public SQLiteDbContext()
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(base.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<League>().HasKey(x => x.Id);
            modelBuilder.Entity<StageStanding>().HasKey(x => x.Id);
            modelBuilder.Entity<Team>().HasKey(x => x.Id);
            modelBuilder.Entity<TeamLeagueRel>().HasKey(x => x.Id);
            modelBuilder.Entity<Tournament>().HasKey(x => x.Id);
            modelBuilder.Entity<Match>().HasKey(x => x.Id);
            modelBuilder.Entity<MatchEvent>().HasKey(x => x.Id);
            modelBuilder.Entity<UserMatchBet>().HasKey(x => x.Id);
            modelBuilder.Entity<Stage>().HasKey(h => h.Id);
            modelBuilder.Entity<StageStanding>().HasKey(h => h.Id);


            modelBuilder.Entity<User>().HasMany(m => m.UserMatchBets).WithOne(o => o.User);


            modelBuilder.Entity<Match>().HasOne(h => h.Stage).WithMany(m => m.Matches);
            modelBuilder.Entity<Match>().HasOne(h => h.HomeTeam).WithMany(m => m.HomeMatches);
            modelBuilder.Entity<Match>().HasOne(h => h.AwayTeam).WithMany(m => m.AwayMatches);
            modelBuilder.Entity<Match>().HasOne(h => h.Tournament).WithMany(m => m.Matches);

            modelBuilder.Entity<Match>().HasOne(h => h.MatchEvent).WithOne(m => m.Match);
            modelBuilder.Entity<Match>().HasMany(h => h.UserMatchBets).WithOne(m => m.Match);


            modelBuilder.Entity<TeamLeagueRel>().HasOne(h => h.Team).WithMany(m => m.TeamLeagueRels);
            modelBuilder.Entity<TeamLeagueRel>().HasOne(h => h.League).WithMany(m => m.TeamLeagueRels);

            modelBuilder.Entity<UserMatchBet>().HasOne(h => h.User).WithMany(m => m.UserMatchBets);
            modelBuilder.Entity<UserMatchBet>().HasOne(h => h.Match).WithMany(m => m.UserMatchBets);

            modelBuilder.Entity<Stage>().HasOne(h => h.StageStanding).WithOne(o => o.Stage);
            modelBuilder.Entity<Stage>().HasOne(h => h.League).WithMany(o => o.Stages);

            modelBuilder.Entity<MatchEvent>().Property(p => p.Events)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<MatchEvents>(v));


            modelBuilder.Entity<MatchEvent>().Property(p => p.MatchStat)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<MatchStats>(v));


            modelBuilder.Entity<MatchEvent>().Property(p => p.MatchLineup)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<MatchLineups>(v));


            modelBuilder.Entity<StageStanding>().Property(p => p.Standings)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<Standing>>(v));

            base.OnModelCreating(modelBuilder);
        }

        #endregion Protected Methods
    }
}