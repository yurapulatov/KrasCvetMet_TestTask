using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Services
{
    public class ExportService : IExportService
    {
        public byte[] ExportScheduleToExcelFile(IEnumerable<Schedule> schedules)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("schedules");
            var currentRow = 1;
            var groupByMachineTools = schedules.GroupBy(x => x.MachineToolId);
            var machineTools = groupByMachineTools
                .Select(x => x.First().MachineTool)
                .OrderBy(x => x.Id)
                .ToArray();
            for (var i = 0; i < machineTools.Length; i++)
            {
                worksheet.Cell(currentRow, i + 2).Value = machineTools[i].Name;
            }
            
            var groupByParties = schedules.GroupBy(x => x.PartyId);
            foreach (var groupByParty in groupByParties)
            {
                currentRow++;
                var currentColumn = 1;
                var party = groupByParty.First().Party;
                worksheet.Cell(currentRow, currentColumn++).Value = party.Nomenclature.Name;
                foreach (var machineTool in machineTools)
                {
                    var schedule = groupByParty.FirstOrDefault(x => x.MachineToolId == machineTool.Id);
                    if (schedule != null)
                    {
                        worksheet.Cell(currentRow, currentColumn++).Value = $"{schedule.DateTimeFrom:hh:mm} - {schedule.DateTimeTo:hh:mm}";
                    }
                    else
                    {
                        currentColumn++;
                    }
                }
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }
        
    }
}