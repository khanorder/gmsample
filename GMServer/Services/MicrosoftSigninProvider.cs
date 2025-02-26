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

namespace GMServer.Services
{
    public class MicrosoftSigninProvider
    {
        private readonly ILogger<MicrosoftSigninProvider> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;

        public readonly string _microsoftLoginAPIHost;
        public readonly string _clientID;
        public readonly string _clientSecret;

        public MicrosoftSigninProvider(ILogger<MicrosoftSigninProvider> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _microsoftLoginAPIHost = "https://login.microsoftonline.com";
            _clientID = "";
            _clientSecret = "";
        }

        public async Task<string> GetToken(string code, string clientHost)
        {
            if (string.IsNullOrWhiteSpace(code))
                return "";

            var host = $"{_microsoftLoginAPIHost}/common/oauth2/v2.0/token";
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

                    redirectURI = $"{clientHost}/oauth/callback/microsoft";
                }

                var body = $"client_id={_clientID}"
                    + "&scope=https%3A%2F%2Fgraph.microsoft.com%2Fuser.read"
                    + $"&code={code}"
                    + $"&redirect_uri={redirectURI}"
                    + "&grant_type=authorization_code"
                    //+ "&code_verifier=ThisIsntRandomButItNeedsToBe43CharactersLong"
                    + $"&client_secret={_clientSecret}";

                HttpClient client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, host)
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
                var resObj = System.Text.Json.JsonSerializer.Deserialize<MicrosoftLoginTokenResult>(resBody);

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

        public JwtSecurityToken? DeserializeToken(string? tokenString)
        {
            if (string.IsNullOrWhiteSpace(tokenString))
                return null;

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(tokenString);

            if (null == jwtSecurityToken)
                return null;

            return jwtSecurityToken;
        }
    }
}
