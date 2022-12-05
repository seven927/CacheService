namespace CacheService.Cache
{
    public interface ICacheService<TKey,TValue>
    {
        /// <summary>
        /// Get a cached value for a specific key
        /// </summary>
        /// <param name="key">cache key</param>
        /// <returns>Cached value</returns>
        TValue Get(TKey key);

        /// <summary>
        /// Add a value to the cache
        /// </summary>
        /// <param name="key">cache key</param>
        /// <param name="value">cache value</param>
        void Set(TKey key, TValue value);
    }
}
