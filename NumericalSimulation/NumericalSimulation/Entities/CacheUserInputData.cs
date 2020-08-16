using System.Collections.Generic;

namespace NumericalSimulation.Entities
{
    public class CacheUserInputData
    {
        public IEnumerable<ExecuteTime> ExecuteTimesList { get; set; }
        public IEnumerable<MachineTool> MachineToolsList { get; set; }
        public IEnumerable<Nomenclature> NomenclaturesList { get; set; }
        public IEnumerable<Party> PartiesList { get; set; }
    }
}