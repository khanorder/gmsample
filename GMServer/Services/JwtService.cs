using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GMServer.Models;
using Microsoft.IdentityModel.Tokens;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Models;
using NGEL.Data.Helpers;
using NGEL.Data.Settings;
using Newtonsoft.Json;
using Quartz.Util;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Newtonsoft.Json.Linq;

namespace GMServer.Services
{
    public class JwtService
    {
        private readonly ILogger<JwtService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CommonSettings _commonSettings;
        private readonly NavMenuSettings _navMenuSettings;
        private readonly JwtSettings _jwtSettings;
        private readonly DBHelper _dbHelper;

        public JwtService(ILogger<JwtService> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, NavMenuSettings navMenuSettings, CommonSettings commonSettings, JwtSettings jwtSettings, DBHelper dbHelper)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _navMenuSettings = navMenuSettings;
            _commonSettings = commonSettings;
            _jwtSettings = jwtSettings;
            _dbHelper = dbHelper;
        }

        private List<Claim> GetClaims(OAuthSignInUser user, List<UserRoleName> roles, out Guid Id)
        {
            Id = Guid.NewGuid();
            var claims = new List<Claim>
            {
                new Claim("signinId", user.signinId.ToString()),
                new Claim("userId", user.userId.ToString()),
                new Claim("emailId", user.emailId.ToString()),
                new Claim("provider", ((int)user.provider).ToString()),
                new Claim("keyId", user.oAuthKeyId.ToString()),
                new Claim("updateTime", user.updatedTime.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(_jwtSettings.expireMinutes).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
            };
            if (0 < roles.Count)
            {
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.name));
            }
            return claims;
        }

        public async Task<(List<UserRoleName>, List<UserMenu>)> GetUserAuthorizationWithRole(OAuthSignInUser user)
        {
            return await GetUserAuthorizationWithRole(user.userId, user.email, user.provider);
        }

        public async Task<(List<UserRoleName>, List<UserMenu>)> GetUserAuthorizationWithRole(Guid userId, string email, Defines.OAuthProvider provider)
        {
            var userRoleNames = new List<UserRoleName>();
            var userMenus = new List<UserMenu>();
            try
            {
                var selectUserRoleNamesTask = await _dbHelper.GameManager.SelectUserRoleNames(userId);
                if (selectUserRoleNamesTask.Item1 && 0 < selectUserRoleNamesTask.Item2.Count)
                    userRoleNames = selectUserRoleNamesTask.Item2;

                if (false == _commonSettings.isService)
                    userRoleNames.Add(new UserRoleName { id = Guid.NewGuid(), name = "최고 권한", userId = userId });

                userMenus.AddRange(_navMenuSettings.navMenus.GetAllowedUserMenu(userRoleNames));
                //if (Defines.OAuthProvider.CustomEmail == provider)
                //    userMenus.Add(new UserMenu { name = "비밀번호 변경", children = new List<UserMenu>(), path = "/Password" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            return (userRoleNames, userMenus);
        }

        public async Task<(Errors, string?)> IssueTokenkey(Guid clientId, OAuthSignInUser user)
        {
            try
            {
                if (null == user)
                {
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"{nameof(user)} is null. failed to issue authentication token.");
                    return (Errors.IssueTokenkey_UserRequired, "");
                }

                if (Guid.Empty == clientId)
                {
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"audience client id is empty. failed to issue authentication token.");
                    return (Errors.IssueTokenkey_ClientIdRequired, "");
                }

                var clientInfo = _commonSettings.clientInfo.Find(_ => _.id == clientId);
                if (null == clientInfo || string.IsNullOrWhiteSpace(clientInfo.host))
                {
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"audience host is null. failed to issue authentication token.");
                    return (Errors.IssueTokenkey_ClientIdRequired, "");
                }

                var selectUserAuthorizationTask = await GetUserAuthorizationWithRole(user);
                user.menus = selectUserAuthorizationTask.Item2;

                // Get secret key
                var key = Encoding.ASCII.GetBytes(_jwtSettings.issuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.Now.AddMinutes(_jwtSettings.expireMinutes);
                var JWToken = new JwtSecurityToken(
                    issuer: _jwtSettings.validIssuer,
                    audience: clientInfo.host,
                    claims: GetClaims(user, selectUserAuthorizationTask.Item1, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                if (string.IsNullOrWhiteSpace(token))
                {
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"failed to issue authentication token.");
                    return (Errors.IssueTokenkey_FailedToIssueToken, "");
                }
                user.token = token;
                return (Errors.None, token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} failed to issue authentication token.");
                _logger.LogError(ex.StackTrace);
            }
            return (Errors.IssueTokenkey_UnknownError, "");
        }

        public Errors DeserializeToken(string? tokenString, out JwtSecurityToken? token)
        {
            token = null;

            if (string.IsNullOrWhiteSpace(tokenString))
                return Errors.JWTDeserializeToken_TokenStringRequired;

            var handler = new JwtSecurityTokenHandler();

            try
            {
                token = handler.ReadJwtToken(tokenString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.JWTDeserializeToken_FailedToReadToken;
            }

            if (null == token)
                return Errors.JWTDeserializeToken_TokenNull;

            if (false == token.Payload.TryGetValue("aud", out var aud))
                return Errors.JWTDeserializeToken_AudienceRequired;

            if (0 < DateTime.UtcNow.CompareTo(token.ValidTo))
                return Errors.JWTDeserializeToken_TokenExpired;

            if (string.IsNullOrWhiteSpace(_jwtSettings.issuerSigningKey))
                return Errors.JWTDeserializeToken_TokenSigningKeyRequired;

            try
            {
                var key = Encoding.ASCII.GetBytes(_jwtSettings.issuerSigningKey);
                var principal = handler.ValidateToken(tokenString, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.validIssuer,
                    ValidAudience = aud.ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out var validatedToken);

                if (null == principal || null == validatedToken)
                    return Errors.JWTDeserializeToken_ValidTokenNull;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.JWTDeserializeToken_FailedToValidateToken;
            }

            return Errors.None;
        }

        public Errors GetPrincipal(string? tokenString, out ClaimsPrincipal? principal)
        {
            principal = null;
            JwtSecurityToken? token = null;

            if (string.IsNullOrWhiteSpace(tokenString))
                return Errors.JWTGetPrincipal_TokenStringRequired;

            var handler = new JwtSecurityTokenHandler();

            try
            {
                token = handler.ReadJwtToken(tokenString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.JWTGetPrincipal_FailedToReadToken;
            }

            if (null == token)
                return Errors.JWTGetPrincipal_TokenNull;

            if (false == token.Payload.TryGetValue("aud", out var aud))
                return Errors.JWTGetPrincipal_AudienceRequired;

            if (0 < DateTime.UtcNow.CompareTo(token.ValidTo))
                return Errors.JWTGetPrincipal_TokenExpired;

            if (string.IsNullOrWhiteSpace(_jwtSettings.issuerSigningKey))
                return Errors.JWTGetPrincipal_TokenSigningKeyRequired;

            try
            {
                var key = Encoding.ASCII.GetBytes(_jwtSettings.issuerSigningKey);
                principal = handler.ValidateToken(tokenString, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.validIssuer,
                    ValidAudience = aud.ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out var validatedToken);

                if (null == principal || null == validatedToken)
                    return Errors.JWTGetPrincipal_ValidTokenNull;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.JWTGetPrincipal_FailedToValidateToken;
            }

            return Errors.None;
        }
    }
}
