import {
    EUserAssetType,
    EContentsType,
    EHeroType,
    ENpcType,
    ECollectionType,
    ERewardType,
    EItemType,
    ELimitTime,
    EExpressionType,
    EExpressionCategory,
    EGuideMissionCategory,
    EGuideMissionType,
    EItemGradeType,
    EMissionGroup,
    EPenaltyReportState,
    EPenaltyType,
    EProfileType,
    EMissionType,
    EWeaponCategory,
    EStoreGoodsType,
} from '../models/lobby';
import { Errors } from '../autoErrors';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';

export namespace DataTableModels {

    export class AccountLevel {
        constructor(data?: AccountLevel) {
            if (data) {
                this.ID = data.ID;
                this.Experience = data.Experience;
            }
        }

        ID: number = 0;
        Experience: number = 0;
    }

    export class AchievementData {
        constructor(data?: AchievementData) {
            if (data) {
                this.ID = data.ID;
                this.AchievementGroupID = data.AchievementGroupID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.AchievementType = data.AchievementType;
                this.Enable = data.Enable;
                this.AchievementCount = data.AchievementCount;
                this.RewardAssetType = data.RewardAssetType;
                this.RewardAssetCount = data.RewardAssetCount;
            }
        }

        ID: number = 0;
        AchievementGroupID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        AchievementType: string = '';
        Enable: boolean = false;
        AchievementCount: number = 0;
        RewardAssetType: EUserAssetType = 0;
        RewardAssetCount: number = 0;
    }

    export class AchievementGroupData {
        constructor(data?: AchievementGroupData) {
            if (data) {
                this.ID = data.ID;
                this.GameMode = data.GameMode;
                this.ChapterID = data.ChapterID;
                this.CategoryName = data.CategoryName;
                this.CategoryNameString = data.CategoryNameString;
                this.CategoryNameStringWithID = data.CategoryNameStringWithID;
                this.Enable = data.Enable;
                this.EntitlementID = data.EntitlementID;
            }
        }

        ID: number = 0;
        GameMode: EContentsType = 0;
        ChapterID: number = 0;
        CategoryName: string = '';
        CategoryNameString: string = '';
        CategoryNameStringWithID: string = '';
        Enable: boolean = false;
        EntitlementID: number = 0;
    }

    export class ArtifactData {
        constructor(data?: ArtifactData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.Default = data.Default;
                this.ArtifactSet = data.ArtifactSet;
                this.Group = data.Group;
                this.Grade = data.Grade;
                this.MaxEnhance = data.MaxEnhance;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        Default: boolean = false;
        ArtifactSet: string = '';
        Group: number = 0;
        Grade: number = 0;
        MaxEnhance: number = 0;
    }

    export class AssetData {
        constructor(data?: AssetData) {
            if (data) {
                this.ID = data.ID;
                this.ItemID = data.ItemID;
                this.MaxValue = data.MaxValue;
                this.AssetString = data.AssetString;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
            }
        }

        ID: EUserAssetType = 0;
        ItemID: number = 0;
        MaxValue: number = 0;
        AssetString: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
    }

    export class AttendanceData {
        constructor(data?: AttendanceData) {
            if (data) {
                this.ID = data.ID;
                this.Description = data.Description;
                this.DescriptionString = data.DescriptionString;
                this.DescriptionStringWithID = data.DescriptionStringWithID;
                this.RewardTotalCount = data.RewardTotalCount;
                this.RewardGroup = data.RewardGroup;
            }
        }

        ID: number = 0;
        Description: string = '';
        DescriptionString: string = '';
        DescriptionStringWithID: string = '';
        RewardTotalCount: number = 0;
        RewardGroup: number = 0;
    }

    export class AttendanceRewardData {
        constructor(data?: AttendanceRewardData) {
            if (data) {
                this.ID = data.ID;
                this.RewardGroup = data.RewardGroup;
                this.GroupIndex = data.GroupIndex;
                this.AssetType = data.AssetType;
                this.AssetCount = data.AssetCount;
                this.ItemID = data.ItemID;
                this.ItemCount = data.ItemCount;
                this.HeroID = data.HeroID;
            }
        }

        ID: number = 0;
        RewardGroup: number = 0;
        GroupIndex: number = 0;
        AssetType: EUserAssetType = 0;
        AssetCount: number = 0;
        ItemID: number = 0;
        ItemCount: number = 0;
        HeroID: number = 0;
    }

    export class AttributeData {
        constructor(data?: AttributeData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.MaxLevel = data.MaxLevel;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        MaxLevel: number = 0;
    }

    export class BattleStoreData {
        constructor(data?: BattleStoreData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.MainType = data.MainType;
                this.SubType = data.SubType;
                this.SellItem = data.SellItem;
                this.SellAsset = data.SellAsset;
                this.Count = data.Count;
                this.CostAssetType = data.CostAssetType;
                this.CostCount = data.CostCount;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        MainType: string = '';
        SubType: string = '';
        SellItem: number = 0;
        SellAsset: string = '';
        Count: number = 0;
        CostAssetType: string = '';
        CostCount: number = 0;
    }

    export class ChapterData {
        constructor(data?: ChapterData) {
            if (data) {
                this.ID = data.ID;
                this.Chapter = data.Chapter;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.CharacterUnlock = data.CharacterUnlock;
                this.Rewards = data.Rewards;
                this.StageID = data.StageID;
                this.StageGroup = data.StageGroup;
                this.NextChapterID = data.NextChapterID;
            }
        }

        ID: number = 0;
        Chapter: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        CharacterUnlock: number[] = [];
        Rewards: number[] = [];
        StageID: number[] = [];
        StageGroup: number = 0;
        NextChapterID: number = 0;
    }

    export class CharacterData {
        constructor(data?: CharacterData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.HeroType = data.HeroType;
                this.NPCType = data.NPCType;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        HeroType: EHeroType = 0;
        NPCType: ENpcType = ENpcType.Normal;
    }

    export class CollectionData {
        constructor(data?: CollectionData) {
            if (data) {
                this.ID = data.ID;
                this.Type = data.Type;
                this.Enable = data.Enable;
                this.Group = data.Group;
                this.GroupName = data.GroupName;
                this.GroupNameString = data.GroupNameString;
                this.GroupNameStringWithID = data.GroupNameStringWithID;
                this.ReqID = data.ReqID;
            }
        }

