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
    public class StringDataTableService
    {
        private string tableName = "StringDataTable";
        private string csvFileName = "tb_String";
        private readonly ILogger<StringDataTableService> _logger;
        private double _version { get; set; } = 0;
        private List<NGEL.Data.Tables.Models.StringData> _datas { get; set; } = new List<NGEL.Data.Tables.Models.StringData>();
        public double version { get => _version; }
        public List<NGEL.Data.Tables.Models.StringData> datas { get => _datas; }
        private object lockObject = new object();

        public StringDataTableService(ILogger<StringDataTableService> logger)
        {
            _logger = logger;
        }

        public void LoadTable()
        {
            _datas.Clear();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = arg => arg.Header.ToLower(),
                Mode = CsvMode.NoEscape
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
                csv.Context.RegisterClassMap<StringDataMap>();
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

                    var Key = csv.GetField("Key") ?? "";
                    if (null == Key)
                    {
                        Key = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member Key is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(Key))
                    {
                        Key = "";
                    }

                    var SourceString = csv.GetField("SourceString") ?? "";
                    if (null == SourceString)
                    {
                        SourceString = "";
                        _logger.LogError($"{tableName}({rowIndex}): Member SourceString is null.");
                    }
                    else if (string.IsNullOrWhiteSpace(SourceString))
                    {
                        SourceString = "";
                    }

                    var data = new NGEL.Data.Tables.Models.StringData
                    {
                        Key = Key,
                        SourceString = SourceString,
                    };
                    _datas.Add(data);
                    rowIndex++;
                }
                _logger.LogInformation($"Loaded {tableName} Version: {version}, Count: {_datas.Count}", ConsoleColor.Magenta, ConsoleColor.White);
            }
        }
    }
}
