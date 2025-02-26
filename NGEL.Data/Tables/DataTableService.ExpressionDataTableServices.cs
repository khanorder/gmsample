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
    public class ExpressionDataTableService
    {
        private string tableName = "ExpressionDataTable";
        private string csvFileName = "tb_Expression";
        private readonly ILogger<ExpressionDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.ExpressionData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.ExpressionData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.ExpressionData> datas { get => _datas; }
        private object lockObject = new object();

        public ExpressionDataTableService(ILogger<ExpressionDataTableService> logger)
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
                csv.Context.RegisterClassMap<ExpressionDataMap>();
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

                    EHeroType Character = 0;
                    try
                    {
                        var fieldCharacter = csv.GetField("Character");
                        if (null != fieldCharacter)
                            Character = Enum.Parse<EHeroType>(fieldCharacter, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Character.");
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

                    EExpressionType ExpressionType = 0;
                    try
                    {
                        var fieldExpressionType = csv.GetField("ExpressionType");
                        if (null != fieldExpressionType)
                            ExpressionType = Enum.Parse<EExpressionType>(fieldExpressionType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ExpressionType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EExpressionCategory ExpressionCategory = 0;
                    try
                    {
                        var fieldExpressionCategory = csv.GetField("ExpressionCategory");
                        if (null != fieldExpressionCategory)
                            ExpressionCategory = Enum.Parse<EExpressionCategory>(fieldExpressionCategory, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ExpressionCategory.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var Name = csv.GetField("Name") ?? "";
                    if (null == Name)
                    {
                        Name = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Name is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Name))
                    {
                        Name = "";
                    }

                    var data = new NGEL.Data.Tables.Models.ExpressionData
                    {
                        ID = ID,
                        Character = Character,
                        Enable = Enable,
                        ExpressionType = ExpressionType,
                        ExpressionCategory = ExpressionCategory,
                        Name = Name,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
