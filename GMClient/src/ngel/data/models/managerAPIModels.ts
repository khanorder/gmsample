import { Models } from '@ngeldata/models/index';
import { DataTableModels } from '@ngeldata/tables/model';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngeldata/autoErrors';
import { APIModelInterfaces } from '@ngeldata/models/apiModelInterfaces';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';

export namespace ManagerAPIModels {

    export class ManagerUserInfosResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userInfos: Models.UserInfoForManage[] = [];
    }

    export class ManagerUserInfoParameters {
        userId: string = NIL;
    }

    export class ManagerUserInfoResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userInfo: Models.UserInfo|null = null;
        menus: Models.NavMenu[] = [];
    }

    export class ManagerUserMenusResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        menus: Models.NavMenu[] = [];
    }

    export class ManagerAddUserParameters {
        userInfo: Models.UserInfoForAdd|null = null;
    }

    export class ManagerAddUserResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        userInfo: Models.UserInfo|null = null;
        emailConfirmId: string = NIL;
    }

    export class ManagerDeleteUserParameters {
        userId: string = NIL;
    }

    export class ManagerDeleteUserResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerRestoreUserParameters {
        userId: string = NIL;
    }

    export class ManagerRestoreUserResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerSaveUserRoleParameters {
        userId: string = NIL;
        roles: string[] = [];
    }

    export class ManagerSaveUserRoleResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerResetCountFailedSigninParameters {
        userId: string = NIL;
    }

    export class ManagerResetCountFailedSigninResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerResetLatestSignInParameters {
        userId: string = NIL;
    }

    export class ManagerResetLatestSignInResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerVersionInfosResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        versionInfos: Models.VersionInfo[] = [];
    }

    export class ManagerVersionInfoParameters {
        version: string = '';
        platform?: Defines.ServiceVersionManagementPlatform|null = 0;
    }

    export class ManagerVersionInfoResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        versionInfo: Models.VersionInfo|null = null;
    }

    export class ManagerSaveVersionInfosParameters {
        versionInfos?: Models.VersionInfo[]|null = [];
    }

    export class ManagerSaveVersionInfosResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDeleteVersionInfosParameters {
        versionInfos?: Models.VersionInfo[]|null = [];
    }

    export class ManagerDeleteVersionInfosResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDownloadVersionInfosParameters {
        version: string = '';
        platform?: Defines.ServiceVersionManagementPlatform|null = 0;
    }

    export class ManagerDownloadVersionInfosResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        fileName: string = "";
        contentType: string = "";
        base64Data: string = "";
    }

    export class ManagerBlockContentsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        blockContents: Models.BlockContent[] = [];
    }

    export class ManagerSaveBlockContentsParameters {
        blockContents?: Models.BlockContent[]|null = [];
    }

    export class ManagerSaveBlockContentsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDeleteBlockContentsParameters {
        blockContents?: Models.BlockContent[]|null = [];
    }

    export class ManagerDeleteBlockContentsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerBiskitLogsParameters {
        startDate?: string|null = null;
        endDate?: string|null = null;
        userSearchType: Defines.UserSearchType = 0;
        userSearchValue?: string|null = null;
        eventIdSearchType?: string[]|null = [];
    }

    export class ManagerBiskitLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        biskitLogs: Models.BiskitLog[] = [];
    }

    export class ManagerDownloadGameLogsParameters {
        startTime?: string|null = null;
        endTime?: string|null = null;
        userSearchType: Defines.UserSearchType = 0;
        userSearchValue?: string|null = null;
        gameLogSearchType: Defines.GameLogSearchType = 0;
        gameLogSearchValue?: string|null = null;
        eventIdSearchType?: string[]|null = [];
    }

    export class ManagerDownloadGameLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        fileName: string = "";
        contentType: string = "";
        base64Data: string = "";
    }

    export class ManagerGMCombinedLogsParameters {
        startTime?: string|null = null;
        endTime?: string|null = null;
    }

    export class ManagerGMCombinedLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        gmCombinedLogs: Models.GMCombinedLog[] = [];
    }

    export class ManagerDownloadManagerLogsParameters {
        name?: string|null = null;
        url?: string|null = null;
        startTime?: string|null = null;
        endTime?: string|null = null;
    }

    export class ManagerDownloadManagerLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        fileName: string = "";
        contentType: string = "";
        base64Data: string = "";
    }

    export class ManagerChatLogsParameters {
        startTime?: string|null = null;
        endTime?: string|null = null;
    }

    export class ManagerChatLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        chatLogs: Models.ChatLog[] = [];
    }

    export class ManagerChangePasswordParameters {
        password: string = '';
        newPassword: string = '';
        newPasswordConfirm: string = '';
    }

    export class ManagerChangePasswordResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerSetRefreshPasswordParameters {
        email: string = '';
    }

    export class ManagerSetRefreshPasswordResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerWhiteListResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        resultData: Models.WhiteList[] = [];
    }

    export class ManagerSaveWhiteListParameters {
        paramData: Models.WhiteList[] = [];
    }

    export class ManagerSaveWhiteListResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDeleteWhiteListParameters {
        paramData: number[] = [];
    }

    export class ManagerDeleteWhiteListResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerBlockIPResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        resultData: Models.BlockIP[] = [];
    }

    export class ManagerSaveBlockIPParameters {
        paramData: Models.BlockIP[] = [];
    }

    export class ManagerSaveBlockIPResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDeleteBlockIPParameters {
        paramData: number[] = [];
    }

    export class ManagerDeleteBlockIPResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerSlangResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        resultData: Models.Slang[] = [];
    }

    export class ManagerSaveSlangParameters {
        paramData: Models.Slang[] = [];
    }

    export class ManagerSaveSlangResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDeleteSlangParameters {
        paramData: number[] = [];
    }

    export class ManagerDeleteSlangResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerFirewallsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        resultData: Models.Firewall[] = [];
    }

    export class ManagerSaveFirewallParameters {
        paramData: Models.Firewall[] = [];
    }

    export class ManagerSaveFirewallResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerDeleteFirewallParameters {
        paramData: string[] = [];
    }

    export class ManagerDeleteFirewallResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        errorIndex: number = 0;
    }

    export class ManagerCCUParameters {
        startTime?: Date|null = new Date();
        endTime?: Date|null = new Date();
    }

    export class ManagerCCUResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        resultData: Models.CCU[] = [];
    }

    export class ManagerManagerServerLogsParameters {
        startTime?: string|null = null;
        endTime?: string|null = null;
    }

    export class ManagerManagerServerLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        managerServerLogs: Models.ManagerServerLog[] = [];
    }

    export class ManagerExpireLogSettingResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        expireLogSetting: Models.Settings|null = null;
    }

    export class ManagerSaveExpireLogSettingParameters {
        value: number = 0;
    }

    export class ManagerSaveExpireLogSettingResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class ManagerSaveVisitParameters {
        session: string = '';
        fp: number = 0;
        deviceType: string = '';
        deviceVendor: string = '';
        deviceModel: string = '';
        agent: string = '';
        browser: string = '';
        browserVersion: string = '';
        engine: string = '';
        engineVersion: string = '';
        os: string = '';
        osVersion: string = '';
        host: string = '';
        parameter: string = '';
        path: string = '';
        title: string = '';
        localTime: Date = new Date();
    }

    export class ManagerSaveVisitResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
    }

}
