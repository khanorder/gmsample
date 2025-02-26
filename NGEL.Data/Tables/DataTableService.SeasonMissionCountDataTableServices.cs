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
    public class SeasonMissionCountDataTableService
    {
        private string tableName = "SeasonMissionCountDataTable";
        private string csvFileName = "tb_SeasonMissionCount";
        private readonly ILogger<SeasonMissionCountDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.SeasonMissionCountData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.SeasonMissionCountData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.SeasonMissionCountData> datas { get => _datas; }
        private object lockObject = new object();

        public SeasonMissionCountDataTableService(ILogger<SeasonMissionCountDataTableService> logger)
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
                csv.Context.RegisterClassMap<SeasonMissionCountDataMap>();
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

                    int ID = 0;
                    try
                    {
                        var fieldID = csv.GetField("ID");
                        if (null != fieldID)
                            ID = int.Parse(fieldID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int OpenWorldCount = 0;
                    try
                    {
                        var fieldOpenWorldCount = csv.GetField("OpenWorldCount");
                        if (null != fieldOpenWorldCount)
                            OpenWorldCount = int.Parse(fieldOpenWorldCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse OpenWorldCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int GoldClashCount = 0;
                    try
                    {
                        var fieldGoldClashCount = csv.GetField("GoldClashCount");
                        if (null != fieldGoldClashCount)
                            GoldClashCount = int.Parse(fieldGoldClashCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse GoldClashCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int RPGCount = 0;
                    try
                    {
                        var fieldRPGCount = csv.GetField("RPGCount");
                        if (null != fieldRPGCount)
                            RPGCount = int.Parse(fieldRPGCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RPGCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.SeasonMissionCountData
                    {
                        ID = ID,
                        OpenWorldCount = OpenWorldCount,
                        GoldClashCount = GoldClashCount,
                        RPGCount = RPGCount,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
