using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagePack;
using Newtonsoft.Json;
using GMServer.Services;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Vars;
using NGEL.Data.Models;
using NGEL.Data.Helpers;
using NGEL.Data.Settings;
using NGEL.Data.Tables;

namespace GMServer.Hubs
{
    public abstract class AbstractGMServerHub : Hub
    {
        protected abstract ILogger<GMServerHub> _logger { get; }
        protected abstract GMAuthentication _gmAuthentication { get; }
        protected abstract DBHelper _dbHelper { get; }
        protected abstract RequestDataService _requestDataService { get; }
        protected abstract GMLogService _gmLogService { get; }
        protected abstract IHttpContextAccessor _httpContextAccessor { get; }
        protected abstract CommonSettings _commonSettings { get; }
        protected abstract JwtService _jwtService { get; }
        protected abstract ServerStateService _serverStateService { get; }
        protected abstract DataTableService _dataTableService { get; }
        protected abstract IWebHostEnvironment _webHostEnvironment { get; }
        protected abstract AutoGMServerHubSend _hubSend { get; }

        public override async Task OnConnectedAsync()
        {
            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"client connected: {Context.ConnectionId}.", ConsoleColor.DarkGreen, ConsoleColor.White);

            var ack = new PConnectedAck
            {
                error = Errors.None, 
                serverId = _serverStateService.serverId, 
                serverVersion = _serverStateService.version, 
                recommendClientMasterVersion = _serverStateService.recommendClientMasterVersion, 
                recommendClientUpdateVersion = _serverStateService.recommendClientUpdateVersion, 
                recommendClientMaintenanceVersion = _serverStateService.recommendClientMaintenanceVersion, 
            };
            await _hubSend.ConnectedAck(Context.ConnectionId, ack);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"client disconnected: {Context.ConnectionId}.", ConsoleColor.DarkRed, ConsoleColor.White);

