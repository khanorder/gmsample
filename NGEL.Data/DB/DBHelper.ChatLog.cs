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
    public class DBHelperChatLog
    {
        string _conStr = "";

        readonly ILogger<DBHelperChatLog> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperChatLog(ILogger<DBHelperChatLog> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, List<ChatLog>)> SelectChatLogs(string searchDate)
        {
            bool queryRes = false;
            List<ChatLog> ret = new List<ChatLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @q = concat('select `event_id`, `stove_member_no`, `stove_nickname_no`, `account_id`, `account_name`, `session_id`, `ip_address`, `message`, `timestamp` from ChatLog_', @01); PREPARE stmt from @q; EXECUTE stmt; DEALLOCATE PREPARE stmt;", con);
                    cmd.Parameters.AddWithValue("@01", searchDate);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<ChatLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new ChatLog();
                        rdr.ConvertDBData(0, out string ret0); retVar.eventId = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.stoveMemberNo = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.stoveNicknameNo = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.accountId = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.accountName = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.sessionId = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.ipAddress = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.message = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.timeStamp = ret8;
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

        public async Task<(bool, List<ChatLog>)> SelectChatLogs(string startDate, string endDate)
        {
            bool queryRes = false;
            List<ChatLog> ret = new List<ChatLog>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"x_searchChatLogs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("__startDate", startDate);
                    cmd.Parameters.AddWithValue("__endDate", endDate);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<ChatLog>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new ChatLog();
                        rdr.ConvertDBData(0, out string ret0); retVar.eventId = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.stoveMemberNo = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.stoveNicknameNo = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.accountId = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.accountName = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.sessionId = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.ipAddress = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.message = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.timeStamp = ret8;
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
