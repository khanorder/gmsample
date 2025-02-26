using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Logging;
using NGEL.Data.Tables.Models;
using NGEL.Data.Helpers;
using Newtonsoft.Json;
using Lobby;

namespace NGEL.Data.Tables
{
    public class MissionDataTableService
    {
        private string tableName = "MissionDataTable";
        private string csvFileName = "tb_Mission";
        private readonly ILogger<MissionDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.MissionData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.MissionData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.MissionData> datas { get => _datas; }
        private object lockObject = new object();

        public MissionDataTableService(ILogger<MissionDataTableService> logger)
        {
            _logger = logger;
        }

        public void LoadTable()
        {
            _datas.Clear();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = arg => arg.Header.ToLower(),
                Mode = CsvMode.RFC4180
            };
            
            var csvPath = $"./csv/{csvFileName}.csv";
            if (false == File.Exists(csvPath))
            {
                csvPath = $"./csv_server/{csvFileName}.csv";
            }
            
            if (false == File.Exists(csvPath))
            {
                _logger.LogError($"Not Found csv File. ({csvPath})");
                return;
            }
            
            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, config))
            {
                _logger.LogInformation($"Load {tableName} CSV file: {csvPath}", ConsoleColor.Magenta, ConsoleColor.White);
                csv.Context.RegisterClassMap<MissionDataMap>();
                csv.Read();
                csv.ReadHeader();
                Int64 rowIndex = 0;

                while (csv.Read())
                {
                    var firstFiled = csv.GetField(0);
                    if (null != firstFiled && firstFiled.StartsWith("Version"))
                    {
                        var versionString = firstFiled.Replace("Version:", "");
                        try
                        {
                            _version = double.Parse(versionString);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"{tableName}({rowIndex}): NotFound Version Information.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            break;
                        }
                        continue;
                    }

                    int ID = 0;
                    try
                    {
                        var fieldID = csv.GetField("ID");
                        if (null != fieldID)
                            ID = int.Parse(fieldID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EMissionGroup MissionGroup = 0;
                    try
                    {
                        var fieldMissionGroup = csv.GetField("MissionGroup");
                        if (null != fieldMissionGroup)
                            MissionGroup = Enum.Parse<EMissionGroup>(fieldMissionGroup, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MissionGroup.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int EXPGroup = 0;
                    try
                    {
                        var fieldEXPGroup = csv.GetField("EXPGroup");
                        if (null != fieldEXPGroup)
                            EXPGroup = int.Parse(fieldEXPGroup);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse EXPGroup.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int CreateCount = 0;
                    try
                    {
                        var fieldCreateCount = csv.GetField("CreateCount");
                        if (null != fieldCreateCount)
                            CreateCount = int.Parse(fieldCreateCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CreateCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    bool UseDate = false;
                    try
                    {
                        var fieldUseDate = csv.GetField("UseDate");
                        if (null != fieldUseDate)
                            UseDate = fieldUseDate.ToLower() == "true" || fieldUseDate == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse UseDate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    DateTime StartDate = DateTime.Now;
                    try
                    {
                        var fieldStartDate = csv.GetField("StartDate");
                        if (null != fieldStartDate)
                        {
                            if (Regex.Match(fieldStartDate, @"^[0-9]{4}\.{1}[0-9]{2}\.{1}[0-9]{2}\-{1}[0-9]{2}\.{1}[0-9]{2}\.{1}[0-9]{2}$").Success)
                            {
                                var dateStringArr = fieldStartDate.Split("-");
                                if (1 < dateStringArr.Length)
                                    fieldStartDate = dateStringArr[0].Replace(".", " ") + " " + dateStringArr[1].Replace(".", ":");
                            }
                            StartDate = DateTime.Parse(fieldStartDate);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse StartDate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    DateTime EndDate = DateTime.Now;
                    try
                    {
                        var fieldEndDate = csv.GetField("EndDate");
                        if (null != fieldEndDate)
                        {
                            if (Regex.Match(fieldEndDate, @"^[0-9]{4}\.{1}[0-9]{2}\.{1}[0-9]{2}\-{1}[0-9]{2}\.{1}[0-9]{2}\.{1}[0-9]{2}$").Success)
                            {
                                var dateStringArr = fieldEndDate.Split("-");
                                if (1 < dateStringArr.Length)
                                    fieldEndDate = dateStringArr[0].Replace(".", " ") + " " + dateStringArr[1].Replace(".", ":");
                            }
                            EndDate = DateTime.Parse(fieldEndDate);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse EndDate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.MissionData
                    {
                        ID = ID,
                        MissionGroup = MissionGroup,
                        EXPGroup = EXPGroup,
                        CreateCount = CreateCount,
                        UseDate = UseDate,
                        StartDate = StartDate,
                        EndDate = EndDate,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
