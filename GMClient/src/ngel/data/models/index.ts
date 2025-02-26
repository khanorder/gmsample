import { Defines } from '@ngeldata/autoDefines';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';
import { v4 as uuidv4, NIL } from 'uuid';
import deepmerge from 'deepmerge';
import isEmpty from 'lodash/isEmpty';
import {
    EMailType,
    EUserState,
    ECollectionType,
    EExpressionType,
    EGuideMissionCategory,
    EItemType,
    EMailStateType,
    ERewardType,
    EPenaltyReportState,
    EProfileType,
    EContentsType,
    EMissionGroup,
    EMarketCode,
    EPlatform,
    ENoticeType,
} from '../models/lobby';

export namespace Models {

    export interface BlockIPInterface {
        ID: number;
        IPAddress: string;
        StartTime: Date;
        EndTime: Date;
        Reason: string;
    }

    export class BlockIP implements BlockIPInterface {
        constructor (data?: BlockIP) {
            if (data) {
                this.ID = data.ID;
                this.IPAddress = data.IPAddress;
                this.StartTime = data.StartTime;
                this.EndTime = data.EndTime;
                this.Reason = data.Reason;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        ID: number = 0;
        IPAddress: string = '';
        StartTime: Date = new Date();
        EndTime: Date = new Date();
        Reason: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: BlockIP, ID: number): boolean {
           return data.ID == ID;
        }

        static compareInstance(data: BlockIP, rdata: BlockIP): boolean {
           return data.ID == rdata.ID;
        }
    }

