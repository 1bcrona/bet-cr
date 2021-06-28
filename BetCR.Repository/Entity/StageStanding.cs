using BetCR.Repository.Entity.Base;
using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetCR.Repository.Entity
{
    public class StageStanding : BaseEntity<string>
    {
        #region Private Fields

        private Stage _stage;

        #endregion Private Fields

        #region Public Constructors

        public StageStanding()
        {
        }

        public StageStanding(ILazyLoader lazyLoader) : base(lazyLoader)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Id { get; set; }
        public string Serialized { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage
        {
            get => LazyLoader.Load(this, ref _stage);
            set => _stage = value;
        }

        public List<Standing> Standings { get; set; }

        #endregion Public Properties
    }
}