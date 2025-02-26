/********************************************************/
/*Auto Create File*/
/*Source : Tool.AutoCreateComponents*/
/********************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using MessagePack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NGEL.Data.Tables.Models;
using NGEL.Data.Helpers;
using Lobby;

namespace NGEL.Data.Tables.Models
{

    [MessagePackObject]
    public class DataTable
    {
        [Key("accountLevelVersion")]
        public string accountLevelVersion = "";
        [Key("accountLevels")]
        public List<AccountLevel> accountLevels { get; set; } = new List<AccountLevel>();
        [Key("achievementDataVersion")]
        public string achievementDataVersion = "";
        [Key("achievementDatas")]
        public List<AchievementData> achievementDatas { get; set; } = new List<AchievementData>();
        [Key("achievementGroupDataVersion")]
        public string achievementGroupDataVersion = "";
        [Key("achievementGroupDatas")]
        public List<AchievementGroupData> achievementGroupDatas { get; set; } = new List<AchievementGroupData>();
        [Key("artifactDataVersion")]
        public string artifactDataVersion = "";
        [Key("artifactDatas")]
        public List<ArtifactData> artifactDatas { get; set; } = new List<ArtifactData>();
        [Key("assetDataVersion")]
        public string assetDataVersion = "";
        [Key("assetDatas")]
        public List<AssetData> assetDatas { get; set; } = new List<AssetData>();
        [Key("attendanceDataVersion")]
        public string attendanceDataVersion = "";
        [Key("attendanceDatas")]
        public List<AttendanceData> attendanceDatas { get; set; } = new List<AttendanceData>();
        [Key("attendanceRewardDataVersion")]
        public string attendanceRewardDataVersion = "";
        [Key("attendanceRewardDatas")]
        public List<AttendanceRewardData> attendanceRewardDatas { get; set; } = new List<AttendanceRewardData>();
        [Key("attributeDataVersion")]
        public string attributeDataVersion = "";
        [Key("attributeDatas")]
        public List<AttributeData> attributeDatas { get; set; } = new List<AttributeData>();
        [Key("battleStoreDataVersion")]
        public string battleStoreDataVersion = "";
        [Key("battleStoreDatas")]
        public List<BattleStoreData> battleStoreDatas { get; set; } = new List<BattleStoreData>();
        [Key("chapterDataVersion")]
        public string chapterDataVersion = "";
        [Key("chapterDatas")]
        public List<ChapterData> chapterDatas { get; set; } = new List<ChapterData>();
        [Key("characterDataVersion")]
        public string characterDataVersion = "";
        [Key("characterDatas")]
        public List<CharacterData> characterDatas { get; set; } = new List<CharacterData>();
        [Key("collectionDataVersion")]
        public string collectionDataVersion = "";
        [Key("collectionDatas")]
        public List<CollectionData> collectionDatas { get; set; } = new List<CollectionData>();
        [Key("collectionGroupDataVersion")]
        public string collectionGroupDataVersion = "";
        [Key("collectionGroupDatas")]
        public List<CollectionGroupData> collectionGroupDatas { get; set; } = new List<CollectionGroupData>();
        [Key("colorDataVersion")]
        public string colorDataVersion = "";
        [Key("colorDatas")]
        public List<ColorData> colorDatas { get; set; } = new List<ColorData>();
        [Key("dataChipStoreListDataVersion")]
        public string dataChipStoreListDataVersion = "";
        [Key("dataChipStoreListDatas")]
        public List<DataChipStoreListData> dataChipStoreListDatas { get; set; } = new List<DataChipStoreListData>();
        [Key("entitlementDataVersion")]
        public string entitlementDataVersion = "";
        [Key("entitlementDatas")]
        public List<EntitlementData> entitlementDatas { get; set; } = new List<EntitlementData>();
        [Key("eventStoreDataVersion")]
        public string eventStoreDataVersion = "";
        [Key("eventStoreDatas")]
        public List<EventStoreData> eventStoreDatas { get; set; } = new List<EventStoreData>();
        [Key("expressionDataVersion")]
        public string expressionDataVersion = "";
        [Key("expressionDatas")]
        public List<ExpressionData> expressionDatas { get; set; } = new List<ExpressionData>();
        [Key("globalDefineDataVersion")]
        public string globalDefineDataVersion = "";
        [Key("globalDefineDatas")]
        public List<GlobalDefineData> globalDefineDatas { get; set; } = new List<GlobalDefineData>();
        [Key("guideMissionDataVersion")]
        public string guideMissionDataVersion = "";
        [Key("guideMissionDatas")]
        public List<GuideMissionData> guideMissionDatas { get; set; } = new List<GuideMissionData>();
        [Key("guideMissionStepRewardVersion")]
        public string guideMissionStepRewardVersion = "";
        [Key("guideMissionStepRewards")]
        public List<GuideMissionStepReward> guideMissionStepRewards { get; set; } = new List<GuideMissionStepReward>();
        [Key("glitchStoreDataVersion")]
        public string glitchStoreDataVersion = "";
        [Key("glitchStoreDatas")]
        public List<GlitchStoreData> glitchStoreDatas { get; set; } = new List<GlitchStoreData>();
        [Key("goldMedalStoreDataVersion")]
        public string goldMedalStoreDataVersion = "";
        [Key("goldMedalStoreDatas")]
        public List<GoldMedalStoreData> goldMedalStoreDatas { get; set; } = new List<GoldMedalStoreData>();
        [Key("incubationDataVersion")]
        public string incubationDataVersion = "";
        [Key("incubationDatas")]
        public List<IncubationData> incubationDatas { get; set; } = new List<IncubationData>();
        [Key("instantGuideUIDataVersion")]
        public string instantGuideUIDataVersion = "";
        [Key("instantGuideUIDatas")]
        public List<InstantGuideUIData> instantGuideUIDatas { get; set; } = new List<InstantGuideUIData>();
        [Key("itemVersion")]
        public string itemVersion = "";
        [Key("items")]
        public List<Item> items { get; set; } = new List<Item>();
        [Key("introduceDataVersion")]
        public string introduceDataVersion = "";
        [Key("introduceDatas")]
        public List<IntroduceData> introduceDatas { get; set; } = new List<IntroduceData>();
        [Key("levelUpBuffListDataVersion")]
        public string levelUpBuffListDataVersion = "";
        [Key("levelUpBuffListDatas")]
        public List<LevelUpBuffListData> levelUpBuffListDatas { get; set; } = new List<LevelUpBuffListData>();
        [Key("missionDataVersion")]
        public string missionDataVersion = "";
        [Key("missionDatas")]
        public List<MissionData> missionDatas { get; set; } = new List<MissionData>();
        [Key("parameterDataVersion")]
        public string parameterDataVersion = "";
        [Key("parameterDatas")]
        public List<ParameterData> parameterDatas { get; set; } = new List<ParameterData>();
        [Key("penaltyDataVersion")]
        public string penaltyDataVersion = "";
        [Key("penaltyDatas")]
        public List<PenaltyData> penaltyDatas { get; set; } = new List<PenaltyData>();
        [Key("petDataVersion")]
        public string petDataVersion = "";
        [Key("petDatas")]
        public List<PetData> petDatas { get; set; } = new List<PetData>();
        [Key("petAbilityListDataVersion")]
        public string petAbilityListDataVersion = "";
        [Key("petAbilityListDatas")]
        public List<PetAbilityListData> petAbilityListDatas { get; set; } = new List<PetAbilityListData>();
        [Key("petEggDataVersion")]
        public string petEggDataVersion = "";
        [Key("petEggDatas")]
        public List<PetEggData> petEggDatas { get; set; } = new List<PetEggData>();
        [Key("petEggGroupDataVersion")]
        public string petEggGroupDataVersion = "";
        [Key("petEggGroupDatas")]
        public List<PetEggGroupData> petEggGroupDatas { get; set; } = new List<PetEggGroupData>();
        [Key("profileDataVersion")]
        public string profileDataVersion = "";
        [Key("profileDatas")]
        public List<ProfileData> profileDatas { get; set; } = new List<ProfileData>();
        [Key("questDataVersion")]
        public string questDataVersion = "";
        [Key("questDatas")]
        public List<QuestData> questDatas { get; set; } = new List<QuestData>();
        [Key("rewardDataVersion")]
        public string rewardDataVersion = "";
        [Key("rewardDatas")]
        public List<RewardData> rewardDatas { get; set; } = new List<RewardData>();
        [Key("seasonMissionCountDataVersion")]
        public string seasonMissionCountDataVersion = "";
        [Key("seasonMissionCountDatas")]
        public List<SeasonMissionCountData> seasonMissionCountDatas { get; set; } = new List<SeasonMissionCountData>();
        [Key("seasonMissionListDataVersion")]
        public string seasonMissionListDataVersion = "";
        [Key("seasonMissionListDatas")]
        public List<SeasonMissionListData> seasonMissionListDatas { get; set; } = new List<SeasonMissionListData>();
        [Key("seasonPassDataVersion")]
        public string seasonPassDataVersion = "";
        [Key("seasonPassDatas")]
        public List<SeasonPassData> seasonPassDatas { get; set; } = new List<SeasonPassData>();
        [Key("seasonPassLevelDataVersion")]
        public string seasonPassLevelDataVersion = "";
        [Key("seasonPassLevelDatas")]
        public List<SeasonPassLevelData> seasonPassLevelDatas { get; set; } = new List<SeasonPassLevelData>();
        [Key("seasonPassRewardDataVersion")]
        public string seasonPassRewardDataVersion = "";
        [Key("seasonPassRewardDatas")]
        public List<SeasonPassRewardData> seasonPassRewardDatas { get; set; } = new List<SeasonPassRewardData>();
        [Key("silverMedalStoreDataVersion")]
        public string silverMedalStoreDataVersion = "";
        [Key("silverMedalStoreDatas")]
        public List<SilverMedalStoreData> silverMedalStoreDatas { get; set; } = new List<SilverMedalStoreData>();
        [Key("skillDataVersion")]
        public string skillDataVersion = "";
        [Key("skillDatas")]
        public List<SkillData> skillDatas { get; set; } = new List<SkillData>();
        [Key("skinDataVersion")]
        public string skinDataVersion = "";
        [Key("skinDatas")]
        public List<SkinData> skinDatas { get; set; } = new List<SkinData>();
        [Key("stringDataVersion")]
        public string stringDataVersion = "";
        [Key("stringDatas")]
        public List<StringData> stringDatas { get; set; } = new List<StringData>();
        [Key("userBlockStringDataVersion")]
        public string userBlockStringDataVersion = "";
        [Key("userBlockStringDatas")]
        public List<UserBlockStringData> userBlockStringDatas { get; set; } = new List<UserBlockStringData>();
        [Key("treasureBoxDataVersion")]
        public string treasureBoxDataVersion = "";
        [Key("treasureBoxDatas")]
        public List<TreasureBoxData> treasureBoxDatas { get; set; } = new List<TreasureBoxData>();
        [Key("vehicleDataVersion")]
        public string vehicleDataVersion = "";
        [Key("vehicleDatas")]
        public List<VehicleData> vehicleDatas { get; set; } = new List<VehicleData>();
        [Key("weaponCategoryDataVersion")]
        public string weaponCategoryDataVersion = "";
        [Key("weaponCategoryDatas")]
        public List<WeaponCategoryData> weaponCategoryDatas { get; set; } = new List<WeaponCategoryData>();
        [Key("weaponCategoryUpgradeDataVersion")]
        public string weaponCategoryUpgradeDataVersion = "";
        [Key("weaponCategoryUpgradeDatas")]
        public List<WeaponCategoryUpgradeData> weaponCategoryUpgradeDatas { get; set; } = new List<WeaponCategoryUpgradeData>();
        [Key("wonderCubeDataVersion")]
        public string wonderCubeDataVersion = "";
        [Key("wonderCubeDatas")]
        public List<WonderCubeData> wonderCubeDatas { get; set; } = new List<WonderCubeData>();
        [Key("wonderCubeRwardDataVersion")]
        public string wonderCubeRwardDataVersion = "";
        [Key("wonderCubeRwardDatas")]
        public List<WonderCubeRwardData> wonderCubeRwardDatas { get; set; } = new List<WonderCubeRwardData>();
        [Key("wonderStoreDataVersion")]
        public string wonderStoreDataVersion = "";
        [Key("wonderStoreDatas")]
        public List<WonderStoreData> wonderStoreDatas { get; set; } = new List<WonderStoreData>();
        [Key("worldMapDataVersion")]
        public string worldMapDataVersion = "";
        [Key("worldMapDatas")]
        public List<WorldMapData> worldMapDatas { get; set; } = new List<WorldMapData>();
        [Key("biskitLogEventIDVersion")]
        public string biskitLogEventIDVersion = "";
        [Key("biskitLogEventIDs")]
        public Dictionary<string, BiskitLogEventID> biskitLogEventIDs { get; set; } = new Dictionary<string, BiskitLogEventID>();
        [Key("blockContentDataVersion")]
        public string blockContentDataVersion = "";
        [Key("blockContentDatas")]
        public Dictionary<string, BlockContentData> blockContentDatas { get; set; } = new Dictionary<string, BlockContentData>();
        [Key("errorsDataVersion")]
        public string errorsDataVersion = "";
        [Key("errorsDatas")]
        public Dictionary<Errors, ErrorsData> errorsDatas { get; set; } = new Dictionary<Errors, ErrorsData>();
        [Key("navMenuDataVersion")]
        public string navMenuDataVersion = "";
        [Key("navMenuDatas")]
        public Dictionary<string, NavMenuData> navMenuDatas { get; set; } = new Dictionary<string, NavMenuData>();
    }
}
