using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NumericalSimulation.Entities;
using NumericalSimulation.Entities.Enums;

namespace NumericalSimulation.Interfaces
{
    public interface ISimulationService
    {
        Task<ErrorLoadFileEnum> SetInputData(IFormFile formFile, InputDataTypeEnum type, Guid sessionId);
        IEnumerable<Schedule> GetScheduleByType(Guid sessionIdGuid, ScheduleAlgorithmTypeEnum algorithmType);
    }
}