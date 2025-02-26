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
    public class DBHelperGameManagerWriteOnly
    {
        string _conStr = "";

        readonly ILogger<DBHelperGameManagerWriteOnly> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperGameManagerWriteOnly(ILogger<DBHelperGameManagerWriteOnly> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, Guid)> InsertUser(User argClass)
        {
            return await InsertUser(argClass.password, argClass.name);
        }

        public async Task<(bool, Guid)> InsertUser(string password, string name)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into users (id, password, name, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime) values (@keyId, @01, @02, false, 0, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", password);
                    cmd.Parameters.AddWithValue("@02", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<(bool, Guid)> InsertUserWithoutPW(User argClass)
        {
            return await InsertUserWithoutPW(argClass.name);
        }

        public async Task<(bool, Guid)> InsertUserWithoutPW(string name)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into users (id, password, name, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime) values (@keyId, '', @01, false, 0, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<bool> DeleteUser(Guid userId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from users where id = @01; delete from user_emails where userId = @01;  delete from user_oauth_keys where userId = @01;  delete from user_roles where userId = @01;", con);
                    cmd.Parameters.AddWithValue("@01", userId);
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

        public async Task<bool> DeleteUserSoft(Guid userId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update users set isDeleted = 1 where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
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

        public async Task<bool> RestoreUser(Guid userId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update users set isDeleted = 0 where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
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

        public async Task<(bool, Guid)> InsertUserEmail(UserEmail argClass)
        {
            return await InsertUserEmail(argClass.email, argClass.userId);
        }

        public async Task<(bool, Guid)> InsertUserEmail(string email, Guid userId)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into user_emails (id, email, emailConfirmId, isEmailConfirmed, emailConfirmIdIssuedTime, userId) values (@keyId, @01, '', false, UTC_TIMESTAMP(6), @02); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", email);
                    cmd.Parameters.AddWithValue("@02", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<(bool, Guid)> InsertUserEmailWithConfirm(UserEmail argClass)
        {
            return await InsertUserEmailWithConfirm(argClass.email, argClass.emailConfirmId, argClass.userId);
        }

        public async Task<(bool, Guid)> InsertUserEmailWithConfirm(string email, string emailConfirmId, Guid userId)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into user_emails (id, email, emailConfirmId, isEmailConfirmed, emailConfirmIdIssuedTime, userId) values (@keyId, @01, @02, false, UTC_TIMESTAMP(6), @03); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", email);
                    cmd.Parameters.AddWithValue("@02", emailConfirmId);
                    cmd.Parameters.AddWithValue("@03", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<bool> SetInitUserEmail(string email)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update user_emails set isEmailConfirmed = false, emailConfirmId = UUID() where email = @01", con);
                    cmd.Parameters.AddWithValue("@01", email);
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

        public async Task<bool> ConfirmUserEmail(string id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update user_emails set isEmailConfirmed = true where emailConfirmId = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> DeleteUserEmail(Guid emailId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from user_emails where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", emailId);
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

        public async Task<(bool, Guid)> InsertUserOAuthKey(UserOAuthKey argClass)
        {
            return await InsertUserOAuthKey(argClass.provider, argClass.providerUid, argClass.userId, argClass.userEmailId);
        }

        public async Task<(bool, Guid)> InsertUserOAuthKey(Defines.OAuthProvider provider, string providerUid, Guid userId, Guid userEmailId)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into user_oauth_keys (id, provider, providerUid, userId, userEmailId) values (@keyId, @01, @02, @03, @04); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)provider);
                    cmd.Parameters.AddWithValue("@02", providerUid);
                    cmd.Parameters.AddWithValue("@03", userId);
                    cmd.Parameters.AddWithValue("@04", userEmailId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<(bool, Guid)> InsertRole(string name)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @id = uuid(); insert into roles (id, name) values (@id, @01); select @id", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<(bool, Guid)> InsertUserRole(Guid userId, Guid roleId)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @id = uuid(); insert into user_roles (id, userId, roleId) values (@id, @01, @02); select @id", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", roleId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<bool> SetPasswordUserInfo(Guid id, string password)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update users set password = @02, latestChangePW = UTC_TIMESTAMP(6) where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", password);
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

        public async Task<bool> AddCountFailedSigninUserInfo(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update users set countFailedSignin = countFailedSignin + 1 where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> ClearCountFailedSigninUserInfo(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update users set countFailedSignin = 0 where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> ResetLatestSignInUserInfo(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update users set latestSignin = UTC_TIMESTAMP(6) where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> DeleteUserRoles(Guid userId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from user_roles where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
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

        public async Task<bool> InsertUserSignin(Guid userId, Guid signinId, string connectionId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into user_signins (userId, signinId, connectionId, latestUpdate, latestActive) values (@01, @02, @03, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6))", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", signinId);
                    cmd.Parameters.AddWithValue("@03", connectionId);
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

        public async Task<(bool, DateTime?)> SignInUser(Guid userId, Guid signinId, string connectionId)
        {
            bool queryRes = false;
            DateTime? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @utcnow = UTC_TIMESTAMP(6); update users set latestSignin = @utcnow where id = @01; update user_signins set signinId = @02, connectionId = @03, latestUpdate = @utcnow, latestActive = @utcnow where userId = @01; select @utcnow", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", signinId);
                    cmd.Parameters.AddWithValue("@03", connectionId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out DateTime retVar);
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

        public async Task<(bool, DateTime?)> UpdateSignInUser(Guid userId, string connectionId)
        {
            bool queryRes = false;
            DateTime? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @utcnow = UTC_TIMESTAMP(6); update user_signins set connectionId = @02, latestUpdate = @utcnow where userId = @01; select @utcnow", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", connectionId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out DateTime retVar);
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

        public async Task<bool> UpdateUserSignIn(Guid signinId, string connectionId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update user_signins set connectionId = @02, latestUpdate = UTC_TIMESTAMP(6) where signinId = @01", con);
                    cmd.Parameters.AddWithValue("@01", signinId);
                    cmd.Parameters.AddWithValue("@02", connectionId);
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

        public async Task<(bool, DateTime?)> ActiveSignInUser(Guid userId)
        {
            bool queryRes = false;
            DateTime? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @utcnow = UTC_TIMESTAMP(6); update user_signins set latestUpdate = @utcnow, latestActive = @utcnow where userId = @01; select @utcnow", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out DateTime retVar);
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

        public async Task<bool> ActiveUserSignIn(Guid signinId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update user_signins set latestUpdate = UTC_TIMESTAMP(6), latestActive = UTC_TIMESTAMP(6) where signinId = @01", con);
                    cmd.Parameters.AddWithValue("@01", signinId);
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

        public async Task<(bool, DateTime?)> SignOutUser(OAuthSignInUser argClass)
        {
            return await SignOutUser(argClass.userId);
        }

        public async Task<(bool, DateTime?)> SignOutUser(Guid userId)
        {
            bool queryRes = false;
            DateTime? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @utcnow = UTC_TIMESTAMP(6); update users set latestSignout = @utcnow where id = @01; delete from user_signins where userId = @01; select @utcnow", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out DateTime retVar);
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

        public async Task<(bool, DateTime?)> UpdateSignOutUser(OAuthSignInUser argClass)
        {
            return await UpdateSignOutUser(argClass.userId);
        }

        public async Task<(bool, DateTime?)> UpdateSignOutUser(Guid userId)
        {
            bool queryRes = false;
            DateTime? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @utcnow = UTC_TIMESTAMP(6); update user_signins set latestUpdate = @utcnow where userId = @01; delete from user_signins where userId = @01; select @utcnow", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out DateTime retVar);
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

        public async Task<(bool, DateTime?)> UpdateDisconnectedUser(string connectionId)
        {
            bool queryRes = false;
            DateTime? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @utcnow = UTC_TIMESTAMP(6); update user_signins set connectionId = '', latestUpdate = @utcnow where connectionId = @01; select @utcnow", con);
                    cmd.Parameters.AddWithValue("@01", connectionId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out DateTime retVar);
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

        public async Task<bool> InsertVersionInfo(VersionInfo argClass)
        {
            return await InsertVersionInfo(argClass.version, argClass.platform, argClass.serverState, argClass.clientState, argClass.cdnInfo);
        }

        public async Task<bool> InsertVersionInfo(string version, Defines.ServiceVersionManagementPlatform platform, Defines.ServiceVersionManagementServerState serverState, Defines.ServiceVersionManagementClientState clientState, string cdnInfo)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into version_infos (version, platform, serverState, clientState, cdnInfo) values (@01, @02, @03, @04, @05)", con);
                    cmd.Parameters.AddWithValue("@01", version);
                    cmd.Parameters.AddWithValue("@02", (Int64)platform);
                    cmd.Parameters.AddWithValue("@03", (Int64)serverState);
                    cmd.Parameters.AddWithValue("@04", (Int64)clientState);
                    cmd.Parameters.AddWithValue("@05", cdnInfo);
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

        public async Task<bool> UpdateVersionInfo(VersionInfo argClass)
        {
            return await UpdateVersionInfo(argClass.version, argClass.platform, argClass.serverState, argClass.clientState, argClass.cdnInfo);
        }

        public async Task<bool> UpdateVersionInfo(string version, Defines.ServiceVersionManagementPlatform platform, Defines.ServiceVersionManagementServerState serverState, Defines.ServiceVersionManagementClientState clientState, string cdnInfo)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update version_infos set serverState = @03, clientState = @04, cdnInfo = @05 where version = @01 and platform = @02", con);
                    cmd.Parameters.AddWithValue("@01", version);
                    cmd.Parameters.AddWithValue("@02", (Int64)platform);
                    cmd.Parameters.AddWithValue("@03", (Int64)serverState);
                    cmd.Parameters.AddWithValue("@04", (Int64)clientState);
                    cmd.Parameters.AddWithValue("@05", cdnInfo);
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

        public async Task<bool> DeleteVersionInfo(VersionInfo argClass)
        {
            return await DeleteVersionInfo(argClass.version, argClass.platform);
        }

        public async Task<bool> DeleteVersionInfo(string version, Defines.ServiceVersionManagementPlatform platform)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from version_infos where version = @01 and platform = @02", con);
                    cmd.Parameters.AddWithValue("@01", version);
                    cmd.Parameters.AddWithValue("@02", (Int64)platform);
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

        public async Task<bool> InsertBlockContent(BlockContent argClass)
        {
            return await InsertBlockContent(argClass.packetID);
        }

        public async Task<bool> InsertBlockContent(string packetID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into block_contents (packetID) values (@01)", con);
                    cmd.Parameters.AddWithValue("@01", packetID);
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

        public async Task<bool> DeleteBlockContent(BlockContent argClass)
        {
            return await DeleteBlockContent(argClass.packetID);
        }

        public async Task<bool> DeleteBlockContent(string packetID)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from block_contents where packetID = @01", con);
                    cmd.Parameters.AddWithValue("@01", packetID);
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

        public async Task<(bool, Guid)> InsertChattingNotice(ChattingNotice argClass)
        {
            return await InsertChattingNotice(argClass.message, argClass.noticeType, argClass.visibleTime, argClass.visibleCount, argClass.noticeTime);
        }

        public async Task<(bool, Guid)> InsertChattingNotice(string message, ENoticeType noticeType, int visibleTime, int visibleCount, DateTime noticeTime)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into chatting_notices (id, message, noticeType, visibleTime, visibleCount, noticeTime, regTime) values (@keyId, @01, @02, @03, @04, @05, UTC_TIMESTAMP(6)); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", message);
                    cmd.Parameters.AddWithValue("@02", noticeType);
                    cmd.Parameters.AddWithValue("@03", visibleTime);
                    cmd.Parameters.AddWithValue("@04", visibleCount);
                    cmd.Parameters.AddWithValue("@05", (noticeTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<bool> UpdateChattingNotice(ChattingNotice argClass)
        {
            return await UpdateChattingNotice(argClass.id, argClass.message, argClass.noticeType, argClass.visibleTime, argClass.visibleCount, argClass.noticeTime);
        }

        public async Task<bool> UpdateChattingNotice(Guid id, string message, ENoticeType noticeType, int visibleTime, int visibleCount, DateTime noticeTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update chatting_notices set message = @02, noticeType = @03, visibleTime = @04, visibleCount = @05, noticeTime = @06 where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", message);
                    cmd.Parameters.AddWithValue("@03", noticeType);
                    cmd.Parameters.AddWithValue("@04", visibleTime);
                    cmd.Parameters.AddWithValue("@05", visibleCount);
                    cmd.Parameters.AddWithValue("@06", (noticeTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<bool> DeleteChattingNotice(ChattingNotice argClass)
        {
            return await DeleteChattingNotice(argClass.id);
        }

        public async Task<bool> DeleteChattingNotice(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from chatting_notices where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<(bool, Guid)> InsertFirewall(Firewall argClass)
        {
            return await InsertFirewall(argClass.method, argClass.ipAddress, argClass.description);
        }

        public async Task<(bool, Guid)> InsertFirewall(Defines.FirewallMethod method, string ipAddress, string description)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into firewalls (id, method, ipAddress, description) values (@keyId, @01, @02, @03); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)method);
                    cmd.Parameters.AddWithValue("@02", ipAddress);
                    cmd.Parameters.AddWithValue("@03", description);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<bool> UpdateFirewall(Firewall argClass)
        {
            return await UpdateFirewall(argClass.id, argClass.ipAddress, argClass.description);
        }

        public async Task<bool> UpdateFirewall(Guid id, string ipAddress, string description)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update firewalls set ipAddress = @02, description where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", ipAddress);
                    cmd.Parameters.AddWithValue("@03", description);
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

        public async Task<bool> DeleteFirewall(Firewall argClass)
        {
            return await DeleteFirewall(argClass.id);
        }

        public async Task<bool> DeleteFirewall(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from firewalls where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> InsertCCU(string LobbyID, UInt64 Total)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into CCU (LobbyID, CreateAt, Total) values (@01, UTC_TIMESTAMP(), @02)", con);
                    cmd.Parameters.AddWithValue("@01", LobbyID);
                    cmd.Parameters.AddWithValue("@02", Total);
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

        public async Task<(bool, Guid)> InsertUserPassword(UserPassword argClass)
        {
            return await InsertUserPassword(argClass.userId, argClass.password);
        }

        public async Task<(bool, Guid)> InsertUserPassword(Guid userId, string password)
        {
            bool queryRes = false;
            Guid ret = Guid.Empty;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr + ";Allow User Variables=true"))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"set @keyId = uuid(); insert into user_passwords (id, userId, password, regTime) values (@keyId, @01, @02, UTC_TIMESTAMP(6)); select @keyId", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", password);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out Guid retVar);
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

        public async Task<bool> DeleteUserPasswords(Guid userId, string currentPassword)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from user_passwords where userId = @01 and `password` != @02;", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", currentPassword);
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

        public async Task<bool> DeleteUserPasswords(Guid userId)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from user_passwords where userId = @01;", con);
                    cmd.Parameters.AddWithValue("@01", userId);
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

        public async Task<bool> InsertUserJob(UserJob argClass)
        {
            return await InsertUserJob(argClass.id, argClass.userId, argClass.jobType, argClass.jobCount);
        }

        public async Task<bool> InsertUserJob(Guid id, Guid userId, Defines.UserJobType jobType, UInt64 jobCount)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into user_jobs (id, userId, jobType, jobCount, message) values (@01, @02, @03, @04, '')", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    cmd.Parameters.AddWithValue("@02", userId);
                    cmd.Parameters.AddWithValue("@03", (Int64)jobType);
                    cmd.Parameters.AddWithValue("@04", jobCount);
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

        public async Task<bool> AddSucceededUserJob(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update user_jobs set succeededCount = succeededCount + 1 where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> AddFailedUserJob(Guid id, string message)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"update user_jobs set failedCount = failedCount + 1, message = trim(concat(message, '\n', @02)) where id = @01", con);
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

        public async Task<bool> SaveExpireLogSetting(NGEL.Data.Models.Settings argClass)
        {
            return await SaveExpireLogSetting(argClass.value);
        }

        public async Task<bool> SaveExpireLogSetting(int value)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"insert into `settings` (`key`, `value`) values ('ExpireLog', @01) on duplicate key update `value` = @01", con);
                    cmd.Parameters.AddWithValue("@01", value);
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

        public async Task<bool> DeleteUserJob(UserJob argClass)
        {
            return await DeleteUserJob(argClass.id);
        }

        public async Task<bool> DeleteUserJob(Guid id)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"delete from user_jobs where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
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

        public async Task<bool> SaveChattingMessage(ChattingMessage argClass)
        {
            return await SaveChattingMessage(argClass.id, argClass.messageType, argClass.message, argClass.sendSigninId, argClass.sendUserName, argClass.sendConnectionId, argClass.targetUserId, argClass.targetConnectionId, argClass.localSendTime, argClass.localReceiveTime, argClass.serverReceiveTime, argClass.serverSendTime);
        }

        public async Task<bool> SaveChattingMessage(Guid id, Defines.ChattingMessageType messageType, string message, Guid sendSigninId, string sendUserName, string sendConnectionId, Guid targetUserId, string targetConnectionId, DateTime? localSendTime, DateTime? localReceiveTime, DateTime? serverReceiveTime, DateTime? serverSendTime)
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"x_saveChattingMessage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("__id", id);
                    cmd.Parameters.AddWithValue("__messageType", (Int64)messageType);
                    cmd.Parameters.AddWithValue("__message", message);
                    cmd.Parameters.AddWithValue("__sendSigninId", sendSigninId);
                    cmd.Parameters.AddWithValue("__sendUserName", sendUserName);
                    cmd.Parameters.AddWithValue("__sendConnectionId", sendConnectionId);
                    cmd.Parameters.AddWithValue("__targetUserId", targetUserId);
                    cmd.Parameters.AddWithValue("__targetConnectionId", targetConnectionId);
                    cmd.Parameters.AddWithValue("__localSendTime", (localSendTime?.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("__localReceiveTime", (localReceiveTime?.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("__serverReceiveTime", (serverReceiveTime?.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("__serverSendTime", (serverSendTime?.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
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

        public async Task<(bool, Server?)> CheckMasterServer(Guid id)
        {
            bool queryRes = false;
            Server? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"x_checkMasterServer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("__id", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Server();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out bool ret0); retVar.isMaster = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.updateTime = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.regTime = ret2;
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

        public async Task<bool> ClearIdleServer()
        {
            bool queryRes = false;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"x_clearIdleServer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
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
