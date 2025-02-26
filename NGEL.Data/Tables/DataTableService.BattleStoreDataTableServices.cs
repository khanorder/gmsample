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
    public class BattleStoreDataTableService
    {
        private string tableName = "BattleStoreDataTable";
        private string csvFileName = "tb_BattleStore";
        private readonly ILogger<BattleStoreDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.BattleStoreData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.BattleStoreData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.BattleStoreData> datas { get => _datas; }
        private object lockObject = new object();

        public BattleStoreDataTableService(ILogger<BattleStoreDataTableService> logger)
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
                csv.Context.RegisterClassMap<BattleStoreDataMap>();
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

                    var MainType = csv.GetField("MainType") ?? "";
                    if (null == MainType)
                    {
                        MainType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member MainType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(MainType))
                    {
                        MainType = "";
                    }

                    var SubType = csv.GetField("SubType") ?? "";
                    if (null == SubType)
                    {
                        SubType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member SubType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(SubType))
                    {
                        SubType = "";
                    }

                    Int64 SellItem = 0;
                    try
                    {
                        var fieldSellItem = csv.GetField("SellItem");
                        if (null != fieldSellItem)
                            SellItem = Int64.Parse(fieldSellItem);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SellItem.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var SellAsset = csv.GetField("SellAsset") ?? "";
                    if (null == SellAsset)
                    {
                        SellAsset = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member SellAsset is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(SellAsset))
                    {
                        SellAsset = "";
                    }

                    Int64 Count = 0;
                    try
                    {
                        var fieldCount = csv.GetField("Count");
                        if (null != fieldCount)
                            Count = Int64.Parse(fieldCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Count.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var CostAssetType = csv.GetField("CostAssetType") ?? "";
                    if (null == CostAssetType)
                    {
                        CostAssetType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member CostAssetType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(CostAssetType))
                    {
                        CostAssetType = "";
                    }

                    Int64 CostCount = 0;
                    try
                    {
                        var fieldCostCount = csv.GetField("CostCount");
                        if (null != fieldCostCount)
                            CostCount = Int64.Parse(fieldCostCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CostCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.BattleStoreData
                    {
                        ID = ID,
                        Enable = Enable,
                        MainType = MainType,
                        SubType = SubType,
                        SellItem = SellItem,
                        SellAsset = SellAsset,
                        Count = Count,
                        CostAssetType = CostAssetType,
                        CostCount = CostCount,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
