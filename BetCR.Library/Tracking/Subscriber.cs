using BetCR.Library.Tracking.Infrastructure;
using System;

namespace BetCR.Library.Tracking
{
    public class Subscriber<T> : ISubscriber<T>
    {
        #region Private Fields

        private readonly IPublisher<T> _Publisher;

        #endregion Private Fields

        #region Public Constructors

        public Subscriber(IPublisher<T> publisher)
        {
            this._Publisher = publisher;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<T> MessageReceived;

        #endregion Public Events

        #region Public Methods

        public void Subscribe()
        {
            _Publisher.OnPublish += OnNotificationReceived;
        }

        public void Unsubscribe()
        {
            _Publisher.OnPublish -= OnNotificationReceived;
        }

        #endregion Public Methods

        #region Private Methods

        private void OnNotificationReceived(object sender, T e)
        {
            MessageReceived?.Invoke(this, e);
        }

        #endregion Private Methods
    }

    public class Subscriber : ISubscriber
    {
        #region Private Fields

        private readonly IPublisher _Publisher;

        #endregion Private Fields

        #region Public Constructors

        public Subscriber(IPublisher publisher)
        {
            this._Publisher = publisher;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<object> MessageReceived;

        #endregion Public Events

        #region Public Methods

        public void Subscribe()
        {
            _Publisher.OnPublish += OnNotificationReceived;
        }

        public void Unsubscribe()
        {
            _Publisher.OnPublish -= OnNotificationReceived;
        }

        #endregion Public Methods

        #region Private Methods

        private void OnNotificationReceived(object sender, object e)
        {
            MessageReceived?.Invoke(this, e);
        }

        #endregion Private Methods
    }
}