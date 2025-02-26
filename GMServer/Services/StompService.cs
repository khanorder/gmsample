using System;
using Newtonsoft.Json;
using Websocket.Client.Models;
using Websocket.Client;
using Netina.Stomp.Client;
//using Netina.Stomp.Client.Messages;
using NGEL.Data;
using NGEL.Data.Settings;
using Netina.Stomp.Client.Utils;
using GMServer.Stomp;
using NGEL.Data.Models;
using GMServer.Stomp.Messages;
using Lobby;
using GMServer.Hubs;
using NGEL.Data.Vars;
using NGEL.Data.DB;
using Quartz.Util;
using NPOI.POIFS.Crypt.Dsig;
using NGEL.Data.Helpers;

namespace GMServer.Services
{
    public class StompService
    {
        private readonly ILogger<StompService> _logger;
        private readonly CommonSettings _commonSettings;
        private readonly LobbyTimeService _lobbyTimeService;
        private readonly GMLogService _gmLogService;
        private readonly AutoGMServerHubSend _autoGMServerHubSend;
        private readonly DBHelper _dbHelper;
        private readonly GMUserService _gmUserService;
        private int _currentBrokerIndex = 0;
        private ActiveMQStompClient? _stompClient { get; set; }
        //private StompClient? _stompClient { get; set; }

        public StompService (ILogger<StompService> logger, CommonSettings commonSettings, LobbyTimeService lobbyTimeService, GMLogService gmLogService, AutoGMServerHubSend autoGMServerHubSend, DBHelper dbHelper, GMUserService gmUserService)
        {
            _logger = logger;
            _commonSettings = commonSettings;
            _lobbyTimeService = lobbyTimeService;
            _gmLogService = gmLogService;
            _autoGMServerHubSend = autoGMServerHubSend;
            _dbHelper = dbHelper;
            _gmUserService = gmUserService;
        }

        private async void OnConnect(object? sender, StompMessage e)
        {
            if (Defines.ServerStateType.Service != _commonSettings.serverState)
            {
                _logger.LogInformation($"OnConnect: [{_currentBrokerIndex}] {_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]}");
                _logger.LogInformation(JsonConvert.SerializeObject(e));
            }

            if (null != _stompClient)
            {
                await SubGameManager();
                await SubQueue();
                await SubTopicGMChat();
                await PubGetSysTime();
            }
        }

        private async void OnReconnect(object? sender, ReconnectionInfo info)
        {
            if (Defines.ServerStateType.Service != _commonSettings.serverState)
            {
                _logger.LogInformation($"OnReconnect: [{_currentBrokerIndex}] {_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]}");
                _logger.LogInformation(JsonConvert.SerializeObject(info));
            }

            if (null != _stompClient)
            {
                await SubGameManager();
                await SubQueue();
                await SubTopicGMChat();
                await PubGetSysTime();
            }
        }

        private void OnError(object? sender, StompMessage e)
        {
            _logger.LogError("OnError");
            _logger.LogError(JsonConvert.SerializeObject(e));
        }

        private void OnClose(object? sender, DisconnectionInfo info)
        {
            _logger.LogInformation($"Close: [{_currentBrokerIndex}] {_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]}");
            if (null != _stompClient)
            {
                try
                {
                    _stompClient.OnConnect -= OnConnect;
                    _stompClient.OnReconnect -= OnReconnect;
                    _stompClient.OnError -= OnError;
                    _stompClient.OnClose -= OnClose;
                    _stompClient.Dispose();
                    _stompClient = null;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Remove closed connection: [{_currentBrokerIndex}] {_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]}");
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                }
            }

            _currentBrokerIndex++;
            if (_commonSettings.stompInfo.brokerUrls.Count <= _currentBrokerIndex)
                _currentBrokerIndex = 0;

            int waitTime = 0;
            while (waitTime < 10)
            {
                waitTime++;
                _logger.LogInformation($"Wait for reconnecting: {waitTime}");
                Thread.Sleep(1000);
            }

            _ = Connect();

            //if (Defines.ServerStateType.InsideTest == _commonSettings.serverState)
            //    _logger.LogError(JsonConvert.SerializeObject(info));
        }

        public async Task Connect()
        {
            if (Defines.ServerStateType.Service != _commonSettings.serverState)
                _logger.LogInformation($"Tring connect: [{_currentBrokerIndex}] {_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]}");

            if (null == _stompClient)
            {
                //_stompClient = new ActiveMQStompClient(_commonSettings.stompInfo);
                _stompClient = new ActiveMQStompClient(_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]);
                _stompClient.OnConnect += OnConnect;
                _stompClient.OnReconnect += OnReconnect;
                _stompClient.OnError += OnError;
                _stompClient.OnClose += OnClose;
                //_stompClient.OnMessage += OnMessage;
            }

