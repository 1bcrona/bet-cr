using System;
using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BetCR.Repository.ValueObject;

namespace BetCR.Repository.Entity
{
    public class Match : EntityBase<string>
    {

        public Match()
        {
        }
        public Match(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }
        private Stage _stage;
        private Team _homeTeam;
        private Team _awayTeam;
        private Tournament _tournament;
        private IEnumerable<UserMatchBet> _userMatchBets;
        private MatchEvent _matchEvent;

        public override string Id { get; set; }

        public string ExternalId { get; set; }

        public string Week { get; set; }

        public int ResultState { get; set; }

        public long MatchDateEpoch { get; set; }

        public CustomDateTime MatchDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(MatchDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        public Tournament Tournament
        {
            get => LazyLoader.Load(this, ref _tournament);
            set => _tournament = value;
        }
        public Stage Stage
        {
            get => LazyLoader.Load(this, ref _stage);
            set => _stage = value;
        }

        public string HomeTeamId { get; set; }
        public string AwayTeamId { get; set; }
        public Team HomeTeam
        {
            get => LazyLoader.Load(this, ref _homeTeam);
            set => _homeTeam = value;
        }

        public Team AwayTeam
        {
            get => LazyLoader.Load(this, ref _awayTeam);
            set => _awayTeam = value;
        }

        public IEnumerable<UserMatchBet> UserMatchBets
        {
            get => LazyLoader.Load(this, ref _userMatchBets);
            set => _userMatchBets = value;
        }

        [ForeignKey("MatchEventId")]
        public MatchEvent MatchEvent
        {
            get => LazyLoader.Load(this, ref _matchEvent);
            set => _matchEvent = value;
        }
    }

}