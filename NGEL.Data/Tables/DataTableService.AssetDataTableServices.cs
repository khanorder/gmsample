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
    public class AssetDataTableService
    {
        private string tableName = "AssetDataTable";
        private string csvFileName = "tb_Asset";
        private readonly ILogger<AssetDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.AssetData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.AssetData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.AssetData> datas { get => _datas; }
        private object lockObject = new object();

        public AssetDataTableService(ILogger<AssetDataTableService> logger)
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
                csv.Context.RegisterClassMap<AssetDataMap>();
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

                    EUserAssetType ID = 0;
                    try
                    {
                        var fieldID = csv.GetField("ID");
                        if (null != fieldID)
                            ID = Enum.Parse<EUserAssetType>(fieldID, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ItemID = 0;
                    try
                    {
                        var fieldItemID = csv.GetField("ItemID");
                        if (null != fieldItemID)
                            ItemID = Int64.Parse(fieldItemID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 MaxValue = 0;
                    try
                    {
                        var fieldMaxValue = csv.GetField("MaxValue");
                        if (null != fieldMaxValue)
                            MaxValue = Int64.Parse(fieldMaxValue);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MaxValue.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var AssetString = csv.GetField("AssetString") ?? "";
                    if (null == AssetString)
                    {
                        AssetString = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member AssetString is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(AssetString))
                    {
                        AssetString = "";
                    }

                    var data = new NGEL.Data.Tables.Models.AssetData
                    {
                        ID = ID,
                        ItemID = ItemID,
                        MaxValue = MaxValue,
                        AssetString = AssetString,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
