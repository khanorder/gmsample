import { Errors } from '../autoErrors';
import { DataTableModels } from './model';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import deepmerge from 'deepmerge';

interface TableState {
    accountLevelTableVersion: string;
    accountLevelTable: DataTableModels.AccountLevel[];
    achievementDataTableVersion: string;
    achievementDataTable: DataTableModels.AchievementData[];
    achievementGroupDataTableVersion: string;
    achievementGroupDataTable: DataTableModels.AchievementGroupData[];
    artifactDataTableVersion: string;
    artifactDataTable: DataTableModels.ArtifactData[];
    assetDataTableVersion: string;
    assetDataTable: DataTableModels.AssetData[];
    attendanceDataTableVersion: string;
    attendanceDataTable: DataTableModels.AttendanceData[];
    attendanceRewardDataTableVersion: string;
    attendanceRewardDataTable: DataTableModels.AttendanceRewardData[];
    attributeDataTableVersion: string;
    attributeDataTable: DataTableModels.AttributeData[];
    battleStoreDataTableVersion: string;
    battleStoreDataTable: DataTableModels.BattleStoreData[];
    chapterDataTableVersion: string;
    chapterDataTable: DataTableModels.ChapterData[];
    characterDataTableVersion: string;
    characterDataTable: DataTableModels.CharacterData[];
    collectionDataTableVersion: string;
    collectionDataTable: DataTableModels.CollectionData[];
    collectionGroupDataTableVersion: string;
    collectionGroupDataTable: DataTableModels.CollectionGroupData[];
    colorDataTableVersion: string;
    colorDataTable: DataTableModels.ColorData[];
    dataChipStoreListDataTableVersion: string;
    dataChipStoreListDataTable: DataTableModels.DataChipStoreListData[];
    entitlementDataTableVersion: string;
    entitlementDataTable: DataTableModels.EntitlementData[];
    eventStoreDataTableVersion: string;
    eventStoreDataTable: DataTableModels.EventStoreData[];
    expressionDataTableVersion: string;
    expressionDataTable: DataTableModels.ExpressionData[];
    globalDefineDataTableVersion: string;
    globalDefineDataTable: DataTableModels.GlobalDefineData[];
    guideMissionDataTableVersion: string;
    guideMissionDataTable: DataTableModels.GuideMissionData[];
    guideMissionStepRewardTableVersion: string;
    guideMissionStepRewardTable: DataTableModels.GuideMissionStepReward[];
    glitchStoreDataTableVersion: string;
    glitchStoreDataTable: DataTableModels.GlitchStoreData[];
    goldMedalStoreDataTableVersion: string;
    goldMedalStoreDataTable: DataTableModels.GoldMedalStoreData[];
    incubationDataTableVersion: string;
    incubationDataTable: DataTableModels.IncubationData[];
    instantGuideUIDataTableVersion: string;
    instantGuideUIDataTable: DataTableModels.InstantGuideUIData[];
    itemTableVersion: string;
    itemTable: DataTableModels.Item[];
    introduceDataTableVersion: string;
    introduceDataTable: DataTableModels.IntroduceData[];
    levelUpBuffListDataTableVersion: string;
    levelUpBuffListDataTable: DataTableModels.LevelUpBuffListData[];
    missionDataTableVersion: string;
    missionDataTable: DataTableModels.MissionData[];
    parameterDataTableVersion: string;
    parameterDataTable: DataTableModels.ParameterData[];
    penaltyDataTableVersion: string;
    penaltyDataTable: DataTableModels.PenaltyData[];
    petDataTableVersion: string;
    petDataTable: DataTableModels.PetData[];
    petAbilityListDataTableVersion: string;
    petAbilityListDataTable: DataTableModels.PetAbilityListData[];
    petEggDataTableVersion: string;
    petEggDataTable: DataTableModels.PetEggData[];
    petEggGroupDataTableVersion: string;
    petEggGroupDataTable: DataTableModels.PetEggGroupData[];
    profileDataTableVersion: string;
    profileDataTable: DataTableModels.ProfileData[];
    questDataTableVersion: string;
    questDataTable: DataTableModels.QuestData[];
    rewardDataTableVersion: string;
    rewardDataTable: DataTableModels.RewardData[];
    seasonMissionCountDataTableVersion: string;
    seasonMissionCountDataTable: DataTableModels.SeasonMissionCountData[];
    seasonMissionListDataTableVersion: string;
    seasonMissionListDataTable: DataTableModels.SeasonMissionListData[];
    seasonPassDataTableVersion: string;
    seasonPassDataTable: DataTableModels.SeasonPassData[];
    seasonPassLevelDataTableVersion: string;
    seasonPassLevelDataTable: DataTableModels.SeasonPassLevelData[];
    seasonPassRewardDataTableVersion: string;
    seasonPassRewardDataTable: DataTableModels.SeasonPassRewardData[];
    silverMedalStoreDataTableVersion: string;
    silverMedalStoreDataTable: DataTableModels.SilverMedalStoreData[];
    skillDataTableVersion: string;
    skillDataTable: DataTableModels.SkillData[];
    skinDataTableVersion: string;
    skinDataTable: DataTableModels.SkinData[];
    stringDataTableVersion: string;
    stringDataTable: DataTableModels.StringData[];
    userBlockStringDataTableVersion: string;
    userBlockStringDataTable: DataTableModels.UserBlockStringData[];
    treasureBoxDataTableVersion: string;
    treasureBoxDataTable: DataTableModels.TreasureBoxData[];
    vehicleDataTableVersion: string;
    vehicleDataTable: DataTableModels.VehicleData[];
    weaponCategoryDataTableVersion: string;
    weaponCategoryDataTable: DataTableModels.WeaponCategoryData[];
    weaponCategoryUpgradeDataTableVersion: string;
    weaponCategoryUpgradeDataTable: DataTableModels.WeaponCategoryUpgradeData[];
    wonderCubeDataTableVersion: string;
    wonderCubeDataTable: DataTableModels.WonderCubeData[];
    wonderCubeRwardDataTableVersion: string;
    wonderCubeRwardDataTable: DataTableModels.WonderCubeRwardData[];
    wonderStoreDataTableVersion: string;
    wonderStoreDataTable: DataTableModels.WonderStoreData[];
    worldMapDataTableVersion: string;
    worldMapDataTable: DataTableModels.WorldMapData[];
    biskitLogEventIDTableVersion: string;
    biskitLogEventIDTable: Map<string, DataTableModels.BiskitLogEventID>;
    blockContentDataTableVersion: string;
    blockContentDataTable: Map<string, DataTableModels.BlockContentData>;
    errorsDataTableVersion: string;
    errorsDataTable: Map<Errors, DataTableModels.ErrorsData>;
    navMenuDataTableVersion: string;
    navMenuDataTable: Map<string, DataTableModels.NavMenuData>;
}

