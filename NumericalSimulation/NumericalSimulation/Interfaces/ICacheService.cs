using System;
using System.Threading.Tasks;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface ICacheService
    {
        Task AddNewEntity(CacheUserInputData userInputData, Guid sessionId);
        Task<CacheUserInputData> GetEntity(Guid sessionId);
        Task RemoveEntityById(Guid sessionId);
    }
}