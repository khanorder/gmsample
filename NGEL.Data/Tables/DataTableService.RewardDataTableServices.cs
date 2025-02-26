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
    public class RewardDataTableService
    {
        private string tableName = "RewardDataTable";
        private string csvFileName = "tb_Reward";
        private readonly ILogger<RewardDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.RewardData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.RewardData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.RewardData> datas { get => _datas; }
        private object lockObject = new object();

        public RewardDataTableService(ILogger<RewardDataTableService> logger)
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
                csv.Context.RegisterClassMap<RewardDataMap>();
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

                    var RewardType = csv.GetField("RewardType") ?? "";
                    if (null == RewardType)
                    {
                        RewardType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member RewardType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(RewardType))
                    {
                        RewardType = "";
                    }

                    int RewardCount = 0;
                    try
                    {
                        var fieldRewardCount = csv.GetField("RewardCount");
                        if (null != fieldRewardCount)
                            RewardCount = int.Parse(fieldRewardCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 CharacterID = 0;
                    try
                    {
                        var fieldCharacterID = csv.GetField("CharacterID");
                        if (null != fieldCharacterID)
                            CharacterID = Int64.Parse(fieldCharacterID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CharacterID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 CostumeID1 = 0;
                    try
                    {
                        var fieldCostumeID1 = csv.GetField("CostumeID1");
                        if (null != fieldCostumeID1)
                            CostumeID1 = Int64.Parse(fieldCostumeID1);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CostumeID1.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 CostumeID2 = 0;
                    try
                    {
                        var fieldCostumeID2 = csv.GetField("CostumeID2");
                        if (null != fieldCostumeID2)
                            CostumeID2 = Int64.Parse(fieldCostumeID2);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CostumeID2.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemID1 = 0;
                    try
                    {
                        var fieldItemID1 = csv.GetField("ItemID1");
                        if (null != fieldItemID1)
                            ItemID1 = Int64.Parse(fieldItemID1);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemID1.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemCount1 = 0;
                    try
                    {
                        var fieldItemCount1 = csv.GetField("ItemCount1");
                        if (null != fieldItemCount1)
                            ItemCount1 = Int64.Parse(fieldItemCount1);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemCount1.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemID2 = 0;
                    try
                    {
                        var fieldItemID2 = csv.GetField("ItemID2");
                        if (null != fieldItemID2)
                            ItemID2 = Int64.Parse(fieldItemID2);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemID2.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemCount2 = 0;
                    try
                    {
                        var fieldItemCount2 = csv.GetField("ItemCount2");
                        if (null != fieldItemCount2)
                            ItemCount2 = Int64.Parse(fieldItemCount2);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemCount2.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemID3 = 0;
                    try
                    {
                        var fieldItemID3 = csv.GetField("ItemID3");
                        if (null != fieldItemID3)
                            ItemID3 = Int64.Parse(fieldItemID3);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemID3.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemCount3 = 0;
                    try
                    {
                        var fieldItemCount3 = csv.GetField("ItemCount3");
                        if (null != fieldItemCount3)
                            ItemCount3 = Int64.Parse(fieldItemCount3);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemCount3.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EUserAssetType AssetID1 = 0;
                    try
                    {
                        var fieldAssetID1 = csv.GetField("AssetID1");
                        if (null != fieldAssetID1)
                            AssetID1 = Enum.Parse<EUserAssetType>(fieldAssetID1, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetID1.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 AssetCount1 = 0;
                    try
                    {
                        var fieldAssetCount1 = csv.GetField("AssetCount1");
                        if (null != fieldAssetCount1)
                            AssetCount1 = Int64.Parse(fieldAssetCount1);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetCount1.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EUserAssetType AssetID2 = 0;
                    try
                    {
                        var fieldAssetID2 = csv.GetField("AssetID2");
                        if (null != fieldAssetID2)
                            AssetID2 = Enum.Parse<EUserAssetType>(fieldAssetID2, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetID2.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 AssetCount2 = 0;
                    try
                    {
                        var fieldAssetCount2 = csv.GetField("AssetCount2");
                        if (null != fieldAssetCount2)
                            AssetCount2 = Int64.Parse(fieldAssetCount2);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetCount2.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EUserAssetType AssetID3 = 0;
                    try
                    {
                        var fieldAssetID3 = csv.GetField("AssetID3");
                        if (null != fieldAssetID3)
                            AssetID3 = Enum.Parse<EUserAssetType>(fieldAssetID3, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetID3.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 AssetCount3 = 0;
                    try
                    {
                        var fieldAssetCount3 = csv.GetField("AssetCount3");
                        if (null != fieldAssetCount3)
                            AssetCount3 = Int64.Parse(fieldAssetCount3);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetCount3.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.RewardData
                    {
                        ID = ID,
                        RewardType = RewardType,
                        RewardCount = RewardCount,
                        CharacterID = CharacterID,
                        CostumeID1 = CostumeID1,
                        CostumeID2 = CostumeID2,
                        ItemID1 = ItemID1,
                        ItemCount1 = ItemCount1,
                        ItemID2 = ItemID2,
                        ItemCount2 = ItemCount2,
                        ItemID3 = ItemID3,
                        ItemCount3 = ItemCount3,
                        AssetID1 = AssetID1,
                        AssetCount1 = AssetCount1,
                        AssetID2 = AssetID2,
                        AssetCount2 = AssetCount2,
                        AssetID3 = AssetID3,
                        AssetCount3 = AssetCount3,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
