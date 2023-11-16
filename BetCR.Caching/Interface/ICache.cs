using System;
using System.Threading.Tasks;

namespace BetCR.Caching.Interface
{
    public interface ICache : IDisposable
    {
        #region Public Methods

        Task<bool> Delete(string key);

        Task<bool> Exists(string key);

        Task<bool> Expire(string key, TimeSpan expiry);

        Task Flush();

        Task<T> Get<T>(string key, T defaultValue = default);

        Task<bool> Persist(string key);

        Task<bool> Set(string key, object value, TimeSpan? expiry = null);

        #endregion Public Methods
    }
}