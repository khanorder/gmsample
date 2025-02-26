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
    public class LevelUpBuffListDataTableService
    {
        private string tableName = "LevelUpBuffListDataTable";
        private string csvFileName = "tb_LevelUpBuffList";
        private readonly ILogger<LevelUpBuffListDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.LevelUpBuffListData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.LevelUpBuffListData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.LevelUpBuffListData> datas { get => _datas; }
        private object lockObject = new object();

        public LevelUpBuffListDataTableService(ILogger<LevelUpBuffListDataTableService> logger)
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
                csv.Context.RegisterClassMap<LevelUpBuffListDataMap>();
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

                    int Stack = 0;
                    try
                    {
                        var fieldStack = csv.GetField("Stack");
                        if (null != fieldStack)
                            Stack = int.Parse(fieldStack);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse Stack.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    var ModeType = csv.GetField("ModeType") ?? "";
                    if (null == ModeType)
                    {
                        ModeType = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member ModeType is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(ModeType))
                    {
                        ModeType = "";
                    }

                    EHeroType HeroType = 0;
                    try
                    {
                        var fieldHeroType = csv.GetField("HeroType");
                        if (null != fieldHeroType)
                            HeroType = Enum.Parse<EHeroType>(fieldHeroType, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse HeroType.");
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

                    bool KeyBuff = false;
                    try
                    {
                        var fieldKeyBuff = csv.GetField("KeyBuff");
                        if (null != fieldKeyBuff)
                            KeyBuff = fieldKeyBuff.ToLower() == "true" || fieldKeyBuff == "1";
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse KeyBuff.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int SlotIndex = 0;
                    try
                    {
                        var fieldSlotIndex = csv.GetField("SlotIndex");
                        if (null != fieldSlotIndex)
                            SlotIndex = int.Parse(fieldSlotIndex);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse SlotIndex.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int HeroLevel = 0;
                    try
                    {
                        var fieldHeroLevel = csv.GetField("HeroLevel");
                        if (null != fieldHeroLevel)
                            HeroLevel = int.Parse(fieldHeroLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse HeroLevel.");
                        _logger.LogError(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }

                    int FavorityLevel = 0;
                    try
                    {
                        var fieldFavorityLevel = csv.GetField("FavorityLevel");
                        if (null != fieldFavorityLevel)
                            FavorityLevel = int.Parse(fieldFavorityLevel);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{tableName}({rowIndex}): Occurred Error to parse FavorityLevel.");
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

                    var data = new NGEL.Data.Tables.Models.LevelUpBuffListData
                    {
                        ID = ID,
                        Enable = Enable,
                        Stack = Stack,
                        ModeType = ModeType,
                        HeroType = HeroType,
                        GroupIndex = GroupIndex,
                        KeyBuff = KeyBuff,
                        SlotIndex = SlotIndex,
                        HeroLevel = HeroLevel,
                        FavorityLevel = FavorityLevel,
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
