namespace EFCoreQueryCachingDemo.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Get a value from the cache.
        /// </summary>
        /// <typeparam name="T">Type of Value want to get</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// Set a value to the cache.
        /// </summary>
        /// <typeparam name="T">Type of Value want to set to the cache</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Value want to set to the cache</param>
        /// <param name="expiredInMinute">Cache Expired Add Time on Now, Minute</param>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, T value, int expiredInMinute = 120);
        /// <summary>
        /// Remove Cache with keys, if exist
        /// </summary>
        /// <param name="key">Cache Keys</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);
    }
}
