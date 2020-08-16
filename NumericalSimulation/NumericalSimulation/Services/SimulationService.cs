using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NumericalSimulation.Entities;
using NumericalSimulation.Entities.Enums;
using NumericalSimulation.Interfaces;

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

        public async Task SetInputData(IFormFile formFile, InputDataTypeEnum type, Guid sessionId)
        {
            var userInputData = _cacheService.GetUserInputData(sessionId);
            if (userInputData == null)
            {
                userInputData = await CreateNewUserInputData(formFile, type);
            }
            else
            {
                userInputData = await UpdateUserInputData(formFile, type, userInputData);
            }
            _cacheService.AddOrUpdateNewUserInputData(userInputData, sessionId);
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