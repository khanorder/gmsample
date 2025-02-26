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
    public class SeasonPassDataTableService
    {
        private string tableName = "SeasonPassDataTable";
        private string csvFileName = "tb_SeasonPass";
        private readonly ILogger<SeasonPassDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.SeasonPassData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.SeasonPassData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.SeasonPassData> datas { get => _datas; }
        private object lockObject = new object();

        public SeasonPassDataTableService(ILogger<SeasonPassDataTableService> logger)
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
                csv.Context.RegisterClassMap<SeasonPassDataMap>();
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

                    int SeasonNum = 0;
                    try
                    {
                        var fieldSeasonNum = csv.GetField("SeasonNum");
                        if (null != fieldSeasonNum)
                            SeasonNum = int.Parse(fieldSeasonNum);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SeasonNum.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ExtraStartLevel = 0;
                    try
                    {
                        var fieldExtraStartLevel = csv.GetField("ExtraStartLevel");
                        if (null != fieldExtraStartLevel)
                            ExtraStartLevel = int.Parse(fieldExtraStartLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ExtraStartLevel.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int FreeMaxLevel = 0;
                    try
                    {
                        var fieldFreeMaxLevel = csv.GetField("FreeMaxLevel");
                        if (null != fieldFreeMaxLevel)
                            FreeMaxLevel = int.Parse(fieldFreeMaxLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse FreeMaxLevel.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int PaidMaxLevel = 0;
                    try
                    {
                        var fieldPaidMaxLevel = csv.GetField("PaidMaxLevel");
                        if (null != fieldPaidMaxLevel)
                            PaidMaxLevel = int.Parse(fieldPaidMaxLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PaidMaxLevel.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    bool IsActive = false;
                    try
                    {
                        var fieldIsActive = csv.GetField("IsActive");
                        if (null != fieldIsActive)
                            IsActive = fieldIsActive.ToLower() == "true" || fieldIsActive == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse IsActive.");
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

                    var data = new NGEL.Data.Tables.Models.SeasonPassData
                    {
                        ID = ID,
                        SeasonNum = SeasonNum,
                        ExtraStartLevel = ExtraStartLevel,
                        FreeMaxLevel = FreeMaxLevel,
                        PaidMaxLevel = PaidMaxLevel,
                        IsActive = IsActive,
                        Name = Name,
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
