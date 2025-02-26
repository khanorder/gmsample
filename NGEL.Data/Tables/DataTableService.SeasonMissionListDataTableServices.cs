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
    public class SeasonMissionListDataTableService
    {
        private string tableName = "SeasonMissionListDataTable";
        private string csvFileName = "tb_SeasonMissionList";
        private readonly ILogger<SeasonMissionListDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.SeasonMissionListData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.SeasonMissionListData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.SeasonMissionListData> datas { get => _datas; }
        private object lockObject = new object();

        public SeasonMissionListDataTableService(ILogger<SeasonMissionListDataTableService> logger)
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
                csv.Context.RegisterClassMap<SeasonMissionListDataMap>();
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

                    bool Enable = false;
                    try
                    {
                        var fieldEnable = csv.GetField("Enable");
                        if (null != fieldEnable)
                            Enable = fieldEnable.ToLower() == "true" || fieldEnable == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Enable.");
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

                    bool IsTargetName = false;
                    try
                    {
                        var fieldIsTargetName = csv.GetField("IsTargetName");
                        if (null != fieldIsTargetName)
                            IsTargetName = fieldIsTargetName.ToLower() == "true" || fieldIsTargetName == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse IsTargetName.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ReqHeroID = 0;
                    try
                    {
                        var fieldReqHeroID = csv.GetField("ReqHeroID");
                        if (null != fieldReqHeroID)
                            ReqHeroID = int.Parse(fieldReqHeroID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReqHeroID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EMissionType MissionType = 0;
                    try
                    {
                        var fieldMissionType = csv.GetField("MissionType");
                        if (null != fieldMissionType)
                            MissionType = Enum.Parse<EMissionType>(fieldMissionType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MissionType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int MissionValue = 0;
                    try
                    {
                        var fieldMissionValue = csv.GetField("MissionValue");
                        if (null != fieldMissionValue)
                            MissionValue = int.Parse(fieldMissionValue);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MissionValue.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int MissionCount = 0;
                    try
                    {
                        var fieldMissionCount = csv.GetField("MissionCount");
                        if (null != fieldMissionCount)
                            MissionCount = int.Parse(fieldMissionCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MissionCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var Description = csv.GetField("Description") ?? "";
                    if (null == Description)
                    {
                        Description = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Description is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Description))
                    {
                        Description = "";
                    }

                    Int64 RewardExp = 0;
                    try
                    {
                        var fieldRewardExp = csv.GetField("RewardExp");
                        if (null != fieldRewardExp)
                            RewardExp = Int64.Parse(fieldRewardExp);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardExp.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.SeasonMissionListData
                    {
                        ID = ID,
                        Enable = Enable,
                        MissionGroup = MissionGroup,
                        IsTargetName = IsTargetName,
                        ReqHeroID = ReqHeroID,
                        MissionType = MissionType,
                        MissionValue = MissionValue,
                        MissionCount = MissionCount,
                        Description = Description,
                        RewardExp = RewardExp,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