        ID: number = 0;
        Type: ECollectionType = 0;
        Enable: boolean = false;
        Group: number = 0;
        GroupName: string = '';
        GroupNameString: string = '';
        GroupNameStringWithID: string = '';
        ReqID: number = 0;
    }

    export class CollectionGroupData {
        constructor(data?: CollectionGroupData) {
            if (data) {
                this.ID = data.ID;
                this.UIIdentifier = data.UIIdentifier;
                this.CategoryName = data.CategoryName;
                this.CategoryNameString = data.CategoryNameString;
                this.CategoryNameStringWithID = data.CategoryNameStringWithID;
                this.Enable = data.Enable;
            }
        }

        ID: number = 0;
        UIIdentifier: string = '';
        CategoryName: string = '';
        CategoryNameString: string = '';
        CategoryNameStringWithID: string = '';
        Enable: boolean = false;
    }

    export class ColorData {
        constructor(data?: ColorData) {
            if (data) {
                this.ID = data.ID;
                this.Color = data.Color;
            }
        }

        ID: number = 0;
        Color: string = '';
    }

    export class DataChipStoreListData {
        constructor(data?: DataChipStoreListData) {
            if (data) {
                this.ID = data.ID;
                this.ProductItem = data.ProductItem;
                this.ProductType = data.ProductType;
                this.Enable = data.Enable;
                this.BuyCostDataChip = data.BuyCostDataChip;
            }
        }

        ID: number = 0;
        ProductItem: number = 0;
        ProductType: string = '';
        Enable: boolean = false;
        BuyCostDataChip: number = 0;
    }

    export class EntitlementData {
        constructor(data?: EntitlementData) {
            if (data) {
                this.ID = data.ID;
                this.EntitlementName = data.EntitlementName;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.Enable = data.Enable;
            }
        }

        ID: string = '';
        EntitlementName: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        Enable: boolean = false;
    }

    export class EventStoreData {
        constructor(data?: EventStoreData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.RewardType = data.RewardType;
                this.ItemType = data.ItemType;
                this.ItemID = data.ItemID;
                this.ItemCount = data.ItemCount;
                this.ReqAssetID = data.ReqAssetID;
                this.ReqAssetCount = data.ReqAssetCount;
                this.LimitTime = data.LimitTime;
                this.LimitCount = data.LimitCount;
                this.StartDate = data.StartDate;
                this.EndDate = data.EndDate;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        RewardType: ERewardType = 0;
        ItemType: EItemType = 0;
        ItemID: number = 0;
        ItemCount: number = 0;
        ReqAssetID: EUserAssetType = 0;
        ReqAssetCount: number = 0;
        LimitTime: ELimitTime = 0;
        LimitCount: number = 0;
        StartDate: Date = new Date();
        EndDate: Date = new Date();
    }

    export class ExpressionData {
        constructor(data?: ExpressionData) {
            if (data) {
                this.ID = data.ID;
                this.Character = data.Character;
                this.Enable = data.Enable;
                this.ExpressionType = data.ExpressionType;
                this.ExpressionCategory = data.ExpressionCategory;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
            }
        }

        ID: number = 0;
        Character: EHeroType = 0;
        Enable: boolean = false;
        ExpressionType: EExpressionType = 0;
        ExpressionCategory: EExpressionCategory = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
    }

    export class GlobalDefineData {
        constructor(data?: GlobalDefineData) {
            if (data) {
                this.ID = data.ID;
                this.Value = data.Value;
            }
        }

        ID: string = '';
        Value: string[] = [];
    }

    export class GuideMissionData {
        constructor(data?: GuideMissionData) {
            if (data) {
                this.ID = data.ID;
                this.GuideMissionCategory = data.GuideMissionCategory;
                this.MissionType = data.MissionType;
                this.Step = data.Step;
                this.NextStepMissionID = data.NextStepMissionID;
                this.Title = data.Title;
                this.TitleString = data.TitleString;
                this.TitleStringWithID = data.TitleStringWithID;
                this.TrackingID = data.TrackingID;
                this.RewardType = data.RewardType;
                this.RewardID = data.RewardID;
                this.RewardCount = data.RewardCount;
            }
        }

        ID: number = 0;
        GuideMissionCategory: EGuideMissionCategory = 0;
        MissionType: EGuideMissionType = 0;
        Step: number = 0;
        NextStepMissionID: number = 0;
        Title: string = '';
        TitleString: string = '';
        TitleStringWithID: string = '';
        TrackingID: number = 0;
        RewardType: ERewardType = 0;
        RewardID: string = '';
        RewardCount: number = 0;
    }

    export class GuideMissionStepReward {
        constructor(data?: GuideMissionStepReward) {
            if (data) {
                this.ID = data.ID;
                this.PreviousReward = data.PreviousReward;
                this.ReqCompleteMissionCount = data.ReqCompleteMissionCount;
                this.RewardType = data.RewardType;
                this.RewardID = data.RewardID;
                this.RewardCount = data.RewardCount;
                this.GradeType = data.GradeType;
            }
        }

        ID: number = 0;
        PreviousReward: number = 0;
        ReqCompleteMissionCount: number = 0;
        RewardType: ERewardType = 0;
        RewardID: string = '';
        RewardCount: number = 0;
        GradeType: string = '';
    }

    export class GlitchStoreData {
        constructor(data?: GlitchStoreData) {
            if (data) {
                this.ID = data.ID;
                this.ProductType = data.ProductType;
                this.ProductItem = data.ProductItem;
                this.AssetType = data.AssetType;
                this.AssetPrice = data.AssetPrice;
                this.Chapter = data.Chapter;
            }
        }

        ID: number = 0;
        ProductType: string = '';
        ProductItem: number = 0;
        AssetType: string = '';
        AssetPrice: number = 0;
        Chapter: number = 0;
    }

