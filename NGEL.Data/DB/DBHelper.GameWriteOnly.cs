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
    public class DBHelperGameWriteOnly
    {
        string _conStr = "";

        readonly ILogger<DBHelperGameWriteOnly> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperGameWriteOnly(ILogger<DBHelperGameWriteOnly> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<bool> UpdateUserAccount(UserAccount argClass)
        {
            return await UpdateUserAccount(argClass.UID, argClass.Nick, argClass.HeroID, argClass.ProfileIconID, argClass.ProfileBGID, argClass.EntitlementID, argClass.PetUniqueID, argClass.VehicleID, argClass.LastClearChapterID, argClass.IsGlitchTutorialComplete, argClass.IsGoldClashTutorialComplete, argClass.AddArtifactDeckCount, argClass.PenaltyReportCount, argClass.LastPenaltyReportAt, argClass.IntroduceID);
        }

        public async Task<bool> UpdateUserAccount(Int64 UID, string Nick, int HeroID, int ProfileIconID, int ProfileBGID, int EntitlementID, int PetUniqueID, int VehicleID, int LastClearChapterID, bool IsGlitchTutorialComplete, bool IsGoldClashTutorialComplete, int AddArtifactDeckCount, int PenaltyReportCount, Int64 LastPenaltyReportAt, Int64 IntroduceID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update UserAccount set Nick = @02, HeroID = @03, ProfileIconID = @04, ProfileBGID = @05, EntitlementID = @06, PetUniqueID = @07, VehicleID = @08, LastClearChapterID = @09, IsGlitchTutorialComplete = @10, IsGoldClashTutorialComplete = @11, AddArtifactDeckCount = @12, PenaltyReportCount = @13, LastPenaltyReportAt = @14, IntroduceID = @15 where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", Nick);
                    cmd.Parameters.AddWithValue("@03", HeroID);
                    cmd.Parameters.AddWithValue("@04", ProfileIconID);
                    cmd.Parameters.AddWithValue("@05", ProfileBGID);
                    cmd.Parameters.AddWithValue("@06", EntitlementID);
                    cmd.Parameters.AddWithValue("@07", PetUniqueID);
                    cmd.Parameters.AddWithValue("@08", VehicleID);
                    cmd.Parameters.AddWithValue("@09", LastClearChapterID);
                    cmd.Parameters.AddWithValue("@10", (IsGlitchTutorialComplete ? 1 : 0));
                    cmd.Parameters.AddWithValue("@11", (IsGoldClashTutorialComplete ? 1 : 0));
                    cmd.Parameters.AddWithValue("@12", AddArtifactDeckCount);
                    cmd.Parameters.AddWithValue("@13", PenaltyReportCount);
                    cmd.Parameters.AddWithValue("@14", LastPenaltyReportAt);
                    cmd.Parameters.AddWithValue("@15", IntroduceID);
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

        public async Task<bool> InsertAchievement(Achievement argClass)
        {
            return await InsertAchievement(argClass.UID, argClass.AchievementID, argClass.Count, argClass.CompleteAt);
        }

        public async Task<bool> InsertAchievement(Int64 UID, Int64 AchievementID, Int64 Count, Int64 CompleteAt)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Achievement (UID, AchievementID, Count, CompleteAt) values (@01, @02, @03, @04)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AchievementID);
                    cmd.Parameters.AddWithValue("@03", Count);
                    cmd.Parameters.AddWithValue("@04", CompleteAt);
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

        public async Task<bool> UpdateAchievement(Achievement argClass)
        {
            return await UpdateAchievement(argClass.UID, argClass.AchievementID, argClass.Count, argClass.CompleteAt);
        }

        public async Task<bool> UpdateAchievement(Int64 UID, Int64 AchievementID, Int64 Count, Int64 CompleteAt)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Achievement set Count = @03, CompleteAt = @04 where UID = @01 and AchievementID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AchievementID);
                    cmd.Parameters.AddWithValue("@03", Count);
                    cmd.Parameters.AddWithValue("@04", CompleteAt);
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

        public async Task<bool> DeleteAchievement(Achievement argClass)
        {
            return await DeleteAchievement(argClass.UID, argClass.AchievementID);
        }

        public async Task<bool> DeleteAchievement(Int64 UID, Int64 AchievementID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Achievement where UID = @01 and AchievementID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AchievementID);
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

        public async Task<bool> InsertAsset(Asset argClass)
        {
            return await InsertAsset(argClass.UID, argClass.AssetID, argClass.Count);
        }

        public async Task<bool> InsertAsset(Int64 UID, Int64 AssetID, Int64 Count)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Asset (UID, AssetID, Count) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AssetID);
                    cmd.Parameters.AddWithValue("@03", Count);
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

        public async Task<bool> UpdateAsset(Asset argClass)
        {
            return await UpdateAsset(argClass.UID, argClass.AssetID, argClass.Count);
        }

        public async Task<bool> UpdateAsset(Int64 UID, Int64 AssetID, Int64 Count)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Asset set Count = @03 where UID = @01 and AssetID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AssetID);
                    cmd.Parameters.AddWithValue("@03", Count);
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

        public async Task<bool> DeleteAsset(Asset argClass)
        {
            return await DeleteAsset(argClass.UID, argClass.AssetID);
        }

        public async Task<bool> DeleteAsset(Int64 UID, Int64 AssetID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Asset where UID = @01 and AssetID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AssetID);
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

        public async Task<bool> InsertAttendance(Attendance argClass)
        {
            return await InsertAttendance(argClass.UID, argClass.AttendanceID, argClass.RewardState);
        }

        public async Task<bool> InsertAttendance(Int64 UID, int AttendanceID, string RewardState)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Attendance (UID, AttendanceID, RewardState, LastAttendanceAt) values (@01, @02, @03, UNIX_TIMESTAMP())", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AttendanceID);
                    cmd.Parameters.AddWithValue("@03", RewardState);
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

        public async Task<bool> UpdateAttendance(Attendance argClass)
        {
            return await UpdateAttendance(argClass.UID, argClass.AttendanceID, argClass.RewardState);
        }

        public async Task<bool> UpdateAttendance(Int64 UID, int AttendanceID, string RewardState)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Attendance set RewardState = @03, LastAttendanceAt = UNIX_TIMESTAMP() where UID = @01 and AttendanceID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", AttendanceID);
                    cmd.Parameters.AddWithValue("@03", RewardState);
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

        public async Task<bool> InsertArtifact(Artifact argClass)
        {
            return await InsertArtifact(argClass.UID, argClass.ArtifactID, argClass.Enhance, argClass.Count);
        }

        public async Task<bool> InsertArtifact(Int64 UID, int ArtifactID, int Enhance, int Count)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Artifact (UID, ArtifactID, Enhance, Count) values (@01, @02, @03, @04)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ArtifactID);
                    cmd.Parameters.AddWithValue("@03", Enhance);
                    cmd.Parameters.AddWithValue("@04", Count);
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

        public async Task<bool> UpdateArtifact(Artifact argClass)
        {
            return await UpdateArtifact(argClass.UID, argClass.ArtifactID, argClass.Enhance, argClass.Count);
        }

        public async Task<bool> UpdateArtifact(Int64 UID, int ArtifactID, int Enhance, int Count)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Artifact set Enhance = @03, Count = @04 where UID = @01 and ArtifactID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ArtifactID);
                    cmd.Parameters.AddWithValue("@03", Enhance);
                    cmd.Parameters.AddWithValue("@04", Count);
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

        public async Task<bool> DeleteArtifact(Artifact argClass)
        {
            return await DeleteArtifact(argClass.UID, argClass.ArtifactID);
        }

        public async Task<bool> DeleteArtifact(Int64 UID, int ArtifactID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Artifact where UID = @01 and ArtifactID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ArtifactID);
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

        public async Task<bool> InsertCollectionAccessory(Collection argClass)
        {
            return await InsertCollectionAccessory(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> InsertCollectionAccessory(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Collection_Accessory (UID, CollectionID, IsRewarded) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> UpdateCollectionAccessory(Collection argClass)
        {
            return await UpdateCollectionAccessory(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> UpdateCollectionAccessory(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Collection_Accessory set IsRewarded = @03 where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> DeleteCollectionAccessory(Collection argClass)
        {
            return await DeleteCollectionAccessory(argClass.UID, argClass.CollectionID);
        }

        public async Task<bool> DeleteCollectionAccessory(Int64 UID, Int64 CollectionID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Collection_Accessory where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
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

        public async Task<bool> InsertCollectionCostume(Collection argClass)
        {
            return await InsertCollectionCostume(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> InsertCollectionCostume(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Collection_Costume (UID, CollectionID, IsRewarded) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> UpdateCollectionCostume(Collection argClass)
        {
            return await UpdateCollectionCostume(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> UpdateCollectionCostume(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Collection_Costume set IsRewarded = @03 where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> DeleteCollectionCostume(Collection argClass)
        {
            return await DeleteCollectionCostume(argClass.UID, argClass.CollectionID);
        }

        public async Task<bool> DeleteCollectionCostume(Int64 UID, Int64 CollectionID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Collection_Costume where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
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

        public async Task<bool> InsertCollectionMonster(Collection argClass)
        {
            return await InsertCollectionMonster(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> InsertCollectionMonster(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Collection_Monster (UID, CollectionID, IsRewarded) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> UpdateCollectionMonster(Collection argClass)
        {
            return await UpdateCollectionMonster(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> UpdateCollectionMonster(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Collection_Monster set IsRewarded = @03 where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> DeleteCollectionMonster(Collection argClass)
        {
            return await DeleteCollectionMonster(argClass.UID, argClass.CollectionID);
        }

        public async Task<bool> DeleteCollectionMonster(Int64 UID, Int64 CollectionID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Collection_Monster where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
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

        public async Task<bool> InsertCollectionPet(Collection argClass)
        {
            return await InsertCollectionPet(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> InsertCollectionPet(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Collection_Pet (UID, CollectionID, IsRewarded) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> UpdateCollectionPet(Collection argClass)
        {
            return await UpdateCollectionPet(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> UpdateCollectionPet(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Collection_Pet set IsRewarded = @03 where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> DeleteCollectionPet(Collection argClass)
        {
            return await DeleteCollectionPet(argClass.UID, argClass.CollectionID);
        }

        public async Task<bool> DeleteCollectionPet(Int64 UID, Int64 CollectionID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Collection_Pet where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
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

        public async Task<bool> InsertCollectionVehicle(Collection argClass)
        {
            return await InsertCollectionVehicle(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> InsertCollectionVehicle(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Collection_Vehicle (UID, CollectionID, IsRewarded) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> UpdateCollectionVehicle(Collection argClass)
        {
            return await UpdateCollectionVehicle(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> UpdateCollectionVehicle(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Collection_Vehicle set IsRewarded = @03 where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> DeleteCollectionVehicle(Collection argClass)
        {
            return await DeleteCollectionVehicle(argClass.UID, argClass.CollectionID);
        }

        public async Task<bool> DeleteCollectionVehicle(Int64 UID, Int64 CollectionID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Collection_Vehicle where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
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

        public async Task<bool> InsertCollectionWeapon(Collection argClass)
        {
            return await InsertCollectionWeapon(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> InsertCollectionWeapon(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Collection_Weapon (UID, CollectionID, IsRewarded) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> UpdateCollectionWeapon(Collection argClass)
        {
            return await UpdateCollectionWeapon(argClass.UID, argClass.CollectionID, argClass.IsRewarded);
        }

        public async Task<bool> UpdateCollectionWeapon(Int64 UID, Int64 CollectionID, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Collection_Weapon set IsRewarded = @03 where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
                    cmd.Parameters.AddWithValue("@03", (IsRewarded ? 1 : 0));
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

        public async Task<bool> DeleteCollectionWeapon(Collection argClass)
        {
            return await DeleteCollectionWeapon(argClass.UID, argClass.CollectionID);
        }

        public async Task<bool> DeleteCollectionWeapon(Int64 UID, Int64 CollectionID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Collection_Weapon where UID = @01 and CollectionID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", CollectionID);
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

        public async Task<bool> InsertEntitlement(Entitlement argClass)
        {
            return await InsertEntitlement(argClass.UID, argClass.EntitlementID);
        }

        public async Task<bool> InsertEntitlement(Int64 UID, Int64 EntitlementID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Entitlement (UID, EntitlementID) values (@01, @02)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", EntitlementID);
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

        public async Task<bool> DeleteEntitlement(Entitlement argClass)
        {
            return await DeleteEntitlement(argClass.UID, argClass.EntitlementID);
        }

        public async Task<bool> DeleteEntitlement(Int64 UID, Int64 EntitlementID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Entitlement where UID = @01 and EntitlementID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", EntitlementID);
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

        public async Task<bool> UpdateGuideMission(GuideMission argClass)
        {
            return await UpdateGuideMission(argClass.UID, argClass.GuideMissionCategory, argClass.MissionID, argClass.IsCompleted, argClass.IsRewarded);
        }

        public async Task<bool> UpdateGuideMission(Int64 UID, EGuideMissionCategory GuideMissionCategory, Int64 MissionID, bool IsCompleted, bool IsRewarded)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update GuideMission set MissionID = @03, IsCompleted = @04, IsRewarded = @05 where UID = @01 and GuideMissionCategory = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", GuideMissionCategory);
                    cmd.Parameters.AddWithValue("@03", MissionID);
                    cmd.Parameters.AddWithValue("@04", (IsCompleted ? 1 : 0));
                    cmd.Parameters.AddWithValue("@05", (IsRewarded ? 1 : 0));
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

        public async Task<bool> InsertInventory(Inventory argClass)
        {
            return await InsertInventory(argClass.UID, argClass.ItemID, argClass.Count);
        }

        public async Task<bool> InsertInventory(Int64 UID, Int64 ItemID, Int64 Count)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Inventory (UID, ItemID, Count) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ItemID);
                    cmd.Parameters.AddWithValue("@03", Count);
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

        public async Task<bool> UpdateInventory(Inventory argClass)
        {
            return await UpdateInventory(argClass.UID, argClass.ItemID, argClass.Count);
        }

        public async Task<bool> UpdateInventory(Int64 UID, Int64 ItemID, Int64 Count)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Inventory set Count = @03 where UID = @01 and ItemID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ItemID);
                    cmd.Parameters.AddWithValue("@03", Count);
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

        public async Task<bool> DeleteInventory(Inventory argClass)
        {
            return await DeleteInventory(argClass.UID, argClass.ItemID);
        }

        public async Task<bool> DeleteInventory(Int64 UID, Int64 ItemID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Inventory where UID = @01 and ItemID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ItemID);
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

        public async Task<(bool, Mail?)> InsertMail(Mail argClass)
        {
            return await InsertMail(argClass.UID, argClass.MailType, argClass.State, argClass.IsBM, argClass.Title, argClass.Message, argClass.RewardList, argClass.ExpireAt);
        }

        public async Task<(bool, Mail?)> InsertMail(Int64 UID, EMailType MailType, EMailStateType State, bool IsBM, string Title, string Message, List<MailReward> RewardList, int ExpireAt)
        {
            bool queryRes = false;
            Mail? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @MailID = (select max(MailID) + 1 from Mail);if @MailID is null then set @MailID = 1; end if;insert into Mail (MailID, UID, MailType, State, IsBM, Title, Message, RewardList, ExpireAt) values (@MailID, @01, @02, @03, @04, @05, @06, @07, @08);select @MailID;", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", MailType);
                    cmd.Parameters.AddWithValue("@03", State);
                    cmd.Parameters.AddWithValue("@04", (IsBM ? 1 : 0));
                    cmd.Parameters.AddWithValue("@05", Title);
                    cmd.Parameters.AddWithValue("@06", Message);
                    cmd.Parameters.AddWithValue("@07", JsonConvert.SerializeObject(RewardList));
                    cmd.Parameters.AddWithValue("@08", ExpireAt);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Mail();
                        rdr.ConvertDBData(0, out Int64 ret0); retVar.MailID = ret0;
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

        public async Task<bool> DeleteMail(Mail argClass)
        {
            return await DeleteMail(argClass.MailID);
        }

        public async Task<bool> DeleteMail(Int64 MailID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Mail where MailID = @01", con);
                    cmd.Parameters.AddWithValue("@01", MailID);
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

        public async Task<bool> DisablePenalty(PenaltyWithUser argClass)
        {
            return await DisablePenalty(argClass.UID, argClass.ReportState, argClass.PenaltyGrade);
        }

        public async Task<bool> DisablePenalty(Int64 UID, EPenaltyReportState ReportState, int PenaltyGrade)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update Penalty set IsActive = false, PenaltyGrade = @03, PenaltyPoint = 0, PenaltyCount = 0, PenaltyEndAt = 0, ClearPenaltyAt = 0 where UID = @01 and ReportState = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ReportState);
                    cmd.Parameters.AddWithValue("@03", PenaltyGrade);
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

        public async Task<bool> InsertUserBlock(UserBlock argClass)
        {
            return await InsertUserBlock(argClass.MemberNo, argClass.UID, argClass.BlockReasonID, argClass.BlockReasonStr, argClass.StartTime, argClass.EndTime);
        }

        public async Task<bool> InsertUserBlock(UInt64 MemberNo, Int64 UID, int BlockReasonID, string BlockReasonStr, DateTime StartTime, DateTime EndTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into UserBlock (MemberNo, UID, BlockReasonID, BlockReasonStr, StartTime, EndTime) values (@01, @02, @03, @04, @05, @06)", con);
                    cmd.Parameters.AddWithValue("@01", MemberNo);
                    cmd.Parameters.AddWithValue("@02", UID);
                    cmd.Parameters.AddWithValue("@03", BlockReasonID);
                    cmd.Parameters.AddWithValue("@04", BlockReasonStr);
                    cmd.Parameters.AddWithValue("@05", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@06", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<bool> UpdateUserBlock(UserBlock argClass)
        {
            return await UpdateUserBlock(argClass.MemberNo, argClass.BlockReasonID, argClass.BlockReasonStr, argClass.StartTime, argClass.EndTime);
        }

        public async Task<bool> UpdateUserBlock(UInt64 MemberNo, int BlockReasonID, string BlockReasonStr, DateTime StartTime, DateTime EndTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update UserBlock set BlockReasonID = @02, BlockReasonStr = @03, StartTime = @04, EndTime = @05 where MemberNo = @01", con);
                    cmd.Parameters.AddWithValue("@01", MemberNo);
                    cmd.Parameters.AddWithValue("@02", BlockReasonID);
                    cmd.Parameters.AddWithValue("@03", BlockReasonStr);
                    cmd.Parameters.AddWithValue("@04", (StartTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@05", (EndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<bool> DeleteUserBlock(UserBlock argClass)
        {
            return await DeleteUserBlock(argClass.MemberNo);
        }

        public async Task<bool> DeleteUserBlock(UInt64 MemberNo)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from UserBlock where MemberNo = @01", con);
                    cmd.Parameters.AddWithValue("@01", MemberNo);
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

        public async Task<bool> InsertProfile(Profile argClass)
        {
            return await InsertProfile(argClass.UID, argClass.ProfileID, argClass.ProfileType);
        }

        public async Task<bool> InsertProfile(Int64 UID, int ProfileID, EProfileType ProfileType)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into Profile (UID, ProfileID, ProfileType) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ProfileID);
                    cmd.Parameters.AddWithValue("@03", ProfileType);
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

        public async Task<bool> DeleteProfile(Profile argClass)
        {
            return await DeleteProfile(argClass.UID, argClass.ProfileID);
        }

        public async Task<bool> DeleteProfile(Int64 UID, int ProfileID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from Profile where UID = @01 and ProfileID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ProfileID);
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

        public async Task<bool> InsertRpgAttribute(RpgAttribute argClass)
        {
            return await InsertRpgAttribute(argClass.UID, argClass.ID, argClass.Level);
        }

        public async Task<bool> InsertRpgAttribute(Int64 UID, Int64 ID, int Level)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into RpgAttribute (UID, ID, Level) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ID);
                    cmd.Parameters.AddWithValue("@03", Level);
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

        public async Task<bool> UpdateRpgAttribute(RpgAttribute argClass)
        {
            return await UpdateRpgAttribute(argClass.UID, argClass.ID, argClass.Level);
        }

        public async Task<bool> UpdateRpgAttribute(Int64 UID, Int64 ID, int Level)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update RpgAttribute set Level = @03 where UID = @01 and ID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ID);
                    cmd.Parameters.AddWithValue("@03", Level);
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

        public async Task<bool> DeleteRpgAttribute(RpgAttribute argClass)
        {
            return await DeleteRpgAttribute(argClass.UID, argClass.ID);
        }

        public async Task<bool> DeleteRpgAttribute(Int64 UID, Int64 ID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from RpgAttribute where UID = @01 and ID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", ID);
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

        public async Task<bool> UpdateSeasonMission(SeasonMission argClass)
        {
            return await UpdateSeasonMission(argClass.UID, argClass.SeasonPassID, argClass.MissionID, argClass.Count, argClass.IsComplete);
        }

        public async Task<bool> UpdateSeasonMission(Int64 UID, int SeasonPassID, Int64 MissionID, Int64 Count, bool IsComplete)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update SeasonMission set Count = @04, IsComplete = @05 where UID = @01 and SeasonPassID = @02 and MissionID = @03", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", SeasonPassID);
                    cmd.Parameters.AddWithValue("@03", MissionID);
                    cmd.Parameters.AddWithValue("@04", Count);
                    cmd.Parameters.AddWithValue("@05", (IsComplete ? 1 : 0));
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

        public async Task<bool> InsertSeasonPass(SeasonPass argClass)
        {
            return await InsertSeasonPass(argClass.UID, argClass.SeasonPassID, argClass.SeasonNum, argClass.IsPaid, argClass.Level, argClass.Exp, argClass.RewardState);
        }

        public async Task<bool> InsertSeasonPass(Int64 UID, int SeasonPassID, int SeasonNum, bool IsPaid, int Level, Int64 Exp, string RewardState)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into SeasonPass (UID, SeasonPassID, SeasonNum, IsPaid, Level, Exp, RewardState) values (@01, @02, @03, @04, @05, @06, @07)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", SeasonPassID);
                    cmd.Parameters.AddWithValue("@03", SeasonNum);
                    cmd.Parameters.AddWithValue("@04", (IsPaid ? 1 : 0));
                    cmd.Parameters.AddWithValue("@05", Level);
                    cmd.Parameters.AddWithValue("@06", Exp);
                    cmd.Parameters.AddWithValue("@07", RewardState);
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

        public async Task<bool> UpdateSeasonPass(SeasonPass argClass)
        {
            return await UpdateSeasonPass(argClass.UID, argClass.SeasonPassID, argClass.SeasonNum, argClass.IsPaid, argClass.Level, argClass.Exp, argClass.RewardState);
        }

        public async Task<bool> UpdateSeasonPass(Int64 UID, int SeasonPassID, int SeasonNum, bool IsPaid, int Level, Int64 Exp, string RewardState)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update SeasonPass set SeasonNum = @03, IsPaid = @04, Level = @05, Exp = @06, RewardState = @07 where UID = @01 and SeasonPassID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", SeasonPassID);
                    cmd.Parameters.AddWithValue("@03", SeasonNum);
                    cmd.Parameters.AddWithValue("@04", (IsPaid ? 1 : 0));
                    cmd.Parameters.AddWithValue("@05", Level);
                    cmd.Parameters.AddWithValue("@06", Exp);
                    cmd.Parameters.AddWithValue("@07", RewardState);
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

        public async Task<bool> InsertTreasureBox(TreasureBox argClass)
        {
            return await InsertTreasureBox(argClass.UID, argClass.BoxList, argClass.ExpireAt);
        }

        public async Task<bool> InsertTreasureBox(Int64 UID, List<UserTreasureBox> BoxList, Int64 ExpireAt)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into TreasureBox (UID, BoxList, ExpireAt) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", JsonConvert.SerializeObject(BoxList));
                    cmd.Parameters.AddWithValue("@03", ExpireAt);
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

        public async Task<bool> DeleteTreasureBox(TreasureBox argClass)
        {
            return await DeleteTreasureBox(argClass.UID);
        }

        public async Task<bool> DeleteTreasureBox(Int64 UID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from TreasureBox where UID = @01", con);
                    cmd.Parameters.AddWithValue("@01", UID);
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

        public async Task<bool> InsertWeaponCategory(WeaponCategory argClass)
        {
            return await InsertWeaponCategory(argClass.UID, argClass.WeaponCategoryID, argClass.Exp);
        }

        public async Task<bool> InsertWeaponCategory(Int64 UID, Int64 WeaponCategoryID, Int64 Exp)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into WeaponCategory (UID, WeaponCategoryID, Exp) values (@01, @02, @03)", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", WeaponCategoryID);
                    cmd.Parameters.AddWithValue("@03", Exp);
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

        public async Task<bool> UpdateWeaponCategory(WeaponCategory argClass)
        {
            return await UpdateWeaponCategory(argClass.UID, argClass.WeaponCategoryID, argClass.Exp);
        }

        public async Task<bool> UpdateWeaponCategory(Int64 UID, Int64 WeaponCategoryID, Int64 Exp)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update WeaponCategory set Exp = @03 where UID = @01 and WeaponCategoryID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", WeaponCategoryID);
                    cmd.Parameters.AddWithValue("@03", Exp);
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

        public async Task<bool> DeleteWeaponCategory(WeaponCategory argClass)
        {
            return await DeleteWeaponCategory(argClass.UID, argClass.WeaponCategoryID);
        }

        public async Task<bool> DeleteWeaponCategory(Int64 UID, Int64 WeaponCategoryID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from WeaponCategory where UID = @01 and WeaponCategoryID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", WeaponCategoryID);
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

        public async Task<bool> UpdateWonderCube(WonderCube argClass)
        {
            return await UpdateWonderCube(argClass.UID, argClass.SlotID, argClass.WonderCubeID, argClass.CreateAt, argClass.UpdateAt);
        }

        public async Task<bool> UpdateWonderCube(Int64 UID, int SlotID, Int64 WonderCubeID, DateTime CreateAt, DateTime UpdateAt)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update WonderCube set WonderCubeID = @03 where UID = @01 and SlotID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", SlotID);
                    cmd.Parameters.AddWithValue("@03", WonderCubeID);
                    cmd.Parameters.AddWithValue("@04", (CreateAt.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@05", (UpdateAt.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<bool> UpdateWonderStore(WonderStore argClass)
        {
            return await UpdateWonderStore(argClass.UID, argClass.StoreID, argClass.IsSubscription, argClass.SubscriptionExpireAt);
        }

        public async Task<bool> UpdateWonderStore(Int64 UID, int StoreID, bool IsSubscription, Int64 SubscriptionExpireAt)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update WonderStore set IsSubscription = @03, SubscriptionExpireAt = @04 where UID = @01 and StoreID = @02", con);
                    cmd.Parameters.AddWithValue("@01", UID);
                    cmd.Parameters.AddWithValue("@02", StoreID);
                    cmd.Parameters.AddWithValue("@03", (IsSubscription ? 1 : 0));
                    cmd.Parameters.AddWithValue("@04", SubscriptionExpireAt);
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
