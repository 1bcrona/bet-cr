using System;

namespace BetCR.Library.Tracking.Infrastructure
{
    public interface ISubscriber<T>
    {
        #region Public Events

        public event EventHandler<T> MessageReceived;

        #endregion Public Events

        #region Public Methods

        public void Subscribe();

        public void Unsubscribe();

        #endregion Public Methods
    }

    public interface ISubscriber : ISubscriber<object>
    {
    }
}