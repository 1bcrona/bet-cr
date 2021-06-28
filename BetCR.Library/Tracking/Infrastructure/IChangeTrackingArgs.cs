using System;

namespace BetCR.Library.Tracking.Infrastructure
{
    public interface IChangeTrackingArgs<out T>
    {

        #region Public Properties

        public T NewValue { get; }
        public T OldValue { get; }
        public Type ValueType { get; }

        

        #endregion Public Properties

    }

    public interface IChangeTrackingArgs : IChangeTrackingArgs<object>
    {
    }
}