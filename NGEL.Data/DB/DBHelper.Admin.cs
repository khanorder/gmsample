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
    public class DBHelperAdmin
    {
        string _conStr = "";

        readonly ILogger<DBHelperAdmin> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperAdmin(ILogger<DBHelperAdmin> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, List<BiskitLogEventID>)> SelectBiskitLogEventIDs()
        {
            bool queryRes = false;
            List<BiskitLogEventID> ret = new List<BiskitLogEventID>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SeqID, EventID, EvenName from BiskitLogEventID", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<BiskitLogEventID>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new BiskitLogEventID();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.SeqID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.EventID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.EventName = ret2;
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

        public async Task<(bool, List<BlockContentData>)> SelectBlockContents()
        {
            bool queryRes = false;
            List<BlockContentData> ret = new List<BlockContentData>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SeqID, PacketID, PacketName from BlockContents", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<BlockContentData>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new BlockContentData();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.SeqID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.PacketID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.PacketName = ret2;
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

        public async Task<(bool, List<BlockIP>)> SelectBlockIPs()
        {
            bool queryRes = false;
            List<BlockIP> ret = new List<BlockIP>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, IPAddress, StartTime, EndTime, Reason from BlockIP", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<BlockIP>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new BlockIP();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.IPAddress = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.StartTime = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.EndTime = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Reason = ret4;
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

        public async Task<(bool, List<BlockIP>)> SelectBlockIPs(DateTime StartTime)
        {
            bool queryRes = false;
            List<BlockIP> ret = new List<BlockIP>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, IPAddress, StartTime, EndTime, Reason from BlockIP where StartTime >= @01", con);
                    cmd.Parameters.AddWithValue("@01", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<BlockIP>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new BlockIP();
                        retVar.StartTime = StartTime;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.IPAddress = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.StartTime = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.EndTime = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Reason = ret4;
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

        public async Task<(bool, List<BlockIP>)> SelectBlockIPs(DateTime StartTime, DateTime EndTime)
        {
            bool queryRes = false;
            List<BlockIP> ret = new List<BlockIP>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, IPAddress, StartTime, EndTime, Reason from BlockIP where StartTime >= @01 and StartTime <= @02", con);
                    cmd.Parameters.AddWithValue("@01", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<BlockIP>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new BlockIP();
                        retVar.StartTime = StartTime;
                        retVar.EndTime = EndTime;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.IPAddress = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.StartTime = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.EndTime = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Reason = ret4;
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

        public async Task<(bool, BlockIP?)> SelectBlockIP(BlockIP argClass)
        {
            return await SelectBlockIP(argClass.ID);
        }

        public async Task<(bool, BlockIP?)> SelectBlockIP(Int64 ID)
        {
            bool queryRes = false;
            BlockIP? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select IPAddress, StartTime, EndTime, Reason from BlockIP where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new BlockIP();
                        retVar.ID = ID;
                        rdr.ConvertDBData(0, out string ret0); retVar.IPAddress = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.StartTime = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.EndTime = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Reason = ret3;
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

        public async Task<(bool, BlockIP?)> SelectBlockIPByAddr(BlockIP argClass)
        {
            return await SelectBlockIPByAddr(argClass.IPAddress);
        }

        public async Task<(bool, BlockIP?)> SelectBlockIPByAddr(string IPAddress)
        {
            bool queryRes = false;
            BlockIP? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, StartTime, EndTime, Reason from BlockIP where IPAddress = @01", con);
                    cmd.Parameters.AddWithValue("@01", IPAddress);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new BlockIP();
                        retVar.IPAddress = IPAddress;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.StartTime = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.EndTime = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Reason = ret3;
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

        public async Task<(bool, List<EventMail>)> SelectEventMails()
        {
            bool queryRes = false;
            List<EventMail> ret = new List<EventMail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, MailType, Title, Message, RewardList, ExpireTime, StartTime, EndTime, CreateAt, UpdateAt from EventMail", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<EventMail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new EventMail();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.MailType = (EMailType)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Title = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Message = ret3;
                        rdr.ConvertDBData(4, out List<MailReward> ret4); retVar.RewardList = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.ExpireTime = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.StartTime = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.EndTime = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.CreateAt = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.UpdateAt = ret9;
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

        public async Task<(bool, List<EventMail>)> SelectEventMails(DateTime StartTime)
        {
            bool queryRes = false;
            List<EventMail> ret = new List<EventMail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, MailType, Title, Message, RewardList, ExpireTime, StartTime, EndTime, CreateAt, UpdateAt from EventMail where StartTime >= @01", con);
                    cmd.Parameters.AddWithValue("@01", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<EventMail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new EventMail();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.MailType = (EMailType)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Title = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Message = ret3;
                        rdr.ConvertDBData(4, out List<MailReward> ret4); retVar.RewardList = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.ExpireTime = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.StartTime = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.EndTime = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.CreateAt = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.UpdateAt = ret9;
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

        public async Task<(bool, List<EventMail>)> SelectEventMails(DateTime StartTime, DateTime EndTime)
        {
            bool queryRes = false;
            List<EventMail> ret = new List<EventMail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, MailType, Title, Message, RewardList, ExpireTime, StartTime, EndTime, CreateAt, UpdateAt from EventMail where StartTime >= @01 and StartTime <= @02", con);
                    cmd.Parameters.AddWithValue("@01", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<EventMail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new EventMail();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.MailType = (EMailType)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Title = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Message = ret3;
                        rdr.ConvertDBData(4, out List<MailReward> ret4); retVar.RewardList = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.ExpireTime = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.StartTime = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.EndTime = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.CreateAt = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.UpdateAt = ret9;
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

        public async Task<(bool, EventMail?)> SelectEventMail(EventMail argClass)
        {
            return await SelectEventMail(argClass.ID);
        }

        public async Task<(bool, EventMail?)> SelectEventMail(Int64 ID)
        {
            bool queryRes = false;
            EventMail? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MailType, Title, Message, RewardList, ExpireTime, StartTime, EndTime, CreateAt, UpdateAt from EventMail where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new EventMail();
                        retVar.ID = ID;
                        rdr.ConvertDBData(0, out int ret0); retVar.MailType = (EMailType)ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Title = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Message = ret2;
                        rdr.ConvertDBData(3, out List<MailReward> ret3); retVar.RewardList = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ExpireTime = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.StartTime = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.EndTime = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.CreateAt = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.UpdateAt = ret8;
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

        public async Task<(bool, List<Maintenance>)> SelectMaintenances()
        {
            bool queryRes = false;
            List<Maintenance> ret = new List<Maintenance>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MaintenanceID, Platform, Area, State, StartTime, EndTime, NoticeStrID, UpdateStrID, RecomUpdateStrID, CreateAt, UpdateAt from Maintenance", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Maintenance>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Maintenance();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.MaintenanceID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.Platform = (Defines.MaintenancePlatform)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Area = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.State = (Defines.MaintenanceState)ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.StartTime = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.EndTime = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.NoticeStrID = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.UpdateStrID = ret7;
                        rdr.ConvertDBData(8, out string ret8); retVar.RecomUpdateStrID = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.CreateAt = ret9;
                        rdr.ConvertDBData(10, out DateTime ret10); retVar.UpdateAt = ret10;
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

        public async Task<(bool, Maintenance?)> SelectMaintenance(Maintenance argClass)
        {
            return await SelectMaintenance(argClass.Platform, argClass.Area);
        }

        public async Task<(bool, Maintenance?)> SelectMaintenance(Defines.MaintenancePlatform Platform, string Area)
        {
            bool queryRes = false;
            Maintenance? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MaintenanceID, State, StartTime, EndTime, NoticeStrID, UpdateStrID, RecomUpdateStrID, CreateAt, UpdateAt from Maintenance where Platform = @01 and Area = @02", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)Platform);
                    cmd.Parameters.AddWithValue("@02", Area);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Maintenance();
                        retVar.Platform = Platform;
                        retVar.Area = Area;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.MaintenanceID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.State = (Defines.MaintenanceState)ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.StartTime = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.EndTime = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.NoticeStrID = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.UpdateStrID = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.RecomUpdateStrID = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.CreateAt = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.UpdateAt = ret8;
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

        public async Task<(bool, Maintenance?)> SelectMaintenanceByID(Maintenance argClass)
        {
            return await SelectMaintenanceByID(argClass.MaintenanceID);
        }

        public async Task<(bool, Maintenance?)> SelectMaintenanceByID(Int64 MaintenanceID)
        {
            bool queryRes = false;
            Maintenance? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select Platform, Area, State, StartTime, EndTime, NoticeStrID, UpdateStrID, RecomUpdateStrID, CreateAt, UpdateAt from Maintenance where MaintenanceID = @01", con);
                    cmd.Parameters.AddWithValue("@01", MaintenanceID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Maintenance();
                        retVar.MaintenanceID = MaintenanceID;
                        rdr.ConvertDBData(0, out int ret0); retVar.Platform = (Defines.MaintenancePlatform)ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Area = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.State = (Defines.MaintenanceState)ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.StartTime = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.EndTime = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.NoticeStrID = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.UpdateStrID = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.RecomUpdateStrID = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.CreateAt = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.UpdateAt = ret9;
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

        public async Task<(bool, List<Maintenance>)> SelectCurrentMaintenances()
        {
            bool queryRes = false;
            List<Maintenance> ret = new List<Maintenance>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MaintenanceID, Platform, Area, State, StartTime, EndTime, NoticeStrID, UpdateStrID, RecomUpdateStrID, CreateAt, UpdateAt from Maintenance where State = 2 and UTC_TIMESTAMP() BETWEEN StartTime and EndTime", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Maintenance>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Maintenance();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.MaintenanceID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.Platform = (Defines.MaintenancePlatform)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Area = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.State = (Defines.MaintenanceState)ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.StartTime = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.EndTime = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.NoticeStrID = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.UpdateStrID = ret7;
                        rdr.ConvertDBData(8, out string ret8); retVar.RecomUpdateStrID = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.CreateAt = ret9;
                        rdr.ConvertDBData(10, out DateTime ret10); retVar.UpdateAt = ret10;
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

        public async Task<(bool, List<NoticeBanner>)> SelectNoticeBanners()
        {
            bool queryRes = false;
            List<NoticeBanner> ret = new List<NoticeBanner>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select BannerID, StartAt, EndAt, ImageURL, Title, Message, CreateAt, UpdateAt from NoticeBanner", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NoticeBanner>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NoticeBanner();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.BannerID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.StartAt = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.EndAt = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.ImageURL = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Title = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.Message = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.CreateAt = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.UpdateAt = ret7;
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

        public async Task<(bool, NoticeBanner?)> SelectNoticeBanner(Int64 BannerID)
        {
            bool queryRes = false;
            NoticeBanner? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select StartAt, EndAt, ImageURL, Title, Message, CreateAt, UpdateAt from NoticeBanner where BannerID = @01", con);
                    cmd.Parameters.AddWithValue("@01", BannerID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new NoticeBanner();
                        retVar.BannerID = BannerID;
                        rdr.ConvertDBData(0, out int ret0); retVar.StartAt = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.EndAt = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.ImageURL = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Title = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Message = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.CreateAt = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.UpdateAt = ret6;
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

        public async Task<(bool, List<Slang>)> SelectSlangs()
        {
            bool queryRes = false;
            List<Slang> ret = new List<Slang>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, Word from Slang", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Slang>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Slang();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Word = ret1;
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

        public async Task<(bool, List<Slang>)> SelectSlangs(Slang argClass)
        {
            return await SelectSlangs(argClass.Word);
        }

        public async Task<(bool, List<Slang>)> SelectSlangs(string Word)
        {
            bool queryRes = false;
            List<Slang> ret = new List<Slang>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, Word from Slang where Word like concat('%', @01, '%')", con);
                    cmd.Parameters.AddWithValue("@01", Word);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Slang>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Slang();
                        retVar.Word = Word;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Word = ret1;
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

        public async Task<(bool, Slang?)> SelectSlangByID(Slang argClass)
        {
            return await SelectSlangByID(argClass.ID);
        }

        public async Task<(bool, Slang?)> SelectSlangByID(Int64 ID)
        {
            bool queryRes = false;
            Slang? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select Word from Slang where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Slang();
                        retVar.ID = ID;
                        rdr.ConvertDBData(0, out string ret0); retVar.Word = ret0;
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

        public async Task<(bool, Slang?)> SelectSlangByWord(Slang argClass)
        {
            return await SelectSlangByWord(argClass.Word);
        }

        public async Task<(bool, Slang?)> SelectSlangByWord(string Word)
        {
            bool queryRes = false;
            Slang? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID from Slang where Word = @01", con);
                    cmd.Parameters.AddWithValue("@01", Word);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Slang();
                        retVar.Word = Word;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
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

        public async Task<(bool, List<WhiteList>)> SelectWhiteLists()
        {
            bool queryRes = false;
            List<WhiteList> ret = new List<WhiteList>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, DeviceID, MemberNo, Comment, CreateAt, UpdateAt from WhiteList", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<WhiteList>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new WhiteList();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.DeviceID = ret1;
                        rdr.ConvertDBData(2, out UInt64 ret2); retVar.MemberNo = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.Comment = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.CreateAt = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.UpdateAt = ret5;
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

        public async Task<(bool, WhiteList?)> SelectWhiteList(WhiteList argClass)
        {
            return await SelectWhiteList(argClass.ID);
        }

        public async Task<(bool, WhiteList?)> SelectWhiteList(Int64 ID)
        {
            bool queryRes = false;
            WhiteList? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select DeviceID, MemberNo, Comment, CreateAt, UpdateAt from WhiteList where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new WhiteList();
                        retVar.ID = ID;
                        rdr.ConvertDBData(0, out string ret0); retVar.DeviceID = ret0;
                        rdr.ConvertDBData(1, out UInt64 ret1); retVar.MemberNo = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Comment = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.CreateAt = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.UpdateAt = ret4;
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

        public async Task<(bool, WhiteList?)> SelectWhiteListByKey(WhiteList argClass)
        {
            return await SelectWhiteListByKey(argClass.DeviceID, argClass.MemberNo);
        }

        public async Task<(bool, WhiteList?)> SelectWhiteListByKey(string DeviceID, UInt64 MemberNo)
        {
            bool queryRes = false;
            WhiteList? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, Comment, CreateAt, UpdateAt from WhiteList where DeviceID = @01 and MemberNo = @02", con);
                    cmd.Parameters.AddWithValue("@01", DeviceID);
                    cmd.Parameters.AddWithValue("@02", MemberNo);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new WhiteList();
                        retVar.DeviceID = DeviceID;
                        retVar.MemberNo = MemberNo;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Comment = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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
