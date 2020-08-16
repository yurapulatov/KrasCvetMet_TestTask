using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumericalSimulation.Entities.Enums;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Controllers
{
    [ApiController]
    [Route("simulation")]
    public class SimulationController : Controller
    {
        private readonly ISimulationService _simulationService;

        public SimulationController(ISimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        public async Task<IActionResult> AddUserFile([FromBody] IFormFile formFile, [FromQuery] InputDataTypeEnum type,
            [FromQuery] string sessionId)
        {
            try
            {
                var sessionIdGuid = Guid.Parse(sessionId);
                await _simulationService.SetInputData(formFile, type, sessionIdGuid);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult CalculationSchedule([FromQuery] string sessionId, [FromQuery] ScheduleAlgorithmTypeEnum algorithmType)
        {
            try
            {
                var sessionIdGuid = Guid.Parse(sessionId);
                var schedules = _simulationService.GetScheduleByType(sessionIdGuid, algorithmType);
                return Ok(schedules);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

}
}