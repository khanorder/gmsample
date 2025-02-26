using Microsoft.AspNetCore.Mvc;
using NGEL.Data.DB;
using NGEL.Data.Helpers;
using NGEL.Data.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;
using NGEL.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NGEL.Data.Models;
using NPOI.SS.Formula.Functions;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Principal;
using NPOI.SS.Formula.Eval;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Quartz.Util;

namespace GMServer.Services
{
    public class GMAuthentication
    {
        private readonly ILogger<GMAuthentication> _logger;
        private readonly DBHelper _dbHelper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAuthorizationService _authorizationService;
        private readonly CommonSettings _commonSettings;
        private readonly NavMenuSettings _navMenuSettings;
        private readonly JwtService _jwtService;
        private object lockObject = new object();

        public GMAuthentication(ILogger<GMAuthentication> logger, DBHelper dbHelper, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService, CommonSettings commonSettings, NavMenuSettings navMenuSettings, JwtService jwtService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _authorizationService = authorizationService;
            _commonSettings = commonSettings;
            _navMenuSettings = navMenuSettings;
            _jwtService = jwtService;
        }

        private async Task<Errors> SignInClaimsAsync(Guid clientId, OAuthSignInUser user)
        {
            try
            {
                if (Guid.Empty == clientId)
                    return Errors.SignInClaim_ClientIdRequired;

                var clientInfo = _commonSettings.clientInfo.Find(_ => _.id == clientId);
                if (null == clientInfo || string.IsNullOrWhiteSpace(clientInfo.host))
                    return Errors.SignInClaim_NotFoundClientId;

                if (_commonSettings.superEmails.Contains(user.email))
                {
                    Role? superRole = null;
                    var selectSuperRoleTask = await _dbHelper.GameManager.SelectRoleByName("최고 권한");
                    if (true == selectSuperRoleTask.Item1 && null != selectSuperRoleTask.Item2)
                    {
                        superRole = selectSuperRoleTask.Item2;
                    }
                    else
                    {
                        var insertSuperRoleTask = await _dbHelper.GameManagerWriteOnly.InsertRole("최고 권한");
                        if (false == insertSuperRoleTask.Item1 || Guid.Empty == insertSuperRoleTask.Item2)
                        {
                            return Errors.SignInClaim_FailedToInsertSuperRole;
                        }
                        else
                        {
                            superRole = new Role { id = insertSuperRoleTask.Item2, name = "최고 권한" };
                        }
                    }

                    var selectUserRolesTask = await _dbHelper.GameManager.SelectUserRoles(user.userId);
                    if (false == selectUserRolesTask.Item1 || 1 > selectUserRolesTask.Item2.Count || null == selectUserRolesTask.Item2.Find(_ => _.roleId == superRole.id))
                    {
                        var insertSuperUserRoleTask = await _dbHelper.GameManagerWriteOnly.InsertUserRole(user.userId, superRole.id);
                        if (false == insertSuperUserRoleTask.Item1 || Guid.Empty == insertSuperUserRoleTask.Item2)
                            return Errors.SignInClaim_FailedToAddSuperRoleUser;
                    }
                }

                var selectUserSigninTask = await _dbHelper.GameManager.SelectUserSignin(user);
                if (selectUserSigninTask.Item1 && null != selectUserSigninTask.Item2 && false == string.IsNullOrWhiteSpace(selectUserSigninTask.Item2.connectionId))
                {
                    var updateTimeSpan = DateTime.UtcNow.Subtract(selectUserSigninTask.Item2.latestUpdate);
                    if (1 >= updateTimeSpan.TotalMinutes)
                        return Errors.SignInClaim_AlreadySignIn;
                }

                var issueTokenTask = await _jwtService.IssueTokenkey(clientId, user);
                if (Errors.None != issueTokenTask.Item1)
                    return Errors.SignInClaim_FailedToIssueToken;

                return Errors.None;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.Unknown;
            }
        }