            await base.OnDisconnectedAsync(exception);
        }

        protected abstract Task OnCheckAuthentication(PCheckAuthenticationReq packet);
        public async Task CheckAuthentication(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PCheckAuthenticationReq>(out var packet);
            if (Errors.None != error)
            {
                _logger.LogError($"OnCheckAuthentication({Context.ConnectionId}): {error.ToString()}");
                return;
            }

            if (null == packet)
            {
                _logger.LogError($"OnCheckAuthentication({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"CheckAuthentication - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnCheckAuthentication(packet);
        }

        protected abstract Task OnCheckConnection(PCheckConnectionReq packet);
        public async Task CheckConnection(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PCheckConnectionReq>(out var packet);
            if (Errors.None != error)
            {
                _logger.LogError($"CheckConnection({Context.ConnectionId}): {error.ToString()}");
                return;
            }

            if (null == packet)
            {
                _logger.LogError($"CheckConnection({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"CheckConnection - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnCheckConnection(packet);
        }

        protected abstract Task OnSignIn(PSignInReq packet);
        public async Task SignIn(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PSignInReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerSignIn", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"SignIn({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            if (null == packet)
            {
                _logger.LogError($"SignIn({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SignInReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnSignIn(packet);
        }

        protected abstract Task OnSignInLDAP(PSignInLDAPReq packet);
        public async Task SignInLDAP(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PSignInLDAPReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerSignInLDAP", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"SignInLDAP({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            if (null == packet)
            {
                _logger.LogError($"SignInLDAP({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SignInLDAPReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnSignInLDAP(packet);
        }

        protected abstract Task OnSignInEmail(PSignInEmailReq packet);
        public async Task SignInEmail(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PSignInEmailReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerSignInEmail", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"SignInEmail({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            if (null == packet)
            {
                _logger.LogError($"SignInEmail({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SignInEmailReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnSignInEmail(packet);
        }

        protected abstract Task OnSignOut(PSignOutReq packet, string userIdString);
        public async Task SignOut(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PSignOutReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerSignOut", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"SignOut({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"SignOut({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SignOut({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"SignOut({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SignOutReq - connectionId: {Context.ConnectionId}");

            await OnSignOut(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerSignOut", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, "", _requestDataService.remoteAddress);
        }

        protected abstract Task OnSendGameMail(PSendGameMailReq packet, string userIdString);
        public async Task SendGameMail(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PSendGameMailReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerSendGameMail", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"SendGameMail({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"SendGameMail({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendGameMail({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"SendGameMail({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SendGameMailReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnSendGameMail(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerSendGameMail", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
        }

        protected abstract Task OnInsertEventMail(PInsertEventMailReq packet, string userIdString);
        public async Task InsertEventMail(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PInsertEventMailReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerInsertEventMail", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"InsertEventMail({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"InsertEventMail({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"InsertEventMail({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"InsertEventMail({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"InsertEventMailReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnInsertEventMail(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerInsertEventMail", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
        }

        protected abstract Task OnUpdateEventMail(PUpdateEventMailReq packet, string userIdString);
        public async Task UpdateEventMail(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PUpdateEventMailReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerUpdateEventMail", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"UpdateEventMail({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"UpdateEventMail({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateEventMail({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"UpdateEventMail({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"UpdateEventMailReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnUpdateEventMail(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerUpdateEventMail", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
        }

        protected abstract Task OnRemoveEventMail(PRemoveEventMailReq packet, string userIdString);
        public async Task RemoveEventMail(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PRemoveEventMailReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerRemoveEventMail", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"RemoveEventMail({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"RemoveEventMail({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"RemoveEventMail({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"RemoveEventMail({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"RemoveEventMailReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnRemoveEventMail(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerRemoveEventMail", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
        }

        protected abstract Task OnSendChattingMessage(PSendChattingMessageReq packet, string userIdString);
        public async Task SendChattingMessage(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PSendChattingMessageReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerSendChattingMessage", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"SendChattingMessage({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"SendChattingMessage({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendChattingMessage({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"SendChattingMessage({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SendChattingMessageReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnSendChattingMessage(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerSendChattingMessage", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
        }

        protected abstract Task OnTest(PTestReq packet, string userIdString);
        public async Task Test(string base64Packet)
        {
            var error = base64Packet.DeserializeMessagePack<PTestReq>(out var packet);
            switch (error)
            {
                case Errors.None:
                    break;

                default:
                    var errorAck = new PCommonNoticeAck { error = error };
                    await _hubSend.CommonNoticeAck(Context.ConnectionId, errorAck);
                    _ = _gmLogService.InsertHubReqLog("", "GMServerTest", "", error.ToString(), _requestDataService.userAgent, "", _requestDataService.remoteAddress);
                    _logger.LogError($"Test({Context.ConnectionId}): {error.ToString()}");
                    return;
            }

            string userId = "";

            if (null == packet)
            {
                _logger.LogError($"Test({Context.ConnectionId}): {Errors.Common_PacketArgsEmpty}");
                return;
            }

            try
            {
                var getSignedInUserTask = await _gmAuthentication.GetSignedInUser(packet.token);

                if (Errors.None != getSignedInUserTask.Item1 || null == getSignedInUserTask.Item2)
                    throw new Exception(getSignedInUserTask.Item1.ToString());

                userId = getSignedInUserTask.Item2.userId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Test({Context.ConnectionId}): {ex.Message}");
                _logger.LogError($"Test({Context.ConnectionId}): {ex.StackTrace}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"TestReq - connectionId: {Context.ConnectionId}, packet: {JsonConvert.SerializeObject(packet)}");

            await OnTest(packet, userId);

            _ = _gmLogService.InsertHubReqLog(userId, "GMServerTest", (packet.reqPathName ?? ""), "", _requestDataService.userAgent, JsonConvert.SerializeObject(packet), _requestDataService.remoteAddress);
        }

    }

}
