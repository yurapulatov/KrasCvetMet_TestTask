using System.Collections.Generic;

namespace NumericalSimulation.Entities
{
    public class Nomenclature
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ExecuteTime> ExecuteTimeList { get; set; }
    }
}