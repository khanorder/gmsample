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
    public class DBHelperGameManager
    {
        string _conStr = "";

        readonly ILogger<DBHelperGameManager> _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly CommonSettings _commonSettings;
        public DBHelperGameManager(ILogger<DBHelperGameManager> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
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

        public async Task<(bool, List<User>)> SelectUsers()
        {
            bool queryRes = false;
            List<User> ret = new List<User>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from users", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<User>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new User();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.password = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.name = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isDeleted = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.latestSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignout = ret5;
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

        public async Task<(bool, User?)> SelectUserByID(Guid id)
        {
            bool queryRes = false;
            User? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select password, name, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime from users where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new User();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out string ret0); retVar.password = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.isDeleted = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.countFailedSignin = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.latestSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignout = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestChangePW = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.regTime = ret7;
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

        public async Task<(bool, User?)> SelectUserByName(string name)
        {
            bool queryRes = false;
            User? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, password, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime from users where name = @01", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new User();
                        retVar.name = name;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.password = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.isDeleted = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.countFailedSignin = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.latestSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignout = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestChangePW = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.regTime = ret7;
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

        public async Task<(bool, List<UserEmail>)> SelectUserEmails()
        {
            bool queryRes = false;
            List<UserEmail> ret = new List<UserEmail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from user_emails", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserEmail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserEmail();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.email = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.emailConfirmId = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isEmailConfirmed = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.emailConfirmIdIssuedTime = ret4;
                        rdr.ConvertDBData(5, out Guid ret5); retVar.userId = ret5;
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

        public async Task<(bool, List<UserEmail>)> SelectUserEmails(Guid userId)
        {
            bool queryRes = false;
            List<UserEmail> ret = new List<UserEmail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, email, emailConfirmId, isEmailConfirmed, emailConfirmIdIssuedTime from user_emails where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserEmail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserEmail();
                        retVar.userId = userId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.email = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.emailConfirmId = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isEmailConfirmed = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.emailConfirmIdIssuedTime = ret4;
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

        public async Task<(bool, List<UserEmail>)> SelectUserEmailByID(Guid id)
        {
            bool queryRes = false;
            List<UserEmail> ret = new List<UserEmail>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select email, emailConfirmId, isEmailConfirmed, emailConfirmIdIssuedTime, userId from user_emails where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserEmail>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserEmail();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out string ret0); retVar.email = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.emailConfirmId = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.isEmailConfirmed = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.emailConfirmIdIssuedTime = ret3;
                        rdr.ConvertDBData(4, out Guid ret4); retVar.userId = ret4;
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

        public async Task<(bool, UserEmail?)> SelectUserEmailByEmail(string email)
        {
            bool queryRes = false;
            UserEmail? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, emailConfirmId, isEmailConfirmed, emailConfirmIdIssuedTime, userId from user_emails where email = @01", con);
                    cmd.Parameters.AddWithValue("@01", email);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserEmail();
                        retVar.email = email;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.emailConfirmId = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.isEmailConfirmed = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.emailConfirmIdIssuedTime = ret3;
                        rdr.ConvertDBData(4, out Guid ret4); retVar.userId = ret4;
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

        public async Task<(bool, UserOAuthKey?)> SelectUserOAuthKey(Defines.OAuthProvider provider, string providerUid)
        {
            bool queryRes = false;
            UserOAuthKey? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, userId, userEmailId from user_oauth_keys where provider = @01 and providerUid = @02", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)provider);
                    cmd.Parameters.AddWithValue("@02", providerUid);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserOAuthKey();
                        retVar.provider = provider;
                        retVar.providerUid = providerUid;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.userId = ret1;
                        rdr.ConvertDBData(2, out Guid ret2); retVar.userEmailId = ret2;
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

        public async Task<(bool, UserOAuthKey?)> SelectUserOAuthKey(Guid userId, Guid userEmailId)
        {
            bool queryRes = false;
            UserOAuthKey? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, provider, providerUid from user_oauth_keys where userId = @01 and userEmailId = @02 limit 1", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    cmd.Parameters.AddWithValue("@02", userEmailId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserOAuthKey();
                        retVar.userId = userId;
                        retVar.userEmailId = userEmailId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.provider = (Defines.OAuthProvider)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.providerUid = ret2;
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

        public async Task<(bool, OAuthSignInUser?)> SelectOAuthSignInUser(Defines.OAuthProvider provider, string providerUid)
        {
            bool queryRes = false;
            OAuthSignInUser? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select b.id, uuid(), b.name, b.isDeleted, b.countFailedSignin, b.latestSignin, b.latestSignout, b.latestChangePW, c.id, c.email, c.emailConfirmId, c.isEmailConfirmed, a.id, a.provider from user_oauth_keys as a left join users as b on a.userId = b.id left join user_emails as c on a.userEmailId = c.id where a.provider = @01 and a.providerUid = @02", con);
                    cmd.Parameters.AddWithValue("@01", (Int64)provider);
                    cmd.Parameters.AddWithValue("@02", providerUid);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new OAuthSignInUser();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.userId = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.signinId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.name = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isDeleted = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.countFailedSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignin = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestSignout = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.latestChangePW = ret7;
                        rdr.ConvertDBData(8, out Guid ret8); retVar.emailId = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.email = ret9;
                        rdr.ConvertDBData(10, out string ret10); retVar.emailConfirmId = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.isEmailConfirmed = ret11;
                        rdr.ConvertDBData(12, out Guid ret12); retVar.oAuthKeyId = ret12;
                        rdr.ConvertDBData(13, out int ret13); retVar.provider = (Defines.OAuthProvider)ret13;
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

        public async Task<(bool, OAuthSignInUser?)> SelectOAuthSignInUser(Guid oAuthKeyId)
        {
            bool queryRes = false;
            OAuthSignInUser? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select b.id, uuid(), b.name, b.isDeleted, b.countFailedSignin, b.latestSignin, b.latestSignout, b.latestChangePW, c.id, c.email, c.emailConfirmId, c.isEmailConfirmed, a.provider from user_oauth_keys as a left join users as b on a.userId = b.id left join user_emails as c on a.userEmailId = c.id where a.id = @01", con);
                    cmd.Parameters.AddWithValue("@01", oAuthKeyId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new OAuthSignInUser();
                        retVar.oAuthKeyId = oAuthKeyId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.userId = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.signinId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.name = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isDeleted = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.countFailedSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignin = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestSignout = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.latestChangePW = ret7;
                        rdr.ConvertDBData(8, out Guid ret8); retVar.emailId = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.email = ret9;
                        rdr.ConvertDBData(10, out string ret10); retVar.emailConfirmId = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.isEmailConfirmed = ret11;
                        rdr.ConvertDBData(12, out int ret12); retVar.provider = (Defines.OAuthProvider)ret12;
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

        public async Task<(bool, OAuthSignInUser?)> SelectOAuthSignInUser(string id)
        {
            bool queryRes = false;
            OAuthSignInUser? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select b.id, uuid(), b.name, b.isDeleted, b.countFailedSignin, b.latestSignin, b.latestSignout. b.latestChangePW, c.id, c.email, c.emailConfirmId, c.isEmailConfirmed, a.id, a.provider from user_oauth_keys as a left join users as b on a.userId = b.id left join user_emails as c on a.userEmailId = c.id where a.id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new OAuthSignInUser();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.userId = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.signinId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.name = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isDeleted = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.countFailedSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignin = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestSignout = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.latestChangePW = ret7;
                        rdr.ConvertDBData(8, out Guid ret8); retVar.emailId = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.email = ret9;
                        rdr.ConvertDBData(10, out string ret10); retVar.emailConfirmId = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.isEmailConfirmed = ret11;
                        rdr.ConvertDBData(12, out Guid ret12); retVar.oAuthKeyId = ret12;
                        rdr.ConvertDBData(13, out int ret13); retVar.provider = (Defines.OAuthProvider)ret13;
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

        public async Task<(bool, OAuthSignInUser?)> SelectOAuthSignInUserByEmailConfirmID(string id)
        {
            bool queryRes = false;
            OAuthSignInUser? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select b.id, uuid(), b.name, b.isDeleted, b.countFailedSignin, b.latestSignin, b.latestSignout, b.latestChangePW, c.id, c.email, c.emailConfirmId, c.isEmailConfirmed, a.id, a.provider from user_oauth_keys as a left join users as b on a.userId = b.id left join user_emails as c on a.userEmailId = c.id where c.emailConfirmId = @01 and isEmailConfirmed = 0", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new OAuthSignInUser();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.userId = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.signinId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.name = ret2;
                        rdr.ConvertDBData(3, out bool ret3); retVar.isDeleted = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.countFailedSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignin = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestSignout = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.latestChangePW = ret7;
                        rdr.ConvertDBData(8, out Guid ret8); retVar.emailId = ret8;
                        rdr.ConvertDBData(9, out string ret9); retVar.email = ret9;
                        rdr.ConvertDBData(10, out string ret10); retVar.emailConfirmId = ret10;
                        rdr.ConvertDBData(11, out bool ret11); retVar.isEmailConfirmed = ret11;
                        rdr.ConvertDBData(12, out Guid ret12); retVar.oAuthKeyId = ret12;
                        rdr.ConvertDBData(13, out int ret13); retVar.provider = (Defines.OAuthProvider)ret13;
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

        public async Task<(bool, List<Role>)> SelectRoles()
        {
            bool queryRes = false;
            List<Role> ret = new List<Role>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select * from roles", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Role>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Role();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
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

        public async Task<(bool, Role?)> SelectRoleById(Guid id)
        {
            bool queryRes = false;
            Role? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select name from roles where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Role();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out string ret0); retVar.name = ret0;
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

        public async Task<(bool, Role?)> SelectRoleByName(string name)
        {
            bool queryRes = false;
            Role? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id from roles where name = @01", con);
                    cmd.Parameters.AddWithValue("@01", name);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Role();
                        retVar.name = name;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
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

        public async Task<(bool, List<UserRole>)> SelectUserRoles(Guid userId)
        {
            bool queryRes = false;
            List<UserRole> ret = new List<UserRole>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, roleId from user_roles where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserRole>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserRole();
                        retVar.userId = userId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.roleId = ret1;
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

        public async Task<(bool, List<UserRoleName>)> SelectUserRoleNames(Guid userId)
        {
            bool queryRes = false;
            List<UserRoleName> ret = new List<UserRoleName>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select a.id, a.roleId, b.name from user_roles as a left join roles as b on a.roleId = b.id where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserRoleName>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserRoleName();
                        retVar.userId = userId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out Guid ret1); retVar.roleId = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.name = ret2;
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

        public async Task<(bool, List<NGEL.Data.Models.UserInfo>)> SelectUserInfos()
        {
            bool queryRes = false;
            List<NGEL.Data.Models.UserInfo> ret = new List<NGEL.Data.Models.UserInfo>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, name, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime from users", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<NGEL.Data.Models.UserInfo>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.UserInfo();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.isDeleted = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.countFailedSignin = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.latestSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignout = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestChangePW = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.regTime = ret7;
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

        public async Task<(bool, List<UserInfoForManage>)> SelectUserInfosForManage()
        {
            bool queryRes = false;
            List<UserInfoForManage> ret = new List<UserInfoForManage>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, name, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime from users", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserInfoForManage>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserInfoForManage();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.name = ret1;
                        rdr.ConvertDBData(2, out bool ret2); retVar.isDeleted = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.countFailedSignin = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.latestSignin = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestSignout = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.latestChangePW = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.regTime = ret7;
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

        public async Task<(bool, NGEL.Data.Models.UserInfo?)> SelectUserInfo(Guid id)
        {
            bool queryRes = false;
            NGEL.Data.Models.UserInfo? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select name, isDeleted, countFailedSignin, latestSignin, latestSignout, latestChangePW, regTime from users where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.UserInfo();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out string ret0); retVar.name = ret0;
                        rdr.ConvertDBData(1, out bool ret1); retVar.isDeleted = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.countFailedSignin = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.latestSignin = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.latestSignout = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.latestChangePW = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.regTime = ret6;
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

        public async Task<(bool, UserSignin?)> SelectUserSignin(OAuthSignInUser argClass)
        {
            return await SelectUserSignin(argClass.userId);
        }

        public async Task<(bool, UserSignin?)> SelectUserSignin(Guid userId)
        {
            bool queryRes = false;
            UserSignin? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select signinId, connectionId, latestUpdate, latestActive from user_signins where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserSignin();
                        retVar.userId = userId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.signinId = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.connectionId = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.latestUpdate = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.latestActive = ret3;
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

        public async Task<(bool, UserSignin?)> SelectUserSigninBySigninId(OAuthSignInUser argClass)
        {
            return await SelectUserSigninBySigninId(argClass.signinId);
        }

        public async Task<(bool, UserSignin?)> SelectUserSigninBySigninId(Guid signinId)
        {
            bool queryRes = false;
            UserSignin? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select userId, connectionId, latestUpdate, latestActive from user_signins where signinId = @01", con);
                    cmd.Parameters.AddWithValue("@01", signinId);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserSignin();
                        retVar.signinId = signinId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.userId = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.connectionId = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.latestUpdate = ret2;
                        rdr.ConvertDBData(3, out DateTime ret3); retVar.latestActive = ret3;
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

        public async Task<(bool, List<VersionInfo>)> SelectVersionInfo()
        {
            bool queryRes = false;
            List<VersionInfo> ret = new List<VersionInfo>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select version, platform, serverState, clientState, cdnInfo from version_infos", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<VersionInfo>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new VersionInfo();
                        rdr.ConvertDBData(0, out string ret0); retVar.version = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.platform = (Defines.ServiceVersionManagementPlatform)ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.serverState = (Defines.ServiceVersionManagementServerState)ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.clientState = (Defines.ServiceVersionManagementClientState)ret3;
                        rdr.ConvertDBData(4, out string ret4); retVar.cdnInfo = ret4;
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

        public async Task<(bool, VersionInfo?)> SelectVersionInfo(VersionInfo argClass)
        {
            return await SelectVersionInfo(argClass.version, argClass.platform);
        }

        public async Task<(bool, VersionInfo?)> SelectVersionInfo(string version, Defines.ServiceVersionManagementPlatform platform)
        {
            bool queryRes = false;
            VersionInfo? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select serverState, clientState, cdnInfo from version_infos where version = @01 and platform = @02", con);
                    cmd.Parameters.AddWithValue("@01", version);
                    cmd.Parameters.AddWithValue("@02", (Int64)platform);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new VersionInfo();
                        retVar.version = version;
                        retVar.platform = platform;
                        rdr.ConvertDBData(0, out int ret0); retVar.serverState = (Defines.ServiceVersionManagementServerState)ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.clientState = (Defines.ServiceVersionManagementClientState)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.cdnInfo = ret2;
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

        public async Task<(bool, List<ChattingNotice>)> SelectChattingNotices()
        {
            bool queryRes = false;
            List<ChattingNotice> ret = new List<ChattingNotice>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, message, noticeType, visibleTime, visibleCount, noticeTime, regTime from chatting_notices", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<ChattingNotice>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new ChattingNotice();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.message = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.noticeType = (ENoticeType)ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.visibleTime = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.visibleCount = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.noticeTime = ret5;
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

        public async Task<(bool, ChattingNotice?)> SelectChattingNotice(Guid id)
        {
            bool queryRes = false;
            ChattingNotice? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select message, noticeType, visibleTime, visibleCount, noticeTime, regTime from chatting_notices where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new ChattingNotice();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out string ret0); retVar.message = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.noticeType = (ENoticeType)ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.visibleTime = ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.visibleCount = ret3;
                        rdr.ConvertDBData(4, out DateTime ret4); retVar.noticeTime = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.regTime = ret5;
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

        public async Task<(bool, List<ChattingNotice>)> SelectCurrentChattingNotices()
        {
            bool queryRes = false;
            List<ChattingNotice> ret = new List<ChattingNotice>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, message, noticeType, visibleTime, visibleCount, noticeTime, regTime from chatting_notices where date_format(noticeTime, '%Y-%m-%d %H:%i') = date_format(utc_timestamp, '%Y-%m-%d %H:%i')", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<ChattingNotice>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new ChattingNotice();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.message = ret1;
                        rdr.ConvertDBData(2, out int ret2); retVar.noticeType = (ENoticeType)ret2;
                        rdr.ConvertDBData(3, out int ret3); retVar.visibleTime = ret3;
                        rdr.ConvertDBData(4, out int ret4); retVar.visibleCount = ret4;
                        rdr.ConvertDBData(5, out DateTime ret5); retVar.noticeTime = ret5;
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

        public async Task<(bool, BlockContent?)> SelectBlockContent(BlockContent argClass)
        {
            return await SelectBlockContent(argClass.packetID);
        }

        public async Task<(bool, BlockContent?)> SelectBlockContent(string packetID)
        {
            bool queryRes = false;
            BlockContent? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select packetID from block_contents where packetID = @01", con);
                    cmd.Parameters.AddWithValue("@01", packetID);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new BlockContent();
                        rdr.ConvertDBData(0, out string ret0); retVar.packetID = ret0;
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

        public async Task<(bool, List<BlockContent>)> SelectBlockContents(BlockContent argClass)
        {
            return await SelectBlockContents();
        }

        public async Task<(bool, List<BlockContent>)> SelectBlockContents()
        {
            bool queryRes = false;
            List<BlockContent> ret = new List<BlockContent>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select packetID from block_contents", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<BlockContent>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new BlockContent();
                        rdr.ConvertDBData(0, out string ret0); retVar.packetID = ret0;
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

        public async Task<(bool, List<Firewall>)> SelectFirewalls()
        {
            bool queryRes = false;
            List<Firewall> ret = new List<Firewall>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, method, ipAddress, description from firewalls", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<Firewall>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new Firewall();
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.method = (Defines.FirewallMethod)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.ipAddress = ret2;
                        rdr.ConvertDBData(3, out string ret3); retVar.description = ret3;
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

        public async Task<(bool, Firewall?)> SelectFirewall(Firewall argClass)
        {
            return await SelectFirewall(argClass.id);
        }

        public async Task<(bool, Firewall?)> SelectFirewall(Guid id)
        {
            bool queryRes = false;
            Firewall? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select method, ipAddress, description from firewalls where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Firewall();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out int ret0); retVar.method = (Defines.FirewallMethod)ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.ipAddress = ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.description = ret2;
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

        public async Task<(bool, Firewall?)> SelectFirewallByIP(Firewall argClass)
        {
            return await SelectFirewallByIP(argClass.ipAddress);
        }

        public async Task<(bool, Firewall?)> SelectFirewallByIP(string ipAddress)
        {
            bool queryRes = false;
            Firewall? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, method, description from firewalls where ipAddress = @01", con);
                    cmd.Parameters.AddWithValue("@01", ipAddress);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new Firewall();
                        retVar.ipAddress = ipAddress;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.method = (Defines.FirewallMethod)ret1;
                        rdr.ConvertDBData(2, out string ret2); retVar.description = ret2;
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

        public async Task<(bool, List<CCU>)> SelectCCU(DateTime startTime, DateTime endTime)
        {
            bool queryRes = false;
            List<CCU> ret = new List<CCU>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, LobbyID, CreateAt, Total from CCU where CreateAt BETWEEN @01 and @02", con);
                    cmd.Parameters.AddWithValue("@01", (startTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    cmd.Parameters.AddWithValue("@02", (endTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<CCU>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new CCU();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.LobbyID = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.CreateAt = ret2;
                        rdr.ConvertDBData(3, out UInt64 ret3); retVar.Total = ret3;
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

        public async Task<(bool, CCU?)> SelectCCUAt(string LobbyID, string UTC)
        {
            bool queryRes = false;
            CCU? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select ID, LobbyID, CreateAt, Total from CCU where LobbyID = @01 and CreateAt = @02", con);
                    cmd.Parameters.AddWithValue("@01", LobbyID);
                    cmd.Parameters.AddWithValue("@02", UTC);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new CCU();
                        rdr.ConvertDBData(0, out UInt64 ret0); retVar.ID = ret0;
                        rdr.ConvertDBData(1, out DateTime ret1); retVar.CreateAt = ret1;
                        rdr.ConvertDBData(2, out UInt64 ret2); retVar.Total = ret2;
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

        public async Task<(bool, List<UserPassword>)> SelectUserPasswords(Guid userId)
        {
            bool queryRes = false;
            List<UserPassword> ret = new List<UserPassword>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, password, regTime from user_passwords where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserPassword>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserPassword();
                        retVar.userId = userId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out string ret1); retVar.password = ret1;
                        rdr.ConvertDBData(2, out DateTime ret2); retVar.regTime = ret2;
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

        public async Task<(bool, List<UserJob>)> SelectUserJobs(Guid userId)
        {
            bool queryRes = false;
            List<UserJob> ret = new List<UserJob>();
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select id, jobType, jobCount, succeededCount, failedCount, message, startTime, updateTime from user_jobs where userId = @01", con);
                    cmd.Parameters.AddWithValue("@01", userId);
                    rdr = await cmd.ExecuteReaderAsync();
                    ret = new List<UserJob>();
                    while(await rdr.ReadAsync())
                    {
                        var retVar = new UserJob();
                        retVar.userId = userId;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.id = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.jobType = (Defines.UserJobType)ret1;
                        rdr.ConvertDBData(2, out UInt64 ret2); retVar.jobCount = ret2;
                        rdr.ConvertDBData(3, out UInt64 ret3); retVar.succeededCount = ret3;
                        rdr.ConvertDBData(4, out UInt64 ret4); retVar.failedCount = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.message = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.startTime = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.updateTime = ret7;
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

        public async Task<(bool, UserJob?)> SelectUserJob(Guid id)
        {
            bool queryRes = false;
            UserJob? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select userId, jobType, jobCount, succeededCount, failedCount, message, startTime, updateTime from user_jobs where id = @01", con);
                    cmd.Parameters.AddWithValue("@01", id);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new UserJob();
                        retVar.id = id;
                        rdr.ConvertDBData(0, out Guid ret0); retVar.userId = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.jobType = (Defines.UserJobType)ret1;
                        rdr.ConvertDBData(2, out UInt64 ret2); retVar.jobCount = ret2;
                        rdr.ConvertDBData(3, out UInt64 ret3); retVar.succeededCount = ret3;
                        rdr.ConvertDBData(4, out UInt64 ret4); retVar.failedCount = ret4;
                        rdr.ConvertDBData(5, out string ret5); retVar.message = ret5;
                        rdr.ConvertDBData(6, out DateTime ret6); retVar.startTime = ret6;
                        rdr.ConvertDBData(7, out DateTime ret7); retVar.updateTime = ret7;
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

        public async Task<(bool, NGEL.Data.Models.Settings?)> SelectExpireLogSetting()
        {
            bool queryRes = false;
            NGEL.Data.Models.Settings? ret = null;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select `key`, `value` from `settings` where `key` = 'ExpireLog'", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        var retVar = new NGEL.Data.Models.Settings();
                        rdr.ConvertDBData(0, out string ret0); retVar.key = ret0;
                        rdr.ConvertDBData(1, out int ret1); retVar.value = ret1;
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

        public async Task<(bool, int)> SelectUserCount()
        {
            bool queryRes = false;
            int ret = 0;
            MySqlDataReader? rdr = null;
            MySqlConnection? con = null;

            try
            {
                using(con = new MySqlConnection(_conStr))
                {
                    await con.OpenAsync();
                    var cmd = new MySqlCommand($"select count(*) from users", con);
                    rdr = await cmd.ExecuteReaderAsync();
                    if (await rdr.ReadAsync())
                    {
                        rdr.ConvertDBData(0, out int retVar);
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
