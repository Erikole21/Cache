using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace CO.Servidor.Raps.Cache
{
    public class AdministradorCache
    {
        static readonly ObjectCache Cache = MemoryCache.Default;
        static readonly int MinutosExpiracionDefault = 20;

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(KeysCahe key) where T : class
        {
            try
            {
                return (T)Cache[key.ToString()];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(T objectToCache, KeysCahe key, DateTimeOffset? tiempoExpiracion = null) where T : class
        {
            Cache.Add(key.ToString(), objectToCache, tiempoExpiracion.HasValue ? tiempoExpiracion.Value : DateTime.Now.AddMinutes(MinutosExpiracionDefault));
        }

        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(KeysCahe key)
        {
            Cache.Remove(key.ToString());
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(KeysCahe key)
        {
            return Cache.Get(key.ToString()) != null;
        }

        /// <summary>
        /// Gets all cached items as a list by their key.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAll()
        {
            return Cache.Select(keyValuePair => keyValuePair.Key).ToList();
        }
    }
}
