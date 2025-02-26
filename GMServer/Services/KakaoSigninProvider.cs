using Microsoft.AspNetCore.Mvc;
using GMServer.Models;
using NGEL.Data.DB;
using NGEL.Data.Helpers;
using NGEL.Data.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System.Diagnostics;
using System;
using NGEL.Data;
using NPOI.SS.Formula.Functions;

namespace GMServer.Services
{
    public class KakaoSigninProvider
    {
        private readonly ILogger<KakaoSigninProvider> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;

        public readonly string _loginAPIURL;
        public readonly string _profileAPIURL;
        public readonly string _clientID;
        public readonly string _clientSecret;

        public KakaoSigninProvider(ILogger<KakaoSigninProvider> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _loginAPIURL = "https://kauth.kakao.com/oauth/token";
            _profileAPIURL = "https://kapi.kakao.com/v2/user/me";
            _clientID = "";
            _clientSecret = "";
        }

        private async Task<string> GetToken(string code, string stateCode, string clientHost)
        {
            if (string.IsNullOrWhiteSpace(code))
                return "";

            var redirectURI = "";
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    if (null == httpContext)
                    {
                        _logger.LogError($"httpContext is null.");
                        return "";
                    }
                    redirectURI = $"{clientHost}/oauth/callback/kakao";
                }

                var host = $"{_loginAPIURL}?&redirectURI={redirectURI}&code={code}&state={stateCode}";

                var body = $"grant_type=authorization_code"
                    + $"&client_id={_clientID}"
                    + $"&client_secret={_clientSecret}"
                    + $"&redirect_uri={redirectURI}"
                    + $"&code={code}"
                    + $"&state={stateCode}";

                HttpClient client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, _loginAPIURL)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"),
                };

                var res = await client.SendAsync(req);
                if (HttpStatusCode.OK != res.StatusCode)
                {
                    // API 요청 처리 실패 처리
                    _logger.LogError($"GetAccessToken - HttpStatusCode : {res.StatusCode}");
                    return "";
                }

                string resBody = await res.Content.ReadAsStringAsync();
                var resObj = System.Text.Json.JsonSerializer.Deserialize<KakaoLoginTokenResult>(resBody);

                if (null == resObj || string.IsNullOrWhiteSpace(resObj.access_token))
                    return "";

                return resObj.access_token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                _logger.LogError($"&redirect_uri={redirectURI}");
                return "";
            }
        }

        public async Task<(Errors, KakaoLoginUser?)> GetUser(string access_token)
        {
            if (string.IsNullOrWhiteSpace(access_token))
                return (Errors.None, null);

            var body = "{ secure_resource: true, property_keys: [ 'email', 'profile_nickname', 'profile_image', 'story_permalink' ] }";

            HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, _profileAPIURL)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"),
            };
            req.Headers.Add("Authorization", $"Bearer {access_token}");

            var res = await client.SendAsync(req);
            if (HttpStatusCode.OK != res.StatusCode)
            {
                // API 요청 처리 실패 처리
                _logger.LogError($"GetAccessToken - HttpStatusCode : {res.StatusCode}");
                return (Errors.KakaoGetUser_FailedToRequestProfileAPI, null);
            }

            string resBody = await res.Content.ReadAsStringAsync();
            KakaoLoginUser? user = null;

            try
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<KakaoLoginUserResult>(resBody);
                if (null != result && 0 < result.id && null != result.kakao_account && false == string.IsNullOrWhiteSpace(result.kakao_account.email) && null != result.properties && false == string.IsNullOrWhiteSpace(result.properties.nickname))
                {
                    user = new KakaoLoginUser
                    {
                        id = result.id,
                        email = result.kakao_account.email,
                        name = result.properties.nickname,
                        nickname = result.properties.nickname,
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.KakaoGetUser_NotFoundUser, null);
            }

            return (Errors.None, user);
        }

        public async Task<(Errors, KakaoLoginUser?)> GetUser(string code, string stateCode, string clientHost)
        {
            var accessToken = await GetToken(code, stateCode, clientHost);

            return await GetUser(accessToken);
        }

    }
}
