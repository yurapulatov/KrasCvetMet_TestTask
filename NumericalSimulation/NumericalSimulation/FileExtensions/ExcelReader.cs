using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using NumericalSimulation.Entities;
using NumericalSimulation.Interfaces;

namespace NumericalSimulation.FileExtensions
{
    public class ExcelReader : IDataReader
    {
        public async Task<IEnumerable<MachineTool>> ReadMachineToolsList(IFormFile formFile)
        {
            var result = new List<MachineTool>();
            await using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);
            int? positionId = null, positionName = null;
            while (reader.Read()) //Each row of the file
            {
                if (positionId == null || positionName == null)
                {
                    var columnNames = new List<string>();
                    columnNames = GetColumnNameFromFile(reader, 2);
                    if (!columnNames.Any())
                    {
                        throw new ArgumentException("Файл пустой!");
                    }
                    for (var i = 0; i < columnNames.Count; i++)
                    {
                        switch (columnNames[i])
                        {
                            case "id":
                            {
                                positionId = i;
                                break;
                            }
                            case "name":
                            {
                                positionName = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var newMachineTool = new MachineTool
                    {
                        Id = int.Parse(reader.GetValue(positionId.Value).ToString()),
                        Name = reader.GetValue(positionName.Value).ToString()
                    };
                    result.Add(newMachineTool);
                }
            }

            return result;
        }

        public async Task<IEnumerable<Party>> ReadPartiesList(IFormFile formFile)
        {
            var result = new List<Party>();
            await using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);
            int? positionId = null, positionNomenclatureId = null;
            while (reader.Read()) //Each row of the file
            {
                if (positionId == null || positionNomenclatureId == null)
                {
                    var columnNames = new List<string>();
                    columnNames = GetColumnNameFromFile(reader, 2);
                    if (!columnNames.Any())
                    {
                        throw new ArgumentException("Файл пустой!");
                    }
                    for (var i = 0; i < columnNames.Count; i++)
                    {
                        switch (columnNames[i])
                        {
                            case "id":
                            {
                                positionId = i;
                                break;
                            }
                            case "nomenclature id":
                            {
                                positionNomenclatureId = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var newParty = new Party
                    {
                        Id = int.Parse(reader.GetValue(positionId.Value).ToString()),
                        NomenclatureId = int.Parse(reader.GetValue(positionNomenclatureId.Value).ToString())
                    };
                    result.Add(newParty);
                }
            }

            return result;
        }

        public async Task<IEnumerable<Nomenclature>> ReadNomenclaturesList(IFormFile formFile)
        {
            var result = new List<Nomenclature>();
            await using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);
            int? positionId = null, positionName = null;
            while (reader.Read()) //Each row of the file
            {
                if (positionId == null || positionName == null)
                {
                    var columnNames = new List<string>();
                    columnNames = GetColumnNameFromFile(reader, 2);
                    if (!columnNames.Any())
                    {
                        throw new ArgumentException("Файл пустой!");
                    }
                    for (var i = 0; i < columnNames.Count; i++)
                    {
                        switch (columnNames[i])
                        {
                            case "id":
                            {
                                positionId = i;
                                break;
                            }
                            case "nomenclature":
                            {
                                positionName = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var newNomenclature = new Nomenclature
                    {
                        Id = int.Parse(reader.GetValue(positionId.Value).ToString()),
                        Name = reader.GetValue(positionName.Value).ToString()
                    };
                    result.Add(newNomenclature);
                }

            }

            return result;
        }

        public async Task<IEnumerable<ExecuteTime>> ReadExecuteTimesList(IFormFile formFile)
        {
            var result = new List<ExecuteTime>();
            await using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);
            int? positionMachineToolId = null, positionOperationTime = null, positionNomenclatureId = null;
            while (reader.Read()) //Each row of the file
            {
                if (positionMachineToolId == null || positionOperationTime == null || positionNomenclatureId == null)
                {
                    var columnNames = new List<string>();
                    columnNames = GetColumnNameFromFile(reader, 3);
                    if (!columnNames.Any())
                    {
                        throw new ArgumentException("Файл пустой!");
                    }
                    for (var i = 0; i < columnNames.Count; i++)
                    {
                        switch (columnNames[i])
                        {
                            case "machine tool id":
                            {
                                positionMachineToolId = i;
                                break;
                            }
                            case "operation time":
                            {
                                positionOperationTime = i;
                                break;
                            }
                            case "nomenclature id":
                            {
                                positionNomenclatureId = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var executeTime = new ExecuteTime
                    {
                        MachineToolId = int.Parse(reader.GetValue(positionMachineToolId.Value).ToString()),
                        NomenclatureId = int.Parse(reader.GetValue(positionNomenclatureId.Value).ToString()),
                        OperationTime = int.Parse(reader.GetValue(positionOperationTime.Value).ToString())
                    };
                    result.Add(executeTime);
                }
            }

            return result;
        }

        private List<string> GetColumnNameFromFile(IExcelDataReader reader, int countColumns)
        {
            var result = new List<string>();
            for (int i = 0; i < countColumns; i++)
            {
                result.Add(reader.GetValue(i).ToString());
            }

            return result;
        }
    }
}