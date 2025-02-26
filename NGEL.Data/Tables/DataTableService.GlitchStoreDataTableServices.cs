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
    public class GlitchStoreDataTableService
    {
        private string tableName = "GlitchStoreDataTable";
        private string csvFileName = "tb_GlitchStore";
        private readonly ILogger<GlitchStoreDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.GlitchStoreData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.GlitchStoreData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.GlitchStoreData> datas { get => _datas; }
        private object lockObject = new object();

        public GlitchStoreDataTableService(ILogger<GlitchStoreDataTableService> logger)
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
                csv.Context.RegisterClassMap<GlitchStoreDataMap>();
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

                    var ProductType = csv.GetField("ProductType") ?? "";
                    if (null == ProductType)
                    {
                        ProductType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductType))
                    {
                        ProductType = "";
                    }

                    Int64 ProductItem = 0;
                    try
                    {
                        var fieldProductItem = csv.GetField("ProductItem");
                        if (null != fieldProductItem)
                            ProductItem = Int64.Parse(fieldProductItem);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ProductItem.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var AssetType = csv.GetField("AssetType") ?? "";
                    if (null == AssetType)
                    {
                        AssetType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member AssetType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(AssetType))
                    {
                        AssetType = "";
                    }

                    Int64 AssetPrice = 0;
                    try
                    {
                        var fieldAssetPrice = csv.GetField("AssetPrice");
                        if (null != fieldAssetPrice)
                            AssetPrice = Int64.Parse(fieldAssetPrice);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AssetPrice.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int Chapter = 0;
                    try
                    {
                        var fieldChapter = csv.GetField("Chapter");
                        if (null != fieldChapter)
                            Chapter = int.Parse(fieldChapter);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Chapter.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.GlitchStoreData
                    {
                        ID = ID,
                        ProductType = ProductType,
                        ProductItem = ProductItem,
                        AssetType = AssetType,
                        AssetPrice = AssetPrice,
                        Chapter = Chapter,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
