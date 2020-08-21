using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NumericalSimulation.Entities;
using NumericalSimulation.Entities.Enums;
using NumericalSimulation.Interfaces;
using NumericalSimulation.Services.CalculationSchedule;

namespace NumericalSimulation.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ICacheService _cacheService;
        private readonly IDataReader _dataReader;

        public SimulationService(ICacheService cacheService, IDataReader dataReader)
        {
            _cacheService = cacheService;
            _dataReader = dataReader;
        }

        public async Task<ErrorLoadFileEnum> SetInputData(IFormFile formFile, InputDataTypeEnum type, Guid sessionId)
        {
            var userInputData = _cacheService.GetUserInputData(sessionId);
            try
            {
                if (userInputData == null)
                {
                    userInputData = await CreateNewUserInputData(formFile, type);
                }
                else
                {
                    userInputData = await UpdateUserInputData(formFile, type, userInputData);
                }
            }
            catch (Exception e)
            {
                return ErrorLoadFileEnum.BadFileFormat;
            }
            
            _cacheService.AddOrUpdateNewUserInputData(userInputData, sessionId);
            return ErrorLoadFileEnum.Ok;
        }

        public IEnumerable<Schedule> GetScheduleByType(Guid sessionIdGuid,
            ScheduleAlgorithmTypeEnum algorithmType)
        {
            var inputData = _cacheService.GetUserInputData(sessionIdGuid);
            if (inputData == null)
            {
                throw new ArgumentNullException(nameof(inputData));
            }
            var parties = BindUserInputDataToParties(inputData);
            ICalculationScheduleService calculationScheduleService;
            switch (algorithmType)
            {
                case ScheduleAlgorithmTypeEnum.Simple:
                    calculationScheduleService = new SimpleCalculationScheduleService();
                    break;
                case ScheduleAlgorithmTypeEnum.PetrovSokolicinAlgorithm:
                    calculationScheduleService = new PetrovSokolicinCalculationScheduleService();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }
            var schedules = calculationScheduleService.GetSchedule(parties, inputData.MachineToolsList);
            return schedules;
        }

        private IEnumerable<Party> BindUserInputDataToParties(CacheUserInputData data)
        {
            var executeTimesListWithMT = new List<ExecuteTime>();
            foreach (var machineTool in data.MachineToolsList)
            {
                var executeTimesList = data.ExecuteTimesList.Where(x => x.MachineToolId == machineTool.Id);
                executeTimesListWithMT.AddRange(executeTimesList.Select(x => new ExecuteTime
                {
                    MachineToolId = machineTool.Id,
                    MachineTool = machineTool,
                    NomenclatureId = x.NomenclatureId,
                    OperationTime = x.OperationTime
                }));
            }

            var nomenclatureWithExecuteTime = new List<Nomenclature>();
            var groupingExecuteTimesListWithMT = executeTimesListWithMT.GroupBy(x => x.NomenclatureId);
            foreach (var group in groupingExecuteTimesListWithMT)
            {
                var nomenclatureList = data.NomenclaturesList.Where(x => x.Id == group.Key);

                nomenclatureWithExecuteTime.AddRange(nomenclatureList.Select(x => new Nomenclature
                {
                    Id = x.Id,
                    Name = x.Name,
                    ExecuteTimeList = group.ToList()
                }));
            }

            var result = new List<Party>();
            foreach (var nomenclature in nomenclatureWithExecuteTime)
            {
                var partiesList = data.PartiesList.Where(x => x.NomenclatureId == nomenclature.Id);
                result.AddRange(partiesList.Select(x => new Party
                {
                    Id = x.Id,
                    NomenclatureId = nomenclature.Id,
                    Nomenclature = nomenclature
                }));
            }

            return result;
        }

        private async Task<CacheUserInputData> UpdateUserInputData(IFormFile formFile, InputDataTypeEnum type,
            CacheUserInputData userInputData)
        {
            switch (type)
            {
                case InputDataTypeEnum.MachineTool:
                {
                    userInputData.MachineToolsList = await _dataReader.ReadMachineToolsList(formFile);
                    break;
                }
                case InputDataTypeEnum.Nomenclature:
                {
                    userInputData.NomenclaturesList = await _dataReader.ReadNomenclaturesList(formFile);
                    break;
                }
                case InputDataTypeEnum.Party:
                {
                    userInputData.PartiesList = await _dataReader.ReadPartiesList(formFile);
                    break;
                }
                case InputDataTypeEnum.ExecuteTime:
                {
                    userInputData.ExecuteTimesList = await _dataReader.ReadExecuteTimesList(formFile);
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return userInputData;
        }
        
        private async Task<CacheUserInputData> CreateNewUserInputData(IFormFile formFile, InputDataTypeEnum type)
        {
            var userInputData = new CacheUserInputData();
            switch (type)
            {
                case InputDataTypeEnum.MachineTool:
                {
                    userInputData.MachineToolsList = await _dataReader.ReadMachineToolsList(formFile);
                    break;
                }
                case InputDataTypeEnum.Nomenclature:
                {
                    userInputData.NomenclaturesList = await _dataReader.ReadNomenclaturesList(formFile);
                    break;
                }
                case InputDataTypeEnum.Party:
                {
                    userInputData.PartiesList = await _dataReader.ReadPartiesList(formFile);
                    break;
                }
                case InputDataTypeEnum.ExecuteTime:
                {
                    userInputData.ExecuteTimesList = await _dataReader.ReadExecuteTimesList(formFile);
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return userInputData;
        }
    }
}