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
    public class GuideMissionDataTableService
    {
        private string tableName = "GuideMissionDataTable";
        private string csvFileName = "tb_GuideMission";
        private readonly ILogger<GuideMissionDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.GuideMissionData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.GuideMissionData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.GuideMissionData> datas { get => _datas; }
        private object lockObject = new object();

        public GuideMissionDataTableService(ILogger<GuideMissionDataTableService> logger)
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
                csv.Context.RegisterClassMap<GuideMissionDataMap>();
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

                    Int64 ID = 0;
                    try
                    {
                        var fieldID = csv.GetField("ID");
                        if (null != fieldID)
                            ID = Int64.Parse(fieldID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EGuideMissionCategory GuideMissionCategory = 0;
                    try
                    {
                        var fieldGuideMissionCategory = csv.GetField("GuideMissionCategory");
                        if (null != fieldGuideMissionCategory)
                            GuideMissionCategory = Enum.Parse<EGuideMissionCategory>(fieldGuideMissionCategory, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse GuideMissionCategory.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EGuideMissionType MissionType = 0;
                    try
                    {
                        var fieldMissionType = csv.GetField("MissionType");
                        if (null != fieldMissionType)
                            MissionType = Enum.Parse<EGuideMissionType>(fieldMissionType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MissionType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int Step = 0;
                    try
                    {
                        var fieldStep = csv.GetField("Step");
                        if (null != fieldStep)
                            Step = int.Parse(fieldStep);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Step.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 NextStepMissionID = 0;
                    try
                    {
                        var fieldNextStepMissionID = csv.GetField("NextStepMissionID");
                        if (null != fieldNextStepMissionID)
                            NextStepMissionID = Int64.Parse(fieldNextStepMissionID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse NextStepMissionID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var Title = csv.GetField("Title") ?? "";
                    if (null == Title)
                    {
                        Title = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Title is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Title))
                    {
                        Title = "";
                    }

                    Int64 TrackingID = 0;
                    try
                    {
                        var fieldTrackingID = csv.GetField("TrackingID");
                        if (null != fieldTrackingID)
                            TrackingID = Int64.Parse(fieldTrackingID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse TrackingID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    ERewardType RewardType = 0;
                    try
                    {
                        var fieldRewardType = csv.GetField("RewardType");
                        if (null != fieldRewardType)
                            RewardType = Enum.Parse<ERewardType>(fieldRewardType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var RewardID = csv.GetField("RewardID") ?? "";
                    if (null == RewardID)
                    {
                        RewardID = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member RewardID is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(RewardID))
                    {
                        RewardID = "";
                    }

                    Int64 RewardCount = 0;
                    try
                    {
                        var fieldRewardCount = csv.GetField("RewardCount");
                        if (null != fieldRewardCount)
                            RewardCount = Int64.Parse(fieldRewardCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.GuideMissionData
                    {
                        ID = ID,
                        GuideMissionCategory = GuideMissionCategory,
                        MissionType = MissionType,
                        Step = Step,
                        NextStepMissionID = NextStepMissionID,
                        Title = Title,
                        TrackingID = TrackingID,
                        RewardType = RewardType,
                        RewardID = RewardID,
                        RewardCount = RewardCount,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