    export class GoldMedalStoreData {
        constructor(data?: GoldMedalStoreData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.RewardType = data.RewardType;
                this.ItemType = data.ItemType;
                this.ItemID = data.ItemID;
                this.ItemCount = data.ItemCount;
                this.ReqAssetID = data.ReqAssetID;
                this.ReqAssetCount = data.ReqAssetCount;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        RewardType: ERewardType = 0;
        ItemType: EItemType = 0;
        ItemID: number = 0;
        ItemCount: number = 0;
        ReqAssetID: EUserAssetType = 0;
        ReqAssetCount: number = 0;
    }

    export class IncubationData {
        constructor(data?: IncubationData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
    }

    export class InstantGuideUIData {
        constructor(data?: InstantGuideUIData) {
            if (data) {
                this.ID = data.ID;
                this.GuideCategory = data.GuideCategory;
                this.GuideCategoryName = data.GuideCategoryName;
                this.GuideCategoryNameString = data.GuideCategoryNameString;
                this.GuideCategoryNameStringWithID = data.GuideCategoryNameStringWithID;
                this.Title = data.Title;
                this.TitleString = data.TitleString;
                this.TitleStringWithID = data.TitleStringWithID;
            }
        }

        ID: number = 0;
        GuideCategory: number = 0;
        GuideCategoryName: string = '';
        GuideCategoryNameString: string = '';
        GuideCategoryNameStringWithID: string = '';
        Title: string = '';
        TitleString: string = '';
        TitleStringWithID: string = '';
    }

    export class Item {
        constructor(data?: Item) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.ItemType = data.ItemType;
                this.WeaponType = data.WeaponType;
                this.Grade = data.Grade;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        ItemType: EItemType = 0;
        WeaponType: string = '';
        Grade: EItemGradeType = 0;
    }

    export class IntroduceData {
        constructor(data?: IntroduceData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.IntroduceCategory = data.IntroduceCategory;
                this.AchievementID = data.AchievementID;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        IntroduceCategory: string = '';
        AchievementID: number = 0;
    }

    export class LevelUpBuffListData {
        constructor(data?: LevelUpBuffListData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.Stack = data.Stack;
                this.ModeType = data.ModeType;
                this.HeroType = data.HeroType;
                this.GroupIndex = data.GroupIndex;
                this.KeyBuff = data.KeyBuff;
                this.SlotIndex = data.SlotIndex;
                this.HeroLevel = data.HeroLevel;
                this.FavorityLevel = data.FavorityLevel;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        Stack: number = 0;
        ModeType: string = '';
        HeroType: EHeroType = 0;
        GroupIndex: number = 0;
        KeyBuff: boolean = false;
        SlotIndex: number = 0;
        HeroLevel: number = 0;
        FavorityLevel: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
    }

    export class MissionData {
        constructor(data?: MissionData) {
            if (data) {
                this.ID = data.ID;
                this.MissionGroup = data.MissionGroup;
                this.EXPGroup = data.EXPGroup;
                this.CreateCount = data.CreateCount;
                this.UseDate = data.UseDate;
                this.StartDate = data.StartDate;
                this.EndDate = data.EndDate;
            }
        }

        ID: number = 0;
        MissionGroup: EMissionGroup = 0;
        EXPGroup: number = 0;
        CreateCount: number = 0;
        UseDate: boolean = false;
        StartDate: Date = new Date();
        EndDate: Date = new Date();
    }

    export class ParameterData {
        constructor(data?: ParameterData) {
            if (data) {
                this.ParameterName = data.ParameterName;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithParameter = data.NameStringWithParameter;
                this.SortIndex = data.SortIndex;
                this.CalcType = data.CalcType;
                this.CalcValue = data.CalcValue;
                this.DigitCount = data.DigitCount;
                this.AddString = data.AddString;
            }
        }

        ParameterName: string = '';
        Name: string = '';
        NameString: string = '';
        NameStringWithParameter: string = '';
        SortIndex: number = 0;
        CalcType: string = '';
        CalcValue: number = 0;
        DigitCount: number = 0;
        AddString: number = 0;
    }

    export class PenaltyData {
        constructor(data?: PenaltyData) {
            if (data) {
                this.ID = data.ID;
                this.ReportState = data.ReportState;
                this.PenaltyGrade = data.PenaltyGrade;
                this.ReqPenaltyPoint = data.ReqPenaltyPoint;
                this.PenaltyType = data.PenaltyType;
                this.PenaltyTime = data.PenaltyTime;
                this.PenaltyCount = data.PenaltyCount;
                this.ClearPenaltyTime = data.ClearPenaltyTime;
                this.ClearPenaltyGrade = data.ClearPenaltyGrade;
                this.ReportStateText = data.ReportStateText;
                this.ReportStateTextString = data.ReportStateTextString;
                this.ReportStateTextStringWithID = data.ReportStateTextStringWithID;
                this.PenaltyTypeText = data.PenaltyTypeText;
                this.PenaltyTypeTextString = data.PenaltyTypeTextString;
                this.PenaltyTypeTextStringWithID = data.PenaltyTypeTextStringWithID;
            }
        }

        ID: number = 0;
        ReportState: EPenaltyReportState = 0;
        PenaltyGrade: number = 0;
        ReqPenaltyPoint: number = 0;
        PenaltyType: EPenaltyType = 0;
        PenaltyTime: number = 0;
        PenaltyCount: number = 0;
        ClearPenaltyTime: number = 0;
        ClearPenaltyGrade: number = 0;
        ReportStateText: string = '';
        ReportStateTextString: string = '';
        ReportStateTextStringWithID: string = '';
        PenaltyTypeText: string = '';
        PenaltyTypeTextString: string = '';
        PenaltyTypeTextStringWithID: string = '';
    }

    export class PetData {
        constructor(data?: PetData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.Enable = data.Enable;
                this.Grade = data.Grade;
                this.ActiveSkill = data.ActiveSkill;
                this.PassiveSkill = data.PassiveSkill;
                this.LikeSection = data.LikeSection;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        Enable: boolean = false;
        Grade: EItemGradeType = 0;
        ActiveSkill: number = 0;
        PassiveSkill: number[] = [];
        LikeSection: number[] = [];
    }

    export class PetAbilityListData {
        constructor(data?: PetAbilityListData) {
            if (data) {
                this.ID = data.ID;
                this.AbilityType = data.AbilityType;
                this.AbilityGroup = data.AbilityGroup;
                this.AbilityValue = data.AbilityValue;
                this.AbilityRate = data.AbilityRate;
            }
        }

