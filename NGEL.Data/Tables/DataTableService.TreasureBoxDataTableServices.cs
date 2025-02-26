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
    public class TreasureBoxDataTableService
    {
        private string tableName = "TreasureBoxDataTable";
        private string csvFileName = "tb_TreasureBox";
        private readonly ILogger<TreasureBoxDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.TreasureBoxData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.TreasureBoxData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.TreasureBoxData> datas { get => _datas; }
        private object lockObject = new object();

        public TreasureBoxDataTableService(ILogger<TreasureBoxDataTableService> logger)
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
                csv.Context.RegisterClassMap<TreasureBoxDataMap>();
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

                    List<EUserAssetType> AssetType = new List<EUserAssetType>();
                    try
                    {
                        var fieldAssetType = csv.GetField("AssetType");
                        if (null != fieldAssetType)
                        {
                            var listDataString = fieldAssetType.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Enum.Parse<EUserAssetType>(item, true);
                                AssetType.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<Int64> AssetCount = new List<Int64>();
                    try
                    {
                        var fieldAssetCount = csv.GetField("AssetCount");
                        if (null != fieldAssetCount)
                        {
                            var listDataString = fieldAssetCount.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Int64.Parse(item);
                                AssetCount.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<Int64> RewardItemID = new List<Int64>();
                    try
                    {
                        var fieldRewardItemID = csv.GetField("RewardItemID");
                        if (null != fieldRewardItemID)
                        {
                            var listDataString = fieldRewardItemID.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Int64.Parse(item);
                                RewardItemID.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardItemID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<Int64> RewardItemCount = new List<Int64>();
                    try
                    {
                        var fieldRewardItemCount = csv.GetField("RewardItemCount");
                        if (null != fieldRewardItemCount)
                        {
                            var listDataString = fieldRewardItemCount.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Int64.Parse(item);
                                RewardItemCount.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardItemCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var Location = csv.GetField("Location") ?? "";
                    if (null == Location)
                    {
                        Location = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Location is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Location))
                    {
                        Location = "";
                    }

                    var Rotation = csv.GetField("Rotation") ?? "";
                    if (null == Rotation)
                    {
                        Rotation = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Rotation is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Rotation))
                    {
                        Rotation = "";
                    }

                    var Scale = csv.GetField("Scale") ?? "";
                    if (null == Scale)
                    {
                        Scale = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Scale is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Scale))
                    {
                        Scale = "";
                    }

                    var data = new NGEL.Data.Tables.Models.TreasureBoxData
                    {
                        ID = ID,
                        AssetType = AssetType,
                        AssetCount = AssetCount,
                        RewardItemID = RewardItemID,
                        RewardItemCount = RewardItemCount,
                        Location = Location,
                        Rotation = Rotation,
                        Scale = Scale,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
