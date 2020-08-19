using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumericalSimulation.Entities;
using NumericalSimulation.Entities.Enums;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Controllers
{
    [ApiController]
    [Route("simulation")]
    public class SimulationController : Controller
    {
        private readonly ISimulationService _simulationService;
        private readonly IExportService _exportService;

        public SimulationController(ISimulationService simulationService, IExportService exportService)
        {
            _simulationService = simulationService;
            _exportService = exportService;
        }

        [HttpPost("add_data")]
        public async Task<IActionResult> AddUserFile([FromForm] IFormFile fileData,  [FromForm] InputDataTypeEnum type,  [FromForm] string sessionId)
        {
            try
            {
                var sessionIdGuid = Guid.Parse(sessionId);
                var result = await _simulationService.SetInputData(fileData, type, sessionIdGuid);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("schedule")]
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

        public IActionResult ExportScheduleToExcelFile([FromBody] IEnumerable<Schedule> schedules)
        {
            try
            {
                var bytes = _exportService.ExportScheduleToExcelFile(schedules);
                var fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "schedule.xlsx";
                return File(bytes, fileType, fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
}
}