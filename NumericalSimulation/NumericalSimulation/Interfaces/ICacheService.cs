using System;
using System.Threading.Tasks;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface ICacheService
    {
        void AddOrUpdateNewUserInputData(CacheUserInputData userInputData, Guid sessionId);
        CacheUserInputData GetUserInputData(Guid sessionId);
        void RemoveEntityById(Guid sessionId);
    }
}