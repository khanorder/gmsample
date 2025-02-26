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
    public class DBHelperGMLogWriteOnly
    {
        string _conStr = "";

        readonly ILogger<DBHelperGMLogWriteOnly> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperGMLogWriteOnly(ILogger<DBHelperGMLogWriteOnly> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<bool> InsertLog(GMLog argClass)
        {
            return await InsertLog(argClass.userId, argClass.type, argClass.methodId, argClass.urlId, argClass.errorId, argClass.userAgentId, argClass.message, argClass.remoteAddress);
        }

        public async Task<bool> InsertLog(string userId, Defines.GMLogType type, Int64 methodId, Int64 urlId, Int64 errorId, Int64 userAgentId, string message, string remoteAddress)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into `logs` (userId, `type`, methodId, urlId, errorId, userAgentId, `message`, `remoteAddress`, regTime) values (@01, @02, @03, @04, @05, @06, @07, @08, UTC_TIMESTAMP(6))", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", (Int64)type);
                    cmd.Parameters.AddWithValue("@03", methodId);
                    cmd.Parameters.AddWithValue("@04", urlId);
                    cmd.Parameters.AddWithValue("@05", errorId);
                    cmd.Parameters.AddWithValue("@06", userAgentId);
                    cmd.Parameters.AddWithValue("@07", message);
                    cmd.Parameters.AddWithValue("@08", remoteAddress);
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

        public async Task<bool> InsertLogError(GMLogError argClass)
        {
            return await InsertLogError(argClass.id, argClass.name);
        }

        public async Task<bool> InsertLogError(Int64 id, string name)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into log_errors (`id`, `name`) values (@01, @02)", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", name);
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

        public async Task<bool> InsertLogMethod(GMLogMethod argClass)
        {
            return await InsertLogMethod(argClass.id, argClass.name);
        }

        public async Task<bool> InsertLogMethod(Int64 id, string name)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into log_methods (`id`, `name`) values (@01, @02)", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", name);
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

        public async Task<bool> InsertLogURL(GMLogURL argClass)
        {
            return await InsertLogURL(argClass.id, argClass.name);
        }

        public async Task<bool> InsertLogURL(Int64 id, string name)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into log_urls (`id`, `name`) values (@01, @02)", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", name);
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

        public async Task<bool> InsertLogUserAgent(GMLogUserAgent argClass)
        {
            return await InsertLogUserAgent(argClass.id, argClass.name);
        }

        public async Task<bool> InsertLogUserAgent(Int64 id, string name)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into log_user_agents (`id`, `name`) values (@01, @02)", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", name);
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

        public async Task<bool> UpdateLogMessage(GMLog argClass)
        {
            return await UpdateLogMessage(argClass.id, argClass.message);
        }

        public async Task<bool> UpdateLogMessage(UInt64 id, string message)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update `logs` set `message` = @02 where `id` = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", message);
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

        public async Task<bool> DeleteExpiredLogs(DateTime expireTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from `logs` where `regTime` < @01", con);
                    cmd.Parameters.AddWithValue("@01", (expireTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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
