// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

public enum EStompMessageType : uint
{
  None = 0,
  LobbyToDedi_OpenWorldUserData = 10000,
  LobbyToDedi_GoldClashUserData = 10001,
  LobbyToDedi_RaidUserData = 10002,
  LobbyToDedi_SetGameSetting = 10003,
  LobbyToDedi_ExecCommand = 10004,
  LobbyToDedi_OpenWorldTime = 10005,
  LobbyToDedi_EntitlementAdd = 10006,
  LobbyToDedi_WaitRoomUserData = 10007,
  LobbyToDedi_WaitRoomFinish = 10008,
  LobbyToDedi_HeroData = 10009,
  LobbyToDedi_HeroSkinPresetData = 10010,
  DediToLobby_GoldClashStart = 15000,
  DediToLobby_GoldClashFinish = 15001,
  DediToLobby_RaidStart = 15002,
  DediToLobby_RaidFinish = 15003,
  DediToLobby_OpenWorldTime = 15004,
  DediToLobby_OpenWorldUserData_Req = 15005,
  DediToLobby_GoldClashUserData_Req = 15006,
  DediToLobby_WaitRoomUserData_Req = 15007,
  DediToLobby_WaitRoomFinish_Req = 15008,
  DediToLobby_HeroData_Req = 15009,
  DediToLobby_HeroSkinPresetData_Req = 15010,
  LobbyToLobby_DuplicateLoginDisconnect = 20000,
  LobbyToLobby_FriendDelete = 20001,
  LobbyToLobby_FriendAccept = 20002,
  LobbyToLobby_FriendRequest = 20003,
  LobbyToLobby_FriendChatSend = 20004,
  LobbyToLobby_FriendChatDelete = 20005,
  LobbyToLobby_FriendState = 20006,
  LobbyToLobby_PartyInviteUser = 20007,
  LobbyToLobby_PartyInviteAnswer = 20008,
  LobbyToLobby_PartyJoinAccept = 20009,
  LobbyToLobby_PartyJoinReject = 20010,
  LobbyToLobby_PartyWelcomeNewUser = 20011,
  LobbyToLobby_PartyLeaveMember = 20012,
  LobbyToLobby_PartyLeaveToMaster = 20013,
  LobbyToLobby_PartyKickMember = 20014,
  LobbyToLobby_PartyChangeMaster = 20015,
  LobbyToLobby_CustomGameInviteUser = 20016,
  LobbyToLobby_CustomGameInviteAnswer = 20017,
  LobbyToLobby_CustomGameJoinAccept = 20018,
  LobbyToLobby_CustomGameJoinReject = 20019,
  LobbyToLobby_CustomGameWelcomeNewUser = 20020,
  LobbyToLobby_CustomGameLeaveMember = 20021,
  LobbyToLobby_CustomGameBrokenRoom = 20022,
  LobbyToLobby_CustomGameLeaveToMaster = 20023,
  LobbyToLobby_CustomGameKickMember = 20024,
  LobbyToLobby_CustomGameFollowMaster = 20025,
  LobbyToLobby_CustomGameChangeSlotNo = 20026,
  LobbyToLobby_MatchingStartMatch = 20027,
  LobbyToLobby_MatchingStopMatchToMaster = 20028,
  LobbyToLobby_MatchingCancelMatch = 20029,
  LobbyToLobby_MatchingDelayMatch = 20030,
  LobbyToLobby_EnterMultiMode = 20031,
  LobbyToLobby_ModifyNickName = 20032,
  LobbyToLobby_PenaltyReport = 20033,
  LobbyToLobby_OptoolEventMail = 20034,
  LobbyToLobby_OptoolEventMailRemove = 20035,
  SnsToLobby_MatchmakingSucceeded = 25000,
  SnsToLobby_MatchmakingCancelled = 25001,
  SnsToLobby_PlacementFulfilled = 25002,
  SnsToLobby_PlacementCancelled = 25003,
  SnsToLobby_PotentialMatchCreated = 25004,
  SnsToLobby_AcceptMatch = 25005,
  SnsToLobby_AcceptMatchCompleted = 25006,
  SnsToLobby_BillingNotice = 25007,
  SnsToLobby_CouponNotice = 25008,
  SnsToLobby_CouponItemList = 25009,
  SnsToLobby_WebEventRewardNotice = 25010,
  SnsToLobby_WebEventRewardTableList = 25011,
  SnsToLobby_UserOpenWorldEnter = 25012,
  SnsToLobby_UserOpenWorldLeave = 25013,
  SnsToLobby_SeasonPassList = 25014,
  LobbyToOptool_GetSystemTime = 26000,
  LobbyToOptool_ChangeSystemTime = 26001,
  LobbyToOptool_SendMail = 26002,
  LobbyToOptool_InsertEventMail = 26003,
  LobbyToOptool_UpdateEventMail = 26004,
  LobbyToOptool_RemoveEventMail = 26005,
  OptoolToLobby_UserKick = 27000,
  OptoolToLobby_GetSystemTime = 27001,
  OptoolToLobby_ChangeSystemTime = 27002,
  OptoolToLobby_SystemNotice = 27003,
  OptoolToLobby_Maintenance = 27004,
  OptoolToLobby_AllUserKick = 27005,
  OptoolToLobby_SetOpenWorldTime = 27006,
  OptoolToLobby_AddBlockContents = 27007,
  OptoolToLobby_RemoveBlockContents = 27008,
  OptoolToLobby_NoticeBanner = 27009,
  OptoolToLobby_Add_BlockIP = 27010,
  OptoolToLobby_Remove_BlockIP = 27011,
  OptoolToLobby_Add_Slang = 27012,
  OptoolToLobby_Remove_Slang = 27013,
  OptoolToLobby_Add_WhiteList = 27014,
  OptoolToLobby_Remove_WhiteList = 27015,
  OptoolToLobby_SendMail = 27016,
  OptoolToLobby_InsertEventMail = 27017,
  OptoolToLobby_UpdateEventMail = 27018,
  OptoolToLobby_RemoveEventMail = 27019,
  DediToSns_ServerReady = 28000,
  DediToSns_ServerKill = 28001,
  DediToSns_UserEnter = 28002,
  DediToSns_UserLeave = 28003,
  LobbyToChat_NickNameChange = 29000,
};


}
