namespace NumericalSimulation.Entities
{
    public class ExecuteTime
    {
        public long MachineToolId { get; set; }
        public MachineTool MachineTool { get; set; }
        public long NomenclatureId { get; set; }
        public decimal OperationTime { get; set; }
    }
}