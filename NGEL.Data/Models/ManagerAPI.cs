using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NGEL.Data.Tables.Models;

namespace NGEL.Data.Models
{

    [MessagePackObject]
    public class ManagerUserInfosParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerUserInfosResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userInfos")]
        public List<UserInfoForManage> userInfos { get; set; } = new List<UserInfoForManage>();
    }

    [MessagePackObject]
    public class ManagerUserInfoParameters
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerUserInfoResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userInfo")]
        public UserInfo userInfo { get; set; } = new UserInfo();
        [Key("menus")]
        public List<NavMenu> menus { get; set; } = new List<NavMenu>();
    }

    [MessagePackObject]
    public class ManagerUserMenusParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerUserMenusResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("menus")]
        public List<NavMenu> menus { get; set; } = new List<NavMenu>();
    }

    [MessagePackObject]
    public class ManagerAddUserParameters
    {
        [Key("userInfo")]
        public UserInfoForAdd userInfo { get; set; } = new UserInfoForAdd();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerAddUserResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userInfo")]
        public UserInfo userInfo { get; set; } = new UserInfo();
        [Key("emailConfirmId")]
        public Guid emailConfirmId { get; set; } = Guid.Empty;
    }

    [MessagePackObject]
    public class ManagerDeleteUserParameters
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteUserResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerRestoreUserParameters
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerRestoreUserResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerSaveUserRoleParameters
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("roles")]
        public string[] roles { get; set; } = Array.Empty<string>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveUserRoleResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerResetCountFailedSigninParameters
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerResetCountFailedSigninResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerResetLatestSignInParameters
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerResetLatestSignInResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerVersionInfosParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerVersionInfosResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("versionInfos")]
        public List<VersionInfo> versionInfos { get; set; } = new List<VersionInfo>();
    }

    [MessagePackObject]
    public class ManagerVersionInfoParameters
    {
        [Key("version")]
        public string version { get; set; } = "";
        [Key("platform")]
        public Defines.ServiceVersionManagementPlatform? platform { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerVersionInfoResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("versionInfo")]
        public VersionInfo versionInfo { get; set; } = new VersionInfo();
    }

    [MessagePackObject]
    public class ManagerSaveVersionInfosParameters
    {
        [Key("versionInfos")]
        public List<VersionInfo>? versionInfos { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveVersionInfosResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDeleteVersionInfosParameters
    {
        [Key("versionInfos")]
        public List<VersionInfo>? versionInfos { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteVersionInfosResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDownloadVersionInfosParameters
    {
        [Key("version")]
        public string version { get; set; } = "";
        [Key("platform")]
        public Defines.ServiceVersionManagementPlatform? platform { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDownloadVersionInfosResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("fileName")]
        public string fileName { get; set; } = "";
        [Key("contentType")]
        public string contentType { get; set; } = "application/json";
        [Key("base64Data")]
        public string base64Data { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerBlockContentsParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerBlockContentsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("blockContents")]
        public List<BlockContent> blockContents { get; set; } = new List<BlockContent>();
    }

    [MessagePackObject]
    public class ManagerSaveBlockContentsParameters
    {
        [Key("blockContents")]
        public List<BlockContent>? blockContents { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveBlockContentsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDeleteBlockContentsParameters
    {
        [Key("blockContents")]
        public List<BlockContent>? blockContents { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteBlockContentsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerBiskitLogsParameters
    {
        [Key("startDate")]
        public string? startDate { get; set; } = null;
        [Key("endDate")]
        public string? endDate { get; set; } = null;
        [Key("userSearchType")]
        public Defines.UserSearchType userSearchType { get; set; } = 0;
        [Key("userSearchValue")]
        public string? userSearchValue { get; set; } = null;
        [Key("eventIdSearchType")]
        public List<string>? eventIdSearchType { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerBiskitLogsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("biskitLogs")]
        public List<BiskitLog> biskitLogs { get; set; } = new List<BiskitLog>();
    }

    [MessagePackObject]
    public class ManagerDownloadGameLogsParameters
    {
        [Key("startTime")]
        public string? startTime { get; set; } = null;
        [Key("endTime")]
        public string? endTime { get; set; } = null;
        [Key("userSearchType")]
        public Defines.UserSearchType userSearchType { get; set; } = 0;
        [Key("userSearchValue")]
        public string? userSearchValue { get; set; } = null;
        [Key("gameLogSearchType")]
        public Defines.GameLogSearchType gameLogSearchType { get; set; } = 0;
        [Key("gameLogSearchValue")]
        public string? gameLogSearchValue { get; set; } = null;
        [Key("eventIdSearchType")]
        public List<string>? eventIdSearchType { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDownloadGameLogsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("fileName")]
        public string fileName { get; set; } = "";
        [Key("contentType")]
        public string contentType { get; set; } = "application/json";
        [Key("base64Data")]
        public string base64Data { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerGMCombinedLogsParameters
    {
        [Key("startTime")]
        public string? startTime { get; set; } = null;
        [Key("endTime")]
        public string? endTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerGMCombinedLogsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("gmCombinedLogs")]
        public List<GMCombinedLog> gmCombinedLogs { get; set; } = new List<GMCombinedLog>();
    }

    [MessagePackObject]
    public class ManagerDownloadManagerLogsParameters
    {
        [Key("name")]
        public string? name { get; set; } = null;
        [Key("url")]
        public string? url { get; set; } = null;
        [Key("startTime")]
        public string? startTime { get; set; } = null;
        [Key("endTime")]
        public string? endTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDownloadManagerLogsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("fileName")]
        public string fileName { get; set; } = "";
        [Key("contentType")]
        public string contentType { get; set; } = "application/json";
        [Key("base64Data")]
        public string base64Data { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerChatLogsParameters
    {
        [Key("startTime")]
        public string? startTime { get; set; } = null;
        [Key("endTime")]
        public string? endTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerChatLogsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("chatLogs")]
        public List<ChatLog> chatLogs { get; set; } = new List<ChatLog>();
    }

    [MessagePackObject]
    public class ManagerChangePasswordParameters
    {
        [Key("password")]
        public string password { get; set; } = "";
        [Key("newPassword")]
        public string newPassword { get; set; } = "";
        [Key("newPasswordConfirm")]
        public string newPasswordConfirm { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerChangePasswordResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerSetRefreshPasswordParameters
    {
        [Key("email")]
        public string email { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSetRefreshPasswordResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerWhiteListParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerWhiteListResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("resultData")]
        public List<WhiteList> resultData { get; set; } = new List<WhiteList>();
    }

    [MessagePackObject]
    public class ManagerSaveWhiteListParameters
    {
        [Key("paramData")]
        public List<WhiteList> paramData { get; set; } = new List<WhiteList>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveWhiteListResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDeleteWhiteListParameters
    {
        [Key("paramData")]
        public List<Int64> paramData { get; set; } = new List<Int64>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteWhiteListResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerBlockIPParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerBlockIPResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("resultData")]
        public List<BlockIP> resultData { get; set; } = new List<BlockIP>();
    }

    [MessagePackObject]
    public class ManagerSaveBlockIPParameters
    {
        [Key("paramData")]
        public List<BlockIP> paramData { get; set; } = new List<BlockIP>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveBlockIPResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDeleteBlockIPParameters
    {
        [Key("paramData")]
        public List<Int64> paramData { get; set; } = new List<Int64>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteBlockIPResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerSlangParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSlangResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("resultData")]
        public List<Slang> resultData { get; set; } = new List<Slang>();
    }

    [MessagePackObject]
    public class ManagerSaveSlangParameters
    {
        [Key("paramData")]
        public List<Slang> paramData { get; set; } = new List<Slang>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveSlangResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDeleteSlangParameters
    {
        [Key("paramData")]
        public List<Int64> paramData { get; set; } = new List<Int64>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteSlangResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerFirewallsParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerFirewallsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("resultData")]
        public List<Firewall> resultData { get; set; } = new List<Firewall>();
    }

    [MessagePackObject]
    public class ManagerSaveFirewallParameters
    {
        [Key("paramData")]
        public List<Firewall> paramData { get; set; } = new List<Firewall>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveFirewallResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerDeleteFirewallParameters
    {
        [Key("paramData")]
        public List<string> paramData { get; set; } = new List<string>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerDeleteFirewallResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
    }

    [MessagePackObject]
    public class ManagerCCUParameters
    {
        [Key("startTime")]
        public DateTime? startTime { get; set; } = null;
        [Key("endTime")]
        public DateTime? endTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerCCUResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("resultData")]
        public List<CCU> resultData { get; set; } = new List<CCU>();
    }

    [MessagePackObject]
    public class ManagerManagerServerLogsParameters
    {
        [Key("startTime")]
        public string? startTime { get; set; } = null;
        [Key("endTime")]
        public string? endTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerManagerServerLogsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("managerServerLogs")]
        public List<ManagerServerLog> managerServerLogs { get; set; } = new List<ManagerServerLog>();
    }

    [MessagePackObject]
    public class ManagerExpireLogSettingParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerExpireLogSettingResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("expireLogSetting")]
        public Settings expireLogSetting { get; set; } = new Settings();
    }

    [MessagePackObject]
    public class ManagerSaveExpireLogSettingParameters
    {
        [Key("value")]
        public int value { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveExpireLogSettingResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class ManagerSaveVisitParameters
    {
        [Key("session")]
        public string session { get; set; } = "";
        [Key("fp")]
        public UInt64 fp { get; set; } = 0;
        [Key("deviceType")]
        public string deviceType { get; set; } = "";
        [Key("deviceVendor")]
        public string deviceVendor { get; set; } = "";
        [Key("deviceModel")]
        public string deviceModel { get; set; } = "";
        [Key("agent")]
        public string agent { get; set; } = "";
        [Key("browser")]
        public string browser { get; set; } = "";
        [Key("browserVersion")]
        public string browserVersion { get; set; } = "";
        [Key("engine")]
        public string engine { get; set; } = "";
        [Key("engineVersion")]
        public string engineVersion { get; set; } = "";
        [Key("os")]
        public string os { get; set; } = "";
        [Key("osVersion")]
        public string osVersion { get; set; } = "";
        [Key("host")]
        public string host { get; set; } = "";
        [Key("parameter")]
        public string parameter { get; set; } = "";
        [Key("path")]
        public string path { get; set; } = "";
        [Key("title")]
        public string title { get; set; } = "";
        [Key("localTime")]
        public DateTime localTime { get; set; } = DateTime.UtcNow;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ManagerSaveVisitResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

}
