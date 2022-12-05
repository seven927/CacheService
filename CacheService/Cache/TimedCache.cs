using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace CacheService.Cache
{
    /// <summary>
    /// A cache implementatin with TTL mechanism 
    /// </summary>
    /// <typeparam name="TKey">Type for the cahce key</typeparam>
    /// <typeparam name="TValue">Type for the cache value</typeparam>
    public class TimedCache<TKey, TValue> : ICacheService<TKey, TValue>, IDisposable
    {
        // How often (in minute) the cache entry should be checked 
        private const int CACHE_CHECK_INTERVAL = 2;

        // The TTL for the cache entries in the current cache instance
        private readonly int _expirationLimit;
        private readonly Dictionary<TKey, CacheEntry> _store;
        private readonly Timer _timer;
        private readonly ReaderWriterLockSlim _lock;

        public TimedCache(int expirationLimit) 
        {
            _expirationLimit = expirationLimit;
            _store = new Dictionary<TKey, CacheEntry>();
            _timer = new Timer(CheckCacheCallback, null, CACHE_CHECK_INTERVAL * 60000, CACHE_CHECK_INTERVAL * 60000);
            _lock = new ReaderWriterLockSlim();
        }

        public TimedCache() : this(5) 
        {
        }

        /// <summary>
        /// Get a cached value for a specific key
        /// </summary>
        /// <param name="key">cache key</param>
        /// <returns>Cached value</returns>
        public TValue Get(TKey key)
        {
            CacheEntry entry = null;
            _lock.EnterReadLock();
            try
            {
                _store.TryGetValue(key, out entry);
            }
            finally 
            {
                if (entry != null) 
                {
                    entry.LastAcessTime = DateTime.UtcNow;
                }
                _lock.ExitReadLock();
            }

            if (entry != null) 
            {
                return entry.Value;
            }
            return default(TValue);
        }

        /// <summary>
        /// Add a value to the cache
        /// </summary>
        /// <param name="key">cache key</param>
        /// <param name="value">cache value</param>
        public void Set(TKey key, TValue value)
        {
            _lock.EnterWriteLock();
            try
            {
                _store[key] = new CacheEntry(value);
            }
            finally 
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Callback to get rid of expired cache entries
        /// </summary>
        /// <param name="state"></param>
        private void CheckCacheCallback(object state) 
        {
            List<TKey> removeList = new();
            _lock.EnterReadLock();
            try
            {
                foreach (KeyValuePair<TKey, CacheEntry> entry in _store) 
                {
                    if (entry.Value.LastAcessTime.AddMinutes(_expirationLimit) <= DateTime.UtcNow) 
                    {
                        removeList.Add(entry.Key);
                    }
                }
            }
            finally 
            {
                _lock.ExitReadLock();
            }

            _lock.EnterWriteLock();
            try
            {
                foreach (TKey key in removeList) 
                {
                    _store.Remove(key);
                }
            }
            finally 
            {
                _lock.ExitWriteLock();
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
            _lock.Dispose();
        }

        /// <summary>
        /// Cache entry that wraps the actual cached value
        /// </summary>
        private class CacheEntry 
        {
            public CacheEntry(TValue value) 
            {
                Value = value;
                LastAcessTime = DateTime.UtcNow;
            }

            /// <summary>
            /// Cache value
            /// </summary>
            public TValue Value { get; set; }

            /// <summary>
            /// Date and time of the last access for this cache entry
            /// </summary>
            public DateTime LastAcessTime { get; set; }
        }
    }
}
