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
    public class DBHelperLog
    {
        string _conStr = "";

        readonly ILogger<DBHelperLog> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperLog(ILogger<DBHelperLog> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, List<NGEL.Data.Models.BiskitLog>)> SelectBiskitLogs()
        {
            bool queryRes = false;
            List<NGEL.Data.Models.BiskitLog> ret = new List<NGEL.Data.Models.BiskitLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `log_id`, `event_group_id`, `event_id`, `timestamp`, `sequence_number`, `stove_member_no`, `stove_nickname_no`, `account_id`, `account_level`, `account_name`, `character_id`, `character_level`, `session_id`, `market_code`, `server_code`, `channel_code`, `ip_address`, `device_id`, `device_type`, `device_model`, `os`, `v1`, `v2`, `v3`, `v4`, `v5`, `v6`, `v7`, `v8`, `v9`, `v10`, `v11`, `v12`, `v13`, `v14`, `v15`, `v16`, `v17`, `v18`, `v19`, `v20`, `v21`, `v22`, `v23`, `v24`, `v25`, `v26`, `v27`, `v28`, `v29`, `v30` from biskit_log", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NGEL.Data.Models.BiskitLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.BiskitLog();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.logID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.eventGroupID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.eventID = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.timestamp = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.sequenceNumber = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.stoveMemberNO = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.stoveNickNameNO = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.accountID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.accountLevel = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.accountName = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.characterID = ret10;
                        rdr.ConvertDBData(11, out int ret11); retVar.characterLevel = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.sessionID = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.marketCode = ret13;
                        rdr.ConvertDBData(14, out string ret14); retVar.serverCode = ret14;
                        rdr.ConvertDBData(15, out string ret15); retVar.channelCode = ret15;
                        rdr.ConvertDBData(16, out string ret16); retVar.ipAddress = ret16;
                        rdr.ConvertDBData(17, out string ret17); retVar.deviceID = ret17;
                        rdr.ConvertDBData(18, out string ret18); retVar.deviceType = ret18;
                        rdr.ConvertDBData(19, out string ret19); retVar.deviceModel = ret19;
                        rdr.ConvertDBData(20, out string ret20); retVar.os = ret20;
                        rdr.ConvertDBData(21, out string ret21); retVar.v01 = ret21;
                        rdr.ConvertDBData(22, out string ret22); retVar.v02 = ret22;
                        rdr.ConvertDBData(23, out string ret23); retVar.v03 = ret23;
                        rdr.ConvertDBData(24, out string ret24); retVar.v04 = ret24;
                        rdr.ConvertDBData(25, out string ret25); retVar.v05 = ret25;
                        rdr.ConvertDBData(26, out string ret26); retVar.v06 = ret26;
                        rdr.ConvertDBData(27, out string ret27); retVar.v07 = ret27;
                        rdr.ConvertDBData(28, out string ret28); retVar.v08 = ret28;
                        rdr.ConvertDBData(29, out string ret29); retVar.v09 = ret29;
                        rdr.ConvertDBData(30, out string ret30); retVar.v10 = ret30;
                        rdr.ConvertDBData(31, out string ret31); retVar.v11 = ret31;
                        rdr.ConvertDBData(32, out string ret32); retVar.v12 = ret32;
                        rdr.ConvertDBData(33, out string ret33); retVar.v13 = ret33;
                        rdr.ConvertDBData(34, out string ret34); retVar.v14 = ret34;
                        rdr.ConvertDBData(35, out string ret35); retVar.v15 = ret35;
                        rdr.ConvertDBData(36, out string ret36); retVar.v16 = ret36;
                        rdr.ConvertDBData(37, out string ret37); retVar.v17 = ret37;
                        rdr.ConvertDBData(38, out string ret38); retVar.v18 = ret38;
                        rdr.ConvertDBData(39, out string ret39); retVar.v19 = ret39;
                        rdr.ConvertDBData(40, out string ret40); retVar.v20 = ret40;
                        rdr.ConvertDBData(41, out string ret41); retVar.v21 = ret41;
                        rdr.ConvertDBData(42, out string ret42); retVar.v22 = ret42;
                        rdr.ConvertDBData(43, out string ret43); retVar.v23 = ret43;
                        rdr.ConvertDBData(44, out string ret44); retVar.v24 = ret44;
                        rdr.ConvertDBData(45, out string ret45); retVar.v25 = ret45;
                        rdr.ConvertDBData(46, out string ret46); retVar.v26 = ret46;
                        rdr.ConvertDBData(47, out string ret47); retVar.v27 = ret47;
                        rdr.ConvertDBData(48, out string ret48); retVar.v28 = ret48;
                        rdr.ConvertDBData(49, out string ret49); retVar.v29 = ret49;
                        rdr.ConvertDBData(50, out string ret50); retVar.v30 = ret50;
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

        public async Task<(bool, Int64)> SelectBiskitLogsTotalCount()
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
                    var cmd = new MySqlCommand($"select count(*) as cnt from biskit_log", con);
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

        public async Task<(bool, List<NGEL.Data.Models.BiskitLog>)> SelectBiskitLogs(DateTime startDate, DateTime endDate, Defines.UserSearchType userSearchType, string userSearchValue)
        {
            bool queryRes = false;
            List<NGEL.Data.Models.BiskitLog> ret = new List<NGEL.Data.Models.BiskitLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `log_id`, `event_group_id`, `event_id`, `timestamp`, `sequence_number`, `stove_member_no`, `stove_nickname_no`, `account_id`, `account_level`, `account_name`, `character_id`, `character_level`, `session_id`, `market_code`, `server_code`, `channel_code`, `ip_address`, `device_id`, `device_type`, `device_model`, `os`, `v1`, `v2`, `v3`, `v4`, `v5`, `v6`, `v7`, `v8`, `v9`, `v10`, `v11`, `v12`, `v13`, `v14`, `v15`, `v16`, `v17`, `v18`, `v19`, `v20`, `v21`, `v22`, `v23`, `v24`, `v25`, `v26`, `v27`, `v28`, `v29`, `v30` from biskit_log where `timestamp` >= @01 and `timestamp` <= @02 and case when @03 = 1 then `account_id` when @03 = 2 then `account_name` when @03 = 3 then `stove_member_no` when @03 = 4 then `stove_nickname_no` else `account_id` end = @04", con);
                    cmd.Parameters.AddWithValue("@01", (startDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@03", (Int64)userSearchType);
                    cmd.Parameters.AddWithValue("@04", userSearchValue);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NGEL.Data.Models.BiskitLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.BiskitLog();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.logID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.eventGroupID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.eventID = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.timestamp = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.sequenceNumber = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.stoveMemberNO = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.stoveNickNameNO = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.accountID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.accountLevel = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.accountName = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.characterID = ret10;
                        rdr.ConvertDBData(11, out int ret11); retVar.characterLevel = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.sessionID = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.marketCode = ret13;
                        rdr.ConvertDBData(14, out string ret14); retVar.serverCode = ret14;
                        rdr.ConvertDBData(15, out string ret15); retVar.channelCode = ret15;
                        rdr.ConvertDBData(16, out string ret16); retVar.ipAddress = ret16;
                        rdr.ConvertDBData(17, out string ret17); retVar.deviceID = ret17;
                        rdr.ConvertDBData(18, out string ret18); retVar.deviceType = ret18;
                        rdr.ConvertDBData(19, out string ret19); retVar.deviceModel = ret19;
                        rdr.ConvertDBData(20, out string ret20); retVar.os = ret20;
                        rdr.ConvertDBData(21, out string ret21); retVar.v01 = ret21;
                        rdr.ConvertDBData(22, out string ret22); retVar.v02 = ret22;
                        rdr.ConvertDBData(23, out string ret23); retVar.v03 = ret23;
                        rdr.ConvertDBData(24, out string ret24); retVar.v04 = ret24;
                        rdr.ConvertDBData(25, out string ret25); retVar.v05 = ret25;
                        rdr.ConvertDBData(26, out string ret26); retVar.v06 = ret26;
                        rdr.ConvertDBData(27, out string ret27); retVar.v07 = ret27;
                        rdr.ConvertDBData(28, out string ret28); retVar.v08 = ret28;
                        rdr.ConvertDBData(29, out string ret29); retVar.v09 = ret29;
                        rdr.ConvertDBData(30, out string ret30); retVar.v10 = ret30;
                        rdr.ConvertDBData(31, out string ret31); retVar.v11 = ret31;
                        rdr.ConvertDBData(32, out string ret32); retVar.v12 = ret32;
                        rdr.ConvertDBData(33, out string ret33); retVar.v13 = ret33;
                        rdr.ConvertDBData(34, out string ret34); retVar.v14 = ret34;
                        rdr.ConvertDBData(35, out string ret35); retVar.v15 = ret35;
                        rdr.ConvertDBData(36, out string ret36); retVar.v16 = ret36;
                        rdr.ConvertDBData(37, out string ret37); retVar.v17 = ret37;
                        rdr.ConvertDBData(38, out string ret38); retVar.v18 = ret38;
                        rdr.ConvertDBData(39, out string ret39); retVar.v19 = ret39;
                        rdr.ConvertDBData(40, out string ret40); retVar.v20 = ret40;
                        rdr.ConvertDBData(41, out string ret41); retVar.v21 = ret41;
                        rdr.ConvertDBData(42, out string ret42); retVar.v22 = ret42;
                        rdr.ConvertDBData(43, out string ret43); retVar.v23 = ret43;
                        rdr.ConvertDBData(44, out string ret44); retVar.v24 = ret44;
                        rdr.ConvertDBData(45, out string ret45); retVar.v25 = ret45;
                        rdr.ConvertDBData(46, out string ret46); retVar.v26 = ret46;
                        rdr.ConvertDBData(47, out string ret47); retVar.v27 = ret47;
                        rdr.ConvertDBData(48, out string ret48); retVar.v28 = ret48;
                        rdr.ConvertDBData(49, out string ret49); retVar.v29 = ret49;
                        rdr.ConvertDBData(50, out string ret50); retVar.v30 = ret50;
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

        public async Task<(bool, Int64)> SelectBiskitLogsTotalCount(DateTime startDate, DateTime endDate, Defines.UserSearchType userSearchType, string userSearchValue)
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
                    var cmd = new MySqlCommand($"select count(*) as cnt from biskit_log where `timestamp` >= @01 and `timestamp` <= @02 and case when @03 = 1 then `account_id` when @03 = 2 then `account_name` when @03 = 3 then `stove_member_no` when @03 = 4 then `stove_nickname_no` else `account_id` end = @04", con);
                    cmd.Parameters.AddWithValue("@01", (startDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@03", (Int64)userSearchType);
                    cmd.Parameters.AddWithValue("@04", userSearchValue);
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

        public async Task<(bool, List<NGEL.Data.Models.BiskitLog>)> SelectBiskitLogs(DateTime startDate, DateTime endDate, string eventIDList)
        {
            bool queryRes = false;
            List<NGEL.Data.Models.BiskitLog> ret = new List<NGEL.Data.Models.BiskitLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `log_id`, `event_group_id`, `event_id`, `timestamp`, `sequence_number`, `stove_member_no`, `stove_nickname_no`, `account_id`, `account_level`, `account_name`, `character_id`, `character_level`, `session_id`, `market_code`, `server_code`, `channel_code`, `ip_address`, `device_id`, `device_type`, `device_model`, `os`, `v1`, `v2`, `v3`, `v4`, `v5`, `v6`, `v7`, `v8`, `v9`, `v10`, `v11`, `v12`, `v13`, `v14`, `v15`, `v16`, `v17`, `v18`, `v19`, `v20`, `v21`, `v22`, `v23`, `v24`, `v25`, `v26`, `v27`, `v28`, `v29`, `v30` from biskit_log where `timestamp` >= @01 and `timestamp` <= @02 and `event_id` in ({eventIDList})", con);
                    cmd.Parameters.AddWithValue("@01", (startDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NGEL.Data.Models.BiskitLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.BiskitLog();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.logID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.eventGroupID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.eventID = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.timestamp = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.sequenceNumber = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.stoveMemberNO = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.stoveNickNameNO = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.accountID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.accountLevel = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.accountName = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.characterID = ret10;
                        rdr.ConvertDBData(11, out int ret11); retVar.characterLevel = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.sessionID = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.marketCode = ret13;
                        rdr.ConvertDBData(14, out string ret14); retVar.serverCode = ret14;
                        rdr.ConvertDBData(15, out string ret15); retVar.channelCode = ret15;
                        rdr.ConvertDBData(16, out string ret16); retVar.ipAddress = ret16;
                        rdr.ConvertDBData(17, out string ret17); retVar.deviceID = ret17;
                        rdr.ConvertDBData(18, out string ret18); retVar.deviceType = ret18;
                        rdr.ConvertDBData(19, out string ret19); retVar.deviceModel = ret19;
                        rdr.ConvertDBData(20, out string ret20); retVar.os = ret20;
                        rdr.ConvertDBData(21, out string ret21); retVar.v01 = ret21;
                        rdr.ConvertDBData(22, out string ret22); retVar.v02 = ret22;
                        rdr.ConvertDBData(23, out string ret23); retVar.v03 = ret23;
                        rdr.ConvertDBData(24, out string ret24); retVar.v04 = ret24;
                        rdr.ConvertDBData(25, out string ret25); retVar.v05 = ret25;
                        rdr.ConvertDBData(26, out string ret26); retVar.v06 = ret26;
                        rdr.ConvertDBData(27, out string ret27); retVar.v07 = ret27;
                        rdr.ConvertDBData(28, out string ret28); retVar.v08 = ret28;
                        rdr.ConvertDBData(29, out string ret29); retVar.v09 = ret29;
                        rdr.ConvertDBData(30, out string ret30); retVar.v10 = ret30;
                        rdr.ConvertDBData(31, out string ret31); retVar.v11 = ret31;
                        rdr.ConvertDBData(32, out string ret32); retVar.v12 = ret32;
                        rdr.ConvertDBData(33, out string ret33); retVar.v13 = ret33;
                        rdr.ConvertDBData(34, out string ret34); retVar.v14 = ret34;
                        rdr.ConvertDBData(35, out string ret35); retVar.v15 = ret35;
                        rdr.ConvertDBData(36, out string ret36); retVar.v16 = ret36;
                        rdr.ConvertDBData(37, out string ret37); retVar.v17 = ret37;
                        rdr.ConvertDBData(38, out string ret38); retVar.v18 = ret38;
                        rdr.ConvertDBData(39, out string ret39); retVar.v19 = ret39;
                        rdr.ConvertDBData(40, out string ret40); retVar.v20 = ret40;
                        rdr.ConvertDBData(41, out string ret41); retVar.v21 = ret41;
                        rdr.ConvertDBData(42, out string ret42); retVar.v22 = ret42;
                        rdr.ConvertDBData(43, out string ret43); retVar.v23 = ret43;
                        rdr.ConvertDBData(44, out string ret44); retVar.v24 = ret44;
                        rdr.ConvertDBData(45, out string ret45); retVar.v25 = ret45;
                        rdr.ConvertDBData(46, out string ret46); retVar.v26 = ret46;
                        rdr.ConvertDBData(47, out string ret47); retVar.v27 = ret47;
                        rdr.ConvertDBData(48, out string ret48); retVar.v28 = ret48;
                        rdr.ConvertDBData(49, out string ret49); retVar.v29 = ret49;
                        rdr.ConvertDBData(50, out string ret50); retVar.v30 = ret50;
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

        public async Task<(bool, Int64)> SelectBiskitLogsTotalCount(DateTime startDate, DateTime endDate, string eventIDList)
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
                    var cmd = new MySqlCommand($"select count(*) as cnt from biskit_log where `timestamp` >= @01 and `timestamp` <= @02 and `event_id` in ({eventIDList})", con);
                    cmd.Parameters.AddWithValue("@01", (startDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<(bool, List<NGEL.Data.Models.BiskitLog>)> SelectBiskitLogs(DateTime startDate, DateTime endDate, Defines.UserSearchType userSearchType, string userSearchValue, string eventIDList)
        {
            bool queryRes = false;
            List<NGEL.Data.Models.BiskitLog> ret = new List<NGEL.Data.Models.BiskitLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `log_id`, `event_group_id`, `event_id`, `timestamp`, `sequence_number`, `stove_member_no`, `stove_nickname_no`, `account_id`, `account_level`, `account_name`, `character_id`, `character_level`, `session_id`, `market_code`, `server_code`, `channel_code`, `ip_address`, `device_id`, `device_type`, `device_model`, `os`, `v1`, `v2`, `v3`, `v4`, `v5`, `v6`, `v7`, `v8`, `v9`, `v10`, `v11`, `v12`, `v13`, `v14`, `v15`, `v16`, `v17`, `v18`, `v19`, `v20`, `v21`, `v22`, `v23`, `v24`, `v25`, `v26`, `v27`, `v28`, `v29`, `v30` from biskit_log where `timestamp` >= @01 and `timestamp` <= @02 and case when @03 = 1 then `account_id` when @03 = 2 then `account_name` when @03 = 3 then `stove_member_no` when @03 = 4 then `stove_nickname_no` else `account_id` end = @04 and `event_id` in ({eventIDList})", con);
                    cmd.Parameters.AddWithValue("@01", (startDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@03", (Int64)userSearchType);
                    cmd.Parameters.AddWithValue("@04", userSearchValue);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NGEL.Data.Models.BiskitLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.BiskitLog();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.logID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.eventGroupID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.eventID = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.timestamp = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.sequenceNumber = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.stoveMemberNO = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.stoveNickNameNO = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.accountID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.accountLevel = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.accountName = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.characterID = ret10;
                        rdr.ConvertDBData(11, out int ret11); retVar.characterLevel = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.sessionID = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.marketCode = ret13;
                        rdr.ConvertDBData(14, out string ret14); retVar.serverCode = ret14;
                        rdr.ConvertDBData(15, out string ret15); retVar.channelCode = ret15;
                        rdr.ConvertDBData(16, out string ret16); retVar.ipAddress = ret16;
                        rdr.ConvertDBData(17, out string ret17); retVar.deviceID = ret17;
                        rdr.ConvertDBData(18, out string ret18); retVar.deviceType = ret18;
                        rdr.ConvertDBData(19, out string ret19); retVar.deviceModel = ret19;
                        rdr.ConvertDBData(20, out string ret20); retVar.os = ret20;
                        rdr.ConvertDBData(21, out string ret21); retVar.v01 = ret21;
                        rdr.ConvertDBData(22, out string ret22); retVar.v02 = ret22;
                        rdr.ConvertDBData(23, out string ret23); retVar.v03 = ret23;
                        rdr.ConvertDBData(24, out string ret24); retVar.v04 = ret24;
                        rdr.ConvertDBData(25, out string ret25); retVar.v05 = ret25;
                        rdr.ConvertDBData(26, out string ret26); retVar.v06 = ret26;
                        rdr.ConvertDBData(27, out string ret27); retVar.v07 = ret27;
                        rdr.ConvertDBData(28, out string ret28); retVar.v08 = ret28;
                        rdr.ConvertDBData(29, out string ret29); retVar.v09 = ret29;
                        rdr.ConvertDBData(30, out string ret30); retVar.v10 = ret30;
                        rdr.ConvertDBData(31, out string ret31); retVar.v11 = ret31;
                        rdr.ConvertDBData(32, out string ret32); retVar.v12 = ret32;
                        rdr.ConvertDBData(33, out string ret33); retVar.v13 = ret33;
                        rdr.ConvertDBData(34, out string ret34); retVar.v14 = ret34;
                        rdr.ConvertDBData(35, out string ret35); retVar.v15 = ret35;
                        rdr.ConvertDBData(36, out string ret36); retVar.v16 = ret36;
                        rdr.ConvertDBData(37, out string ret37); retVar.v17 = ret37;
                        rdr.ConvertDBData(38, out string ret38); retVar.v18 = ret38;
                        rdr.ConvertDBData(39, out string ret39); retVar.v19 = ret39;
                        rdr.ConvertDBData(40, out string ret40); retVar.v20 = ret40;
                        rdr.ConvertDBData(41, out string ret41); retVar.v21 = ret41;
                        rdr.ConvertDBData(42, out string ret42); retVar.v22 = ret42;
                        rdr.ConvertDBData(43, out string ret43); retVar.v23 = ret43;
                        rdr.ConvertDBData(44, out string ret44); retVar.v24 = ret44;
                        rdr.ConvertDBData(45, out string ret45); retVar.v25 = ret45;
                        rdr.ConvertDBData(46, out string ret46); retVar.v26 = ret46;
                        rdr.ConvertDBData(47, out string ret47); retVar.v27 = ret47;
                        rdr.ConvertDBData(48, out string ret48); retVar.v28 = ret48;
                        rdr.ConvertDBData(49, out string ret49); retVar.v29 = ret49;
                        rdr.ConvertDBData(50, out string ret50); retVar.v30 = ret50;
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

        public async Task<(bool, Int64)> SelectBiskitLogsTotalCount(DateTime startDate, DateTime endDate, Defines.UserSearchType userSearchType, string userSearchValue, string eventIDList)
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
                    var cmd = new MySqlCommand($"select count(*) as cnt from biskit_log where `timestamp` >= @01 and `timestamp` <= @02 and case when @03 = 1 then `account_id` when @03 = 2 then `account_name` when @03 = 3 then `stove_member_no` when @03 = 4 then `stove_nickname_no` else `account_id` end = @04 and `event_id` in ({eventIDList})", con);
                    cmd.Parameters.AddWithValue("@01", (startDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endDate.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@03", (Int64)userSearchType);
                    cmd.Parameters.AddWithValue("@04", userSearchValue);
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

    }

}