        ID: number = 0;
        AbilityType: string = '';
        AbilityGroup: number = 0;
        AbilityValue: number[] = [];
        AbilityRate: number[] = [];
    }

    export class PetEggData {
        constructor(data?: PetEggData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.PetEggGroupID = data.PetEggGroupID;
                this.ConsumeAssetType = data.ConsumeAssetType;
                this.ConsumeAssetCount = data.ConsumeAssetCount;
                this.ReduceIncubateTime = data.ReduceIncubateTime;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        PetEggGroupID: number = 0;
        ConsumeAssetType: EUserAssetType[] = [];
        ConsumeAssetCount: number[] = [];
        ReduceIncubateTime: number = 0;
    }

    export class PetEggGroupData {
        constructor(data?: PetEggGroupData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.PetEggGroup = data.PetEggGroup;
                this.PetID = data.PetID;
                this.RewardProb = data.RewardProb;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        PetEggGroup: number = 0;
        PetID: number = 0;
        RewardProb: number = 0;
    }

    export class ProfileData {
        constructor(data?: ProfileData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.Type = data.Type;
                this.Enable = data.Enable;
                this.IsSquare = data.IsSquare;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        Type: EProfileType = 0;
        Enable: boolean = false;
        IsSquare: boolean = false;
    }

    export class QuestData {
        constructor(data?: QuestData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.QuestType = data.QuestType;
                this.MissionCount = data.MissionCount;
                this.MissionIDList = data.MissionIDList;
                this.CompleteRewardItemID = data.CompleteRewardItemID;
                this.CompleteRewardItemCount = data.CompleteRewardItemCount;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        QuestType: string = '';
        MissionCount: number = 0;
        MissionIDList: string = '';
        CompleteRewardItemID: number = 0;
        CompleteRewardItemCount: number = 0;
    }

    export class RewardData {
        constructor(data?: RewardData) {
            if (data) {
                this.ID = data.ID;
                this.RewardType = data.RewardType;
                this.RewardCount = data.RewardCount;
                this.CharacterID = data.CharacterID;
                this.CostumeID1 = data.CostumeID1;
                this.CostumeID2 = data.CostumeID2;
                this.ItemID1 = data.ItemID1;
                this.ItemCount1 = data.ItemCount1;
                this.ItemID2 = data.ItemID2;
                this.ItemCount2 = data.ItemCount2;
                this.ItemID3 = data.ItemID3;
                this.ItemCount3 = data.ItemCount3;
                this.AssetID1 = data.AssetID1;
                this.AssetCount1 = data.AssetCount1;
                this.AssetID2 = data.AssetID2;
                this.AssetCount2 = data.AssetCount2;
                this.AssetID3 = data.AssetID3;
                this.AssetCount3 = data.AssetCount3;
            }
        }

        ID: number = 0;
        RewardType: string = '';
        RewardCount: number = 0;
        CharacterID: number = 0;
        CostumeID1: number = 0;
        CostumeID2: number = 0;
        ItemID1: number = 0;
        ItemCount1: number = 0;
        ItemID2: number = 0;
        ItemCount2: number = 0;
        ItemID3: number = 0;
        ItemCount3: number = 0;
        AssetID1: EUserAssetType = 0;
        AssetCount1: number = 0;
        AssetID2: EUserAssetType = 0;
        AssetCount2: number = 0;
        AssetID3: EUserAssetType = 0;
        AssetCount3: number = 0;
    }

    export class SeasonMissionCountData {
        constructor(data?: SeasonMissionCountData) {
            if (data) {
                this.ID = data.ID;
                this.OpenWorldCount = data.OpenWorldCount;
                this.GoldClashCount = data.GoldClashCount;
                this.RPGCount = data.RPGCount;
            }
        }

        ID: number = 0;
        OpenWorldCount: number = 0;
        GoldClashCount: number = 0;
        RPGCount: number = 0;
    }

    export class SeasonMissionListData {
        constructor(data?: SeasonMissionListData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.MissionGroup = data.MissionGroup;
                this.IsTargetName = data.IsTargetName;
                this.ReqHeroID = data.ReqHeroID;
                this.CharacterName = data.CharacterName;
                this.CharacterNameString = data.CharacterNameString;
                this.CharacterNameStringWithID = data.CharacterNameStringWithID;
                this.MissionType = data.MissionType;
                this.MissionValue = data.MissionValue;
                this.MissionCount = data.MissionCount;
                this.Description = data.Description;
                this.DescriptionString = data.DescriptionString;
                this.DescriptionStringWithID = data.DescriptionStringWithID;
                this.RewardExp = data.RewardExp;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        MissionGroup: EMissionGroup = 0;
        IsTargetName: boolean = false;
        ReqHeroID: number = 0;
        CharacterName: string = '';
        CharacterNameString: string = '';
        CharacterNameStringWithID: string = '';
        MissionType: EMissionType = 0;
        MissionValue: number = 0;
        MissionCount: number = 0;
        Description: string = '';
        DescriptionString: string = '';
        DescriptionStringWithID: string = '';
        RewardExp: number = 0;
    }

    export class SeasonPassData {
        constructor(data?: SeasonPassData) {
            if (data) {
                this.ID = data.ID;
                this.SeasonNum = data.SeasonNum;
                this.ExtraStartLevel = data.ExtraStartLevel;
                this.FreeMaxLevel = data.FreeMaxLevel;
                this.PaidMaxLevel = data.PaidMaxLevel;
                this.IsActive = data.IsActive;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.StartDate = data.StartDate;
                this.EndDate = data.EndDate;
            }
        }

        ID: number = 0;
        SeasonNum: number = 0;
        ExtraStartLevel: number = 0;
        FreeMaxLevel: number = 0;
        PaidMaxLevel: number = 0;
        IsActive: boolean = false;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        StartDate: Date = new Date();
        EndDate: Date = new Date();
    }

    export class SeasonPassLevelData {
        constructor(data?: SeasonPassLevelData) {
            if (data) {
                this.ID = data.ID;
                this.SeasonPassLevel = data.SeasonPassLevel;
                this.SeasonPassID = data.SeasonPassID;
                this.ReqExp = data.ReqExp;
            }
        }

