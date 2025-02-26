import { Models } from '@ngeldata/models/index';
import { DataTableModels } from '@ngeldata/tables/model';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngeldata/autoErrors';
import { APIModelInterfaces } from '@ngeldata/models/apiModelInterfaces';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';

export namespace GameAPIModels {

    export class GameKickUserParameters {
        UID: string = '';
    }

    export class GameKickUserResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
    }

    export class GameAccountsParameters {
        UID: number = 0;
    }

    export class GameAccountsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        accounts: Models.Account[] = [];
    }

    export class GameAchievementsParameters {
        UID: number = 0;
    }

    export class GameAchievementsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        achievements: Models.Achievement[] = [];
    }

    export class GameSaveAchievementsParameters {
        achievements: Models.Achievement[] = [];
    }

    export class GameSaveAchievementsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteAchievementsParameters {
        achievements: Models.Achievement[] = [];
    }

    export class GameDeleteAchievementsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameArtifactsParameters {
        UID: number = 0;
    }

    export class GameArtifactsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        artifacts: Models.Artifact[] = [];
    }

    export class GameSaveArtifactParameters {
        artifacts: Models.Artifact[] = [];
    }

    export class GameSaveArtifactResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteArtifactParameters {
        artifacts: Models.Artifact[] = [];
    }

    export class GameDeleteArtifactResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameArtifactDecksParameters {
        UID: number = 0;
    }

    export class GameArtifactDecksResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        artifactDecks: Models.ArtifactDeck[] = [];
    }

    export class GameAssetsParameters {
        UID: number = 0;
    }

    export class GameAssetsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        assets: Models.Asset[] = [];
    }

    export class GameSaveAssetsParameters {
        assets: Models.Asset[] = [];
    }

    export class GameSaveAssetsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteAssetsParameters {
        assets: Models.Asset[] = [];
    }

    export class GameDeleteAssetsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameAttendancesParameters {
        UID: number = 0;
    }

    export class GameAttendancesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        attendances: Models.Attendance[] = [];
    }

    export class GameSaveAttendanceParameters {
        UID: number = 0;
        AttendanceID: number = 0;
        RewardState: string = '';
    }

    export class GameSaveAttendanceResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GameEventStoresParameters {
        UID: number = 0;
    }

    export class GameEventStoresResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        eventStores: Models.EventStore[] = [];
    }

    export class GameExpressionsParameters {
        UID: number = 0;
    }

    export class GameExpressionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        expressions: Models.Expression[] = [];
    }

    export class GameExpressionPresetsParameters {
        UID: number = 0;
    }

    export class GameExpressionPresetsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        expressionPresets: Models.ExpressionPreset[] = [];
    }

    export class GameCollectionsParameters {
        UID: number = 0;
    }

    export class GameCollectionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        collections: Models.Collection[] = [];
    }

    export class GameSaveCollectionsParameters {
        collections: Models.Collection[] = [];
    }

    export class GameSaveCollectionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteCollectionsParameters {
        collections: Models.Collection[] = [];
    }

    export class GameDeleteCollectionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameCraftsParameters {
        UID: number = 0;
    }

    export class GameCraftsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        crafts: Models.Craft[] = [];
    }

    export class GameWonderCubesParameters {
        UID: number = 0;
    }

    export class GameWonderCubesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        wonderCubes: Models.WonderCube[] = [];
    }

    export class GameSaveWonderCubesParameters {
        wonderCubes: Models.WonderCube[] = [];
    }

    export class GameSaveWonderCubesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameWonderStoresParameters {
        UID: number = 0;
    }

    export class GameWonderStoresResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        wonderStores: Models.WonderStore[] = [];
    }

    export class GameSaveWonderStoresParameters {
        wonderStores: Models.WonderStore[] = [];
    }

    export class GameSaveWonderStoresResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameEntitlementsParameters {
        UID: number = 0;
    }

    export class GameEntitlementsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        entitlements: Models.Entitlement[] = [];
    }

    export class GameSaveEntitlementsParameters {
        entitlements: Models.Entitlement[] = [];
    }

    export class GameSaveEntitlementsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteEntitlementsParameters {
        entitlements: Models.Entitlement[] = [];
    }

    export class GameDeleteEntitlementsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameFriendsParameters {
        UID: number = 0;
    }

    export class GameFriendsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        friends: Models.Friend[] = [];
    }

    export class GameGlitchStoresParameters {
        UID: number = 0;
    }

    export class GameGlitchStoresResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        glitchStores: Models.GlitchStore[] = [];
    }

    export class GameGuideMissionsParameters {
        UID: number = 0;
    }

    export class GameGuideMissionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        guideMissions: Models.GuideMission[] = [];
    }

    export class GameSaveGuideMissionsParameters {
        guideMissions: Models.GuideMission[] = [];
    }

    export class GameSaveGuideMissionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameGuideMissionProgressRewardParameters {
        UID: number = 0;
    }

    export class GameGuideMissionProgressRewardResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        guideMissionProgressReward?: Models.GuideMissionProgressReward|null = null;
    }

    export class GameHeroesParameters {
        UID: number = 0;
    }

    export class GameHeroesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        heroes: Models.Hero[] = [];
    }

    export class GameSkinsParameters {
        UID: number = 0;
    }

    export class GameSkinsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        skins: Models.HeroSkin[] = [];
    }

    export class GameSkinPresetsParameters {
        UID: number = 0;
    }

    export class GameSkinPresetsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        skinPresets: Models.HeroSkinPreset[] = [];
    }

    export class GameInventoriesParameters {
        UID: number = 0;
    }

    export class GameInventoriesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        inventories: Models.Inventory[] = [];
    }

    export class GameSaveInventoriesParameters {
        inventories: Models.Inventory[] = [];
    }

    export class GameSaveInventoriesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteInventoriesParameters {
        inventories: Models.Inventory[] = [];
    }

    export class GameDeleteInventoriesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameMailsParameters {
        UID: number = 0;
    }

    export class GameMailsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        mails: Models.Mail[] = [];
    }

    export class GameDeleteMailsParameters {
        mails?: Models.Mail[]|null = [];
    }

    export class GameDeleteMailsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameMazeRewardBoxesParameters {
        UID: number = 0;
    }

    export class GameMazeRewardBoxesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        mazeRewardBoxes: Models.MazeRewardBox[] = [];
    }

    export class GameUserBillingsParameters {
        UID: number = 0;
    }

    export class GameUserBillingsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        billings: Models.Billing[] = [];
    }

    export class GameIncubationsParameters {
        UID: number = 0;
    }

    export class GameIncubationsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        incubations: Models.Incubation[] = [];
    }

    export class GameInstantGuidesParameters {
        UID: number = 0;
    }

    export class GameInstantGuidesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        instantGuides: Models.InstantGuide[] = [];
    }

    export class GameBillingsParameters {
        startTime: string = '';
        endTime: string = '';
    }

    export class GameBillingsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        billings: Models.Billing[] = [];
    }

    export class GameNicePlayersAllResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        nicePlayers: Models.NicePlayer[] = [];
    }

    export class GameNicePlayersParameters {
        UID: number = 0;
    }

    export class GameNicePlayersResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        nicePlayers: Models.NicePlayer[] = [];
    }

    export class GamePenaltiesAllParameters {
        startTime?: string|null = null;
        endTime?: string|null = null;
        uid?: number|null = 0;
        memberNo?: number|null = 0;
        nick?: string|null = null;
    }

    export class GamePenaltiesAllResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        penalties: Models.PenaltyWithUser[] = [];
    }

    export class GamePenaltyParameters {
        penalty: Models.Penalty|null = null;
    }

    export class GamePenaltyResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        penalty: Models.PenaltyWithUser|null = null;
    }

    export class GamePenaltiesParameters {
        UID: number = 0;
    }

    export class GamePenaltiesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        penalties: Models.Penalty[] = [];
    }

    export class GameSavePenaltyParameters {
        penalty: Models.PenaltyWithUser|null = null;
    }

    export class GameSavePenaltyResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GamePetsParameters {
        UID: number = 0;
    }

    export class GamePetsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        pets: Models.Pet[] = [];
    }

    export class GamePlayRecordGoldClashesParameters {
        UID: number = 0;
    }

    export class GamePlayRecordGoldClashesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        playRecordGoldClashes: Models.PlayRecordGoldClash[] = [];
    }

    export class GamePlayRecordRpgsParameters {
        UID: number = 0;
    }

    export class GamePlayRecordRpgsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        playRecordRpgs: Models.PlayRecordRpg[] = [];
    }

    export class GameProfilesParameters {
        UID: number = 0;
    }

    export class GameProfilesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        profiles: Models.Profile[] = [];
    }

    export class GameSaveProfilesParameters {
        profiles: Models.Profile[] = [];
    }

    export class GameSaveProfilesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteProfilesParameters {
        profiles: Models.Profile[] = [];
    }

    export class GameDeleteProfilesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameRankingRewardsParameters {
        UID: number = 0;
    }

    export class GameRankingRewardsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        rankingRewards: Models.RankingReward[] = [];
    }

    export class GameRpgAttributesParameters {
        UID: number = 0;
    }

    export class GameRpgAttributesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        rpgAttributes: Models.RpgAttribute[] = [];
    }

    export class GameSaveRpgAttributesParameters {
        rpgAttributes: Models.RpgAttribute[] = [];
    }

    export class GameSaveRpgAttributesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteRpgAttributesParameters {
        rpgAttributes: Models.RpgAttribute[] = [];
    }

    export class GameDeleteRpgAttributesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameSeasonMissionsParameters {
        UID: number = 0;
    }

    export class GameSeasonMissionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        seasonMissions: Models.SeasonMission[] = [];
    }

    export class GameSaveSeasonMissionsParameters {
        seasonMissions: Models.SeasonMission[] = [];
    }

    export class GameSaveSeasonMissionsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameSeasonPassesParameters {
        UID: number = 0;
    }

    export class GameSeasonPassesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        seasonPasses: Models.SeasonPass[] = [];
    }

    export class GameSaveSeasonPassParameters {
        seasonPass: Models.SeasonPass|null = null;
    }

    export class GameSaveSeasonPassResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GameTreasureBoxesParameters {
        UID: number = 0;
    }

    export class GameTreasureBoxesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        treasureBoxes: Models.TreasureBox[] = [];
    }

    export class GameSaveTreasureBoxesParameters {
        treasureBoxes: Models.TreasureBox[] = [];
    }

    export class GameSaveTreasureBoxesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteTreasureBoxesParameters {
        treasureBoxes: Models.TreasureBox[] = [];
    }

    export class GameDeleteTreasureBoxesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameUserAccountByNickParameters {
        Nick: string = '';
    }

    export class GameUserAccountByNickResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
    }

    export class GameUserAccountByUIDParameters {
        UID: string = '';
    }

    export class GameUserAccountByUIDResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
    }

    export class GameUserAccountByMemberNoParameters {
        MemberNo: string = '';
    }

    export class GameUserAccountByMemberNoResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
    }

    export class GameSaveUserAccountParameters {
        userAccount: Models.UserAccount|null = null;
    }

    export class GameSaveUserAccountResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GameUserBlockByNickParameters {
        Nick: string = '';
    }

    export class GameUserBlockByNickResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
        accounts: Models.Account[] = [];
        userBlocks: Models.UserBlock[] = [];
    }

    export class GameUserBlockByMemberNoParameters {
        MemberNo: string = '';
    }

    export class GameUserBlockByMemberNoResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
        accounts: Models.Account[] = [];
        userBlocks: Models.UserBlock[] = [];
    }

    export class GameUserBlockByUIDParameters {
        UID: string = '';
    }

    export class GameUserBlockByUIDResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userAccount: Models.UserAccount|null = null;
        accounts: Models.Account[] = [];
        userBlocks: Models.UserBlock[] = [];
    }

    export class GameSaveUserBlockParameters {
        userBlocks: Models.UserBlock[] = [];
    }

    export class GameSaveUserBlockResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteUserBlockParameters {
        userBlocks: Models.UserBlock[] = [];
    }

    export class GameDeleteUserBlockResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameUserDevicesParameters {
        UID: number = 0;
    }

    export class GameUserDevicesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userDevices: Models.UserDevice[] = [];
    }

    export class GameWeaponCategoriesParameters {
        UID: number = 0;
    }

    export class GameWeaponCategoriesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        weaponCategories: Models.WeaponCategory[] = [];
    }

    export class GameSaveWeaponCategoriesParameters {
        weaponCategories: Models.WeaponCategory[] = [];
    }

    export class GameSaveWeaponCategoriesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteWeaponCategoriesParameters {
        weaponCategories: Models.WeaponCategory[] = [];
    }

    export class GameDeleteWeaponCategoriesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameSearchUserAccountsParameters {
        userUIDs: number[] = [];
    }

    export class GameSearchUserAccountsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
        userAccounts: Map<number,Models.UserAccount|null> = new Map<number,Models.UserAccount|null>();
    }

    export class GameSearchUserAccountsByMemberNoParameters {
        memberNos: number[] = [];
    }

    export class GameSearchUserAccountsByMemberNoResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
        userAccounts: Map<number,Models.UserAccount|null> = new Map<number,Models.UserAccount|null>();
    }

    export class GameImmediatelySendMailParameters {
        userUIDs: number[] = [];
        mailInput: Models.MailInput|null = null;
    }

    export class GameImmediatelySendMailResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameMailWithUsersParameters {
        startTime: string = '';
        endTime: string = '';
    }

    export class GameMailWithUsersResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        mails: Models.MailWithUser[] = [];
    }

    export class GameDeleteMailWithUsersParameters {
        mailIDs: number[] = [];
    }

    export class GameDeleteMailWithUsersResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorMailId: number = 0;
    }

    export class GameMaintenancesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        maintenances: Models.Maintenance[] = [];
    }

    export class GameSaveMaintenancesParameters {
        maintenances?: Models.Maintenance[]|null = [];
    }

    export class GameSaveMaintenancesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteMaintenancesParameters {
        maintenances?: Models.Maintenance[]|null = [];
    }

    export class GameDeleteMaintenancesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameNoticeBannersResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        noticeBanners: Models.NoticeBanner[] = [];
    }

    export class GameNoticeBannerParameters {
        BannerID: number = 0;
    }

    export class GameNoticeBannerResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        noticeBanner: Models.NoticeBanner|null = null;
    }

    export class GameSaveNoticeBannersParameters {
        noticeBanners?: Models.NoticeBanner[]|null = [];
    }

    export class GameSaveNoticeBannersResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteNoticeBannersParameters {
        noticeBanners?: Models.NoticeBanner[]|null = [];
    }

    export class GameDeleteNoticeBannersResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameOpenWorldTimeResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        openWorldTime: Models.OpenWorldTime|null = null;
    }

    export class GameSaveOpenWorldTimeParameters {
        openWorldTime: Models.OpenWorldTime|null = null;
    }

    export class GameSaveOpenWorldTimeResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GameChattingNoticesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        chattingNotices: Models.ChattingNotice[] = [];
    }

    export class GameChattingNoticeParameters {
        id: string = '';
    }

    export class GameChattingNoticeResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        chattingNotice?: Models.ChattingNotice|null = null;
    }

    export class GameSaveChattingNoticesParameters {
        chattingNotices?: Models.ChattingNotice[]|null = [];
    }

    export class GameSaveChattingNoticesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameDeleteChattingNoticesParameters {
        chattingNotices?: Models.ChattingNotice[]|null = [];
    }

    export class GameDeleteChattingNoticesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class GameImmediatelyChattingNoticeParameters {
        id: string = NIL;
    }

    export class GameImmediatelyChattingNoticeResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GameGoldClashTimeResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        goldClashTime: Models.GoldClashTime|null = null;
    }

    export class GameSaveGoldClashTimeParameters {
        goldClashTime: Models.GoldClashTime|null = null;
    }

    export class GameSaveGoldClashTimeResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class GameSilverMedalStoresParameters {
        UID: number = 0;
    }

    export class GameSilverMedalStoresResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        silverMedalStores: Models.SilverMedalStore[] = [];
    }

    export class GameLobbyServerTimesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        lobbyServerTimes: Models.LobbyServerTime[] = [];
    }

    export class GameSaveLobbyServerTimesParameters {
        serverTime?: Date|null = new Date();
    }

    export class GameSaveLobbyServerTimesResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        lobbyServerTimes: Models.LobbyServerTime[] = [];
    }

    export class GameEventMailsParameters {
        startTime?: Date|null = new Date();
        endTime?: Date|null = new Date();
    }

    export class GameEventMailsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        eventMails: Models.EventMail[] = [];
    }

    export class GameEventMailParameters {
        id?: number|null = 0;
    }

    export class GameEventMailResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        eventMail?: Models.EventMail|null = null;
    }

    export class GameDailyRankingParameters {
        gameType: Defines.GameType = 0;
        dateTime?: Date|null = new Date();
    }

    export class GameDailyRankingResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        gameType: Defines.GameType = 0;
        gameRankingScore: Models.GameRankingScore[] = [];
    }

}
