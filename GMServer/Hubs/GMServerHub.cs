using GMServer.Services;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Vars;
using NGEL.Data.Helpers;
using Newtonsoft.Json;
using NGEL.Data.Models;
using System.Text.RegularExpressions;
using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.DirectoryServices.Protocols;
using System.Net;
using NPOI.SS.Formula.Functions;
using System.Text;
using NGEL.Data.Tables;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Org.BouncyCastle.Bcpg;

namespace GMServer.Hubs
{
    public class GMServerHub : AbstractGMServerHub
    {
        protected override ILogger<GMServerHub> _logger { get; }
        protected override GMAuthentication _gmAuthentication { get; }
        protected override DBHelper _dbHelper { get; }
        protected override RequestDataService _requestDataService { get; }
        protected override GMLogService _gmLogService { get; }
        protected override IHttpContextAccessor _httpContextAccessor { get; }
        protected override CommonSettings _commonSettings { get; }
        protected override JwtService _jwtService { get; }
        protected override ServerStateService _serverStateService { get; }
        protected override DataTableService _dataTableService { get; }
        protected override IWebHostEnvironment _webHostEnvironment { get; }
        protected override AutoGMServerHubSend _hubSend { get; }
        private readonly MicrosoftSigninProvider _microsoftSigninProvider;
        private readonly StompService _stompService;
        private readonly GMUserService _gmUserService;

        public GMServerHub(
            ILogger<GMServerHub> logger,
            GMAuthentication gmAuthentication,
            DBHelper dbHelper,
            RequestDataService requestDataService,
            GMLogService gmLogService,
            IHttpContextAccessor httpContextAccessor,
            CommonSettings commonSettings,
            JwtService jwtService,
            ServerStateService serverStateService,
            DataTableService dataTableService,
            IWebHostEnvironment webHostEnvironment,
            AutoGMServerHubSend hubSend,
            MicrosoftSigninProvider microsoftSigninProvider,
            StompService stompService,
            GMUserService gmUserService
        )
        {
            _logger = logger;
            _gmAuthentication = gmAuthentication;
            _dbHelper = dbHelper;
            _requestDataService = requestDataService;
            _gmLogService = gmLogService;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _jwtService = jwtService;
            _serverStateService = serverStateService;
            _dataTableService = dataTableService;
            _webHostEnvironment = webHostEnvironment;
            _hubSend = hubSend;
            _microsoftSigninProvider = microsoftSigninProvider;
            _stompService = stompService;
            _gmUserService = gmUserService;
        }

