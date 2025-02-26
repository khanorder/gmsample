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
    public class SkillDataTableService
    {
        private string tableName = "SkillDataTable";
        private string csvFileName = "tb_Skill";
        private readonly ILogger<SkillDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.SkillData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.SkillData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.SkillData> datas { get => _datas; }
        private object lockObject = new object();

        public SkillDataTableService(ILogger<SkillDataTableService> logger)
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
                csv.Context.RegisterClassMap<SkillDataMap>();
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

                    int SkillID = 0;
                    try
                    {
                        var fieldSkillID = csv.GetField("SkillID");
                        if (null != fieldSkillID)
                            SkillID = int.Parse(fieldSkillID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SkillID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var DesignName = csv.GetField("DesignName") ?? "";
                    if (null == DesignName)
                    {
                        DesignName = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member DesignName is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(DesignName))
                    {
                        DesignName = "";
                    }

                    var SkillType = csv.GetField("SkillType") ?? "";
                    if (null == SkillType)
                    {
                        SkillType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member SkillType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(SkillType))
                    {
                        SkillType = "";
                    }

                    bool IsPetSkill = false;
                    try
                    {
                        var fieldIsPetSkill = csv.GetField("IsPetSkill");
                        if (null != fieldIsPetSkill)
                            IsPetSkill = fieldIsPetSkill.ToLower() == "true" || fieldIsPetSkill == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse IsPetSkill.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.SkillData
                    {
                        SkillID = SkillID,
                        DesignName = DesignName,
                        SkillType = SkillType,
                        IsPetSkill = IsPetSkill,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
