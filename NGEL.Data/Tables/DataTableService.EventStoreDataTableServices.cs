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
    public class EventStoreDataTableService
    {
        private string tableName = "EventStoreDataTable";
        private string csvFileName = "tb_EventStore";
        private readonly ILogger<EventStoreDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.EventStoreData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.EventStoreData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.EventStoreData> datas { get => _datas; }
        private object lockObject = new object();

        public EventStoreDataTableService(ILogger<EventStoreDataTableService> logger)
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
                csv.Context.RegisterClassMap<EventStoreDataMap>();
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

                    EItemType ItemType = 0;
                    try
                    {
                        var fieldItemType = csv.GetField("ItemType");
                        if (null != fieldItemType)
                            ItemType = Enum.Parse<EItemType>(fieldItemType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ItemType.");
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

                    var data = new NGEL.Data.Tables.Models.EventStoreData
                    {
                        ID = ID,
                        Enable = Enable,
                        RewardType = RewardType,
                        ItemType = ItemType,
                        ItemID = ItemID,
                        ItemCount = ItemCount,
                        ReqAssetID = ReqAssetID,
                        ReqAssetCount = ReqAssetCount,
                        LimitTime = LimitTime,
                        LimitCount = LimitCount,
                        StartDate = StartDate,
                        EndDate = EndDate,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
