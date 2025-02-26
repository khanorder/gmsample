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
    public class ErrorsDataTableService
    {
        private readonly ILogger<ErrorsDataTableService> _logger;
        private double _version { get; set; } = 0;
        private Dictionary<Errors, NGEL.Data.Tables.Models.ErrorsData> _datas { get; set; } = new Dictionary<Errors, NGEL.Data.Tables.Models.ErrorsData>();
        public double version { get => _version; }
        public Dictionary<Errors, NGEL.Data.Tables.Models.ErrorsData> datas { get => _datas; }
        private object lockObject = new object();

        public ErrorsDataTableService(ILogger<ErrorsDataTableService> logger)
        {
            _logger = logger;
        }

        public void SetTable(Dictionary<Errors, NGEL.Data.Tables.Models.ErrorsData> customDatas)
        {
            lock (lockObject)
            {
                _datas = customDatas;
            }
        }

        public void SetTableVersion(double customVersion)
        {
            lock (lockObject)
            {
                _version = customVersion;
            }
        }
    }
}