            try
            {
                var connectionHeaders = new Dictionary<string, string>
                {
                    { "login", _commonSettings.stompInfo.login },
                    { "passcode", _commonSettings.stompInfo.passcode }
                };
                await _stompClient.ConnectAsync(connectionHeaders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to connect: [{_currentBrokerIndex}] {_commonSettings.stompInfo.brokerUrls[_currentBrokerIndex]}");
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }

        private async Task SubGameManager()
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            var subGetAllSysTimeHeaders = new Dictionary<string, string>();
            await _stompClient.SubscribeAsync("/topic/gm", subGetAllSysTimeHeaders, (async (sender, stompMessage) =>
            {
                if (null == stompMessage)
                {
                    _logger.LogError($"/topic/gm: SubMessage is null.");
                    return;
                }

                if (1 > stompMessage.Headers.Count)
                {
                    _logger.LogError($"/topic/gm: Not Found Any Headers.");
                    return;
                }

                if (false == stompMessage.Headers.TryGetValue("cmd", out var cmd))
                {
                    _logger.LogError($"/topic/gm: Not Found CMD Header.");
                    return;
                }

                switch (cmd)
                {
                    #region 로비서버 시스템시간 받기
                    case "LobbyToOptool_GetSystemTime":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gm[getSystemTime]: Body is empty.");
                            return;
                        }

                        var getSystemTimeBody = new Dictionary<string, string>();
                        try
                        {
                            getSystemTimeBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[getSystemTime]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == getSystemTimeBody)
                        {
                            _logger.LogError($"/topic/gm[getSystemTime]: Body is null.");
                            return;
                        }

                        if (false == getSystemTimeBody.TryGetValue("LobbyID", out var getSystemTimeLobbyID) || string.IsNullOrWhiteSpace(getSystemTimeLobbyID))
                        {
                            _logger.LogError($"/topic/gm[getSystemTime]: LobbyID is empty.");
                            return;
                        }

                        if (false == getSystemTimeBody.TryGetValue("systemTime", out var getSystemTimeString) || string.IsNullOrWhiteSpace(getSystemTimeString))
                        {
                            _logger.LogError($"/topic/gm[getSystemTime]: systemTime is empty.");
                            return;
                        }

                        try
                        {
                            var getSystemTime = DateTime.Parse(getSystemTimeString);
                            _lobbyTimeService.SetLobbyTime(getSystemTimeLobbyID, getSystemTime);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[getSystemTime]: Parse systemTime Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                        break;
                    #endregion

                    #region 로비서버 시스템시간 변경 결과확인
                    case "LobbyToOptool_ChangeSystemTime":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gm[changeSystemTime]: Body is empty.");
                            return;
                        }

                        var changeSystemTimeBody = new Dictionary<string, string>();
                        try
                        {
                            changeSystemTimeBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[changeSystemTime]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == changeSystemTimeBody)
                        {
                            _logger.LogError($"/topic/gm[changeSystemTime]: Body is null.");
                            return;
                        }

                        if (false == changeSystemTimeBody.TryGetValue("LobbyID", out var changeSystemTimeLobbyID) || string.IsNullOrWhiteSpace(changeSystemTimeLobbyID))
                        {
                            _logger.LogError($"/topic/gm[changeSystemTime]: LobbyID is empty.");
                            return;
                        }

                        if (false == changeSystemTimeBody.TryGetValue("systemTime", out var changeSystemTimeString) || string.IsNullOrWhiteSpace(changeSystemTimeString))
                        {
                            _logger.LogError($"/topic/gm[changeSystemTime]: systemTime is empty.");
                            return;
                        }

                        try
                        {
                            var changeSystemTime = DateTime.Parse(changeSystemTimeString);
                            _lobbyTimeService.SetLobbyTime(changeSystemTimeLobbyID, changeSystemTime);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[changeSystemTime]: Parse systemTime Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                        break;
                    #endregion

                    #region 우편 즉시발송 결과확인
                    case "LobbyToOptool_SendMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gm[sendMail]: Body is empty.");
                            return;
                        }

                        var sendMailBody = new Dictionary<string, string>();
                        try
                        {
                            sendMailBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[sendMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == sendMailBody)
                        {
                            _logger.LogError($"/topic/gm[sendMail]: Body is null.");
                            return;
                        }

                        if (false == sendMailBody.TryGetValue("TargetUID", out var sendMailTargetUID) || string.IsNullOrWhiteSpace(sendMailTargetUID))
                        {
                            _logger.LogError($"/topic/gm[sendMail]: TargetUID is empty.");
                            return;
                        }

                        if (false == sendMailBody.TryGetValue("MailID", out var sendMailMailID) || string.IsNullOrWhiteSpace(sendMailMailID))
                        {
                            _logger.LogError($"/topic/gm[sendMail]: MailID is empty.");
                            return;
                        }

                        sendMailBody.TryGetValue("connectionId", out var sendMailConnectionId);
                        sendMailBody.TryGetValue("jobId", out var sendMailJobIdString);
                        sendMailBody.TryGetValue("debugString", out var sendMailDebugString);

                        Int64 TargetUID = 0;
                        Int64 MailID = 0;

                        try
                        {
                            TargetUID = Int64.Parse(sendMailTargetUID);;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[sendMail]: Parse TargetUID Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        try
                        {
                            MailID = Int64.Parse(sendMailMailID); ;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[sendMail]: Parse MailID Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        _logger.LogInformation(JsonConvert.SerializeObject(sendMailBody));

                        if (false == string.IsNullOrWhiteSpace(sendMailConnectionId) && false == string.IsNullOrWhiteSpace(sendMailJobIdString))
                        {
                            Guid sendMailJobId = Guid.Empty;
                            try
                            {
                                sendMailJobId = Guid.Parse(sendMailJobIdString);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex.Message);
                                _logger.LogError(ex.StackTrace);
                            }

                            if (Guid.Empty == sendMailJobId)
                            {
                                _logger.LogError($"/topic/gm[sendMail]: Not Suitable jobId.");
                                return;
                            }

                            if (Errors.None != _gmUserService.GetUser(sendMailConnectionId, out var user) || null == user)
                                return;

                            var userJob = new UserJob
                            {
                                id = sendMailJobId,
                                userId = user.userId,
                                startTime = DateTime.UtcNow,
                                updateTime = DateTime.UtcNow
                            };

                            if (0 < MailID)
                            {
                                _gmUserService.AddUserJobCount(sendMailConnectionId, userJob, false);
                            }
                            else
                            {
                                var message = "메일 발송 실패";
                                if (false == string.IsNullOrWhiteSpace(sendMailDebugString))
                                    message = message + $"({sendMailDebugString})";

                                userJob.message = message;
                                _gmUserService.AddUserJobCount(sendMailConnectionId, userJob, true);
                            }

                            if (Errors.None == _gmUserService.GetUserJob(sendMailConnectionId, sendMailJobId, out var checkUserJob) && null != checkUserJob)
                            {
                                var ack = new PSendUserJobAck { userJob = checkUserJob };
                                if (checkUserJob.jobCount == (checkUserJob.succeededCount + checkUserJob.failedCount))
                                    _gmUserService.ClearUserJob(sendMailConnectionId, sendMailJobId);

                                _ = _autoGMServerHubSend.SendUserJobAck(sendMailConnectionId, ack);
                            }
                        }
                        break;
                    #endregion

                    #region 이벤트 우편 발송 결과확인
                    case "LobbyToOptool_InsertEventMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gm[insertEventMail]: Body is empty.");
                            return;
                        }

                        var insertEventMailBody = new Dictionary<string, string>();
                        try
                        {
                            insertEventMailBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[insertEventMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == insertEventMailBody)
                        {
                            _logger.LogError($"/topic/gm[insertEventMail]: Body is null.");
                            return;
                        }

                        if (false == insertEventMailBody.TryGetValue("ID", out var insertEventMailIDString) || string.IsNullOrWhiteSpace(insertEventMailIDString))
                        {
                            _logger.LogError($"/topic/gm[insertEventMail]: ID is empty.");
                            return;
                        }

                        insertEventMailBody.TryGetValue("connectionId", out var insertEventMailConnectionId);
                        insertEventMailBody.TryGetValue("debugString", out var insertEventMailDebugString);

                        Int64 insertEventMailID = 0;

                        try
                        {
                            insertEventMailID = Int64.Parse(insertEventMailIDString); ;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[insertEventMail]: Parse ID Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        if (false == string.IsNullOrWhiteSpace(insertEventMailConnectionId))
                        {
                            var ack = new PInsertEventMailResultAck();
                            if (0 < insertEventMailID)
                            {
                                ack.message = $"이벤트 메일 등록 완료(ID: {insertEventMailID})";
                            }
                            else
                            {
                                ack.error = Errors.InsertEventMail_FailedToInsert;
                                ack.message = "이벤트 메일 등록 실패";
                                if (false == string.IsNullOrWhiteSpace(insertEventMailDebugString))
                                    ack.message = ack.message + $"({insertEventMailDebugString})";
                            }
                            _ = _autoGMServerHubSend.InsertEventMailResultAck(insertEventMailConnectionId, ack);
                        }
                        break;
                    #endregion

                    #region 이벤트 우편 수정 결과확인
                    case "LobbyToOptool_UpdateEventMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gm[updateEventMail]: Body is empty.");
                            return;
                        }

                        var updateEventMailBody = new Dictionary<string, string>();
                        try
                        {
                            updateEventMailBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[updateEventMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == updateEventMailBody)
                        {
                            _logger.LogError($"/topic/gm[updateEventMail]: Body is null.");
                            return;
                        }

                        if (false == updateEventMailBody.TryGetValue("ID", out var updateEventMailIDString) || string.IsNullOrWhiteSpace(updateEventMailIDString))
                        {
                            _logger.LogError($"/topic/gm[updateEventMail]: ID is empty.");
                            return;
                        }

                        updateEventMailBody.TryGetValue("connectionId", out var updateEventMailConnectionId);
                        updateEventMailBody.TryGetValue("debugString", out var updateEventMailDebugString);

                        Int64 updateEventMailID = 0;

                        try
                        {
                            updateEventMailID = Int64.Parse(updateEventMailIDString); ;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[updateEventMail]: Parse ID Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        if (false == string.IsNullOrWhiteSpace(updateEventMailConnectionId))
                        {
                            var ack = new PUpdateEventMailResultAck();
                            if (0 < updateEventMailID)
                            {
                                ack.message = $"이벤트 메일 수정 완료(ID: {updateEventMailID})";
                            }
                            else
                            {
                                ack.error = Errors.UpdateEventMail_FailedToUpdate;
                                ack.message = "이벤트 메일 수정 실패";
                                if (false == string.IsNullOrWhiteSpace(updateEventMailDebugString))
                                    ack.message = ack.message + $"({updateEventMailDebugString})";
                            }
                            _ = _autoGMServerHubSend.UpdateEventMailResultAck(updateEventMailConnectionId, ack);
                        }
                        break;
                    #endregion

                    #region 이벤트 우편 삭제 결과확인
                    case "LobbyToOptool_RemoveEventMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gm[removeEventMail]: Body is empty.");
                            return;
                        }

                        var removeEventMailBody = new Dictionary<string, string>();
                        try
                        {
                            removeEventMailBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[removeEventMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == removeEventMailBody)
                        {
                            _logger.LogError($"/topic/gm[removeEventMail]: Body is null.");
                            return;
                        }

                        if (false == removeEventMailBody.TryGetValue("ID", out var removeEventMailIDString) || string.IsNullOrWhiteSpace(removeEventMailIDString))
                        {
                            _logger.LogError($"/topic/gm[removeEventMail]: ID is empty.");
                            return;
                        }

                        removeEventMailBody.TryGetValue("connectionId", out var removeEventMailConnectionId);
                        removeEventMailBody.TryGetValue("debugString", out var removeEventMailDebugString);

                        Int64 removeEventMailID = 0;

                        try
                        {
                            removeEventMailID = Int64.Parse(removeEventMailIDString); ;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gm[removeEventMail]: Parse ID Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        if (false == string.IsNullOrWhiteSpace(removeEventMailConnectionId))
                        {
                            var ack = new PRemoveEventMailResultAck();
                            if (0 < removeEventMailID)
                            {
                                ack.message = $"이벤트 메일 삭제 완료(ID: {removeEventMailID})";
                            }
                            else
                            {
                                ack.error = Errors.RemoveEventMail_FailedToRemove;
                                ack.message = "이벤트 메일 삭제 실패";
                                if (false == string.IsNullOrWhiteSpace(removeEventMailDebugString))
                                    ack.message = ack.message + $"({removeEventMailDebugString})";
                            }
                            _ = _autoGMServerHubSend.RemoveEventMailResultAck(removeEventMailConnectionId, ack);
                        }
                        break;
                    #endregion

                    default:
                        _logger.LogError($"/topic/gm: '{cmd}' Not Found Suitable CMD Header.");
                        return;
                }
            }));
        }

