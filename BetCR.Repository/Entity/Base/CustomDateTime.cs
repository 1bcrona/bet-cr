using System;

namespace BetCR.Repository.Entity.Base
{
    public class CustomDateTime
    {
        #region Private Fields

        private DateTime _inner;

        #endregion Private Fields

        #region Public Constructors

        public CustomDateTime(DateTime dt)
        {
            _inner = dt;
        }

        #endregion Public Constructors

        #region Public Properties

        public static DateTime MaxDateTime => DateTime.MaxValue;
        public DateTime Inner => _inner;

        public DateTime Local
        {
            get
            {
                return _inner.ToLocalTime();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static explicit operator CustomDateTime(DateTime mdt)
        {
            return new(mdt);
        }

        public static explicit operator DateTime(CustomDateTime mdt)
        {
            return mdt._inner;
        }

        #endregion Public Methods
    }
}