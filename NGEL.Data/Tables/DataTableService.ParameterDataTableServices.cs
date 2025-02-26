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
    public class ParameterDataTableService
    {
        private string tableName = "ParameterDataTable";
        private string csvFileName = "tb_Parameter";
        private readonly ILogger<ParameterDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.ParameterData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.ParameterData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.ParameterData> datas { get => _datas; }
        private object lockObject = new object();

        public ParameterDataTableService(ILogger<ParameterDataTableService> logger)
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
                csv.Context.RegisterClassMap<ParameterDataMap>();
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

                    var ParameterName = csv.GetField("ParameterName") ?? "";
                    if (null == ParameterName)
                    {
                        ParameterName = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ParameterName is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ParameterName))
                    {
                        ParameterName = "";
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

                    int SortIndex = 0;
                    try
                    {
                        var fieldSortIndex = csv.GetField("SortIndex");
                        if (null != fieldSortIndex)
                            SortIndex = int.Parse(fieldSortIndex);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SortIndex.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var CalcType = csv.GetField("CalcType") ?? "";
                    if (null == CalcType)
                    {
                        CalcType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member CalcType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(CalcType))
                    {
                        CalcType = "";
                    }

                    int CalcValue = 0;
                    try
                    {
                        var fieldCalcValue = csv.GetField("CalcValue");
                        if (null != fieldCalcValue)
                            CalcValue = int.Parse(fieldCalcValue);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CalcValue.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int DigitCount = 0;
                    try
                    {
                        var fieldDigitCount = csv.GetField("DigitCount");
                        if (null != fieldDigitCount)
                            DigitCount = int.Parse(fieldDigitCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse DigitCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int AddString = 0;
                    try
                    {
                        var fieldAddString = csv.GetField("AddString");
                        if (null != fieldAddString)
                            AddString = int.Parse(fieldAddString);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AddString.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.ParameterData
                    {
                        ParameterName = ParameterName,
                        Name = Name,
                        SortIndex = SortIndex,
                        CalcType = CalcType,
                        CalcValue = CalcValue,
                        DigitCount = DigitCount,
                        AddString = AddString,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
