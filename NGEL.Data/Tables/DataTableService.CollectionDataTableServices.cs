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
    public class CollectionDataTableService
    {
        private string tableName = "CollectionDataTable";
        private string csvFileName = "tb_Collection";
        private readonly ILogger<CollectionDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.CollectionData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.CollectionData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.CollectionData> datas { get => _datas; }
        private object lockObject = new object();

        public CollectionDataTableService(ILogger<CollectionDataTableService> logger)
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
                csv.Context.RegisterClassMap<CollectionDataMap>();
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

                    ECollectionType Type = 0;
                    try
                    {
                        var fieldType = csv.GetField("Type");
                        if (null != fieldType)
                            Type = Enum.Parse<ECollectionType>(fieldType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Type.");
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

                    int Group = 0;
                    try
                    {
                        var fieldGroup = csv.GetField("Group");
                        if (null != fieldGroup)
                            Group = int.Parse(fieldGroup);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Group.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var GroupName = csv.GetField("GroupName") ?? "";
                    if (null == GroupName)
                    {
                        GroupName = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member GroupName is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(GroupName))
                    {
                        GroupName = "";
                    }

                    Int64 ReqID = 0;
                    try
                    {
                        var fieldReqID = csv.GetField("ReqID");
                        if (null != fieldReqID)
                            ReqID = Int64.Parse(fieldReqID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReqID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.CollectionData
                    {
                        ID = ID,
                        Type = Type,
                        Enable = Enable,
                        Group = Group,
                        GroupName = GroupName,
                        ReqID = ReqID,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
