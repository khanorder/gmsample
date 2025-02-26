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
    public class SeasonPassRewardDataTableService
    {
        private string tableName = "SeasonPassRewardDataTable";
        private string csvFileName = "tb_SeasonPassReward";
        private readonly ILogger<SeasonPassRewardDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.SeasonPassRewardData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.SeasonPassRewardData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.SeasonPassRewardData> datas { get => _datas; }
        private object lockObject = new object();

        public SeasonPassRewardDataTableService(ILogger<SeasonPassRewardDataTableService> logger)
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
                csv.Context.RegisterClassMap<SeasonPassRewardDataMap>();
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

                    int SeasonPassID = 0;
                    try
                    {
                        var fieldSeasonPassID = csv.GetField("SeasonPassID");
                        if (null != fieldSeasonPassID)
                            SeasonPassID = int.Parse(fieldSeasonPassID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SeasonPassID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int SeasonPassLevel = 0;
                    try
                    {
                        var fieldSeasonPassLevel = csv.GetField("SeasonPassLevel");
                        if (null != fieldSeasonPassLevel)
                            SeasonPassLevel = int.Parse(fieldSeasonPassLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SeasonPassLevel.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int GroupIndex = 0;
                    try
                    {
                        var fieldGroupIndex = csv.GetField("GroupIndex");
                        if (null != fieldGroupIndex)
                            GroupIndex = int.Parse(fieldGroupIndex);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse GroupIndex.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    bool IsPaid = false;
                    try
                    {
                        var fieldIsPaid = csv.GetField("IsPaid");
                        if (null != fieldIsPaid)
                            IsPaid = fieldIsPaid.ToLower() == "true" || fieldIsPaid == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse IsPaid.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int RewardTableID = 0;
                    try
                    {
                        var fieldRewardTableID = csv.GetField("RewardTableID");
                        if (null != fieldRewardTableID)
                            RewardTableID = int.Parse(fieldRewardTableID);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse RewardTableID.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    bool IsMainReward = false;
                    try
                    {
                        var fieldIsMainReward = csv.GetField("IsMainReward");
                        if (null != fieldIsMainReward)
                            IsMainReward = fieldIsMainReward.ToLower() == "true" || fieldIsMainReward == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse IsMainReward.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.SeasonPassRewardData
                    {
                        ID = ID,
                        SeasonPassID = SeasonPassID,
                        SeasonPassLevel = SeasonPassLevel,
                        GroupIndex = GroupIndex,
                        IsPaid = IsPaid,
                        RewardTableID = RewardTableID,
                        IsMainReward = IsMainReward,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
