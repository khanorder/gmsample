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
    public class GlobalDefineDataTableService
    {
        private string tableName = "GlobalDefineDataTable";
        private string csvFileName = "tb_GlobalDefine";
        private readonly ILogger<GlobalDefineDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.GlobalDefineData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.GlobalDefineData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.GlobalDefineData> datas { get => _datas; }
        private object lockObject = new object();

        public GlobalDefineDataTableService(ILogger<GlobalDefineDataTableService> logger)
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
                csv.Context.RegisterClassMap<GlobalDefineDataMap>();
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

                    var ID = csv.GetField("ID") ?? "";
                    if (null == ID)
                    {
                        ID = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ID is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ID))
                    {
                        ID = "";
                    }

                    List<string> Value = new List<string>();
                    try
                    {
                        var fieldValue = csv.GetField("Value");
                        if (null != fieldValue)
                        {
                            var listDataString = fieldValue.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                Value.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Value.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.GlobalDefineData
                    {
                        ID = ID,
                        Value = Value,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
