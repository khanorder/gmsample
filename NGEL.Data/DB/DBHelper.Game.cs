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
    public class DBHelperGame
    {
        string _conStr = "";

        readonly ILogger<DBHelperGame> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperGame(ILogger<DBHelperGame> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, List<Account>)> SelectAccounts(Int64 UID)
        {
            bool queryRes = false;
            List<Account> ret = new List<Account>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select AccountType, AccountID, MemberNo, WorldID, IsLeave, CreateAt from Account where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Account>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Account();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.AccountType = (Defines.GameAuthType)ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.AccountID = ret1;
                        rdr.ConvertDBData(2, out UInt64 ret2); retVar.MemberNo = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.WorldID = ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsLeave = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.CreateAt = ret5;
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

        public async Task<(bool, Account?)> SelectAccount(UInt64 MemberNo)
        {
            bool queryRes = false;
            Account? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select UID, AccountType, AccountID, WorldID, IsLeave, CreateAt from Account where MemberNo = @01", con);
                    cmd.Parameters.AddWithValue("@01", MemberNo);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Account();
                        retVar.MemberNo = MemberNo;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.AccountType = (Defines.GameAuthType)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.AccountID = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.WorldID = ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsLeave = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.CreateAt = ret5;
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

        public async Task<(bool, List<Achievement>)> SelectAchievements(Int64 UID)
        {
            bool queryRes = false;
            List<Achievement> ret = new List<Achievement>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select AchievementID, Count, CompleteAt, CreateAt, UpdateAt from Achievement where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Achievement>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Achievement();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.AchievementID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.Count = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.CompleteAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.CreateAt = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.UpdateAt = ret4;
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

        public async Task<(bool, Achievement?)> SelectAchievement(Int64 UID, Int64 AchievementID)
        {
            bool queryRes = false;
            Achievement? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select AchievementID, Count, CompleteAt, CreateAt, UpdateAt from Achievement where UID = @01 and AchievementID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AchievementID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Achievement();
                        retVar.UID = UID;
                        retVar.AchievementID = AchievementID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.Count = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.CompleteAt = ret1;
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

        public async Task<(bool, List<Artifact>)> SelectArtifacts(Int64 UID)
        {
            bool queryRes = false;
            List<Artifact> ret = new List<Artifact>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ArtifactID, Enhance, Count, CreateAt, UpdateAt from Artifact where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Artifact>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Artifact();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.ArtifactID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.Enhance = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.Count = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.CreateAt = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.UpdateAt = ret4;
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

        public async Task<(bool, List<ArtifactDeck>)> SelectArtifactDecks(Int64 UID)
        {
            bool queryRes = false;
            List<ArtifactDeck> ret = new List<ArtifactDeck>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.SlotID, a.DeckName, a.ArtifactID0, a.ArtifactID1, a.ArtifactID2, a.ArtifactID3, a.ArtifactID4, a.ArtifactID5, a.ArtifactID6, a.ArtifactID7, a.ArtifactID8, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID0) as Enhance0, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID1) as Enhance1, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID2) as Enhance2, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID3) as Enhance3, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID4) as Enhance4, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID5) as Enhance5, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID6) as Enhance6, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID7) as Enhance7, (SELECT Enhance FROM Artifact WHERE UID = a.UID AND ArtifactID = a.ArtifactID8) as Enhance8, a.CreateAt, a.UpdateAt from ArtifactDeck as a where a.UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<ArtifactDeck>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new ArtifactDeck();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SlotID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.DeckName = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.ArtifactID0 = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.ArtifactID1 = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ArtifactID2 = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.ArtifactID3 = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.ArtifactID4 = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.ArtifactID5 = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.ArtifactID6 = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.ArtifactID7 = ret9;
                        rdr.ConvertDBData(10, out int ret10); retVar.ArtifactID8 = ret10;
                        rdr.ConvertDBData(11, out int? ret11); retVar.Enhance0 = ret11;
                        rdr.ConvertDBData(12, out int? ret12); retVar.Enhance1 = ret12;
                        rdr.ConvertDBData(13, out int? ret13); retVar.Enhance2 = ret13;
                        rdr.ConvertDBData(14, out int? ret14); retVar.Enhance3 = ret14;
                        rdr.ConvertDBData(15, out int? ret15); retVar.Enhance4 = ret15;
                        rdr.ConvertDBData(16, out int? ret16); retVar.Enhance5 = ret16;
                        rdr.ConvertDBData(17, out int? ret17); retVar.Enhance6 = ret17;
                        rdr.ConvertDBData(18, out int? ret18); retVar.Enhance7 = ret18;
                        rdr.ConvertDBData(19, out int? ret19); retVar.Enhance8 = ret19;
                        rdr.ConvertDBData(20, out DateTime ret20); retVar.CreateAt = ret20;
                        rdr.ConvertDBData(21, out DateTime ret21); retVar.UpdateAt = ret21;
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

        public async Task<(bool, List<Asset>)> SelectAssets(Int64 UID)
        {
            bool queryRes = false;
            List<Asset> ret = new List<Asset>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select AssetID, Count, CreateAt, UpdateAt from Asset where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Asset>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Asset();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.AssetID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.Count = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Attendance>)> SelectAttendances(Int64 UID)
        {
            bool queryRes = false;
            List<Attendance> ret = new List<Attendance>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select AttendanceID, AttendanceDay, RewardState, LastAttendanceAt, CreateAt, UpdateAt from Attendance where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Attendance>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Attendance();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.AttendanceID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.AttendanceDay = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.RewardState = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.LastAttendanceAt = ret3;
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

        public async Task<(bool, Attendance?)> SelectAttendance(Int64 UID, int AttendanceID)
        {
            bool queryRes = false;
            Attendance? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select AttendanceDay, RewardState, LastAttendanceAt, CreateAt, UpdateAt from Attendance where UID = @01 and AttendanceID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AttendanceID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Attendance();
                        retVar.UID = UID;
                        retVar.AttendanceID = AttendanceID;
                        rdr.ConvertDBData(0, out int ret0); retVar.AttendanceDay = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.RewardState = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.LastAttendanceAt = ret2;
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

        public async Task<(bool, Attendance?)> SelectAttendanceSP(Int64 UID, int AttendanceID)
        {
            bool queryRes = false;
            Attendance? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("__UID", UID);
                    cmd.Parameters.AddWithValue("__AttendanceID", AttendanceID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Attendance();
                        retVar.UID = UID;
                        retVar.AttendanceID = AttendanceID;
                        rdr.ConvertDBData(0, out int ret0); retVar.AttendanceDay = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.RewardState = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.LastAttendanceAt = ret2;
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

        public async Task<(bool, List<Collection>)> SelectCollectionsAccessory(Int64 UID, ECollectionType CollectionType)
        {
            bool queryRes = false;
            List<Collection> ret = new List<Collection>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CollectionID, IsRewarded, CreateAt, UpdateAt from Collection_Accessory where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionType);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Collection>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Collection();
                        retVar.UID = UID;
                        retVar.CollectionType = CollectionType;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.CollectionID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsRewarded = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Collection>)> SelectCollectionsCostume(Int64 UID, ECollectionType CollectionType)
        {
            bool queryRes = false;
            List<Collection> ret = new List<Collection>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CollectionID, IsRewarded, CreateAt, UpdateAt from Collection_Costume where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionType);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Collection>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Collection();
                        retVar.UID = UID;
                        retVar.CollectionType = CollectionType;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.CollectionID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsRewarded = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Collection>)> SelectCollectionsMonster(Int64 UID, ECollectionType CollectionType)
        {
            bool queryRes = false;
            List<Collection> ret = new List<Collection>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CollectionID, IsRewarded, CreateAt, UpdateAt from Collection_Monster where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionType);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Collection>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Collection();
                        retVar.UID = UID;
                        retVar.CollectionType = CollectionType;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.CollectionID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsRewarded = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Collection>)> SelectCollectionsPet(Int64 UID, ECollectionType CollectionType)
        {
            bool queryRes = false;
            List<Collection> ret = new List<Collection>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CollectionID, IsRewarded, CreateAt, UpdateAt from Collection_Pet where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionType);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Collection>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Collection();
                        retVar.UID = UID;
                        retVar.CollectionType = CollectionType;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.CollectionID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsRewarded = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Collection>)> SelectCollectionsVehicle(Int64 UID, ECollectionType CollectionType)
        {
            bool queryRes = false;
            List<Collection> ret = new List<Collection>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CollectionID, IsRewarded, CreateAt, UpdateAt from Collection_Vehicle where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionType);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Collection>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Collection();
                        retVar.UID = UID;
                        retVar.CollectionType = CollectionType;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.CollectionID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsRewarded = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Collection>)> SelectCollectionsWeapon(Int64 UID, ECollectionType CollectionType)
        {
            bool queryRes = false;
            List<Collection> ret = new List<Collection>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CollectionID, IsRewarded, CreateAt, UpdateAt from Collection_Weapon where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionType);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Collection>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Collection();
                        retVar.UID = UID;
                        retVar.CollectionType = CollectionType;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.CollectionID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsRewarded = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Craft>)> SelectCrafts(Int64 UID)
        {
            bool queryRes = false;
            List<Craft> ret = new List<Craft>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select CraftID, Count, CreateAt, UpdateAt from Craft where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Craft>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Craft();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.CraftID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.Count = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Entitlement>)> SelectEntitlements(Int64 UID)
        {
            bool queryRes = false;
            List<Entitlement> ret = new List<Entitlement>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select EntitlementID, CreateAt, UpdateAt from Entitlement where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Entitlement>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Entitlement();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.EntitlementID = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.CreateAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.UpdateAt = ret2;
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

        public async Task<(bool, List<EventStore>)> SelectEventStores(Int64 UID)
        {
            bool queryRes = false;
            List<EventStore> ret = new List<EventStore>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select StoreID, BuyCount, NextResetAt, ExpireAt, SeasonNum, IsExpire, CreateAt, UpdateAt from EventStore where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<EventStore>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new EventStore();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.StoreID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.BuyCount = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.NextResetAt = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.ExpireAt = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.SeasonNum = ret4;
                        rdr.ConvertDBData(5, out bool ret5); retVar.IsExpire = ret5;
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

        public async Task<(bool, List<Expression>)> SelectExpressions(Int64 UID)
        {
            bool queryRes = false;
            List<Expression> ret = new List<Expression>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ExpressionType, ExpressionID, CreateAt, UpdateAt from Expression where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Expression>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Expression();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.ExpressionType = (EExpressionType)ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.ExpressionID = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<ExpressionPreset>)> SelectExpressionPresets(Int64 UID)
        {
            bool queryRes = false;
            List<ExpressionPreset> ret = new List<ExpressionPreset>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select HeroID, Preset, CreateAt, UpdateAt from ExpressionPreset where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<ExpressionPreset>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new ExpressionPreset();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.HeroID = ret0;
                        rdr.ConvertDBData(1, out List<Int64> ret1); retVar.Preset = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Friend>)> SelectFriends(Int64 UID)
        {
            bool queryRes = false;
            List<Friend> ret = new List<Friend>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select b.UID, a.IsDeleted, c.MemberNo, b.Nick from Friend as a left join UserAccount as b on a.FriendUID = b.UID left join Account as c on a.FriendUID = c.UID where a.UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Friend>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Friend();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.FriendUID = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsDeleted = ret1;
                        rdr.ConvertDBData(2, out UInt64 ret2); retVar.FriendMemberNo = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.FriendNick = ret3;
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

        public async Task<(bool, List<GlitchStore>)> SelectGlitchStores(Int64 UID)
        {
            bool queryRes = false;
            List<GlitchStore> ret = new List<GlitchStore>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select StoreID, CreateAt, UpdateAt from GlitchStore where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GlitchStore>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GlitchStore();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.StoreID = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.CreateAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.UpdateAt = ret2;
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

        public async Task<(bool, List<SilverMedalStore>)> SelectSilverMedalStores(Int64 UID)
        {
            bool queryRes = false;
            List<SilverMedalStore> ret = new List<SilverMedalStore>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select StoreID, BuyCount, NextResetAt, SeasonNum, CreateAt, UpdateAt from SilverMedalStore where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<SilverMedalStore>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new SilverMedalStore();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.StoreID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.BuyCount = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.NextResetAt = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.SeasonNum = ret3;
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

        public async Task<(bool, List<GuideMission>)> SelectGuideMissions(Int64 UID)
        {
            bool queryRes = false;
            List<GuideMission> ret = new List<GuideMission>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select GuideMissionCategory, MissionID, IsCompleted, IsRewarded, CreateAt, UpdateAt from GuideMission where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<GuideMission>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new GuideMission();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.GuideMissionCategory = (EGuideMissionCategory)ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.MissionID = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.IsCompleted = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.IsRewarded = ret3;
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

        public async Task<(bool, GuideMissionProgressReward?)> SelectGuideMissionProgressReward(Int64 UID)
        {
            bool queryRes = false;
            GuideMissionProgressReward? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select LastReceiveID, CreateAt, UpdateAt from GuideMissionProgressReward where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new GuideMissionProgressReward();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.LastReceiveID = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.CreateAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.UpdateAt = ret2;
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

        public async Task<(bool, List<Hero>)> SelectHeroes(Int64 UID)
        {
            bool queryRes = false;
            List<Hero> ret = new List<Hero>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select HeroID, BattleLevel, BattleExp, RewardedLevel, LeftEyeHexColor, RightEyeHexColor, HairSkinID, BodySkinID, HeadID, HeadOffset, HeadRotate, FaceID, FaceOffset, FaceRotate, BackID, BackOffset, BackRotate, PelvisID, PelvisOffset, PelvisRotate, WeaponID, WinPoseID, ExpireAt, AddPresetCount, CreateAt, UpdateAt from Hero where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Hero>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Hero();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.HeroID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.BattleLevel = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.BattleExp = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.RewardedLevel = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.LeftEyeHexColor = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.RightEyeHexColor = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.HairSkinID = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.BodySkinID = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.HeadID = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.HeadOffset = ret9;
                        rdr.ConvertDBData(10, out string ret10); retVar.HeadRotate = ret10;
                        rdr.ConvertDBData(11, out Int64 ret11); retVar.FaceID = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.FaceOffset = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.FaceRotate = ret13;
                        rdr.ConvertDBData(14, out Int64 ret14); retVar.BackID = ret14;
                        rdr.ConvertDBData(15, out string ret15); retVar.BackOffset = ret15;
                        rdr.ConvertDBData(16, out string ret16); retVar.BackRotate = ret16;
                        rdr.ConvertDBData(17, out Int64 ret17); retVar.PelvisID = ret17;
                        rdr.ConvertDBData(18, out string ret18); retVar.PelvisOffset = ret18;
                        rdr.ConvertDBData(19, out string ret19); retVar.PelvisRotate = ret19;
                        rdr.ConvertDBData(20, out Int64 ret20); retVar.WeaponID = ret20;
                        rdr.ConvertDBData(21, out Int64 ret21); retVar.WinPoseID = ret21;
                        rdr.ConvertDBData(22, out Int64 ret22); retVar.ExpireAt = ret22;
                        rdr.ConvertDBData(23, out int ret23); retVar.AddPresetCount = ret23;
                        rdr.ConvertDBData(24, out DateTime ret24); retVar.CreateAt = ret24;
                        rdr.ConvertDBData(25, out DateTime ret25); retVar.UpdateAt = ret25;
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

        public async Task<(bool, List<HeroSkin>)> SelectHeroSkins(Int64 UID)
        {
            bool queryRes = false;
            List<HeroSkin> ret = new List<HeroSkin>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SkinID, HexColor1, HexColor2, HexColor3, HexColor4, CreateAt, UpdateAt from HeroSkin where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<HeroSkin>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new HeroSkin();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SkinID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.HexColor1 = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.HexColor2 = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.HexColor3 = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.HexColor4 = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.CreateAt = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.UpdateAt = ret6;
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

        public async Task<(bool, List<HeroSkinPreset>)> SelectHeroSkinPresets(Int64 UID)
        {
            bool queryRes = false;
            List<HeroSkinPreset> ret = new List<HeroSkinPreset>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select HeroID, SlotID, PresetData, CreateAt, UpdateAt from HeroSkinPreset where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<HeroSkinPreset>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new HeroSkinPreset();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.HeroID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.SlotID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.PresetData = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.CreateAt = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.UpdateAt = ret4;
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

        public async Task<(bool, List<Inventory>)> SelectInventories(Int64 UID)
        {
            bool queryRes = false;
            List<Inventory> ret = new List<Inventory>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ItemID, Count, CreateAt, UpdateAt from Inventory where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Inventory>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Inventory();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ItemID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.Count = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Inventory>)> SelectInventoriesExists(Int64 UID)
        {
            bool queryRes = false;
            List<Inventory> ret = new List<Inventory>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ItemID, Count, CreateAt, UpdateAt from Inventory where UID = @01 and `Count` > 0", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Inventory>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Inventory();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ItemID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.Count = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<Mail>)> SelectMails(Int64 UID)
        {
            bool queryRes = false;
            List<Mail> ret = new List<Mail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MailID, MailType, State, IsBM, Title, Message, RewardList, ExpireAt, ReceiveAt, CreateAt, UpdateAt from Mail where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Mail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Mail();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.MailID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.MailType = (EMailType)ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.State = (EMailStateType)ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.IsBM = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Title = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.Message = ret5;
                        rdr.ConvertDBData(6, out List<MailReward> ret6); retVar.RewardList = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.ExpireAt = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.ReceiveAt = ret8;
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

        public async Task<(bool, List<Mail>)> SelectMails(string mailIdList)
        {
            bool queryRes = false;
            List<Mail> ret = new List<Mail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MailID, UID, MailType, State, IsBM, Title, Message, RewardList, ExpireAt, ReceiveAt, CreateAt, UpdateAt from Mail where MailID in ({mailIdList})", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Mail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Mail();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.MailID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.UID = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.MailType = (EMailType)ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.State = (EMailStateType)ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsBM = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.Title = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.Message = ret6;
                        rdr.ConvertDBData(7, out List<MailReward> ret7); retVar.RewardList = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.ExpireAt = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.ReceiveAt = ret9;
                        rdr.ConvertDBData(10, out DateTime ret10); retVar.CreateAt = ret10;
                        rdr.ConvertDBData(11, out DateTime ret11); retVar.UpdateAt = ret11;
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

        public async Task<(bool, Mail?)> SelectMail(Int64 MailID)
        {
            bool queryRes = false;
            Mail? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select UID, MailType, State, IsBM, Title, Message, RewardList, ExpireAt, ReceiveAt, CreateAt, UpdateAt from Mail where MailID = @01", con);
                    cmd.Parameters.AddWithValue("@01", MailID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Mail();
                        retVar.MailID = MailID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.MailType = (EMailType)ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.State = (EMailStateType)ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.IsBM = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.Title = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.Message = ret5;
                        rdr.ConvertDBData(6, out List<MailReward> ret6); retVar.RewardList = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.ExpireAt = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.ReceiveAt = ret8;
                        rdr.ConvertDBData(9, out DateTime ret9); retVar.CreateAt = ret9;
                        rdr.ConvertDBData(10, out DateTime ret10); retVar.UpdateAt = ret10;
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

        public async Task<(bool, List<MailWithUser>)> SelectMailsBuyPeriod(DateTime startTime, DateTime endTime)
        {
            bool queryRes = false;
            List<MailWithUser> ret = new List<MailWithUser>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `c`.`MemberNo`, `a`.`UID`, `b`.`Nick`, `a`.`MailID`, `a`.`MailType`, `a`.`State`, `a`.`IsBM`, `a`.`Title`, `a`.`Message`, `a`.`RewardList`, `a`.`ExpireAt`, `a`.`ReceiveAt`, `a`.`CreateAt`, `a`.`UpdateAt` from `Mail` as `a` left join `UserAccount` as `b` on `a`.`UID` = `b`.`UID` left join `Account` as `c` on `a`.`UID` = `c`.`UID` where `a`.`CreateAt` >= @01 and `a`.`CreateAt` <= @02", con);
                    cmd.Parameters.AddWithValue("@01", (startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<MailWithUser>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new MailWithUser();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.MemberNo = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.UID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Nick = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.MailID = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.MailType = (EMailType)ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.State = (EMailStateType)ret5;
                        rdr.ConvertDBData(6, out bool ret6); retVar.IsBM = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.Title = ret7;
                        rdr.ConvertDBData(8, out string ret8); retVar.Message = ret8;
                        rdr.ConvertDBData(9, out List<MailReward> ret9); retVar.RewardList = ret9;
                        rdr.ConvertDBData(10, out int ret10); retVar.ExpireAt = ret10;
                        rdr.ConvertDBData(11, out int ret11); retVar.ReceiveAt = ret11;
                        rdr.ConvertDBData(12, out DateTime ret12); retVar.CreateAt = ret12;
                        rdr.ConvertDBData(13, out DateTime ret13); retVar.UpdateAt = ret13;
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

        public async Task<(bool, List<MazeRewardBox>)> SelectMazeRewardBoxes(Int64 UID)
        {
            bool queryRes = false;
            List<MazeRewardBox> ret = new List<MazeRewardBox>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SlotID, BoxID, ExpiredAt, IsOpened, CreateAt, UpdateAt from MazeRewardBox where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<MazeRewardBox>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new MazeRewardBox();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SlotID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.BoxID = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.ExpiredAt = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.IsOpened = ret3;
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

        public async Task<(bool, List<NicePlayer>)> SelectNicePlayers()
        {
            bool queryRes = false;
            List<NicePlayer> ret = new List<NicePlayer>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select NiceLevel, NicePoint, CreateAt, UpdateAt from NicePlayer", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NicePlayer>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NicePlayer();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.NiceLevel = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.NicePoint = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.CreateAt = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.UpdateAt = ret4;
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

        public async Task<(bool, List<NicePlayer>)> SelectNicePlayers(Int64 UID)
        {
            bool queryRes = false;
            List<NicePlayer> ret = new List<NicePlayer>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select NiceLevel, NicePoint, CreateAt, UpdateAt from NicePlayer where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NicePlayer>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NicePlayer();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.NiceLevel = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.NicePoint = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<PenaltyWithUser>)> SelectPenalties(DateTime StartTime, DateTime EndTime)
        {
            bool queryRes = false;
            List<PenaltyWithUser> ret = new List<PenaltyWithUser>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.UID, b.MemberNo, c.Nick, a.ReportState, a.IsActive, a.PenaltyGrade, a.PenaltyPoint, a.PenaltyCount, a.PenaltyEndAt, a.ClearPenaltyAt, a.CreateAt, a.UpdateAt from Penalty as a left join Account as b on a.UID = b.UID left join UserAccount as c on a.UID = c.UID where a.CreateAt between @01 and @02", con);
                    cmd.Parameters.AddWithValue("@01", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<PenaltyWithUser>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new PenaltyWithUser();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out UInt64 ret1); retVar.MemberNo = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Nick = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.ReportState = (EPenaltyReportState)ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsActive = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.PenaltyGrade = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.PenaltyPoint = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.PenaltyCount = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.PenaltyEndAt = ret8;
                        rdr.ConvertDBData(9, out Int64 ret9); retVar.ClearPenaltyAt = ret9;
                        rdr.ConvertDBData(10, out DateTime ret10); retVar.CreateAt = ret10;
                        rdr.ConvertDBData(11, out DateTime ret11); retVar.UpdateAt = ret11;
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

        public async Task<(bool, PenaltyWithUser?)> SelectPenalty(Int64 UID, EPenaltyReportState ReportState)
        {
            bool queryRes = false;
            PenaltyWithUser? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.UID, b.MemberNo, c.Nick, a.ReportState, a.IsActive, a.PenaltyGrade, a.PenaltyPoint, a.PenaltyCount, a.PenaltyEndAt, a.ClearPenaltyAt, a.CreateAt, a.UpdateAt from Penalty as a left join Account as b on a.UID = b.UID left join UserAccount as c on a.UID = c.UID where a.UID = @01 and a.ReportState = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ReportState);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new PenaltyWithUser();
                        retVar.UID = UID;
                        retVar.ReportState = ReportState;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out UInt64 ret1); retVar.MemberNo = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Nick = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.ReportState = (EPenaltyReportState)ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsActive = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.PenaltyGrade = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.PenaltyPoint = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.PenaltyCount = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.PenaltyEndAt = ret8;
                        rdr.ConvertDBData(9, out Int64 ret9); retVar.ClearPenaltyAt = ret9;
                        rdr.ConvertDBData(10, out DateTime ret10); retVar.CreateAt = ret10;
                        rdr.ConvertDBData(11, out DateTime ret11); retVar.UpdateAt = ret11;
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

        public async Task<(bool, List<Penalty>)> SelectPenalties(Int64 UID)
        {
            bool queryRes = false;
            List<Penalty> ret = new List<Penalty>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ReportState, IsActive, PenaltyGrade, PenaltyPoint, PenaltyCount, PenaltyEndAt, ClearPenaltyAt, CreateAt, UpdateAt from Penalty where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Penalty>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Penalty();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.ReportState = (EPenaltyReportState)ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsActive = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.PenaltyGrade = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.PenaltyPoint = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.PenaltyCount = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.PenaltyEndAt = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.ClearPenaltyAt = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.CreateAt = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.UpdateAt = ret8;
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

        public async Task<(bool, List<Pet>)> SelectPets(Int64 UID)
        {
            bool queryRes = false;
            List<Pet> ret = new List<Pet>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select UniqueID, PetID, Ability, `Like`, IsLocked, IsDeleted, CreateAt, UpdateAt from Pet where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Pet>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Pet();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UniqueID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.PetID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Ability = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.Like = ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsLocked = ret4;
                        rdr.ConvertDBData(5, out bool ret5); retVar.IsDeleted = ret5;
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

        public async Task<(bool, List<PlayRecordGoldClash>)> SelectPlayRecordGoldClashes(Int64 UID)
        {
            bool queryRes = false;
            List<PlayRecordGoldClash> ret = new List<PlayRecordGoldClash>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `SeasonID`, `HeroID`, `Win`, `Lose`, `Kill`, `Death`, `Mvp`, `AvgGold`, `AvgDamage`, `AvgHeal`, `AvgDamageBlock`, `CreateAt`, `UpdateAt` from `PlayRecord_GoldClash` where `UID` = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<PlayRecordGoldClash>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new PlayRecordGoldClash();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.SeasonID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.HeroID = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.Win = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.Lose = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.Kill = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.Death = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.Mvp = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.AvgGold = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.AvgDamage = ret8;
                        rdr.ConvertDBData(9, out Int64 ret9); retVar.AvgHeal = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.AvgDamageBlock = ret10;
                        rdr.ConvertDBData(11, out DateTime ret11); retVar.CreateAt = ret11;
                        rdr.ConvertDBData(12, out DateTime ret12); retVar.UpdateAt = ret12;
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

        public async Task<(bool, List<PlayRecordRpg>)> SelectPlayRecordRpgs(Int64 UID)
        {
            bool queryRes = false;
            List<PlayRecordRpg> ret = new List<PlayRecordRpg>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ChapterID, HeroID, BossClear, TopPoint, ShortestPlayTime, AvgFragment, AvgArtifact, AvgLevel, CreateAt, UpdateAt from PlayRecord_Rpg where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<PlayRecordRpg>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new PlayRecordRpg();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ChapterID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.HeroID = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.BossClear = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.TopPoint = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.ShortestPlayTime = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.AvgFragment = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.AvgArtifact = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.AvgLevel = ret7;
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

        public async Task<(bool, List<Profile>)> SelectProfiles(Int64 UID)
        {
            bool queryRes = false;
            List<Profile> ret = new List<Profile>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ProfileType, ProfileID, CreateAt, UpdateAt from Profile where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Profile>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Profile();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.ProfileType = (EProfileType)ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.ProfileID = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<RankingReward>)> SelectRankingRewards(Int64 UID)
        {
            bool queryRes = false;
            List<RankingReward> ret = new List<RankingReward>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ContentType, RewardedAt, CreateAt, UpdateAt from RankingReward where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<RankingReward>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new RankingReward();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.ContentType = (EContentsType)ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.RewardedAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<RpgAttribute>)> SelectRpgAttributes(Int64 UID)
        {
            bool queryRes = false;
            List<RpgAttribute> ret = new List<RpgAttribute>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, Level, CreateAt, UpdateAt from RpgAttribute where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<RpgAttribute>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new RpgAttribute();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.Level = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<SeasonMission>)> SelectSeasonMissions(Int64 UID)
        {
            bool queryRes = false;
            List<SeasonMission> ret = new List<SeasonMission>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SeasonPassID, MissionGroup, MissionID, Count, IsComplete, ResetAt, CreateAt, UpdateAt from SeasonMission where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<SeasonMission>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new SeasonMission();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SeasonPassID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.MissionGroup = (EMissionGroup)ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.MissionID = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.Count = ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsComplete = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.ResetAt = ret5;
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

        public async Task<(bool, List<SeasonPass>)> SelectSeasonPasses(Int64 UID)
        {
            bool queryRes = false;
            List<SeasonPass> ret = new List<SeasonPass>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SeasonPassID, SeasonNum, IsPaid, Level, Exp, RewardState, CreateAt, UpdateAt from SeasonPass where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<SeasonPass>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new SeasonPass();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SeasonPassID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.SeasonNum = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.IsPaid = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.Level = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.Exp = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.RewardState = ret5;
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

        public async Task<(bool, SeasonPass?)> SelectSeasonPass(SeasonPass argClass)
        {
            return await SelectSeasonPass(argClass.UID, argClass.SeasonPassID);
        }

        public async Task<(bool, SeasonPass?)> SelectSeasonPass(Int64 UID, int SeasonPassID)
        {
            bool queryRes = false;
            SeasonPass? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SeasonNum, IsPaid, Level, Exp, RewardState, CreateAt, UpdateAt from SeasonPass where UID = @01 and SeasonPassID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", SeasonPassID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new SeasonPass();
                        retVar.UID = UID;
                        retVar.SeasonPassID = SeasonPassID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SeasonNum = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.IsPaid = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.Level = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.Exp = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.RewardState = ret4;
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

        public async Task<(bool, List<SpecialLevel>)> SelectSpecialLevels(Int64 UID)
        {
            bool queryRes = false;
            List<SpecialLevel> ret = new List<SpecialLevel>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select LevelID, CompletedAt, BoxID0, BoxID1, BoxID2, BoxID3, BoxID4, CreateAt, UpdateAt from SpecialLevel where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<SpecialLevel>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new SpecialLevel();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.LevelID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.CompletedAt = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.BoxID0 = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.BoxID1 = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.BoxID2 = ret4;
                        rdr.ConvertDBData(5, out Int64 ret5); retVar.BoxID3 = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.BoxID4 = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.CreateAt = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.UpdateAt = ret8;
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

        public async Task<(bool, List<TreasureBox>)> SelectTreasureBoxes(Int64 UID)
        {
            bool queryRes = false;
            List<TreasureBox> ret = new List<TreasureBox>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select BoxList, ExpireAt, CreateAt, UpdateAt from TreasureBox where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<TreasureBox>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new TreasureBox();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out List<UserTreasureBox> ret0); retVar.BoxList = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.ExpireAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, UserAccount?)> SelectUserAccount(string Nick)
        {
            bool queryRes = false;
            UserAccount? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.UID, a.WUID, a.HeroID, a.ProfileIconID, a.ProfileBGID, a.EntitlementID, a.PetUniqueID, b.PetID, a.VehicleID, a.LastClearChapterID, a.IsGlitchTutorialComplete, a.IsGoldClashTutorialComplete, a.LastPosition, a.AddArtifactDeckCount, a.PenaltyReportCount, a.LastPenaltyReportAt, a.IntroduceID, a.LastLoginAt, a.CreateAt, a.UpdateAt from UserAccount as a left join Pet as b on a.UID = b.UID and a.PetUniqueID = b.UniqueID where a.Nick = @01", con);
                    cmd.Parameters.AddWithValue("@01", Nick);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserAccount();
                        retVar.Nick = Nick;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.WUID = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.HeroID = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.ProfileIconID = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ProfileBGID = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.EntitlementID = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.PetUniqueID = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.PetID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.VehicleID = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.LastClearChapterID = ret9;
                        rdr.ConvertDBData(10, out bool ret10); retVar.IsGlitchTutorialComplete = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.IsGoldClashTutorialComplete = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.LastPosition = ret12;
                        rdr.ConvertDBData(13, out int ret13); retVar.AddArtifactDeckCount = ret13;
                        rdr.ConvertDBData(14, out int ret14); retVar.PenaltyReportCount = ret14;
                        rdr.ConvertDBData(15, out Int64 ret15); retVar.LastPenaltyReportAt = ret15;
                        rdr.ConvertDBData(16, out Int64 ret16); retVar.IntroduceID = ret16;
                        rdr.ConvertDBData(17, out Int64 ret17); retVar.LastLoginAt = ret17;
                        rdr.ConvertDBData(18, out DateTime ret18); retVar.CreateAt = ret18;
                        rdr.ConvertDBData(19, out DateTime ret19); retVar.UpdateAt = ret19;
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

        public async Task<(bool, UserAccount?)> SelectUserAccount(Int64 UID)
        {
            bool queryRes = false;
            UserAccount? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.WUID, a.Nick, a.HeroID, a.ProfileIconID, a.ProfileBGID, a.EntitlementID, a.PetUniqueID, b.PetID, a.VehicleID, a.LastClearChapterID, a.IsGlitchTutorialComplete, a.IsGoldClashTutorialComplete, a.LastPosition, a.AddArtifactDeckCount, a.PenaltyReportCount, a.LastPenaltyReportAt, a.IntroduceID, a.LastLoginAt, a.CreateAt, a.UpdateAt from UserAccount as a left join Pet as b on a.UID = b.UID and a.PetUniqueID = b.UniqueID where a.UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserAccount();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out string ret0); retVar.WUID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Nick = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.HeroID = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.ProfileIconID = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ProfileBGID = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.EntitlementID = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.PetUniqueID = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.PetID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.VehicleID = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.LastClearChapterID = ret9;
                        rdr.ConvertDBData(10, out bool ret10); retVar.IsGlitchTutorialComplete = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.IsGoldClashTutorialComplete = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.LastPosition = ret12;
                        rdr.ConvertDBData(13, out int ret13); retVar.AddArtifactDeckCount = ret13;
                        rdr.ConvertDBData(14, out int ret14); retVar.PenaltyReportCount = ret14;
                        rdr.ConvertDBData(15, out Int64 ret15); retVar.LastPenaltyReportAt = ret15;
                        rdr.ConvertDBData(16, out Int64 ret16); retVar.IntroduceID = ret16;
                        rdr.ConvertDBData(17, out Int64 ret17); retVar.LastLoginAt = ret17;
                        rdr.ConvertDBData(18, out DateTime ret18); retVar.CreateAt = ret18;
                        rdr.ConvertDBData(19, out DateTime ret19); retVar.UpdateAt = ret19;
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

        public async Task<(bool, UserAccount?)> SelectUserAccountByMemberNo(UInt64 MemberNo)
        {
            bool queryRes = false;
            UserAccount? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.UID, a.Nick, a.HeroID, a.ProfileIconID, a.ProfileBGID, a.EntitlementID, a.PetUniqueID, b.PetID, a.VehicleID, a.LastClearChapterID, a.IsGlitchTutorialComplete, a.IsGoldClashTutorialComplete, a.LastPosition, a.AddArtifactDeckCount, a.PenaltyReportCount, a.LastPenaltyReportAt, a.IntroduceID, a.LastLoginAt, a.CreateAt, a.UpdateAt from UserAccount as a left join Pet as b on a.UID = b.UID and a.PetUniqueID = b.UniqueID left join Account as c on a.UID = c.UID where c.MemberNo = @01", con);
                    cmd.Parameters.AddWithValue("@01", MemberNo);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserAccount();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.Nick = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.HeroID = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.ProfileIconID = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ProfileBGID = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.EntitlementID = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.PetUniqueID = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.PetID = ret7;
                        rdr.ConvertDBData(8, out int ret8); retVar.VehicleID = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.LastClearChapterID = ret9;
                        rdr.ConvertDBData(10, out bool ret10); retVar.IsGlitchTutorialComplete = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.IsGoldClashTutorialComplete = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.LastPosition = ret12;
                        rdr.ConvertDBData(13, out int ret13); retVar.AddArtifactDeckCount = ret13;
                        rdr.ConvertDBData(14, out int ret14); retVar.PenaltyReportCount = ret14;
                        rdr.ConvertDBData(15, out Int64 ret15); retVar.LastPenaltyReportAt = ret15;
                        rdr.ConvertDBData(16, out Int64 ret16); retVar.IntroduceID = ret16;
                        rdr.ConvertDBData(17, out Int64 ret17); retVar.LastLoginAt = ret17;
                        rdr.ConvertDBData(18, out DateTime ret18); retVar.CreateAt = ret18;
                        rdr.ConvertDBData(19, out DateTime ret19); retVar.UpdateAt = ret19;
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

        public async Task<(bool, List<UserAccount>)> SelectUserAccounts(string UIDList)
        {
            bool queryRes = false;
            List<UserAccount> ret = new List<UserAccount>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.UID, a.WUID, a.Nick, a.HeroID, a.ProfileIconID, a.ProfileBGID, a.EntitlementID, a.PetUniqueID, b.PetID, a.VehicleID, a.LastClearChapterID, a.IsGlitchTutorialComplete, a.IsGoldClashTutorialComplete, a.LastPosition, a.AddArtifactDeckCount, a.PenaltyReportCount, a.LastPenaltyReportAt, a.IntroduceID, a.LastLoginAt, a.CreateAt, a.UpdateAt from UserAccount as a left join Pet as b on a.UID = b.UID and a.PetUniqueID = b.UniqueID where a.UID in ({UIDList})", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserAccount>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserAccount();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.WUID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Nick = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.HeroID = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ProfileIconID = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.ProfileBGID = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.EntitlementID = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.PetUniqueID = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.PetID = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.VehicleID = ret9;
                        rdr.ConvertDBData(10, out int ret10); retVar.LastClearChapterID = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.IsGlitchTutorialComplete = ret11;
                        rdr.ConvertDBData(12, out bool ret12); retVar.IsGoldClashTutorialComplete = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.LastPosition = ret13;
                        rdr.ConvertDBData(14, out int ret14); retVar.AddArtifactDeckCount = ret14;
                        rdr.ConvertDBData(15, out int ret15); retVar.PenaltyReportCount = ret15;
                        rdr.ConvertDBData(16, out Int64 ret16); retVar.LastPenaltyReportAt = ret16;
                        rdr.ConvertDBData(17, out Int64 ret17); retVar.IntroduceID = ret17;
                        rdr.ConvertDBData(18, out Int64 ret18); retVar.LastLoginAt = ret18;
                        rdr.ConvertDBData(19, out DateTime ret19); retVar.CreateAt = ret19;
                        rdr.ConvertDBData(20, out DateTime ret20); retVar.UpdateAt = ret20;
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

        public async Task<(bool, List<UserAccount>)> SelectUserAccountsByMemberNo(string MemberNoList)
        {
            bool queryRes = false;
            List<UserAccount> ret = new List<UserAccount>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.UID, a.WUID, a.Nick, a.HeroID, a.ProfileIconID, a.ProfileBGID, a.EntitlementID, a.PetUniqueID, b.PetID, a.VehicleID, a.LastClearChapterID, a.IsGlitchTutorialComplete, a.IsGoldClashTutorialComplete, a.LastPosition, a.AddArtifactDeckCount, a.PenaltyReportCount, a.LastPenaltyReportAt, a.IntroduceID, a.LastLoginAt, a.CreateAt, a.UpdateAt from UserAccount as a left join Pet as b on a.UID = b.UID and a.PetUniqueID = b.UniqueID left join Account as c on a.UID = c.UID where c.MemberNo in ({MemberNoList})", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserAccount>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserAccount();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.WUID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.Nick = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.HeroID = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.ProfileIconID = ret4;
                        rdr.ConvertDBData(5, out int ret5); retVar.ProfileBGID = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.EntitlementID = ret6;
                        rdr.ConvertDBData(7, out int ret7); retVar.PetUniqueID = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.PetID = ret8;
                        rdr.ConvertDBData(9, out int ret9); retVar.VehicleID = ret9;
                        rdr.ConvertDBData(10, out int ret10); retVar.LastClearChapterID = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.IsGlitchTutorialComplete = ret11;
                        rdr.ConvertDBData(12, out bool ret12); retVar.IsGoldClashTutorialComplete = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.LastPosition = ret13;
                        rdr.ConvertDBData(14, out int ret14); retVar.AddArtifactDeckCount = ret14;
                        rdr.ConvertDBData(15, out int ret15); retVar.PenaltyReportCount = ret15;
                        rdr.ConvertDBData(16, out Int64 ret16); retVar.LastPenaltyReportAt = ret16;
                        rdr.ConvertDBData(17, out Int64 ret17); retVar.IntroduceID = ret17;
                        rdr.ConvertDBData(18, out Int64 ret18); retVar.LastLoginAt = ret18;
                        rdr.ConvertDBData(19, out DateTime ret19); retVar.CreateAt = ret19;
                        rdr.ConvertDBData(20, out DateTime ret20); retVar.UpdateAt = ret20;
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

        public async Task<(bool, List<UserBlock>)> SelectUserBlocks(Int64 UID)
        {
            bool queryRes = false;
            List<UserBlock> ret = new List<UserBlock>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select MemberNo, BlockReasonID, BlockReasonStr, StartTime, EndTime from UserBlock where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserBlock>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserBlock();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.MemberNo = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.BlockReasonID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.BlockReasonStr = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.StartTime = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.EndTime = ret4;
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

        public async Task<(bool, UserBlock?)> SelectUserBlock(UserBlock argClass)
        {
            return await SelectUserBlock(argClass.MemberNo);
        }

        public async Task<(bool, UserBlock?)> SelectUserBlock(UInt64 MemberNo)
        {
            bool queryRes = false;
            UserBlock? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select UID, BlockReasonID, BlockReasonStr, StartTime, EndTime from UserBlock where MemberNo = @01", con);
                    cmd.Parameters.AddWithValue("@01", MemberNo);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserBlock();
                        retVar.MemberNo = MemberNo;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.BlockReasonID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.BlockReasonStr = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.StartTime = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.EndTime = ret4;
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

        public async Task<(bool, List<UserDevice>)> SelectUserDevices(UserDevice argClass)
        {
            return await SelectUserDevices(argClass.UID);
        }

        public async Task<(bool, List<UserDevice>)> SelectUserDevices(Int64 UID)
        {
            bool queryRes = false;
            List<UserDevice> ret = new List<UserDevice>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select DeviceID, DeviceType, DeviceModel, OS, MarketCode, Provider, Platform, CreateAt, UpdateAt from UserDevice where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserDevice>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserDevice();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out string ret0); retVar.DeviceID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.DeviceType = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.DeviceModel = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.OS = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.MarketCode = (EMarketCode)ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.Provider = ret5;
                        rdr.ConvertDBData(6, out int ret6); retVar.Platform = (EPlatform)ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.CreateAt = ret7;
                        rdr.ConvertDBData(8, out DateTime ret8); retVar.UpdateAt = ret8;
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

        public async Task<(bool, List<WeaponCategory>)> SelectWeaponCategories(Int64 UID)
        {
            bool queryRes = false;
            List<WeaponCategory> ret = new List<WeaponCategory>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select WeaponCategoryID, Exp, CreateAt, UpdateAt from WeaponCategory where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<WeaponCategory>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new WeaponCategory();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.WeaponCategoryID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.Exp = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, WonderCube?)> SelectWonderCube(Int64 UID, int SlotID)
        {
            bool queryRes = false;
            WonderCube? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select WonderCubeID, CreateAt, UpdateAt from WonderCube where UID = @01 and SlotID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", SlotID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new WonderCube();
                        retVar.UID = UID;
                        retVar.SlotID = SlotID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.WonderCubeID = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.CreateAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.UpdateAt = ret2;
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

        public async Task<(bool, List<WonderCube>)> SelectWonderCubes(Int64 UID)
        {
            bool queryRes = false;
            List<WonderCube> ret = new List<WonderCube>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select SlotID, WonderCubeID, CreateAt, UpdateAt from WonderCube where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<WonderCube>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new WonderCube();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.SlotID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.WonderCubeID = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.UpdateAt = ret3;
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

        public async Task<(bool, List<WonderStore>)> SelectWonderStores(Int64 UID)
        {
            bool queryRes = false;
            List<WonderStore> ret = new List<WonderStore>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select StoreID, TID, StoveProductID, BuyCount, SeasonPassID, IsSubscription, SubscriptionExpireAt, BuyAbleStartAt, BuyAbleEndAt, NextResetAt, IsDeleted, CreateAt, UpdateAt from WonderStore where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<WonderStore>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new WonderStore();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out int ret0); retVar.StoreID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.TID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.StoveProductID = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.BuyCount = ret3;
                        rdr.ConvertDBData(4, out Int64 ret4); retVar.SeasonPassID = ret4;
                        rdr.ConvertDBData(5, out bool ret5); retVar.IsSubscription = ret5;
                        rdr.ConvertDBData(6, out Int64 ret6); retVar.SubscriptionExpireAt = ret6;
                        rdr.ConvertDBData(7, out Int64 ret7); retVar.BuyAbleStartAt = ret7;
                        rdr.ConvertDBData(8, out Int64 ret8); retVar.BuyAbleEndAt = ret8;
                        rdr.ConvertDBData(9, out Int64 ret9); retVar.NextResetAt = ret9;
                        rdr.ConvertDBData(10, out bool ret10); retVar.IsDeleted = ret10;
                        rdr.ConvertDBData(11, out DateTime ret11); retVar.CreateAt = ret11;
                        rdr.ConvertDBData(12, out DateTime ret12); retVar.UpdateAt = ret12;
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

        public async Task<(bool, List<Billing>)> SelectBillings(DateTime startTime, DateTime endTime)
        {
            bool queryRes = false;
            List<Billing> ret = new List<Billing>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select UID, TID, OriginalTID, MemberNo, NickNameNo, NotiType, TxnTime, MarketCode, ProductID, SubStatusCode, ExpireTime, Price, IsRewarded, BillingData, CreateAt, UpdateAt from Billing where CreateAt >= @01 and CreateAt <= @02", con);
                    cmd.Parameters.AddWithValue("@01", (startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Billing>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Billing();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.UID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.TID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.OriginalTID = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.MemberNo = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.NickNameNo = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.NotiType = ret5;
                        rdr.ConvertDBData(6, out UInt64 ret6); retVar.TxnTime = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.MarketCode = ret7;
                        rdr.ConvertDBData(8, out string ret8); retVar.ProductID = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.SubStatusCode = ret9;
                        rdr.ConvertDBData(10, out UInt64 ret10); retVar.ExpireTime = ret10;
                        rdr.ConvertDBData(11, out Int64 ret11); retVar.Price = ret11;
                        rdr.ConvertDBData(12, out bool ret12); retVar.IsRewarded = ret12;
                        rdr.ConvertDBData(13, out string ret13); retVar.BillingData = ret13;
                        rdr.ConvertDBData(14, out DateTime ret14); retVar.CreateAt = ret14;
                        rdr.ConvertDBData(15, out DateTime ret15); retVar.UpdateAt = ret15;
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

        public async Task<(bool, List<Billing>)> SelectBillings(Int64 UID)
        {
            bool queryRes = false;
            List<Billing> ret = new List<Billing>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select TID, OriginalTID, MemberNo, NickNameNo, NotiType, TxnTime, MarketCode, ProductID, SubStatusCode, ExpireTime, Price, IsRewarded, BillingData, CreateAt, UpdateAt from Billing where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Billing>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Billing();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out string ret0); retVar.TID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.OriginalTID = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.MemberNo = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.NickNameNo = ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.NotiType = ret4;
                        rdr.ConvertDBData(5, out UInt64 ret5); retVar.TxnTime = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.MarketCode = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.ProductID = ret7;
                        rdr.ConvertDBData(8, out string ret8); retVar.SubStatusCode = ret8;
                        rdr.ConvertDBData(9, out UInt64 ret9); retVar.ExpireTime = ret9;
                        rdr.ConvertDBData(10, out Int64 ret10); retVar.Price = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.IsRewarded = ret11;
                        rdr.ConvertDBData(12, out string ret12); retVar.BillingData = ret12;
                        rdr.ConvertDBData(13, out DateTime ret13); retVar.CreateAt = ret13;
                        rdr.ConvertDBData(14, out DateTime ret14); retVar.UpdateAt = ret14;
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

        public async Task<(bool, Billing?)> SelectBilling(Int64 UID, string TID)
        {
            bool queryRes = false;
            Billing? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select OriginalTID, MemberNo, NickNameNo, NotiType, TxnTime, MarketCode, ProductID, SubStatusCode, ExpireTime, Price, IsRewarded, BillingData, CreateAt, UpdateAt from Billing where UID = @01 and TID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", TID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Billing();
                        retVar.UID = UID;
                        retVar.TID = TID;
                        rdr.ConvertDBData(0, out string ret0); retVar.OriginalTID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.MemberNo = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.NickNameNo = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.NotiType = ret3;
                        rdr.ConvertDBData(4, out UInt64 ret4); retVar.TxnTime = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.MarketCode = ret5;
                        rdr.ConvertDBData(6, out string ret6); retVar.ProductID = ret6;
                        rdr.ConvertDBData(7, out string ret7); retVar.SubStatusCode = ret7;
                        rdr.ConvertDBData(8, out UInt64 ret8); retVar.ExpireTime = ret8;
                        rdr.ConvertDBData(9, out Int64 ret9); retVar.Price = ret9;
                        rdr.ConvertDBData(10, out bool ret10); retVar.IsRewarded = ret10;
                        rdr.ConvertDBData(11, out string ret11); retVar.BillingData = ret11;
                        rdr.ConvertDBData(12, out DateTime ret12); retVar.CreateAt = ret12;
                        rdr.ConvertDBData(13, out DateTime ret13); retVar.UpdateAt = ret13;
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

        public async Task<(bool, List<Incubation>)> SelectIncubations(Int64 UID)
        {
            bool queryRes = false;
            List<Incubation> ret = new List<Incubation>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select IncubatorID, IncubateCount, PetEggID, IncubationEndAt, IsDeleted, CreateAt, UpdateAt from Incubation where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Incubation>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Incubation();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.IncubatorID = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.IncubateCount = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.PetEggID = ret2;
                        rdr.ConvertDBData(3, out Int64 ret3); retVar.IncubationEndAt = ret3;
                        rdr.ConvertDBData(4, out bool ret4); retVar.IsDeleted = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.CreateAt = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.UpdateAt = ret6;
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

        public async Task<(bool, Incubation?)> SelectIncubation(Int64 UID, Int64 IncubatorID)
        {
            bool queryRes = false;
            Incubation? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select IncubateCount, PetEggID, IncubationEndAt, IsDeleted, CreateAt, UpdateAt from Incubation where UID = @01 and IncubatorID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", IncubatorID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Incubation();
                        retVar.UID = UID;
                        retVar.IncubatorID = IncubatorID;
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.IncubateCount = ret0;
                        rdr.ConvertDBData(1, out Int64 ret1); retVar.PetEggID = ret1;
                        rdr.ConvertDBData(2, out Int64 ret2); retVar.IncubationEndAt = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.IsDeleted = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.CreateAt = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.UpdateAt = ret5;
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

        public async Task<(bool, List<InstantGuide>)> SelectInstantGuides(Int64 UID)
        {
            bool queryRes = false;
            List<InstantGuide> ret = new List<InstantGuide>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select InstantGuideList, CreateAt, UpdateAt from InstantGuide where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<InstantGuide>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new InstantGuide();
                        retVar.UID = UID;
                        rdr.ConvertDBData(0, out List<Int64> ret0); retVar.InstantGuideList = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.CreateAt = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.UpdateAt = ret2;
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
