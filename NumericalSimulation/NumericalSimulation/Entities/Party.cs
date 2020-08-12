using System.Collections.Generic;

namespace NumericalSimulation.Entities
{
    public class Party
    {
        public long Id { get; set; }
        public long NomenclatureId { get; set; }
        public List<Nomenclature> Nomenclatures { get; set; }
    }
}