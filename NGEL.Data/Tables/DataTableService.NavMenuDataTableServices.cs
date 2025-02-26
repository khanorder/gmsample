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
    public class NavMenuDataTableService
    {
        private readonly ILogger<NavMenuDataTableService> _logger;
        private double _version { get; set; } = 0;
        private Dictionary<string, NGEL.Data.Tables.Models.NavMenuData> _datas { get; set; } = new Dictionary<string, NGEL.Data.Tables.Models.NavMenuData>();
        public double version { get => _version; }
        public Dictionary<string, NGEL.Data.Tables.Models.NavMenuData> datas { get => _datas; }
        private object lockObject = new object();

        public NavMenuDataTableService(ILogger<NavMenuDataTableService> logger)
        {
            _logger = logger;
        }

        public void SetTable(Dictionary<string, NGEL.Data.Tables.Models.NavMenuData> customDatas)
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
