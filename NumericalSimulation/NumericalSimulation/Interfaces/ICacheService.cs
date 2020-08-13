using System;
using System.Threading.Tasks;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface ICacheService
    {
        void AddNewEntity(CacheUserInputData userInputData, Guid sessionId);
        CacheUserInputData GetEntity(Guid sessionId);
        void RemoveEntityById(Guid sessionId);
    }
}