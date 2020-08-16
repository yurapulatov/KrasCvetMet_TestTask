using System;

namespace NumericalSimulation.Entities
{
    public class Schedule
    {
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public long PartyId { get; set; }
        public Party Party { get; set; }
    }
}