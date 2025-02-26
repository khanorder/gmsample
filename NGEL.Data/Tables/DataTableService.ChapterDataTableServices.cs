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
    public class ChapterDataTableService
    {
        private string tableName = "ChapterDataTable";
        private string csvFileName = "tb_Chapter";
        private readonly ILogger<ChapterDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.ChapterData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.ChapterData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.ChapterData> datas { get => _datas; }
        private object lockObject = new object();

        public ChapterDataTableService(ILogger<ChapterDataTableService> logger)
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
                csv.Context.RegisterClassMap<ChapterDataMap>();
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

                    Int64 Chapter = 0;
                    try
                    {
                        var fieldChapter = csv.GetField("Chapter");
                        if (null != fieldChapter)
                            Chapter = Int64.Parse(fieldChapter);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Chapter.");
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

                    List<Int64> CharacterUnlock = new List<Int64>();
                    try
                    {
                        var fieldCharacterUnlock = csv.GetField("CharacterUnlock");
                        if (null != fieldCharacterUnlock)
                        {
                            var listDataString = fieldCharacterUnlock.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Int64.Parse(item);
                                CharacterUnlock.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse CharacterUnlock.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<Int64> Rewards = new List<Int64>();
                    try
                    {
                        var fieldRewards = csv.GetField("Rewards");
                        if (null != fieldRewards)
                        {
                            var listDataString = fieldRewards.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Int64.Parse(item);
                                Rewards.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Rewards.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<Int64> StageID = new List<Int64>();
                    try
                    {
                        var fieldStageID = csv.GetField("StageID");
                        if (null != fieldStageID)
                        {
                            var listDataString = fieldStageID.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = Int64.Parse(item);
                                StageID.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse StageID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 StageGroup = 0;
                    try
                    {
                        var fieldStageGroup = csv.GetField("StageGroup");
                        if (null != fieldStageGroup)
                            StageGroup = Int64.Parse(fieldStageGroup);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse StageGroup.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    Int64 NextChapterID = 0;
                    try
                    {
                        var fieldNextChapterID = csv.GetField("NextChapterID");
                        if (null != fieldNextChapterID)
                            NextChapterID = Int64.Parse(fieldNextChapterID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse NextChapterID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.ChapterData
                    {
                        ID = ID,
                        Chapter = Chapter,
                        Name = Name,
                        CharacterUnlock = CharacterUnlock,
                        Rewards = Rewards,
                        StageID = StageID,
                        StageGroup = StageGroup,
                        NextChapterID = NextChapterID,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
