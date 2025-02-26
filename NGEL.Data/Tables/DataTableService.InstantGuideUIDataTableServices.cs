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
    public class InstantGuideUIDataTableService
    {
        private string tableName = "InstantGuideUIDataTable";
        private string csvFileName = "tb_InstantGuideUI";
        private readonly ILogger<InstantGuideUIDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.InstantGuideUIData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.InstantGuideUIData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.InstantGuideUIData> datas { get => _datas; }
        private object lockObject = new object();

        public InstantGuideUIDataTableService(ILogger<InstantGuideUIDataTableService> logger)
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
                csv.Context.RegisterClassMap<InstantGuideUIDataMap>();
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

                    int GuideCategory = 0;
                    try
                    {
                        var fieldGuideCategory = csv.GetField("GuideCategory");
                        if (null != fieldGuideCategory)
                            GuideCategory = int.Parse(fieldGuideCategory);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse GuideCategory.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var GuideCategoryName = csv.GetField("GuideCategoryName") ?? "";
                    if (null == GuideCategoryName)
                    {
                        GuideCategoryName = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member GuideCategoryName is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(GuideCategoryName))
                    {
                        GuideCategoryName = "";
                    }

                    var Title = csv.GetField("Title") ?? "";
                    if (null == Title)
                    {
                        Title = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Title is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Title))
                    {
                        Title = "";
                    }

                    var data = new NGEL.Data.Tables.Models.InstantGuideUIData
                    {
                        ID = ID,
                        GuideCategory = GuideCategory,
                        GuideCategoryName = GuideCategoryName,
                        Title = Title,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
