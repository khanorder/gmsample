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
    public class PetDataTableService
    {
        private string tableName = "PetDataTable";
        private string csvFileName = "tb_Pet";
        private readonly ILogger<PetDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.PetData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.PetData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.PetData> datas { get => _datas; }
        private object lockObject = new object();

        public PetDataTableService(ILogger<PetDataTableService> logger)
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
                csv.Context.RegisterClassMap<PetDataMap>();
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

                    EItemGradeType Grade = 0;
                    try
                    {
                        var fieldGrade = csv.GetField("Grade");
                        if (null != fieldGrade)
                            Grade = Enum.Parse<EItemGradeType>(fieldGrade, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Grade.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int ActiveSkill = 0;
                    try
                    {
                        var fieldActiveSkill = csv.GetField("ActiveSkill");
                        if (null != fieldActiveSkill)
                            ActiveSkill = int.Parse(fieldActiveSkill);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse ActiveSkill.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<int> PassiveSkill = new List<int>();
                    try
                    {
                        var fieldPassiveSkill = csv.GetField("PassiveSkill");
                        if (null != fieldPassiveSkill)
                        {
                            var listDataString = fieldPassiveSkill.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = int.Parse(item);
                                PassiveSkill.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse PassiveSkill.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<int> LikeSection = new List<int>();
                    try
                    {
                        var fieldLikeSection = csv.GetField("LikeSection");
                        if (null != fieldLikeSection)
                        {
                            var listDataString = fieldLikeSection.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = int.Parse(item);
                                LikeSection.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse LikeSection.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.PetData
                    {
                        ID = ID,
                        Name = Name,
                        Enable = Enable,
                        Grade = Grade,
                        ActiveSkill = ActiveSkill,
                        PassiveSkill = PassiveSkill,
                        LikeSection = LikeSection,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
