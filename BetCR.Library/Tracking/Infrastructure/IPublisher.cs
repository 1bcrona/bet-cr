using System;

namespace BetCR.Library.Tracking.Infrastructure
{
    public interface IPublisher<T>
    {
        #region Public Events

        public event EventHandler<T> OnPublish;

        #endregion Public Events

        #region Public Methods

        public void Publish(T data);

        #endregion Public Methods
    }

    public interface IPublisher
    {

        public string Id { get; }
        public event EventHandler<object> OnPublish;
        public void Publish(object data);
    }
}