        public async Task<(Errors, OAuthSignInUser?)> SignInAsync(Guid clientId, Defines.OAuthProvider provider, string? oid, string? email, string? name, string? connectionId)
        {
            OAuthSignInUser? user = null;

            if (Guid.Empty == clientId)
                return (Errors.Authentication_ClientIdRequired, null);

            var clientInfo = _commonSettings.clientInfo.Find(_ => _.id == clientId);
            if (null == clientInfo || string.IsNullOrWhiteSpace(clientInfo.host))
                return (Errors.Authentication_NotFoundClientId, null);

            if (Defines.OAuthProvider.None == provider)
                return (Errors.Authentication_OAuthProviderRequired, null);

            if (string.IsNullOrWhiteSpace(oid))
                return (Errors.Authentication_OidRequired, null);

            if (string.IsNullOrWhiteSpace(email))
                return (Errors.Authentication_EmailRequired, null);

            if (string.IsNullOrWhiteSpace(name))
                return (Errors.Authentication_NameRequired, null);

            //if (string.IsNullOrWhiteSpace(connectionId))
            //    return (Errors.Authentication_ConnectionIdRequired, null);

            try
            {
                var selectUserOAuthKeyTask = await _dbHelper.GameManager.SelectUserOAuthKey(provider, oid);
                if (false == selectUserOAuthKeyTask.Item1 || null == selectUserOAuthKeyTask.Item2)
                {
                    if (false == await SignUpByOAuth(provider, oid, email, name))
                        return (Errors.Authentication_FailedSignUp, null);
                }

                var selectOAuthSigninUserTask = await _dbHelper.GameManager.SelectOAuthSignInUser(provider, oid);
                if (false == selectOAuthSigninUserTask.Item1 || null == selectOAuthSigninUserTask.Item2)
                    return (Errors.Authentication_NotFoundUser, null);

                user = selectOAuthSigninUserTask.Item2;
                if (user.isDeleted)
                    return (Errors.Authentication_DeletedUser, null);

                var selectUserSigninTask = await _dbHelper.GameManager.SelectUserSignin(user);
                if (selectUserSigninTask.Item1 && null != selectUserSigninTask.Item2 && false == string.IsNullOrWhiteSpace(selectUserSigninTask.Item2.connectionId))
                {
                    var updateTimeSpan = DateTime.UtcNow.Subtract(selectUserSigninTask.Item2.latestUpdate);
                    if (1 >= updateTimeSpan.TotalMinutes)
                        return (Errors.Authentication_AlreadySignIn, null);
                }

                switch (await SignInClaimsAsync(clientId, user))
                {
                    case Errors.None:
                        break;

                    default:
                        return (Errors.Authentication_FailedSignIn, null);
                }

                if (false == selectUserSigninTask.Item1 || null == selectUserSigninTask.Item2)
                {
                    var insertUserSigninTask = await _dbHelper.GameManagerWriteOnly.InsertUserSignin(user.userId, user.signinId, connectionId ?? "");
                    if (false == insertUserSigninTask)
                        return (Errors.Authentication_FailedToInsertSignInTime, null);
                }

                var updateSigninUserTask = await _dbHelper.GameManagerWriteOnly.SignInUser(user.userId, user.signinId, connectionId ?? "");
                if (false == updateSigninUserTask.Item1 || null == updateSigninUserTask.Item2)
                    return (Errors.Authentication_FailedToUpdateSignInTime, null);

                user.latestSignin = (DateTime)updateSigninUserTask.Item2;
                user.updatedTime = (DateTime)updateSigninUserTask.Item2;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.Authentication_UnknownError, null);
            }

            return (Errors.None, user);
        }