        ID: number = 0;
        SeasonPassLevel: number = 0;
        SeasonPassID: number = 0;
        ReqExp: number = 0;
    }

    export class SeasonPassRewardData {
        constructor(data?: SeasonPassRewardData) {
            if (data) {
                this.ID = data.ID;
                this.SeasonPassID = data.SeasonPassID;
                this.SeasonPassLevel = data.SeasonPassLevel;
                this.GroupIndex = data.GroupIndex;
                this.IsPaid = data.IsPaid;
                this.RewardTableID = data.RewardTableID;
                this.IsMainReward = data.IsMainReward;
            }
        }

        ID: number = 0;
        SeasonPassID: number = 0;
        SeasonPassLevel: number = 0;
        GroupIndex: number = 0;
        IsPaid: boolean = false;
        RewardTableID: number = 0;
        IsMainReward: boolean = false;
    }

    export class SilverMedalStoreData {
        constructor(data?: SilverMedalStoreData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.RewardType = data.RewardType;
                this.ItemType = data.ItemType;
                this.ItemID = data.ItemID;
                this.ItemCount = data.ItemCount;
                this.ReqAssetID = data.ReqAssetID;
                this.ReqAssetCount = data.ReqAssetCount;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        RewardType: ERewardType = 0;
        ItemType: EItemType = 0;
        ItemID: number = 0;
        ItemCount: number = 0;
        ReqAssetID: EUserAssetType = 0;
        ReqAssetCount: number = 0;
    }

    export class SkillData {
        constructor(data?: SkillData) {
            if (data) {
                this.SkillID = data.SkillID;
                this.DesignName = data.DesignName;
                this.SkillType = data.SkillType;
                this.IsPetSkill = data.IsPetSkill;
            }
        }

        SkillID: number = 0;
        DesignName: string = '';
        SkillType: string = '';
        IsPetSkill: boolean = false;
    }

    export class SkinData {
        constructor(data?: SkinData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
    }

    export class StringData {
        constructor(data?: StringData) {
            if (data) {
                this.Key = data.Key;
                this.SourceString = data.SourceString;
            }
        }

        Key: string = '';
        SourceString: string = '';
    }

    export class UserBlockStringData {
        constructor(data?: UserBlockStringData) {
            if (data) {
                this.Key = data.Key;
                this.SourceString = data.SourceString;
            }
        }

        Key: string = '';
        SourceString: string = '';
    }

    export class TreasureBoxData {
        constructor(data?: TreasureBoxData) {
            if (data) {
                this.ID = data.ID;
                this.AssetType = data.AssetType;
                this.AssetCount = data.AssetCount;
                this.RewardItemID = data.RewardItemID;
                this.RewardItemCount = data.RewardItemCount;
                this.Location = data.Location;
                this.Rotation = data.Rotation;
                this.Scale = data.Scale;
            }
        }

        ID: number = 0;
        AssetType: EUserAssetType[] = [];
        AssetCount: number[] = [];
        RewardItemID: number[] = [];
        RewardItemCount: number[] = [];
        Location: string = '';
        Rotation: string = '';
        Scale: string = '';
    }

    export class VehicleData {
        constructor(data?: VehicleData) {
            if (data) {
                this.ID = data.ID;
                this.Description = data.Description;
                this.DescriptionString = data.DescriptionString;
                this.DescriptionStringWithID = data.DescriptionStringWithID;
                this.Type = data.Type;
            }
        }

        ID: number = 0;
        Description: string = '';
        DescriptionString: string = '';
        DescriptionStringWithID: string = '';
        Type: string = '';
    }

    export class WeaponCategoryData {
        constructor(data?: WeaponCategoryData) {
            if (data) {
                this.ID = data.ID;
                this.WeaponCategory = data.WeaponCategory;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
            }
        }

        ID: number = 0;
        WeaponCategory: EWeaponCategory = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
    }

    export class WeaponCategoryUpgradeData {
        constructor(data?: WeaponCategoryUpgradeData) {
            if (data) {
                this.ID = data.ID;
                this.WeaponCategoryID = data.WeaponCategoryID;
                this.Parameter = data.Parameter;
                this.Value = data.Value;
                this.Rate = data.Rate;
                this.Level = data.Level;
                this.Exp = data.Exp;
                this.AssetType = data.AssetType;
                this.AssetCount = data.AssetCount;
            }
        }

        ID: number = 0;
        WeaponCategoryID: number = 0;
        Parameter: string = '';
        Value: number = 0;
        Rate: number = 0;
        Level: number = 0;
        Exp: number = 0;
        AssetType: EUserAssetType = 0;
        AssetCount: number = 0;
    }

    export class WonderCubeData {
        constructor(data?: WonderCubeData) {
            if (data) {
                this.ID = data.ID;
                this.Name = data.Name;
                this.NameString = data.NameString;
                this.NameStringWithID = data.NameStringWithID;
                this.SlotCount = data.SlotCount;
                this.AssetType1 = data.AssetType1;
                this.AssetCount1 = data.AssetCount1;
                this.AssetType2 = data.AssetType2;
                this.AssetCount2 = data.AssetCount2;
            }
        }

        ID: number = 0;
        Name: string = '';
        NameString: string = '';
        NameStringWithID: string = '';
        SlotCount: number = 0;
        AssetType1: EUserAssetType = 0;
        AssetCount1: number[] = [];
        AssetType2: EUserAssetType = 0;
        AssetCount2: number[] = [];
    }

    export class WonderCubeRwardData {
        constructor(data?: WonderCubeRwardData) {
            if (data) {
                this.ID = data.ID;
                this.Enable = data.Enable;
                this.RewardGroup = data.RewardGroup;
                this.RewardType = data.RewardType;
                this.RewardID = data.RewardID;
                this.RewardCount = data.RewardCount;
                this.RewardProb = data.RewardProb;
            }
        }

        ID: number = 0;
        Enable: boolean = false;
        RewardGroup: number = 0;
        RewardType: ERewardType = 0;
        RewardID: number = 0;
        RewardCount: number = 0;
        RewardProb: number = 0;
    }