        public override async Task OnConnectedAsync()
        {
            _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _dbHelper.GameManagerWriteOnly.UpdateDisconnectedUser(Context.ConnectionId);
            if (Errors.None == _gmUserService.GetUser(Context.ConnectionId, out var user) && null != user)
            {
                try
                {
                    var notifyChatting = new ChattingMessage
                    {
                        id = Guid.NewGuid(),
                        messageType = Defines.ChattingMessageType.Notice,
                        message = $"{user.name}({user.email})님이 채팅 서버와 연결을 종료했습니다."
                    };

                    await _stompService.PubChattingMessage(notifyChatting);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                }
            }
            _gmUserService.RemoveConnectedUser(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        protected override async Task OnCheckAuthentication(PCheckAuthenticationReq packet)
        {
            var updateSigninTask = await _gmAuthentication.UpdateSignInAsync(packet.token, Context.ConnectionId);
            var ack = new PCheckAuthenticationAck { error = updateSigninTask.Item1 };

            switch (updateSigninTask.Item1)
            {
                case Errors.None:
                    if (null != updateSigninTask.Item2)
                    {
                        ack.user = updateSigninTask.Item2.CloneParents();
                        ack.dataTable = _dataTableService.ExportDataTable;

                        if (_webHostEnvironment.IsDevelopment())
                            _logger.LogInformation($"user: {updateSigninTask.Item2.name} - the user_signin was updated.");

                        _gmUserService.UpdateConnectedUser(Context.ConnectionId, updateSigninTask.Item2);
                    }
                    else
                    {
                        ack.error = Errors.CheckAuthentication_NotFoundUser;
                        _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    }
                    break;

                case Errors.UpdateSignIn_ReissueToken:
                    if (null != updateSigninTask.Item2 && false == string.IsNullOrWhiteSpace(updateSigninTask.Item3))
                    {
                        ack.error = Errors.CheckAuthentication_ReissueToken;
                        ack.user = updateSigninTask.Item2.CloneParents();
                        ack.dataTable = _dataTableService.ExportDataTable;

                        try
                        {
                            ack.token = updateSigninTask.Item3;
                            if (_webHostEnvironment.IsDevelopment())
                                _logger.LogInformation($"user: {updateSigninTask.Item2.name} - the token was reissued.");

                            _gmUserService.UpdateConnectedUser(Context.ConnectionId, updateSigninTask.Item2);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            ack.error = Errors.CheckAuthentication_ReissueToken;
                            ack.token = "";
                            ack.user = null;

                            _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                        }
                    }
                    else
                    {
                        ack.error = Errors.CheckAuthentication_NotFoundUser;
                        _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    }
                    break;

                default:
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogInformation(updateSigninTask.Item1.ToString());

                    _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    break;
            }

            await _hubSend.CheckAuthenticationAck(Context.ConnectionId, ack);
        }

        protected override async Task OnCheckConnection(PCheckConnectionReq packet)
        {
            var updateSigninTask = await _gmAuthentication.UpdateSignInAsync(packet.token, Context.ConnectionId);
            var ack = new PCheckConnectionAck { error = updateSigninTask.Item1 };

            switch (updateSigninTask.Item1)
            {
                case Errors.None:
                    if (null != updateSigninTask.Item2)
                    {
                        if (_webHostEnvironment.IsDevelopment())
                            _logger.LogInformation($"user: {updateSigninTask.Item2.name} - the user_signin was updated.");

                        if (Defines.OAuthProvider.CustomEmail == updateSigninTask.Item2.provider && 7776000 <= DateTime.UtcNow.Subtract(updateSigninTask.Item2.latestChangePW).TotalSeconds)
                        {
                            ack.error = Errors.CheckConnection_PasswordExpired;
                            _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                        }
                        else
                        {
                            _gmUserService.UpdateConnectedUser(Context.ConnectionId, updateSigninTask.Item2);
                        }
                    }
                    else
                    {
                        ack.error = Errors.CheckConnection_NotFoundUser;
                        _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    }
                    break;

                case Errors.UpdateSignIn_ReissueToken:
                    if (null != updateSigninTask.Item2 && false == string.IsNullOrWhiteSpace(updateSigninTask.Item3))
                    {
                        ack.error = Errors.CheckConnection_ReissueToken;
                        try
                        {
                            ack.token = updateSigninTask.Item3;
                            if (_webHostEnvironment.IsDevelopment())
                                _logger.LogInformation($"user: {updateSigninTask.Item2.name} - the token was reissued.");

                            _gmUserService.UpdateConnectedUser(Context.ConnectionId, updateSigninTask.Item2);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            ack.error = Errors.CheckConnection_FailedToReissueToken;
                            ack.token = "";
                            _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                        }
                    }
                    else
                    {
                        ack.error = Errors.CheckConnection_NotFoundUser;
                        _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    }
                    break;

                case Errors.UpdateSignIn_AuthExpired:
                    ack.error = Errors.CheckConnection_AuthExpired;
                    string userIdString = "";
                    if (Errors.None == _jwtService.DeserializeToken(packet.token, out var token) && null != token)
                    {
                        if (token.Payload.TryGetValue("userId", out var userIdClaim) && null != userIdClaim)
                            userIdString = userIdClaim.ToString() ?? "";
                    }

                    _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    _ = _gmLogService.InsertHubReqLog(userIdString, "OnCheckConnection", packet.reqPathName ?? "", ack.error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    break;

                default:
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogInformation(updateSigninTask.Item1.ToString());

                    _gmUserService.UpdateConnectedUser(Context.ConnectionId, null);
                    break;
            }

            await _hubSend.CheckConnectionAck(Context.ConnectionId, ack);
        }

        protected override async Task OnSignIn(PSignInReq packet)
        {
            var ack = new PSignInAck();

            if (string.IsNullOrWhiteSpace(packet.clientId))
            {
                ack.error = Errors.SignIn_ClientIdRequired;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.code))
            {
                ack.error = Errors.SignIn_OAuthCodeRequired;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            var clientId = Guid.Empty;
            try
            {
                clientId = Guid.Parse(packet.clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (Guid.Empty == clientId)
            {
                ack.error = Errors.SignIn_ClientIdMustGuid;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            var client = _commonSettings.clientInfo.Find(_ => _.id == clientId);
            if (null == client)
            {
                ack.error = Errors.SignIn_NotAllowedClientId;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            var token = await _microsoftSigninProvider.GetToken(packet.code, client.host);

            if (string.IsNullOrWhiteSpace(token))
            {
                ack.error = Errors.SignIn_TokenRequired;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            var jwt = _microsoftSigninProvider.DeserializeToken(token);
            if (null == jwt)
            {
                ack.error = Errors.SignIn_FailedToParseToken;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            if (false == jwt.Payload.TryGetValue("unique_name", out var emailValue))
            {
                ack.error = Errors.SignIn_EmailRequired;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            if (false == Regex.IsMatch((string)emailValue, @"^[^\@]+\@(ngelgames|smilegate)\.com$"))
            {
                ack.error = Errors.SignIn_MustUseCompanyMail;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            if (false == jwt.Payload.TryGetValue("name", out var nameValue))
            {
                ack.error = Errors.SignIn_NameRequired;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            if (false == jwt.Payload.TryGetValue("oid", out var oidValue))
            {
                ack.error = Errors.SignIn_OidRequired;
                await _hubSend.SignInAck(Context.ConnectionId, ack);
                return;
            }

            var signInTask = await _gmAuthentication.SignInAsync(clientId, Defines.OAuthProvider.Microsoft, oidValue.ToString(), emailValue.ToString(), nameValue.ToString(), Context.ConnectionId);

            switch (signInTask.Item1)
            {
                case Errors.None:
                    if (null != signInTask.Item2)
                    {
                        ack.user = signInTask.Item2.CloneParents();
                        ack.token = signInTask.Item2.token;

                        await _hubSend.DataTableAck(Context.ConnectionId, new PDataTableAck { dataTable = _dataTableService.ExportDataTable });

                        _ = _gmLogService.InsertHubReqLog(signInTask.Item2.userId.ToString(), "GMServerOnSignIn", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
                    }
                    else
                    {
                        ack.error = Errors.SignIn_FailedSignIn;
                    }
                    break;

                default:
                    _ = _gmLogService.InsertHubReqLog("", "GMServerOnSignIn", (packet.reqPathName ?? ""), signInTask.Item1.ToString(), _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);

                    if (Errors.Authentication_AlreadySignIn == signInTask.Item1)
                    {
                        ack.error = Errors.SignIn_AlreadySignIn;
                    }
                    else
                    {
                        ack.error = Errors.SignIn_FailedSignIn;
                    }
                    break;
            }

            await _hubSend.SignInAck(Context.ConnectionId, ack);
        }

        protected override async Task OnSignInLDAP(PSignInLDAPReq packet)
        {
            var ldapServer = "192.168.109.50";
            var ldapPort = 389;

            var ack = new PSignInLDAPAck();

            if (string.IsNullOrWhiteSpace(packet.clientId))
            {
                ack.error = Errors.SignInLDAP_ClientIdRequired;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            var clientId = Guid.Empty;

            try
            {
                clientId = Guid.Parse(packet.clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                ack.error = Errors.SignInLDAP_ClientIdMustGuid;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (Guid.Empty == clientId)
            {
                ack.error = Errors.SignInLDAP_ClientIdMustGuid;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            var clientInfo = _commonSettings.clientInfo.Find(_ => _.id == clientId);
            if (null == clientInfo)
            {
                ack.error = Errors.SignInLDAP_NotAllowedClientId;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.email))
            {
                ack.error = Errors.SignInLDAP_EmailRequired;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.password))
            {
                ack.error = Errors.SignInLDAP_PasswordRequired;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            var email = "";
            var name = "";
            var oauthUid = "";

            try
            {
                LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier(ldapServer, ldapPort, true, false);
                LdapConnection lc = new LdapConnection(ldi);
                lc.AuthType = AuthType.Basic;
                lc.SessionOptions.ProtocolVersion = 3;
                String ldapUser = String.Format("uid={0},cn=users,dc=ngelgames,dc=com", Regex.Replace(packet.email, @"@ngelgames\.com$", ""));
                lc.Credential = new NetworkCredential(ldapUser, packet.password);
                lc.Bind();

                SearchRequest searchRequest = new SearchRequest("dc=ngelgames,dc=com", String.Format("(&(objectClass=person)(uid={0}))", Regex.Replace(packet.email, @"@ngelgames\.com$", "")), SearchScope.Subtree, null);
                var searchResponse = (SearchResponse)lc.SendRequest(searchRequest);

                if (searchResponse.Entries.Count > 0)
                {
                    var item = searchResponse.Entries[0];

                    var entryuuid = item.Attributes["entryuuid"];
                    var firstName = item.Attributes["givenName"];
                    var lastName = item.Attributes["sn"];
                    var receivedEmail = item.Attributes["mail"];
                    //_logger.LogError("item: " + JsonConvert.SerializeObject(item, Formatting.Indented));
                    //_logger.LogError("entryuuid: " + JsonConvert.SerializeObject(entryuuid));
                    //_logger.LogError("firstName: " + JsonConvert.SerializeObject(firstName));
                    //_logger.LogError("lastName: " + JsonConvert.SerializeObject(lastName));
                    //_logger.LogError("receivedEmail: " + JsonConvert.SerializeObject(receivedEmail));

                    var FirstName = firstName.Count > 0 ? firstName[0] as string : "";
                    var LastName = lastName.Count > 0 ? lastName[0] as string : "";
                    name = LastName + FirstName;
                    email = receivedEmail.Count > 0 ? receivedEmail[0] as string : Regex.Replace(packet.email, @"@ngelgames\.com$", "") + "@ngelgames.com";
                    oauthUid = entryuuid.Count > 0 ? entryuuid[0] as string : null;
                }

                lc.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                ack.error = Errors.SignInLDAP_DoNotMatchedData;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                ack.error = Errors.SignInLDAP_OAuthEmailNull;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (false == Regex.IsMatch(email, @"^[^\@]+\@ngelgames\.com$"))
            {
                ack.error = Errors.SignInLDAP_MustUseCompanyMail;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ack.error = Errors.SignInLDAP_OAuthNameNull;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(oauthUid))
            {
                ack.error = Errors.SignInLDAP_OAuthUidNull;
                await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
                return;
            }

            var signInTask = await _gmAuthentication.SignInAsync(clientId, Defines.OAuthProvider.Microsoft, oauthUid, email, name, Context.ConnectionId);

            switch (signInTask.Item1)
            {
                case Errors.None:
                    if (null != signInTask.Item2)
                    {
                        ack.user = signInTask.Item2.CloneParents();
                        ack.token = signInTask.Item2.token;

                        await _hubSend.DataTableAck(Context.ConnectionId, new PDataTableAck { dataTable = _dataTableService.ExportDataTable });

                        packet.password = "";
                        _ = _gmLogService.InsertHubReqLog(signInTask.Item2.userId.ToString(), "GMServerOnSignInLDAP", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
                    }
                    else
                    {
                        ack.error = Errors.SignInLDAP_FailedSignIn;
                    }
                    break;

                default:
                    packet.password = "";
                    _ = _gmLogService.InsertHubReqLog("", "GMServerOnSignInLDAP", (packet.reqPathName ?? ""), signInTask.Item1.ToString(), _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);

                    if (Errors.Authentication_AlreadySignIn == signInTask.Item1)
                    {
                        ack.error = Errors.SignInLDAP_AlreadySignIn;
                    }
                    else
                    {
                        ack.error = Errors.SignInLDAP_FailedSignIn;
                    }
                    break;
            }

            await _hubSend.SignInLDAPAck(Context.ConnectionId, ack);
        }

        protected override async Task OnSignInEmail(PSignInEmailReq packet)
        {
            var ack = new PSignInEmailAck();

            if (string.IsNullOrWhiteSpace(packet.clientId))
            {
                ack.error = Errors.SignInEmail_ClientIdRequired;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            var clientId = Guid.Empty;

            try
            {
                clientId = Guid.Parse(packet.clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                ack.error = Errors.SignInEmail_ClientIdMustGuid;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (Guid.Empty == clientId)
            {
                ack.error = Errors.SignInEmail_ClientIdMustGuid;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            var clientInfo = _commonSettings.clientInfo.Find(_ => _.id == clientId);
            if (null == clientInfo)
            {
                ack.error = Errors.SignInEmail_NotAllowedClientId;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.email))
            {
                ack.error = Errors.SignInEmail_EmailRequired;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.password))
            {
                ack.error = Errors.SignInEmail_PasswordRequired;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            var email = packet.email;
            var name = "";
            var oauthUid = "";
            if (false == email.ToLower().EndsWith("@smilegate.com"))
                email += "@smilegate.com";

            var selectUserEmailTask = await _dbHelper.GameManager.SelectUserEmailByEmail(email);
            if (false == selectUserEmailTask.Item1 || null == selectUserEmailTask.Item2)
            {
                ack.error = Errors.SignInEmail_DoNotMatchedData;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (false == string.IsNullOrWhiteSpace(selectUserEmailTask.Item2.emailConfirmId) && false == selectUserEmailTask.Item2.isEmailConfirmed)
            {
                ack.error = Errors.SignInEmail_ConfirmRequired;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            var selectUserTask = await _dbHelper.GameManager.SelectUserByID(selectUserEmailTask.Item2.userId);
            if (false == selectUserTask.Item1 || null == selectUserTask.Item2)
            {
                ack.error = Errors.SignInEmail_DoNotMatchedData;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (4 < selectUserTask.Item2.countFailedSignin)
            {
                ack.error = Errors.SignInEmail_TooManyFailedSignin;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (false == BCrypt.Net.BCrypt.Verify(packet.password, Encoding.UTF8.GetString(Convert.FromBase64String(selectUserTask.Item2.password))))
            {
                ack.countFailedSignin = selectUserTask.Item2.countFailedSignin;

                if (5 > selectUserTask.Item2.countFailedSignin)
                {
                    await _dbHelper.GameManagerWriteOnly.AddCountFailedSigninUserInfo(selectUserTask.Item2.id);
                    ack.countFailedSignin++;
                    packet.password = "";
                    _ = _gmLogService.InsertHubReqLog(selectUserTask.Item2.id.ToString(), "GMServerOnSignInEmail", (packet.reqPathName ?? ""), "SignInEmail_DoNotMatchedData", _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                }

                ack.error = Errors.SignInEmail_DoNotMatchedData;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }
            else
            {
                await _dbHelper.GameManagerWriteOnly.ClearCountFailedSigninUserInfo(selectUserTask.Item2.id);
            }

            name = selectUserTask.Item2.name;

            var selectOAuthTask = await _dbHelper.GameManager.SelectUserOAuthKey(selectUserEmailTask.Item2.userId, selectUserEmailTask.Item2.id);
            if (false == selectOAuthTask.Item1 || null == selectOAuthTask.Item2)
            {
                ack.error = Errors.SignInEmail_DoNotMatchedData;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            oauthUid = selectOAuthTask.Item2.providerUid;

            if (string.IsNullOrWhiteSpace(email))
            {
                ack.error = Errors.SignInEmail_OAuthEmailNull;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (false == Regex.IsMatch(email, @"^[^\@]+\@(ngelgames|smilegate)\.com$"))
            {
                ack.error = Errors.SignInEmail_MustUseCompanyMail;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ack.error = Errors.SignInEmail_OAuthNameNull;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(oauthUid))
            {
                ack.error = Errors.SignInEmail_OAuthUidNull;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            if (7776000 <= DateTime.UtcNow.Subtract(selectUserTask.Item2.latestSignin).TotalSeconds)
            {
                ack.error = Errors.SignInEmail_TooLongLatestSignin;
                await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
                return;
            }

            var signInTask = await _gmAuthentication.SignInAsync(clientId, Defines.OAuthProvider.CustomEmail, oauthUid, email, name, Context.ConnectionId);

            switch (signInTask.Item1)
            {
                case Errors.None:
                    if (null != signInTask.Item2)
                    {
                        ack.user = signInTask.Item2.CloneParents();
                        ack.token = signInTask.Item2.token;

                        packet.password = "";
                        _ = _gmLogService.InsertHubReqLog(signInTask.Item2.userId.ToString(), "GMServerOnSignInEmail", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
                    }
                    else
                    {
                        ack.error = Errors.SignInEmail_FailedSignIn;
                    }
                    break;

                default:
                    packet.password = "";
                    _ = _gmLogService.InsertHubReqLog("", "GMServerOnSignInEmail", (packet.reqPathName ?? ""), signInTask.Item1.ToString(), _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);

                    if (Errors.Authentication_AlreadySignIn == signInTask.Item1)
                    {
                        ack.error = Errors.SignInEmail_AlreadySignIn;
                    }
                    else
                    {
                        if (5 > selectUserTask.Item2.countFailedSignin)
                            await _dbHelper.GameManagerWriteOnly.AddCountFailedSigninUserInfo(selectUserTask.Item2.id);

                        ack.error = Errors.SignInEmail_FailedSignIn;
                    }
                    break;
            }

            await _hubSend.SignInEmailAck(Context.ConnectionId, ack);
        }

        protected override async Task OnSignOut(PSignOutReq packet, string userIdString)
        {
            var ack = new PSignOutAck();

            if (Errors.None == _jwtService.DeserializeToken(packet.token, out var token))
            {
                if (null == token)
                {
                    ack.error = Errors.SignOut_TokenNull;
                    await _hubSend.SignOutAck(Context.ConnectionId, ack);
                    return;
                }

                if (false == token.Payload.TryGetValue("userId", out var userIdClaim))
                {
                    ack.error = Errors.SignOut_NotFoundUserId;
                    await _hubSend.SignOutAck(Context.ConnectionId, ack);
                    return;
                }

                if (null == userIdClaim)
                {
                    ack.error = Errors.SignOut_NotFoundUserId;
                    await _hubSend.SignOutAck(Context.ConnectionId, ack);
                    return;
                }

                var userIdClaimString = userIdClaim.ToString();

                if (string.IsNullOrEmpty(userIdClaimString))
                {
                    ack.error = Errors.SignOut_NotFoundUserId;
                    await _hubSend.SignOutAck(Context.ConnectionId, ack);
                    return;
                }

                var userId = Guid.Empty;
                try
                {
                    userId = Guid.Parse(userIdClaimString);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                }

                await _dbHelper.GameManagerWriteOnly.SignOutUser(userId);
            }

            await _hubSend.SignOutAck(Context.ConnectionId, ack);
        }

        protected override async Task OnSendGameMail(PSendGameMailReq packet, string userIdString)
        {
            var ack = new PSendGameMailResultAck();

            if (null == packet.userUIDs || 1 > packet.userUIDs.Count)
            {
                ack.error = Errors.SendGameMail_UIDRequired;
                await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                return;
            }

            for (var i = 0; i < packet.userUIDs.Count; i++)
            {
                var uid = packet.userUIDs[i];
                var selectUserAccountTask = await _dbHelper.Game.SelectUserAccount(uid);
                if (false == selectUserAccountTask.Item1 || null == selectUserAccountTask.Item2)
                {
                    ack.error = Errors.SendGameMail_NotFoundUserAccount;
                    ack.errorIndex = i;
                    await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                    return;
                }
            }

            if (null == packet.mailInput)
            {
                ack.error = Errors.SendGameMail_EmptyToSendMail;
                await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.mailInput.title))
            {
                ack.error = Errors.SendGameMail_TitleRequired;
                await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.mailInput.message))
            {
                ack.error = Errors.SendGameMail_MessageRequired;
                await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (0 < packet.mailInput.items.Count)
            {
                for (var i = 0; i < packet.mailInput.items.Count; i++)
                {
                    var item = packet.mailInput.items[i];
                    if (1 > item.id)
                    {
                        ack.error = Errors.SendGameMail_ItemIDRequired;
                        ack.errorIndex = i;
                        await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                        return;
                    }

                    if (1 > item.count)
                    {
                        ack.error = Errors.SendGameMail_ItemCountMustUpperZero;
                        ack.errorIndex = i;
                        await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                        return;
                    }
                }
            }

            var userId = Guid.Empty;

            try
            {
                userId = Guid.Parse(userIdString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (Guid.Empty == userId)
            {
                ack.error = Errors.SendGameMail_NotSuitableUserId;
                await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var userJob = new UserJob
            {
                id = Guid.NewGuid(),
                userId = userId,
                jobType = Defines.UserJobType.SendImmdGameMail,
                jobCount = (UInt64)packet.userUIDs.Count
            };

            if (Errors.None != _gmUserService.AddUserJobCount(Context.ConnectionId, userJob, false))
            {
                ack.error = Errors.SendGameMail_FailedToInsertJob;
                await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                return;
            }

            for (var i = 0; i < packet.userUIDs.Count; i++)
            {
                var uid = packet.userUIDs[i];
                try
                {
                    var toSendMail = new Mail();
                    toSendMail.MailType = packet.mailInput.mailtype;
                    toSendMail.UID = uid;
                    toSendMail.IsBM = packet.mailInput.isBM;
                    toSendMail.Title = packet.mailInput.title;
                    toSendMail.Message = packet.mailInput.message;
                    toSendMail.ExpireAt = packet.mailInput.expireAt;
                    if (0 < packet.mailInput.items.Count)
                    {
                        for (var j = 0; j < packet.mailInput.items.Count; j++)
                        {
                            var item = packet.mailInput.items[j];
                            toSendMail.RewardList.Add(new MailReward { RewardType = item.rewardType, RewardID = item.id, RewardCount = item.count });
                        }
                    }
                    await _stompService.PubSendMail(toSendMail, Context.ConnectionId, userJob.id, userIdString, packet.reqPathName ?? "", _requestDataService.remoteAddress);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                    ack.error = Errors.SendGameMail_FailedToSendMail;
                    await _hubSend.SendGameMailResultAck(Context.ConnectionId, ack);
                    return;
                }
            }

            var jobAck = new PSendUserJobAck { userJob = userJob };
            await _hubSend.SendUserJobAck(Context.ConnectionId, jobAck);
        }

        protected override async Task OnInsertEventMail(PInsertEventMailReq packet, string userIdString)
        {
            var ack = new PInsertEventMailResultAck();

            if (null == packet.eventMail)
            {
                ack.error = Errors.InsertEventMail_ParametersEmpty;
                await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.eventMail.Title))
            {
                ack.error = Errors.InsertEventMail_TitleRequired;
                await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.eventMail.Message))
            {
                ack.error = Errors.InsertEventMail_MessageRequired;
                await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (0 < packet.eventMail.RewardList.Count)
            {
                for (var i = 0; i < packet.eventMail.RewardList.Count; i++)
                {
                    var item = packet.eventMail.RewardList[i];
                    if (1 > (int)item.RewardType)
                    {
                        ack.error = Errors.InsertEventMail_RewardTypeRequired;
                        ack.errorIndex = i;
                        await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                        return;
                    }

                    if (1 > item.RewardID)
                    {
                        ack.error = Errors.InsertEventMail_RewardIDRequired;
                        ack.errorIndex = i;
                        await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                        return;
                    }

                    if (1 > item.RewardCount)
                    {
                        ack.error = Errors.InsertEventMail_RewardCountRequired;
                        ack.errorIndex = i;
                        await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                        return;
                    }
                }
            }

            var userId = Guid.Empty;

            try
            {
                userId = Guid.Parse(userIdString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (Guid.Empty == userId)
            {
                ack.error = Errors.InsertEventMail_NotSuitableUserId;
                await _hubSend.InsertEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var userJob = new UserJob
            {
                id = Guid.NewGuid(),
                userId = userId,
                jobType = Defines.UserJobType.InsertEventMail,
                jobCount = 1
            };

            try
            {
                await _stompService.PubInsertEventMail(packet.eventMail, Context.ConnectionId, userJob.id, userIdString, packet.reqPathName ?? "", _requestDataService.remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }

        protected override async Task OnUpdateEventMail(PUpdateEventMailReq packet, string userIdString)
        {
            var ack = new PUpdateEventMailResultAck();

            if (null == packet.eventMail)
            {
                ack.error = Errors.UpdateEventMail_ParametersEmpty;
                await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (1 > packet.eventMail.ID)
            {
                ack.error = Errors.UpdateEventMail_MailIDRequired;
                await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.eventMail.Title))
            {
                ack.error = Errors.UpdateEventMail_TitleRequired;
                await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.eventMail.Message))
            {
                ack.error = Errors.UpdateEventMail_MessageRequired;
                await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (0 < packet.eventMail.RewardList.Count)
            {
                for (var i = 0; i < packet.eventMail.RewardList.Count; i++)
                {
                    var item = packet.eventMail.RewardList[i];
                    if (1 > (int)item.RewardType)
                    {
                        ack.error = Errors.UpdateEventMail_RewardTypeRequired;
                        ack.errorIndex = i;
                        await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                        return;
                    }

                    if (1 > item.RewardID)
                    {
                        ack.error = Errors.UpdateEventMail_RewardIDRequired;
                        ack.errorIndex = i;
                        await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                        return;
                    }

                    if (1 > item.RewardCount)
                    {
                        ack.error = Errors.UpdateEventMail_RewardCountRequired;
                        ack.errorIndex = i;
                        await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                        return;
                    }
                }
            }

            var selectEventMailTask = await _dbHelper.Admin.SelectEventMail(packet.eventMail.ID);
            if (false == selectEventMailTask.Item1 || null == selectEventMailTask.Item2)
            {
                ack.error = Errors.UpdateEventMail_NotFoundData;
                await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var userId = Guid.Empty;

            try
            {
                userId = Guid.Parse(userIdString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (Guid.Empty == userId)
            {
                ack.error = Errors.UpdateEventMail_NotSuitableUserId;
                await _hubSend.UpdateEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var userJob = new UserJob
            {
                id = Guid.NewGuid(),
                userId = userId,
                jobType = Defines.UserJobType.UpdateEventMail,
                jobCount = 1
            };

            try
            {
                await _stompService.PubUpdateEventMail(packet.eventMail, Context.ConnectionId, userJob.id, userIdString, packet.reqPathName ?? "", _requestDataService.remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }

        protected override async Task OnRemoveEventMail(PRemoveEventMailReq packet, string userIdString)
        {
            var ack = new PRemoveEventMailResultAck();

            if (null == packet.eventMail)
            {
                ack.error = Errors.RemoveEventMail_ParametersEmpty;
                await _hubSend.RemoveEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            if (1 > packet.eventMail.ID)
            {
                ack.error = Errors.RemoveEventMail_MailIDRequired;
                await _hubSend.RemoveEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var selectEventMailTask = await _dbHelper.Admin.SelectEventMail(packet.eventMail.ID);
            if (false == selectEventMailTask.Item1 || null == selectEventMailTask.Item2)
            {
                ack.error = Errors.RemoveEventMail_NotFoundData;
                await _hubSend.RemoveEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var userId = Guid.Empty;

            try
            {
                userId = Guid.Parse(userIdString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (Guid.Empty == userId)
            {
                ack.error = Errors.RemoveEventMail_NotSuitableUserId;
                await _hubSend.RemoveEventMailResultAck(Context.ConnectionId, ack);
                return;
            }

            var userJob = new UserJob
            {
                id = Guid.NewGuid(),
                userId = userId,
                jobType = Defines.UserJobType.RemoveEventMail,
                jobCount = 1
            };

            try
            {
                await _stompService.PubRemoveEventMail(packet.eventMail, Context.ConnectionId, userJob.id, userIdString, packet.reqPathName ?? "", _requestDataService.remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }

        protected override async Task OnSendChattingMessage(PSendChattingMessageReq packet, string userIdString)
        {
            var ack = new PSendChattingMessageResultAck();

            if (null == packet.chattingMessage)
            {
                ack.error = Errors.SendChattingMessage_ChattingMessageRequired;
                await _hubSend.SendChattingMessageResultAck(Context.ConnectionId, ack);
                return;
            }

            packet.chattingMessage.serverReceiveTime = DateTime.UtcNow;

            if (Guid.Empty == packet.chattingMessage.id)
            {
                ack.error = Errors.SendChattingMessage_IdRequired;
                await _hubSend.SendChattingMessageResultAck(Context.ConnectionId, ack);
                return;
            }

            if (string.IsNullOrWhiteSpace(packet.chattingMessage.message))
            {
                ack.error = Errors.SendChattingMessage_MessageRequired;
                await _hubSend.SendChattingMessageResultAck(Context.ConnectionId, ack);
                return;
            }

            Guid sendSigninId = Guid.Empty;
            string sendUserName = "";
            if (Errors.None == _jwtService.DeserializeToken(packet.token, out var token))
            {
                if (false == token.Payload.TryGetValue("signinId", out var signinIdClaim))
                {
                    ack.error = Errors.SendChattingMessage_SendSigninIdRequired;
                    await _hubSend.SendChattingMessageResultAck(Context.ConnectionId, ack);
                    return;
                }

                if (Errors.None != signinIdClaim.ToString().TryToGuid(out sendSigninId) || Guid.Empty == sendSigninId)
                {
                    ack.error = Errors.SendChattingMessage_SendSigninIdRequired;
                    await _hubSend.SendChattingMessageResultAck(Context.ConnectionId, ack);
                    return;
                }

                sendUserName = signinIdClaim.ToString();

                if (token.Payload.TryGetValue(ClaimTypes.Name, out var nameClaim))
                    sendUserName = nameClaim.ToString();
            }

            if (string.IsNullOrWhiteSpace(sendUserName))
            {
                _logger.LogInformation($"sendSigninId: {sendSigninId}");
                ack.error = Errors.SendChattingMessage_SendUserNameRequired;
                await _hubSend.SendChattingMessageResultAck(Context.ConnectionId, ack);
                return;
            }

            packet.chattingMessage.sendSigninId = sendSigninId;
            packet.chattingMessage.sendUserName = sendUserName;
            packet.chattingMessage.sendConnectionId = Context.ConnectionId;

            await _stompService.PubChattingMessage(packet.chattingMessage);

            _ = _dbHelper.GameManagerWriteOnly.ActiveSignInUser((Guid)userIdString.ToGuid());
        }

        protected override async Task OnTest(PTestReq packet, string userIdString)
        {
            _logger.LogInformation($"client: {Context.ConnectionId} Execute TestReq.({JsonConvert.SerializeObject(packet)})", ConsoleColor.Blue, ConsoleColor.White);
            await _hubSend.TestAck(Context.ConnectionId, new PTestAck { error = NGEL.Data.Errors.Artifact_UIDRequired, isSigned = true });
        }
    }
}
