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
    public class WonderStoreDataTableService
    {
        private string tableName = "WonderStoreDataTable";
        private string csvFileName = "tb_WonderStore";
        private readonly ILogger<WonderStoreDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.WonderStoreData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.WonderStoreData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.WonderStoreData> datas { get => _datas; }
        private object lockObject = new object();

        public WonderStoreDataTableService(ILogger<WonderStoreDataTableService> logger)
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
                csv.Context.RegisterClassMap<WonderStoreDataMap>();
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

                    EStoreGoodsType StoreGoodsType = 0;
                    try
                    {
                        var fieldStoreGoodsType = csv.GetField("StoreGoodsType");
                        if (null != fieldStoreGoodsType)
                            StoreGoodsType = Enum.Parse<EStoreGoodsType>(fieldStoreGoodsType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse StoreGoodsType.");
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

                    bool IAPUse = false;
                    try
                    {
                        var fieldIAPUse = csv.GetField("IAPUse");
                        if (null != fieldIAPUse)
                            IAPUse = fieldIAPUse.ToLower() == "true" || fieldIAPUse == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse IAPUse.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var ProductID_QA_PC = csv.GetField("ProductID_QA_PC") ?? "";
                    if (null == ProductID_QA_PC)
                    {
                        ProductID_QA_PC = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductID_QA_PC is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductID_QA_PC))
                    {
                        ProductID_QA_PC = "";
                    }

                    var ProductID_QA_AOS = csv.GetField("ProductID_QA_AOS") ?? "";
                    if (null == ProductID_QA_AOS)
                    {
                        ProductID_QA_AOS = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductID_QA_AOS is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductID_QA_AOS))
                    {
                        ProductID_QA_AOS = "";
                    }

                    var ProductID_QA_IOS = csv.GetField("ProductID_QA_IOS") ?? "";
                    if (null == ProductID_QA_IOS)
                    {
                        ProductID_QA_IOS = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductID_QA_IOS is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductID_QA_IOS))
                    {
                        ProductID_QA_IOS = "";
                    }

                    var ProductID_LIVE_PC = csv.GetField("ProductID_LIVE_PC") ?? "";
                    if (null == ProductID_LIVE_PC)
                    {
                        ProductID_LIVE_PC = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductID_LIVE_PC is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductID_LIVE_PC))
                    {
                        ProductID_LIVE_PC = "";
                    }

                    var ProductID_LIVE_AOS = csv.GetField("ProductID_LIVE_AOS") ?? "";
                    if (null == ProductID_LIVE_AOS)
                    {
                        ProductID_LIVE_AOS = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductID_LIVE_AOS is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductID_LIVE_AOS))
                    {
                        ProductID_LIVE_AOS = "";
                    }

                    var ProductID_LIVE_IOS = csv.GetField("ProductID_LIVE_IOS") ?? "";
                    if (null == ProductID_LIVE_IOS)
                    {
                        ProductID_LIVE_IOS = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductID_LIVE_IOS is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductID_LIVE_IOS))
                    {
                        ProductID_LIVE_IOS = "";
                    }

                    Int64 RewardTableID = 0;
                    try
                    {
                        var fieldRewardTableID = csv.GetField("RewardTableID");
                        if (null != fieldRewardTableID)
                            RewardTableID = Int64.Parse(fieldRewardTableID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardTableID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var SaleText = csv.GetField("SaleText") ?? "";
                    if (null == SaleText)
                    {
                        SaleText = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member SaleText is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(SaleText))
                    {
                        SaleText = "";
                    }

                    EUserAssetType ReqAssetID = 0;
                    try
                    {
                        var fieldReqAssetID = csv.GetField("ReqAssetID");
                        if (null != fieldReqAssetID)
                            ReqAssetID = Enum.Parse<EUserAssetType>(fieldReqAssetID, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReqAssetID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 ReqAssetCount = 0;
                    try
                    {
                        var fieldReqAssetCount = csv.GetField("ReqAssetCount");
                        if (null != fieldReqAssetCount)
                            ReqAssetCount = Int64.Parse(fieldReqAssetCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReqAssetCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    bool DateSwitch = false;
                    try
                    {
                        var fieldDateSwitch = csv.GetField("DateSwitch");
                        if (null != fieldDateSwitch)
                            DateSwitch = fieldDateSwitch.ToLower() == "true" || fieldDateSwitch == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse DateSwitch.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    DateTime StartDate = DateTime.Now;
                    try
                    {
                        var fieldStartDate = csv.GetField("StartDate");
                        if (null != fieldStartDate)
                        {
                            if (Regex.Match(fieldStartDate, @"^[0-9]{4}\.{1}[0-9]{2}\.{1}[0-9]{2}\-{1}[0-9]{2}\.{1}[0-9]{2}\.{1}[0-9]{2}$").Success)
                            {
                                var dateStringArr = fieldStartDate.Split("-");
                                if (1 < dateStringArr.Length)
                                    fieldStartDate = dateStringArr[0].Replace(".", " ") + " " + dateStringArr[1].Replace(".", ":");
                            }
                            StartDate = DateTime.Parse(fieldStartDate);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse StartDate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    DateTime EndDate = DateTime.Now;
                    try
                    {
                        var fieldEndDate = csv.GetField("EndDate");
                        if (null != fieldEndDate)
                        {
                            if (Regex.Match(fieldEndDate, @"^[0-9]{4}\.{1}[0-9]{2}\.{1}[0-9]{2}\-{1}[0-9]{2}\.{1}[0-9]{2}\.{1}[0-9]{2}$").Success)
                            {
                                var dateStringArr = fieldEndDate.Split("-");
                                if (1 < dateStringArr.Length)
                                    fieldEndDate = dateStringArr[0].Replace(".", " ") + " " + dateStringArr[1].Replace(".", ":");
                            }
                            EndDate = DateTime.Parse(fieldEndDate);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse EndDate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    ELimitTime LimitTime = 0;
                    try
                    {
                        var fieldLimitTime = csv.GetField("LimitTime");
                        if (null != fieldLimitTime)
                            LimitTime = Enum.Parse<ELimitTime>(fieldLimitTime, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse LimitTime.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 LimitCount = 0;
                    try
                    {
                        var fieldLimitCount = csv.GetField("LimitCount");
                        if (null != fieldLimitCount)
                            LimitCount = Int64.Parse(fieldLimitCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse LimitCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var ProductName = csv.GetField("ProductName") ?? "";
                    if (null == ProductName)
                    {
                        ProductName = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ProductName is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ProductName))
                    {
                        ProductName = "";
                    }

                    var data = new NGEL.Data.Tables.Models.WonderStoreData
                    {
                        ID = ID,
                        StoreGoodsType = StoreGoodsType,
                        MainType = MainType,
                        SubType = SubType,
                        IAPUse = IAPUse,
                        ProductID_QA_PC = ProductID_QA_PC,
                        ProductID_QA_AOS = ProductID_QA_AOS,
                        ProductID_QA_IOS = ProductID_QA_IOS,
                        ProductID_LIVE_PC = ProductID_LIVE_PC,
                        ProductID_LIVE_AOS = ProductID_LIVE_AOS,
                        ProductID_LIVE_IOS = ProductID_LIVE_IOS,
                        RewardTableID = RewardTableID,
                        SaleText = SaleText,
                        ReqAssetID = ReqAssetID,
                        ReqAssetCount = ReqAssetCount,
                        DateSwitch = DateSwitch,
                        StartDate = StartDate,
                        EndDate = EndDate,
                        LimitTime = LimitTime,
                        LimitCount = LimitCount,
                        ProductName = ProductName,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
