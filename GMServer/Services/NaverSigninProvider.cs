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
    public class NaverSigninProvider
    {
        private readonly ILogger<NaverSigninProvider> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;

        public readonly string _loginAPIURL;
        public readonly string _profileAPIURL;
        public readonly string _clientID;
        public readonly string _clientSecret;

        public NaverSigninProvider(ILogger<NaverSigninProvider> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _loginAPIURL = "https://nid.naver.com/oauth2.0/token";
            _profileAPIURL = "https://openapi.naver.com/v1/nid/me";
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
                    redirectURI = $"{clientHost}/oauth/callback/naver";
                }

                var host = $"{_loginAPIURL}?grant_type=authorization_code&client_id={_clientID}&client_secret={_clientSecret}&redirectURI={redirectURI}&code={code}&state={stateCode}";

                HttpClient client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Get, host);
                req.Headers.Add("X-Naver-Client-Id", _clientID);
                req.Headers.Add("X-Naver-Client-Secret", _clientSecret);

                var res = await client.SendAsync(req);
                if (HttpStatusCode.OK != res.StatusCode)
                {
                    // API 요청 처리 실패 처리
                    _logger.LogError($"GetAccessToken - HttpStatusCode : {res.StatusCode}");
                    return "";
                }

                string resBody = await res.Content.ReadAsStringAsync();
                _logger.LogInformation(resBody);
                var resObj = System.Text.Json.JsonSerializer.Deserialize<NaverLoginTokenResult>(resBody);

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

        public async Task<(Errors, NaverLoginUser?)> GetUser(string access_token)
        {
            if (string.IsNullOrWhiteSpace(access_token))
                return (Errors.None, null);

            HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, _profileAPIURL);
            req.Headers.Add("Authorization", $"Bearer {access_token}");

            var res = await client.SendAsync(req);
            if (HttpStatusCode.OK != res.StatusCode)
            {
                // API 요청 처리 실패 처리
                _logger.LogError($"GetAccessToken - HttpStatusCode : {res.StatusCode}");
                return (Errors.NaverGetUser_FailedToRequestProfileAPI, null);
            }

            string resBody = await res.Content.ReadAsStringAsync();
            _logger.LogInformation(resBody);
            NaverLoginUser? user = null;

            try
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<NaverLoginUserResult>(resBody);
                if (null != result && "success" == result.message && null != result.response)
                    user = result.response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.NaverGetUser_NotFoundUser, null);
            }

            return (Errors.None, user);
        }

        public async Task<(Errors, NaverLoginUser?)> GetUser(string code, string stateCode, string clientHost)
        {
            var accessToken = await GetToken(code, stateCode, clientHost);

            return await GetUser(accessToken);
        }
    }
}
