using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NumericalSimulation.Entities;

namespace NumericalSimulation.Interfaces
{
    public interface IDataReader
    {
        public Task<IEnumerable<MachineTool>> ReadMachineToolsList(IFormFile formFile);
        public Task<IEnumerable<Party>> ReadPartiesList(IFormFile formFile);
        public Task<IEnumerable<Nomenclature>> ReadNomenclaturesList(IFormFile formFile);
        public Task<IEnumerable<ExecuteTime>> ReadExecuteTimesList(IFormFile formFile);
    }
}