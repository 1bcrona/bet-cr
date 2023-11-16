using BetCR.Repository.Entity.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetCR.Repository.Entity
{
    public class Match : BaseEntity<string>
    {
        #region Private Fields

        private Team _awayTeam;

        private Team _homeTeam;

        private MatchEvent _matchEvent;

        private Stage _stage;

        private Tournament _tournament;

        private IEnumerable<UserMatchBet> _userMatchBets;

        #endregion Private Fields

        #region Public Constructors

        public Match()
        {
        }

        public Match(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Team AwayTeam
        {
            get => LazyLoader.Load(this, ref _awayTeam);
            set => _awayTeam = value;
        }

        public string AwayTeamId { get; set; }
        public string ExternalId { get; set; }

        public Team HomeTeam
        {
            get => LazyLoader.Load(this, ref _homeTeam);
            set => _homeTeam = value;
        }

        public string HomeTeamId { get; set; }
        public override string Id { get; set; }

        public CustomDateTime MatchDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(MatchDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime) dt;
            }
        }

        public long MatchDateEpoch { get; set; }

        [ForeignKey("MatchEventId")]
        public MatchEvent MatchEvent
        {
            get => LazyLoader.Load(this, ref _matchEvent);
            set => _matchEvent = value;
        }

        public int ResultState { get; set; }

        public Stage Stage
        {
            get => LazyLoader.Load(this, ref _stage);
            set => _stage = value;
        }

        public Tournament Tournament
        {
            get => LazyLoader.Load(this, ref _tournament);
            set => _tournament = value;
        }

        public IEnumerable<UserMatchBet> UserMatchBets
        {
            get => LazyLoader.Load(this, ref _userMatchBets);
            set => _userMatchBets = value;
        }

        public string Week { get; set; }

        #endregion Public Properties
    }
}