    export interface EventMailInterface {
        ID: number;
        MailType: EMailType;
        Title: string;
        Message: string;
        RewardList: MailReward[];
        ExpireTime: number;
        StartTime: Date;
        EndTime: Date;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class EventMail implements EventMailInterface {
        constructor (data?: EventMail) {
            if (data) {
                this.ID = data.ID;
                this.MailType = data.MailType;
                this.Title = data.Title;
                this.Message = data.Message;
                this.RewardList.push(...data.RewardList);
                this.ExpireTime = data.ExpireTime;
                this.StartTime = data.StartTime;
                this.EndTime = data.EndTime;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        ID: number = 0;
        MailType: EMailType = 0;
        Title: string = '';
        Message: string = '';
        RewardList: MailReward[] = [];
        ExpireTime: number = 0;
        StartTime: Date = new Date();
        EndTime: Date = new Date();
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: EventMail, ID: number): boolean {
           return data.ID == ID;
        }

        static compareInstance(data: EventMail, rdata: EventMail): boolean {
           return data.ID == rdata.ID;
        }
    }

    export interface MaintenanceInterface {
        MaintenanceID: number;
        Platform: Defines.MaintenancePlatform;
        Area: string;
        State: Defines.MaintenanceState;
        StartTime: Date;
        EndTime: Date;
        NoticeStrID: string;
        UpdateStrID: string;
        RecomUpdateStrID: string;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Maintenance implements MaintenanceInterface {
        constructor (data?: Maintenance) {
            if (data) {
                this.MaintenanceID = data.MaintenanceID;
                this.Platform = data.Platform;
                this.Area = data.Area;
                this.State = data.State;
                this.StartTime = data.StartTime;
                this.EndTime = data.EndTime;
                this.NoticeStrID = data.NoticeStrID;
                this.UpdateStrID = data.UpdateStrID;
                this.RecomUpdateStrID = data.RecomUpdateStrID;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
                this.isKicked = data.isKicked;
            }
        }

        MaintenanceID: number = 0;
        Platform: Defines.MaintenancePlatform = 0;
        Area: string = '';
        State: Defines.MaintenanceState = 0;
        StartTime: Date = new Date();
        EndTime: Date = new Date();
        NoticeStrID: string = '';
        UpdateStrID: string = '';
        RecomUpdateStrID: string = '';
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;
        isKicked?: boolean = false;

        static compareKey(data: Maintenance, MaintenanceID: number): boolean {
           return data.MaintenanceID == MaintenanceID;
        }

        static compareInstance(data: Maintenance, rdata: Maintenance): boolean {
           return data.MaintenanceID == rdata.MaintenanceID;
        }
    }

    export interface NoticeBannerInterface {
        BannerID: number;
        StartAt: number;
        EndAt: number;
        ImageURL: string;
        Title: string;
        Message: string;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class NoticeBanner implements NoticeBannerInterface {
        constructor (data?: NoticeBanner) {
            if (data) {
                this.BannerID = data.BannerID;
                this.StartAt = data.StartAt;
                this.EndAt = data.EndAt;
                this.ImageURL = data.ImageURL;
                this.Title = data.Title;
                this.Message = data.Message;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        BannerID: number = 0;
        StartAt: number = 0;
        EndAt: number = 0;
        ImageURL: string = '';
        Title: string = '';
        Message: string = '';
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: NoticeBanner, BannerID: number): boolean {
           return data.BannerID == BannerID;
        }

        static compareInstance(data: NoticeBanner, rdata: NoticeBanner): boolean {
           return data.BannerID == rdata.BannerID;
        }
    }

    export interface SlangInterface {
        ID: number;
        Word: string;
    }

    export class Slang implements SlangInterface {
        constructor (data?: Slang) {
            if (data) {
                this.ID = data.ID;
                this.Word = data.Word;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        ID: number = 0;
        Word: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Slang, ID: number): boolean {
           return data.ID == ID;
        }

        static compareInstance(data: Slang, rdata: Slang): boolean {
           return data.ID == rdata.ID;
        }
    }

    export interface WhiteListInterface {
        ID: number;
        DeviceID: string;
        MemberNo: number;
        Comment: string;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class WhiteList implements WhiteListInterface {
        constructor (data?: WhiteList) {
            if (data) {
                this.ID = data.ID;
                this.DeviceID = data.DeviceID;
                this.MemberNo = data.MemberNo;
                this.Comment = data.Comment;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        ID: number = 0;
        DeviceID: string = '';
        MemberNo: number = 0;
        Comment: string = '';
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: WhiteList, ID: number): boolean {
           return data.ID == ID;
        }

        static compareInstance(data: WhiteList, rdata: WhiteList): boolean {
           return data.ID == rdata.ID;
        }
    }

    export interface ChatLogInterface {
        logID: number;
        accountId: number;
        eventId: string;
        stoveMemberNo: string;
        stoveNicknameNo: string;
        accountName: string;
        sessionId: string;
        ipAddress: string;
        message: string;
        timeStamp: Date;
    }

    export class ChatLog implements ChatLogInterface {
        constructor (data?: ChatLog) {
            if (data) {
                this.logID = data.logID;
                this.accountId = data.accountId;
                this.eventId = data.eventId;
                this.stoveMemberNo = data.stoveMemberNo;
                this.stoveNicknameNo = data.stoveNicknameNo;
                this.accountName = data.accountName;
                this.sessionId = data.sessionId;
                this.ipAddress = data.ipAddress;
                this.message = data.message;
                this.timeStamp = data.timeStamp;
            }
        }

        logID: number = 0;
        accountId: number = 0;
        eventId: string = '';
        stoveMemberNo: string = '';
        stoveNicknameNo: string = '';
        accountName: string = '';
        sessionId: string = '';
        ipAddress: string = '';
        message: string = '';
        timeStamp: Date = new Date();

        static compareKey(data: ChatLog, logID: number, accountId: number): boolean {
           return data.logID == logID && data.accountId == accountId;
        }

        static compareInstance(data: ChatLog, rdata: ChatLog): boolean {
           return data.logID == rdata.logID && data.accountId == rdata.accountId;
        }
    }

    export interface ActiveUserAccountInterface {
        UID: number;
        Nick: string;
        UserLevel: number;
        MemberNo: number|null;
        LogoutAt: number;
        UserState: Defines.UserState;
        ProfileIconID: number;
        LobbyID: string;
    }

    export class ActiveUserAccount implements ActiveUserAccountInterface {
        constructor (data?: ActiveUserAccount) {
            if (data) {
                this.UID = data.UID;
                this.Nick = data.Nick;
                this.UserLevel = data.UserLevel;
                this.MemberNo = data.MemberNo;
                this.LogoutAt = data.LogoutAt;
                this.UserState = data.UserState;
                this.ProfileIconID = data.ProfileIconID;
                this.LobbyID = data.LobbyID;
            }
        }

        UID: number = 0;
        Nick: string = '';
        UserLevel: number = 0;
        MemberNo: number|null = 0;
        LogoutAt: number = 0;
        UserState: Defines.UserState = 0;
        ProfileIconID: number = 0;
        LobbyID: string = '';

        static compareKey(data: ActiveUserAccount, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: ActiveUserAccount, rdata: ActiveUserAccount): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface ActiveUserInterface {
        UID: number;
        LobbyID: string;
        UserState: EUserState;
        MatchState: number;
        OpenWorldID: string;
    }

    export class ActiveUser implements ActiveUserInterface {
        constructor (data?: ActiveUser) {
            if (data) {
                this.UID = data.UID;
                this.LobbyID = data.LobbyID;
                this.UserState = data.UserState;
                this.MatchState = data.MatchState;
                this.OpenWorldID = data.OpenWorldID;
            }
        }

        UID: number = 0;
        LobbyID: string = '';
        UserState: EUserState = 0;
        MatchState: number = 0;
        OpenWorldID: string = '';

        static compareKey(data: ActiveUser, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: ActiveUser, rdata: ActiveUser): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface OpenWorldTimeInterface {
        Minutes: number;
        Multiply: number;
        StartAt: number;
    }

    export class OpenWorldTime implements OpenWorldTimeInterface {
        constructor (data?: OpenWorldTime) {
            if (data) {
                this.Minutes = data.Minutes;
                this.Multiply = data.Multiply;
                this.StartAt = data.StartAt;
            }
        }

        Minutes: number = 0;
        Multiply: number = 0;
        StartAt: number = 0;
    }

    export interface GoldClashTimeInterface {
        StartHour: number;
        StartMinute: number;
        EndHour: number;
        EndMinute: number;
    }

    export class GoldClashTime implements GoldClashTimeInterface {
        constructor (data?: GoldClashTime) {
            if (data) {
                this.StartHour = data.StartHour;
                this.StartMinute = data.StartMinute;
                this.EndHour = data.EndHour;
                this.EndMinute = data.EndMinute;
            }
        }

        StartHour: number = 0;
        StartMinute: number = 0;
        EndHour: number = 0;
        EndMinute: number = 0;
    }

    export interface LobbyServerTimeInterface {
        lobbyID: string;
        serverTime: Date;
    }

    export class LobbyServerTime implements LobbyServerTimeInterface {
        constructor (data?: LobbyServerTime) {
            if (data) {
                this.lobbyID = data.lobbyID;
                this.serverTime = data.serverTime;
                this.isChanged = data.isChanged;
            }
        }

        lobbyID: string = '';
        serverTime: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: LobbyServerTime, lobbyID: string): boolean {
           return data.lobbyID == lobbyID;
        }

        static compareInstance(data: LobbyServerTime, rdata: LobbyServerTime): boolean {
           return data.lobbyID == rdata.lobbyID;
        }
    }

    export interface GameRankingScoreInterface {
        UID: number;
        UserAccount: Models.UserAccount|null;
        Score: number;
    }

    export class GameRankingScore implements GameRankingScoreInterface {
        constructor (data?: GameRankingScore) {
            if (data) {
                this.UID = data.UID;
                this.UserAccount = data.UserAccount;
                this.Score = data.Score;
            }
        }

        UID: number = 0;
        UserAccount: Models.UserAccount|null = null;
        Score: number = 0;

        static compareKey(data: GameRankingScore, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: GameRankingScore, rdata: GameRankingScore): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface AccountInterface {
        UID: number;
        AccountType: Defines.GameAuthType;
        AccountID: string;
        MemberNo: number;
        WorldID: string;
        IsLeave: boolean;
        CreateAt: Date;
    }

    export class Account implements AccountInterface {
        constructor (data?: Account) {
            if (data) {
                this.UID = data.UID;
                this.AccountType = data.AccountType;
                this.AccountID = data.AccountID;
                this.MemberNo = data.MemberNo;
                this.WorldID = data.WorldID;
                this.IsLeave = data.IsLeave;
                this.CreateAt = data.CreateAt;
            }
        }

        UID: number = 0;
        AccountType: Defines.GameAuthType = 0;
        AccountID: string = '';
        MemberNo: number = 0;
        WorldID: string = '';
        IsLeave: boolean = false;
        CreateAt: Date = new Date();

        static compareKey(data: Account, UID: number, AccountType: Defines.GameAuthType, AccountID: string): boolean {
           return data.UID == UID && data.AccountType == AccountType && data.AccountID == AccountID;
        }

        static compareInstance(data: Account, rdata: Account): boolean {
           return data.UID == rdata.UID && data.AccountType == rdata.AccountType && data.AccountID == rdata.AccountID;
        }
    }

    export interface AchievementInterface {
        UID: number;
        AchievementID: number;
        Count: number;
        CompleteAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Achievement implements AchievementInterface {
        constructor (data?: Achievement) {
            if (data) {
                this.UID = data.UID;
                this.AchievementID = data.AchievementID;
                this.Count = data.Count;
                this.CompleteAt = data.CompleteAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        AchievementID: number = 0;
        Count: number = 0;
        CompleteAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Achievement, UID: number, AchievementID: number): boolean {
           return data.UID == UID && data.AchievementID == AchievementID;
        }

        static compareInstance(data: Achievement, rdata: Achievement): boolean {
           return data.UID == rdata.UID && data.AchievementID == rdata.AchievementID;
        }
    }

    export interface ArtifactInterface {
        UID: number;
        ArtifactID: number;
        Enhance: number;
        Count: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Artifact implements ArtifactInterface {
        constructor (data?: Artifact) {
            if (data) {
                this.UID = data.UID;
                this.ArtifactID = data.ArtifactID;
                this.Enhance = data.Enhance;
                this.Count = data.Count;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        ArtifactID: number = 0;
        Enhance: number = 0;
        Count: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Artifact, UID: number, ArtifactID: number): boolean {
           return data.UID == UID && data.ArtifactID == ArtifactID;
        }

        static compareInstance(data: Artifact, rdata: Artifact): boolean {
           return data.UID == rdata.UID && data.ArtifactID == rdata.ArtifactID;
        }
    }

    export interface ArtifactDeckInterface {
        UID: number;
        SlotID: number;
        DeckName: string;
        ArtifactID0: number;
        ArtifactID1: number;
        ArtifactID2: number;
        ArtifactID3: number;
        ArtifactID4: number;
        ArtifactID5: number;
        ArtifactID6: number;
        ArtifactID7: number;
        ArtifactID8: number;
        Enhance0: number|null;
        Enhance1: number|null;
        Enhance2: number|null;
        Enhance3: number|null;
        Enhance4: number|null;
        Enhance5: number|null;
        Enhance6: number|null;
        Enhance7: number|null;
        Enhance8: number|null;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class ArtifactDeck implements ArtifactDeckInterface {
        constructor (data?: ArtifactDeck) {
            if (data) {
                this.UID = data.UID;
                this.SlotID = data.SlotID;
                this.DeckName = data.DeckName;
                this.ArtifactID0 = data.ArtifactID0;
                this.ArtifactID1 = data.ArtifactID1;
                this.ArtifactID2 = data.ArtifactID2;
                this.ArtifactID3 = data.ArtifactID3;
                this.ArtifactID4 = data.ArtifactID4;
                this.ArtifactID5 = data.ArtifactID5;
                this.ArtifactID6 = data.ArtifactID6;
                this.ArtifactID7 = data.ArtifactID7;
                this.ArtifactID8 = data.ArtifactID8;
                this.Enhance0 = data.Enhance0;
                this.Enhance1 = data.Enhance1;
                this.Enhance2 = data.Enhance2;
                this.Enhance3 = data.Enhance3;
                this.Enhance4 = data.Enhance4;
                this.Enhance5 = data.Enhance5;
                this.Enhance6 = data.Enhance6;
                this.Enhance7 = data.Enhance7;
                this.Enhance8 = data.Enhance8;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        SlotID: number = 0;
        DeckName: string = '';
        ArtifactID0: number = 0;
        ArtifactID1: number = 0;
        ArtifactID2: number = 0;
        ArtifactID3: number = 0;
        ArtifactID4: number = 0;
        ArtifactID5: number = 0;
        ArtifactID6: number = 0;
        ArtifactID7: number = 0;
        ArtifactID8: number = 0;
        Enhance0: number|null = 0;
        Enhance1: number|null = 0;
        Enhance2: number|null = 0;
        Enhance3: number|null = 0;
        Enhance4: number|null = 0;
        Enhance5: number|null = 0;
        Enhance6: number|null = 0;
        Enhance7: number|null = 0;
        Enhance8: number|null = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: ArtifactDeck, UID: number, SlotID: number): boolean {
           return data.UID == UID && data.SlotID == SlotID;
        }

        static compareInstance(data: ArtifactDeck, rdata: ArtifactDeck): boolean {
           return data.UID == rdata.UID && data.SlotID == rdata.SlotID;
        }
    }

    export interface AssetInterface {
        UID: number;
        AssetID: number;
        Count: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Asset implements AssetInterface {
        constructor (data?: Asset) {
            if (data) {
                this.UID = data.UID;
                this.AssetID = data.AssetID;
                this.Count = data.Count;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        AssetID: number = 0;
        Count: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Asset, UID: number, AssetID: number): boolean {
           return data.UID == UID && data.AssetID == AssetID;
        }

        static compareInstance(data: Asset, rdata: Asset): boolean {
           return data.UID == rdata.UID && data.AssetID == rdata.AssetID;
        }
    }

    export interface AttendanceInterface {
        UID: number;
        AttendanceID: number;
        AttendanceDay: number;
        RewardState: string;
        LastAttendanceAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Attendance implements AttendanceInterface {
        constructor (data?: Attendance) {
            if (data) {
                this.UID = data.UID;
                this.AttendanceID = data.AttendanceID;
                this.AttendanceDay = data.AttendanceDay;
                this.RewardState = data.RewardState;
                this.LastAttendanceAt = data.LastAttendanceAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        AttendanceID: number = 0;
        AttendanceDay: number = 0;
        RewardState: string = '';
        LastAttendanceAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Attendance, UID: number, AttendanceID: number): boolean {
           return data.UID == UID && data.AttendanceID == AttendanceID;
        }

        static compareInstance(data: Attendance, rdata: Attendance): boolean {
           return data.UID == rdata.UID && data.AttendanceID == rdata.AttendanceID;
        }
    }

    export interface BattleStoreInterface {
        UID: number;
        StoreID: number;
        Count: number;
        ExpireAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class BattleStore implements BattleStoreInterface {
        constructor (data?: BattleStore) {
            if (data) {
                this.UID = data.UID;
                this.StoreID = data.StoreID;
                this.Count = data.Count;
                this.ExpireAt = data.ExpireAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        StoreID: number = 0;
        Count: number = 0;
        ExpireAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: BattleStore, UID: number, StoreID: number): boolean {
           return data.UID == UID && data.StoreID == StoreID;
        }

        static compareInstance(data: BattleStore, rdata: BattleStore): boolean {
           return data.UID == rdata.UID && data.StoreID == rdata.StoreID;
        }
    }

    export interface CollectionInterface {
        UID: number;
        CollectionType: ECollectionType;
        CollectionID: number;
        IsRewarded: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Collection implements CollectionInterface {
        constructor (data?: Collection) {
            if (data) {
                this.UID = data.UID;
                this.CollectionType = data.CollectionType;
                this.CollectionID = data.CollectionID;
                this.IsRewarded = data.IsRewarded;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        CollectionType: ECollectionType = 0;
        CollectionID: number = 0;
        IsRewarded: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Collection, UID: number, CollectionType: ECollectionType, CollectionID: number): boolean {
           return data.UID == UID && data.CollectionType == CollectionType && data.CollectionID == CollectionID;
        }

        static compareInstance(data: Collection, rdata: Collection): boolean {
           return data.UID == rdata.UID && data.CollectionType == rdata.CollectionType && data.CollectionID == rdata.CollectionID;
        }
    }

    export interface CraftInterface {
        UID: number;
        CraftID: number;
        Count: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Craft implements CraftInterface {
        constructor (data?: Craft) {
            if (data) {
                this.UID = data.UID;
                this.CraftID = data.CraftID;
                this.Count = data.Count;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        CraftID: number = 0;
        Count: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Craft, UID: number, CraftID: number): boolean {
           return data.UID == UID && data.CraftID == CraftID;
        }

        static compareInstance(data: Craft, rdata: Craft): boolean {
           return data.UID == rdata.UID && data.CraftID == rdata.CraftID;
        }
    }

    export interface DataChipStoreInterface {
        UID: number;
        StoreID: number;
        HaveCount: number;
        LastResetAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class DataChipStore implements DataChipStoreInterface {
        constructor (data?: DataChipStore) {
            if (data) {
                this.UID = data.UID;
                this.StoreID = data.StoreID;
                this.HaveCount = data.HaveCount;
                this.LastResetAt = data.LastResetAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        StoreID: number = 0;
        HaveCount: number = 0;
        LastResetAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: DataChipStore, UID: number, StoreID: number): boolean {
           return data.UID == UID && data.StoreID == StoreID;
        }

        static compareInstance(data: DataChipStore, rdata: DataChipStore): boolean {
           return data.UID == rdata.UID && data.StoreID == rdata.StoreID;
        }
    }

    export interface EntitlementInterface {
        UID: number;
        EntitlementID: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Entitlement implements EntitlementInterface {
        constructor (data?: Entitlement) {
            if (data) {
                this.UID = data.UID;
                this.EntitlementID = data.EntitlementID;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
            }
        }

        UID: number = 0;
        EntitlementID: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;

        static compareKey(data: Entitlement, UID: number, EntitlementID: number): boolean {
           return data.UID == UID && data.EntitlementID == EntitlementID;
        }

        static compareInstance(data: Entitlement, rdata: Entitlement): boolean {
           return data.UID == rdata.UID && data.EntitlementID == rdata.EntitlementID;
        }
    }

    export interface EventStoreInterface {
        UID: number;
        StoreID: number;
        BuyCount: number;
        NextResetAt: number;
        ExpireAt: number;
        SeasonNum: number;
        IsExpire: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class EventStore implements EventStoreInterface {
        constructor (data?: EventStore) {
            if (data) {
                this.UID = data.UID;
                this.StoreID = data.StoreID;
                this.BuyCount = data.BuyCount;
                this.NextResetAt = data.NextResetAt;
                this.ExpireAt = data.ExpireAt;
                this.SeasonNum = data.SeasonNum;
                this.IsExpire = data.IsExpire;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        StoreID: number = 0;
        BuyCount: number = 0;
        NextResetAt: number = 0;
        ExpireAt: number = 0;
        SeasonNum: number = 0;
        IsExpire: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: EventStore, UID: number, StoreID: number): boolean {
           return data.UID == UID && data.StoreID == StoreID;
        }

        static compareInstance(data: EventStore, rdata: EventStore): boolean {
           return data.UID == rdata.UID && data.StoreID == rdata.StoreID;
        }
    }

    export interface ExpressionInterface {
        UID: number;
        ExpressionID: number;
        ExpressionType: EExpressionType;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Expression implements ExpressionInterface {
        constructor (data?: Expression) {
            if (data) {
                this.UID = data.UID;
                this.ExpressionID = data.ExpressionID;
                this.ExpressionType = data.ExpressionType;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        ExpressionID: number = 0;
        ExpressionType: EExpressionType = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Expression, UID: number, ExpressionID: number): boolean {
           return data.UID == UID && data.ExpressionID == ExpressionID;
        }

        static compareInstance(data: Expression, rdata: Expression): boolean {
           return data.UID == rdata.UID && data.ExpressionID == rdata.ExpressionID;
        }
    }

    export interface ExpressionPresetInterface {
        UID: number;
        HeroID: number;
        Preset: number[];
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class ExpressionPreset implements ExpressionPresetInterface {
        constructor (data?: ExpressionPreset) {
            if (data) {
                this.UID = data.UID;
                this.HeroID = data.HeroID;
                this.Preset.push(...data.Preset);
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        HeroID: number = 0;
        Preset: number[] = [];
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: ExpressionPreset, UID: number, HeroID: number): boolean {
           return data.UID == UID && data.HeroID == HeroID;
        }

        static compareInstance(data: ExpressionPreset, rdata: ExpressionPreset): boolean {
           return data.UID == rdata.UID && data.HeroID == rdata.HeroID;
        }
    }

    export interface FriendInterface {
        UID: number;
        FriendUID: number;
        IsDeleted: boolean;
        FriendMemberNo: number;
        FriendNick: string;
    }

    export class Friend implements FriendInterface {
        constructor (data?: Friend) {
            if (data) {
                this.UID = data.UID;
                this.FriendUID = data.FriendUID;
                this.IsDeleted = data.IsDeleted;
                this.FriendMemberNo = data.FriendMemberNo;
                this.FriendNick = data.FriendNick;
            }
        }

        UID: number = 0;
        FriendUID: number = 0;
        IsDeleted: boolean = false;
        FriendMemberNo: number = 0;
        FriendNick: string = '';

        static compareKey(data: Friend, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: Friend, rdata: Friend): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface GlitchStoreInterface {
        UID: number;
        StoreID: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class GlitchStore implements GlitchStoreInterface {
        constructor (data?: GlitchStore) {
            if (data) {
                this.UID = data.UID;
                this.StoreID = data.StoreID;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        StoreID: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: GlitchStore, UID: number, StoreID: number): boolean {
           return data.UID == UID && data.StoreID == StoreID;
        }

        static compareInstance(data: GlitchStore, rdata: GlitchStore): boolean {
           return data.UID == rdata.UID && data.StoreID == rdata.StoreID;
        }
    }

    export interface SilverMedalStoreInterface {
        UID: number;
        StoreID: number;
        BuyCount: number;
        NextResetAt: number;
        SeasonNum: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class SilverMedalStore implements SilverMedalStoreInterface {
        constructor (data?: SilverMedalStore) {
            if (data) {
                this.UID = data.UID;
                this.StoreID = data.StoreID;
                this.BuyCount = data.BuyCount;
                this.NextResetAt = data.NextResetAt;
                this.SeasonNum = data.SeasonNum;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        StoreID: number = 0;
        BuyCount: number = 0;
        NextResetAt: number = 0;
        SeasonNum: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: SilverMedalStore, UID: number, StoreID: number): boolean {
           return data.UID == UID && data.StoreID == StoreID;
        }

        static compareInstance(data: SilverMedalStore, rdata: SilverMedalStore): boolean {
           return data.UID == rdata.UID && data.StoreID == rdata.StoreID;
        }
    }

    export interface GuideMissionInterface {
        UID: number;
        GuideMissionCategory: EGuideMissionCategory;
        MissionID: number;
        IsCompleted: boolean;
        IsRewarded: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class GuideMission implements GuideMissionInterface {
        constructor (data?: GuideMission) {
            if (data) {
                this.UID = data.UID;
                this.GuideMissionCategory = data.GuideMissionCategory;
                this.MissionID = data.MissionID;
                this.IsCompleted = data.IsCompleted;
                this.IsRewarded = data.IsRewarded;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        GuideMissionCategory: EGuideMissionCategory = 0;
        MissionID: number = 0;
        IsCompleted: boolean = false;
        IsRewarded: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: GuideMission, UID: number, GuideMissionCategory: EGuideMissionCategory): boolean {
           return data.UID == UID && data.GuideMissionCategory == GuideMissionCategory;
        }

        static compareInstance(data: GuideMission, rdata: GuideMission): boolean {
           return data.UID == rdata.UID && data.GuideMissionCategory == rdata.GuideMissionCategory;
        }
    }

    export interface GuideMissionProgressRewardInterface {
        UID: number;
        LastReceiveID: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class GuideMissionProgressReward implements GuideMissionProgressRewardInterface {
        constructor (data?: GuideMissionProgressReward) {
            if (data) {
                this.UID = data.UID;
                this.LastReceiveID = data.LastReceiveID;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        LastReceiveID: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: GuideMissionProgressReward, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: GuideMissionProgressReward, rdata: GuideMissionProgressReward): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface HeroInterface {
        UID: number;
        HeroID: number;
        BattleLevel: number;
        BattleExp: number;
        RewardedLevel: number;
        LeftEyeHexColor: string;
        RightEyeHexColor: string;
        HairSkinID: number;
        BodySkinID: number;
        HeadID: number;
        HeadOffset: string;
        HeadRotate: string;
        FaceID: number;
        FaceOffset: string;
        FaceRotate: string;
        BackID: number;
        BackOffset: string;
        BackRotate: string;
        PelvisID: number;
        PelvisOffset: string;
        PelvisRotate: string;
        WeaponID: number;
        WinPoseID: number;
        ExpireAt: number;
        AddPresetCount: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Hero implements HeroInterface {
        constructor (data?: Hero) {
            if (data) {
                this.UID = data.UID;
                this.HeroID = data.HeroID;
                this.BattleLevel = data.BattleLevel;
                this.BattleExp = data.BattleExp;
                this.RewardedLevel = data.RewardedLevel;
                this.LeftEyeHexColor = data.LeftEyeHexColor;
                this.RightEyeHexColor = data.RightEyeHexColor;
                this.HairSkinID = data.HairSkinID;
                this.BodySkinID = data.BodySkinID;
                this.HeadID = data.HeadID;
                this.HeadOffset = data.HeadOffset;
                this.HeadRotate = data.HeadRotate;
                this.FaceID = data.FaceID;
                this.FaceOffset = data.FaceOffset;
                this.FaceRotate = data.FaceRotate;
                this.BackID = data.BackID;
                this.BackOffset = data.BackOffset;
                this.BackRotate = data.BackRotate;
                this.PelvisID = data.PelvisID;
                this.PelvisOffset = data.PelvisOffset;
                this.PelvisRotate = data.PelvisRotate;
                this.WeaponID = data.WeaponID;
                this.WinPoseID = data.WinPoseID;
                this.ExpireAt = data.ExpireAt;
                this.AddPresetCount = data.AddPresetCount;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        HeroID: number = 0;
        BattleLevel: number = 0;
        BattleExp: number = 0;
        RewardedLevel: number = 0;
        LeftEyeHexColor: string = '';
        RightEyeHexColor: string = '';
        HairSkinID: number = 0;
        BodySkinID: number = 0;
        HeadID: number = 0;
        HeadOffset: string = '';
        HeadRotate: string = '';
        FaceID: number = 0;
        FaceOffset: string = '';
        FaceRotate: string = '';
        BackID: number = 0;
        BackOffset: string = '';
        BackRotate: string = '';
        PelvisID: number = 0;
        PelvisOffset: string = '';
        PelvisRotate: string = '';
        WeaponID: number = 0;
        WinPoseID: number = 0;
        ExpireAt: number = 0;
        AddPresetCount: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Hero, UID: number, HeroID: number): boolean {
           return data.UID == UID && data.HeroID == HeroID;
        }

        static compareInstance(data: Hero, rdata: Hero): boolean {
           return data.UID == rdata.UID && data.HeroID == rdata.HeroID;
        }
    }

    export interface HeroSkinInterface {
        UID: number;
        SkinID: number;
        HexColor1: string;
        HexColor2: string;
        HexColor3: string;
        HexColor4: string;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class HeroSkin implements HeroSkinInterface {
        constructor (data?: HeroSkin) {
            if (data) {
                this.UID = data.UID;
                this.SkinID = data.SkinID;
                this.HexColor1 = data.HexColor1;
                this.HexColor2 = data.HexColor2;
                this.HexColor3 = data.HexColor3;
                this.HexColor4 = data.HexColor4;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        SkinID: number = 0;
        HexColor1: string = '';
        HexColor2: string = '';
        HexColor3: string = '';
        HexColor4: string = '';
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: HeroSkin, UID: number, SkinID: number): boolean {
           return data.UID == UID && data.SkinID == SkinID;
        }

        static compareInstance(data: HeroSkin, rdata: HeroSkin): boolean {
           return data.UID == rdata.UID && data.SkinID == rdata.SkinID;
        }
    }

    export interface HeroSkinPresetInterface {
        UID: number;
        HeroID: number;
        SlotID: number;
        PresetData: string;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class HeroSkinPreset implements HeroSkinPresetInterface {
        constructor (data?: HeroSkinPreset) {
            if (data) {
                this.UID = data.UID;
                this.HeroID = data.HeroID;
                this.SlotID = data.SlotID;
                this.PresetData = data.PresetData;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        HeroID: number = 0;
        SlotID: number = 0;
        PresetData: string = '';
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: HeroSkinPreset, UID: number, HeroID: number, SlotID: number): boolean {
           return data.UID == UID && data.HeroID == HeroID && data.SlotID == SlotID;
        }

        static compareInstance(data: HeroSkinPreset, rdata: HeroSkinPreset): boolean {
           return data.UID == rdata.UID && data.HeroID == rdata.HeroID && data.SlotID == rdata.SlotID;
        }
    }

    export interface IncubationInterface {
        UID: number;
        IncubatorID: number;
        IncubateCount: number;
        PetEggID: number;
        IncubationEndAt: number;
        IsDeleted: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Incubation implements IncubationInterface {
        constructor (data?: Incubation) {
            if (data) {
                this.UID = data.UID;
                this.IncubatorID = data.IncubatorID;
                this.IncubateCount = data.IncubateCount;
                this.PetEggID = data.PetEggID;
                this.IncubationEndAt = data.IncubationEndAt;
                this.IsDeleted = data.IsDeleted;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        IncubatorID: number = 0;
        IncubateCount: number = 0;
        PetEggID: number = 0;
        IncubationEndAt: number = 0;
        IsDeleted: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Incubation, UID: number, IncubatorID: number): boolean {
           return data.UID == UID && data.IncubatorID == IncubatorID;
        }

        static compareInstance(data: Incubation, rdata: Incubation): boolean {
           return data.UID == rdata.UID && data.IncubatorID == rdata.IncubatorID;
        }
    }

    export interface InstantGuideInterface {
        UID: number;
        InstantGuideList: number[];
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class InstantGuide implements InstantGuideInterface {
        constructor (data?: InstantGuide) {
            if (data) {
                this.UID = data.UID;
                this.InstantGuideList.push(...data.InstantGuideList);
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        InstantGuideList: number[] = [];
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: InstantGuide, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: InstantGuide, rdata: InstantGuide): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface InventoryInterface {
        UID: number;
        ItemID: number;
        Count: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Inventory implements InventoryInterface {
        constructor (data?: Inventory) {
            if (data) {
                this.UID = data.UID;
                this.ItemID = data.ItemID;
                this.Count = data.Count;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.ItemType = data.ItemType;
                this.ItemNameStringWithID = data.ItemNameStringWithID;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        ItemID: number = 0;
        Count: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        ItemType?: EItemType = 0;
        ItemNameStringWithID?: string = '';
        isChanged?: boolean = false;

        static compareKey(data: Inventory, UID: number, ItemID: number): boolean {
           return data.UID == UID && data.ItemID == ItemID;
        }

        static compareInstance(data: Inventory, rdata: Inventory): boolean {
           return data.UID == rdata.UID && data.ItemID == rdata.ItemID;
        }
    }

    export interface MailInterface {
        MailID: number;
        UID: number;
        MailType: EMailType;
        State: EMailStateType;
        IsBM: boolean;
        Title: string;
        Message: string;
        RewardList: Models.MailReward[];
        ExpireAt: number;
        ReceiveAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Mail implements MailInterface {
        constructor (data?: Mail) {
            if (data) {
                this.MailID = data.MailID;
                this.UID = data.UID;
                this.MailType = data.MailType;
                this.State = data.State;
                this.IsBM = data.IsBM;
                this.Title = data.Title;
                this.Message = data.Message;
                this.RewardList.push(...data.RewardList);
                this.ExpireAt = data.ExpireAt;
                this.ReceiveAt = data.ReceiveAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        MailID: number = 0;
        UID: number = 0;
        MailType: EMailType = 0;
        State: EMailStateType = 0;
        IsBM: boolean = false;
        Title: string = '';
        Message: string = '';
        RewardList: Models.MailReward[] = [];
        ExpireAt: number = 0;
        ReceiveAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Mail, MailID: number): boolean {
           return data.MailID == MailID;
        }

        static compareInstance(data: Mail, rdata: Mail): boolean {
           return data.MailID == rdata.MailID;
        }
    }

    export interface MailWithUserInterface {
        MailID: number;
        MemberNo: number;
        UID: number;
        Nick: string;
        MailType: EMailType;
        State: EMailStateType;
        IsBM: boolean;
        Title: string;
        Message: string;
        RewardList: Models.MailReward[];
        ExpireAt: number;
        ReceiveAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class MailWithUser implements MailWithUserInterface {
        constructor (data?: MailWithUser) {
            if (data) {
                this.MailID = data.MailID;
                this.MemberNo = data.MemberNo;
                this.UID = data.UID;
                this.Nick = data.Nick;
                this.MailType = data.MailType;
                this.State = data.State;
                this.IsBM = data.IsBM;
                this.Title = data.Title;
                this.Message = data.Message;
                this.RewardList.push(...data.RewardList);
                this.ExpireAt = data.ExpireAt;
                this.ReceiveAt = data.ReceiveAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isSelected = data.isSelected;
            }
        }

        MailID: number = 0;
        MemberNo: number = 0;
        UID: number = 0;
        Nick: string = '';
        MailType: EMailType = 0;
        State: EMailStateType = 0;
        IsBM: boolean = false;
        Title: string = '';
        Message: string = '';
        RewardList: Models.MailReward[] = [];
        ExpireAt: number = 0;
        ReceiveAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isSelected?: boolean = false;

        static compareKey(data: MailWithUser, MailID: number): boolean {
           return data.MailID == MailID;
        }

        static compareInstance(data: MailWithUser, rdata: MailWithUser): boolean {
           return data.MailID == rdata.MailID;
        }
    }

    export interface MailRewardInterface {
        RewardType: ERewardType;
        RewardID: number;
        RewardCount: number;
    }

    export class MailReward implements MailRewardInterface {
        constructor (data?: MailReward) {
            if (data) {
                this.RewardType = data.RewardType;
                this.RewardID = data.RewardID;
                this.RewardCount = data.RewardCount;
            }
        }

        RewardType: ERewardType = 0;
        RewardID: number = 0;
        RewardCount: number = 0;
    }

    export interface MailInputItemInterface {
        rewardType: ERewardType;
        id: number;
        name: string;
        count: number;
    }

    export class MailInputItem implements MailInputItemInterface {
        constructor (data?: MailInputItem) {
            if (data) {
                this.rewardType = data.rewardType;
                this.id = data.id;
                this.name = data.name;
                this.count = data.count;
            }
        }

        rewardType: ERewardType = 0;
        id: number = 0;
        name: string = '';
        count: number = 0;

        static compareKey(data: MailInputItem, rewardType: ERewardType, id: number): boolean {
           return data.rewardType == rewardType && data.id == id;
        }

        static compareInstance(data: MailInputItem, rdata: MailInputItem): boolean {
           return data.rewardType == rdata.rewardType && data.id == rdata.id;
        }
    }

    export interface MailInputInterface {
        mailtype: EMailType;
        isBM: boolean;
        title: string;
        message: string;
        expireAt: number;
        items: Models.MailInputItem[];
    }

    export class MailInput implements MailInputInterface {
        constructor (data?: MailInput) {
            if (data) {
                this.mailtype = data.mailtype;
                this.isBM = data.isBM;
                this.title = data.title;
                this.message = data.message;
                this.expireAt = data.expireAt;
                this.items.push(...data.items);
            }
        }

        mailtype: EMailType = 0;
        isBM: boolean = false;
        title: string = '';
        message: string = '';
        expireAt: number = 0;
        items: Models.MailInputItem[] = [];
    }

    export interface MazeRewardBoxInterface {
        UID: number;
        SlotID: number;
        BoxID: number;
        ExpiredAt: number;
        IsOpened: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class MazeRewardBox implements MazeRewardBoxInterface {
        constructor (data?: MazeRewardBox) {
            if (data) {
                this.UID = data.UID;
                this.SlotID = data.SlotID;
                this.BoxID = data.BoxID;
                this.ExpiredAt = data.ExpiredAt;
                this.IsOpened = data.IsOpened;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        SlotID: number = 0;
        BoxID: number = 0;
        ExpiredAt: number = 0;
        IsOpened: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: MazeRewardBox, UID: number, SlotID: number): boolean {
           return data.UID == UID && data.SlotID == SlotID;
        }

        static compareInstance(data: MazeRewardBox, rdata: MazeRewardBox): boolean {
           return data.UID == rdata.UID && data.SlotID == rdata.SlotID;
        }
    }

    export interface NicePlayerInterface {
        UID: number;
        NiceLevel: number;
        NicePoint: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class NicePlayer implements NicePlayerInterface {
        constructor (data?: NicePlayer) {
            if (data) {
                this.UID = data.UID;
                this.NiceLevel = data.NiceLevel;
                this.NicePoint = data.NicePoint;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        NiceLevel: number = 0;
        NicePoint: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: NicePlayer, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: NicePlayer, rdata: NicePlayer): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface PenaltyInterface {
        UID: number;
        ReportState: EPenaltyReportState;
        IsActive: boolean;
        PenaltyGrade: number;
        PenaltyPoint: number;
        PenaltyCount: number;
        PenaltyEndAt: number;
        ClearPenaltyAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Penalty implements PenaltyInterface {
        constructor (data?: Penalty) {
            if (data) {
                this.UID = data.UID;
                this.ReportState = data.ReportState;
                this.IsActive = data.IsActive;
                this.PenaltyGrade = data.PenaltyGrade;
                this.PenaltyPoint = data.PenaltyPoint;
                this.PenaltyCount = data.PenaltyCount;
                this.PenaltyEndAt = data.PenaltyEndAt;
                this.ClearPenaltyAt = data.ClearPenaltyAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        ReportState: EPenaltyReportState = 0;
        IsActive: boolean = false;
        PenaltyGrade: number = 0;
        PenaltyPoint: number = 0;
        PenaltyCount: number = 0;
        PenaltyEndAt: number = 0;
        ClearPenaltyAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Penalty, UID: number, ReportState: EPenaltyReportState): boolean {
           return data.UID == UID && data.ReportState == ReportState;
        }

        static compareInstance(data: Penalty, rdata: Penalty): boolean {
           return data.UID == rdata.UID && data.ReportState == rdata.ReportState;
        }
    }

    export interface PenaltyWithUserInterface {
        UID: number;
        ReportState: EPenaltyReportState;
        MemberNo: number;
        Nick: string;
        IsActive: boolean;
        PenaltyGrade: number;
        PenaltyPoint: number;
        PenaltyCount: number;
        PenaltyEndAt: number;
        ClearPenaltyAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class PenaltyWithUser implements PenaltyWithUserInterface {
        constructor (data?: PenaltyWithUser) {
            if (data) {
                this.UID = data.UID;
                this.ReportState = data.ReportState;
                this.MemberNo = data.MemberNo;
                this.Nick = data.Nick;
                this.IsActive = data.IsActive;
                this.PenaltyGrade = data.PenaltyGrade;
                this.PenaltyPoint = data.PenaltyPoint;
                this.PenaltyCount = data.PenaltyCount;
                this.PenaltyEndAt = data.PenaltyEndAt;
                this.ClearPenaltyAt = data.ClearPenaltyAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        ReportState: EPenaltyReportState = 0;
        MemberNo: number = 0;
        Nick: string = '';
        IsActive: boolean = false;
        PenaltyGrade: number = 0;
        PenaltyPoint: number = 0;
        PenaltyCount: number = 0;
        PenaltyEndAt: number = 0;
        ClearPenaltyAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: PenaltyWithUser, UID: number, ReportState: EPenaltyReportState): boolean {
           return data.UID == UID && data.ReportState == ReportState;
        }

        static compareInstance(data: PenaltyWithUser, rdata: PenaltyWithUser): boolean {
           return data.UID == rdata.UID && data.ReportState == rdata.ReportState;
        }
    }

    export interface PetInterface {
        UID: number;
        UniqueID: number;
        PetID: number;
        Ability: string;
        Like: number;
        IsLocked: boolean;
        IsDeleted: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Pet implements PetInterface {
        constructor (data?: Pet) {
            if (data) {
                this.UID = data.UID;
                this.UniqueID = data.UniqueID;
                this.PetID = data.PetID;
                this.Ability = data.Ability;
                this.Like = data.Like;
                this.IsLocked = data.IsLocked;
                this.IsDeleted = data.IsDeleted;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        UniqueID: number = 0;
        PetID: number = 0;
        Ability: string = '';
        Like: number = 0;
        IsLocked: boolean = false;
        IsDeleted: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Pet, UID: number, UniqueID: number): boolean {
           return data.UID == UID && data.UniqueID == UniqueID;
        }

        static compareInstance(data: Pet, rdata: Pet): boolean {
           return data.UID == rdata.UID && data.UniqueID == rdata.UniqueID;
        }
    }

    export interface PlayRecordGoldClashInterface {
        UID: number;
        SeasonID: number;
        HeroID: number;
        Win: number;
        Lose: number;
        Kill: number;
        Death: number;
        Mvp: number;
        AvgGold: number;
        AvgDamage: number;
        AvgHeal: number;
        AvgDamageBlock: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class PlayRecordGoldClash implements PlayRecordGoldClashInterface {
        constructor (data?: PlayRecordGoldClash) {
            if (data) {
                this.UID = data.UID;
                this.SeasonID = data.SeasonID;
                this.HeroID = data.HeroID;
                this.Win = data.Win;
                this.Lose = data.Lose;
                this.Kill = data.Kill;
                this.Death = data.Death;
                this.Mvp = data.Mvp;
                this.AvgGold = data.AvgGold;
                this.AvgDamage = data.AvgDamage;
                this.AvgHeal = data.AvgHeal;
                this.AvgDamageBlock = data.AvgDamageBlock;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        SeasonID: number = 0;
        HeroID: number = 0;
        Win: number = 0;
        Lose: number = 0;
        Kill: number = 0;
        Death: number = 0;
        Mvp: number = 0;
        AvgGold: number = 0;
        AvgDamage: number = 0;
        AvgHeal: number = 0;
        AvgDamageBlock: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: PlayRecordGoldClash, UID: number, SeasonID: number, HeroID: number): boolean {
           return data.UID == UID && data.SeasonID == SeasonID && data.HeroID == HeroID;
        }

        static compareInstance(data: PlayRecordGoldClash, rdata: PlayRecordGoldClash): boolean {
           return data.UID == rdata.UID && data.SeasonID == rdata.SeasonID && data.HeroID == rdata.HeroID;
        }
    }

    export interface PlayRecordRpgInterface {
        UID: number;
        ChapterID: number;
        HeroID: number;
        BossClear: number;
        TopPoint: number;
        ShortestPlayTime: number;
        AvgFragment: number;
        AvgArtifact: number;
        AvgLevel: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class PlayRecordRpg implements PlayRecordRpgInterface {
        constructor (data?: PlayRecordRpg) {
            if (data) {
                this.UID = data.UID;
                this.ChapterID = data.ChapterID;
                this.HeroID = data.HeroID;
                this.BossClear = data.BossClear;
                this.TopPoint = data.TopPoint;
                this.ShortestPlayTime = data.ShortestPlayTime;
                this.AvgFragment = data.AvgFragment;
                this.AvgArtifact = data.AvgArtifact;
                this.AvgLevel = data.AvgLevel;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        ChapterID: number = 0;
        HeroID: number = 0;
        BossClear: number = 0;
        TopPoint: number = 0;
        ShortestPlayTime: number = 0;
        AvgFragment: number = 0;
        AvgArtifact: number = 0;
        AvgLevel: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: PlayRecordRpg, UID: number, ChapterID: number, HeroID: number): boolean {
           return data.UID == UID && data.ChapterID == ChapterID && data.HeroID == HeroID;
        }

        static compareInstance(data: PlayRecordRpg, rdata: PlayRecordRpg): boolean {
           return data.UID == rdata.UID && data.ChapterID == rdata.ChapterID && data.HeroID == rdata.HeroID;
        }
    }

    export interface ProfileInterface {
        UID: number;
        ProfileID: number;
        ProfileType: EProfileType;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Profile implements ProfileInterface {
        constructor (data?: Profile) {
            if (data) {
                this.UID = data.UID;
                this.ProfileID = data.ProfileID;
                this.ProfileType = data.ProfileType;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
            }
        }

        UID: number = 0;
        ProfileID: number = 0;
        ProfileType: EProfileType = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;

        static compareKey(data: Profile, UID: number, ProfileID: number): boolean {
           return data.UID == UID && data.ProfileID == ProfileID;
        }

        static compareInstance(data: Profile, rdata: Profile): boolean {
           return data.UID == rdata.UID && data.ProfileID == rdata.ProfileID;
        }
    }

    export interface RankingRewardInterface {
        UID: number;
        ContentType: EContentsType;
        RewardedAt: Date;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class RankingReward implements RankingRewardInterface {
        constructor (data?: RankingReward) {
            if (data) {
                this.UID = data.UID;
                this.ContentType = data.ContentType;
                this.RewardedAt = data.RewardedAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        ContentType: EContentsType = 0;
        RewardedAt: Date = new Date();
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: RankingReward, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: RankingReward, rdata: RankingReward): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface RpgAttributeInterface {
        UID: number;
        ID: number;
        Level: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class RpgAttribute implements RpgAttributeInterface {
        constructor (data?: RpgAttribute) {
            if (data) {
                this.UID = data.UID;
                this.ID = data.ID;
                this.Level = data.Level;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        ID: number = 0;
        Level: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: RpgAttribute, UID: number, ID: number): boolean {
           return data.UID == UID && data.ID == ID;
        }

        static compareInstance(data: RpgAttribute, rdata: RpgAttribute): boolean {
           return data.UID == rdata.UID && data.ID == rdata.ID;
        }
    }

    export interface SeasonMissionInterface {
        UID: number;
        SeasonPassID: number;
        MissionID: number;
        MissionGroup: EMissionGroup;
        Count: number;
        IsComplete: boolean;
        ResetAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class SeasonMission implements SeasonMissionInterface {
        constructor (data?: SeasonMission) {
            if (data) {
                this.UID = data.UID;
                this.SeasonPassID = data.SeasonPassID;
                this.MissionID = data.MissionID;
                this.MissionGroup = data.MissionGroup;
                this.Count = data.Count;
                this.IsComplete = data.IsComplete;
                this.ResetAt = data.ResetAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        SeasonPassID: number = 0;
        MissionID: number = 0;
        MissionGroup: EMissionGroup = 0;
        Count: number = 0;
        IsComplete: boolean = false;
        ResetAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: SeasonMission, UID: number, SeasonPassID: number, MissionID: number): boolean {
           return data.UID == UID && data.SeasonPassID == SeasonPassID && data.MissionID == MissionID;
        }

        static compareInstance(data: SeasonMission, rdata: SeasonMission): boolean {
           return data.UID == rdata.UID && data.SeasonPassID == rdata.SeasonPassID && data.MissionID == rdata.MissionID;
        }
    }

    export interface SeasonPassInterface {
        UID: number;
        SeasonPassID: number;
        SeasonNum: number;
        IsPaid: boolean;
        Level: number;
        Exp: number;
        RewardState: string;
        ReqDirection: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class SeasonPass implements SeasonPassInterface {
        constructor (data?: SeasonPass) {
            if (data) {
                this.UID = data.UID;
                this.SeasonPassID = data.SeasonPassID;
                this.SeasonNum = data.SeasonNum;
                this.IsPaid = data.IsPaid;
                this.Level = data.Level;
                this.Exp = data.Exp;
                this.RewardState = data.RewardState;
                this.ReqDirection = data.ReqDirection;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        SeasonPassID: number = 0;
        SeasonNum: number = 0;
        IsPaid: boolean = false;
        Level: number = 0;
        Exp: number = 0;
        RewardState: string = '';
        ReqDirection: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: SeasonPass, UID: number, SeasonPassID: number): boolean {
           return data.UID == UID && data.SeasonPassID == SeasonPassID;
        }

        static compareInstance(data: SeasonPass, rdata: SeasonPass): boolean {
           return data.UID == rdata.UID && data.SeasonPassID == rdata.SeasonPassID;
        }
    }

    export interface SpecialLevelInterface {
        UID: number;
        LevelID: number;
        CompletedAt: number;
        BoxID0: number;
        BoxID1: number;
        BoxID2: number;
        BoxID3: number;
        BoxID4: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class SpecialLevel implements SpecialLevelInterface {
        constructor (data?: SpecialLevel) {
            if (data) {
                this.UID = data.UID;
                this.LevelID = data.LevelID;
                this.CompletedAt = data.CompletedAt;
                this.BoxID0 = data.BoxID0;
                this.BoxID1 = data.BoxID1;
                this.BoxID2 = data.BoxID2;
                this.BoxID3 = data.BoxID3;
                this.BoxID4 = data.BoxID4;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        LevelID: number = 0;
        CompletedAt: number = 0;
        BoxID0: number = 0;
        BoxID1: number = 0;
        BoxID2: number = 0;
        BoxID3: number = 0;
        BoxID4: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: SpecialLevel, UID: number, LevelID: number): boolean {
           return data.UID == UID && data.LevelID == LevelID;
        }

        static compareInstance(data: SpecialLevel, rdata: SpecialLevel): boolean {
           return data.UID == rdata.UID && data.LevelID == rdata.LevelID;
        }
    }

    export interface TreasureBoxInterface {
        UID: number;
        BoxList: Models.UserTreasureBox[];
        ExpireAt: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class TreasureBox implements TreasureBoxInterface {
        constructor (data?: TreasureBox) {
            if (data) {
                this.UID = data.UID;
                this.BoxList.push(...data.BoxList);
                this.ExpireAt = data.ExpireAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        BoxList: Models.UserTreasureBox[] = [];
        ExpireAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: TreasureBox, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: TreasureBox, rdata: TreasureBox): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface UserTreasureBoxInterface {
        BoxID: number;
        IsOpened: boolean;
    }

    export class UserTreasureBox implements UserTreasureBoxInterface {
        constructor (data?: UserTreasureBox) {
            if (data) {
                this.BoxID = data.BoxID;
                this.IsOpened = data.IsOpened;
            }
        }

        BoxID: number = 0;
        IsOpened: boolean = false;

        static compareKey(data: UserTreasureBox, BoxID: number): boolean {
           return data.BoxID == BoxID;
        }

        static compareInstance(data: UserTreasureBox, rdata: UserTreasureBox): boolean {
           return data.BoxID == rdata.BoxID;
        }
    }

    export interface UserAccountInterface {
        UID: number;
        WUID: string;
        Nick: string;
        HeroID: number;
        ProfileIconID: number;
        ProfileBGID: number;
        EntitlementID: number;
        PetUniqueID: number;
        PetID: number;
        VehicleID: number;
        LastClearChapterID: number;
        IsGlitchTutorialComplete: boolean;
        IsGoldClashTutorialComplete: boolean;
        LastPosition: string;
        AddArtifactDeckCount: number;
        PenaltyReportCount: number;
        LastPenaltyReportAt: number;
        IntroduceID: number;
        LastLoginAt: number;
        CreateAt: Date;
        UpdateAt: Date;
        IsSignIn: boolean;
        currentLobbyID: string;
    }

    export class UserAccount implements UserAccountInterface {
        constructor (data?: UserAccount) {
            if (data) {
                this.UID = data.UID;
                this.WUID = data.WUID;
                this.Nick = data.Nick;
                this.HeroID = data.HeroID;
                this.ProfileIconID = data.ProfileIconID;
                this.ProfileBGID = data.ProfileBGID;
                this.EntitlementID = data.EntitlementID;
                this.PetUniqueID = data.PetUniqueID;
                this.PetID = data.PetID;
                this.VehicleID = data.VehicleID;
                this.LastClearChapterID = data.LastClearChapterID;
                this.IsGlitchTutorialComplete = data.IsGlitchTutorialComplete;
                this.IsGoldClashTutorialComplete = data.IsGoldClashTutorialComplete;
                this.LastPosition = data.LastPosition;
                this.AddArtifactDeckCount = data.AddArtifactDeckCount;
                this.PenaltyReportCount = data.PenaltyReportCount;
                this.LastPenaltyReportAt = data.LastPenaltyReportAt;
                this.IntroduceID = data.IntroduceID;
                this.LastLoginAt = data.LastLoginAt;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.IsSignIn = data.IsSignIn;
                this.currentLobbyID = data.currentLobbyID;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        WUID: string = '';
        Nick: string = '';
        HeroID: number = 0;
        ProfileIconID: number = 0;
        ProfileBGID: number = 0;
        EntitlementID: number = 0;
        PetUniqueID: number = 0;
        PetID: number = 0;
        VehicleID: number = 0;
        LastClearChapterID: number = 0;
        IsGlitchTutorialComplete: boolean = false;
        IsGoldClashTutorialComplete: boolean = false;
        LastPosition: string = '';
        AddArtifactDeckCount: number = 0;
        PenaltyReportCount: number = 0;
        LastPenaltyReportAt: number = 0;
        IntroduceID: number = 0;
        LastLoginAt: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        IsSignIn: boolean = false;
        currentLobbyID: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: UserAccount, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: UserAccount, rdata: UserAccount): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface UserBlockInterface {
        MemberNo: number;
        UID: number;
        BlockReasonID: number;
        BlockReasonStr: string;
        StartTime: Date;
        EndTime: Date;
    }

    export class UserBlock implements UserBlockInterface {
        constructor (data?: UserBlock) {
            if (data) {
                this.MemberNo = data.MemberNo;
                this.UID = data.UID;
                this.BlockReasonID = data.BlockReasonID;
                this.BlockReasonStr = data.BlockReasonStr;
                this.StartTime = data.StartTime;
                this.EndTime = data.EndTime;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        MemberNo: number = 0;
        UID: number = 0;
        BlockReasonID: number = 0;
        BlockReasonStr: string = '';
        StartTime: Date = new Date();
        EndTime: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: UserBlock, MemberNo: number): boolean {
           return data.MemberNo == MemberNo;
        }

        static compareInstance(data: UserBlock, rdata: UserBlock): boolean {
           return data.MemberNo == rdata.MemberNo;
        }
    }

    export interface UserDeviceInterface {
        UID: number;
        DeviceID: string;
        DeviceType: string;
        DeviceModel: string;
        OS: string;
        MarketCode: EMarketCode;
        Provider: string;
        Platform: EPlatform;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class UserDevice implements UserDeviceInterface {
        constructor (data?: UserDevice) {
            if (data) {
                this.UID = data.UID;
                this.DeviceID = data.DeviceID;
                this.DeviceType = data.DeviceType;
                this.DeviceModel = data.DeviceModel;
                this.OS = data.OS;
                this.MarketCode = data.MarketCode;
                this.Provider = data.Provider;
                this.Platform = data.Platform;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        DeviceID: string = '';
        DeviceType: string = '';
        DeviceModel: string = '';
        OS: string = '';
        MarketCode: EMarketCode = 0;
        Provider: string = '';
        Platform: EPlatform = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: UserDevice, UID: number): boolean {
           return data.UID == UID;
        }

        static compareInstance(data: UserDevice, rdata: UserDevice): boolean {
           return data.UID == rdata.UID;
        }
    }

    export interface WeaponCategoryInterface {
        UID: number;
        WeaponCategoryID: number;
        Exp: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class WeaponCategory implements WeaponCategoryInterface {
        constructor (data?: WeaponCategory) {
            if (data) {
                this.UID = data.UID;
                this.WeaponCategoryID = data.WeaponCategoryID;
                this.Exp = data.Exp;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        WeaponCategoryID: number = 0;
        Exp: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: WeaponCategory, UID: number, WeaponCategoryID: number): boolean {
           return data.UID == UID && data.WeaponCategoryID == WeaponCategoryID;
        }

        static compareInstance(data: WeaponCategory, rdata: WeaponCategory): boolean {
           return data.UID == rdata.UID && data.WeaponCategoryID == rdata.WeaponCategoryID;
        }
    }

    export interface WonderCubeInterface {
        UID: number;
        SlotID: number;
        WonderCubeID: number;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class WonderCube implements WonderCubeInterface {
        constructor (data?: WonderCube) {
            if (data) {
                this.UID = data.UID;
                this.SlotID = data.SlotID;
                this.WonderCubeID = data.WonderCubeID;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        SlotID: number = 0;
        WonderCubeID: number = 0;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: WonderCube, UID: number, SlotID: number): boolean {
           return data.UID == UID && data.SlotID == SlotID;
        }

        static compareInstance(data: WonderCube, rdata: WonderCube): boolean {
           return data.UID == rdata.UID && data.SlotID == rdata.SlotID;
        }
    }

    export interface WonderStoreInterface {
        UID: number;
        StoreID: number;
        TID: string;
        StoveProductID: string;
        BuyCount: number;
        SeasonPassID: number;
        IsSubscription: boolean;
        SubscriptionExpireAt: number;
        BuyAbleStartAt: number;
        BuyAbleEndAt: number;
        NextResetAt: number;
        IsDeleted: boolean;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class WonderStore implements WonderStoreInterface {
        constructor (data?: WonderStore) {
            if (data) {
                this.UID = data.UID;
                this.StoreID = data.StoreID;
                this.TID = data.TID;
                this.StoveProductID = data.StoveProductID;
                this.BuyCount = data.BuyCount;
                this.SeasonPassID = data.SeasonPassID;
                this.IsSubscription = data.IsSubscription;
                this.SubscriptionExpireAt = data.SubscriptionExpireAt;
                this.BuyAbleStartAt = data.BuyAbleStartAt;
                this.BuyAbleEndAt = data.BuyAbleEndAt;
                this.NextResetAt = data.NextResetAt;
                this.IsDeleted = data.IsDeleted;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
                this.isChanged = data.isChanged;
            }
        }

        UID: number = 0;
        StoreID: number = 0;
        TID: string = '';
        StoveProductID: string = '';
        BuyCount: number = 0;
        SeasonPassID: number = 0;
        IsSubscription: boolean = false;
        SubscriptionExpireAt: number = 0;
        BuyAbleStartAt: number = 0;
        BuyAbleEndAt: number = 0;
        NextResetAt: number = 0;
        IsDeleted: boolean = false;
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();
        isChanged?: boolean = false;

        static compareKey(data: WonderStore, UID: number, StoreID: number): boolean {
           return data.UID == UID && data.StoreID == StoreID;
        }

        static compareInstance(data: WonderStore, rdata: WonderStore): boolean {
           return data.UID == rdata.UID && data.StoreID == rdata.StoreID;
        }
    }

    export interface BillingInterface {
        UID: number;
        TID: string;
        OriginalTID: string;
        MemberNo: string;
        NickNameNo: string;
        NotiType: string;
        TxnTime: number;
        MarketCode: string;
        ProductID: string;
        SubStatusCode: string;
        ExpireTime: number;
        Price: number;
        IsRewarded: boolean;
        BillingData: string;
        CreateAt: Date;
        UpdateAt: Date;
    }

    export class Billing implements BillingInterface {
        constructor (data?: Billing) {
            if (data) {
                this.UID = data.UID;
                this.TID = data.TID;
                this.OriginalTID = data.OriginalTID;
                this.MemberNo = data.MemberNo;
                this.NickNameNo = data.NickNameNo;
                this.NotiType = data.NotiType;
                this.TxnTime = data.TxnTime;
                this.MarketCode = data.MarketCode;
                this.ProductID = data.ProductID;
                this.SubStatusCode = data.SubStatusCode;
                this.ExpireTime = data.ExpireTime;
                this.Price = data.Price;
                this.IsRewarded = data.IsRewarded;
                this.BillingData = data.BillingData;
                this.CreateAt = data.CreateAt;
                this.UpdateAt = data.UpdateAt;
            }
        }

        UID: number = 0;
        TID: string = '';
        OriginalTID: string = '';
        MemberNo: string = '';
        NickNameNo: string = '';
        NotiType: string = '';
        TxnTime: number = 0;
        MarketCode: string = '';
        ProductID: string = '';
        SubStatusCode: string = '';
        ExpireTime: number = 0;
        Price: number = 0;
        IsRewarded: boolean = false;
        BillingData: string = '';
        CreateAt: Date = new Date();
        UpdateAt: Date = new Date();

        static compareKey(data: Billing, UID: number, TID: string): boolean {
           return data.UID == UID && data.TID == TID;
        }

        static compareInstance(data: Billing, rdata: Billing): boolean {
           return data.UID == rdata.UID && data.TID == rdata.TID;
        }
    }

    export interface UserMenuInterface {
        name: string;
        path: string;
        parameters: Map<string,string>;
        children: Models.UserMenu[];
        opened: boolean;
        active: boolean;
    }

    export class UserMenu implements UserMenuInterface {
        constructor (data?: UserMenu) {
            if (data) {
                this.name = data.name;
                this.path = data.path;
                if (null != data.parameters && 'object' == typeof data.parameters) {
                    const keys = Object.keys(data.parameters);
                    for (let i = 0; i < keys.length; i++) {
                        this.parameters.set(keys[i], data.parameters[keys[i]]);
                    }
                }
                this.children.push(...data.children);
                this.opened = data.opened;
                this.active = data.active;
            }
        }

        name: string = '';
        path: string = '';
        parameters: Map<string,string> = new Map<string,string>();
        children: Models.UserMenu[] = [];
        opened: boolean = false;
        active: boolean = false;

        static compareKey(data: UserMenu, name: string): boolean {
           return data.name == name;
        }

        static compareInstance(data: UserMenu, rdata: UserMenu): boolean {
           return data.name == rdata.name;
        }
    }

    export interface NavMenuInterface extends UserMenuInterface {
        name: string;
        path: string;
        parameters: Map<string,string>;
        policyName: string;
        roles: string[];
        children: Models.NavMenu[];
    }

    export class NavMenu extends UserMenu implements NavMenuInterface {
        constructor (data?: NavMenu) {
            super();

            if (data) {
                this.name = data.name;
                this.path = data.path;
                if (null != data.parameters && 'object' == typeof data.parameters) {
                    const keys = Object.keys(data.parameters);
                    for (let i = 0; i < keys.length; i++) {
                        this.parameters.set(keys[i], data.parameters[keys[i]]);
                    }
                }
                this.policyName = data.policyName;
                this.roles.push(...data.roles);
                this.children.push(...data.children);
            }
        }

        name: string = '';
        path: string = '';
        parameters: Map<string,string> = new Map<string,string>();
        policyName: string = '';
        roles: string[] = [];
        children: Models.NavMenu[] = [];

        static compareKey(data: NavMenu, name: string): boolean {
           return data.name == name;
        }

        static compareInstance(data: NavMenu, rdata: NavMenu): boolean {
           return data.name == rdata.name;
        }
    }

    export interface SignInUserInterface {
        signinId: string;
        name: string;
        latestSignin: Date;
        latestSignout: Date;
        latestChangePW: Date;
        email: string;
        emailConfirmId: string;
        isEmailConfirmed: boolean;
        provider: Defines.OAuthProvider;
        updatedTime: Date;
        menus: Models.UserMenu[];
        menusLinear: Models.UserMenu[];
        roles: string[];
    }

    export class SignInUser implements SignInUserInterface {
        constructor (data?: SignInUser) {
            if (data) {
                this.signinId = data.signinId;
                this.name = data.name;
                this.latestSignin = data.latestSignin;
                this.latestSignout = data.latestSignout;
                this.latestChangePW = data.latestChangePW;
                this.email = data.email;
                this.emailConfirmId = data.emailConfirmId;
                this.isEmailConfirmed = data.isEmailConfirmed;
                this.provider = data.provider;
                this.updatedTime = data.updatedTime;
                this.menus.push(...data.menus);
                this.menusLinear.push(...data.menusLinear);
                this.roles.push(...data.roles);
            }
        }

        signinId: string = NIL;
        name: string = '';
        latestSignin: Date = new Date();
        latestSignout: Date = new Date();
        latestChangePW: Date = new Date();
        email: string = '';
        emailConfirmId: string = '';
        isEmailConfirmed: boolean = false;
        provider: Defines.OAuthProvider = 0;
        updatedTime: Date = new Date();
        menus: Models.UserMenu[] = [];
        menusLinear: Models.UserMenu[] = [];
        roles: string[] = [];

        static compareKey(data: SignInUser, signinId: string): boolean {
           return data.signinId == signinId;
        }

        static compareInstance(data: SignInUser, rdata: SignInUser): boolean {
           return data.signinId == rdata.signinId;
        }
    }

    export interface UserJobInterface {
        id: string;
        userId: string;
        jobType: Defines.UserJobType;
        jobCount: number;
        succeededCount: number;
        failedCount: number;
        message: string;
        startTime: Date;
        updateTime: Date;
    }

    export class UserJob implements UserJobInterface {
        constructor (data?: UserJob) {
            if (data) {
                this.id = data.id;
                this.userId = data.userId;
                this.jobType = data.jobType;
                this.jobCount = data.jobCount;
                this.succeededCount = data.succeededCount;
                this.failedCount = data.failedCount;
                this.message = data.message;
                this.startTime = data.startTime;
                this.updateTime = data.updateTime;
                this.isOpenMessage = data.isOpenMessage;
            }
        }

        id: string = NIL;
        userId: string = NIL;
        jobType: Defines.UserJobType = 0;
        jobCount: number = 0;
        succeededCount: number = 0;
        failedCount: number = 0;
        message: string = '';
        startTime: Date = new Date();
        updateTime: Date = new Date();
        isOpenMessage?: boolean = false;

        static compareKey(data: UserJob, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: UserJob, rdata: UserJob): boolean {
           return data.id == rdata.id;
        }
    }

    export interface RoleInterface {
        id: string;
        name: string;
    }

    export class Role implements RoleInterface {
        constructor (data?: Role) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
            }
        }

        id: string = NIL;
        name: string = '';

        static compareKey(data: Role, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: Role, rdata: Role): boolean {
           return data.id == rdata.id;
        }
    }

    export interface UserRoleInterface {
        id: string;
        userId: string;
        roleId: string;
    }

    export class UserRole implements UserRoleInterface {
        constructor (data?: UserRole) {
            if (data) {
                this.id = data.id;
                this.userId = data.userId;
                this.roleId = data.roleId;
            }
        }

        id: string = NIL;
        userId: string = NIL;
        roleId: string = NIL;

        static compareKey(data: UserRole, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: UserRole, rdata: UserRole): boolean {
           return data.id == rdata.id;
        }
    }

    export interface UserRoleNameInterface {
        id: string;
        userId: string;
        roleId: string;
        name: string;
    }

    export class UserRoleName implements UserRoleNameInterface {
        constructor (data?: UserRoleName) {
            if (data) {
                this.id = data.id;
                this.userId = data.userId;
                this.roleId = data.roleId;
                this.name = data.name;
            }
        }

        id: string = NIL;
        userId: string = NIL;
        roleId: string = NIL;
        name: string = '';

        static compareKey(data: UserRoleName, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: UserRoleName, rdata: UserRoleName): boolean {
           return data.id == rdata.id;
        }
    }

    export interface UserInfoInterface {
        id: string;
        name: string;
        isDeleted: boolean;
        countFailedSignin: number;
        latestSignin: Date;
        latestSignout: Date;
        latestChangePW: Date;
        regTime: Date;
        emails: string[];
        roles: string[];
    }

    export class UserInfo implements UserInfoInterface {
        constructor (data?: UserInfo) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
                this.isDeleted = data.isDeleted;
                this.countFailedSignin = data.countFailedSignin;
                this.latestSignin = data.latestSignin;
                this.latestSignout = data.latestSignout;
                this.latestChangePW = data.latestChangePW;
                this.regTime = data.regTime;
                this.emails.push(...data.emails);
                this.roles.push(...data.roles);
            }
        }

        id: string = NIL;
        name: string = '';
        isDeleted: boolean = false;
        countFailedSignin: number = 0;
        latestSignin: Date = new Date();
        latestSignout: Date = new Date();
        latestChangePW: Date = new Date();
        regTime: Date = new Date();
        emails: string[] = [];
        roles: string[] = [];

        static compareKey(data: UserInfo, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: UserInfo, rdata: UserInfo): boolean {
           return data.id == rdata.id;
        }
    }

    export interface UserInfoForManageInterface {
        id: string;
        name: string;
        isDeleted: boolean;
        countFailedSignin: number;
        latestSignin: Date;
        latestSignout: Date;
        latestChangePW: Date;
        regTime: Date;
        emails: string[];
        roles: string[];
        emailId: string;
        provider: Defines.OAuthProvider;
        emailConfirmId: string;
        isEmailConfirmed: boolean;
    }

    export class UserInfoForManage implements UserInfoForManageInterface {
        constructor (data?: UserInfoForManage) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
                this.isDeleted = data.isDeleted;
                this.countFailedSignin = data.countFailedSignin;
                this.latestSignin = data.latestSignin;
                this.latestSignout = data.latestSignout;
                this.latestChangePW = data.latestChangePW;
                this.regTime = data.regTime;
                this.emails.push(...data.emails);
                this.roles.push(...data.roles);
                this.emailId = data.emailId;
                this.provider = data.provider;
                this.emailConfirmId = data.emailConfirmId;
                this.isEmailConfirmed = data.isEmailConfirmed;
            }
        }

        id: string = NIL;
        name: string = '';
        isDeleted: boolean = false;
        countFailedSignin: number = 0;
        latestSignin: Date = new Date();
        latestSignout: Date = new Date();
        latestChangePW: Date = new Date();
        regTime: Date = new Date();
        emails: string[] = [];
        roles: string[] = [];
        emailId: string = NIL;
        provider: Defines.OAuthProvider = 0;
        emailConfirmId: string = '';
        isEmailConfirmed: boolean = false;

        static compareKey(data: UserInfoForManage, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: UserInfoForManage, rdata: UserInfoForManage): boolean {
           return data.id == rdata.id;
        }
    }

    export interface UserInfoForAddInterface {
        name: string;
        email: string;
        password: string;
        roles: string[];
    }

    export class UserInfoForAdd implements UserInfoForAddInterface {
        constructor (data?: UserInfoForAdd) {
            if (data) {
                this.name = data.name;
                this.email = data.email;
                this.password = data.password;
                this.roles.push(...data.roles);
            }
        }

        name: string = '';
        email: string = '';
        password: string = '';
        roles: string[] = [];
    }

    export interface UserSigninInterface {
        userId: string;
        signinId: string;
        connectionId: string;
        latestUpdate: Date;
        latestActive: Date;
    }

    export class UserSignin implements UserSigninInterface {
        constructor (data?: UserSignin) {
            if (data) {
                this.userId = data.userId;
                this.signinId = data.signinId;
                this.connectionId = data.connectionId;
                this.latestUpdate = data.latestUpdate;
                this.latestActive = data.latestActive;
            }
        }

        userId: string = NIL;
        signinId: string = NIL;
        connectionId: string = '';
        latestUpdate: Date = new Date();
        latestActive: Date = new Date();

        static compareKey(data: UserSignin, userId: string): boolean {
           return data.userId == userId;
        }

        static compareInstance(data: UserSignin, rdata: UserSignin): boolean {
           return data.userId == rdata.userId;
        }
    }

    export interface UserPasswordInterface {
        id: string;
        userId: string;
        password: string;
        regTime: Date;
    }

    export class UserPassword implements UserPasswordInterface {
        constructor (data?: UserPassword) {
            if (data) {
                this.id = data.id;
                this.userId = data.userId;
                this.password = data.password;
                this.regTime = data.regTime;
            }
        }

        id: string = NIL;
        userId: string = NIL;
        password: string = '';
        regTime: Date = new Date();

        static compareKey(data: UserPassword, id: string, userId: string, password: string): boolean {
           return data.id == id && data.userId == userId && data.password == password;
        }

        static compareInstance(data: UserPassword, rdata: UserPassword): boolean {
           return data.id == rdata.id && data.userId == rdata.userId && data.password == rdata.password;
        }
    }

    export interface ServiceVersionManagementInfoInterface {
        Version: string;
        Platform: Defines.ServiceVersionManagementPlatform;
        ServerState: Defines.ServiceVersionManagementServerState;
        ClientState: Defines.ServiceVersionManagementClientState;
        CDNInfo: string;
    }

    export class ServiceVersionManagementInfo implements ServiceVersionManagementInfoInterface {
        constructor (data?: ServiceVersionManagementInfo) {
            if (data) {
                this.Version = data.Version;
                this.Platform = data.Platform;
                this.ServerState = data.ServerState;
                this.ClientState = data.ClientState;
                this.CDNInfo = data.CDNInfo;
            }
        }

        Version: string = '';
        Platform: Defines.ServiceVersionManagementPlatform = 0;
        ServerState: Defines.ServiceVersionManagementServerState = 0;
        ClientState: Defines.ServiceVersionManagementClientState = 0;
        CDNInfo: string = '';

        static compareKey(data: ServiceVersionManagementInfo, Version: string, Platform: Defines.ServiceVersionManagementPlatform): boolean {
           return data.Version == Version && data.Platform == Platform;
        }

        static compareInstance(data: ServiceVersionManagementInfo, rdata: ServiceVersionManagementInfo): boolean {
           return data.Version == rdata.Version && data.Platform == rdata.Platform;
        }
    }

    export interface VersionInfoInterface {
        version: string;
        platform: Defines.ServiceVersionManagementPlatform;
        serverState: Defines.ServiceVersionManagementServerState;
        clientState: Defines.ServiceVersionManagementClientState;
        cdnInfo: string;
    }

    export class VersionInfo implements VersionInfoInterface {
        constructor (data?: VersionInfo) {
            if (data) {
                this.version = data.version;
                this.platform = data.platform;
                this.serverState = data.serverState;
                this.clientState = data.clientState;
                this.cdnInfo = data.cdnInfo;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        version: string = '';
        platform: Defines.ServiceVersionManagementPlatform = 0;
        serverState: Defines.ServiceVersionManagementServerState = 0;
        clientState: Defines.ServiceVersionManagementClientState = 0;
        cdnInfo: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: VersionInfo, version: string, platform: Defines.ServiceVersionManagementPlatform): boolean {
           return data.version == version && data.platform == platform;
        }

        static compareInstance(data: VersionInfo, rdata: VersionInfo): boolean {
           return data.version == rdata.version && data.platform == rdata.platform;
        }
    }

    export interface ChattingNoticeInterface {
        id: string;
        message: string;
        noticeType: ENoticeType;
        visibleTime: number;
        visibleCount: number;
        noticeTime: Date;
        regTime: Date;
    }

    export class ChattingNotice implements ChattingNoticeInterface {
        constructor (data?: ChattingNotice) {
            if (data) {
                this.id = data.id;
                this.message = data.message;
                this.noticeType = data.noticeType;
                this.visibleTime = data.visibleTime;
                this.visibleCount = data.visibleCount;
                this.noticeTime = data.noticeTime;
                this.regTime = data.regTime;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        id: string = NIL;
        message: string = '';
        noticeType: ENoticeType = 0;
        visibleTime: number = 0;
        visibleCount: number = 0;
        noticeTime: Date = new Date();
        regTime: Date = new Date();
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: ChattingNotice, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: ChattingNotice, rdata: ChattingNotice): boolean {
           return data.id == rdata.id;
        }
    }

    export interface BlockContentInterface {
        packetID: string;
    }

    export class BlockContent implements BlockContentInterface {
        constructor (data?: BlockContent) {
            if (data) {
                this.packetID = data.packetID;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        packetID: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: BlockContent, packetID: string): boolean {
           return data.packetID == packetID;
        }

        static compareInstance(data: BlockContent, rdata: BlockContent): boolean {
           return data.packetID == rdata.packetID;
        }
    }

    export interface WhiteListDataInterface {
        whiteIP: string;
    }

    export class WhiteListData implements WhiteListDataInterface {
        constructor (data?: WhiteListData) {
            if (data) {
                this.whiteIP = data.whiteIP;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        whiteIP: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: WhiteListData, whiteIP: string): boolean {
           return data.whiteIP == whiteIP;
        }

        static compareInstance(data: WhiteListData, rdata: WhiteListData): boolean {
           return data.whiteIP == rdata.whiteIP;
        }
    }

    export interface FirewallInterface {
        id: string;
        method: Defines.FirewallMethod;
        ipAddress: string;
        description: string;
    }

    export class Firewall implements FirewallInterface {
        constructor (data?: Firewall) {
            if (data) {
                this.id = data.id;
                this.method = data.method;
                this.ipAddress = data.ipAddress;
                this.description = data.description;
                this.isNewData = data.isNewData;
                this.isChanged = data.isChanged;
            }
        }

        id: string = NIL;
        method: Defines.FirewallMethod = 0;
        ipAddress: string = '';
        description: string = '';
        isNewData?: boolean = false;
        isChanged?: boolean = false;

        static compareKey(data: Firewall, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: Firewall, rdata: Firewall): boolean {
           return data.id == rdata.id;
        }
    }

    export interface CCUInterface {
        ID: number;
        LobbyID: string;
        CreateAt: Date;
        Total: number;
    }

    export class CCU implements CCUInterface {
        constructor (data?: CCU) {
            if (data) {
                this.ID = data.ID;
                this.LobbyID = data.LobbyID;
                this.CreateAt = data.CreateAt;
                this.Total = data.Total;
            }
        }

        ID: number = 0;
        LobbyID: string = '';
        CreateAt: Date = new Date();
        Total: number = 0;

        static compareKey(data: CCU, ID: number): boolean {
           return data.ID == ID;
        }

        static compareInstance(data: CCU, rdata: CCU): boolean {
           return data.ID == rdata.ID;
        }
    }

    export interface ManagerServerLogInterface {
        logData: string;
        logTime: Date|null;
        logType: string;
    }

    export class ManagerServerLog implements ManagerServerLogInterface {
        constructor (data?: ManagerServerLog) {
            if (data) {
                this.logData = data.logData;
                this.logTime = data.logTime;
                this.logType = data.logType;
            }
        }

        logData: string = '';
        logTime: Date|null = new Date();
        logType: string = '';
    }

    export interface SettingsInterface {
        key: string;
        value: number;
    }

    export class Settings implements SettingsInterface {
        constructor (data?: Settings) {
            if (data) {
                this.key = data.key;
                this.value = data.value;
                this.isChanged = data.isChanged;
            }
        }

        key: string = '';
        value: number = 0;
        isChanged?: boolean = false;

        static compareKey(data: Settings, key: string): boolean {
           return data.key == key;
        }

        static compareInstance(data: Settings, rdata: Settings): boolean {
           return data.key == rdata.key;
        }
    }

    export interface ChattingMessageInterface {
        id: string;
        messageType: Defines.ChattingMessageType;
        message: string;
        sendSigninId: string;
        sendUserName: string;
        sendConnectionId: string;
        targetUserId: string;
        targetConnectionId: string;
        localSendTime: Date|null;
        localReceiveTime: Date|null;
        serverReceiveTime: Date|null;
        serverSendTime: Date|null;
    }

    export class ChattingMessage implements ChattingMessageInterface {
        constructor (data?: ChattingMessage) {
            if (data) {
                this.id = data.id;
                this.messageType = data.messageType;
                this.message = data.message;
                this.sendSigninId = data.sendSigninId;
                this.sendUserName = data.sendUserName;
                this.sendConnectionId = data.sendConnectionId;
                this.targetUserId = data.targetUserId;
                this.targetConnectionId = data.targetConnectionId;
                this.localSendTime = data.localSendTime;
                this.localReceiveTime = data.localReceiveTime;
                this.serverReceiveTime = data.serverReceiveTime;
                this.serverSendTime = data.serverSendTime;
                this.isRead = data.isRead;
            }
        }

        id: string = NIL;
        messageType: Defines.ChattingMessageType = 0;
        message: string = '';
        sendSigninId: string = NIL;
        sendUserName: string = '';
        sendConnectionId: string = '';
        targetUserId: string = NIL;
        targetConnectionId: string = '';
        localSendTime: Date|null = new Date();
        localReceiveTime: Date|null = new Date();
        serverReceiveTime: Date|null = new Date();
        serverSendTime: Date|null = new Date();
        isRead?: boolean = false;

        static compareKey(data: ChattingMessage, id: string): boolean {
           return data.id == id;
        }

        static compareInstance(data: ChattingMessage, rdata: ChattingMessage): boolean {
           return data.id == rdata.id;
        }
    }

    export interface GMLogInterface {
        id: number;
        userId: string;
        type: Defines.GMLogType;
        methodId: number;
        urlId: number;
        errorId: number;
        userAgentId: number;
        message: string;
        remoteAddress: string;
        regTime: Date;
    }

    export class GMLog implements GMLogInterface {
        constructor (data?: GMLog) {
            if (data) {
                this.id = data.id;
                this.userId = data.userId;
                this.type = data.type;
                this.methodId = data.methodId;
                this.urlId = data.urlId;
                this.errorId = data.errorId;
                this.userAgentId = data.userAgentId;
                this.message = data.message;
                this.remoteAddress = data.remoteAddress;
                this.regTime = data.regTime;
            }
        }

        id: number = 0;
        userId: string = '';
        type: Defines.GMLogType = 0;
        methodId: number = 0;
        urlId: number = 0;
        errorId: number = 0;
        userAgentId: number = 0;
        message: string = '';
        remoteAddress: string = '';
        regTime: Date = new Date();

        static compareKey(data: GMLog, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: GMLog, rdata: GMLog): boolean {
           return data.id == rdata.id;
        }
    }

    export interface GMCombinedLogInterface {
        id: number;
        userId: string;
        userName: string;
        type: Defines.GMLogType;
        methodId: number;
        methodName: string;
        urlId: number;
        urlName: string;
        errorId: number;
        errorName: string;
        userAgentId: number;
        userAgentName: string;
        message: string;
        remoteAddress: string;
        regTime: Date;
    }

    export class GMCombinedLog implements GMCombinedLogInterface {
        constructor (data?: GMCombinedLog) {
            if (data) {
                this.id = data.id;
                this.userId = data.userId;
                this.userName = data.userName;
                this.type = data.type;
                this.methodId = data.methodId;
                this.methodName = data.methodName;
                this.urlId = data.urlId;
                this.urlName = data.urlName;
                this.errorId = data.errorId;
                this.errorName = data.errorName;
                this.userAgentId = data.userAgentId;
                this.userAgentName = data.userAgentName;
                this.message = data.message;
                this.remoteAddress = data.remoteAddress;
                this.regTime = data.regTime;
            }
        }

        id: number = 0;
        userId: string = '';
        userName: string = '';
        type: Defines.GMLogType = 0;
        methodId: number = 0;
        methodName: string = '';
        urlId: number = 0;
        urlName: string = '';
        errorId: number = 0;
        errorName: string = '';
        userAgentId: number = 0;
        userAgentName: string = '';
        message: string = '';
        remoteAddress: string = '';
        regTime: Date = new Date();

        static compareKey(data: GMCombinedLog, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: GMCombinedLog, rdata: GMCombinedLog): boolean {
           return data.id == rdata.id;
        }
    }

    export interface GMLogErrorInterface {
        id: number;
        name: string;
    }

    export class GMLogError implements GMLogErrorInterface {
        constructor (data?: GMLogError) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
            }
        }

        id: number = 0;
        name: string = '';

        static compareKey(data: GMLogError, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: GMLogError, rdata: GMLogError): boolean {
           return data.id == rdata.id;
        }
    }

    export interface GMLogMethodInterface {
        id: number;
        name: string;
    }

    export class GMLogMethod implements GMLogMethodInterface {
        constructor (data?: GMLogMethod) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
            }
        }

        id: number = 0;
        name: string = '';

        static compareKey(data: GMLogMethod, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: GMLogMethod, rdata: GMLogMethod): boolean {
           return data.id == rdata.id;
        }
    }

    export interface GMLogURLInterface {
        id: number;
        name: string;
    }

    export class GMLogURL implements GMLogURLInterface {
        constructor (data?: GMLogURL) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
            }
        }

        id: number = 0;
        name: string = '';

        static compareKey(data: GMLogURL, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: GMLogURL, rdata: GMLogURL): boolean {
           return data.id == rdata.id;
        }
    }

    export interface GMLogUserAgentInterface {
        id: number;
        name: string;
    }

    export class GMLogUserAgent implements GMLogUserAgentInterface {
        constructor (data?: GMLogUserAgent) {
            if (data) {
                this.id = data.id;
                this.name = data.name;
            }
        }

        id: number = 0;
        name: string = '';

        static compareKey(data: GMLogUserAgent, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: GMLogUserAgent, rdata: GMLogUserAgent): boolean {
           return data.id == rdata.id;
        }
    }

    export interface BiskitLogInterface {
        logID: number;
        eventGroupID: string;
        eventID: string;
        timestamp: Date;
        sequenceNumber: number;
        stoveMemberNO: number;
        stoveNickNameNO: number;
        accountID: number;
        accountLevel: number;
        accountName: string;
        characterID: number;
        characterLevel: number;
        sessionID: string;
        marketCode: string;
        serverCode: string;
        channelCode: string;
        ipAddress: string;
        deviceID: string;
        deviceType: string;
        deviceModel: string;
        os: string;
        v01: string;
        v02: string;
        v03: string;
        v04: string;
        v05: string;
        v06: string;
        v07: string;
        v08: string;
        v09: string;
        v10: string;
        v11: string;
        v12: string;
        v13: string;
        v14: string;
        v15: string;
        v16: string;
        v17: string;
        v18: string;
        v19: string;
        v20: string;
        v21: string;
        v22: string;
        v23: string;
        v24: string;
        v25: string;
        v26: string;
        v27: string;
        v28: string;
        v29: string;
        v30: string;
    }

    export class BiskitLog implements BiskitLogInterface {
        constructor (data?: BiskitLog) {
            if (data) {
                this.logID = data.logID;
                this.eventGroupID = data.eventGroupID;
                this.eventID = data.eventID;
                this.timestamp = data.timestamp;
                this.sequenceNumber = data.sequenceNumber;
                this.stoveMemberNO = data.stoveMemberNO;
                this.stoveNickNameNO = data.stoveNickNameNO;
                this.accountID = data.accountID;
                this.accountLevel = data.accountLevel;
                this.accountName = data.accountName;
                this.characterID = data.characterID;
                this.characterLevel = data.characterLevel;
                this.sessionID = data.sessionID;
                this.marketCode = data.marketCode;
                this.serverCode = data.serverCode;
                this.channelCode = data.channelCode;
                this.ipAddress = data.ipAddress;
                this.deviceID = data.deviceID;
                this.deviceType = data.deviceType;
                this.deviceModel = data.deviceModel;
                this.os = data.os;
                this.v01 = data.v01;
                this.v02 = data.v02;
                this.v03 = data.v03;
                this.v04 = data.v04;
                this.v05 = data.v05;
                this.v06 = data.v06;
                this.v07 = data.v07;
                this.v08 = data.v08;
                this.v09 = data.v09;
                this.v10 = data.v10;
                this.v11 = data.v11;
                this.v12 = data.v12;
                this.v13 = data.v13;
                this.v14 = data.v14;
                this.v15 = data.v15;
                this.v16 = data.v16;
                this.v17 = data.v17;
                this.v18 = data.v18;
                this.v19 = data.v19;
                this.v20 = data.v20;
                this.v21 = data.v21;
                this.v22 = data.v22;
                this.v23 = data.v23;
                this.v24 = data.v24;
                this.v25 = data.v25;
                this.v26 = data.v26;
                this.v27 = data.v27;
                this.v28 = data.v28;
                this.v29 = data.v29;
                this.v30 = data.v30;
            }
        }

        logID: number = 0;
        eventGroupID: string = '';
        eventID: string = '';
        timestamp: Date = new Date();
        sequenceNumber: number = 0;
        stoveMemberNO: number = 0;
        stoveNickNameNO: number = 0;
        accountID: number = 0;
        accountLevel: number = 0;
        accountName: string = '';
        characterID: number = 0;
        characterLevel: number = 0;
        sessionID: string = '';
        marketCode: string = '';
        serverCode: string = '';
        channelCode: string = '';
        ipAddress: string = '';
        deviceID: string = '';
        deviceType: string = '';
        deviceModel: string = '';
        os: string = '';
        v01: string = '';
        v02: string = '';
        v03: string = '';
        v04: string = '';
        v05: string = '';
        v06: string = '';
        v07: string = '';
        v08: string = '';
        v09: string = '';
        v10: string = '';
        v11: string = '';
        v12: string = '';
        v13: string = '';
        v14: string = '';
        v15: string = '';
        v16: string = '';
        v17: string = '';
        v18: string = '';
        v19: string = '';
        v20: string = '';
        v21: string = '';
        v22: string = '';
        v23: string = '';
        v24: string = '';
        v25: string = '';
        v26: string = '';
        v27: string = '';
        v28: string = '';
        v29: string = '';
        v30: string = '';

        static compareKey(data: BiskitLog, logID: number): boolean {
           return data.logID == logID;
        }

        static compareInstance(data: BiskitLog, rdata: BiskitLog): boolean {
           return data.logID == rdata.logID;
        }
    }

    export interface VisitLogInterface {
        id: number;
        data: string;
    }

    export class VisitLog implements VisitLogInterface {
        constructor (data?: VisitLog) {
            if (data) {
                this.id = data.id;
                this.data = data.data;
            }
        }

        id: number = 0;
        data: string = '';

        static compareKey(data: VisitLog, id: number): boolean {
           return data.id == id;
        }

        static compareInstance(data: VisitLog, rdata: VisitLog): boolean {
           return data.id == rdata.id;
        }
    }

}
