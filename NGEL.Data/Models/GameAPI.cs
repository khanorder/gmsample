using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NGEL.Data.Tables.Models;

namespace NGEL.Data.Models
{

    [MessagePackObject]
    public class GameKickUserParameters
    {
        [Key("UID")]
        public string UID { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameKickUserResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
    }

    [MessagePackObject]
    public class GameAccountsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameAccountsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("accounts")]
        public List<Account> accounts { get; set; } = new List<Account>();
    }

    [MessagePackObject]
    public class GameAchievementsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameAchievementsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("achievements")]
        public List<Achievement> achievements { get; set; } = new List<Achievement>();
    }

    [MessagePackObject]
    public class GameSaveAchievementsParameters
    {
        [Key("achievements")]
        public List<Achievement> achievements { get; set; } = new List<Achievement>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveAchievementsResponses : IAPIResponse
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
    public class GameDeleteAchievementsParameters
    {
        [Key("achievements")]
        public List<Achievement> achievements { get; set; } = new List<Achievement>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteAchievementsResponses : IAPIResponse
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
    public class GameArtifactsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameArtifactsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("artifacts")]
        public List<Artifact> artifacts { get; set; } = new List<Artifact>();
    }

    [MessagePackObject]
    public class GameSaveArtifactParameters
    {
        [Key("artifacts")]
        public List<Artifact> artifacts { get; set; } = new List<Artifact>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveArtifactResponses : IAPIResponse
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
    public class GameDeleteArtifactParameters
    {
        [Key("artifacts")]
        public List<Artifact> artifacts { get; set; } = new List<Artifact>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteArtifactResponses : IAPIResponse
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
    public class GameArtifactDecksParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameArtifactDecksResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("artifactDecks")]
        public List<ArtifactDeck> artifactDecks { get; set; } = new List<ArtifactDeck>();
    }

    [MessagePackObject]
    public class GameAssetsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameAssetsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("assets")]
        public List<Asset> assets { get; set; } = new List<Asset>();
    }

    [MessagePackObject]
    public class GameSaveAssetsParameters
    {
        [Key("assets")]
        public List<Asset> assets { get; set; } = new List<Asset>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveAssetsResponses : IAPIResponse
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
    public class GameDeleteAssetsParameters
    {
        [Key("assets")]
        public List<Asset> assets { get; set; } = new List<Asset>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteAssetsResponses : IAPIResponse
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
    public class GameAttendancesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameAttendancesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("attendances")]
        public List<Attendance> attendances { get; set; } = new List<Attendance>();
    }

    [MessagePackObject]
    public class GameSaveAttendanceParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("AttendanceID")]
        public int AttendanceID { get; set; } = 0;
        [Key("RewardState")]
        public string RewardState { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveAttendanceResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GameEventStoresParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameEventStoresResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("eventStores")]
        public List<EventStore> eventStores { get; set; } = new List<EventStore>();
    }

    [MessagePackObject]
    public class GameExpressionsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameExpressionsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("expressions")]
        public List<Expression> expressions { get; set; } = new List<Expression>();
    }

    [MessagePackObject]
    public class GameExpressionPresetsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameExpressionPresetsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("expressionPresets")]
        public List<ExpressionPreset> expressionPresets { get; set; } = new List<ExpressionPreset>();
    }

    [MessagePackObject]
    public class GameCollectionsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameCollectionsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("collections")]
        public List<Collection> collections { get; set; } = new List<Collection>();
    }

    [MessagePackObject]
    public class GameSaveCollectionsParameters
    {
        [Key("collections")]
        public List<Collection> collections { get; set; } = new List<Collection>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveCollectionsResponses : IAPIResponse
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
    public class GameDeleteCollectionsParameters
    {
        [Key("collections")]
        public List<Collection> collections { get; set; } = new List<Collection>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteCollectionsResponses : IAPIResponse
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
    public class GameCraftsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameCraftsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("crafts")]
        public List<Craft> crafts { get; set; } = new List<Craft>();
    }

    [MessagePackObject]
    public class GameWonderCubesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameWonderCubesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("wonderCubes")]
        public List<WonderCube> wonderCubes { get; set; } = new List<WonderCube>();
    }

    [MessagePackObject]
    public class GameSaveWonderCubesParameters
    {
        [Key("wonderCubes")]
        public List<WonderCube> wonderCubes { get; set; } = new List<WonderCube>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveWonderCubesResponses : IAPIResponse
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
    public class GameWonderStoresParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameWonderStoresResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("wonderStores")]
        public List<WonderStore> wonderStores { get; set; } = new List<WonderStore>();
    }

    [MessagePackObject]
    public class GameSaveWonderStoresParameters
    {
        [Key("wonderStores")]
        public List<WonderStore> wonderStores { get; set; } = new List<WonderStore>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveWonderStoresResponses : IAPIResponse
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
    public class GameEntitlementsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameEntitlementsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("entitlements")]
        public List<Entitlement> entitlements { get; set; } = new List<Entitlement>();
    }

    [MessagePackObject]
    public class GameSaveEntitlementsParameters
    {
        [Key("entitlements")]
        public List<Entitlement> entitlements { get; set; } = new List<Entitlement>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveEntitlementsResponses : IAPIResponse
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
    public class GameDeleteEntitlementsParameters
    {
        [Key("entitlements")]
        public List<Entitlement> entitlements { get; set; } = new List<Entitlement>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteEntitlementsResponses : IAPIResponse
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
    public class GameFriendsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameFriendsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("friends")]
        public List<Friend> friends { get; set; } = new List<Friend>();
    }

    [MessagePackObject]
    public class GameGlitchStoresParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameGlitchStoresResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("glitchStores")]
        public List<GlitchStore> glitchStores { get; set; } = new List<GlitchStore>();
    }

    [MessagePackObject]
    public class GameGuideMissionsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameGuideMissionsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("guideMissions")]
        public List<GuideMission> guideMissions { get; set; } = new List<GuideMission>();
    }

    [MessagePackObject]
    public class GameSaveGuideMissionsParameters
    {
        [Key("guideMissions")]
        public List<GuideMission> guideMissions { get; set; } = new List<GuideMission>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveGuideMissionsResponses : IAPIResponse
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
    public class GameGuideMissionProgressRewardParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameGuideMissionProgressRewardResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("guideMissionProgressReward")]
        public GuideMissionProgressReward? guideMissionProgressReward { get; set; } = null;
    }

    [MessagePackObject]
    public class GameHeroesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameHeroesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("heroes")]
        public List<Hero> heroes { get; set; } = new List<Hero>();
    }

    [MessagePackObject]
    public class GameSkinsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSkinsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("skins")]
        public List<HeroSkin> skins { get; set; } = new List<HeroSkin>();
    }

    [MessagePackObject]
    public class GameSkinPresetsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSkinPresetsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("skinPresets")]
        public List<HeroSkinPreset> skinPresets { get; set; } = new List<HeroSkinPreset>();
    }

    [MessagePackObject]
    public class GameInventoriesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameInventoriesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("inventories")]
        public List<Inventory> inventories { get; set; } = new List<Inventory>();
    }

    [MessagePackObject]
    public class GameSaveInventoriesParameters
    {
        [Key("inventories")]
        public List<Inventory> inventories { get; set; } = new List<Inventory>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveInventoriesResponses : IAPIResponse
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
    public class GameDeleteInventoriesParameters
    {
        [Key("inventories")]
        public List<Inventory> inventories { get; set; } = new List<Inventory>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteInventoriesResponses : IAPIResponse
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
    public class GameMailsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameMailsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("mails")]
        public List<Mail> mails { get; set; } = new List<Mail>();
    }

    [MessagePackObject]
    public class GameDeleteMailsParameters
    {
        [Key("mails")]
        public List<Mail>? mails { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteMailsResponses : IAPIResponse
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
    public class GameMazeRewardBoxesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameMazeRewardBoxesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("mazeRewardBoxes")]
        public List<MazeRewardBox> mazeRewardBoxes { get; set; } = new List<MazeRewardBox>();
    }

    [MessagePackObject]
    public class GameUserBillingsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserBillingsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("billings")]
        public List<Billing> billings { get; set; } = new List<Billing>();
    }

    [MessagePackObject]
    public class GameIncubationsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameIncubationsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("incubations")]
        public List<Incubation> incubations { get; set; } = new List<Incubation>();
    }

    [MessagePackObject]
    public class GameInstantGuidesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameInstantGuidesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("instantGuides")]
        public List<InstantGuide> instantGuides { get; set; } = new List<InstantGuide>();
    }

    [MessagePackObject]
    public class GameBillingsParameters
    {
        [Key("startTime")]
        public string startTime { get; set; } = "";
        [Key("endTime")]
        public string endTime { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameBillingsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("billings")]
        public List<Billing> billings { get; set; } = new List<Billing>();
    }

    [MessagePackObject]
    public class GameNicePlayersAllParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameNicePlayersAllResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("nicePlayers")]
        public List<NicePlayer> nicePlayers { get; set; } = new List<NicePlayer>();
    }

    [MessagePackObject]
    public class GameNicePlayersParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameNicePlayersResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("nicePlayers")]
        public List<NicePlayer> nicePlayers { get; set; } = new List<NicePlayer>();
    }

    [MessagePackObject]
    public class GamePenaltiesAllParameters
    {
        [Key("startTime")]
        public string? startTime { get; set; } = null;
        [Key("endTime")]
        public string? endTime { get; set; } = null;
        [Key("uid")]
        public Int64? uid { get; set; } = null;
        [Key("memberNo")]
        public UInt64? memberNo { get; set; } = null;
        [Key("nick")]
        public string? nick { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GamePenaltiesAllResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("penalties")]
        public List<PenaltyWithUser> penalties { get; set; } = new List<PenaltyWithUser>();
    }

    [MessagePackObject]
    public class GamePenaltyParameters
    {
        [Key("penalty")]
        public Penalty penalty { get; set; } = new Penalty();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GamePenaltyResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("penalty")]
        public PenaltyWithUser penalty { get; set; } = new PenaltyWithUser();
    }

    [MessagePackObject]
    public class GamePenaltiesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GamePenaltiesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("penalties")]
        public List<Penalty> penalties { get; set; } = new List<Penalty>();
    }

    [MessagePackObject]
    public class GameSavePenaltyParameters
    {
        [Key("penalty")]
        public PenaltyWithUser penalty { get; set; } = new PenaltyWithUser();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSavePenaltyResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GamePetsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GamePetsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("pets")]
        public List<Pet> pets { get; set; } = new List<Pet>();
    }

    [MessagePackObject]
    public class GamePlayRecordGoldClashesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GamePlayRecordGoldClashesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("playRecordGoldClashes")]
        public List<PlayRecordGoldClash> playRecordGoldClashes { get; set; } = new List<PlayRecordGoldClash>();
    }

    [MessagePackObject]
    public class GamePlayRecordRpgsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GamePlayRecordRpgsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("playRecordRpgs")]
        public List<PlayRecordRpg> playRecordRpgs { get; set; } = new List<PlayRecordRpg>();
    }

    [MessagePackObject]
    public class GameProfilesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameProfilesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("profiles")]
        public List<Profile> profiles { get; set; } = new List<Profile>();
    }

    [MessagePackObject]
    public class GameSaveProfilesParameters
    {
        [Key("profiles")]
        public List<Profile> profiles { get; set; } = new List<Profile>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveProfilesResponses : IAPIResponse
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
    public class GameDeleteProfilesParameters
    {
        [Key("profiles")]
        public List<Profile> profiles { get; set; } = new List<Profile>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteProfilesResponses : IAPIResponse
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
    public class GameRankingRewardsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameRankingRewardsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("rankingRewards")]
        public List<RankingReward> rankingRewards { get; set; } = new List<RankingReward>();
    }

    [MessagePackObject]
    public class GameRpgAttributesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameRpgAttributesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("rpgAttributes")]
        public List<RpgAttribute> rpgAttributes { get; set; } = new List<RpgAttribute>();
    }

    [MessagePackObject]
    public class GameSaveRpgAttributesParameters
    {
        [Key("rpgAttributes")]
        public List<RpgAttribute> rpgAttributes { get; set; } = new List<RpgAttribute>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveRpgAttributesResponses : IAPIResponse
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
    public class GameDeleteRpgAttributesParameters
    {
        [Key("rpgAttributes")]
        public List<RpgAttribute> rpgAttributes { get; set; } = new List<RpgAttribute>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteRpgAttributesResponses : IAPIResponse
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
    public class GameSeasonMissionsParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSeasonMissionsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("seasonMissions")]
        public List<SeasonMission> seasonMissions { get; set; } = new List<SeasonMission>();
    }

    [MessagePackObject]
    public class GameSaveSeasonMissionsParameters
    {
        [Key("seasonMissions")]
        public List<SeasonMission> seasonMissions { get; set; } = new List<SeasonMission>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveSeasonMissionsResponses : IAPIResponse
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
    public class GameSeasonPassesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSeasonPassesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("seasonPasses")]
        public List<SeasonPass> seasonPasses { get; set; } = new List<SeasonPass>();
    }

    [MessagePackObject]
    public class GameSaveSeasonPassParameters
    {
        [Key("seasonPass")]
        public SeasonPass seasonPass { get; set; } = new SeasonPass();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveSeasonPassResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GameTreasureBoxesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameTreasureBoxesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("treasureBoxes")]
        public List<TreasureBox> treasureBoxes { get; set; } = new List<TreasureBox>();
    }

    [MessagePackObject]
    public class GameSaveTreasureBoxesParameters
    {
        [Key("treasureBoxes")]
        public List<TreasureBox> treasureBoxes { get; set; } = new List<TreasureBox>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveTreasureBoxesResponses : IAPIResponse
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
    public class GameDeleteTreasureBoxesParameters
    {
        [Key("treasureBoxes")]
        public List<TreasureBox> treasureBoxes { get; set; } = new List<TreasureBox>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteTreasureBoxesResponses : IAPIResponse
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
    public class GameUserAccountByNickParameters
    {
        [Key("Nick")]
        public string Nick { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserAccountByNickResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
    }

    [MessagePackObject]
    public class GameUserAccountByUIDParameters
    {
        [Key("UID")]
        public string UID { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserAccountByUIDResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
    }

    [MessagePackObject]
    public class GameUserAccountByMemberNoParameters
    {
        [Key("MemberNo")]
        public string MemberNo { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserAccountByMemberNoResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
    }

    [MessagePackObject]
    public class GameSaveUserAccountParameters
    {
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveUserAccountResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GameUserBlockByNickParameters
    {
        [Key("Nick")]
        public string Nick { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserBlockByNickResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
        [Key("accounts")]
        public List<Account> accounts { get; set; } = new List<Account>();
        [Key("userBlocks")]
        public List<UserBlock> userBlocks { get; set; } = new List<UserBlock>();
    }

    [MessagePackObject]
    public class GameUserBlockByMemberNoParameters
    {
        [Key("MemberNo")]
        public string MemberNo { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserBlockByMemberNoResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
        [Key("accounts")]
        public List<Account> accounts { get; set; } = new List<Account>();
        [Key("userBlocks")]
        public List<UserBlock> userBlocks { get; set; } = new List<UserBlock>();
    }

    [MessagePackObject]
    public class GameUserBlockByUIDParameters
    {
        [Key("UID")]
        public string UID { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserBlockByUIDResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userAccount")]
        public UserAccount userAccount { get; set; } = new UserAccount();
        [Key("accounts")]
        public List<Account> accounts { get; set; } = new List<Account>();
        [Key("userBlocks")]
        public List<UserBlock> userBlocks { get; set; } = new List<UserBlock>();
    }

    [MessagePackObject]
    public class GameSaveUserBlockParameters
    {
        [Key("userBlocks")]
        public List<UserBlock> userBlocks { get; set; } = new List<UserBlock>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveUserBlockResponses : IAPIResponse
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
    public class GameDeleteUserBlockParameters
    {
        [Key("userBlocks")]
        public List<UserBlock> userBlocks { get; set; } = new List<UserBlock>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteUserBlockResponses : IAPIResponse
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
    public class GameUserDevicesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameUserDevicesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("userDevices")]
        public List<UserDevice> userDevices { get; set; } = new List<UserDevice>();
    }

    [MessagePackObject]
    public class GameWeaponCategoriesParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameWeaponCategoriesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("weaponCategories")]
        public List<WeaponCategory> weaponCategories { get; set; } = new List<WeaponCategory>();
    }

    [MessagePackObject]
    public class GameSaveWeaponCategoriesParameters
    {
        [Key("weaponCategories")]
        public List<WeaponCategory> weaponCategories { get; set; } = new List<WeaponCategory>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveWeaponCategoriesResponses : IAPIResponse
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
    public class GameDeleteWeaponCategoriesParameters
    {
        [Key("weaponCategories")]
        public List<WeaponCategory> weaponCategories { get; set; } = new List<WeaponCategory>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteWeaponCategoriesResponses : IAPIResponse
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
    public class GameSearchUserAccountsParameters
    {
        [Key("userUIDs")]
        public List<Int64> userUIDs { get; set; } = new List<Int64>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSearchUserAccountsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
        [Key("userAccounts")]
        public Dictionary<Int64, UserAccount?> userAccounts { get; set; } = new Dictionary<Int64, UserAccount?>();
    }

    [MessagePackObject]
    public class GameSearchUserAccountsByMemberNoParameters
    {
        [Key("memberNos")]
        public List<UInt64> memberNos { get; set; } = new List<UInt64>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSearchUserAccountsByMemberNoResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorIndex")]
        public int errorIndex { get; set; } = 0;
        [Key("userAccounts")]
        public Dictionary<Int64, UserAccount?> userAccounts { get; set; } = new Dictionary<Int64, UserAccount?>();
    }

    [MessagePackObject]
    public class GameImmediatelySendMailParameters
    {
        [Key("userUIDs")]
        public List<Int64> userUIDs { get; set; } = new List<Int64>();
        [Key("mailInput")]
        public MailInput mailInput { get; set; } = new MailInput();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameImmediatelySendMailResponses : IAPIResponse
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
    public class GameMailWithUsersParameters
    {
        [Key("startTime")]
        public string startTime { get; set; } = "";
        [Key("endTime")]
        public string endTime { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameMailWithUsersResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("mails")]
        public List<MailWithUser> mails { get; set; } = new List<MailWithUser>();
    }

    [MessagePackObject]
    public class GameDeleteMailWithUsersParameters
    {
        [Key("mailIDs")]
        public List<UInt64> mailIDs { get; set; } = new List<UInt64>();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteMailWithUsersResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("errorMailId")]
        public Int64 errorMailId { get; set; } = 0;
    }

    [MessagePackObject]
    public class GameMaintenancesParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameMaintenancesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("maintenances")]
        public List<Maintenance> maintenances { get; set; } = new List<Maintenance>();
    }

    [MessagePackObject]
    public class GameSaveMaintenancesParameters
    {
        [Key("maintenances")]
        public List<Maintenance>? maintenances { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveMaintenancesResponses : IAPIResponse
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
    public class GameDeleteMaintenancesParameters
    {
        [Key("maintenances")]
        public List<Maintenance>? maintenances { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteMaintenancesResponses : IAPIResponse
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
    public class GameNoticeBannersParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameNoticeBannersResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("noticeBanners")]
        public List<NoticeBanner> noticeBanners { get; set; } = new List<NoticeBanner>();
    }

    [MessagePackObject]
    public class GameNoticeBannerParameters
    {
        [Key("BannerID")]
        public Int64 BannerID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameNoticeBannerResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("noticeBanner")]
        public NoticeBanner noticeBanner { get; set; } = new NoticeBanner();
    }

    [MessagePackObject]
    public class GameSaveNoticeBannersParameters
    {
        [Key("noticeBanners")]
        public List<NoticeBanner>? noticeBanners { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveNoticeBannersResponses : IAPIResponse
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
    public class GameDeleteNoticeBannersParameters
    {
        [Key("noticeBanners")]
        public List<NoticeBanner>? noticeBanners { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteNoticeBannersResponses : IAPIResponse
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
    public class GameOpenWorldTimeParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameOpenWorldTimeResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("openWorldTime")]
        public OpenWorldTime openWorldTime { get; set; } = new OpenWorldTime();
    }

    [MessagePackObject]
    public class GameSaveOpenWorldTimeParameters
    {
        [Key("openWorldTime")]
        public OpenWorldTime openWorldTime { get; set; } = new OpenWorldTime();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveOpenWorldTimeResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GameChattingNoticesParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameChattingNoticesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("chattingNotices")]
        public List<ChattingNotice> chattingNotices { get; set; } = new List<ChattingNotice>();
    }

    [MessagePackObject]
    public class GameChattingNoticeParameters
    {
        [Key("id")]
        public string id { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameChattingNoticeResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("chattingNotice")]
        public ChattingNotice? chattingNotice { get; set; } = null;
    }

    [MessagePackObject]
    public class GameSaveChattingNoticesParameters
    {
        [Key("chattingNotices")]
        public List<ChattingNotice>? chattingNotices { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveChattingNoticesResponses : IAPIResponse
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
    public class GameDeleteChattingNoticesParameters
    {
        [Key("chattingNotices")]
        public List<ChattingNotice>? chattingNotices { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDeleteChattingNoticesResponses : IAPIResponse
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
    public class GameImmediatelyChattingNoticeParameters
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameImmediatelyChattingNoticeResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GameGoldClashTimeParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameGoldClashTimeResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("goldClashTime")]
        public GoldClashTime goldClashTime { get; set; } = new GoldClashTime();
    }

    [MessagePackObject]
    public class GameSaveGoldClashTimeParameters
    {
        [Key("goldClashTime")]
        public GoldClashTime goldClashTime { get; set; } = new GoldClashTime();
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveGoldClashTimeResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class GameSilverMedalStoresParameters
    {
        [Key("UID")]
        public Int64 UID { get; set; } = 0;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSilverMedalStoresResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("silverMedalStores")]
        public List<SilverMedalStore> silverMedalStores { get; set; } = new List<SilverMedalStore>();
    }

    [MessagePackObject]
    public class GameLobbyServerTimesParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameLobbyServerTimesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("lobbyServerTimes")]
        public List<LobbyServerTime> lobbyServerTimes { get; set; } = new List<LobbyServerTime>();
    }

    [MessagePackObject]
    public class GameSaveLobbyServerTimesParameters
    {
        [Key("serverTime")]
        public DateTime? serverTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameSaveLobbyServerTimesResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("lobbyServerTimes")]
        public List<LobbyServerTime> lobbyServerTimes { get; set; } = new List<LobbyServerTime>();
    }

    [MessagePackObject]
    public class GameEventMailsParameters
    {
        [Key("startTime")]
        public DateTime? startTime { get; set; } = null;
        [Key("endTime")]
        public DateTime? endTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameEventMailsResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("eventMails")]
        public List<EventMail> eventMails { get; set; } = new List<EventMail>();
    }

    [MessagePackObject]
    public class GameEventMailParameters
    {
        [Key("id")]
        public Int64? id { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameEventMailResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("eventMail")]
        public EventMail? eventMail { get; set; } = null;
    }

    [MessagePackObject]
    public class GameDailyRankingParameters
    {
        [Key("gameType")]
        public Defines.GameType gameType { get; set; } = 0;
        [Key("dateTime")]
        public DateTime? dateTime { get; set; } = null;
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class GameDailyRankingResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("gameType")]
        public Defines.GameType gameType { get; set; } = 0;
        [Key("gameRankingScore")]
        public List<GameRankingScore> gameRankingScore { get; set; } = new List<GameRankingScore>();
    }

}