        public async Task<(Errors, OAuthSignInUser?, string? token)> UpdateSignInAsync(string? tokenString, string? connectionId)
        {
            if (string.IsNullOrWhiteSpace(tokenString))
                return (Errors.UpdateSignIn_TokenNull, null, null);

            if (string.IsNullOrWhiteSpace(connectionId))
                return (Errors.UpdateSignIn_ConnectionIdRequired, null, null);

            var deserializeTokenResult = _jwtService.DeserializeToken(tokenString, out var token);
            if (Errors.None != deserializeTokenResult)
                return (deserializeTokenResult, null, null);

            if (null == token)
                return (Errors.UpdateSignIn_TokenNull, null, "");

            if (0 < DateTime.UtcNow.CompareTo(token.ValidTo))
                return (Errors.UpdateSignIn_TokenExpired, null, "");

            var signinId = Guid.Empty;
            if (false == token.Payload.TryGetValue("signinId", out var signinIdClaim))
                return (Errors.UpdateSignIn_NotFoundSigninId, null, "");

            var signinIdString = signinIdClaim.ToString();
            if (string.IsNullOrWhiteSpace(signinIdString))
                return (Errors.UpdateSignIn_NotFoundSigninId, null, "");

            try
            {
                signinId = Guid.Parse(signinIdString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.UpdateSignIn_SigninIdMustGuid, null, "");
            }

            if (Guid.Empty == signinId)
                return (Errors.UpdateSignIn_SigninIdMustGuid, null, "");

            var keyId = Guid.Empty;
            if (false == token.Payload.TryGetValue("keyId", out var keyIdClaim))
                return (Errors.UpdateSignIn_NotFoundSigninId, null, "");

            var keyIdString = keyIdClaim.ToString();
            if (string.IsNullOrWhiteSpace(keyIdString))
                return (Errors.UpdateSignIn_NotFoundOAuthKeyId, null, "");

            try
            {
                keyId = Guid.Parse(keyIdString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.UpdateSignIn_OAuthKeyIdMustGuid, null, "");
            }

            if (Guid.Empty == keyId)
                return (Errors.UpdateSignIn_OAuthKeyIdMustGuid, null, "");

            try
            {
                OAuthSignInUser? user = null;

                var selectOAuthSigninUserTask = await _dbHelper.GameManager.SelectOAuthSignInUser(keyId);
                if (false == selectOAuthSigninUserTask.Item1 || null == selectOAuthSigninUserTask.Item2)
                    return (Errors.UpdateSignIn_NotFoundUser, null, "");

                user = selectOAuthSigninUserTask.Item2;
                if (user.isDeleted)
                    return (Errors.UpdateSignIn_DeletedUser, null, "");

                var selectUserSigninTask = await _dbHelper.GameManager.SelectUserSignin(user);
                if (false == selectUserSigninTask.Item1 || null == selectUserSigninTask.Item2)
                    return (Errors.UpdateSignIn_NotSignedInUser, null, "");

                if (Guid.Empty == selectUserSigninTask.Item2.signinId)
                {
                    return (Errors.UpdateSignIn_NotSignedInUser, null, "");
                }
                else if (signinId != selectUserSigninTask.Item2.signinId)
                {
                    // 다른 유저가 로그인 중인 상태와 로그인 했던 상태를 구분할까?
                    return (Errors.UpdateSignIn_AlreadySignIn, null, "");
                }
                else
                {
                    var diffUpdateTime = DateTime.UtcNow.Subtract(selectUserSigninTask.Item2.latestActive);
                    if (_commonSettings.authPersistence < diffUpdateTime.TotalMinutes)
                    {
                        await _dbHelper.GameManagerWriteOnly.UpdateSignOutUser(user);
                        return (Errors.UpdateSignIn_AuthExpired, null, "");
                    }
                }

                var updateSigninUserTask = await _dbHelper.GameManagerWriteOnly.UpdateSignInUser(user.userId, connectionId);
                if (false == updateSigninUserTask.Item1 || null == updateSigninUserTask.Item2)
                    return (Errors.UpdateSignIn_FailedToUpdateSignInTime, null, "");

                user.signinId = signinId;
                user.updatedTime = (DateTime)updateSigninUserTask.Item2;

                var selectUserAuthorizationTask = await _jwtService.GetUserAuthorizationWithRole(user);
                user.menus = selectUserAuthorizationTask.Item2;

                var validTimeSpan = token.ValidTo.Subtract(DateTime.UtcNow);

                if (_webHostEnvironment.IsDevelopment())
                    _logger.LogInformation($"validTo: {token.ValidTo.ToString("yyyy-MM-dd HH:mm:ss")}, timeSpan: {validTimeSpan.TotalMilliseconds}");

                if (validTimeSpan.TotalMilliseconds < 120000)
                {
                    if (false == token.Payload.TryGetValue("aud", out var aud))
                        return (Errors.UpdateSignIn_AudienceRequired, null, "");

                    var clientInfo = _commonSettings.clientInfo.Find(_ => _.host == aud.ToString());

                    if (null == clientInfo)
                        return (Errors.UpdateSignIn_NotFoundClientInfo, null, "");

                    var issueTokenTask = await _jwtService.IssueTokenkey(clientInfo.id, user);
                    if (Errors.None != issueTokenTask.Item1 || string.IsNullOrWhiteSpace(issueTokenTask.Item2))
                        return (Errors.UpdateSignIn_FailedToIssueToken, null, "");

                    return (Errors.UpdateSignIn_ReissueToken, user, issueTokenTask.Item2);
                }

                return (Errors.None, user, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.UpdateSignIn_UnknownError, null, "");
            }
        }

