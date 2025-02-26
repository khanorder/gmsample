import isEmpty from 'lodash/isEmpty';
import deepmerge from 'deepmerge';
import download from 'downloadjs';
import Strings from 'src/helpers/strings';
import { Helpers } from 'src/helpers/index';
import { Errors } from '@ngeldata/autoErrors';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';
import { ATestAPIModels } from '@ngeldata/models/atestAPIModels';
import { GameAPIModels } from '@ngeldata/models/gameAPIModels';
import { ManagerAPIModels } from '@ngeldata/models/managerAPIModels';

export namespace ManagerAPI {

    export async function UserInfosAsync(): Promise<ManagerAPIModels.ManagerUserInfosResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/UserInfos`;
        const resultResponse = new ManagerAPIModels.ManagerUserInfosResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerUserInfosResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function UserInfoAsync(parameters: ManagerAPIModels.ManagerUserInfoParameters): Promise<ManagerAPIModels.ManagerUserInfoResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/UserInfo`;
        const resultResponse = new ManagerAPIModels.ManagerUserInfoResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerUserInfoResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function UserMenusAsync(): Promise<ManagerAPIModels.ManagerUserMenusResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/UserMenus`;
        const resultResponse = new ManagerAPIModels.ManagerUserMenusResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerUserMenusResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function AddUserAsync(parameters: ManagerAPIModels.ManagerAddUserParameters): Promise<ManagerAPIModels.ManagerAddUserResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/AddUser`;
        const resultResponse = new ManagerAPIModels.ManagerAddUserResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerAddUserResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteUserAsync(parameters: ManagerAPIModels.ManagerDeleteUserParameters): Promise<ManagerAPIModels.ManagerDeleteUserResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteUser`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteUserResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteUserResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function RestoreUserAsync(parameters: ManagerAPIModels.ManagerRestoreUserParameters): Promise<ManagerAPIModels.ManagerRestoreUserResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/RestoreUser`;
        const resultResponse = new ManagerAPIModels.ManagerRestoreUserResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerRestoreUserResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveUserRoleAsync(parameters: ManagerAPIModels.ManagerSaveUserRoleParameters): Promise<ManagerAPIModels.ManagerSaveUserRoleResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveUserRole`;
        const resultResponse = new ManagerAPIModels.ManagerSaveUserRoleResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveUserRoleResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function ResetCountFailedSigninAsync(parameters: ManagerAPIModels.ManagerResetCountFailedSigninParameters): Promise<ManagerAPIModels.ManagerResetCountFailedSigninResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/ResetCountFailedSignin`;
        const resultResponse = new ManagerAPIModels.ManagerResetCountFailedSigninResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerResetCountFailedSigninResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function ResetLatestSignInAsync(parameters: ManagerAPIModels.ManagerResetLatestSignInParameters): Promise<ManagerAPIModels.ManagerResetLatestSignInResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/ResetLatestSignIn`;
        const resultResponse = new ManagerAPIModels.ManagerResetLatestSignInResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerResetLatestSignInResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function VersionInfosAsync(): Promise<ManagerAPIModels.ManagerVersionInfosResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/VersionInfos`;
        const resultResponse = new ManagerAPIModels.ManagerVersionInfosResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerVersionInfosResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function VersionInfoAsync(parameters: ManagerAPIModels.ManagerVersionInfoParameters): Promise<ManagerAPIModels.ManagerVersionInfoResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/VersionInfo`;
        const resultResponse = new ManagerAPIModels.ManagerVersionInfoResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerVersionInfoResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveVersionInfosAsync(parameters: ManagerAPIModels.ManagerSaveVersionInfosParameters): Promise<ManagerAPIModels.ManagerSaveVersionInfosResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveVersionInfos`;
        const resultResponse = new ManagerAPIModels.ManagerSaveVersionInfosResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveVersionInfosResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteVersionInfosAsync(parameters: ManagerAPIModels.ManagerDeleteVersionInfosParameters): Promise<ManagerAPIModels.ManagerDeleteVersionInfosResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteVersionInfos`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteVersionInfosResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteVersionInfosResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DownloadVersionInfosAsync(parameters: ManagerAPIModels.ManagerDownloadVersionInfosParameters): Promise<ManagerAPIModels.ManagerDownloadVersionInfosResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DownloadVersionInfos`;
        const resultResponse = new ManagerAPIModels.ManagerDownloadVersionInfosResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDownloadVersionInfosResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else if (responseData.result && Errors.None == responseData.error) {
                if (isEmpty(responseData.base64Data)) {
                    responseData.error = Errors.DownloadFile_EmptyData;
                    return responseData;
                } else if (isEmpty(responseData.fileName)) {
                    responseData.error = Errors.DownloadFile_EmptyFileName;
                    return responseData;
                } else if (isEmpty(responseData.contentType)) {
                    responseData.error = Errors.DownloadFile_EmptyContentType;
                    return responseData;
                } else {
                    download(Uint8Array.from(Buffer.from(responseData.base64Data, "base64")), responseData.fileName, responseData.contentType);
                }
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function BlockContentsAsync(): Promise<ManagerAPIModels.ManagerBlockContentsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/BlockContents`;
        const resultResponse = new ManagerAPIModels.ManagerBlockContentsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerBlockContentsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveBlockContentsAsync(parameters: ManagerAPIModels.ManagerSaveBlockContentsParameters): Promise<ManagerAPIModels.ManagerSaveBlockContentsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveBlockContents`;
        const resultResponse = new ManagerAPIModels.ManagerSaveBlockContentsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveBlockContentsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteBlockContentsAsync(parameters: ManagerAPIModels.ManagerDeleteBlockContentsParameters): Promise<ManagerAPIModels.ManagerDeleteBlockContentsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteBlockContents`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteBlockContentsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteBlockContentsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function BiskitLogsAsync(parameters: ManagerAPIModels.ManagerBiskitLogsParameters): Promise<ManagerAPIModels.ManagerBiskitLogsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/BiskitLogs`;
        const resultResponse = new ManagerAPIModels.ManagerBiskitLogsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerBiskitLogsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DownloadGameLogsAsync(parameters: ManagerAPIModels.ManagerDownloadGameLogsParameters): Promise<ManagerAPIModels.ManagerDownloadGameLogsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DownloadGameLogs`;
        const resultResponse = new ManagerAPIModels.ManagerDownloadGameLogsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDownloadGameLogsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else if (responseData.result && Errors.None == responseData.error) {
                if (isEmpty(responseData.base64Data)) {
                    responseData.error = Errors.DownloadFile_EmptyData;
                    return responseData;
                } else if (isEmpty(responseData.fileName)) {
                    responseData.error = Errors.DownloadFile_EmptyFileName;
                    return responseData;
                } else if (isEmpty(responseData.contentType)) {
                    responseData.error = Errors.DownloadFile_EmptyContentType;
                    return responseData;
                } else {
                    download(Uint8Array.from(Buffer.from(responseData.base64Data, "base64")), responseData.fileName, responseData.contentType);
                }
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function GMCombinedLogsAsync(parameters: ManagerAPIModels.ManagerGMCombinedLogsParameters): Promise<ManagerAPIModels.ManagerGMCombinedLogsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/GMCombinedLogs`;
        const resultResponse = new ManagerAPIModels.ManagerGMCombinedLogsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerGMCombinedLogsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DownloadManagerLogsAsync(parameters: ManagerAPIModels.ManagerDownloadManagerLogsParameters): Promise<ManagerAPIModels.ManagerDownloadManagerLogsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DownloadManagerLogs`;
        const resultResponse = new ManagerAPIModels.ManagerDownloadManagerLogsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDownloadManagerLogsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else if (responseData.result && Errors.None == responseData.error) {
                if (isEmpty(responseData.base64Data)) {
                    responseData.error = Errors.DownloadFile_EmptyData;
                    return responseData;
                } else if (isEmpty(responseData.fileName)) {
                    responseData.error = Errors.DownloadFile_EmptyFileName;
                    return responseData;
                } else if (isEmpty(responseData.contentType)) {
                    responseData.error = Errors.DownloadFile_EmptyContentType;
                    return responseData;
                } else {
                    download(Uint8Array.from(Buffer.from(responseData.base64Data, "base64")), responseData.fileName, responseData.contentType);
                }
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function ChatLogsAsync(parameters: ManagerAPIModels.ManagerChatLogsParameters): Promise<ManagerAPIModels.ManagerChatLogsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/ChatLogs`;
        const resultResponse = new ManagerAPIModels.ManagerChatLogsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerChatLogsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function ChangePasswordAsync(parameters: ManagerAPIModels.ManagerChangePasswordParameters): Promise<ManagerAPIModels.ManagerChangePasswordResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/ChangePassword`;
        const resultResponse = new ManagerAPIModels.ManagerChangePasswordResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerChangePasswordResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SetRefreshPasswordAsync(parameters: ManagerAPIModels.ManagerSetRefreshPasswordParameters): Promise<ManagerAPIModels.ManagerSetRefreshPasswordResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SetRefreshPassword`;
        const resultResponse = new ManagerAPIModels.ManagerSetRefreshPasswordResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSetRefreshPasswordResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function WhiteListAsync(): Promise<ManagerAPIModels.ManagerWhiteListResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/WhiteList`;
        const resultResponse = new ManagerAPIModels.ManagerWhiteListResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerWhiteListResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveWhiteListAsync(parameters: ManagerAPIModels.ManagerSaveWhiteListParameters): Promise<ManagerAPIModels.ManagerSaveWhiteListResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveWhiteList`;
        const resultResponse = new ManagerAPIModels.ManagerSaveWhiteListResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveWhiteListResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteWhiteListAsync(parameters: ManagerAPIModels.ManagerDeleteWhiteListParameters): Promise<ManagerAPIModels.ManagerDeleteWhiteListResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteWhiteList`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteWhiteListResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteWhiteListResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function BlockIPAsync(): Promise<ManagerAPIModels.ManagerBlockIPResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/BlockIP`;
        const resultResponse = new ManagerAPIModels.ManagerBlockIPResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerBlockIPResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveBlockIPAsync(parameters: ManagerAPIModels.ManagerSaveBlockIPParameters): Promise<ManagerAPIModels.ManagerSaveBlockIPResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveBlockIP`;
        const resultResponse = new ManagerAPIModels.ManagerSaveBlockIPResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveBlockIPResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteBlockIPAsync(parameters: ManagerAPIModels.ManagerDeleteBlockIPParameters): Promise<ManagerAPIModels.ManagerDeleteBlockIPResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteBlockIP`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteBlockIPResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteBlockIPResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SlangAsync(): Promise<ManagerAPIModels.ManagerSlangResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/Slang`;
        const resultResponse = new ManagerAPIModels.ManagerSlangResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSlangResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveSlangAsync(parameters: ManagerAPIModels.ManagerSaveSlangParameters): Promise<ManagerAPIModels.ManagerSaveSlangResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveSlang`;
        const resultResponse = new ManagerAPIModels.ManagerSaveSlangResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveSlangResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteSlangAsync(parameters: ManagerAPIModels.ManagerDeleteSlangParameters): Promise<ManagerAPIModels.ManagerDeleteSlangResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteSlang`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteSlangResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteSlangResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function FirewallsAsync(): Promise<ManagerAPIModels.ManagerFirewallsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/Firewalls`;
        const resultResponse = new ManagerAPIModels.ManagerFirewallsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerFirewallsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveFirewallAsync(parameters: ManagerAPIModels.ManagerSaveFirewallParameters): Promise<ManagerAPIModels.ManagerSaveFirewallResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveFirewall`;
        const resultResponse = new ManagerAPIModels.ManagerSaveFirewallResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveFirewallResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function DeleteFirewallAsync(parameters: ManagerAPIModels.ManagerDeleteFirewallParameters): Promise<ManagerAPIModels.ManagerDeleteFirewallResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/DeleteFirewall`;
        const resultResponse = new ManagerAPIModels.ManagerDeleteFirewallResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerDeleteFirewallResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function CCUAsync(parameters: ManagerAPIModels.ManagerCCUParameters): Promise<ManagerAPIModels.ManagerCCUResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/CCU`;
        const resultResponse = new ManagerAPIModels.ManagerCCUResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerCCUResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function ManagerServerLogsAsync(parameters: ManagerAPIModels.ManagerManagerServerLogsParameters): Promise<ManagerAPIModels.ManagerManagerServerLogsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/ManagerServerLogs`;
        const resultResponse = new ManagerAPIModels.ManagerManagerServerLogsResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerManagerServerLogsResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function ExpireLogSettingAsync(): Promise<ManagerAPIModels.ManagerExpireLogSettingResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/ExpireLogSetting`;
        const resultResponse = new ManagerAPIModels.ManagerExpireLogSettingResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            const parameters = { requestURL: document.location.pathname };
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerExpireLogSettingResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveExpireLogSettingAsync(parameters: ManagerAPIModels.ManagerSaveExpireLogSettingParameters): Promise<ManagerAPIModels.ManagerSaveExpireLogSettingResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveExpireLogSetting`;
        const resultResponse = new ManagerAPIModels.ManagerSaveExpireLogSettingResponses();
        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK') ?? "";
        if (isEmpty(token)) {
            if ("production" != process.env.NODE_ENV)
                console.log(Errors[Errors.Common_NotFoundToken]);

            resultResponse.error = Errors.Common_NotFoundToken;
            return resultResponse;
        }

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                    'Authorization': `Bearer ${token}`,
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveExpireLogSettingResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else if (Errors.Common_NotFoundToken === responseData.error) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SaveVisitAsync(parameters: ManagerAPIModels.ManagerSaveVisitParameters): Promise<ManagerAPIModels.ManagerSaveVisitResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Manages/SaveVisit`;
        const resultResponse = new ManagerAPIModels.ManagerSaveVisitResponses();

        try {
            parameters = deepmerge(parameters, { requestURL: document.location.pathname });
            const encodedDataString = Helpers.encodeMessagePack(parameters);
            const response = await fetch(`${routeURI}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain',
                },
                body: encodedDataString,
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            const messagePack = await response.text();
            const responseData = Helpers.decodeMessagePack<ManagerAPIModels.ManagerSaveVisitResponses>(messagePack);
            if (null == responseData) {
                resultResponse.error = Errors.Common_FailedToDecodeMsgPack;
                return resultResponse;
            } else {
                return responseData;
            }
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

}
