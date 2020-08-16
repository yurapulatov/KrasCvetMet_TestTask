using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private static readonly MemoryCacheEntryOptions Options = new MemoryCacheEntryOptions(); 

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            Options.SetAbsoluteExpiration(TimeSpan.FromHours(1));
        }

        public void AddOrUpdateNewUserInputData(CacheUserInputData userInputData, Guid sessionId)
        {
            if (_cache.TryGetValue(sessionId, out CacheUserInputData _))
            {
                _cache.Remove(sessionId);
                _cache.Set(sessionId, userInputData, Options);
            }
            else
            {
                _cache.Set(sessionId, userInputData, Options);
            }
        }

        public CacheUserInputData GetUserInputData(Guid sessionId)
        {
            return _cache.TryGetValue(sessionId, out CacheUserInputData data) ? data : null;
        }

        public void RemoveEntityById(Guid sessionId)
        {
            if (_cache.TryGetValue(sessionId, out CacheUserInputData _))
            {
                _cache.Remove(sessionId);
            }
        }
    }
}