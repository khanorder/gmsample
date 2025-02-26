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
    public class AttendanceRewardDataTableService
    {
        private string tableName = "AttendanceRewardDataTable";
        private string csvFileName = "tb_AttendanceReward";
        private readonly ILogger<AttendanceRewardDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.AttendanceRewardData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.AttendanceRewardData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.AttendanceRewardData> datas { get => _datas; }
        private object lockObject = new object();

        public AttendanceRewardDataTableService(ILogger<AttendanceRewardDataTableService> logger)
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
                csv.Context.RegisterClassMap<AttendanceRewardDataMap>();
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

                    int GroupIndex = 0;
                    try
                    {
                        var fieldGroupIndex = csv.GetField("GroupIndex");
                        if (null != fieldGroupIndex)
                            GroupIndex = int.Parse(fieldGroupIndex);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse GroupIndex.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EUserAssetType AssetType = 0;
                    try
                    {
                        var fieldAssetType = csv.GetField("AssetType");
                        if (null != fieldAssetType)
                            AssetType = Enum.Parse<EUserAssetType>(fieldAssetType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 AssetCount = 0;
                    try
                    {
                        var fieldAssetCount = csv.GetField("AssetCount");
                        if (null != fieldAssetCount)
                            AssetCount = Int64.Parse(fieldAssetCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ItemID = 0;
                    try
                    {
                        var fieldItemID = csv.GetField("ItemID");
                        if (null != fieldItemID)
                            ItemID = int.Parse(fieldItemID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemCount = 0;
                    try
                    {
                        var fieldItemCount = csv.GetField("ItemCount");
                        if (null != fieldItemCount)
                            ItemCount = Int64.Parse(fieldItemCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int HeroID = 0;
                    try
                    {
                        var fieldHeroID = csv.GetField("HeroID");
                        if (null != fieldHeroID)
                            HeroID = int.Parse(fieldHeroID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse HeroID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.AttendanceRewardData
                    {
                        ID = ID,
                        RewardGroup = RewardGroup,
                        GroupIndex = GroupIndex,
                        AssetType = AssetType,
                        AssetCount = AssetCount,
                        ItemID = ItemID,
                        ItemCount = ItemCount,
                        HeroID = HeroID,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
