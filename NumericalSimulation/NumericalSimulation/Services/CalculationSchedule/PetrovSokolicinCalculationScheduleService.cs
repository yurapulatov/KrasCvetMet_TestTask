using System;
using System.Collections.Generic;
using System.Linq;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Services.CalculationSchedule
{
    public class PetrovSokolicinCalculationScheduleService : ICalculationScheduleService
    {
        public IEnumerable<Schedule> GetSchedule(IEnumerable<Party> parties, IEnumerable<MachineTool> machineTools)
        {
            var orderedPartiesWithoutLast = OrderedPartiesBySumFirstsWithoutLastElement(parties, machineTools);
            var orderedPartiesWithoutFirst = OrderedPartiesBySumLastsWithoutFirstElement(parties, machineTools);
            var orderedPartiesByDivisionFirstAndLast = OrderedPartiesByDivisionFirstAndLast(parties, machineTools);
            var simpleCalculationScheduleService = new SimpleCalculationScheduleService();
            var schedule1 = simpleCalculationScheduleService.GetSchedule(orderedPartiesWithoutLast, machineTools);
            var schedule2 = simpleCalculationScheduleService.GetSchedule(orderedPartiesWithoutFirst, machineTools);
            var schedule3 = simpleCalculationScheduleService.GetSchedule(orderedPartiesByDivisionFirstAndLast, machineTools);
            var maxTimeSchedule1 = schedule1.Max(x => x.DateTimeTo);
            var maxTimeSchedule2 = schedule2.Max(x => x.DateTimeTo);
            var maxTimeSchedule3 = schedule3.Max(x => x.DateTimeTo);
            if (maxTimeSchedule3 <= maxTimeSchedule2 && maxTimeSchedule3 <= maxTimeSchedule1)
            {
                return schedule3;
            }
            if (maxTimeSchedule2 <= maxTimeSchedule3 && maxTimeSchedule2 <= maxTimeSchedule1)
            {
                return schedule2;
            }
            else
            {
                return schedule1;
            }
        }

        private IEnumerable<Party> OrderedPartiesByDivisionFirstAndLast(IEnumerable<Party> parties, IEnumerable<MachineTool> machineTools)
        {
            var orderedMachineTools = machineTools.OrderBy(x => x.Id).ToList();
            var firstMachineTool = orderedMachineTools.First();
            var lastMachineTool = orderedMachineTools.Last();
            var listPartyWithTimeExecution = new List<Tuple<Party, decimal>>();
            foreach (var party in parties)
            {
                decimal partyTimeExecution = 0;
                var executeTimeFirstTool = party.Nomenclature.ExecuteTimeList.FirstOrDefault(x => x.MachineToolId == firstMachineTool.Id)?.OperationTime ?? 0;
                var executeTimeLastTool = party.Nomenclature.ExecuteTimeList.FirstOrDefault(x => x.MachineToolId == lastMachineTool.Id)?.OperationTime ?? 0;
                partyTimeExecution = executeTimeLastTool - executeTimeFirstTool;
                listPartyWithTimeExecution.Add(new Tuple<Party, decimal>(party, partyTimeExecution));
            }
            var result = listPartyWithTimeExecution.OrderBy(x => x.Item2).Select(x => x.Item1);
            return result;
        }


        private IEnumerable<Party> OrderedPartiesBySumFirstsWithoutLastElement(IEnumerable<Party> parties, IEnumerable<MachineTool> machineTools)
        {
            return OrderedPartiesBySumWithoutElement(parties, machineTools, machineTools.Count() - 1);
        }
        
        private IEnumerable<Party> OrderedPartiesBySumLastsWithoutFirstElement(IEnumerable<Party> parties, IEnumerable<MachineTool> machineTools)
        {
            return OrderedPartiesBySumWithoutElement(parties, machineTools, 0);
        }
        
        private IEnumerable<Party> OrderedPartiesBySumWithoutElement(IEnumerable<Party> parties,
            IEnumerable<MachineTool> machineTools, int withoutElement)
        {

            var machineToolsWithoutLast = machineTools.OrderBy(x => x.Id).ToList();
            machineToolsWithoutLast.RemoveAt(withoutElement);
            var listPartyWithTimeExecution = new List<Tuple<Party, decimal>>();
            foreach (var party in parties)
            {
                decimal partyTimeExecution = 0;
                foreach (var machineTool in machineToolsWithoutLast)
                {
                    var executeTime = party.Nomenclature.ExecuteTimeList.FirstOrDefault(x => x.MachineToolId == machineTool.Id);
                    partyTimeExecution += executeTime?.OperationTime ?? 0;
                }
                listPartyWithTimeExecution.Add(new Tuple<Party, decimal>(party, partyTimeExecution));
            }

            var result = listPartyWithTimeExecution.OrderBy(x => x.Item2).Select(x => x.Item1);
            return result;
        }

        
    }
}