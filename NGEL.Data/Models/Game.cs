using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class ActiveUserAccount
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("Nick")]
        public string Nick { get; set; } = "";
        [Key("UserLevel")]
        public int UserLevel { get; set; } = 0;
        [Key("MemberNo")]
        public UInt64? MemberNo { get; set; } = null;
        [Key("LogoutAt")]
        public Int64 LogoutAt { get; set; } = 0;
        [Key("UserState")]
        public Defines.UserState UserState { get; set; } = 0;
        [Key("ProfileIconID")]
        public int ProfileIconID { get; set; } = 0;
        [Key("LobbyID")]
        public string LobbyID { get; set; } = "";

        public ActiveUserAccount Clone()
        {
            var clone = new ActiveUserAccount();
            clone.UID = this.UID;
            clone.Nick = this.Nick;
            clone.UserLevel = this.UserLevel;
            clone.MemberNo = this.MemberNo;
            clone.LogoutAt = this.LogoutAt;
            clone.UserState = this.UserState;
            clone.ProfileIconID = this.ProfileIconID;
            clone.LobbyID = this.LobbyID;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(ActiveUserAccount rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class ActiveUser
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("LobbyID")]
        public string LobbyID { get; set; } = "";
        [Key("UserState")]
        public EUserState UserState { get; set; } = 0;
        [Key("MatchState")]
        public int MatchState { get; set; } = 0;
        [Key("OpenWorldID")]
        public string OpenWorldID { get; set; } = "";

        public ActiveUser Clone()
        {
            var clone = new ActiveUser();
            clone.UID = this.UID;
            clone.LobbyID = this.LobbyID;
            clone.UserState = this.UserState;
            clone.MatchState = this.MatchState;
            clone.OpenWorldID = this.OpenWorldID;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(ActiveUser rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class OpenWorldTime
    {
        [Key("Minutes")]
        public int Minutes { get; set; } = 0;
        [Key("Multiply")]
        public double Multiply { get; set; } = 0;
        [Key("StartAt")]
        public int StartAt { get; set; } = 0;

        public OpenWorldTime Clone()
        {
            var clone = new OpenWorldTime();
            clone.Minutes = this.Minutes;
            clone.Multiply = this.Multiply;
            clone.StartAt = this.StartAt;
            return clone;
        }
    }

    [MessagePackObject]
    public class GoldClashTime
    {
        [Key("StartHour")]
        public int StartHour { get; set; } = 0;
        [Key("StartMinute")]
        public int StartMinute { get; set; } = 0;
        [Key("EndHour")]
        public int EndHour { get; set; } = 0;
        [Key("EndMinute")]
        public int EndMinute { get; set; } = 0;

        public GoldClashTime Clone()
        {
            var clone = new GoldClashTime();
            clone.StartHour = this.StartHour;
            clone.StartMinute = this.StartMinute;
            clone.EndHour = this.EndHour;
            clone.EndMinute = this.EndMinute;
            return clone;
        }
    }

    [MessagePackObject]
    public class LobbyServerTime
    {
        [Key("lobbyID")]
        public string lobbyID { get; set; } = "";
        [Key("serverTime")]
        public DateTime serverTime { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public LobbyServerTime Clone()
        {
            var clone = new LobbyServerTime();
            clone.lobbyID = this.lobbyID;
            clone.serverTime = this.serverTime;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(string lobbyID)
        {
           return this.lobbyID == lobbyID;
        }

        public bool CompareKey(LobbyServerTime rdata)
        {
           return lobbyID == rdata.lobbyID;
        }
    }

    [MessagePackObject]
    public class GameRankingScore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("UserAccount")]
        public UserAccount? UserAccount { get; set; } = null;
        [Key("Score")]
        public double Score { get; set; } = 0;

        public GameRankingScore Clone()
        {
            var clone = new GameRankingScore();
            clone.UID = this.UID;
            clone.UserAccount = this.UserAccount;
            clone.Score = this.Score;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(GameRankingScore rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class Account
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("AccountType")]
        public Defines.GameAuthType AccountType { get; set; } = 0;
        [Key("AccountID")]
        public string AccountID { get; set; } = "";
        [Key("MemberNo")]
        public UInt64 MemberNo { get; set; } = 0;
        [Key("WorldID")]
        public string WorldID { get; set; } = "";
        [Key("IsLeave")]
        public bool IsLeave { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Account Clone()
        {
            var clone = new Account();
            clone.UID = this.UID;
            clone.AccountType = this.AccountType;
            clone.AccountID = this.AccountID;
            clone.MemberNo = this.MemberNo;
            clone.WorldID = this.WorldID;
            clone.IsLeave = this.IsLeave;
            clone.CreateAt = this.CreateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, Defines.GameAuthType AccountType, string AccountID)
        {
           return this.UID == UID
                && this.AccountType == AccountType
                && this.AccountID == AccountID;
        }

        public bool CompareKey(Account rdata)
        {
           return UID == rdata.UID
                && AccountType == rdata.AccountType
                && AccountID == rdata.AccountID;
        }
    }

    [MessagePackObject]
    public class Achievement
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("AchievementID")]
        public Int64 AchievementID { get; set; } = 0;
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Key("CompleteAt")]
        public Int64 CompleteAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Achievement Clone()
        {
            var clone = new Achievement();
            clone.UID = this.UID;
            clone.AchievementID = this.AchievementID;
            clone.Count = this.Count;
            clone.CompleteAt = this.CompleteAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 AchievementID)
        {
           return this.UID == UID
                && this.AchievementID == AchievementID;
        }

        public bool CompareKey(Achievement rdata)
        {
           return UID == rdata.UID
                && AchievementID == rdata.AchievementID;
        }
    }

    [MessagePackObject]
    public class Artifact
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ArtifactID")]
        public int ArtifactID { get; set; } = 0;
        [Key("Enhance")]
        public int Enhance { get; set; } = 0;
        [Key("Count")]
        public int Count { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Artifact Clone()
        {
            var clone = new Artifact();
            clone.UID = this.UID;
            clone.ArtifactID = this.ArtifactID;
            clone.Enhance = this.Enhance;
            clone.Count = this.Count;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, int ArtifactID)
        {
           return this.UID == UID
                && this.ArtifactID == ArtifactID;
        }

        public bool CompareKey(Artifact rdata)
        {
           return UID == rdata.UID
                && ArtifactID == rdata.ArtifactID;
        }
    }

    [MessagePackObject]
    public class ArtifactDeck
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SlotID")]
        public int SlotID { get; set; } = 0;
        [Key("DeckName")]
        public string DeckName { get; set; } = "";
        [Key("ArtifactID0")]
        public int ArtifactID0 { get; set; } = 0;
        [Key("ArtifactID1")]
        public int ArtifactID1 { get; set; } = 0;
        [Key("ArtifactID2")]
        public int ArtifactID2 { get; set; } = 0;
        [Key("ArtifactID3")]
        public int ArtifactID3 { get; set; } = 0;
        [Key("ArtifactID4")]
        public int ArtifactID4 { get; set; } = 0;
        [Key("ArtifactID5")]
        public int ArtifactID5 { get; set; } = 0;
        [Key("ArtifactID6")]
        public int ArtifactID6 { get; set; } = 0;
        [Key("ArtifactID7")]
        public int ArtifactID7 { get; set; } = 0;
        [Key("ArtifactID8")]
        public int ArtifactID8 { get; set; } = 0;
        [Key("Enhance0")]
        public int? Enhance0 { get; set; } = null;
        [Key("Enhance1")]
        public int? Enhance1 { get; set; } = null;
        [Key("Enhance2")]
        public int? Enhance2 { get; set; } = null;
        [Key("Enhance3")]
        public int? Enhance3 { get; set; } = null;
        [Key("Enhance4")]
        public int? Enhance4 { get; set; } = null;
        [Key("Enhance5")]
        public int? Enhance5 { get; set; } = null;
        [Key("Enhance6")]
        public int? Enhance6 { get; set; } = null;
        [Key("Enhance7")]
        public int? Enhance7 { get; set; } = null;
        [Key("Enhance8")]
        public int? Enhance8 { get; set; } = null;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public ArtifactDeck Clone()
        {
            var clone = new ArtifactDeck();
            clone.UID = this.UID;
            clone.SlotID = this.SlotID;
            clone.DeckName = this.DeckName;
            clone.ArtifactID0 = this.ArtifactID0;
            clone.ArtifactID1 = this.ArtifactID1;
            clone.ArtifactID2 = this.ArtifactID2;
            clone.ArtifactID3 = this.ArtifactID3;
            clone.ArtifactID4 = this.ArtifactID4;
            clone.ArtifactID5 = this.ArtifactID5;
            clone.ArtifactID6 = this.ArtifactID6;
            clone.ArtifactID7 = this.ArtifactID7;
            clone.ArtifactID8 = this.ArtifactID8;
            clone.Enhance0 = this.Enhance0;
            clone.Enhance1 = this.Enhance1;
            clone.Enhance2 = this.Enhance2;
            clone.Enhance3 = this.Enhance3;
            clone.Enhance4 = this.Enhance4;
            clone.Enhance5 = this.Enhance5;
            clone.Enhance6 = this.Enhance6;
            clone.Enhance7 = this.Enhance7;
            clone.Enhance8 = this.Enhance8;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int SlotID)
        {
           return this.UID == UID
                && this.SlotID == SlotID;
        }

        public bool CompareKey(ArtifactDeck rdata)
        {
           return UID == rdata.UID
                && SlotID == rdata.SlotID;
        }
    }

    [MessagePackObject]
    public class Asset
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("AssetID")]
        public int AssetID { get; set; } = 0;
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Asset Clone()
        {
            var clone = new Asset();
            clone.UID = this.UID;
            clone.AssetID = this.AssetID;
            clone.Count = this.Count;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, int AssetID)
        {
           return this.UID == UID
                && this.AssetID == AssetID;
        }

        public bool CompareKey(Asset rdata)
        {
           return UID == rdata.UID
                && AssetID == rdata.AssetID;
        }
    }

    [MessagePackObject]
    public class Attendance
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("AttendanceID")]
        public int AttendanceID { get; set; } = 0;
        [Key("AttendanceDay")]
        public int AttendanceDay { get; set; } = 0;
        [Key("RewardState")]
        public string RewardState { get; set; } = "";
        [Key("LastAttendanceAt")]
        public int LastAttendanceAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Attendance Clone()
        {
            var clone = new Attendance();
            clone.UID = this.UID;
            clone.AttendanceID = this.AttendanceID;
            clone.AttendanceDay = this.AttendanceDay;
            clone.RewardState = this.RewardState;
            clone.LastAttendanceAt = this.LastAttendanceAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int AttendanceID)
        {
           return this.UID == UID
                && this.AttendanceID == AttendanceID;
        }

        public bool CompareKey(Attendance rdata)
        {
           return UID == rdata.UID
                && AttendanceID == rdata.AttendanceID;
        }
    }

    [MessagePackObject]
    public class BattleStore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("StoreID")]
        public int StoreID { get; set; } = 0;
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Key("ExpireAt")]
        public Int64 ExpireAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public BattleStore Clone()
        {
            var clone = new BattleStore();
            clone.UID = this.UID;
            clone.StoreID = this.StoreID;
            clone.Count = this.Count;
            clone.ExpireAt = this.ExpireAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int StoreID)
        {
           return this.UID == UID
                && this.StoreID == StoreID;
        }

        public bool CompareKey(BattleStore rdata)
        {
           return UID == rdata.UID
                && StoreID == rdata.StoreID;
        }
    }

    [MessagePackObject]
    public class Collection
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("CollectionType")]
        public ECollectionType CollectionType { get; set; } = 0;
        [Key("CollectionID")]
        public Int64 CollectionID { get; set; } = 0;
        [Key("IsRewarded")]
        public bool IsRewarded { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Collection Clone()
        {
            var clone = new Collection();
            clone.UID = this.UID;
            clone.CollectionType = this.CollectionType;
            clone.CollectionID = this.CollectionID;
            clone.IsRewarded = this.IsRewarded;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, ECollectionType CollectionType, Int64 CollectionID)
        {
           return this.UID == UID
                && this.CollectionType == CollectionType
                && this.CollectionID == CollectionID;
        }

        public bool CompareKey(Collection rdata)
        {
           return UID == rdata.UID
                && CollectionType == rdata.CollectionType
                && CollectionID == rdata.CollectionID;
        }
    }

    [MessagePackObject]
    public class Craft
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("CraftID")]
        public int CraftID { get; set; } = 0;
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Craft Clone()
        {
            var clone = new Craft();
            clone.UID = this.UID;
            clone.CraftID = this.CraftID;
            clone.Count = this.Count;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int CraftID)
        {
           return this.UID == UID
                && this.CraftID == CraftID;
        }

        public bool CompareKey(Craft rdata)
        {
           return UID == rdata.UID
                && CraftID == rdata.CraftID;
        }
    }

    [MessagePackObject]
    public class DataChipStore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("StoreID")]
        public int StoreID { get; set; } = 0;
        [Key("HaveCount")]
        public Int64 HaveCount { get; set; } = 0;
        [Key("LastResetAt")]
        public Int64 LastResetAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public DataChipStore Clone()
        {
            var clone = new DataChipStore();
            clone.UID = this.UID;
            clone.StoreID = this.StoreID;
            clone.HaveCount = this.HaveCount;
            clone.LastResetAt = this.LastResetAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int StoreID)
        {
           return this.UID == UID
                && this.StoreID == StoreID;
        }

        public bool CompareKey(DataChipStore rdata)
        {
           return UID == rdata.UID
                && StoreID == rdata.StoreID;
        }
    }

    [MessagePackObject]
    public class Entitlement
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("EntitlementID")]
        public int EntitlementID { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;

        public Entitlement Clone()
        {
            var clone = new Entitlement();
            clone.UID = this.UID;
            clone.EntitlementID = this.EntitlementID;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            return clone;
        }

        public bool CompareKey(Int64 UID, int EntitlementID)
        {
           return this.UID == UID
                && this.EntitlementID == EntitlementID;
        }

        public bool CompareKey(Entitlement rdata)
        {
           return UID == rdata.UID
                && EntitlementID == rdata.EntitlementID;
        }
    }

    [MessagePackObject]
    public class EventStore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("StoreID")]
        public int StoreID { get; set; } = 0;
        [Key("BuyCount")]
        public Int64 BuyCount { get; set; } = 0;
        [Key("NextResetAt")]
        public Int64 NextResetAt { get; set; } = 0;
        [Key("ExpireAt")]
        public Int64 ExpireAt { get; set; } = 0;
        [Key("SeasonNum")]
        public Int64 SeasonNum { get; set; } = 0;
        [Key("IsExpire")]
        public bool IsExpire { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public EventStore Clone()
        {
            var clone = new EventStore();
            clone.UID = this.UID;
            clone.StoreID = this.StoreID;
            clone.BuyCount = this.BuyCount;
            clone.NextResetAt = this.NextResetAt;
            clone.ExpireAt = this.ExpireAt;
            clone.SeasonNum = this.SeasonNum;
            clone.IsExpire = this.IsExpire;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int StoreID)
        {
           return this.UID == UID
                && this.StoreID == StoreID;
        }

        public bool CompareKey(EventStore rdata)
        {
           return UID == rdata.UID
                && StoreID == rdata.StoreID;
        }
    }

    [MessagePackObject]
    public class Expression
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ExpressionID")]
        public int ExpressionID { get; set; } = 0;
        [Key("ExpressionType")]
        public EExpressionType ExpressionType { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Expression Clone()
        {
            var clone = new Expression();
            clone.UID = this.UID;
            clone.ExpressionID = this.ExpressionID;
            clone.ExpressionType = this.ExpressionType;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int ExpressionID)
        {
           return this.UID == UID
                && this.ExpressionID == ExpressionID;
        }

        public bool CompareKey(Expression rdata)
        {
           return UID == rdata.UID
                && ExpressionID == rdata.ExpressionID;
        }
    }

    [MessagePackObject]
    public class ExpressionPreset
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("HeroID")]
        public int HeroID { get; set; } = 0;
        [Key("Preset")]
        public List<Int64> Preset { get; set; } = new List<Int64>();
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public ExpressionPreset Clone()
        {
            var clone = new ExpressionPreset();
            clone.UID = this.UID;
            clone.HeroID = this.HeroID;
            clone.Preset.AddRange(this.Preset);
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int HeroID)
        {
           return this.UID == UID
                && this.HeroID == HeroID;
        }

        public bool CompareKey(ExpressionPreset rdata)
        {
           return UID == rdata.UID
                && HeroID == rdata.HeroID;
        }
    }

    [MessagePackObject]
    public class Friend
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("FriendUID")]
        public Int64 FriendUID { get; set; } = 0;
        [Key("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
        [Key("FriendMemberNo")]
        public UInt64 FriendMemberNo { get; set; } = 0;
        [Key("FriendNick")]
        public string FriendNick { get; set; } = "";

        public Friend Clone()
        {
            var clone = new Friend();
            clone.UID = this.UID;
            clone.FriendUID = this.FriendUID;
            clone.IsDeleted = this.IsDeleted;
            clone.FriendMemberNo = this.FriendMemberNo;
            clone.FriendNick = this.FriendNick;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(Friend rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class GlitchStore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("StoreID")]
        public int StoreID { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public GlitchStore Clone()
        {
            var clone = new GlitchStore();
            clone.UID = this.UID;
            clone.StoreID = this.StoreID;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int StoreID)
        {
           return this.UID == UID
                && this.StoreID == StoreID;
        }

        public bool CompareKey(GlitchStore rdata)
        {
           return UID == rdata.UID
                && StoreID == rdata.StoreID;
        }
    }

    [MessagePackObject]
    public class SilverMedalStore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("StoreID")]
        public int StoreID { get; set; } = 0;
        [Key("BuyCount")]
        public Int64 BuyCount { get; set; } = 0;
        [Key("NextResetAt")]
        public Int64 NextResetAt { get; set; } = 0;
        [Key("SeasonNum")]
        public Int64 SeasonNum { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public SilverMedalStore Clone()
        {
            var clone = new SilverMedalStore();
            clone.UID = this.UID;
            clone.StoreID = this.StoreID;
            clone.BuyCount = this.BuyCount;
            clone.NextResetAt = this.NextResetAt;
            clone.SeasonNum = this.SeasonNum;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int StoreID)
        {
           return this.UID == UID
                && this.StoreID == StoreID;
        }

        public bool CompareKey(SilverMedalStore rdata)
        {
           return UID == rdata.UID
                && StoreID == rdata.StoreID;
        }
    }

    [MessagePackObject]
    public class GuideMission
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("GuideMissionCategory")]
        public EGuideMissionCategory GuideMissionCategory { get; set; } = 0;
        [Key("MissionID")]
        public Int64 MissionID { get; set; } = 0;
        [Key("IsCompleted")]
        public bool IsCompleted { get; set; } = false;
        [Key("IsRewarded")]
        public bool IsRewarded { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public GuideMission Clone()
        {
            var clone = new GuideMission();
            clone.UID = this.UID;
            clone.GuideMissionCategory = this.GuideMissionCategory;
            clone.MissionID = this.MissionID;
            clone.IsCompleted = this.IsCompleted;
            clone.IsRewarded = this.IsRewarded;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, EGuideMissionCategory GuideMissionCategory)
        {
           return this.UID == UID
                && this.GuideMissionCategory == GuideMissionCategory;
        }

        public bool CompareKey(GuideMission rdata)
        {
           return UID == rdata.UID
                && GuideMissionCategory == rdata.GuideMissionCategory;
        }
    }

    [MessagePackObject]
    public class GuideMissionProgressReward
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("LastReceiveID")]
        public Int64 LastReceiveID { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public GuideMissionProgressReward Clone()
        {
            var clone = new GuideMissionProgressReward();
            clone.UID = this.UID;
            clone.LastReceiveID = this.LastReceiveID;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(GuideMissionProgressReward rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class Hero
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("HeroID")]
        public int HeroID { get; set; } = 0;
        [Key("BattleLevel")]
        public int BattleLevel { get; set; } = 0;
        [Key("BattleExp")]
        public Int64 BattleExp { get; set; } = 0;
        [Key("RewardedLevel")]
        public int RewardedLevel { get; set; } = 0;
        [Key("LeftEyeHexColor")]
        public string LeftEyeHexColor { get; set; } = "";
        [Key("RightEyeHexColor")]
        public string RightEyeHexColor { get; set; } = "";
        [Key("HairSkinID")]
        public Int64 HairSkinID { get; set; } = 0;
        [Key("BodySkinID")]
        public Int64 BodySkinID { get; set; } = 0;
        [Key("HeadID")]
        public Int64 HeadID { get; set; } = 0;
        [Key("HeadOffset")]
        public string HeadOffset { get; set; } = "";
        [Key("HeadRotate")]
        public string HeadRotate { get; set; } = "";
        [Key("FaceID")]
        public Int64 FaceID { get; set; } = 0;
        [Key("FaceOffset")]
        public string FaceOffset { get; set; } = "";
        [Key("FaceRotate")]
        public string FaceRotate { get; set; } = "";
        [Key("BackID")]
        public Int64 BackID { get; set; } = 0;
        [Key("BackOffset")]
        public string BackOffset { get; set; } = "";
        [Key("BackRotate")]
        public string BackRotate { get; set; } = "";
        [Key("PelvisID")]
        public Int64 PelvisID { get; set; } = 0;
        [Key("PelvisOffset")]
        public string PelvisOffset { get; set; } = "";
        [Key("PelvisRotate")]
        public string PelvisRotate { get; set; } = "";
        [Key("WeaponID")]
        public Int64 WeaponID { get; set; } = 0;
        [Key("WinPoseID")]
        public Int64 WinPoseID { get; set; } = 0;
        [Key("ExpireAt")]
        public Int64 ExpireAt { get; set; } = 0;
        [Key("AddPresetCount")]
        public int AddPresetCount { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Hero Clone()
        {
            var clone = new Hero();
            clone.UID = this.UID;
            clone.HeroID = this.HeroID;
            clone.BattleLevel = this.BattleLevel;
            clone.BattleExp = this.BattleExp;
            clone.RewardedLevel = this.RewardedLevel;
            clone.LeftEyeHexColor = this.LeftEyeHexColor;
            clone.RightEyeHexColor = this.RightEyeHexColor;
            clone.HairSkinID = this.HairSkinID;
            clone.BodySkinID = this.BodySkinID;
            clone.HeadID = this.HeadID;
            clone.HeadOffset = this.HeadOffset;
            clone.HeadRotate = this.HeadRotate;
            clone.FaceID = this.FaceID;
            clone.FaceOffset = this.FaceOffset;
            clone.FaceRotate = this.FaceRotate;
            clone.BackID = this.BackID;
            clone.BackOffset = this.BackOffset;
            clone.BackRotate = this.BackRotate;
            clone.PelvisID = this.PelvisID;
            clone.PelvisOffset = this.PelvisOffset;
            clone.PelvisRotate = this.PelvisRotate;
            clone.WeaponID = this.WeaponID;
            clone.WinPoseID = this.WinPoseID;
            clone.ExpireAt = this.ExpireAt;
            clone.AddPresetCount = this.AddPresetCount;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int HeroID)
        {
           return this.UID == UID
                && this.HeroID == HeroID;
        }

        public bool CompareKey(Hero rdata)
        {
           return UID == rdata.UID
                && HeroID == rdata.HeroID;
        }
    }

    [MessagePackObject]
    public class HeroSkin
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SkinID")]
        public int SkinID { get; set; } = 0;
        [Key("HexColor1")]
        public string HexColor1 { get; set; } = "";
        [Key("HexColor2")]
        public string HexColor2 { get; set; } = "";
        [Key("HexColor3")]
        public string HexColor3 { get; set; } = "";
        [Key("HexColor4")]
        public string HexColor4 { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public HeroSkin Clone()
        {
            var clone = new HeroSkin();
            clone.UID = this.UID;
            clone.SkinID = this.SkinID;
            clone.HexColor1 = this.HexColor1;
            clone.HexColor2 = this.HexColor2;
            clone.HexColor3 = this.HexColor3;
            clone.HexColor4 = this.HexColor4;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int SkinID)
        {
           return this.UID == UID
                && this.SkinID == SkinID;
        }

        public bool CompareKey(HeroSkin rdata)
        {
           return UID == rdata.UID
                && SkinID == rdata.SkinID;
        }
    }

    [MessagePackObject]
    public class HeroSkinPreset
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("HeroID")]
        public int HeroID { get; set; } = 0;
        [Key("SlotID")]
        public int SlotID { get; set; } = 0;
        [Key("PresetData")]
        public string PresetData { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public HeroSkinPreset Clone()
        {
            var clone = new HeroSkinPreset();
            clone.UID = this.UID;
            clone.HeroID = this.HeroID;
            clone.SlotID = this.SlotID;
            clone.PresetData = this.PresetData;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int HeroID, int SlotID)
        {
           return this.UID == UID
                && this.HeroID == HeroID
                && this.SlotID == SlotID;
        }

        public bool CompareKey(HeroSkinPreset rdata)
        {
           return UID == rdata.UID
                && HeroID == rdata.HeroID
                && SlotID == rdata.SlotID;
        }
    }

    [MessagePackObject]
    public class Incubation
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("IncubatorID")]
        public Int64 IncubatorID { get; set; } = 0;
        [Key("IncubateCount")]
        public Int64 IncubateCount { get; set; } = 0;
        [Key("PetEggID")]
        public Int64 PetEggID { get; set; } = 0;
        [Key("IncubationEndAt")]
        public Int64 IncubationEndAt { get; set; } = 0;
        [Key("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Incubation Clone()
        {
            var clone = new Incubation();
            clone.UID = this.UID;
            clone.IncubatorID = this.IncubatorID;
            clone.IncubateCount = this.IncubateCount;
            clone.PetEggID = this.PetEggID;
            clone.IncubationEndAt = this.IncubationEndAt;
            clone.IsDeleted = this.IsDeleted;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 IncubatorID)
        {
           return this.UID == UID
                && this.IncubatorID == IncubatorID;
        }

        public bool CompareKey(Incubation rdata)
        {
           return UID == rdata.UID
                && IncubatorID == rdata.IncubatorID;
        }
    }

    [MessagePackObject]
    public class InstantGuide
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("InstantGuideList")]
        public List<Int64> InstantGuideList { get; set; } = new List<Int64>();
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public InstantGuide Clone()
        {
            var clone = new InstantGuide();
            clone.UID = this.UID;
            clone.InstantGuideList.AddRange(this.InstantGuideList);
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(InstantGuide rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class Inventory
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ItemID")]
        public Int64 ItemID { get; set; } = 0;
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("ItemType")]
        public EItemType ItemType { get; set; } = 0;
        [Key("ItemNameStringWithID")]
        public string ItemNameStringWithID { get; set; } = "";
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Inventory Clone()
        {
            var clone = new Inventory();
            clone.UID = this.UID;
            clone.ItemID = this.ItemID;
            clone.Count = this.Count;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.ItemType = this.ItemType;
            clone.ItemNameStringWithID = this.ItemNameStringWithID;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 ItemID)
        {
           return this.UID == UID
                && this.ItemID == ItemID;
        }

        public bool CompareKey(Inventory rdata)
        {
           return UID == rdata.UID
                && ItemID == rdata.ItemID;
        }
    }

    [MessagePackObject]
    public class Mail
    {
        [Key("MailID")]
        public Int64 MailID { get; set; } = 0;
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("MailType")]
        public EMailType MailType { get; set; } = 0;
        [Key("State")]
        public EMailStateType State { get; set; } = 0;
        [Key("IsBM")]
        public bool IsBM { get; set; } = false;
        [Key("Title")]
        public string Title { get; set; } = "";
        [Key("Message")]
        public string Message { get; set; } = "";
        [Key("RewardList")]
        public List<MailReward> RewardList { get; set; } = new List<MailReward>();
        [Key("ExpireAt")]
        public int ExpireAt { get; set; } = 0;
        [Key("ReceiveAt")]
        public int ReceiveAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Mail Clone()
        {
            var clone = new Mail();
            clone.MailID = this.MailID;
            clone.UID = this.UID;
            clone.MailType = this.MailType;
            clone.State = this.State;
            clone.IsBM = this.IsBM;
            clone.Title = this.Title;
            clone.Message = this.Message;
            clone.RewardList.AddRange(this.RewardList);
            clone.ExpireAt = this.ExpireAt;
            clone.ReceiveAt = this.ReceiveAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 MailID)
        {
           return this.MailID == MailID;
        }

        public bool CompareKey(Mail rdata)
        {
           return MailID == rdata.MailID;
        }
    }

    [MessagePackObject]
    public class MailWithUser
    {
        [Key("MailID")]
        public Int64 MailID { get; set; } = 0;
        [Key("MemberNo")]
        public UInt64 MemberNo { get; set; } = 0;
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("Nick")]
        public string Nick { get; set; } = "";
        [Key("MailType")]
        public EMailType MailType { get; set; } = 0;
        [Key("State")]
        public EMailStateType State { get; set; } = 0;
        [Key("IsBM")]
        public bool IsBM { get; set; } = false;
        [Key("Title")]
        public string Title { get; set; } = "";
        [Key("Message")]
        public string Message { get; set; } = "";
        [Key("RewardList")]
        public List<MailReward> RewardList { get; set; } = new List<MailReward>();
        [Key("ExpireAt")]
        public int ExpireAt { get; set; } = 0;
        [Key("ReceiveAt")]
        public int ReceiveAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isSelected")]
        public bool isSelected { get; set; } = false;

        public MailWithUser Clone()
        {
            var clone = new MailWithUser();
            clone.MailID = this.MailID;
            clone.MemberNo = this.MemberNo;
            clone.UID = this.UID;
            clone.Nick = this.Nick;
            clone.MailType = this.MailType;
            clone.State = this.State;
            clone.IsBM = this.IsBM;
            clone.Title = this.Title;
            clone.Message = this.Message;
            clone.RewardList.AddRange(this.RewardList);
            clone.ExpireAt = this.ExpireAt;
            clone.ReceiveAt = this.ReceiveAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isSelected = this.isSelected;
            return clone;
        }

        public bool CompareKey(Int64 MailID)
        {
           return this.MailID == MailID;
        }

        public bool CompareKey(MailWithUser rdata)
        {
           return MailID == rdata.MailID;
        }
    }

    [MessagePackObject]
    public class MailReward
    {
        [Key("RewardType")]
        public ERewardType RewardType { get; set; } = 0;
        [Key("RewardID")]
        public Int64 RewardID { get; set; } = 0;
        [Key("RewardCount")]
        public Int64 RewardCount { get; set; } = 0;

        public MailReward Clone()
        {
            var clone = new MailReward();
            clone.RewardType = this.RewardType;
            clone.RewardID = this.RewardID;
            clone.RewardCount = this.RewardCount;
            return clone;
        }
    }

    [MessagePackObject]
    public class MailInputItem
    {
        [Key("rewardType")]
        public ERewardType rewardType { get; set; } = 0;
        [Key("id")]
        public Int64 id { get; set; } = 0;
        [Key("name")]
        public string name { get; set; } = "";
        [Key("count")]
        public Int64 count { get; set; } = 0;

        public MailInputItem Clone()
        {
            var clone = new MailInputItem();
            clone.rewardType = this.rewardType;
            clone.id = this.id;
            clone.name = this.name;
            clone.count = this.count;
            return clone;
        }

        public bool CompareKey(ERewardType rewardType, Int64 id)
        {
           return this.rewardType == rewardType
                && this.id == id;
        }

        public bool CompareKey(MailInputItem rdata)
        {
           return rewardType == rdata.rewardType
                && id == rdata.id;
        }
    }

    [MessagePackObject]
    public class MailInput
    {
        [Key("mailtype")]
        public EMailType mailtype { get; set; } = 0;
        [Key("isBM")]
        public bool isBM { get; set; } = false;
        [Key("title")]
        public string title { get; set; } = "";
        [Key("message")]
        public string message { get; set; } = "";
        [Key("expireAt")]
        public int expireAt { get; set; } = 0;
        [Key("items")]
        public List<MailInputItem> items { get; set; } = new List<MailInputItem>();

        public MailInput Clone()
        {
            var clone = new MailInput();
            clone.mailtype = this.mailtype;
            clone.isBM = this.isBM;
            clone.title = this.title;
            clone.message = this.message;
            clone.expireAt = this.expireAt;
            clone.items.AddRange(this.items);
            return clone;
        }
    }

    [MessagePackObject]
    public class MazeRewardBox
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SlotID")]
        public int SlotID { get; set; } = 0;
        [Key("BoxID")]
        public Int64 BoxID { get; set; } = 0;
        [Key("ExpiredAt")]
        public Int64 ExpiredAt { get; set; } = 0;
        [Key("IsOpened")]
        public bool IsOpened { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public MazeRewardBox Clone()
        {
            var clone = new MazeRewardBox();
            clone.UID = this.UID;
            clone.SlotID = this.SlotID;
            clone.BoxID = this.BoxID;
            clone.ExpiredAt = this.ExpiredAt;
            clone.IsOpened = this.IsOpened;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int SlotID)
        {
           return this.UID == UID
                && this.SlotID == SlotID;
        }

        public bool CompareKey(MazeRewardBox rdata)
        {
           return UID == rdata.UID
                && SlotID == rdata.SlotID;
        }
    }

    [MessagePackObject]
    public class NicePlayer
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("NiceLevel")]
        public int NiceLevel { get; set; } = 0;
        [Key("NicePoint")]
        public int NicePoint { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public NicePlayer Clone()
        {
            var clone = new NicePlayer();
            clone.UID = this.UID;
            clone.NiceLevel = this.NiceLevel;
            clone.NicePoint = this.NicePoint;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(NicePlayer rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class Penalty
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ReportState")]
        public EPenaltyReportState ReportState { get; set; } = 0;
        [Key("IsActive")]
        public bool IsActive { get; set; } = false;
        [Key("PenaltyGrade")]
        public int PenaltyGrade { get; set; } = 0;
        [Key("PenaltyPoint")]
        public int PenaltyPoint { get; set; } = 0;
        [Key("PenaltyCount")]
        public int PenaltyCount { get; set; } = 0;
        [Key("PenaltyEndAt")]
        public Int64 PenaltyEndAt { get; set; } = 0;
        [Key("ClearPenaltyAt")]
        public Int64 ClearPenaltyAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Penalty Clone()
        {
            var clone = new Penalty();
            clone.UID = this.UID;
            clone.ReportState = this.ReportState;
            clone.IsActive = this.IsActive;
            clone.PenaltyGrade = this.PenaltyGrade;
            clone.PenaltyPoint = this.PenaltyPoint;
            clone.PenaltyCount = this.PenaltyCount;
            clone.PenaltyEndAt = this.PenaltyEndAt;
            clone.ClearPenaltyAt = this.ClearPenaltyAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, EPenaltyReportState ReportState)
        {
           return this.UID == UID
                && this.ReportState == ReportState;
        }

        public bool CompareKey(Penalty rdata)
        {
           return UID == rdata.UID
                && ReportState == rdata.ReportState;
        }
    }

    [MessagePackObject]
    public class PenaltyWithUser
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ReportState")]
        public EPenaltyReportState ReportState { get; set; } = 0;
        [Key("MemberNo")]
        public UInt64 MemberNo { get; set; } = 0;
        [Key("Nick")]
        public string Nick { get; set; } = "";
        [Key("IsActive")]
        public bool IsActive { get; set; } = false;
        [Key("PenaltyGrade")]
        public int PenaltyGrade { get; set; } = 0;
        [Key("PenaltyPoint")]
        public int PenaltyPoint { get; set; } = 0;
        [Key("PenaltyCount")]
        public int PenaltyCount { get; set; } = 0;
        [Key("PenaltyEndAt")]
        public Int64 PenaltyEndAt { get; set; } = 0;
        [Key("ClearPenaltyAt")]
        public Int64 ClearPenaltyAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public PenaltyWithUser Clone()
        {
            var clone = new PenaltyWithUser();
            clone.UID = this.UID;
            clone.ReportState = this.ReportState;
            clone.MemberNo = this.MemberNo;
            clone.Nick = this.Nick;
            clone.IsActive = this.IsActive;
            clone.PenaltyGrade = this.PenaltyGrade;
            clone.PenaltyPoint = this.PenaltyPoint;
            clone.PenaltyCount = this.PenaltyCount;
            clone.PenaltyEndAt = this.PenaltyEndAt;
            clone.ClearPenaltyAt = this.ClearPenaltyAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, EPenaltyReportState ReportState)
        {
           return this.UID == UID
                && this.ReportState == ReportState;
        }

        public bool CompareKey(PenaltyWithUser rdata)
        {
           return UID == rdata.UID
                && ReportState == rdata.ReportState;
        }
    }

    [MessagePackObject]
    public class Pet
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("UniqueID")]
        public Int64 UniqueID { get; set; } = 0;
        [Key("PetID")]
        public Int64 PetID { get; set; } = 0;
        [Key("Ability")]
        public string Ability { get; set; } = "";
        [Key("Like")]
        public Int64 Like { get; set; } = 0;
        [Key("IsLocked")]
        public bool IsLocked { get; set; } = false;
        [Key("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Pet Clone()
        {
            var clone = new Pet();
            clone.UID = this.UID;
            clone.UniqueID = this.UniqueID;
            clone.PetID = this.PetID;
            clone.Ability = this.Ability;
            clone.Like = this.Like;
            clone.IsLocked = this.IsLocked;
            clone.IsDeleted = this.IsDeleted;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 UniqueID)
        {
           return this.UID == UID
                && this.UniqueID == UniqueID;
        }

        public bool CompareKey(Pet rdata)
        {
           return UID == rdata.UID
                && UniqueID == rdata.UniqueID;
        }
    }

    [MessagePackObject]
    public class PlayRecordGoldClash
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SeasonID")]
        public Int64 SeasonID { get; set; } = 0;
        [Key("HeroID")]
        public Int64 HeroID { get; set; } = 0;
        [Key("Win")]
        public Int64 Win { get; set; } = 0;
        [Key("Lose")]
        public Int64 Lose { get; set; } = 0;
        [Key("Kill")]
        public Int64 Kill { get; set; } = 0;
        [Key("Death")]
        public Int64 Death { get; set; } = 0;
        [Key("Mvp")]
        public Int64 Mvp { get; set; } = 0;
        [Key("AvgGold")]
        public Int64 AvgGold { get; set; } = 0;
        [Key("AvgDamage")]
        public Int64 AvgDamage { get; set; } = 0;
        [Key("AvgHeal")]
        public Int64 AvgHeal { get; set; } = 0;
        [Key("AvgDamageBlock")]
        public Int64 AvgDamageBlock { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public PlayRecordGoldClash Clone()
        {
            var clone = new PlayRecordGoldClash();
            clone.UID = this.UID;
            clone.SeasonID = this.SeasonID;
            clone.HeroID = this.HeroID;
            clone.Win = this.Win;
            clone.Lose = this.Lose;
            clone.Kill = this.Kill;
            clone.Death = this.Death;
            clone.Mvp = this.Mvp;
            clone.AvgGold = this.AvgGold;
            clone.AvgDamage = this.AvgDamage;
            clone.AvgHeal = this.AvgHeal;
            clone.AvgDamageBlock = this.AvgDamageBlock;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 SeasonID, Int64 HeroID)
        {
           return this.UID == UID
                && this.SeasonID == SeasonID
                && this.HeroID == HeroID;
        }

        public bool CompareKey(PlayRecordGoldClash rdata)
        {
           return UID == rdata.UID
                && SeasonID == rdata.SeasonID
                && HeroID == rdata.HeroID;
        }
    }

    [MessagePackObject]
    public class PlayRecordRpg
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ChapterID")]
        public Int64 ChapterID { get; set; } = 0;
        [Key("HeroID")]
        public Int64 HeroID { get; set; } = 0;
        [Key("BossClear")]
        public Int64 BossClear { get; set; } = 0;
        [Key("TopPoint")]
        public Int64 TopPoint { get; set; } = 0;
        [Key("ShortestPlayTime")]
        public Int64 ShortestPlayTime { get; set; } = 0;
        [Key("AvgFragment")]
        public Int64 AvgFragment { get; set; } = 0;
        [Key("AvgArtifact")]
        public Int64 AvgArtifact { get; set; } = 0;
        [Key("AvgLevel")]
        public Int64 AvgLevel { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public PlayRecordRpg Clone()
        {
            var clone = new PlayRecordRpg();
            clone.UID = this.UID;
            clone.ChapterID = this.ChapterID;
            clone.HeroID = this.HeroID;
            clone.BossClear = this.BossClear;
            clone.TopPoint = this.TopPoint;
            clone.ShortestPlayTime = this.ShortestPlayTime;
            clone.AvgFragment = this.AvgFragment;
            clone.AvgArtifact = this.AvgArtifact;
            clone.AvgLevel = this.AvgLevel;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 ChapterID, Int64 HeroID)
        {
           return this.UID == UID
                && this.ChapterID == ChapterID
                && this.HeroID == HeroID;
        }

        public bool CompareKey(PlayRecordRpg rdata)
        {
           return UID == rdata.UID
                && ChapterID == rdata.ChapterID
                && HeroID == rdata.HeroID;
        }
    }

    [MessagePackObject]
    public class Profile
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ProfileID")]
        public int ProfileID { get; set; } = 0;
        [Key("ProfileType")]
        public EProfileType ProfileType { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;

        public Profile Clone()
        {
            var clone = new Profile();
            clone.UID = this.UID;
            clone.ProfileID = this.ProfileID;
            clone.ProfileType = this.ProfileType;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            return clone;
        }

        public bool CompareKey(Int64 UID, int ProfileID)
        {
           return this.UID == UID
                && this.ProfileID == ProfileID;
        }

        public bool CompareKey(Profile rdata)
        {
           return UID == rdata.UID
                && ProfileID == rdata.ProfileID;
        }
    }

    [MessagePackObject]
    public class RankingReward
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ContentType")]
        public EContentsType ContentType { get; set; } = 0;
        [Key("RewardedAt")]
        public DateTime RewardedAt { get; set; } = DateTime.UtcNow;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public RankingReward Clone()
        {
            var clone = new RankingReward();
            clone.UID = this.UID;
            clone.ContentType = this.ContentType;
            clone.RewardedAt = this.RewardedAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(RankingReward rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class RpgAttribute
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Key("Level")]
        public int Level { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public RpgAttribute Clone()
        {
            var clone = new RpgAttribute();
            clone.UID = this.UID;
            clone.ID = this.ID;
            clone.Level = this.Level;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 ID)
        {
           return this.UID == UID
                && this.ID == ID;
        }

        public bool CompareKey(RpgAttribute rdata)
        {
           return UID == rdata.UID
                && ID == rdata.ID;
        }
    }

    [MessagePackObject]
    public class SeasonMission
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SeasonPassID")]
        public int SeasonPassID { get; set; } = 0;
        [Key("MissionID")]
        public Int64 MissionID { get; set; } = 0;
        [Key("MissionGroup")]
        public EMissionGroup MissionGroup { get; set; } = 0;
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Key("IsComplete")]
        public bool IsComplete { get; set; } = false;
        [Key("ResetAt")]
        public Int64 ResetAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public SeasonMission Clone()
        {
            var clone = new SeasonMission();
            clone.UID = this.UID;
            clone.SeasonPassID = this.SeasonPassID;
            clone.MissionID = this.MissionID;
            clone.MissionGroup = this.MissionGroup;
            clone.Count = this.Count;
            clone.IsComplete = this.IsComplete;
            clone.ResetAt = this.ResetAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, int SeasonPassID, Int64 MissionID)
        {
           return this.UID == UID
                && this.SeasonPassID == SeasonPassID
                && this.MissionID == MissionID;
        }

        public bool CompareKey(SeasonMission rdata)
        {
           return UID == rdata.UID
                && SeasonPassID == rdata.SeasonPassID
                && MissionID == rdata.MissionID;
        }
    }

    [MessagePackObject]
    public class SeasonPass
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SeasonPassID")]
        public int SeasonPassID { get; set; } = 0;
        [Key("SeasonNum")]
        public int SeasonNum { get; set; } = 0;
        [Key("IsPaid")]
        public bool IsPaid { get; set; } = false;
        [Key("Level")]
        public int Level { get; set; } = 0;
        [Key("Exp")]
        public Int64 Exp { get; set; } = 0;
        [Key("RewardState")]
        public string RewardState { get; set; } = "";
        [Key("ReqDirection")]
        public bool ReqDirection { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public SeasonPass Clone()
        {
            var clone = new SeasonPass();
            clone.UID = this.UID;
            clone.SeasonPassID = this.SeasonPassID;
            clone.SeasonNum = this.SeasonNum;
            clone.IsPaid = this.IsPaid;
            clone.Level = this.Level;
            clone.Exp = this.Exp;
            clone.RewardState = this.RewardState;
            clone.ReqDirection = this.ReqDirection;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, int SeasonPassID)
        {
           return this.UID == UID
                && this.SeasonPassID == SeasonPassID;
        }

        public bool CompareKey(SeasonPass rdata)
        {
           return UID == rdata.UID
                && SeasonPassID == rdata.SeasonPassID;
        }
    }

    [MessagePackObject]
    public class SpecialLevel
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("LevelID")]
        public int LevelID { get; set; } = 0;
        [Key("CompletedAt")]
        public Int64 CompletedAt { get; set; } = 0;
        [Key("BoxID0")]
        public Int64 BoxID0 { get; set; } = 0;
        [Key("BoxID1")]
        public Int64 BoxID1 { get; set; } = 0;
        [Key("BoxID2")]
        public Int64 BoxID2 { get; set; } = 0;
        [Key("BoxID3")]
        public Int64 BoxID3 { get; set; } = 0;
        [Key("BoxID4")]
        public Int64 BoxID4 { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public SpecialLevel Clone()
        {
            var clone = new SpecialLevel();
            clone.UID = this.UID;
            clone.LevelID = this.LevelID;
            clone.CompletedAt = this.CompletedAt;
            clone.BoxID0 = this.BoxID0;
            clone.BoxID1 = this.BoxID1;
            clone.BoxID2 = this.BoxID2;
            clone.BoxID3 = this.BoxID3;
            clone.BoxID4 = this.BoxID4;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, int LevelID)
        {
           return this.UID == UID
                && this.LevelID == LevelID;
        }

        public bool CompareKey(SpecialLevel rdata)
        {
           return UID == rdata.UID
                && LevelID == rdata.LevelID;
        }
    }

    [MessagePackObject]
    public class TreasureBox
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("BoxList")]
        public List<UserTreasureBox> BoxList { get; set; } = new List<UserTreasureBox>();
        [Key("ExpireAt")]
        public Int64 ExpireAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public TreasureBox Clone()
        {
            var clone = new TreasureBox();
            clone.UID = this.UID;
            clone.BoxList.AddRange(this.BoxList);
            clone.ExpireAt = this.ExpireAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(TreasureBox rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class UserTreasureBox
    {
        [Key("BoxID")]
        public Int64 BoxID { get; set; } = 0;
        [Key("IsOpened")]
        public bool IsOpened { get; set; } = false;

        public UserTreasureBox Clone()
        {
            var clone = new UserTreasureBox();
            clone.BoxID = this.BoxID;
            clone.IsOpened = this.IsOpened;
            return clone;
        }

        public bool CompareKey(Int64 BoxID)
        {
           return this.BoxID == BoxID;
        }

        public bool CompareKey(UserTreasureBox rdata)
        {
           return BoxID == rdata.BoxID;
        }
    }

    [MessagePackObject]
    public class UserAccount
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("WUID")]
        public string WUID { get; set; } = "";
        [Key("Nick")]
        public string Nick { get; set; } = "";
        [Key("HeroID")]
        public int HeroID { get; set; } = 0;
        [Key("ProfileIconID")]
        public int ProfileIconID { get; set; } = 0;
        [Key("ProfileBGID")]
        public int ProfileBGID { get; set; } = 0;
        [Key("EntitlementID")]
        public int EntitlementID { get; set; } = 0;
        [Key("PetUniqueID")]
        public int PetUniqueID { get; set; } = 0;
        [Key("PetID")]
        public Int64 PetID { get; set; } = 0;
        [Key("VehicleID")]
        public int VehicleID { get; set; } = 0;
        [Key("LastClearChapterID")]
        public int LastClearChapterID { get; set; } = 0;
        [Key("IsGlitchTutorialComplete")]
        public bool IsGlitchTutorialComplete { get; set; } = false;
        [Key("IsGoldClashTutorialComplete")]
        public bool IsGoldClashTutorialComplete { get; set; } = false;
        [Key("LastPosition")]
        public string LastPosition { get; set; } = "";
        [Key("AddArtifactDeckCount")]
        public int AddArtifactDeckCount { get; set; } = 0;
        [Key("PenaltyReportCount")]
        public int PenaltyReportCount { get; set; } = 0;
        [Key("LastPenaltyReportAt")]
        public Int64 LastPenaltyReportAt { get; set; } = 0;
        [Key("IntroduceID")]
        public Int64 IntroduceID { get; set; } = 0;
        [Key("LastLoginAt")]
        public Int64 LastLoginAt { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("IsSignIn")]
        public bool IsSignIn { get; set; } = false;
        [Key("currentLobbyID")]
        public string currentLobbyID { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public UserAccount Clone()
        {
            var clone = new UserAccount();
            clone.UID = this.UID;
            clone.WUID = this.WUID;
            clone.Nick = this.Nick;
            clone.HeroID = this.HeroID;
            clone.ProfileIconID = this.ProfileIconID;
            clone.ProfileBGID = this.ProfileBGID;
            clone.EntitlementID = this.EntitlementID;
            clone.PetUniqueID = this.PetUniqueID;
            clone.PetID = this.PetID;
            clone.VehicleID = this.VehicleID;
            clone.LastClearChapterID = this.LastClearChapterID;
            clone.IsGlitchTutorialComplete = this.IsGlitchTutorialComplete;
            clone.IsGoldClashTutorialComplete = this.IsGoldClashTutorialComplete;
            clone.LastPosition = this.LastPosition;
            clone.AddArtifactDeckCount = this.AddArtifactDeckCount;
            clone.PenaltyReportCount = this.PenaltyReportCount;
            clone.LastPenaltyReportAt = this.LastPenaltyReportAt;
            clone.IntroduceID = this.IntroduceID;
            clone.LastLoginAt = this.LastLoginAt;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.IsSignIn = this.IsSignIn;
            clone.currentLobbyID = this.currentLobbyID;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(UserAccount rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class UserBlock
    {
        [Key("MemberNo")]
        public UInt64 MemberNo { get; set; } = 0;
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("BlockReasonID")]
        public int BlockReasonID { get; set; } = 0;
        [Key("BlockReasonStr")]
        public string BlockReasonStr { get; set; } = "";
        [Key("StartTime")]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        [Key("EndTime")]
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public UserBlock Clone()
        {
            var clone = new UserBlock();
            clone.MemberNo = this.MemberNo;
            clone.UID = this.UID;
            clone.BlockReasonID = this.BlockReasonID;
            clone.BlockReasonStr = this.BlockReasonStr;
            clone.StartTime = this.StartTime;
            clone.EndTime = this.EndTime;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(UInt64 MemberNo)
        {
           return this.MemberNo == MemberNo;
        }

        public bool CompareKey(UserBlock rdata)
        {
           return MemberNo == rdata.MemberNo;
        }
    }

    [MessagePackObject]
    public class UserDevice
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("DeviceID")]
        public string DeviceID { get; set; } = "";
        [Key("DeviceType")]
        public string DeviceType { get; set; } = "";
        [Key("DeviceModel")]
        public string DeviceModel { get; set; } = "";
        [Key("OS")]
        public string OS { get; set; } = "";
        [Key("MarketCode")]
        public EMarketCode MarketCode { get; set; } = 0;
        [Key("Provider")]
        public string Provider { get; set; } = "";
        [Key("Platform")]
        public EPlatform Platform { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public UserDevice Clone()
        {
            var clone = new UserDevice();
            clone.UID = this.UID;
            clone.DeviceID = this.DeviceID;
            clone.DeviceType = this.DeviceType;
            clone.DeviceModel = this.DeviceModel;
            clone.OS = this.OS;
            clone.MarketCode = this.MarketCode;
            clone.Provider = this.Provider;
            clone.Platform = this.Platform;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID)
        {
           return this.UID == UID;
        }

        public bool CompareKey(UserDevice rdata)
        {
           return UID == rdata.UID;
        }
    }

    [MessagePackObject]
    public class WeaponCategory
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("WeaponCategoryID")]
        public Int64 WeaponCategoryID { get; set; } = 0;
        [Key("Exp")]
        public Int64 Exp { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public WeaponCategory Clone()
        {
            var clone = new WeaponCategory();
            clone.UID = this.UID;
            clone.WeaponCategoryID = this.WeaponCategoryID;
            clone.Exp = this.Exp;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, Int64 WeaponCategoryID)
        {
           return this.UID == UID
                && this.WeaponCategoryID == WeaponCategoryID;
        }

        public bool CompareKey(WeaponCategory rdata)
        {
           return UID == rdata.UID
                && WeaponCategoryID == rdata.WeaponCategoryID;
        }
    }

    [MessagePackObject]
    public class WonderCube
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("SlotID")]
        public int SlotID { get; set; } = 0;
        [Key("WonderCubeID")]
        public Int64 WonderCubeID { get; set; } = 0;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public WonderCube Clone()
        {
            var clone = new WonderCube();
            clone.UID = this.UID;
            clone.SlotID = this.SlotID;
            clone.WonderCubeID = this.WonderCubeID;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, int SlotID)
        {
           return this.UID == UID
                && this.SlotID == SlotID;
        }

        public bool CompareKey(WonderCube rdata)
        {
           return UID == rdata.UID
                && SlotID == rdata.SlotID;
        }
    }

    [MessagePackObject]
    public class WonderStore
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("StoreID")]
        public int StoreID { get; set; } = 0;
        [Key("TID")]
        public string TID { get; set; } = "";
        [Key("StoveProductID")]
        public string StoveProductID { get; set; } = "";
        [Key("BuyCount")]
        public Int64 BuyCount { get; set; } = 0;
        [Key("SeasonPassID")]
        public Int64 SeasonPassID { get; set; } = 0;
        [Key("IsSubscription")]
        public bool IsSubscription { get; set; } = false;
        [Key("SubscriptionExpireAt")]
        public Int64 SubscriptionExpireAt { get; set; } = 0;
        [Key("BuyAbleStartAt")]
        public Int64 BuyAbleStartAt { get; set; } = 0;
        [Key("BuyAbleEndAt")]
        public Int64 BuyAbleEndAt { get; set; } = 0;
        [Key("NextResetAt")]
        public Int64 NextResetAt { get; set; } = 0;
        [Key("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public WonderStore Clone()
        {
            var clone = new WonderStore();
            clone.UID = this.UID;
            clone.StoreID = this.StoreID;
            clone.TID = this.TID;
            clone.StoveProductID = this.StoveProductID;
            clone.BuyCount = this.BuyCount;
            clone.SeasonPassID = this.SeasonPassID;
            clone.IsSubscription = this.IsSubscription;
            clone.SubscriptionExpireAt = this.SubscriptionExpireAt;
            clone.BuyAbleStartAt = this.BuyAbleStartAt;
            clone.BuyAbleEndAt = this.BuyAbleEndAt;
            clone.NextResetAt = this.NextResetAt;
            clone.IsDeleted = this.IsDeleted;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 UID, int StoreID)
        {
           return this.UID == UID
                && this.StoreID == StoreID;
        }

        public bool CompareKey(WonderStore rdata)
        {
           return UID == rdata.UID
                && StoreID == rdata.StoreID;
        }
    }

    [MessagePackObject]
    public class Billing
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("TID")]
        public string TID { get; set; } = "";
        [Key("OriginalTID")]
        public string OriginalTID { get; set; } = "";
        [Key("MemberNo")]
        public string MemberNo { get; set; } = "";
        [Key("NickNameNo")]
        public string NickNameNo { get; set; } = "";
        [Key("NotiType")]
        public string NotiType { get; set; } = "";
        [Key("TxnTime")]
        public UInt64 TxnTime { get; set; } = 0;
        [Key("MarketCode")]
        public string MarketCode { get; set; } = "";
        [Key("ProductID")]
        public string ProductID { get; set; } = "";
        [Key("SubStatusCode")]
        public string SubStatusCode { get; set; } = "";
        [Key("ExpireTime")]
        public UInt64 ExpireTime { get; set; } = 0;
        [Key("Price")]
        public Int64 Price { get; set; } = 0;
        [Key("IsRewarded")]
        public bool IsRewarded { get; set; } = false;
        [Key("BillingData")]
        public string BillingData { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Billing Clone()
        {
            var clone = new Billing();
            clone.UID = this.UID;
            clone.TID = this.TID;
            clone.OriginalTID = this.OriginalTID;
            clone.MemberNo = this.MemberNo;
            clone.NickNameNo = this.NickNameNo;
            clone.NotiType = this.NotiType;
            clone.TxnTime = this.TxnTime;
            clone.MarketCode = this.MarketCode;
            clone.ProductID = this.ProductID;
            clone.SubStatusCode = this.SubStatusCode;
            clone.ExpireTime = this.ExpireTime;
            clone.Price = this.Price;
            clone.IsRewarded = this.IsRewarded;
            clone.BillingData = this.BillingData;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            return clone;
        }

        public bool CompareKey(Int64 UID, string TID)
        {
           return this.UID == UID
                && this.TID == TID;
        }

        public bool CompareKey(Billing rdata)
        {
           return UID == rdata.UID
                && TID == rdata.TID;
        }
    }

}
