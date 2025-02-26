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
    public class DBHelperAdminWriteOnly
    {
        string _conStr = "";

        readonly ILogger<DBHelperAdminWriteOnly> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperAdminWriteOnly(ILogger<DBHelperAdminWriteOnly> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, Int64)> InsertBlockIP(BlockIP argClass)
        {
            return await InsertBlockIP(argClass.IPAddress, argClass.StartTime, argClass.EndTime, argClass.Reason);
        }

        public async Task<(bool, Int64)> InsertBlockIP(string IPAddress, DateTime StartTime, DateTime EndTime, string Reason)
        {
            bool queryRes = false;
            Int64 ret = 0;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into BlockIP (IPAddress, StartTime, EndTime, Reason) values (@01, @02, @03, @04); select last_insert_id();", con);
                    cmd.Parameters.AddWithValue("@01", IPAddress);
                    cmd.Parameters.AddWithValue("@02", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@03", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@04", Reason);
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

        public async Task<bool> UpdateBlockIP(BlockIP argClass)
        {
            return await UpdateBlockIP(argClass.ID, argClass.IPAddress, argClass.StartTime, argClass.EndTime, argClass.Reason);
        }

        public async Task<bool> UpdateBlockIP(Int64 ID, string IPAddress, DateTime StartTime, DateTime EndTime, string Reason)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update BlockIP set IPAddress = @02, StartTime = @03, EndTime = @04, Reason = @05 where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    cmd.Parameters.AddWithValue("@02", IPAddress);
                    cmd.Parameters.AddWithValue("@03", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@04", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@05", Reason);
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

        public async Task<bool> DeleteBlockIP(BlockIP argClass)
        {
            return await DeleteBlockIP(argClass.ID);
        }

        public async Task<bool> DeleteBlockIP(Int64 ID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from BlockIP where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
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

        public async Task<(bool, EventMail?)> InsertEventMail(EventMail argClass)
        {
            return await InsertEventMail(argClass.MailType, argClass.Title, argClass.Message, argClass.RewardList, argClass.ExpireTime, argClass.StartTime, argClass.EndTime);
        }

        public async Task<(bool, EventMail?)> InsertEventMail(EMailType MailType, string Title, string Message, List<MailReward> RewardList, int ExpireTime, DateTime StartTime, DateTime EndTime)
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
                    var cmd = new MySqlCommand($"insert into EventMail (MailType, Title, Message, RewardList, ExpireTime, StartTime, EndTime) values (@01, @02, @03, @04, @05, @06, @07);select last_insert_id();", con);
                    cmd.Parameters.AddWithValue("@01", MailType);
                    cmd.Parameters.AddWithValue("@02", Title);
                    cmd.Parameters.AddWithValue("@03", Message);
                    cmd.Parameters.AddWithValue("@04", JsonConvert.SerializeObject(RewardList));
                    cmd.Parameters.AddWithValue("@05", ExpireTime);
                    cmd.Parameters.AddWithValue("@06", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@07", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new EventMail();
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

        public async Task<bool> UpdateEventMail(EventMail argClass)
        {
            return await UpdateEventMail(argClass.ID, argClass.MailType, argClass.Title, argClass.Message, argClass.RewardList, argClass.ExpireTime, argClass.StartTime, argClass.EndTime);
        }

        public async Task<bool> UpdateEventMail(Int64 ID, EMailType MailType, string Title, string Message, List<MailReward> RewardList, int ExpireTime, DateTime StartTime, DateTime EndTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update EventMail set MailType = @02, Title = @03, Message = @04, RewardList = @05, ExpireTime = @06, StartTime = @07, EndTime = @08 where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    cmd.Parameters.AddWithValue("@02", MailType);
                    cmd.Parameters.AddWithValue("@03", Title);
                    cmd.Parameters.AddWithValue("@04", Message);
                    cmd.Parameters.AddWithValue("@05", JsonConvert.SerializeObject(RewardList));
                    cmd.Parameters.AddWithValue("@06", ExpireTime);
                    cmd.Parameters.AddWithValue("@07", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@08", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<bool> DeleteEventMail(EventMail argClass)
        {
            return await DeleteEventMail(argClass.ID);
        }

        public async Task<bool> DeleteEventMail(Int64 ID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from EventMail where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
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

        public async Task<bool> InsertMaintenance(Maintenance argClass)
        {
            return await InsertMaintenance(argClass.Platform, argClass.Area, argClass.State, argClass.StartTime, argClass.EndTime, argClass.NoticeStrID, argClass.UpdateStrID, argClass.RecomUpdateStrID);
        }

        public async Task<bool> InsertMaintenance(Defines.MaintenancePlatform Platform, string Area, Defines.MaintenanceState State, DateTime StartTime, DateTime EndTime, string NoticeStrID, string UpdateStrID, string RecomUpdateStrID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Maintenance (Platform, Area, State, StartTime, EndTime, NoticeStrID, UpdateStrID, RecomUpdateStrID) values (@01, @02, @03, @04, @05, @06, @07, @08)", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)Platform);
                    cmd.Parameters.AddWithValue("@02", Area);
                    cmd.Parameters.AddWithValue("@03", (Int64)State);
                    cmd.Parameters.AddWithValue("@04", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@05", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@06", NoticeStrID);
                    cmd.Parameters.AddWithValue("@07", UpdateStrID);
                    cmd.Parameters.AddWithValue("@08", RecomUpdateStrID);
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

        public async Task<bool> UpdateMaintenance(Maintenance argClass)
        {
            return await UpdateMaintenance(argClass.Platform, argClass.Area, argClass.State, argClass.StartTime, argClass.EndTime, argClass.NoticeStrID, argClass.UpdateStrID, argClass.RecomUpdateStrID);
        }

        public async Task<bool> UpdateMaintenance(Defines.MaintenancePlatform Platform, string Area, Defines.MaintenanceState State, DateTime StartTime, DateTime EndTime, string NoticeStrID, string UpdateStrID, string RecomUpdateStrID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Maintenance set State = @03, StartTime = @04, EndTime = @05, NoticeStrID = @06, UpdateStrID = @07, RecomUpdateStrID = @08 where Platform = @01 and Area = @02", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)Platform);
                    cmd.Parameters.AddWithValue("@02", Area);
                    cmd.Parameters.AddWithValue("@03", (Int64)State);
                    cmd.Parameters.AddWithValue("@04", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@05", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@06", NoticeStrID);
                    cmd.Parameters.AddWithValue("@07", UpdateStrID);
                    cmd.Parameters.AddWithValue("@08", RecomUpdateStrID);
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

        public async Task<bool> DeleteMaintenance(Maintenance argClass)
        {
            return await DeleteMaintenance(argClass.MaintenanceID);
        }

        public async Task<bool> DeleteMaintenance(Int64 MaintenanceID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Maintenance where MaintenanceID = @01", con);
                    cmd.Parameters.AddWithValue("@01", MaintenanceID);
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

        public async Task<(bool, Int64)> InsertNoticeBanner(NoticeBanner argClass)
        {
            return await InsertNoticeBanner(argClass.StartAt, argClass.EndAt, argClass.ImageURL, argClass.Title, argClass.Message);
        }

        public async Task<(bool, Int64)> InsertNoticeBanner(int StartAt, int EndAt, string ImageURL, string Title, string Message)
        {
            bool queryRes = false;
            Int64 ret = 0;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into NoticeBanner (StartAt, EndAt, ImageURL, Title, Message) values (@01, @02, @03, @04, @05); select last_insert_id();", con);
                    cmd.Parameters.AddWithValue("@01", StartAt);
                    cmd.Parameters.AddWithValue("@02", EndAt);
                    cmd.Parameters.AddWithValue("@03", ImageURL);
                    cmd.Parameters.AddWithValue("@04", Title);
                    cmd.Parameters.AddWithValue("@05", Message);
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

        public async Task<bool> UpdateNoticeBanner(NoticeBanner argClass)
        {
            return await UpdateNoticeBanner(argClass.BannerID, argClass.StartAt, argClass.EndAt, argClass.ImageURL, argClass.Title, argClass.Message);
        }

        public async Task<bool> UpdateNoticeBanner(Int64 BannerID, int StartAt, int EndAt, string ImageURL, string Title, string Message)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update NoticeBanner set StartAt = @02, EndAt = @03, ImageURL = @04, Title = @05, Message = @06 where BannerID = @01", con);
                    cmd.Parameters.AddWithValue("@01", BannerID);
                    cmd.Parameters.AddWithValue("@02", StartAt);
                    cmd.Parameters.AddWithValue("@03", EndAt);
                    cmd.Parameters.AddWithValue("@04", ImageURL);
                    cmd.Parameters.AddWithValue("@05", Title);
                    cmd.Parameters.AddWithValue("@06", Message);
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

        public async Task<bool> DeleteNoticeBanner(NoticeBanner argClass)
        {
            return await DeleteNoticeBanner(argClass.BannerID);
        }

        public async Task<bool> DeleteNoticeBanner(Int64 BannerID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from NoticeBanner where BannerID = @01", con);
                    cmd.Parameters.AddWithValue("@01", BannerID);
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

        public async Task<(bool, Int64)> InsertSlang(Slang argClass)
        {
            return await InsertSlang(argClass.Word);
        }

        public async Task<(bool, Int64)> InsertSlang(string Word)
        {
            bool queryRes = false;
            Int64 ret = 0;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Slang (Word) values (@01); select last_insert_id();", con);
                    cmd.Parameters.AddWithValue("@01", Word);
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

        public async Task<bool> UpdateSlang(Slang argClass)
        {
            return await UpdateSlang(argClass.ID, argClass.Word);
        }

        public async Task<bool> UpdateSlang(Int64 ID, string Word)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Slang set Word = @02 where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    cmd.Parameters.AddWithValue("@02", Word);
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

        public async Task<bool> DeleteSlang(Slang argClass)
        {
            return await DeleteSlang(argClass.ID);
        }

        public async Task<bool> DeleteSlang(Int64 ID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Slang where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
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

        public async Task<(bool, Int64)> InsertWhiteList(WhiteList argClass)
        {
            return await InsertWhiteList(argClass.DeviceID, argClass.MemberNo, argClass.Comment);
        }

        public async Task<(bool, Int64)> InsertWhiteList(string DeviceID, UInt64 MemberNo, string Comment)
        {
            bool queryRes = false;
            Int64 ret = 0;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into WhiteList (DeviceID, MemberNo, Comment) values (@01, @02, @03); select last_insert_id();", con);
                    cmd.Parameters.AddWithValue("@01", DeviceID);
                    cmd.Parameters.AddWithValue("@02", MemberNo);
                    cmd.Parameters.AddWithValue("@03", Comment);
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

        public async Task<bool> UpdateWhiteList(WhiteList argClass)
        {
            return await UpdateWhiteList(argClass.ID, argClass.DeviceID, argClass.MemberNo, argClass.Comment);
        }

        public async Task<bool> UpdateWhiteList(Int64 ID, string DeviceID, UInt64 MemberNo, string Comment)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update WhiteList set DeviceID = @02, MemberNo = @02, Comment = @03, UpdateAt = UTC_TIMESTAMP() where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
                    cmd.Parameters.AddWithValue("@02", DeviceID);
                    cmd.Parameters.AddWithValue("@03", MemberNo);
                    cmd.Parameters.AddWithValue("@04", Comment);
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

        public async Task<bool> DeleteWhiteList(WhiteList argClass)
        {
            return await DeleteWhiteList(argClass.ID);
        }

        public async Task<bool> DeleteWhiteList(Int64 ID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from WhiteList where ID = @01", con);
                    cmd.Parameters.AddWithValue("@01", ID);
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