        public async Task<bool> SignOutAsync()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (null == httpContext)
                {
                    _logger.LogError($"httpContext is null.");
                    return false;
                }

                ClaimsPrincipal? user = null;
                IIdentity? identity = null;
                List<Claim>? claims = null;

                if (null != httpContext.User && null != httpContext.User.Identity && null != httpContext.User.Claims)
                {
                    user = httpContext.User;
                    identity = httpContext.User.Identity;
                    claims = httpContext.User.Claims.ToList();
                }

                if (null != identity && false == identity.IsAuthenticated)
                {
                    _logger.LogInformation("do not have authentication.");
                    return false;
                }

                if (null == claims || 1 > claims.Count())
                {
                    _logger.LogInformation("claims are empty.");
                    await httpContext.SignOutAsync();
                    return true;
                }

                var signinIdClaim = claims.Find(_ => _.Type == "signinId");
                if (null == signinIdClaim || string.IsNullOrWhiteSpace(signinIdClaim.Value))
                {
                    _logger.LogInformation("not found signinId claim.");
                    await httpContext.SignOutAsync();
                    return true;
                }

                var signinId = Guid.Empty;

                try
                {
                    signinId = Guid.Parse(signinIdClaim.Value);
                }
                catch (Exception parseEx)
                {
                    _logger.LogError(parseEx.Message);
                    _logger.LogError(parseEx.StackTrace);
                }

                if (Guid.Empty == signinId)
                {
                    _logger.LogInformation("signinId is empty.");
                    await httpContext.SignOutAsync();
                    return true;
                }
                var userIdClaim = claims.Find(_ => _.Type == "userId");
                if (null == userIdClaim || string.IsNullOrWhiteSpace(userIdClaim.Value))
                {
                    _logger.LogInformation("not found userId claim.");
                    await httpContext.SignOutAsync();
                    return true;
                }

                var userId = Guid.Empty;

                try
                {
                    userId = Guid.Parse(userIdClaim.Value);
                }
                catch (Exception parseEx)
                {
                    _logger.LogError(parseEx.Message);
                    _logger.LogError(parseEx.StackTrace);
                }

                if (Guid.Empty == userId)
                {
                    _logger.LogInformation("signinId is empty.");
                    await httpContext.SignOutAsync();
                    return true;
                }

                switch (await SignOutAsync(signinId, userId))
                {
                    case Errors.None:
                        return true;

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        public async Task<Errors> SignOutAsync(Guid signinId, Guid userId)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (null == httpContext)
                    return Errors.SignOut_HTTPContextNull;

                var selectUserSigninTask = await _dbHelper.GameManager.SelectUserSignin(userId);

                if (false == selectUserSigninTask.Item1 || null == selectUserSigninTask.Item2 || Guid.Empty == selectUserSigninTask.Item2.signinId || signinId != selectUserSigninTask.Item2.signinId)
                    return Errors.SignOut_NotFoundAuth;

                await _dbHelper.GameManagerWriteOnly.SignOutUser(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.SignOut_UnknownError;
            }

            return Errors.None;
        }