    export class WonderStoreData {
        constructor(data?: WonderStoreData) {
            if (data) {
                this.ID = data.ID;
                this.StoreGoodsType = data.StoreGoodsType;
                this.MainType = data.MainType;
                this.SubType = data.SubType;
                this.IAPUse = data.IAPUse;
                this.ProductID_QA_PC = data.ProductID_QA_PC;
                this.ProductID_QA_AOS = data.ProductID_QA_AOS;
                this.ProductID_QA_IOS = data.ProductID_QA_IOS;
                this.ProductID_LIVE_PC = data.ProductID_LIVE_PC;
                this.ProductID_LIVE_AOS = data.ProductID_LIVE_AOS;
                this.ProductID_LIVE_IOS = data.ProductID_LIVE_IOS;
                this.RewardTableID = data.RewardTableID;
                this.SaleText = data.SaleText;
                this.ReqAssetID = data.ReqAssetID;
                this.ReqAssetCount = data.ReqAssetCount;
                this.DateSwitch = data.DateSwitch;
                this.StartDate = data.StartDate;
                this.EndDate = data.EndDate;
                this.LimitTime = data.LimitTime;
                this.LimitCount = data.LimitCount;
                this.ProductName = data.ProductName;
                this.ProductNameString = data.ProductNameString;
                this.ProductNameStringWithID = data.ProductNameStringWithID;
            }
        }

        ID: number = 0;
        StoreGoodsType: EStoreGoodsType = 0;
        MainType: string = '';
        SubType: string = '';
        IAPUse: boolean = false;
        ProductID_QA_PC: string = '';
        ProductID_QA_AOS: string = '';
        ProductID_QA_IOS: string = '';
        ProductID_LIVE_PC: string = '';
        ProductID_LIVE_AOS: string = '';
        ProductID_LIVE_IOS: string = '';
        RewardTableID: number = 0;
        SaleText: string = '';
        ReqAssetID: EUserAssetType = 0;
        ReqAssetCount: number = 0;
        DateSwitch: boolean = false;
        StartDate: Date = new Date();
        EndDate: Date = new Date();
        LimitTime: ELimitTime = 0;
        LimitCount: number = 0;
        ProductName: string = '';
        ProductNameString: string = '';
        ProductNameStringWithID: string = '';
    }

    export class WorldMapData {
        constructor(data?: WorldMapData) {
            if (data) {
                this.ID = data.ID;
                this.WorldmapPopTitle = data.WorldmapPopTitle;
                this.WorldmapPopTitleString = data.WorldmapPopTitleString;
                this.WorldmapPopTitleStringWithID = data.WorldmapPopTitleStringWithID;
            }
        }

        ID: number = 0;
        WorldmapPopTitle: string = '';
        WorldmapPopTitleString: string = '';
        WorldmapPopTitleStringWithID: string = '';
    }

    export class BiskitLogEventID {
        constructor(data?: BiskitLogEventID) {
            if (data) {
                this.SeqID = data.SeqID;
                this.EventID = data.EventID;
                this.EventName = data.EventName;
            }
        }

        SeqID: number = 0;
        EventID: string = '';
        EventName: string = '';
    }

    export class BlockContentData {
        constructor(data?: BlockContentData) {
            if (data) {
                this.SeqID = data.SeqID;
                this.PacketID = data.PacketID;
                this.PacketName = data.PacketName;
            }
        }

        SeqID: number = 0;
        PacketID: string = '';
        PacketName: string = '';
    }

    export class ErrorsData {
        constructor(data?: ErrorsData) {
            if (data) {
                this.Error = data.Error;
                this.Name = data.Name;
            }
        }

        Error: Errors = 0;
        Name: string = '';
    }

    export class NavMenuData {
        constructor(data?: NavMenuData) {
            if (data) {
                this.Path = data.Path;
                this.Name = data.Name;
            }
        }

        Path: string = '';
        Name: string = '';
    }

