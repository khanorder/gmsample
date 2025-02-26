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
    public class WonderCubeRwardDataTableService
    {
        private string tableName = "WonderCubeRwardDataTable";
        private string csvFileName = "tb_WonderCubeReward";
        private readonly ILogger<WonderCubeRwardDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.WonderCubeRwardData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.WonderCubeRwardData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.WonderCubeRwardData> datas { get => _datas; }
        private object lockObject = new object();

        public WonderCubeRwardDataTableService(ILogger<WonderCubeRwardDataTableService> logger)
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
                csv.Context.RegisterClassMap<WonderCubeRwardDataMap>();
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

                    int RewardGroup = 0;
                    try
                    {
                        var fieldRewardGroup = csv.GetField("RewardGroup");
                        if (null != fieldRewardGroup)
                            RewardGroup = int.Parse(fieldRewardGroup);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardGroup.");
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

                    Int64 RewardID = 0;
                    try
                    {
                        var fieldRewardID = csv.GetField("RewardID");
                        if (null != fieldRewardID)
                            RewardID = Int64.Parse(fieldRewardID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
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

                    Int64 RewardProb = 0;
                    try
                    {
                        var fieldRewardProb = csv.GetField("RewardProb");
                        if (null != fieldRewardProb)
                            RewardProb = Int64.Parse(fieldRewardProb);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardProb.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.WonderCubeRwardData
                    {
                        ID = ID,
                        Enable = Enable,
                        RewardGroup = RewardGroup,
                        RewardType = RewardType,
                        RewardID = RewardID,
                        RewardCount = RewardCount,
                        RewardProb = RewardProb,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
