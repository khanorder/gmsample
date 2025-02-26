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
    public class PetAbilityListDataTableService
    {
        private string tableName = "PetAbilityListDataTable";
        private string csvFileName = "tb_PetAbilityList";
        private readonly ILogger<PetAbilityListDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.PetAbilityListData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.PetAbilityListData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.PetAbilityListData> datas { get => _datas; }
        private object lockObject = new object();

        public PetAbilityListDataTableService(ILogger<PetAbilityListDataTableService> logger)
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
                csv.Context.RegisterClassMap<PetAbilityListDataMap>();
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

                    var AbilityType = csv.GetField("AbilityType") ?? "";
                    if (null == AbilityType)
                    {
                        AbilityType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member AbilityType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(AbilityType))
                    {
                        AbilityType = "";
                    }

                    int AbilityGroup = 0;
                    try
                    {
                        var fieldAbilityGroup = csv.GetField("AbilityGroup");
                        if (null != fieldAbilityGroup)
                            AbilityGroup = int.Parse(fieldAbilityGroup);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AbilityGroup.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<double> AbilityValue = new List<double>();
                    try
                    {
                        var fieldAbilityValue = csv.GetField("AbilityValue");
                        if (null != fieldAbilityValue)
                        {
                            var listDataString = fieldAbilityValue.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = double.Parse(item);
                                AbilityValue.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AbilityValue.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    List<double> AbilityRate = new List<double>();
                    try
                    {
                        var fieldAbilityRate = csv.GetField("AbilityRate");
                        if (null != fieldAbilityRate)
                        {
                            var listDataString = fieldAbilityRate.Replace("(", "").Replace(")", "");
                              var listData = listDataString.Split(",");

                            foreach (var item in listData)
                            {
                                if (null == item)
                                    continue;
                                var parsedItemData = double.Parse(item);
                                AbilityRate.Add(parsedItemData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse AbilityRate.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var data = new NGEL.Data.Tables.Models.PetAbilityListData
                    {
                        ID = ID,
                        AbilityType = AbilityType,
                        AbilityGroup = AbilityGroup,
                        AbilityValue = AbilityValue,
                        AbilityRate = AbilityRate,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