const initialState: TableState = {
    accountLevelTableVersion: '',
    accountLevelTable: [],
    achievementDataTableVersion: '',
    achievementDataTable: [],
    achievementGroupDataTableVersion: '',
    achievementGroupDataTable: [],
    artifactDataTableVersion: '',
    artifactDataTable: [],
    assetDataTableVersion: '',
    assetDataTable: [],
    attendanceDataTableVersion: '',
    attendanceDataTable: [],
    attendanceRewardDataTableVersion: '',
    attendanceRewardDataTable: [],
    attributeDataTableVersion: '',
    attributeDataTable: [],
    battleStoreDataTableVersion: '',
    battleStoreDataTable: [],
    chapterDataTableVersion: '',
    chapterDataTable: [],
    characterDataTableVersion: '',
    characterDataTable: [],
    collectionDataTableVersion: '',
    collectionDataTable: [],
    collectionGroupDataTableVersion: '',
    collectionGroupDataTable: [],
    colorDataTableVersion: '',
    colorDataTable: [],
    dataChipStoreListDataTableVersion: '',
    dataChipStoreListDataTable: [],
    entitlementDataTableVersion: '',
    entitlementDataTable: [],
    eventStoreDataTableVersion: '',
    eventStoreDataTable: [],
    expressionDataTableVersion: '',
    expressionDataTable: [],
    globalDefineDataTableVersion: '',
    globalDefineDataTable: [],
    guideMissionDataTableVersion: '',
    guideMissionDataTable: [],
    guideMissionStepRewardTableVersion: '',
    guideMissionStepRewardTable: [],
    glitchStoreDataTableVersion: '',
    glitchStoreDataTable: [],
    goldMedalStoreDataTableVersion: '',
    goldMedalStoreDataTable: [],
    incubationDataTableVersion: '',
    incubationDataTable: [],
    instantGuideUIDataTableVersion: '',
    instantGuideUIDataTable: [],
    itemTableVersion: '',
    itemTable: [],
    introduceDataTableVersion: '',
    introduceDataTable: [],
    levelUpBuffListDataTableVersion: '',
    levelUpBuffListDataTable: [],
    missionDataTableVersion: '',
    missionDataTable: [],
    parameterDataTableVersion: '',
    parameterDataTable: [],
    penaltyDataTableVersion: '',
    penaltyDataTable: [],
    petDataTableVersion: '',
    petDataTable: [],
    petAbilityListDataTableVersion: '',
    petAbilityListDataTable: [],
    petEggDataTableVersion: '',
    petEggDataTable: [],
    petEggGroupDataTableVersion: '',
    petEggGroupDataTable: [],
    profileDataTableVersion: '',
    profileDataTable: [],
    questDataTableVersion: '',
    questDataTable: [],
    rewardDataTableVersion: '',
    rewardDataTable: [],
    seasonMissionCountDataTableVersion: '',
    seasonMissionCountDataTable: [],
    seasonMissionListDataTableVersion: '',
    seasonMissionListDataTable: [],
    seasonPassDataTableVersion: '',
    seasonPassDataTable: [],
    seasonPassLevelDataTableVersion: '',
    seasonPassLevelDataTable: [],
    seasonPassRewardDataTableVersion: '',
    seasonPassRewardDataTable: [],
    silverMedalStoreDataTableVersion: '',
    silverMedalStoreDataTable: [],
    skillDataTableVersion: '',
    skillDataTable: [],
    skinDataTableVersion: '',
    skinDataTable: [],
    stringDataTableVersion: '',
    stringDataTable: [],
    userBlockStringDataTableVersion: '',
    userBlockStringDataTable: [],
    treasureBoxDataTableVersion: '',
    treasureBoxDataTable: [],
    vehicleDataTableVersion: '',
    vehicleDataTable: [],
    weaponCategoryDataTableVersion: '',
    weaponCategoryDataTable: [],
    weaponCategoryUpgradeDataTableVersion: '',
    weaponCategoryUpgradeDataTable: [],
    wonderCubeDataTableVersion: '',
    wonderCubeDataTable: [],
    wonderCubeRwardDataTableVersion: '',
    wonderCubeRwardDataTable: [],
    wonderStoreDataTableVersion: '',
    wonderStoreDataTable: [],
    worldMapDataTableVersion: '',
    worldMapDataTable: [],
    biskitLogEventIDTableVersion: '',
    biskitLogEventIDTable: new Map<string, DataTableModels.BiskitLogEventID>(),
    blockContentDataTableVersion: '',
    blockContentDataTable: new Map<string, DataTableModels.BlockContentData>(),
    errorsDataTableVersion: '',
    errorsDataTable: new Map<Errors, DataTableModels.ErrorsData>(),
    navMenuDataTableVersion: '',
    navMenuDataTable: new Map<string, DataTableModels.NavMenuData>(),
}

