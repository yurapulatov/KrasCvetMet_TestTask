using System.Collections.Generic;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface ICalculationScheduleService
    {
        public IEnumerable<Schedule> GetSchedule(IEnumerable<Party> parties, IEnumerable<MachineTool> machineToolCount);
    }
}