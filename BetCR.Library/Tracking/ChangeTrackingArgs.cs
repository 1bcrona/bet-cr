using System;
using BetCR.Library.Tracking.Infrastructure;

namespace BetCR.Library.Tracking
{
    public class ChangeTrackingArgs<T> : IChangeTrackingArgs<T>
    {
        #region Public Properties

        public T NewValue { get; set; }
        public T OldValue { get; set; }
        public string TypeName => ValueType?.FullName;
        public Type ValueType { get; set; }

        #endregion Public Properties
    }

    public class ChangeTrackingArgs : IChangeTrackingArgs
    {
        #region Public Properties

        public object NewValue { get; set; }
        public object OldValue { get; set; }
        public string TypeName => ValueType?.FullName;
        public Type ValueType { get; set; }

        #endregion Public Properties
    }
}