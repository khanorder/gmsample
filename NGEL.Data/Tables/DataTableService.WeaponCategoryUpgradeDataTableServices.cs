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
    public class WeaponCategoryUpgradeDataTableService
    {
        private string tableName = "WeaponCategoryUpgradeDataTable";
        private string csvFileName = "tb_WeaponCategoryUpgrade";
        private readonly ILogger<WeaponCategoryUpgradeDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.WeaponCategoryUpgradeData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.WeaponCategoryUpgradeData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.WeaponCategoryUpgradeData> datas { get => _datas; }
        private object lockObject = new object();

        public WeaponCategoryUpgradeDataTableService(ILogger<WeaponCategoryUpgradeDataTableService> logger)
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
                csv.Context.RegisterClassMap<WeaponCategoryUpgradeDataMap>();
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

                    Int64 WeaponCategoryID = 0;
                    try
                    {
                        var fieldWeaponCategoryID = csv.GetField("WeaponCategoryID");
                        if (null != fieldWeaponCategoryID)
                            WeaponCategoryID = Int64.Parse(fieldWeaponCategoryID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse WeaponCategoryID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var Parameter = csv.GetField("Parameter") ?? "";
                    if (null == Parameter)
                    {
                        Parameter = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Parameter is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Parameter))
                    {
                        Parameter = "";
                    }

                    float Value = 0;
                    try
                    {
                        var fieldValue = csv.GetField("Value");
                        if (null != fieldValue)
                            Value = float.Parse(fieldValue);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Value.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    float Rate = 0;
                    try
                    {
                        var fieldRate = csv.GetField("Rate");
                        if (null != fieldRate)
                            Rate = float.Parse(fieldRate);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Rate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int Level = 0;
                    try
                    {
                        var fieldLevel = csv.GetField("Level");
                        if (null != fieldLevel)
                            Level = int.Parse(fieldLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Level.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 Exp = 0;
                    try
                    {
                        var fieldExp = csv.GetField("Exp");
                        if (null != fieldExp)
                            Exp = Int64.Parse(fieldExp);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Exp.");
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

                    var data = new NGEL.Data.Tables.Models.WeaponCategoryUpgradeData
                    {
                        ID = ID,
                        WeaponCategoryID = WeaponCategoryID,
                        Parameter = Parameter,
                        Value = Value,
                        Rate = Rate,
                        Level = Level,
                        Exp = Exp,
                        AssetType = AssetType,
                        AssetCount = AssetCount,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