        public async Task<bool> SignUpByOAuth(Defines.OAuthProvider provider, string oid, string email, string name)
        {
            if (Defines.OAuthProvider.None == provider)
            {
                _logger.LogError("provider is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(oid))
            {
                _logger.LogError("oid is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogError("email is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("name is required.");
                return false;
            }

            var userID = Guid.Empty;
            var emailID = Guid.Empty;
            var oAuthKeyID = Guid.Empty;

            try
            {
                var insertUserTask = await _dbHelper.GameManagerWriteOnly.InsertUserWithoutPW(name);
                if (false == insertUserTask.Item1 && Guid.Empty == insertUserTask.Item2)
                {
                    _logger.LogError("failed to insert user data.");
                    return false;
                }

                userID = insertUserTask.Item2;

                var insertUserEmailTask = await _dbHelper.GameManagerWriteOnly.InsertUserEmail(email, userID);
                if (false == insertUserEmailTask.Item1 && Guid.Empty == insertUserEmailTask.Item2)
                {
                    _logger.LogError("failed to insert user email data.");
                    if (await _dbHelper.GameManagerWriteOnly.DeleteUser(userID))
                    {
                        _logger.LogError("succeed to delete inserted user.");
                    }
                    else
                    {
                        _logger.LogError($"failed to delete inserted user.(id:{userID})");
                    }
                    return false;
                }

                emailID = insertUserEmailTask.Item2;

                var insertUserOAuthKeyTask = await _dbHelper.GameManagerWriteOnly.InsertUserOAuthKey(provider, oid, userID, emailID);
                if (false == insertUserOAuthKeyTask.Item1 && Guid.Empty == insertUserOAuthKeyTask.Item2)
                {
                    if (await _dbHelper.GameManagerWriteOnly.DeleteUser(userID))
                    {
                        _logger.LogError("succeed to delete inserted user.");
                    }
                    else
                    {
                        _logger.LogError($"failed to delete inserted user.(id:{userID})");
                    }

                    if (await _dbHelper.GameManagerWriteOnly.DeleteUserEmail(emailID))
                    {
                        _logger.LogError("succeed to delete inserted user.");
                    }
                    else
                    {
                        _logger.LogError($"failed to delete inserted user.(id:{userID})");
                    }

                    _logger.LogError("failed to insert user oauth key data.");
                    return false;
                }

                oAuthKeyID = insertUserOAuthKeyTask.Item2;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            return Guid.Empty != oAuthKeyID;
        }

        public async Task<(bool, Guid, Guid)> SignUpBySMILEGATE(string email, string name)
        {
            var result = (false, Guid.Empty, Guid.Empty);
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogError("email is required.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("name is required.");
                return result;
            }

            var userID = Guid.Empty;
            var emailID = Guid.Empty;
            var oAuthKeyID = Guid.Empty;
            var emailConfirmId = Guid.NewGuid();

            try
            {
                var insertUserTask = await _dbHelper.GameManagerWriteOnly.InsertUserWithoutPW(name);
                if (false == insertUserTask.Item1 && Guid.Empty == insertUserTask.Item2)
                {
                    _logger.LogError("failed to insert user data.");
                    return result;
                }

                userID = insertUserTask.Item2;

                var insertUserEmailTask = await _dbHelper.GameManagerWriteOnly.InsertUserEmailWithConfirm(email, emailConfirmId.ToString(), userID);
                if (false == insertUserEmailTask.Item1 && Guid.Empty == insertUserEmailTask.Item2)
                {
                    _logger.LogError("failed to insert user email data.");
                    if (await _dbHelper.GameManagerWriteOnly.DeleteUser(userID))
                    {
                        _logger.LogError("succeed to delete inserted user.");
                    }
                    else
                    {
                        _logger.LogError($"failed to delete inserted user.(id:{userID})");
                    }
                    return result;
                }

                emailID = insertUserEmailTask.Item2;

                var insertUserOAuthKeyTask = await _dbHelper.GameManagerWriteOnly.InsertUserOAuthKey(Defines.OAuthProvider.CustomEmail, Guid.NewGuid().ToString(), userID, emailID);
                if (false == insertUserOAuthKeyTask.Item1 && Guid.Empty == insertUserOAuthKeyTask.Item2)
                {
                    if (await _dbHelper.GameManagerWriteOnly.DeleteUser(userID))
                    {
                        _logger.LogError("succeed to delete inserted user.");
                    }
                    else
                    {
                        _logger.LogError($"failed to delete inserted user.(id:{userID})");
                    }

                    if (await _dbHelper.GameManagerWriteOnly.DeleteUserEmail(emailID))
                    {
                        _logger.LogError("succeed to delete inserted user.");
                    }
                    else
                    {
                        _logger.LogError($"failed to delete inserted user.(id:{userID})");
                    }

                    _logger.LogError("failed to insert user oauth key data.");
                    return result;
                }

                oAuthKeyID = insertUserOAuthKeyTask.Item2;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return result;
            }

            return (Guid.Empty != oAuthKeyID, userID, emailConfirmId);
        }

        public async Task<(Errors, OAuthSignInUser?)> GetSignedInUser(string? tokenString)
        {
            if (string.IsNullOrWhiteSpace(tokenString))
                return (Errors.GetSignedInUser_TokenEmpty, null);

            var deserializeTokenResult = _jwtService.DeserializeToken(tokenString, out var token);
            if (Errors.None != deserializeTokenResult)
                return (deserializeTokenResult, null);

            if (null == token)
                return (Errors.GetSignedInUser_TokenNull, null);

            if (0 < DateTime.UtcNow.CompareTo(token.ValidTo))
                return (Errors.GetSignedInUser_TokenExpired, null);

            if (false == token.Payload.TryGetValue("signinId", out var signinIdClaim))
                return (Errors.GetSignedInUser_NotFoundSigninId, null);

            var signinIdString = signinIdClaim.ToString();
            if (string.IsNullOrWhiteSpace(signinIdString))
                return (Errors.GetSignedInUser_NotFoundSigninId, null);

            if (false == token.Payload.TryGetValue("keyId", out var keyIdClaim))
                return (Errors.GetSignedInUser_NotFoundSigninId, null);

            var keyIdString = keyIdClaim.ToString();
            if (string.IsNullOrWhiteSpace(keyIdString))
                return (Errors.GetSignedInUser_NotFoundOAuthKeyId, null);

            return await GetSignedInUser(signinIdString, keyIdString);
        }

        public async Task<(Errors, OAuthSignInUser?)> GetSignedInUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (null == httpContext)
                return (Errors.GetSignedInUser_HTTPContextNull, null);

            if (null == httpContext.User || null == httpContext.User.Identity || false == httpContext.User.Identity.IsAuthenticated || null == httpContext.User.Claims || 1 > httpContext.User.Claims.Count())
                return (Errors.GetSignedInUser_NotFoundAuth, null);

            var signinIdClaim = httpContext.GetClaim("signinId");

            if (string.IsNullOrWhiteSpace(signinIdClaim))
                return (Errors.GetSignedInUser_NotFoundSigninId, null);

            var keyIdClaim = httpContext.GetClaim("keyId");

            if (string.IsNullOrWhiteSpace(keyIdClaim))
                return (Errors.GetSignedInUser_NotFoundOAuthKeyId, null);

            return await GetSignedInUser(signinIdClaim, keyIdClaim);
        }

        public async Task<(Errors, OAuthSignInUser?)> GetSignedInUser(string? signinIdClaim, string? keyIdClaim)
        {
            if (string.IsNullOrWhiteSpace(signinIdClaim))
                return (Errors.GetSignedInUser_NotFoundSigninId, null);

            if (string.IsNullOrWhiteSpace(keyIdClaim))
                return (Errors.GetSignedInUser_NotFoundOAuthKeyId, null);

            var signinId = Guid.Empty;

            try
            {
                signinId = Guid.Parse(signinIdClaim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.GetSignedInUser_SigninIdMustGuid, null);
            }

            if (Guid.Empty == signinId)
                return (Errors.GetSignedInUser_SigninIdMustGuid, null);

            var keyId = Guid.Empty;

            try
            {
                keyId = Guid.Parse(keyIdClaim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.GetSignedInUser_OAuthKeyIdMustGuid, null);
            }

            if (Guid.Empty == keyId)
                return (Errors.GetSignedInUser_OAuthKeyIdMustGuid, null);

            OAuthSignInUser? user = null;

            try
            {
                var selectOAuthSigninUserTask = await _dbHelper.GameManager.SelectOAuthSignInUser(keyId);
                if (false == selectOAuthSigninUserTask.Item1 || null == selectOAuthSigninUserTask.Item2)
                    return (Errors.GetSignedInUser_NotFoundUser, null);

                user = selectOAuthSigninUserTask.Item2;
                if (user.isDeleted)
                    return (Errors.GetSignedInUser_DeletedUser, null);

                var selectUserSigninTask = await _dbHelper.GameManager.SelectUserSigninBySigninId(signinId);
                if (false == selectUserSigninTask.Item1 || null == selectUserSigninTask.Item2)
                    return (Errors.GetSignedInUser_NotFoundUserSignin, null);

                var diffUpdateTime = DateTime.UtcNow.Subtract(selectUserSigninTask.Item2.latestActive);
                if (_commonSettings.authPersistence < diffUpdateTime.TotalMinutes)
                {
                    await _dbHelper.GameManagerWriteOnly.UpdateSignOutUser(user);
                    return (Errors.GetSignedInUser_AuthExpired, null);
                }

                var selectUserAuthorizationTask = await _jwtService.GetUserAuthorizationWithRole(user);
                user.menus = selectUserAuthorizationTask.Item2;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.GetSignedInUser_UnknownError, null);
            }

            return (Errors.None, user);
        }
    }
}
