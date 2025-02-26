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
using System.Net.Http.Headers;

namespace GMServer.Services
{
    public class LineNotifyProvider
    {
        private readonly ILogger<LineNotifyProvider> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly RequestDataService _requestDataService;
        private readonly CommonSettings _commonSettings;

        public readonly string _authAPIURL;
        public readonly string _tokenAPIURL;
        public readonly string _notifyAPIURL;
        public readonly string _clientID;
        public readonly string _clientSecret;
        public readonly string _access_token_me;

        public LineNotifyProvider(ILogger<LineNotifyProvider> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment, RequestDataService requestDataService , CommonSettings commonSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _requestDataService = requestDataService;
            _commonSettings = commonSettings;
            _authAPIURL = "https://notify-bot.line.me/oauth/authorize";
            _tokenAPIURL = "https://notify-bot.line.me/oauth/token";
            _notifyAPIURL = "https://notify-api.line.me/api/notify";
            _clientID = "";
            _clientSecret = "";
            _access_token_me = "";
        }

        private async Task<LineNotifyAuthResult?> GetAuth(string stateCode)
        {
            var redirectURI = "";
            if (string.IsNullOrWhiteSpace(stateCode))
                stateCode = Guid.NewGuid().ToBase62() ?? "";

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    if (null == httpContext)
                    {
                        _logger.LogError($"httpContext is null.");
                        return null;
                    }
                    redirectURI = $"{_requestDataService.scheme}://{_requestDataService.host}{(_hostEnvironment.IsDevelopment() ? $":${_requestDataService.port}" : "")}/notify/auth";
                }

                var reqURL = $"{_authAPIURL}?response_type=code&client_id={_clientID}&redirect_uri={redirectURI}&scope=notify&state={stateCode}&response_mode=form_post";

                HttpClient client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Get, reqURL);

                var res = await client.SendAsync(req);
                if (HttpStatusCode.OK != res.StatusCode)
                {
                    // API 요청 처리 실패 처리
                    _logger.LogError($"GetAccessToken - HttpStatusCode : {res.StatusCode}");
                    return null;
                }

                string resBody = await res.Content.ReadAsStringAsync();
                var resObj = System.Text.Json.JsonSerializer.Deserialize<LineNotifyAuthResult>(resBody);

                if (null == resObj || string.IsNullOrWhiteSpace(resObj.code) || string.IsNullOrWhiteSpace(resObj.state))
                    return null;

                return resObj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return null;
            }
        }

        private async Task<string> GetToken(string stateCode)
        {
            if (string.IsNullOrWhiteSpace(stateCode))
                stateCode = Guid.NewGuid().ToBase62() ?? "";

            var auth = await GetAuth(stateCode);

            if (null == auth || string.IsNullOrWhiteSpace(auth.code))
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
                    redirectURI = $"{_requestDataService.scheme}://{_requestDataService.host}{(_hostEnvironment.IsDevelopment() ? $":${_requestDataService.port}" : "")}/notify/token";
                }

                var body = $"grant_type=authorization_code"
                    + $"&redirect_uri={redirectURI}"
                    + $"&client_id={_clientID}"
                    + $"&client_secret={_clientSecret}"
                    + $"&code={auth.code}"
                    + $"&state={stateCode}";

                HttpClient client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, _tokenAPIURL)
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
                var resObj = System.Text.Json.JsonSerializer.Deserialize<LineNotifyTokenResult>(resBody);

                if (null == resObj || string.IsNullOrWhiteSpace(resObj.access_token))
                    return "";

                return resObj.access_token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return "";
            }
        }

        public async Task<(Errors, LineNotifyResult?)> Notify(string message)
        {
            return await Notify(message, "");
        }

        public async Task<(Errors, LineNotifyResult?)> Notify(string message, string access_token)
        {
            if (string.IsNullOrWhiteSpace(access_token))
                access_token = _access_token_me;

            var body = $"message={message}";

            HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, _notifyAPIURL)
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
            _logger.LogInformation(resBody);

            try
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<LineNotifyResult>(resBody);
                if (null != result || "ok" == result.message)
                    return (Errors.None, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return (Errors.KakaoGetUser_NotFoundUser, null);
            }

            return (Errors.None, null);
        }

    }
}