        private async Task SubQueue()
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            var subGetAllSysTimeHeaders = new Dictionary<string, string>();
            await _stompClient.SubscribeAsync("/queue/lobby", subGetAllSysTimeHeaders, (async (sender, stompMessage) =>
            {
                if (null == stompMessage)
                {
                    _logger.LogError($"/queue/lobby: SubMessage is null.");
                    return;
                }

                if (1 > stompMessage.Headers.Count)
                {
                    _logger.LogError($"/queue/lobby: Not Found Any Headers.");
                    return;
                }

                if (false == stompMessage.Headers.TryGetValue("cmd", out var cmd))
                {
                    _logger.LogError($"/queue/lobby: Not Found CMD Header.");
                    return;
                }
                switch (cmd)
                {
                    case "OptoolToLobby_SendMail":
                        if (false == stompMessage.Headers.TryGetValue("target-uid", out var targetUID))
                        {
                            _logger.LogError($"/queue/lobby: Not Found uid Header.");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/queue/lobby[sendMail]: Body is empty.");
                            return;
                        }

                        var sendMailBody = new Dictionary<string, object>();
                        try
                        {
                            sendMailBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/queue/lobby[sendMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == sendMailBody)
                        {
                            _logger.LogError($"/queue/lobby[sendMail]: Body is null.");
                            return;
                        }

                        sendMailBody.TryGetValue("MailType", out var sendMailMailType);
                        sendMailBody.TryGetValue("Title", out var sendMailTitle);
                        sendMailBody.TryGetValue("Message", out var sendMailMessage);
                        sendMailBody.TryGetValue("IsBM", out var sendMailIsBM);
                        sendMailBody.TryGetValue("RewardList", out var sendMailRewardListObj);
                        var sendMailRewardList = new List<MailReward>();
                        if (null != sendMailRewardListObj)
                        {
                            var sendMailRewardListString = sendMailRewardListObj.ToString();
                            if (false == string.IsNullOrWhiteSpace(sendMailRewardListString))
                            {
                                try
                                {
                                    sendMailRewardList = JsonConvert.DeserializeObject<List<MailReward>>(sendMailRewardListString);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"/queue/lobby[sendMail]: Parse sendMailRewardList Error.");
                                    _logger.LogError(ex.Message);
                                    _logger.LogError(ex.StackTrace);
                                }
                            }
                        }
                        sendMailBody.TryGetValue("ExpireTime", out var sendMailExpireTime);
                        sendMailBody.TryGetValue("Destination", out var sendMailDestination);
                        sendMailBody.TryGetValue("connectionId", out var sendMailConnectionId);
                        sendMailBody.TryGetValue("jobId", out var sendMailJobId);

                        try
                        {
                            var insertMailTask = await _dbHelper.GameWriteOnly.InsertMail(Int64.Parse(targetUID), (EMailType)(Convert.ToInt32(Int64.Parse(sendMailMailType.ToString()))), EMailStateType.Arrived, (bool)sendMailIsBM, (string)sendMailTitle, (string)sendMailMessage, sendMailRewardList, Convert.ToInt32(Int64.Parse(sendMailExpireTime.ToString())));
                            if (insertMailTask.Item1 && null != insertMailTask.Item2)
                            {
                                await PubAlertSendMail(Int64.Parse(targetUID), insertMailTask.Item2.MailID, sendMailConnectionId.ToString(), Guid.Parse(sendMailJobId.ToString()), "완료", sendMailDestination.ToString());
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                        await PubAlertSendMail(0, 0, sendMailConnectionId.ToString(), Guid.Parse(sendMailJobId.ToString()), "실패", sendMailDestination.ToString());

                        break;

                    case "OptoolToLobby_InsertEventMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/queue/lobby[insertEventMail]: Body is empty.");
                            return;
                        }

                        var insertEventMailBody = new Dictionary<string, object>();
                        try
                        {
                            insertEventMailBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/queue/lobby[insertEventMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == insertEventMailBody)
                        {
                            _logger.LogError($"/queue/lobby[insertEventMail]: Body is null.");
                            return;
                        }

                        insertEventMailBody.TryGetValue("MailType", out var insertEventMailMailType);
                        insertEventMailBody.TryGetValue("Title", out var insertEventMailTitle);
                        insertEventMailBody.TryGetValue("Message", out var insertEventMailMessage);
                        insertEventMailBody.TryGetValue("RewardList", out var insertEventMailRewardListObj);
                        var insertEventMailRewardList = new List<MailReward>();
                        if (null != insertEventMailRewardListObj)
                        {
                            var insertEventMailRewardListString = insertEventMailRewardListObj.ToString();
                            if (false == string.IsNullOrWhiteSpace(insertEventMailRewardListString))
                            {
                                try
                                {
                                    insertEventMailRewardList = JsonConvert.DeserializeObject<List<MailReward>>(insertEventMailRewardListString);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"/queue/lobby[insertEventMail]: Parse insertEventMailRewardList Error.");
                                    _logger.LogError(ex.Message);
                                    _logger.LogError(ex.StackTrace);
                                }
                            }
                        }
                        insertEventMailBody.TryGetValue("ExpireTime", out var insertEventMailExpireTime);
                        insertEventMailBody.TryGetValue("StartTime", out var insertEventMailStartTime);
                        insertEventMailBody.TryGetValue("EndTime", out var insertEventMailEndTime);
                        insertEventMailBody.TryGetValue("Destination", out var insertEventMailDestination);
                        insertEventMailBody.TryGetValue("connectionId", out var insertEventMailConnectionId);
                        insertEventMailBody.TryGetValue("jobId", out var insertEventMailJobId);
                        try
                        {
                            var insertEventMailTask = await _dbHelper.AdminWriteOnly.InsertEventMail((EMailType)(Convert.ToInt32(Int64.Parse(insertEventMailMailType.ToString()))), (string)insertEventMailTitle, (string)insertEventMailMessage, insertEventMailRewardList, Convert.ToInt32(Int64.Parse(insertEventMailExpireTime.ToString())), DateTime.Parse(insertEventMailStartTime.ToString()), DateTime.Parse(insertEventMailEndTime.ToString()));
                            if (insertEventMailTask.Item1 && null != insertEventMailTask.Item2)
                            {
                                await PubAlertInsertEventMail(insertEventMailTask.Item2.ID, insertEventMailConnectionId.ToString(), Guid.Parse(insertEventMailJobId.ToString()), "완료", insertEventMailDestination.ToString());
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                        await PubAlertInsertEventMail(0, insertEventMailConnectionId.ToString(), Guid.Parse(insertEventMailJobId.ToString()), "실패", insertEventMailDestination.ToString());

                        break;

                    case "OptoolToLobby_UpdateEventMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/queue/lobby[updateEventMail]: Body is empty.");
                            return;
                        }

                        var updateEventMailBody = new Dictionary<string, object>();
                        try
                        {
                            updateEventMailBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/queue/lobby[updateEventMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == updateEventMailBody)
                        {
                            _logger.LogError($"/queue/lobby[updateEventMail]: Body is null.");
                            return;
                        }

                        updateEventMailBody.TryGetValue("ID", out var updateEventMailID);
                        updateEventMailBody.TryGetValue("MailType", out var updateEventMailMailType);
                        updateEventMailBody.TryGetValue("Title", out var updateEventMailTitle);
                        updateEventMailBody.TryGetValue("Message", out var updateEventMailMessage);
                        updateEventMailBody.TryGetValue("RewardList", out var updateEventMailRewardListObj);
                        var updateEventMailRewardList = new List<MailReward>();
                        if (null != updateEventMailRewardListObj)
                        {
                            var updateEventMailRewardListString = updateEventMailRewardListObj.ToString();
                            if (false == string.IsNullOrWhiteSpace(updateEventMailRewardListString))
                            {
                                try
                                {
                                    updateEventMailRewardList = JsonConvert.DeserializeObject<List<MailReward>>(updateEventMailRewardListString);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"/queue/lobby[updateEventMail]: Parse updateEventMailRewardList Error.");
                                    _logger.LogError(ex.Message);
                                    _logger.LogError(ex.StackTrace);
                                }
                            }
                        }
                        updateEventMailBody.TryGetValue("ExpireTime", out var updateEventMailExpireTime);
                        updateEventMailBody.TryGetValue("StartTime", out var updateEventMailStartTime);
                        updateEventMailBody.TryGetValue("EndTime", out var updateEventMailEndTime);
                        updateEventMailBody.TryGetValue("Destination", out var updateEventMailDestination);
                        updateEventMailBody.TryGetValue("connectionId", out var updateEventMailConnectionId);
                        updateEventMailBody.TryGetValue("jobId", out var updateEventMailJobId);
                        try
                        {
                            var updateEventMailTask = await _dbHelper.AdminWriteOnly.UpdateEventMail(Int64.Parse(updateEventMailID.ToString()), (EMailType)(Convert.ToInt32(Int64.Parse(updateEventMailMailType.ToString()))), (string)updateEventMailTitle, (string)updateEventMailMessage, updateEventMailRewardList, Convert.ToInt32(Int64.Parse(updateEventMailExpireTime.ToString())), DateTime.Parse(updateEventMailStartTime.ToString()), DateTime.Parse(updateEventMailEndTime.ToString()));
                            if (updateEventMailTask)
                            {
                                await PubAlertUpdateEventMail(Int64.Parse(updateEventMailID.ToString()), updateEventMailConnectionId.ToString(), Guid.Parse(updateEventMailJobId.ToString()), "완료", updateEventMailDestination.ToString());
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                        await PubAlertUpdateEventMail(Int64.Parse(updateEventMailID.ToString()), updateEventMailConnectionId.ToString(), Guid.Parse(updateEventMailJobId.ToString()), "실패", updateEventMailDestination.ToString());

                        break;

                    case "OptoolToLobby_RemoveEventMail":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/queue/lobby[removeEventMail]: Body is empty.");
                            return;
                        }

                        var removeEventMailBody = new Dictionary<string, object>();
                        try
                        {
                            removeEventMailBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/queue/lobby[removeEventMail]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == removeEventMailBody)
                        {
                            _logger.LogError($"/queue/lobby[removeEventMail]: Body is null.");
                            return;
                        }

                        removeEventMailBody.TryGetValue("ID", out var removeEventMailID);
                        removeEventMailBody.TryGetValue("Destination", out var removeEventMailDestination);
                        removeEventMailBody.TryGetValue("connectionId", out var removeEventMailConnectionId);
                        removeEventMailBody.TryGetValue("jobId", out var removeEventMailJobId);
                        try
                        {
                            var deleteEventMailTask = await _dbHelper.AdminWriteOnly.DeleteEventMail(Int64.Parse(removeEventMailID.ToString()));
                            if (deleteEventMailTask)
                            {
                                await PubAlertRemoveEventMail(Int64.Parse(removeEventMailID.ToString()), removeEventMailConnectionId.ToString(), Guid.Parse(removeEventMailJobId.ToString()), "완료", removeEventMailDestination.ToString());
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                        await PubAlertRemoveEventMail(Int64.Parse(removeEventMailID.ToString()), removeEventMailConnectionId.ToString(), Guid.Parse(removeEventMailJobId.ToString()), "실패", removeEventMailDestination.ToString());

                        break;

                    default:
                        _logger.LogError($"/queue/lobby: '{cmd}' Not Found Suitable CMD Header.");
                        return;
                }
            }));
        }

        private async Task SubTopicGMChat()
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            var subGetAllSysTimeHeaders = new Dictionary<string, string>();
            await _stompClient.SubscribeAsync("/topic/gmchat", subGetAllSysTimeHeaders, (async (sender, stompMessage) =>
            {
                if (null == stompMessage)
                {
                    _logger.LogError($"/topic/gmchat: SubMessage is null.");
                    return;
                }

                if (1 > stompMessage.Headers.Count)
                {
                    _logger.LogError($"/topic/gmchat: Not Found Any Headers.");
                    return;
                }

                if (false == stompMessage.Headers.TryGetValue("cmd", out var cmd))
                {
                    _logger.LogError($"/topic/gmchat: Not Found CMD Header.");
                    return;
                }
                switch (cmd)
                {
                    case "OptoolToOptool_ChattingMessage":
                        if (string.IsNullOrWhiteSpace(stompMessage.Body))
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Body is empty.");
                            return;
                        }

                        var chatMsgBody = new Dictionary<string, object>();
                        try
                        {
                            chatMsgBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(stompMessage.Body);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Parse Body Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                            return;
                        }

                        if (null == chatMsgBody)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Body is null.");
                            return;
                        }

                        var chattingMessage = new ChattingMessage();

                        chatMsgBody.TryGetValue("id", out var chatMsgId);

                        if (null == chatMsgId)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: id is null.");
                            return;
                        }

                        if (Errors.None == ((string)chatMsgId).TryToGuid(out var chattingMessageId))
                        {
                            chattingMessage.id = chattingMessageId;
                        }
                        else
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Parse id Error.");
                            return;
                        }

                        chatMsgBody.TryGetValue("messageType", out var chatMsgType);

                        if (null == chatMsgType)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: messageType is null.");
                            return;
                        }

                        try
                        {
                            var chattingMessageType = (Defines.ChattingMessageType)(Convert.ToInt32(Int64.Parse(chatMsgType.ToString())));
                            chattingMessage.messageType = chattingMessageType;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Parse messageType Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        chatMsgBody.TryGetValue("message", out var chatMsgMessage);

                        if (null == chatMsgMessage)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: message is null.");
                            return;
                        }

                        string chattingMessageMessage = (string)chatMsgMessage;
                        chattingMessage.message = chattingMessageMessage;

                        if (Defines.ChattingMessageType.Notice == chattingMessage.messageType)
                        {
                            chattingMessage.serverSendTime = DateTime.UtcNow;
                            var ack = new PReceiveChattingMessageAllAck { chattingMessage = chattingMessage };
                            await _autoGMServerHubSend.ReceiveChattingMessageAllAck(ack);
                            return;
                        }

                        chatMsgBody.TryGetValue("sendSigninId", out var chatMsgSendSigninId);

                        if (null == chatMsgSendSigninId)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: sendSigninId is null.");
                            return;
                        }