    export class DataTable {
        constructor(data?: DataTable) {
            if (data) {
                this.accountLevelVersion = data.accountLevelVersion;
                this.accountLevels = data.accountLevels;
                this.achievementDataVersion = data.achievementDataVersion;
                this.achievementDatas = data.achievementDatas;
                this.achievementGroupDataVersion = data.achievementGroupDataVersion;
                this.achievementGroupDatas = data.achievementGroupDatas;
                this.artifactDataVersion = data.artifactDataVersion;
                this.artifactDatas = data.artifactDatas;
                this.assetDataVersion = data.assetDataVersion;
                this.assetDatas = data.assetDatas;
                this.attendanceDataVersion = data.attendanceDataVersion;
                this.attendanceDatas = data.attendanceDatas;
                this.attendanceRewardDataVersion = data.attendanceRewardDataVersion;
                this.attendanceRewardDatas = data.attendanceRewardDatas;
                this.attributeDataVersion = data.attributeDataVersion;
                this.attributeDatas = data.attributeDatas;
                this.battleStoreDataVersion = data.battleStoreDataVersion;
                this.battleStoreDatas = data.battleStoreDatas;
                this.chapterDataVersion = data.chapterDataVersion;
                this.chapterDatas = data.chapterDatas;
                this.characterDataVersion = data.characterDataVersion;
                this.characterDatas = data.characterDatas;
                this.collectionDataVersion = data.collectionDataVersion;
                this.collectionDatas = data.collectionDatas;
                this.collectionGroupDataVersion = data.collectionGroupDataVersion;
                this.collectionGroupDatas = data.collectionGroupDatas;
                this.colorDataVersion = data.colorDataVersion;
                this.colorDatas = data.colorDatas;
                this.dataChipStoreListDataVersion = data.dataChipStoreListDataVersion;
                this.dataChipStoreListDatas = data.dataChipStoreListDatas;
                this.entitlementDataVersion = data.entitlementDataVersion;
                this.entitlementDatas = data.entitlementDatas;
                this.eventStoreDataVersion = data.eventStoreDataVersion;
                this.eventStoreDatas = data.eventStoreDatas;
                this.expressionDataVersion = data.expressionDataVersion;
                this.expressionDatas = data.expressionDatas;
                this.globalDefineDataVersion = data.globalDefineDataVersion;
                this.globalDefineDatas = data.globalDefineDatas;
                this.guideMissionDataVersion = data.guideMissionDataVersion;
                this.guideMissionDatas = data.guideMissionDatas;
                this.guideMissionStepRewardVersion = data.guideMissionStepRewardVersion;
                this.guideMissionStepRewards = data.guideMissionStepRewards;
                this.glitchStoreDataVersion = data.glitchStoreDataVersion;
                this.glitchStoreDatas = data.glitchStoreDatas;
                this.goldMedalStoreDataVersion = data.goldMedalStoreDataVersion;
                this.goldMedalStoreDatas = data.goldMedalStoreDatas;
                this.incubationDataVersion = data.incubationDataVersion;
                this.incubationDatas = data.incubationDatas;
                this.instantGuideUIDataVersion = data.instantGuideUIDataVersion;
                this.instantGuideUIDatas = data.instantGuideUIDatas;
                this.itemVersion = data.itemVersion;
                this.items = data.items;
                this.introduceDataVersion = data.introduceDataVersion;
                this.introduceDatas = data.introduceDatas;
                this.levelUpBuffListDataVersion = data.levelUpBuffListDataVersion;
                this.levelUpBuffListDatas = data.levelUpBuffListDatas;
                this.missionDataVersion = data.missionDataVersion;
                this.missionDatas = data.missionDatas;
                this.parameterDataVersion = data.parameterDataVersion;
                this.parameterDatas = data.parameterDatas;
                this.penaltyDataVersion = data.penaltyDataVersion;
                this.penaltyDatas = data.penaltyDatas;
                this.petDataVersion = data.petDataVersion;
                this.petDatas = data.petDatas;
                this.petAbilityListDataVersion = data.petAbilityListDataVersion;
                this.petAbilityListDatas = data.petAbilityListDatas;
                this.petEggDataVersion = data.petEggDataVersion;
                this.petEggDatas = data.petEggDatas;
                this.petEggGroupDataVersion = data.petEggGroupDataVersion;
                this.petEggGroupDatas = data.petEggGroupDatas;
                this.profileDataVersion = data.profileDataVersion;
                this.profileDatas = data.profileDatas;
                this.questDataVersion = data.questDataVersion;
                this.questDatas = data.questDatas;
                this.rewardDataVersion = data.rewardDataVersion;
                this.rewardDatas = data.rewardDatas;
                this.seasonMissionCountDataVersion = data.seasonMissionCountDataVersion;
                this.seasonMissionCountDatas = data.seasonMissionCountDatas;
                this.seasonMissionListDataVersion = data.seasonMissionListDataVersion;
                this.seasonMissionListDatas = data.seasonMissionListDatas;
                this.seasonPassDataVersion = data.seasonPassDataVersion;
                this.seasonPassDatas = data.seasonPassDatas;
                this.seasonPassLevelDataVersion = data.seasonPassLevelDataVersion;
                this.seasonPassLevelDatas = data.seasonPassLevelDatas;
                this.seasonPassRewardDataVersion = data.seasonPassRewardDataVersion;
                this.seasonPassRewardDatas = data.seasonPassRewardDatas;
                this.silverMedalStoreDataVersion = data.silverMedalStoreDataVersion;
                this.silverMedalStoreDatas = data.silverMedalStoreDatas;
                this.skillDataVersion = data.skillDataVersion;
                this.skillDatas = data.skillDatas;
                this.skinDataVersion = data.skinDataVersion;
                this.skinDatas = data.skinDatas;
                this.stringDataVersion = data.stringDataVersion;
                this.stringDatas = data.stringDatas;
                this.userBlockStringDataVersion = data.userBlockStringDataVersion;
                this.userBlockStringDatas = data.userBlockStringDatas;
                this.treasureBoxDataVersion = data.treasureBoxDataVersion;
                this.treasureBoxDatas = data.treasureBoxDatas;
                this.vehicleDataVersion = data.vehicleDataVersion;
                this.vehicleDatas = data.vehicleDatas;
                this.weaponCategoryDataVersion = data.weaponCategoryDataVersion;
                this.weaponCategoryDatas = data.weaponCategoryDatas;
                this.weaponCategoryUpgradeDataVersion = data.weaponCategoryUpgradeDataVersion;
                this.weaponCategoryUpgradeDatas = data.weaponCategoryUpgradeDatas;
                this.wonderCubeDataVersion = data.wonderCubeDataVersion;
                this.wonderCubeDatas = data.wonderCubeDatas;
                this.wonderCubeRwardDataVersion = data.wonderCubeRwardDataVersion;
                this.wonderCubeRwardDatas = data.wonderCubeRwardDatas;
                this.wonderStoreDataVersion = data.wonderStoreDataVersion;
                this.wonderStoreDatas = data.wonderStoreDatas;
                this.worldMapDataVersion = data.worldMapDataVersion;
                this.worldMapDatas = data.worldMapDatas;
                this.biskitLogEventIDVersion = data.biskitLogEventIDVersion;
                this.biskitLogEventIDs = data.biskitLogEventIDs;
                this.blockContentDataVersion = data.blockContentDataVersion;
                this.blockContentDatas = data.blockContentDatas;
                this.errorsDataVersion = data.errorsDataVersion;
                this.errorsDatas = data.errorsDatas;
                this.navMenuDataVersion = data.navMenuDataVersion;
                this.navMenuDatas = data.navMenuDatas;
            }
        }