const tableSlice = createSlice({
    name: 'Table',
    initialState,
    reducers: {
        setTables: (state, action: PayloadAction<DataTableModels.DataTable|null|undefined>) => {
            console.log(`call setTables exec.`);

            if (!action || !action.payload) {
                console.log(`dataTable is null.`);
                action.payload = new DataTableModels.DataTable();
            }

            state.accountLevelTableVersion = action.payload.accountLevelVersion;
            state.accountLevelTable = deepmerge([], action.payload.accountLevels);
            state.achievementDataTableVersion = action.payload.achievementDataVersion;
            state.achievementDataTable = deepmerge([], action.payload.achievementDatas);
            state.achievementGroupDataTableVersion = action.payload.achievementGroupDataVersion;
            state.achievementGroupDataTable = deepmerge([], action.payload.achievementGroupDatas);
            state.artifactDataTableVersion = action.payload.artifactDataVersion;
            state.artifactDataTable = deepmerge([], action.payload.artifactDatas);
            state.assetDataTableVersion = action.payload.assetDataVersion;
            state.assetDataTable = deepmerge([], action.payload.assetDatas);
            state.attendanceDataTableVersion = action.payload.attendanceDataVersion;
            state.attendanceDataTable = deepmerge([], action.payload.attendanceDatas);
            state.attendanceRewardDataTableVersion = action.payload.attendanceRewardDataVersion;
            state.attendanceRewardDataTable = deepmerge([], action.payload.attendanceRewardDatas);
            state.attributeDataTableVersion = action.payload.attributeDataVersion;
            state.attributeDataTable = deepmerge([], action.payload.attributeDatas);
            state.battleStoreDataTableVersion = action.payload.battleStoreDataVersion;
            state.battleStoreDataTable = deepmerge([], action.payload.battleStoreDatas);
            state.chapterDataTableVersion = action.payload.chapterDataVersion;
            state.chapterDataTable = deepmerge([], action.payload.chapterDatas);
            state.characterDataTableVersion = action.payload.characterDataVersion;
            state.characterDataTable = deepmerge([], action.payload.characterDatas);
            state.collectionDataTableVersion = action.payload.collectionDataVersion;
            state.collectionDataTable = deepmerge([], action.payload.collectionDatas);
            state.collectionGroupDataTableVersion = action.payload.collectionGroupDataVersion;
            state.collectionGroupDataTable = deepmerge([], action.payload.collectionGroupDatas);
            state.colorDataTableVersion = action.payload.colorDataVersion;
            state.colorDataTable = deepmerge([], action.payload.colorDatas);
            state.dataChipStoreListDataTableVersion = action.payload.dataChipStoreListDataVersion;
            state.dataChipStoreListDataTable = deepmerge([], action.payload.dataChipStoreListDatas);
            state.entitlementDataTableVersion = action.payload.entitlementDataVersion;
            state.entitlementDataTable = deepmerge([], action.payload.entitlementDatas);
            state.eventStoreDataTableVersion = action.payload.eventStoreDataVersion;
            state.eventStoreDataTable = deepmerge([], action.payload.eventStoreDatas);
            state.expressionDataTableVersion = action.payload.expressionDataVersion;
            state.expressionDataTable = deepmerge([], action.payload.expressionDatas);
            state.globalDefineDataTableVersion = action.payload.globalDefineDataVersion;
            state.globalDefineDataTable = deepmerge([], action.payload.globalDefineDatas);
            state.guideMissionDataTableVersion = action.payload.guideMissionDataVersion;
            state.guideMissionDataTable = deepmerge([], action.payload.guideMissionDatas);
            state.guideMissionStepRewardTableVersion = action.payload.guideMissionStepRewardVersion;
            state.guideMissionStepRewardTable = deepmerge([], action.payload.guideMissionStepRewards);
            state.glitchStoreDataTableVersion = action.payload.glitchStoreDataVersion;
            state.glitchStoreDataTable = deepmerge([], action.payload.glitchStoreDatas);
            state.goldMedalStoreDataTableVersion = action.payload.goldMedalStoreDataVersion;
            state.goldMedalStoreDataTable = deepmerge([], action.payload.goldMedalStoreDatas);
            state.incubationDataTableVersion = action.payload.incubationDataVersion;
            state.incubationDataTable = deepmerge([], action.payload.incubationDatas);
            state.instantGuideUIDataTableVersion = action.payload.instantGuideUIDataVersion;
            state.instantGuideUIDataTable = deepmerge([], action.payload.instantGuideUIDatas);
            state.itemTableVersion = action.payload.itemVersion;
            state.itemTable = deepmerge([], action.payload.items);
            state.introduceDataTableVersion = action.payload.introduceDataVersion;
            state.introduceDataTable = deepmerge([], action.payload.introduceDatas);
            state.levelUpBuffListDataTableVersion = action.payload.levelUpBuffListDataVersion;
            state.levelUpBuffListDataTable = deepmerge([], action.payload.levelUpBuffListDatas);
            state.missionDataTableVersion = action.payload.missionDataVersion;
            state.missionDataTable = deepmerge([], action.payload.missionDatas);
            state.parameterDataTableVersion = action.payload.parameterDataVersion;
            state.parameterDataTable = deepmerge([], action.payload.parameterDatas);
            state.penaltyDataTableVersion = action.payload.penaltyDataVersion;
            state.penaltyDataTable = deepmerge([], action.payload.penaltyDatas);
            state.petDataTableVersion = action.payload.petDataVersion;
            state.petDataTable = deepmerge([], action.payload.petDatas);
            state.petAbilityListDataTableVersion = action.payload.petAbilityListDataVersion;
            state.petAbilityListDataTable = deepmerge([], action.payload.petAbilityListDatas);
            state.petEggDataTableVersion = action.payload.petEggDataVersion;
            state.petEggDataTable = deepmerge([], action.payload.petEggDatas);
            state.petEggGroupDataTableVersion = action.payload.petEggGroupDataVersion;
            state.petEggGroupDataTable = deepmerge([], action.payload.petEggGroupDatas);
            state.profileDataTableVersion = action.payload.profileDataVersion;
            state.profileDataTable = deepmerge([], action.payload.profileDatas);
            state.questDataTableVersion = action.payload.questDataVersion;
            state.questDataTable = deepmerge([], action.payload.questDatas);
            state.rewardDataTableVersion = action.payload.rewardDataVersion;
            state.rewardDataTable = deepmerge([], action.payload.rewardDatas);
            state.seasonMissionCountDataTableVersion = action.payload.seasonMissionCountDataVersion;
            state.seasonMissionCountDataTable = deepmerge([], action.payload.seasonMissionCountDatas);
            state.seasonMissionListDataTableVersion = action.payload.seasonMissionListDataVersion;
            state.seasonMissionListDataTable = deepmerge([], action.payload.seasonMissionListDatas);
            state.seasonPassDataTableVersion = action.payload.seasonPassDataVersion;
            state.seasonPassDataTable = deepmerge([], action.payload.seasonPassDatas);
            state.seasonPassLevelDataTableVersion = action.payload.seasonPassLevelDataVersion;
            state.seasonPassLevelDataTable = deepmerge([], action.payload.seasonPassLevelDatas);
            state.seasonPassRewardDataTableVersion = action.payload.seasonPassRewardDataVersion;
            state.seasonPassRewardDataTable = deepmerge([], action.payload.seasonPassRewardDatas);
            state.silverMedalStoreDataTableVersion = action.payload.silverMedalStoreDataVersion;
            state.silverMedalStoreDataTable = deepmerge([], action.payload.silverMedalStoreDatas);
            state.skillDataTableVersion = action.payload.skillDataVersion;
            state.skillDataTable = deepmerge([], action.payload.skillDatas);
            state.skinDataTableVersion = action.payload.skinDataVersion;
            state.skinDataTable = deepmerge([], action.payload.skinDatas);
            state.stringDataTableVersion = action.payload.stringDataVersion;
            state.stringDataTable = deepmerge([], action.payload.stringDatas);
            state.userBlockStringDataTableVersion = action.payload.userBlockStringDataVersion;
            state.userBlockStringDataTable = deepmerge([], action.payload.userBlockStringDatas);
            state.treasureBoxDataTableVersion = action.payload.treasureBoxDataVersion;
            state.treasureBoxDataTable = deepmerge([], action.payload.treasureBoxDatas);
            state.vehicleDataTableVersion = action.payload.vehicleDataVersion;
            state.vehicleDataTable = deepmerge([], action.payload.vehicleDatas);
            state.weaponCategoryDataTableVersion = action.payload.weaponCategoryDataVersion;
            state.weaponCategoryDataTable = deepmerge([], action.payload.weaponCategoryDatas);
            state.weaponCategoryUpgradeDataTableVersion = action.payload.weaponCategoryUpgradeDataVersion;
            state.weaponCategoryUpgradeDataTable = deepmerge([], action.payload.weaponCategoryUpgradeDatas);
            state.wonderCubeDataTableVersion = action.payload.wonderCubeDataVersion;
            state.wonderCubeDataTable = deepmerge([], action.payload.wonderCubeDatas);
            state.wonderCubeRwardDataTableVersion = action.payload.wonderCubeRwardDataVersion;
            state.wonderCubeRwardDataTable = deepmerge([], action.payload.wonderCubeRwardDatas);
            state.wonderStoreDataTableVersion = action.payload.wonderStoreDataVersion;
            state.wonderStoreDataTable = deepmerge([], action.payload.wonderStoreDatas);
            state.worldMapDataTableVersion = action.payload.worldMapDataVersion;
            state.worldMapDataTable = deepmerge([], action.payload.worldMapDatas);
            state.biskitLogEventIDTableVersion = action.payload.biskitLogEventIDVersion;
            state.biskitLogEventIDTable = deepmerge({}, action.payload.biskitLogEventIDs);
            state.blockContentDataTableVersion = action.payload.blockContentDataVersion;
            state.blockContentDataTable = deepmerge({}, action.payload.blockContentDatas);
            state.errorsDataTableVersion = action.payload.errorsDataVersion;
            state.errorsDataTable = deepmerge({}, action.payload.errorsDatas);
            state.navMenuDataTableVersion = action.payload.navMenuDataVersion;
            state.navMenuDataTable = deepmerge({}, action.payload.navMenuDatas);
        },
        setAccountLevelTableVersion: (state, action: PayloadAction<string>) => {
            state.accountLevelTableVersion = action.payload;
        },
        setAccountLevelTable: (state, action: PayloadAction<DataTableModels.AccountLevel[]>) => {
            state.accountLevelTable = deepmerge([], action.payload);
        },
        setAchievementDataTableVersion: (state, action: PayloadAction<string>) => {
            state.achievementDataTableVersion = action.payload;
        },
        setAchievementDataTable: (state, action: PayloadAction<DataTableModels.AchievementData[]>) => {
            state.achievementDataTable = deepmerge([], action.payload);
        },
        setAchievementGroupDataTableVersion: (state, action: PayloadAction<string>) => {
            state.achievementGroupDataTableVersion = action.payload;
        },
        setAchievementGroupDataTable: (state, action: PayloadAction<DataTableModels.AchievementGroupData[]>) => {
            state.achievementGroupDataTable = deepmerge([], action.payload);
        },
        setArtifactDataTableVersion: (state, action: PayloadAction<string>) => {
            state.artifactDataTableVersion = action.payload;
        },
        setArtifactDataTable: (state, action: PayloadAction<DataTableModels.ArtifactData[]>) => {
            state.artifactDataTable = deepmerge([], action.payload);
        },
        setAssetDataTableVersion: (state, action: PayloadAction<string>) => {
            state.assetDataTableVersion = action.payload;
        },
        setAssetDataTable: (state, action: PayloadAction<DataTableModels.AssetData[]>) => {
            state.assetDataTable = deepmerge([], action.payload);
        },
        setAttendanceDataTableVersion: (state, action: PayloadAction<string>) => {
            state.attendanceDataTableVersion = action.payload;
        },
        setAttendanceDataTable: (state, action: PayloadAction<DataTableModels.AttendanceData[]>) => {
            state.attendanceDataTable = deepmerge([], action.payload);
        },
        setAttendanceRewardDataTableVersion: (state, action: PayloadAction<string>) => {
            state.attendanceRewardDataTableVersion = action.payload;
        },
        setAttendanceRewardDataTable: (state, action: PayloadAction<DataTableModels.AttendanceRewardData[]>) => {
            state.attendanceRewardDataTable = deepmerge([], action.payload);
        },
        setAttributeDataTableVersion: (state, action: PayloadAction<string>) => {
            state.attributeDataTableVersion = action.payload;
        },
        setAttributeDataTable: (state, action: PayloadAction<DataTableModels.AttributeData[]>) => {
            state.attributeDataTable = deepmerge([], action.payload);
        },
        setBattleStoreDataTableVersion: (state, action: PayloadAction<string>) => {
            state.battleStoreDataTableVersion = action.payload;
        },
        setBattleStoreDataTable: (state, action: PayloadAction<DataTableModels.BattleStoreData[]>) => {
            state.battleStoreDataTable = deepmerge([], action.payload);
        },
        setChapterDataTableVersion: (state, action: PayloadAction<string>) => {
            state.chapterDataTableVersion = action.payload;
        },
        setChapterDataTable: (state, action: PayloadAction<DataTableModels.ChapterData[]>) => {
            state.chapterDataTable = deepmerge([], action.payload);
        },
        setCharacterDataTableVersion: (state, action: PayloadAction<string>) => {
            state.characterDataTableVersion = action.payload;
        },
        setCharacterDataTable: (state, action: PayloadAction<DataTableModels.CharacterData[]>) => {
            state.characterDataTable = deepmerge([], action.payload);
        },
        setCollectionDataTableVersion: (state, action: PayloadAction<string>) => {
            state.collectionDataTableVersion = action.payload;
        },
        setCollectionDataTable: (state, action: PayloadAction<DataTableModels.CollectionData[]>) => {
            state.collectionDataTable = deepmerge([], action.payload);
        },
        setCollectionGroupDataTableVersion: (state, action: PayloadAction<string>) => {
            state.collectionGroupDataTableVersion = action.payload;
        },
        setCollectionGroupDataTable: (state, action: PayloadAction<DataTableModels.CollectionGroupData[]>) => {
            state.collectionGroupDataTable = deepmerge([], action.payload);
        },
        setColorDataTableVersion: (state, action: PayloadAction<string>) => {
            state.colorDataTableVersion = action.payload;
        },
        setColorDataTable: (state, action: PayloadAction<DataTableModels.ColorData[]>) => {
            state.colorDataTable = deepmerge([], action.payload);
        },
        setDataChipStoreListDataTableVersion: (state, action: PayloadAction<string>) => {
            state.dataChipStoreListDataTableVersion = action.payload;
        },
        setDataChipStoreListDataTable: (state, action: PayloadAction<DataTableModels.DataChipStoreListData[]>) => {
            state.dataChipStoreListDataTable = deepmerge([], action.payload);
        },
        setEntitlementDataTableVersion: (state, action: PayloadAction<string>) => {
            state.entitlementDataTableVersion = action.payload;
        },
        setEntitlementDataTable: (state, action: PayloadAction<DataTableModels.EntitlementData[]>) => {
            state.entitlementDataTable = deepmerge([], action.payload);
        },
        setEventStoreDataTableVersion: (state, action: PayloadAction<string>) => {
            state.eventStoreDataTableVersion = action.payload;
        },
        setEventStoreDataTable: (state, action: PayloadAction<DataTableModels.EventStoreData[]>) => {
            state.eventStoreDataTable = deepmerge([], action.payload);
        },
        setExpressionDataTableVersion: (state, action: PayloadAction<string>) => {
            state.expressionDataTableVersion = action.payload;
        },
        setExpressionDataTable: (state, action: PayloadAction<DataTableModels.ExpressionData[]>) => {
            state.expressionDataTable = deepmerge([], action.payload);
        },
        setGlobalDefineDataTableVersion: (state, action: PayloadAction<string>) => {
            state.globalDefineDataTableVersion = action.payload;
        },
        setGlobalDefineDataTable: (state, action: PayloadAction<DataTableModels.GlobalDefineData[]>) => {
            state.globalDefineDataTable = deepmerge([], action.payload);
        },
        setGuideMissionDataTableVersion: (state, action: PayloadAction<string>) => {
            state.guideMissionDataTableVersion = action.payload;
        },
        setGuideMissionDataTable: (state, action: PayloadAction<DataTableModels.GuideMissionData[]>) => {
            state.guideMissionDataTable = deepmerge([], action.payload);
        },
        setGuideMissionStepRewardTableVersion: (state, action: PayloadAction<string>) => {
            state.guideMissionStepRewardTableVersion = action.payload;
        },
        setGuideMissionStepRewardTable: (state, action: PayloadAction<DataTableModels.GuideMissionStepReward[]>) => {
            state.guideMissionStepRewardTable = deepmerge([], action.payload);
        },
        setGlitchStoreDataTableVersion: (state, action: PayloadAction<string>) => {
            state.glitchStoreDataTableVersion = action.payload;
        },
        setGlitchStoreDataTable: (state, action: PayloadAction<DataTableModels.GlitchStoreData[]>) => {
            state.glitchStoreDataTable = deepmerge([], action.payload);
        },
        setGoldMedalStoreDataTableVersion: (state, action: PayloadAction<string>) => {
            state.goldMedalStoreDataTableVersion = action.payload;
        },
        setGoldMedalStoreDataTable: (state, action: PayloadAction<DataTableModels.GoldMedalStoreData[]>) => {
            state.goldMedalStoreDataTable = deepmerge([], action.payload);
        },
        setIncubationDataTableVersion: (state, action: PayloadAction<string>) => {
            state.incubationDataTableVersion = action.payload;
        },
        setIncubationDataTable: (state, action: PayloadAction<DataTableModels.IncubationData[]>) => {
            state.incubationDataTable = deepmerge([], action.payload);
        },
        setInstantGuideUIDataTableVersion: (state, action: PayloadAction<string>) => {
            state.instantGuideUIDataTableVersion = action.payload;
        },
        setInstantGuideUIDataTable: (state, action: PayloadAction<DataTableModels.InstantGuideUIData[]>) => {
            state.instantGuideUIDataTable = deepmerge([], action.payload);
        },
        setItemTableVersion: (state, action: PayloadAction<string>) => {
            state.itemTableVersion = action.payload;
        },
        setItemTable: (state, action: PayloadAction<DataTableModels.Item[]>) => {
            state.itemTable = deepmerge([], action.payload);
        },
        setIntroduceDataTableVersion: (state, action: PayloadAction<string>) => {
            state.introduceDataTableVersion = action.payload;
        },
        setIntroduceDataTable: (state, action: PayloadAction<DataTableModels.IntroduceData[]>) => {
            state.introduceDataTable = deepmerge([], action.payload);
        },
        setLevelUpBuffListDataTableVersion: (state, action: PayloadAction<string>) => {
            state.levelUpBuffListDataTableVersion = action.payload;
        },
        setLevelUpBuffListDataTable: (state, action: PayloadAction<DataTableModels.LevelUpBuffListData[]>) => {
            state.levelUpBuffListDataTable = deepmerge([], action.payload);
        },
        setMissionDataTableVersion: (state, action: PayloadAction<string>) => {
            state.missionDataTableVersion = action.payload;
        },
        setMissionDataTable: (state, action: PayloadAction<DataTableModels.MissionData[]>) => {
            state.missionDataTable = deepmerge([], action.payload);
        },
        setParameterDataTableVersion: (state, action: PayloadAction<string>) => {
            state.parameterDataTableVersion = action.payload;
        },
        setParameterDataTable: (state, action: PayloadAction<DataTableModels.ParameterData[]>) => {
            state.parameterDataTable = deepmerge([], action.payload);
        },
        setPenaltyDataTableVersion: (state, action: PayloadAction<string>) => {
            state.penaltyDataTableVersion = action.payload;
        },
        setPenaltyDataTable: (state, action: PayloadAction<DataTableModels.PenaltyData[]>) => {
            state.penaltyDataTable = deepmerge([], action.payload);
        },
        setPetDataTableVersion: (state, action: PayloadAction<string>) => {
            state.petDataTableVersion = action.payload;
        },
        setPetDataTable: (state, action: PayloadAction<DataTableModels.PetData[]>) => {
            state.petDataTable = deepmerge([], action.payload);
        },
        setPetAbilityListDataTableVersion: (state, action: PayloadAction<string>) => {
            state.petAbilityListDataTableVersion = action.payload;
        },
        setPetAbilityListDataTable: (state, action: PayloadAction<DataTableModels.PetAbilityListData[]>) => {
            state.petAbilityListDataTable = deepmerge([], action.payload);
        },
        setPetEggDataTableVersion: (state, action: PayloadAction<string>) => {
            state.petEggDataTableVersion = action.payload;
        },
        setPetEggDataTable: (state, action: PayloadAction<DataTableModels.PetEggData[]>) => {
            state.petEggDataTable = deepmerge([], action.payload);
        },
        setPetEggGroupDataTableVersion: (state, action: PayloadAction<string>) => {
            state.petEggGroupDataTableVersion = action.payload;
        },
        setPetEggGroupDataTable: (state, action: PayloadAction<DataTableModels.PetEggGroupData[]>) => {
            state.petEggGroupDataTable = deepmerge([], action.payload);
        },
        setProfileDataTableVersion: (state, action: PayloadAction<string>) => {
            state.profileDataTableVersion = action.payload;
        },
        setProfileDataTable: (state, action: PayloadAction<DataTableModels.ProfileData[]>) => {
            state.profileDataTable = deepmerge([], action.payload);
        },
        setQuestDataTableVersion: (state, action: PayloadAction<string>) => {
            state.questDataTableVersion = action.payload;
        },
        setQuestDataTable: (state, action: PayloadAction<DataTableModels.QuestData[]>) => {
            state.questDataTable = deepmerge([], action.payload);
        },
        setRewardDataTableVersion: (state, action: PayloadAction<string>) => {
            state.rewardDataTableVersion = action.payload;
        },
        setRewardDataTable: (state, action: PayloadAction<DataTableModels.RewardData[]>) => {
            state.rewardDataTable = deepmerge([], action.payload);
        },
        setSeasonMissionCountDataTableVersion: (state, action: PayloadAction<string>) => {
            state.seasonMissionCountDataTableVersion = action.payload;
        },
        setSeasonMissionCountDataTable: (state, action: PayloadAction<DataTableModels.SeasonMissionCountData[]>) => {
            state.seasonMissionCountDataTable = deepmerge([], action.payload);
        },
        setSeasonMissionListDataTableVersion: (state, action: PayloadAction<string>) => {
            state.seasonMissionListDataTableVersion = action.payload;
        },
        setSeasonMissionListDataTable: (state, action: PayloadAction<DataTableModels.SeasonMissionListData[]>) => {
            state.seasonMissionListDataTable = deepmerge([], action.payload);
        },
        setSeasonPassDataTableVersion: (state, action: PayloadAction<string>) => {
            state.seasonPassDataTableVersion = action.payload;
        },
        setSeasonPassDataTable: (state, action: PayloadAction<DataTableModels.SeasonPassData[]>) => {
            state.seasonPassDataTable = deepmerge([], action.payload);
        },
        setSeasonPassLevelDataTableVersion: (state, action: PayloadAction<string>) => {
            state.seasonPassLevelDataTableVersion = action.payload;
        },
        setSeasonPassLevelDataTable: (state, action: PayloadAction<DataTableModels.SeasonPassLevelData[]>) => {
            state.seasonPassLevelDataTable = deepmerge([], action.payload);
        },
        setSeasonPassRewardDataTableVersion: (state, action: PayloadAction<string>) => {
            state.seasonPassRewardDataTableVersion = action.payload;
        },
        setSeasonPassRewardDataTable: (state, action: PayloadAction<DataTableModels.SeasonPassRewardData[]>) => {
            state.seasonPassRewardDataTable = deepmerge([], action.payload);
        },
        setSilverMedalStoreDataTableVersion: (state, action: PayloadAction<string>) => {
            state.silverMedalStoreDataTableVersion = action.payload;
        },
        setSilverMedalStoreDataTable: (state, action: PayloadAction<DataTableModels.SilverMedalStoreData[]>) => {
            state.silverMedalStoreDataTable = deepmerge([], action.payload);
        },
        setSkillDataTableVersion: (state, action: PayloadAction<string>) => {
            state.skillDataTableVersion = action.payload;
        },
        setSkillDataTable: (state, action: PayloadAction<DataTableModels.SkillData[]>) => {
            state.skillDataTable = deepmerge([], action.payload);
        },
        setSkinDataTableVersion: (state, action: PayloadAction<string>) => {
            state.skinDataTableVersion = action.payload;
        },
        setSkinDataTable: (state, action: PayloadAction<DataTableModels.SkinData[]>) => {
            state.skinDataTable = deepmerge([], action.payload);
        },
        setStringDataTableVersion: (state, action: PayloadAction<string>) => {
            state.stringDataTableVersion = action.payload;
        },
        setStringDataTable: (state, action: PayloadAction<DataTableModels.StringData[]>) => {
            state.stringDataTable = deepmerge([], action.payload);
        },
        setUserBlockStringDataTableVersion: (state, action: PayloadAction<string>) => {
            state.userBlockStringDataTableVersion = action.payload;
        },
        setUserBlockStringDataTable: (state, action: PayloadAction<DataTableModels.UserBlockStringData[]>) => {
            state.userBlockStringDataTable = deepmerge([], action.payload);
        },
        setTreasureBoxDataTableVersion: (state, action: PayloadAction<string>) => {
            state.treasureBoxDataTableVersion = action.payload;
        },
        setTreasureBoxDataTable: (state, action: PayloadAction<DataTableModels.TreasureBoxData[]>) => {
            state.treasureBoxDataTable = deepmerge([], action.payload);
        },
        setVehicleDataTableVersion: (state, action: PayloadAction<string>) => {
            state.vehicleDataTableVersion = action.payload;
        },
        setVehicleDataTable: (state, action: PayloadAction<DataTableModels.VehicleData[]>) => {
            state.vehicleDataTable = deepmerge([], action.payload);
        },
        setWeaponCategoryDataTableVersion: (state, action: PayloadAction<string>) => {
            state.weaponCategoryDataTableVersion = action.payload;
        },
        setWeaponCategoryDataTable: (state, action: PayloadAction<DataTableModels.WeaponCategoryData[]>) => {
            state.weaponCategoryDataTable = deepmerge([], action.payload);
        },
        setWeaponCategoryUpgradeDataTableVersion: (state, action: PayloadAction<string>) => {
            state.weaponCategoryUpgradeDataTableVersion = action.payload;
        },
        setWeaponCategoryUpgradeDataTable: (state, action: PayloadAction<DataTableModels.WeaponCategoryUpgradeData[]>) => {
            state.weaponCategoryUpgradeDataTable = deepmerge([], action.payload);
        },
        setWonderCubeDataTableVersion: (state, action: PayloadAction<string>) => {
            state.wonderCubeDataTableVersion = action.payload;
        },
        setWonderCubeDataTable: (state, action: PayloadAction<DataTableModels.WonderCubeData[]>) => {
            state.wonderCubeDataTable = deepmerge([], action.payload);
        },
        setWonderCubeRwardDataTableVersion: (state, action: PayloadAction<string>) => {
            state.wonderCubeRwardDataTableVersion = action.payload;
        },
        setWonderCubeRwardDataTable: (state, action: PayloadAction<DataTableModels.WonderCubeRwardData[]>) => {
            state.wonderCubeRwardDataTable = deepmerge([], action.payload);
        },
        setWonderStoreDataTableVersion: (state, action: PayloadAction<string>) => {
            state.wonderStoreDataTableVersion = action.payload;
        },
        setWonderStoreDataTable: (state, action: PayloadAction<DataTableModels.WonderStoreData[]>) => {
            state.wonderStoreDataTable = deepmerge([], action.payload);
        },
        setWorldMapDataTableVersion: (state, action: PayloadAction<string>) => {
            state.worldMapDataTableVersion = action.payload;
        },
        setWorldMapDataTable: (state, action: PayloadAction<DataTableModels.WorldMapData[]>) => {
            state.worldMapDataTable = deepmerge([], action.payload);
        },
        setBiskitLogEventIDTableVersion: (state, action: PayloadAction<string>) => {
            state.biskitLogEventIDTableVersion = action.payload;
        },
        setBiskitLogEventIDTable: (state, action: PayloadAction<Map<string, DataTableModels.BiskitLogEventID>>) => {
            state.biskitLogEventIDTable = deepmerge({}, action.payload);
        },
        setBlockContentDataTableVersion: (state, action: PayloadAction<string>) => {
            state.blockContentDataTableVersion = action.payload;
        },
        setBlockContentDataTable: (state, action: PayloadAction<Map<string, DataTableModels.BlockContentData>>) => {
            state.blockContentDataTable = deepmerge({}, action.payload);
        },
        setErrorsDataTableVersion: (state, action: PayloadAction<string>) => {
            state.errorsDataTableVersion = action.payload;
        },
        setErrorsDataTable: (state, action: PayloadAction<Map<Errors, DataTableModels.ErrorsData>>) => {
            state.errorsDataTable = deepmerge({}, action.payload);
        },
        setNavMenuDataTableVersion: (state, action: PayloadAction<string>) => {
            state.navMenuDataTableVersion = action.payload;
        },
        setNavMenuDataTable: (state, action: PayloadAction<Map<string, DataTableModels.NavMenuData>>) => {
            state.navMenuDataTable = deepmerge({}, action.payload);
        },
    }
});

