using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BetCR.Repository.Entity.Base
{
    public class EntityBase<T>
    {
        #region Public Constructors

        public EntityBase(ILazyLoader lazyLoader) : this()
        {
            LazyLoader = lazyLoader;
        }

        public EntityBase()
        {
            Active = 1;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Active { get; set; }

        public virtual T Id { get; set; }

        [IgnoreDataMember]

        public ILazyLoader LazyLoader { get; }

        public long UpsertDateEpoch { get; set; }

        public CustomDateTime UpsertDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(UpsertDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime)dt;
            }
        }

        #endregion Public Properties
    }
}