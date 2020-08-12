using System.Collections.Generic;
using System.Threading.Tasks;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface IDataReader
    {
        public Task<IEnumerable<MachineTool>> ReadMachineToolsList();
        public Task<IEnumerable<Party>> ReadPartiesList();
        public Task<IEnumerable<Nomenclature>> ReadNomenclaturesList();
        public Task<IEnumerable<ExecuteTime>> ReadExecuteTimesList();
    }
}