export type { TableState };
export const {
    setTables,
    setAccountLevelTableVersion,
    setAccountLevelTable,
    setAchievementDataTableVersion,
    setAchievementDataTable,
    setAchievementGroupDataTableVersion,
    setAchievementGroupDataTable,
    setArtifactDataTableVersion,
    setArtifactDataTable,
    setAssetDataTableVersion,
    setAssetDataTable,
    setAttendanceDataTableVersion,
    setAttendanceDataTable,
    setAttendanceRewardDataTableVersion,
    setAttendanceRewardDataTable,
    setAttributeDataTableVersion,
    setAttributeDataTable,
    setBattleStoreDataTableVersion,
    setBattleStoreDataTable,
    setChapterDataTableVersion,
    setChapterDataTable,
    setCharacterDataTableVersion,
    setCharacterDataTable,
    setCollectionDataTableVersion,
    setCollectionDataTable,
    setCollectionGroupDataTableVersion,
    setCollectionGroupDataTable,
    setColorDataTableVersion,
    setColorDataTable,
    setDataChipStoreListDataTableVersion,
    setDataChipStoreListDataTable,
    setEntitlementDataTableVersion,
    setEntitlementDataTable,
    setEventStoreDataTableVersion,
    setEventStoreDataTable,
    setExpressionDataTableVersion,
    setExpressionDataTable,
    setGlobalDefineDataTableVersion,
    setGlobalDefineDataTable,
    setGuideMissionDataTableVersion,
    setGuideMissionDataTable,
    setGuideMissionStepRewardTableVersion,
    setGuideMissionStepRewardTable,
    setGlitchStoreDataTableVersion,
    setGlitchStoreDataTable,
    setGoldMedalStoreDataTableVersion,
    setGoldMedalStoreDataTable,
    setIncubationDataTableVersion,
    setIncubationDataTable,
    setInstantGuideUIDataTableVersion,
    setInstantGuideUIDataTable,
    setItemTableVersion,
    setItemTable,
    setIntroduceDataTableVersion,
    setIntroduceDataTable,
    setLevelUpBuffListDataTableVersion,
    setLevelUpBuffListDataTable,
    setMissionDataTableVersion,
    setMissionDataTable,
    setParameterDataTableVersion,
    setParameterDataTable,
    setPenaltyDataTableVersion,
    setPenaltyDataTable,
    setPetDataTableVersion,
    setPetDataTable,
    setPetAbilityListDataTableVersion,
    setPetAbilityListDataTable,
    setPetEggDataTableVersion,
    setPetEggDataTable,
    setPetEggGroupDataTableVersion,
    setPetEggGroupDataTable,
    setProfileDataTableVersion,
    setProfileDataTable,
    setQuestDataTableVersion,
    setQuestDataTable,
    setRewardDataTableVersion,
    setRewardDataTable,
    setSeasonMissionCountDataTableVersion,
    setSeasonMissionCountDataTable,
    setSeasonMissionListDataTableVersion,
    setSeasonMissionListDataTable,
    setSeasonPassDataTableVersion,
    setSeasonPassDataTable,
    setSeasonPassLevelDataTableVersion,
    setSeasonPassLevelDataTable,
    setSeasonPassRewardDataTableVersion,
    setSeasonPassRewardDataTable,
    setSilverMedalStoreDataTableVersion,
    setSilverMedalStoreDataTable,
    setSkillDataTableVersion,
    setSkillDataTable,
    setSkinDataTableVersion,
    setSkinDataTable,
    setStringDataTableVersion,
    setStringDataTable,
    setUserBlockStringDataTableVersion,
    setUserBlockStringDataTable,
    setTreasureBoxDataTableVersion,
    setTreasureBoxDataTable,
    setVehicleDataTableVersion,
    setVehicleDataTable,
    setWeaponCategoryDataTableVersion,
    setWeaponCategoryDataTable,
    setWeaponCategoryUpgradeDataTableVersion,
    setWeaponCategoryUpgradeDataTable,
    setWonderCubeDataTableVersion,
    setWonderCubeDataTable,
    setWonderCubeRwardDataTableVersion,
    setWonderCubeRwardDataTable,
    setWonderStoreDataTableVersion,
    setWonderStoreDataTable,
    setWorldMapDataTableVersion,
    setWorldMapDataTable,
    setBiskitLogEventIDTableVersion,
    setBiskitLogEventIDTable,
    setBlockContentDataTableVersion,
    setBlockContentDataTable,
    setErrorsDataTableVersion,
    setErrorsDataTable,
    setNavMenuDataTableVersion,
    setNavMenuDataTable,
} = tableSlice.actions;
export default tableSlice.reducer;
