using System.Collections.Generic;
using System.IO;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface IExportService
    {
        byte[] ExportScheduleToExcelFile(IEnumerable<Schedule> schedules);
    }
}