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
    public class QuestDataTableService
    {
        private string tableName = "QuestDataTable";
        private string csvFileName = "tb_Quest";
        private readonly ILogger<QuestDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.QuestData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.QuestData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.QuestData> datas { get => _datas; }
        private object lockObject = new object();

        public QuestDataTableService(ILogger<QuestDataTableService> logger)
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
                csv.Context.RegisterClassMap<QuestDataMap>();
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

                    var QuestType = csv.GetField("QuestType") ?? "";
                    if (null == QuestType)
                    {
                        QuestType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member QuestType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(QuestType))
                    {
                        QuestType = "";
                    }

                    int MissionCount = 0;
                    try
                    {
                        var fieldMissionCount = csv.GetField("MissionCount");
                        if (null != fieldMissionCount)
                            MissionCount = int.Parse(fieldMissionCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse MissionCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var MissionIDList = csv.GetField("MissionIDList") ?? "";
                    if (null == MissionIDList)
                    {
                        MissionIDList = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member MissionIDList is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(MissionIDList))
                    {
                        MissionIDList = "";
                    }

                    Int64 CompleteRewardItemID = 0;
                    try
                    {
                        var fieldCompleteRewardItemID = csv.GetField("CompleteRewardItemID");
                        if (null != fieldCompleteRewardItemID)
                            CompleteRewardItemID = Int64.Parse(fieldCompleteRewardItemID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CompleteRewardItemID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 CompleteRewardItemCount = 0;
                    try
                    {
                        var fieldCompleteRewardItemCount = csv.GetField("CompleteRewardItemCount");
                        if (null != fieldCompleteRewardItemCount)
                            CompleteRewardItemCount = Int64.Parse(fieldCompleteRewardItemCount);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CompleteRewardItemCount.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.QuestData
                    {
                        ID = ID,
                        Name = Name,
                        QuestType = QuestType,
                        MissionCount = MissionCount,
                        MissionIDList = MissionIDList,
                        CompleteRewardItemID = CompleteRewardItemID,
                        CompleteRewardItemCount = CompleteRewardItemCount,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
