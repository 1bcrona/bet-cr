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
        private League _league;
        private StageStanding _stageStanding;

        public Stage() : base()
        {

        }

        public Stage(ILazyLoader loader) : base(loader)
        {

        }
        public override string Id { get; set; }

        public string LeagueId { get; set; }
        public string ExternalId { get; set; }

        public bool HasStanding { get; set; }
        public string StageName { get; set; }
        [ForeignKey("StageStandingId")]
        public StageStanding StageStanding
        {
            get => LazyLoader.Load(this, ref _stageStanding);
            set => _stageStanding = value;
        }


        public League League
        {
            get => LazyLoader.Load(this, ref _league);
            set => _league = value;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Match> Matches { get; set; }
    }

}