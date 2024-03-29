﻿using BetCR.Repository.ValueObject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace BetCR.Repository.Entity.Base
{
    public class BaseEntity<T>
    {
        #region Public Constructors

        public BaseEntity(ILazyLoader lazyLoader) : this()
        {
            LazyLoader = lazyLoader;
        }

        public BaseEntity()
        {
            Active = 1;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Active { get; set; }

        public virtual T Id { get; set; }

        [IgnoreDataMember] public ILazyLoader LazyLoader { get; }

        public CustomDateTime UpsertDate
        {
            get
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(UpsertDateEpoch).DateTime;
                DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                return (CustomDateTime) dt;
            }
        }

        public long UpsertDateEpoch { get; set; }

        #endregion Public Properties
    }
}