using System;
using BetCR.Library.Tracking.Infrastructure;

namespace BetCR.Library.Tracking
{
    public class Publisher<T> : IPublisher<T>
    {
        #region Private Fields

        private string _Name;

        #endregion Private Fields

        #region Public Constructors

        public Publisher(string name)
        {
            _Name = name;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<T> OnPublish;

        #endregion Public Events

        #region Public Methods

        public void Publish(T data)
        {
            OnPublish?.Invoke(this, data);
        }

        #endregion Public Methods
    }

    public class Publisher : IPublisher
    {
        #region Private Fields

        private string _Name;
        private string _id;

        #endregion Private Fields

        #region Public Constructors

        public Publisher() : this("publisher")
        {
        }

        public Publisher(string name)
        {
            _Name = name;
            _id = Guid.NewGuid().ToString();
        }

        #endregion Public Constructors

        #region Public Events

        public string Id => _id;
        public event EventHandler<object> OnPublish;

        public void Publish(object data)
        {
            OnPublish?.Invoke(this, data);
        }

        #endregion Public Events

        #region Public Methods

        public void Publish(object oldValue, object newValue)
        {
            if (OnPublish == null) return;

            var changedEvent = new ChangeTrackingArgs {OldValue = oldValue, NewValue = newValue, ValueType = typeof(object)};
            OnPublish(this, changedEvent);
        }

        #endregion Public Methods
    }
}