        accountLevelVersion: string = '';
        accountLevels: DataTableModels.AccountLevel[] = [];
        achievementDataVersion: string = '';
        achievementDatas: DataTableModels.AchievementData[] = [];
        achievementGroupDataVersion: string = '';
        achievementGroupDatas: DataTableModels.AchievementGroupData[] = [];
        artifactDataVersion: string = '';
        artifactDatas: DataTableModels.ArtifactData[] = [];
        assetDataVersion: string = '';
        assetDatas: DataTableModels.AssetData[] = [];
        attendanceDataVersion: string = '';
        attendanceDatas: DataTableModels.AttendanceData[] = [];
        attendanceRewardDataVersion: string = '';
        attendanceRewardDatas: DataTableModels.AttendanceRewardData[] = [];
        attributeDataVersion: string = '';
        attributeDatas: DataTableModels.AttributeData[] = [];
        battleStoreDataVersion: string = '';
        battleStoreDatas: DataTableModels.BattleStoreData[] = [];
        chapterDataVersion: string = '';
        chapterDatas: DataTableModels.ChapterData[] = [];
        characterDataVersion: string = '';
        characterDatas: DataTableModels.CharacterData[] = [];
        collectionDataVersion: string = '';
        collectionDatas: DataTableModels.CollectionData[] = [];
        collectionGroupDataVersion: string = '';
        collectionGroupDatas: DataTableModels.CollectionGroupData[] = [];
        colorDataVersion: string = '';
        colorDatas: DataTableModels.ColorData[] = [];
        dataChipStoreListDataVersion: string = '';
        dataChipStoreListDatas: DataTableModels.DataChipStoreListData[] = [];
        entitlementDataVersion: string = '';
        entitlementDatas: DataTableModels.EntitlementData[] = [];
        eventStoreDataVersion: string = '';
        eventStoreDatas: DataTableModels.EventStoreData[] = [];
        expressionDataVersion: string = '';
        expressionDatas: DataTableModels.ExpressionData[] = [];
        globalDefineDataVersion: string = '';
        globalDefineDatas: DataTableModels.GlobalDefineData[] = [];
        guideMissionDataVersion: string = '';
        guideMissionDatas: DataTableModels.GuideMissionData[] = [];
        guideMissionStepRewardVersion: string = '';
        guideMissionStepRewards: DataTableModels.GuideMissionStepReward[] = [];
        glitchStoreDataVersion: string = '';
        glitchStoreDatas: DataTableModels.GlitchStoreData[] = [];
        goldMedalStoreDataVersion: string = '';
        goldMedalStoreDatas: DataTableModels.GoldMedalStoreData[] = [];
        incubationDataVersion: string = '';
        incubationDatas: DataTableModels.IncubationData[] = [];
        instantGuideUIDataVersion: string = '';
        instantGuideUIDatas: DataTableModels.InstantGuideUIData[] = [];
        itemVersion: string = '';
        items: DataTableModels.Item[] = [];
        introduceDataVersion: string = '';
        introduceDatas: DataTableModels.IntroduceData[] = [];
        levelUpBuffListDataVersion: string = '';
        levelUpBuffListDatas: DataTableModels.LevelUpBuffListData[] = [];
        missionDataVersion: string = '';
        missionDatas: DataTableModels.MissionData[] = [];
        parameterDataVersion: string = '';
        parameterDatas: DataTableModels.ParameterData[] = [];
        penaltyDataVersion: string = '';
        penaltyDatas: DataTableModels.PenaltyData[] = [];
        petDataVersion: string = '';
        petDatas: DataTableModels.PetData[] = [];
        petAbilityListDataVersion: string = '';
        petAbilityListDatas: DataTableModels.PetAbilityListData[] = [];
        petEggDataVersion: string = '';
        petEggDatas: DataTableModels.PetEggData[] = [];
        petEggGroupDataVersion: string = '';
        petEggGroupDatas: DataTableModels.PetEggGroupData[] = [];
        profileDataVersion: string = '';
        profileDatas: DataTableModels.ProfileData[] = [];
        questDataVersion: string = '';
        questDatas: DataTableModels.QuestData[] = [];
        rewardDataVersion: string = '';
        rewardDatas: DataTableModels.RewardData[] = [];
        seasonMissionCountDataVersion: string = '';
        seasonMissionCountDatas: DataTableModels.SeasonMissionCountData[] = [];
        seasonMissionListDataVersion: string = '';
        seasonMissionListDatas: DataTableModels.SeasonMissionListData[] = [];
        seasonPassDataVersion: string = '';
        seasonPassDatas: DataTableModels.SeasonPassData[] = [];
        seasonPassLevelDataVersion: string = '';
        seasonPassLevelDatas: DataTableModels.SeasonPassLevelData[] = [];
        seasonPassRewardDataVersion: string = '';
        seasonPassRewardDatas: DataTableModels.SeasonPassRewardData[] = [];
        silverMedalStoreDataVersion: string = '';
        silverMedalStoreDatas: DataTableModels.SilverMedalStoreData[] = [];
        skillDataVersion: string = '';
        skillDatas: DataTableModels.SkillData[] = [];
        skinDataVersion: string = '';
        skinDatas: DataTableModels.SkinData[] = [];
        stringDataVersion: string = '';
        stringDatas: DataTableModels.StringData[] = [];
        userBlockStringDataVersion: string = '';
        userBlockStringDatas: DataTableModels.UserBlockStringData[] = [];
        treasureBoxDataVersion: string = '';
        treasureBoxDatas: DataTableModels.TreasureBoxData[] = [];
        vehicleDataVersion: string = '';
        vehicleDatas: DataTableModels.VehicleData[] = [];
        weaponCategoryDataVersion: string = '';
        weaponCategoryDatas: DataTableModels.WeaponCategoryData[] = [];
        weaponCategoryUpgradeDataVersion: string = '';
        weaponCategoryUpgradeDatas: DataTableModels.WeaponCategoryUpgradeData[] = [];
        wonderCubeDataVersion: string = '';
        wonderCubeDatas: DataTableModels.WonderCubeData[] = [];
        wonderCubeRwardDataVersion: string = '';
        wonderCubeRwardDatas: DataTableModels.WonderCubeRwardData[] = [];
        wonderStoreDataVersion: string = '';
        wonderStoreDatas: DataTableModels.WonderStoreData[] = [];
        worldMapDataVersion: string = '';
        worldMapDatas: DataTableModels.WorldMapData[] = [];
        biskitLogEventIDVersion: string = '';
        biskitLogEventIDs: Map<string, DataTableModels.BiskitLogEventID> = new Map<string, DataTableModels.BiskitLogEventID>();
        blockContentDataVersion: string = '';
        blockContentDatas: Map<string, DataTableModels.BlockContentData> = new Map<string, DataTableModels.BlockContentData>();
        errorsDataVersion: string = '';
        errorsDatas: Map<Errors, DataTableModels.ErrorsData> = new Map<Errors, DataTableModels.ErrorsData>();
        navMenuDataVersion: string = '';
        navMenuDatas: Map<string, DataTableModels.NavMenuData> = new Map<string, DataTableModels.NavMenuData>();
    }
}
