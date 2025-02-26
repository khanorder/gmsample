using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using NGEL.Data.Tables.Models;
using NGEL.Data.Helpers;
using Lobby;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NGEL.Data.Tables
{

    public class DataTableService
    {
        public readonly AccountLevelTableService _accountLevelTableService;
        public readonly AchievementDataTableService _achievementDataTableService;
        public readonly AchievementGroupDataTableService _achievementGroupDataTableService;
        public readonly ArtifactDataTableService _artifactDataTableService;
        public readonly AssetDataTableService _assetDataTableService;
        public readonly AttendanceDataTableService _attendanceDataTableService;
        public readonly AttendanceRewardDataTableService _attendanceRewardDataTableService;
        public readonly AttributeDataTableService _attributeDataTableService;
        public readonly BattleStoreDataTableService _battleStoreDataTableService;
        public readonly ChapterDataTableService _chapterDataTableService;
        public readonly CharacterDataTableService _characterDataTableService;
        public readonly CollectionDataTableService _collectionDataTableService;
        public readonly CollectionGroupDataTableService _collectionGroupDataTableService;
        public readonly ColorDataTableService _colorDataTableService;
        public readonly DataChipStoreListDataTableService _dataChipStoreListDataTableService;
        public readonly EntitlementDataTableService _entitlementDataTableService;
        public readonly EventStoreDataTableService _eventStoreDataTableService;
        public readonly ExpressionDataTableService _expressionDataTableService;
        public readonly GlobalDefineDataTableService _globalDefineDataTableService;
        public readonly GuideMissionDataTableService _guideMissionDataTableService;
        public readonly GuideMissionStepRewardTableService _guideMissionStepRewardTableService;
        public readonly GlitchStoreDataTableService _glitchStoreDataTableService;
        public readonly GoldMedalStoreDataTableService _goldMedalStoreDataTableService;
        public readonly IncubationDataTableService _incubationDataTableService;
        public readonly InstantGuideUIDataTableService _instantGuideUIDataTableService;
        public readonly ItemTableService _itemTableService;
        public readonly IntroduceDataTableService _introduceDataTableService;
        public readonly LevelUpBuffListDataTableService _levelUpBuffListDataTableService;
        public readonly MissionDataTableService _missionDataTableService;
        public readonly ParameterDataTableService _parameterDataTableService;
        public readonly PenaltyDataTableService _penaltyDataTableService;
        public readonly PetDataTableService _petDataTableService;
        public readonly PetAbilityListDataTableService _petAbilityListDataTableService;
        public readonly PetEggDataTableService _petEggDataTableService;
        public readonly PetEggGroupDataTableService _petEggGroupDataTableService;
        public readonly ProfileDataTableService _profileDataTableService;
        public readonly QuestDataTableService _questDataTableService;
        public readonly RewardDataTableService _rewardDataTableService;
        public readonly SeasonMissionCountDataTableService _seasonMissionCountDataTableService;
        public readonly SeasonMissionListDataTableService _seasonMissionListDataTableService;
        public readonly SeasonPassDataTableService _seasonPassDataTableService;
        public readonly SeasonPassLevelDataTableService _seasonPassLevelDataTableService;
        public readonly SeasonPassRewardDataTableService _seasonPassRewardDataTableService;
        public readonly SilverMedalStoreDataTableService _silverMedalStoreDataTableService;
        public readonly SkillDataTableService _skillDataTableService;
        public readonly SkinDataTableService _skinDataTableService;
        public readonly StringDataTableService _stringDataTableService;
        public readonly UserBlockStringDataTableService _userBlockStringDataTableService;
        public readonly TreasureBoxDataTableService _treasureBoxDataTableService;
        public readonly VehicleDataTableService _vehicleDataTableService;
        public readonly WeaponCategoryDataTableService _weaponCategoryDataTableService;
        public readonly WeaponCategoryUpgradeDataTableService _weaponCategoryUpgradeDataTableService;
        public readonly WonderCubeDataTableService _wonderCubeDataTableService;
        public readonly WonderCubeRwardDataTableService _wonderCubeRwardDataTableService;
        public readonly WonderStoreDataTableService _wonderStoreDataTableService;
        public readonly WorldMapDataTableService _worldMapDataTableService;
        public readonly BiskitLogEventIDTableService _biskitLogEventIDTableService;
        public readonly BlockContentDataTableService _blockContentDataTableService;
        public readonly ErrorsDataTableService _errorsDataTableService;
        public readonly NavMenuDataTableService _navMenuDataTableService;

        public DataTableService
        (
            AccountLevelTableService accountLevelTableService
            ,AchievementDataTableService achievementDataTableService
            ,AchievementGroupDataTableService achievementGroupDataTableService
            ,ArtifactDataTableService artifactDataTableService
            ,AssetDataTableService assetDataTableService
            ,AttendanceDataTableService attendanceDataTableService
            ,AttendanceRewardDataTableService attendanceRewardDataTableService
            ,AttributeDataTableService attributeDataTableService
            ,BattleStoreDataTableService battleStoreDataTableService
            ,ChapterDataTableService chapterDataTableService
            ,CharacterDataTableService characterDataTableService
            ,CollectionDataTableService collectionDataTableService
            ,CollectionGroupDataTableService collectionGroupDataTableService
            ,ColorDataTableService colorDataTableService
            ,DataChipStoreListDataTableService dataChipStoreListDataTableService
            ,EntitlementDataTableService entitlementDataTableService
            ,EventStoreDataTableService eventStoreDataTableService
            ,ExpressionDataTableService expressionDataTableService
            ,GlobalDefineDataTableService globalDefineDataTableService
            ,GuideMissionDataTableService guideMissionDataTableService
            ,GuideMissionStepRewardTableService guideMissionStepRewardTableService
            ,GlitchStoreDataTableService glitchStoreDataTableService
            ,GoldMedalStoreDataTableService goldMedalStoreDataTableService
            ,IncubationDataTableService incubationDataTableService
            ,InstantGuideUIDataTableService instantGuideUIDataTableService
            ,ItemTableService itemTableService
            ,IntroduceDataTableService introduceDataTableService
            ,LevelUpBuffListDataTableService levelUpBuffListDataTableService
            ,MissionDataTableService missionDataTableService
            ,ParameterDataTableService parameterDataTableService
            ,PenaltyDataTableService penaltyDataTableService
            ,PetDataTableService petDataTableService
            ,PetAbilityListDataTableService petAbilityListDataTableService
            ,PetEggDataTableService petEggDataTableService
            ,PetEggGroupDataTableService petEggGroupDataTableService
            ,ProfileDataTableService profileDataTableService
            ,QuestDataTableService questDataTableService
            ,RewardDataTableService rewardDataTableService
            ,SeasonMissionCountDataTableService seasonMissionCountDataTableService
            ,SeasonMissionListDataTableService seasonMissionListDataTableService
            ,SeasonPassDataTableService seasonPassDataTableService
            ,SeasonPassLevelDataTableService seasonPassLevelDataTableService
            ,SeasonPassRewardDataTableService seasonPassRewardDataTableService
            ,SilverMedalStoreDataTableService silverMedalStoreDataTableService
            ,SkillDataTableService skillDataTableService
            ,SkinDataTableService skinDataTableService
            ,StringDataTableService stringDataTableService
            ,UserBlockStringDataTableService userBlockStringDataTableService
            ,TreasureBoxDataTableService treasureBoxDataTableService
            ,VehicleDataTableService vehicleDataTableService
            ,WeaponCategoryDataTableService weaponCategoryDataTableService
            ,WeaponCategoryUpgradeDataTableService weaponCategoryUpgradeDataTableService
            ,WonderCubeDataTableService wonderCubeDataTableService
            ,WonderCubeRwardDataTableService wonderCubeRwardDataTableService
            ,WonderStoreDataTableService wonderStoreDataTableService
            ,WorldMapDataTableService worldMapDataTableService
            ,BiskitLogEventIDTableService biskitLogEventIDTableService
            ,BlockContentDataTableService blockContentDataTableService
            ,ErrorsDataTableService errorsDataTableService
            ,NavMenuDataTableService navMenuDataTableService
        )
        {
            _accountLevelTableService = accountLevelTableService;
            _achievementDataTableService = achievementDataTableService;
            _achievementGroupDataTableService = achievementGroupDataTableService;
            _artifactDataTableService = artifactDataTableService;
            _assetDataTableService = assetDataTableService;
            _attendanceDataTableService = attendanceDataTableService;
            _attendanceRewardDataTableService = attendanceRewardDataTableService;
            _attributeDataTableService = attributeDataTableService;
            _battleStoreDataTableService = battleStoreDataTableService;
            _chapterDataTableService = chapterDataTableService;
            _characterDataTableService = characterDataTableService;
            _collectionDataTableService = collectionDataTableService;
            _collectionGroupDataTableService = collectionGroupDataTableService;
            _colorDataTableService = colorDataTableService;
            _dataChipStoreListDataTableService = dataChipStoreListDataTableService;
            _entitlementDataTableService = entitlementDataTableService;
            _eventStoreDataTableService = eventStoreDataTableService;
            _expressionDataTableService = expressionDataTableService;
            _globalDefineDataTableService = globalDefineDataTableService;
            _guideMissionDataTableService = guideMissionDataTableService;
            _guideMissionStepRewardTableService = guideMissionStepRewardTableService;
            _glitchStoreDataTableService = glitchStoreDataTableService;
            _goldMedalStoreDataTableService = goldMedalStoreDataTableService;
            _incubationDataTableService = incubationDataTableService;
            _instantGuideUIDataTableService = instantGuideUIDataTableService;
            _itemTableService = itemTableService;
            _introduceDataTableService = introduceDataTableService;
            _levelUpBuffListDataTableService = levelUpBuffListDataTableService;
            _missionDataTableService = missionDataTableService;
            _parameterDataTableService = parameterDataTableService;
            _penaltyDataTableService = penaltyDataTableService;
            _petDataTableService = petDataTableService;
            _petAbilityListDataTableService = petAbilityListDataTableService;
            _petEggDataTableService = petEggDataTableService;
            _petEggGroupDataTableService = petEggGroupDataTableService;
            _profileDataTableService = profileDataTableService;
            _questDataTableService = questDataTableService;
            _rewardDataTableService = rewardDataTableService;
            _seasonMissionCountDataTableService = seasonMissionCountDataTableService;
            _seasonMissionListDataTableService = seasonMissionListDataTableService;
            _seasonPassDataTableService = seasonPassDataTableService;
            _seasonPassLevelDataTableService = seasonPassLevelDataTableService;
            _seasonPassRewardDataTableService = seasonPassRewardDataTableService;
            _silverMedalStoreDataTableService = silverMedalStoreDataTableService;
            _skillDataTableService = skillDataTableService;
            _skinDataTableService = skinDataTableService;
            _stringDataTableService = stringDataTableService;
            _userBlockStringDataTableService = userBlockStringDataTableService;
            _treasureBoxDataTableService = treasureBoxDataTableService;
            _vehicleDataTableService = vehicleDataTableService;
            _weaponCategoryDataTableService = weaponCategoryDataTableService;
            _weaponCategoryUpgradeDataTableService = weaponCategoryUpgradeDataTableService;
            _wonderCubeDataTableService = wonderCubeDataTableService;
            _wonderCubeRwardDataTableService = wonderCubeRwardDataTableService;
            _wonderStoreDataTableService = wonderStoreDataTableService;
            _worldMapDataTableService = worldMapDataTableService;
            _biskitLogEventIDTableService = biskitLogEventIDTableService;
            _blockContentDataTableService = blockContentDataTableService;
            _errorsDataTableService = errorsDataTableService;
            _navMenuDataTableService = navMenuDataTableService;
        }

        public DataTable ExportDataTable => new DataTable()
        {
            accountLevelVersion = _accountLevelTableService.version.ToString(),
            accountLevels = _accountLevelTableService.datas,
            achievementDataVersion = _achievementDataTableService.version.ToString(),
            achievementDatas = _achievementDataTableService.datas,
            achievementGroupDataVersion = _achievementGroupDataTableService.version.ToString(),
            achievementGroupDatas = _achievementGroupDataTableService.datas,
            artifactDataVersion = _artifactDataTableService.version.ToString(),
            artifactDatas = _artifactDataTableService.datas,
            assetDataVersion = _assetDataTableService.version.ToString(),
            assetDatas = _assetDataTableService.datas,
            attendanceDataVersion = _attendanceDataTableService.version.ToString(),
            attendanceDatas = _attendanceDataTableService.datas,
            attendanceRewardDataVersion = _attendanceRewardDataTableService.version.ToString(),
            attendanceRewardDatas = _attendanceRewardDataTableService.datas,
            attributeDataVersion = _attributeDataTableService.version.ToString(),
            attributeDatas = _attributeDataTableService.datas,
            battleStoreDataVersion = _battleStoreDataTableService.version.ToString(),
            battleStoreDatas = _battleStoreDataTableService.datas,
            chapterDataVersion = _chapterDataTableService.version.ToString(),
            chapterDatas = _chapterDataTableService.datas,
            characterDataVersion = _characterDataTableService.version.ToString(),
            characterDatas = _characterDataTableService.datas,
            collectionDataVersion = _collectionDataTableService.version.ToString(),
            collectionDatas = _collectionDataTableService.datas,
            collectionGroupDataVersion = _collectionGroupDataTableService.version.ToString(),
            collectionGroupDatas = _collectionGroupDataTableService.datas,
            colorDataVersion = _colorDataTableService.version.ToString(),
            colorDatas = _colorDataTableService.datas,
            dataChipStoreListDataVersion = _dataChipStoreListDataTableService.version.ToString(),
            dataChipStoreListDatas = _dataChipStoreListDataTableService.datas,
            entitlementDataVersion = _entitlementDataTableService.version.ToString(),
            entitlementDatas = _entitlementDataTableService.datas,
            eventStoreDataVersion = _eventStoreDataTableService.version.ToString(),
            eventStoreDatas = _eventStoreDataTableService.datas,
            expressionDataVersion = _expressionDataTableService.version.ToString(),
            expressionDatas = _expressionDataTableService.datas,
            globalDefineDataVersion = _globalDefineDataTableService.version.ToString(),
            globalDefineDatas = _globalDefineDataTableService.datas,
            guideMissionDataVersion = _guideMissionDataTableService.version.ToString(),
            guideMissionDatas = _guideMissionDataTableService.datas,
            guideMissionStepRewardVersion = _guideMissionStepRewardTableService.version.ToString(),
            guideMissionStepRewards = _guideMissionStepRewardTableService.datas,
            glitchStoreDataVersion = _glitchStoreDataTableService.version.ToString(),
            glitchStoreDatas = _glitchStoreDataTableService.datas,
            goldMedalStoreDataVersion = _goldMedalStoreDataTableService.version.ToString(),
            goldMedalStoreDatas = _goldMedalStoreDataTableService.datas,
            incubationDataVersion = _incubationDataTableService.version.ToString(),
            incubationDatas = _incubationDataTableService.datas,
            instantGuideUIDataVersion = _instantGuideUIDataTableService.version.ToString(),
            instantGuideUIDatas = _instantGuideUIDataTableService.datas,
            itemVersion = _itemTableService.version.ToString(),
            items = _itemTableService.datas,
            introduceDataVersion = _introduceDataTableService.version.ToString(),
            introduceDatas = _introduceDataTableService.datas,
            levelUpBuffListDataVersion = _levelUpBuffListDataTableService.version.ToString(),
            levelUpBuffListDatas = _levelUpBuffListDataTableService.datas,
            missionDataVersion = _missionDataTableService.version.ToString(),
            missionDatas = _missionDataTableService.datas,
            parameterDataVersion = _parameterDataTableService.version.ToString(),
            parameterDatas = _parameterDataTableService.datas,
            penaltyDataVersion = _penaltyDataTableService.version.ToString(),
            penaltyDatas = _penaltyDataTableService.datas,
            petDataVersion = _petDataTableService.version.ToString(),
            petDatas = _petDataTableService.datas,
            petAbilityListDataVersion = _petAbilityListDataTableService.version.ToString(),
            petAbilityListDatas = _petAbilityListDataTableService.datas,
            petEggDataVersion = _petEggDataTableService.version.ToString(),
            petEggDatas = _petEggDataTableService.datas,
            petEggGroupDataVersion = _petEggGroupDataTableService.version.ToString(),
            petEggGroupDatas = _petEggGroupDataTableService.datas,
            profileDataVersion = _profileDataTableService.version.ToString(),
            profileDatas = _profileDataTableService.datas,
            questDataVersion = _questDataTableService.version.ToString(),
            questDatas = _questDataTableService.datas,
            rewardDataVersion = _rewardDataTableService.version.ToString(),
            rewardDatas = _rewardDataTableService.datas,
            seasonMissionCountDataVersion = _seasonMissionCountDataTableService.version.ToString(),
            seasonMissionCountDatas = _seasonMissionCountDataTableService.datas,
            seasonMissionListDataVersion = _seasonMissionListDataTableService.version.ToString(),
            seasonMissionListDatas = _seasonMissionListDataTableService.datas,
            seasonPassDataVersion = _seasonPassDataTableService.version.ToString(),
            seasonPassDatas = _seasonPassDataTableService.datas,
            seasonPassLevelDataVersion = _seasonPassLevelDataTableService.version.ToString(),
            seasonPassLevelDatas = _seasonPassLevelDataTableService.datas,
            seasonPassRewardDataVersion = _seasonPassRewardDataTableService.version.ToString(),
            seasonPassRewardDatas = _seasonPassRewardDataTableService.datas,
            silverMedalStoreDataVersion = _silverMedalStoreDataTableService.version.ToString(),
            silverMedalStoreDatas = _silverMedalStoreDataTableService.datas,
            skillDataVersion = _skillDataTableService.version.ToString(),
            skillDatas = _skillDataTableService.datas,
            skinDataVersion = _skinDataTableService.version.ToString(),
            skinDatas = _skinDataTableService.datas,
            stringDataVersion = _stringDataTableService.version.ToString(),
            stringDatas = _stringDataTableService.datas,
            userBlockStringDataVersion = _userBlockStringDataTableService.version.ToString(),
            userBlockStringDatas = _userBlockStringDataTableService.datas,
            treasureBoxDataVersion = _treasureBoxDataTableService.version.ToString(),
            treasureBoxDatas = _treasureBoxDataTableService.datas,
            vehicleDataVersion = _vehicleDataTableService.version.ToString(),
            vehicleDatas = _vehicleDataTableService.datas,
            weaponCategoryDataVersion = _weaponCategoryDataTableService.version.ToString(),
            weaponCategoryDatas = _weaponCategoryDataTableService.datas,
            weaponCategoryUpgradeDataVersion = _weaponCategoryUpgradeDataTableService.version.ToString(),
            weaponCategoryUpgradeDatas = _weaponCategoryUpgradeDataTableService.datas,
            wonderCubeDataVersion = _wonderCubeDataTableService.version.ToString(),
            wonderCubeDatas = _wonderCubeDataTableService.datas,
            wonderCubeRwardDataVersion = _wonderCubeRwardDataTableService.version.ToString(),
            wonderCubeRwardDatas = _wonderCubeRwardDataTableService.datas,
            wonderStoreDataVersion = _wonderStoreDataTableService.version.ToString(),
            wonderStoreDatas = _wonderStoreDataTableService.datas,
            worldMapDataVersion = _worldMapDataTableService.version.ToString(),
            worldMapDatas = _worldMapDataTableService.datas,
            biskitLogEventIDVersion = _biskitLogEventIDTableService.version.ToString(),
            biskitLogEventIDs = _biskitLogEventIDTableService.datas,
            blockContentDataVersion = _blockContentDataTableService.version.ToString(),
            blockContentDatas = _blockContentDataTableService.datas,
            errorsDataVersion = _errorsDataTableService.version.ToString(),
            errorsDatas = _errorsDataTableService.datas,
            navMenuDataVersion = _navMenuDataTableService.version.ToString(),
            navMenuDatas = _navMenuDataTableService.datas
        };
    }
}