                        if (Errors.None == ((string)chatMsgSendSigninId).TryToGuid(out var chattingMessageSendSigninId))
                        {
                            chattingMessage.sendSigninId = chattingMessageSendSigninId;
                        }
                        else
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Parse sendSigninId Error.");
                            return;
                        }

                        chatMsgBody.TryGetValue("sendUserName", out var chatMsgSendUserName);

                        if (null == chatMsgMessage)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: sendUserName is null.");
                            return;
                        }

                        string chattingMessageSendUserName = (string)chatMsgSendUserName;
                        chattingMessage.sendUserName = chattingMessageSendUserName;

                        chatMsgBody.TryGetValue("sendConnectionId", out var chatMsgSendConnectionId);

                        if (null == chatMsgSendConnectionId)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: sendConnectionId is null.");
                            return;
                        }

                        string sendConnectionId = (string)chatMsgSendConnectionId;
                        chattingMessage.sendConnectionId = sendConnectionId;

                        chatMsgBody.TryGetValue("localSendTime", out var chatMsgLocalSendTime);

                        if (null == chatMsgLocalSendTime)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: localSendTime is null.");
                            return;
                        }

                        try
                        {
                            var chattingMessageLocalSendTime = DateTime.Parse(chatMsgLocalSendTime.ToString());
                            chattingMessage.localSendTime = chattingMessageLocalSendTime;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Parse localSendTime Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        chatMsgBody.TryGetValue("serverReceiveTime", out var chatMsgServerReceiveTime);

                        if (null == chatMsgServerReceiveTime)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: serverReceiveTime is null.");
                            return;
                        }

                        try
                        {
                            var chattingMessageServerReceiveTime = DateTime.Parse(chatMsgServerReceiveTime.ToString());
                            chattingMessage.serverReceiveTime = chattingMessageServerReceiveTime;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: Parse serverReceiveTime Error.");
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }

                        chatMsgBody.TryGetValue("targetUserId", out var chatMsgTargetUserId);

                        if (null == chatMsgTargetUserId)
                        {
                            _logger.LogError($"/topic/gmchat[chattingMessage]: targetUserId is null.");
                            return;
                        }

                        if (Errors.None == ((string)chatMsgTargetUserId).TryToGuid(out var chattingMessageTargetUserId))
                        {
                            chattingMessage.targetUserId = chattingMessageTargetUserId;
                            var selectUserSigninTask = await _dbHelper.GameManager.SelectUserSignin(chattingMessageTargetUserId);
                            if (false == selectUserSigninTask.Item1 || null == selectUserSigninTask.Item2)
                            {
                                _logger.LogError($"/topic/gmchat[chattingMessage]: not found target user signin is null.");
                                return;
                            }

                            chattingMessage.targetConnectionId = selectUserSigninTask.Item2.connectionId;
                            chattingMessage.serverSendTime = DateTime.UtcNow;
                            var ack = new PReceiveChattingMessageAck { chattingMessage = chattingMessage };
                            await _autoGMServerHubSend.ReceiveChattingMessageAck(chattingMessage.targetConnectionId, ack);
                        }
                        else
                        {
                            chattingMessage.serverSendTime = DateTime.UtcNow;
                            var ack = new PReceiveChattingMessageAllAck { chattingMessage = chattingMessage };
                            await _autoGMServerHubSend.ReceiveChattingMessageAllAck(ack);
                        }

                        break;

                    default:
                        _logger.LogError($"/topic/gmchat: '{cmd}' Not Found Suitable CMD Header.");
                        return;
                }
            }));
        }

        private Dictionary<string, object> SetNoticeBody(string message, ENoticeType noticeType, float visibleTime, int visibleCount)
        {
            var body = new Dictionary<string, object>
            {
                { "NoticeMessage", message },
                { "NoticeType", noticeType },
                { "VisibleTime", visibleTime },
                { "VisibleCount", visibleCount }
            };
            return body;
        }

        #region 채팅 공지(관리자 정보 포함)
        public async Task PubNoticeAll(ChattingNotice chattingNotice, string userId, string reqURL, string remoteAddress)
        {

            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubNoticeAll] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_SystemNotice" }
                };

                var body = SetNoticeBody(chattingNotice.message, chattingNotice.noticeType, chattingNotice.visibleTime, chattingNotice.visibleCount);

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubNoticeAll", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 채팅 공지
        public async Task PubNoticeAll(ChattingNotice chattingNotice)
        {
            await PubNoticeAll(chattingNotice, "", "", "");
        }
        #endregion

        #region 채팅 공지
        public async Task PubNoticeAll(string message)
        {
            var chattingNotice = new ChattingNotice
            {
                message = message,
                noticeType = ENoticeType.HoldType,
                visibleTime = 3,
                visibleCount = 1
            };
            await PubNoticeAll(chattingNotice);
        }
        #endregion

        #region 유저킥 (관리자 정보 포함)
        public async Task<bool> PubKickUser(string lobbyID, Int64 UID, string userId, string reqURL, string remoteAddress)
        {

            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return false;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubKickUser] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return false;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_UserKick" }
                };

                var body = new Dictionary<string, object>();
                body.Add("UID", UID);

                var destination = $"/topic/{lobbyID}-op";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubKickUser", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }

            return true;
        }
        #endregion

        #region 유저킥
        public async Task<bool> PubKickUser(string lobbyID, Int64 UID)
        {
            return await PubKickUser(lobbyID, UID, "", "", "");
        }
        #endregion

        #region 오픈월드 시간 변경 알림 (관리자 정보 포함)
        public async Task<bool> PubOpenWorldTime(OpenWorldTime openWorldTime, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return false;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubOpenWorldTime] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return false;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_SetOpenWorldTime" }
                };

                var destination = $"/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(openWorldTime), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubOpenWorldTime", JsonConvert.SerializeObject(openWorldTime), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }

            return true;
        }
        #endregion

        #region 오픈월드 시간 변경 알림
        public async Task<bool> PubOpenWorldTime(OpenWorldTime openWorldTime)
        {
            await PubOpenWorldTime(openWorldTime, "", "", "");
            return true;
        }
        #endregion

        private Dictionary<string, object> SetMaintenanceBody(Maintenance maintenance)
        {
            var body = new Dictionary<string, object>
            {
                { "MaintenanceID", maintenance.MaintenanceID },
                { "Platform", maintenance.Platform },
                { "Area", maintenance.Area },
                { "State", maintenance.State },
                { "StartTime", maintenance.StartTime },
                { "EndTime", maintenance.EndTime }
            };
            return body;
        }

        #region 점검 시간변경 알림 (관리자 정보 포함)
        public async Task PubUpdateMaintenance(Maintenance maintenance, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubUpdateMaintenance] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Maintenance" }
                };

                var body = SetMaintenanceBody(maintenance);
                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubUpdateMaintenance", JsonConvert.SerializeObject(maintenance), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 점검 시간변경 알림
        public async Task PubUpdateMaintenance(Maintenance maintenance)
        {
            await PubUpdateMaintenance(maintenance, "", "", "");
        }
        #endregion

        #region 배너공지 변경 알림 (관리자 정보 포함)
        public async Task<bool> PubNoticeBanner(string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return false;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubNoticeBanner] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return false;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_NoticeBanner" }
                };

                var body = new Dictionary<string, object>
                {
                    { "NoticeBannerManager", "reset" }
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubNoticeBanner", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }

            return true;
        }
        #endregion

        #region 배너공지 변경 알림
        public async Task<bool> PubNoticeBanner()
        {
            await PubNoticeBanner("", "", "");
            return true;
        }
        #endregion

        #region 현재 서버시간 받기 (관리자 정보 포함)
        public async Task<bool> PubGetSysTime(string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return false;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubGetSysTime] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return false;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_GetSystemTime" }
                };

                var body = new Dictionary<string, object>
                {
                    { "Destination", "/topic/gm" }
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubGetSysTime", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }
            return true;
        }
        #endregion

        #region 현재 서버시간 받기
        public async Task<bool> PubGetSysTime()
        {
            await PubGetSysTime("", "", "");
            return true;
        }
        #endregion

        #region 서버 시간변경 (관리자 정보 포함)
        public async Task<bool> PubChangeSysTime(DateTime dateTime, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return false;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubChangeSysTime] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return false;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_ChangeSystemTime" }
                };

                var body = new Dictionary<string, object>
                {
                    { "Destination", "/topic/gm" },
                    { "systemTime", dateTime.ToUniversalTime().ToString("o") }
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubChangeSysTime", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }
            return true;
        }
        #endregion

        #region 서버 시간변경
        public async Task<bool> PubChangeSysTime(DateTime dateTime)
        {
            return await PubChangeSysTime(dateTime, "", "", "");
        }
        #endregion

        #region 점검킥 (관리자 정보 포함)
        public async Task PubMaintenanceKick(Defines.MaintenancePlatform platform, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubMaintenanceKick] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_AllUserKick" }
                };

                var body = new Dictionary<string, object>
                {
                    { "Platform", platform }
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubMaintenanceKick", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 점검킥
        public async Task PubMaintenanceKick(Defines.MaintenancePlatform platform)
        {
            await PubMaintenanceKick(platform, "", "", "");
        }
        #endregion

        #region 블럭 컨텐츠 추가 (관리자 정보 포함)
        public async Task PubAddBlockContents(string contentsName, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAddBlockContents] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_AddBlockContents" }
                };

                var body = new Dictionary<string, object>
                {
                    { "contentsName", contentsName }
                };

                var destination = "/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubAddBlockContents", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 블럭 컨텐츠 추가
        public async Task PubAddBlockContents(string contentsName)
        {
            await PubAddBlockContents(contentsName, "", "", "");
        }
        #endregion

        #region 블럭 컨텐츠 삭제 (관리자 정보 포함)
        public async Task PubRemoveBlockContents(string contentsName, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubRemoveBlockContents] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_RemoveBlockContents" }
                };

                var body = new Dictionary<string, object>
                {
                    { "contentsName", contentsName }
                };

                var destination = "/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubRemoveBlockContents", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 블럭 컨텐츠 삭제
        public async Task PubRemoveBlockContents(string contentsName)
        {
            await PubRemoveBlockContents(contentsName, "", "", "");
        }
        #endregion

        #region 블럭 아이피 추가 (관리자 정보 포함)
        public async Task PubAddBlockIP(BlockIP data, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAddBlockIP] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Add_BlockIP" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", data.ID },
                    { "IPAddress", data.IPAddress },
                    { "StartTime", data.StartTime },
                    { "EndTime", data.EndTime },
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubAddBlockIP", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 블럭 아이피 추가
        public async Task PubAddBlockIP(BlockIP data)
        {
            await PubAddBlockIP(data, "", "", "");
        }
        #endregion

        #region 블럭 아이피 삭제 (관리자 정보 포함)
        public async Task PubRemoveBlockIP(BlockIP data, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubRemoveBlockIP] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Remove_BlockIP" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", data.ID },
                    { "IPAddress", data.IPAddress },
                    { "StartTime", data.StartTime },
                    { "EndTime", data.EndTime },
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubRemoveBlockIP", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 블럭 아이피 삭제
        public async Task PubRemoveBlockIP(BlockIP data)
        {
            await PubRemoveBlockIP(data, "", "", "");
        }
        #endregion

        #region 비속어 추가 (관리자 정보 포함)
        public async Task PubAddSlangs(List<string> datas, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (1 > datas.Count)
                {
                    _logger.LogError($"[PubAddSlangs] Empty Data.");
                    return;
                }

                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAddSlangs] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Add_Slang" }
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(datas), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubAddSlangs", JsonConvert.SerializeObject(datas), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 비속어 추가
        public async Task PubAddSlangs(List<string> datas)
        {
            await PubAddSlangs(datas, "", "", "");
        }
        #endregion

        #region 비속어 삭제 (관리자 정보 포함)
        public async Task PubRemoveSlangs(List<string> datas, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (1 > datas.Count)
                {
                    _logger.LogError($"[PubRemoveSlangs] Empty Data.");
                    return;
                }

                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubRemoveSlangs] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Remove_Slang" }
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(datas), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubRemoveSlangs", JsonConvert.SerializeObject(datas), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 비속어 삭제
        public async Task PubRemoveSlangs(List<string> datas)
        {
            await PubRemoveSlangs(datas, "", "", "");
        }
        #endregion

        #region 화이트 리스트 추가 (관리자 정보 포함)
        public async Task PubAddWhiteList(WhiteList data, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAddWhiteList] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Add_WhiteList" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", data.ID },
                    { "DeviceID", data.DeviceID },
                    { "MemberNo", data.MemberNo },
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubAddWhiteList", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 화이트 리스트 추가
        public async Task PubAddWhiteList(WhiteList data)
        {
            await PubAddWhiteList(data, "", "", "");
        }
        #endregion

        #region 화이트 리스트 삭제 (관리자 정보 포함)
        public async Task PubRemoveWhiteList(WhiteList data, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubRemoveWhiteList] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_Remove_WhiteList" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", data.ID },
                    { "DeviceID", data.DeviceID },
                    { "MemberNo", data.MemberNo },
                };

                var destination = "/topic/lobby-all";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubRemoveWhiteList", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 화이트 리스트 삭제
        public async Task PubRemoveWhiteList(WhiteList data)
        {
            await PubRemoveWhiteList(data, "", "", "");
        }
        #endregion

        #region 우편 즉시발송 (관리자 정보 포함)
        public async Task PubSendMail(Mail data, string connectionId, Guid jobId, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubSendMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_SendMail" },
                    { "target-uid", data.UID.ToString() }
                };

                var body = new Dictionary<string, object>
                {
                    { "MailType", data.MailType },
                    { "Title", data.Title },
                    { "Message", data.Message },
                    { "IsBM", data.IsBM },
                    { "ExpireTime", data.ExpireAt },
                    { "RewardList", data.RewardList },
                    { "Destination", "/topic/gm" },
                    { "connectionId", connectionId },
                    { "jobId", jobId }
                };

                var destination = "/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubSendMail", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 우편 즉시발송
        public async Task PubSendMail(Mail data, string connectionId, Guid jobId)
        {
            await PubSendMail(data, connectionId, jobId, "", "", "");
        }
        #endregion

        #region 우편 즉시발송 완료 알림
        public async Task PubAlertSendMail(Int64 uid, Int64 mailID, string connectionId, Guid jobId, string debugString, string destination)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAlertSendMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "LobbyToOptool_SendMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "TargetUID", uid },
                    { "MailID", mailID },
                    { "connectionId", connectionId },
                    { "jobId", jobId },
                    { "debugString", debugString }
                };

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog("PubAlertSendMail", JsonConvert.SerializeObject(body), destination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 이벤트 우편 등록 (관리자 정보 포함)
        public async Task PubInsertEventMail(EventMail data, string connectionId, Guid jobId, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubInsertEventMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_InsertEventMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", 0 },
                    { "MailType", data.MailType },
                    { "Title", data.Title },
                    { "Message", data.Message },
                    { "RewardList", data.RewardList },
                    { "ExpireTime", data.ExpireTime },
                    { "StartTime", data.StartTime },
                    { "EndTime", data.EndTime },
                    { "Destination", "/topic/gm" },
                    { "connectionId", connectionId },
                    { "jobId", jobId }
                };
                _logger.LogInformation(JsonConvert.SerializeObject(body));

                var destination = "/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubInsertEventMail", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 이벤트 우편 등록
        public async Task PubInsertEventMail(EventMail data, string connectionId, Guid jobId)
        {
            await PubInsertEventMail(data, connectionId, jobId, "", "", "");
        }
        #endregion

        #region 이벤트 우편 등록 완료 알림
        public async Task PubAlertInsertEventMail(Int64 id, string connectionId, Guid jobId, string debugString, string destination)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAlertInsertEventMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "LobbyToOptool_InsertEventMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", id },
                    { "connectionId", connectionId },
                    { "debugString", debugString }
                };

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog("PubAlertInsertEventMail", JsonConvert.SerializeObject(body), destination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 이벤트 우편 수정 (관리자 정보 포함)
        public async Task PubUpdateEventMail(EventMail data, string connectionId, Guid jobId, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubUpdateEventMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_UpdateEventMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", data.ID },
                    { "MailType", data.MailType },
                    { "Title", data.Title },
                    { "Message", data.Message },
                    { "RewardList", data.RewardList },
                    { "ExpireTime", data.ExpireTime },
                    { "StartTime", data.StartTime },
                    { "EndTime", data.EndTime },
                    { "Destination", "/topic/gm" },
                    { "connectionId", connectionId },
                    { "jobId", jobId }
                };
                _logger.LogInformation(JsonConvert.SerializeObject(body));

                var destination = "/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubUpdateEventMail", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 이벤트 우편 수정
        public async Task PubUpdateEventMail(EventMail data, string connectionId, Guid jobId)
        {
            await PubUpdateEventMail(data, connectionId, jobId, "", "", "");
        }
        #endregion

        #region 이벤트 우편 수정 완료 알림
        public async Task PubAlertUpdateEventMail(Int64 id, string connectionId, Guid jobId, string debugString, string destination)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAlertUpdateEventMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "LobbyToOptool_UpdateEventMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", id },
                    { "connectionId", connectionId },
                    { "debugString", debugString }
                };

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog("PubAlertUpdateEventMail", JsonConvert.SerializeObject(body), destination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 이벤트 우편 삭제 (관리자 정보 포함)
        public async Task PubRemoveEventMail(EventMail data, string connectionId, Guid jobId, string userId, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubRemoveEventMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToLobby_RemoveEventMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", data.ID },
                    { "MailType", data.MailType },
                    { "Title", data.Title },
                    { "Message", data.Message },
                    { "ExpireTime", data.ExpireTime },
                    { "StartTime", data.StartTime },
                    { "EndTime", data.EndTime },
                    { "Destination", "/topic/gm" },
                    { "connectionId", connectionId },
                    { "jobId", jobId }
                };
                _logger.LogInformation(JsonConvert.SerializeObject(body));

                var destination = "/queue/lobby";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog(userId, "PubRemoveEventMail", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 이벤트 우편 수정
        public async Task PubRemoveEventMail(EventMail data, string connectionId, Guid jobId)
        {
            await PubRemoveEventMail(data, connectionId, jobId, "", "", "");
        }
        #endregion

        #region 이벤트 우편 수정 완료 알림
        public async Task PubAlertRemoveEventMail(Int64 id, string connectionId, Guid jobId, string debugString, string destination)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubAlertRemoveEventMail] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "LobbyToOptool_RemoveEventMail" }
                };

                var body = new Dictionary<string, object>
                {
                    { "ID", id },
                    { "connectionId", connectionId },
                    { "debugString", debugString }
                };

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                _ = _gmLogService.InsertSTOMPLog("PubAlertRemoveEventMail", JsonConvert.SerializeObject(body), destination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 운영툴 채팅 발송 (관리자 정보 포함)
        public async Task PubChattingMessage(ChattingMessage data, string reqURL, string remoteAddress)
        {
            if (null == _stompClient)
            {
                _logger.LogError("StomeClient is null.");
                return;
            }

            try
            {
                if (StompConnectionState.Open != _stompClient.StompState)
                {
                    if (StompConnectionState.Closed == _stompClient.StompState)
                        await _stompClient.Reconnect();
                    _logger.LogError($"[PubChattingMessage] StompConnectionState: {_stompClient.StompState.ToString()}");
                    return;
                }

                var sendHeaders = new Dictionary<string, string>
                {
                    { "cmd", "OptoolToOptool_ChattingMessage" }
                };

                var body = new Dictionary<string, object>
                {
                    { "id", data.id },
                    { "messageType", data.messageType },
                    { "message", data.message },
                    { "sendSigninId", data.sendSigninId },
                    { "sendUserName", data.sendUserName },
                    { "sendConnectionId", data.sendConnectionId },
                    { "targetUserId", data.targetUserId },
                    { "localSendTime", data.localSendTime },
                    { "serverReceiveTime", data.serverReceiveTime }
                };

                _ = _dbHelper.GameManagerWriteOnly.SaveChattingMessage(data);

                var destination = "/topic/gmchat";

                await _stompClient.SendAsync(JsonConvert.SerializeObject(body), destination, sendHeaders);
                //_ = _gmLogService.InsertSTOMPLog(userId, "PubChattingMessage", JsonConvert.SerializeObject(body), destination, reqURL, remoteAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
        #endregion

        #region 운영툴 채팅 발송
        public async Task PubChattingMessage(ChattingMessage data)
        {
            await PubChattingMessage(data, "", "");
        }
        #endregion
    }
}
