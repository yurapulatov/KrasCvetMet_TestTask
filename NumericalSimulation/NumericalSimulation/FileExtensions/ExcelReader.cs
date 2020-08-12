using System.Collections.Generic;
using System.Threading.Tasks;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.FileExtensions
{
    public class ExcelReader : IDataReader
    {
        public async Task<IEnumerable<MachineTool>> ReadMachineToolsList()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Party>> ReadPartiesList()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Nomenclature>> ReadNomenclaturesList()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ExecuteTime>> ReadExecuteTimesList()
        {
            throw new System.NotImplementedException();
        }
        
    }
}