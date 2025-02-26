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
    public class PenaltyDataTableService
    {
        private string tableName = "PenaltyDataTable";
        private string csvFileName = "tb_Penalty";
        private readonly ILogger<PenaltyDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.PenaltyData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.PenaltyData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.PenaltyData> datas { get => _datas; }
        private object lockObject = new object();

        public PenaltyDataTableService(ILogger<PenaltyDataTableService> logger)
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
                csv.Context.RegisterClassMap<PenaltyDataMap>();
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

                    EPenaltyReportState ReportState = 0;
                    try
                    {
                        var fieldReportState = csv.GetField("ReportState");
                        if (null != fieldReportState)
                            ReportState = Enum.Parse<EPenaltyReportState>(fieldReportState, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReportState.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int PenaltyGrade = 0;
                    try
                    {
                        var fieldPenaltyGrade = csv.GetField("PenaltyGrade");
                        if (null != fieldPenaltyGrade)
                            PenaltyGrade = int.Parse(fieldPenaltyGrade);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PenaltyGrade.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ReqPenaltyPoint = 0;
                    try
                    {
                        var fieldReqPenaltyPoint = csv.GetField("ReqPenaltyPoint");
                        if (null != fieldReqPenaltyPoint)
                            ReqPenaltyPoint = int.Parse(fieldReqPenaltyPoint);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ReqPenaltyPoint.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    EPenaltyType PenaltyType = 0;
                    try
                    {
                        var fieldPenaltyType = csv.GetField("PenaltyType");
                        if (null != fieldPenaltyType)
                            PenaltyType = Enum.Parse<EPenaltyType>(fieldPenaltyType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PenaltyType.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int PenaltyTime = 0;
                    try
                    {
                        var fieldPenaltyTime = csv.GetField("PenaltyTime");
                        if (null != fieldPenaltyTime)
                            PenaltyTime = int.Parse(fieldPenaltyTime);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PenaltyTime.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int PenaltyCount = 0;
                    try
                    {
                        var fieldPenaltyCount = csv.GetField("PenaltyCount");
                        if (null != fieldPenaltyCount)
                            PenaltyCount = int.Parse(fieldPenaltyCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PenaltyCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ClearPenaltyTime = 0;
                    try
                    {
                        var fieldClearPenaltyTime = csv.GetField("ClearPenaltyTime");
                        if (null != fieldClearPenaltyTime)
                            ClearPenaltyTime = int.Parse(fieldClearPenaltyTime);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ClearPenaltyTime.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ClearPenaltyGrade = 0;
                    try
                    {
                        var fieldClearPenaltyGrade = csv.GetField("ClearPenaltyGrade");
                        if (null != fieldClearPenaltyGrade)
                            ClearPenaltyGrade = int.Parse(fieldClearPenaltyGrade);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ClearPenaltyGrade.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.PenaltyData
                    {
                        ID = ID,
                        ReportState = ReportState,
                        PenaltyGrade = PenaltyGrade,
                        ReqPenaltyPoint = ReqPenaltyPoint,
                        PenaltyType = PenaltyType,
                        PenaltyTime = PenaltyTime,
                        PenaltyCount = PenaltyCount,
                        ClearPenaltyTime = ClearPenaltyTime,
                        ClearPenaltyGrade = ClearPenaltyGrade,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
