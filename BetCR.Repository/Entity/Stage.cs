using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BetCR.Repository.Entity
{
    public class Stage : EntityBase<string>
    {
        #region Private Fields

        private League _league;
        private StageStanding _stageStanding;

        #endregion Private Fields

        #region Public Constructors

        public Stage() : base()
        {
        }

        public Stage(ILazyLoader loader) : base(loader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public string ExternalId { get; set; }
        public bool HasStanding { get; set; }
        public override string Id { get; set; }

        public League League
        {
            get => LazyLoader.Load(this, ref _league);
            set => _league = value;
        }

        public string LeagueId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Match> Matches { get; set; }

        public string StageName { get; set; }

        [ForeignKey("StageStandingId")]
        public StageStanding StageStanding
        {
            get => LazyLoader.Load(this, ref _stageStanding);
            set => _stageStanding = value;
        }

        #endregion Public Properties
    }
}