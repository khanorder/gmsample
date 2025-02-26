using System;
using System.Collections.Generic;
using MessagePack;
using NGEL.Data;
using NGEL.Data.Models;
using NGEL.Data.Tables.Models;

namespace NGEL.Data.Vars
{

    [MessagePackObject]
    public class PConnectedAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("serverId")]
        public Guid serverId { get; set; } = Guid.Empty;
        [Key("serverVersion")]
        public string serverVersion { get; set; } = "";
        [Key("recommendClientMasterVersion")]
        public int recommendClientMasterVersion { get; set; } = 0;
        [Key("recommendClientUpdateVersion")]
        public int recommendClientUpdateVersion { get; set; } = 0;
        [Key("recommendClientMaintenanceVersion")]
        public int recommendClientMaintenanceVersion { get; set; } = 0;
    }

    [MessagePackObject]
    public class PCheckAuthenticationAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("token")]
        public string? token { get; set; } = "";
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("dataTable")]
        public DataTable? dataTable { get; set; } = null;
    }

    [MessagePackObject]
    public class PCheckConnectionAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("token")]
        public string? token { get; set; } = "";
    }

    [MessagePackObject]
    public class PCommonNoticeAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("message")]
        public string? message { get; set; } = "";
    }

    [MessagePackObject]
    public class PSignInAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("token")]
        public string token { get; set; } = "";
    }

    [MessagePackObject]
    public class PSignInLDAPAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("token")]
        public string token { get; set; } = "";
    }

    [MessagePackObject]
    public class PSignInEmailAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
        [Key("token")]
        public string token { get; set; } = "";
    }

    [MessagePackObject]
    public class PDataTableAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("dataTable")]
        public DataTable? dataTable { get; set; } = null;
    }

    [MessagePackObject]
    public class PSendGameMailResultAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("ackNoticeType")]
        public Defines.AckNoticeType ackNoticeType { get; set; } = 0;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
    }

    [MessagePackObject]
    public class PInsertEventMailResultAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("ackNoticeType")]
        public Defines.AckNoticeType ackNoticeType { get; set; } = 0;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
    }

    [MessagePackObject]
    public class PUpdateEventMailResultAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("ackNoticeType")]
        public Defines.AckNoticeType ackNoticeType { get; set; } = 0;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
    }

    [MessagePackObject]
    public class PRemoveEventMailResultAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("ackNoticeType")]
        public Defines.AckNoticeType ackNoticeType { get; set; } = 0;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
    }

    [MessagePackObject]
    public class PSendUserJobAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("userJob")]
        public UserJob userJob { get; set; } = new UserJob();
    }

    [MessagePackObject]
    public class PSignOutAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
    }

    [MessagePackObject]
    public class PDeniedAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
    }

    [MessagePackObject]
    public class PSendChattingMessageResultAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("messageId")]
        public Guid messageId { get; set; } = Guid.Empty;
    }

    [MessagePackObject]
    public class PReceiveChattingMessageAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("chattingMessage")]
        public ChattingMessage? chattingMessage { get; set; } = null;
    }

    [MessagePackObject]
    public class PTestAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("isSigned")]
        public bool isSigned { get; set; } = false;
    }

    [MessagePackObject]
    public class PNoticeAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
    }

    [MessagePackObject]
    public class PReceiveChattingMessageAllAck
    {
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("chattingMessage")]
        public ChattingMessage? chattingMessage { get; set; } = null;
    }

    [MessagePackObject]
    public class PCheckAuthenticationReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
    }

    [MessagePackObject]
    public class PCheckConnectionReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
    }

    [MessagePackObject]
    public class PSignInReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("code")]
        public string code { get; set; } = "";
    }

    [MessagePackObject]
    public class PSignInLDAPReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("email")]
        public string email { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
    }

    [MessagePackObject]
    public class PSignInEmailReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("email")]
        public string email { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
    }

    [MessagePackObject]
    public class PSignOutReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
    }

    [MessagePackObject]
    public class PSendGameMailReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("userUIDs")]
        public List<Int64> userUIDs { get; set; } = new List<Int64>();
        [Key("mailInput")]
        public MailInput mailInput { get; set; } = new MailInput();
    }

    [MessagePackObject]
    public class PInsertEventMailReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("eventMail")]
        public EventMail eventMail { get; set; } = new EventMail();
    }

    [MessagePackObject]
    public class PUpdateEventMailReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("eventMail")]
        public EventMail eventMail { get; set; } = new EventMail();
    }

    [MessagePackObject]
    public class PRemoveEventMailReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("eventMail")]
        public EventMail eventMail { get; set; } = new EventMail();
    }

    [MessagePackObject]
    public class PSendChattingMessageReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("chattingMessage")]
        public ChattingMessage? chattingMessage { get; set; } = null;
    }

    [MessagePackObject]
    public class PTestReq
    {
        [Key("token")]
        public string? token { get; set; } = null;
        [Key("reqPathName")]
        public string? reqPathName { get; set; } = null;
        [Key("userId")]
        public string userId { get; set; } = "";
    }

}
