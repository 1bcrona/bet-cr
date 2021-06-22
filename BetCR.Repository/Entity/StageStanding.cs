using BetCR.Repository.Entity.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BetCR.Repository.ValueObject;
using Newtonsoft.Json;

namespace BetCR.Repository.Entity
{


    public class StageStanding : EntityBase<string>
    {
        public StageStanding()
        {
        }
        public StageStanding(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        public List<Standing> Standings { get; set; }

        private Stage _stage;

        public override string Id { get; set; }

        public string Serialized { get; set; }


        [ForeignKey("StageId")]
        public virtual Stage Stage
        {
            get => LazyLoader.Load(this, ref _stage);
            set => _stage = value;
        }
    }

}