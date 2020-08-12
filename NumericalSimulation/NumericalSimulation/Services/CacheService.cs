using System;
using System.Threading.Tasks;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Services
{
    public class CacheService : ICacheService
    {
        public Task AddNewEntity(CacheUserInputData userInputData, Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<CacheUserInputData> GetEntity(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveEntityById(Guid sessionId)
        {
            throw new NotImplementedException();
        }
    }
}