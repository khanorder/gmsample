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
    public class GuideMissionStepRewardTableService
    {
        private string tableName = "GuideMissionStepRewardTable";
        private string csvFileName = "tb_GuideMissionStepReward";
        private readonly ILogger<GuideMissionStepRewardTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.GuideMissionStepReward> _datas { get; set; } = new List<NGEL.Data.Tables.Models.GuideMissionStepReward>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.GuideMissionStepReward> datas { get => _datas; }
        private object lockObject = new object();

        public GuideMissionStepRewardTableService(ILogger<GuideMissionStepRewardTableService> logger)
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
            
            var csvPath = $"./csv_server/{csvFileName}.csv";
            
            if (false == File.Exists(csvPath))
            {
                _logger.LogError($"Not Found csv File. ({csvPath})");
                return;
            }
            
            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, config))
            {
                _logger.LogInformation($"Load {tableName} CSV file: {csvPath}", ConsoleColor.Magenta, ConsoleColor.White);
                csv.Context.RegisterClassMap<GuideMissionStepRewardMap>();
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

                    Int64 PreviousReward = 0;
                    try
                    {
                        var fieldPreviousReward = csv.GetField("PreviousReward");
                        if (null != fieldPreviousReward)
                            PreviousReward = Int64.Parse(fieldPreviousReward);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PreviousReward.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ReqCompleteMissionCount = 0;
                    try
                    {
                        var fieldReqCompleteMissionCount = csv.GetField("ReqCompleteMissionCount");
                        if (null != fieldReqCompleteMissionCount)
                            ReqCompleteMissionCount = Int64.Parse(fieldReqCompleteMissionCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReqCompleteMissionCount.");
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

                    var GradeType = csv.GetField("GradeType") ?? "";
                    if (null == GradeType)
                    {
                        GradeType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member GradeType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(GradeType))
                    {
                        GradeType = "";
                    }

                    var data = new NGEL.Data.Tables.Models.GuideMissionStepReward
                    {
                        ID = ID,
                        PreviousReward = PreviousReward,
                        ReqCompleteMissionCount = ReqCompleteMissionCount,
                        RewardType = RewardType,
                        RewardID = RewardID,
                        RewardCount = RewardCount,
                        GradeType = GradeType,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
