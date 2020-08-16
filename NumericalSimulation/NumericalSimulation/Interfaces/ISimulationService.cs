using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NumericalSimulation.Entities.Enums;

namespace NumericalSimulation.Interfaces
{
    public interface ISimulationService
    {
        public Task SetInputData(IFormFile formFile, InputDataTypeEnum type, Guid sessionId);
    }
}