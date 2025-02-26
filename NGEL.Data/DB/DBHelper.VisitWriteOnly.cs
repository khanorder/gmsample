using System;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Lobby;
using Newtonsoft.Json;
using MySqlConnector;
using NGEL.Data.Models;
using NGEL.Data.Tables.Models;
using NGEL.Data.Settings;
using NGEL.Data.Helpers;

namespace NGEL.Data.DB
{
    public class DBHelperVisitWriteOnly
    {
        string _conStr = "";

        readonly ILogger<DBHelperVisitWriteOnly> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperVisitWriteOnly(ILogger<DBHelperVisitWriteOnly> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
        }

        public void InitDBConnect(string connectionString)
        {
            _conStr = connectionString;
        }

        public async Task<bool> SaveVisit(string session, UInt64 fp, string deviceType, string deviceVendor, string deviceModel, string agent, string browser, string browserVersion, string engine, string engineVersion, string os, string osVersion, string host, string ip, string parameter, string path, bool scheme, string title, DateTime localTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"x_saveVisit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("__session", session);
                    cmd.Parameters.AddWithValue("__fp", fp);
                    cmd.Parameters.AddWithValue("__deviceType", deviceType);
                    cmd.Parameters.AddWithValue("__deviceVendor", deviceVendor);
                    cmd.Parameters.AddWithValue("__deviceModel", deviceModel);
                    cmd.Parameters.AddWithValue("__agent", agent);
                    cmd.Parameters.AddWithValue("__browser", browser);
                    cmd.Parameters.AddWithValue("__browserVersion", browserVersion);
                    cmd.Parameters.AddWithValue("__engine", engine);
                    cmd.Parameters.AddWithValue("__engineVersion", engineVersion);
                    cmd.Parameters.AddWithValue("__os", os);
                    cmd.Parameters.AddWithValue("__osVersion", osVersion);
                    cmd.Parameters.AddWithValue("__host", host);
                    cmd.Parameters.AddWithValue("__ip", ip);
                    cmd.Parameters.AddWithValue("__parameter", parameter);
                    cmd.Parameters.AddWithValue("__path", path);
                    cmd.Parameters.AddWithValue("__scheme", (scheme ? 1 : 0));
                    cmd.Parameters.AddWithValue("__title", title);
                    cmd.Parameters.AddWithValue("__localTime", (localTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    var resultCount = await cmd.ExecuteNonQueryAsync();
                    if (0 < resultCount)
                        queryRes = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != con)
                    con.Close();
            }

            return queryRes;
        }

    }

}
