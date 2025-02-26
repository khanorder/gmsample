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
    public class DBHelperGMLog
    {
        string _conStr = "";

        readonly ILogger<DBHelperGMLog> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperGMLog(ILogger<DBHelperGMLog> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, List<GMCombinedLog>)> SelectCombinedLogs()
        {
            bool queryRes = false;
            List<GMCombinedLog> ret = new List<GMCombinedLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @gmDatabase = replace(database(), '_log', ''); set @query = concat('select a.id, a.userId, b.`name`, a.`type`, a.methodId, c.`name`, a.urlId, d.`name`, a.errorId, e.`name`, a.userAgentId, f.`name`, a.`message`, a.`remoteAddress`, a.regTime from logs as a left join ', @gmDatabase, '.`users` as b on a.userId = b.id left join log_methods as c on a.methodId = c.id left join log_urls as d on a.urlId = d.id left join log_errors as e on a.errorId = e.id left join log_user_agents as f on a.userAgentId = f.id'); PREPARE stmt FROM @query; EXECUTE stmt; DEALLOCATE PREPARE stmt;", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMCombinedLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMCombinedLog();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.userId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.userName = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.type = (Defines.GMLogType)ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.methodId = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.methodName = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.urlId = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.urlName = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.errorId = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.errorName = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.userAgentId = ret10;
                        rdr.ConvertDBData(11, out string ret11); retVar.userAgentName = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.message = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.remoteAddress = ret13;
                        rdr.ConvertDBData(14, out DateTime ret14); retVar.regTime = ret14;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMCombinedLog>)> SelectCombinedLogs(DateTime startTime, DateTime endTime)
        {
            bool queryRes = false;
            List<GMCombinedLog> ret = new List<GMCombinedLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"x_selectCombinedLogs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("__startTime", (startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("__endTime", (endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMCombinedLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMCombinedLog();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.userId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.userName = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.type = (Defines.GMLogType)ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.methodId = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.methodName = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.urlId = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.urlName = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.errorId = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.errorName = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.userAgentId = ret10;
                        rdr.ConvertDBData(11, out string ret11); retVar.userAgentName = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.message = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.remoteAddress = ret13;
                        rdr.ConvertDBData(14, out DateTime ret14); retVar.regTime = ret14;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMLog>)> SelectLogs(DateTime startTime, DateTime endTime)
        {
            bool queryRes = false;
            List<GMLog> ret = new List<GMLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from logs where regTime >= @01 and regTime <= @02", con);
                    cmd.Parameters.AddWithValue("@01", (startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMLog();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.userId = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.type = (Defines.GMLogType)ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.methodId = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.errorId = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.message = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.regTime = ret6;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMLogError>)> SelectLogErrors()
        {
            bool queryRes = false;
            List<GMLogError> ret = new List<GMLogError>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_errors", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMLogError>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMLogError();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, GMLogError?)> SelectLogErrorByName(string name)
        {
            bool queryRes = false;
            GMLogError? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_errors where name = @01", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new GMLogError();
                        retVar.name = name;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        ret = retVar;
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMLogMethod>)> SelectLogMethods()
        {
            bool queryRes = false;
            List<GMLogMethod> ret = new List<GMLogMethod>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_methods", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMLogMethod>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMLogMethod();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, GMLogMethod?)> SelectLogMethodByName(string name)
        {
            bool queryRes = false;
            GMLogMethod? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_methods where name = @01", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new GMLogMethod();
                        retVar.name = name;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        ret = retVar;
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMLogURL>)> SelectLogURLs()
        {
            bool queryRes = false;
            List<GMLogURL> ret = new List<GMLogURL>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_urls", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMLogURL>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMLogURL();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, GMLogURL?)> SelectLogURLByName(string name)
        {
            bool queryRes = false;
            GMLogURL? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_urls where name = @01", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new GMLogURL();
                        retVar.name = name;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        ret = retVar;
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMLogUserAgent>)> SelectLogUserAgents()
        {
            bool queryRes = false;
            List<GMLogUserAgent> ret = new List<GMLogUserAgent>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_user_agents", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMLogUserAgent>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMLogUserAgent();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, GMLogUserAgent?)> SelectLogUserAgentByName(string name)
        {
            bool queryRes = false;
            GMLogUserAgent? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from log_user_agents where name = @01", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new GMLogUserAgent();
                        retVar.name = name;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.id = ret0;
                        ret = retVar;
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, Int64)> SelectExpiredLogCount(DateTime expireTime)
        {
            bool queryRes = false;
            Int64 ret = 0;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select count(`id`) from `logs` where `regTime` < @01", con);
                    cmd.Parameters.AddWithValue("@01", (expireTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Int64 retVar);
                        ret = retVar;
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

        public async Task<(bool, List<GMLog>)> SelectPWLogs()
        {
            bool queryRes = false;
            List<GMLog> ret = new List<GMLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from `logs` where `message` regexp 'password\":\"[^\"]+\"'", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GMLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GMLog();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.userId = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.type = (Defines.GMLogType)ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.methodId = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.urlId = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.errorId = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.userAgentId = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.message = ret7;
                        rdr.ConvertDBData(8, out string ret8); retVar.remoteAddress = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.regTime = ret9;
                        ret.Add(retVar);
                    }
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
                if (null != rdr)
                    rdr.Close();
                if (null != con)
                    con.Close();
            }

            return (queryRes, ret);
        }

    }

}
