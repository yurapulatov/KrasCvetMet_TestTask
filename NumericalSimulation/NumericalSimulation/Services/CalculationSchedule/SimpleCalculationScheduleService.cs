using System;
using System.Collections.Generic;
using System.Linq;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.Services.CalculationSchedule
{
    public class SimpleCalculationScheduleService : ICalculationScheduleService
    {
        public IEnumerable<Schedule> GetSchedule(IEnumerable<Party> parties, IEnumerable<MachineTool> machineToolCount)
        {
            var result = new List<Schedule>();
            var machineToolCurrentTimeDictionary = machineToolCount.ToDictionary(x => x.Id, x => DateTime.MinValue);
            foreach (var party in parties)
            {
                DateTime? beginDateForParty = null;
                DateTime? endDateForParty = null;
                foreach (var item in party.Nomenclature.ExecuteTimeList)
                {
                    var machineToolCurrentTime = machineToolCurrentTimeDictionary[item.MachineToolId];
                    if (beginDateForParty == null || endDateForParty == null)
                    {
                        beginDateForParty = machineToolCurrentTime;
                        endDateForParty = machineToolCurrentTime.AddMinutes(Convert.ToDouble(item.OperationTime));
                    }
                    else
                    {
                        if (endDateForParty > machineToolCurrentTime)
                        {
                            beginDateForParty = endDateForParty;
                            endDateForParty = endDateForParty.Value.AddMinutes(Convert.ToDouble(item.OperationTime));
                        }
                        else
                        {
                            beginDateForParty = machineToolCurrentTime;
                            endDateForParty = machineToolCurrentTime.AddMinutes(Convert.ToDouble(item.OperationTime));
                        }
                    }
                    var schedule = new Schedule
                    {
                        DateTimeFrom = beginDateForParty.Value,
                        DateTimeTo = endDateForParty.Value,
                        PartyId = party.Id,
                        Party = party,
                        MachineTool = item.MachineTool,
                        MachineToolId = item.NomenclatureId
                    };
                    result.Add(schedule);
                    machineToolCurrentTimeDictionary[item.MachineToolId] = endDateForParty.Value;
                }
            }
            return result;
        }
    }
}