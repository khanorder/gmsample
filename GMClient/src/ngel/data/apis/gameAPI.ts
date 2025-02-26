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

export namespace GameAPI {

    export async function KickUserAsync(parameters: GameAPIModels.GameKickUserParameters): Promise<GameAPIModels.GameKickUserResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/KickUser`;
        const resultResponse = new GameAPIModels.GameKickUserResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameKickUserResponses>(messagePack);
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

    export async function AccountsAsync(parameters: GameAPIModels.GameAccountsParameters): Promise<GameAPIModels.GameAccountsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Accounts`;
        const resultResponse = new GameAPIModels.GameAccountsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameAccountsResponses>(messagePack);
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

    export async function AchievementsAsync(parameters: GameAPIModels.GameAchievementsParameters): Promise<GameAPIModels.GameAchievementsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Achievements`;
        const resultResponse = new GameAPIModels.GameAchievementsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameAchievementsResponses>(messagePack);
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

    export async function SaveAchievementsAsync(parameters: GameAPIModels.GameSaveAchievementsParameters): Promise<GameAPIModels.GameSaveAchievementsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveAchievements`;
        const resultResponse = new GameAPIModels.GameSaveAchievementsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveAchievementsResponses>(messagePack);
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

    export async function DeleteAchievementsAsync(parameters: GameAPIModels.GameDeleteAchievementsParameters): Promise<GameAPIModels.GameDeleteAchievementsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteAchievements`;
        const resultResponse = new GameAPIModels.GameDeleteAchievementsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteAchievementsResponses>(messagePack);
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

    export async function ArtifactsAsync(parameters: GameAPIModels.GameArtifactsParameters): Promise<GameAPIModels.GameArtifactsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Artifacts`;
        const resultResponse = new GameAPIModels.GameArtifactsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameArtifactsResponses>(messagePack);
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

    export async function SaveArtifactAsync(parameters: GameAPIModels.GameSaveArtifactParameters): Promise<GameAPIModels.GameSaveArtifactResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveArtifact`;
        const resultResponse = new GameAPIModels.GameSaveArtifactResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveArtifactResponses>(messagePack);
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

    export async function DeleteArtifactAsync(parameters: GameAPIModels.GameDeleteArtifactParameters): Promise<GameAPIModels.GameDeleteArtifactResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteArtifact`;
        const resultResponse = new GameAPIModels.GameDeleteArtifactResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteArtifactResponses>(messagePack);
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

    export async function ArtifactDecksAsync(parameters: GameAPIModels.GameArtifactDecksParameters): Promise<GameAPIModels.GameArtifactDecksResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/ArtifactDecks`;
        const resultResponse = new GameAPIModels.GameArtifactDecksResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameArtifactDecksResponses>(messagePack);
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

    export async function AssetsAsync(parameters: GameAPIModels.GameAssetsParameters): Promise<GameAPIModels.GameAssetsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Assets`;
        const resultResponse = new GameAPIModels.GameAssetsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameAssetsResponses>(messagePack);
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

    export async function SaveAssetsAsync(parameters: GameAPIModels.GameSaveAssetsParameters): Promise<GameAPIModels.GameSaveAssetsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveAssets`;
        const resultResponse = new GameAPIModels.GameSaveAssetsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveAssetsResponses>(messagePack);
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

    export async function DeleteAssetsAsync(parameters: GameAPIModels.GameDeleteAssetsParameters): Promise<GameAPIModels.GameDeleteAssetsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteAssets`;
        const resultResponse = new GameAPIModels.GameDeleteAssetsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteAssetsResponses>(messagePack);
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

    export async function AttendancesAsync(parameters: GameAPIModels.GameAttendancesParameters): Promise<GameAPIModels.GameAttendancesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Attendances`;
        const resultResponse = new GameAPIModels.GameAttendancesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameAttendancesResponses>(messagePack);
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

    export async function SaveAttendanceAsync(parameters: GameAPIModels.GameSaveAttendanceParameters): Promise<GameAPIModels.GameSaveAttendanceResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveAttendance`;
        const resultResponse = new GameAPIModels.GameSaveAttendanceResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveAttendanceResponses>(messagePack);
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

    export async function EventStoresAsync(parameters: GameAPIModels.GameEventStoresParameters): Promise<GameAPIModels.GameEventStoresResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/EventStores`;
        const resultResponse = new GameAPIModels.GameEventStoresResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameEventStoresResponses>(messagePack);
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

    export async function ExpressionsAsync(parameters: GameAPIModels.GameExpressionsParameters): Promise<GameAPIModels.GameExpressionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Expressions`;
        const resultResponse = new GameAPIModels.GameExpressionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameExpressionsResponses>(messagePack);
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

    export async function ExpressionPresetsAsync(parameters: GameAPIModels.GameExpressionPresetsParameters): Promise<GameAPIModels.GameExpressionPresetsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/ExpressionPresets`;
        const resultResponse = new GameAPIModels.GameExpressionPresetsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameExpressionPresetsResponses>(messagePack);
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

    export async function CollectionsAsync(parameters: GameAPIModels.GameCollectionsParameters): Promise<GameAPIModels.GameCollectionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Collections`;
        const resultResponse = new GameAPIModels.GameCollectionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameCollectionsResponses>(messagePack);
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

    export async function SaveCollectionsAsync(parameters: GameAPIModels.GameSaveCollectionsParameters): Promise<GameAPIModels.GameSaveCollectionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveCollections`;
        const resultResponse = new GameAPIModels.GameSaveCollectionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveCollectionsResponses>(messagePack);
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

    export async function DeleteCollectionsAsync(parameters: GameAPIModels.GameDeleteCollectionsParameters): Promise<GameAPIModels.GameDeleteCollectionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteCollections`;
        const resultResponse = new GameAPIModels.GameDeleteCollectionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteCollectionsResponses>(messagePack);
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

    export async function CraftsAsync(parameters: GameAPIModels.GameCraftsParameters): Promise<GameAPIModels.GameCraftsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Crafts`;
        const resultResponse = new GameAPIModels.GameCraftsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameCraftsResponses>(messagePack);
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

    export async function WonderCubesAsync(parameters: GameAPIModels.GameWonderCubesParameters): Promise<GameAPIModels.GameWonderCubesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/WonderCubes`;
        const resultResponse = new GameAPIModels.GameWonderCubesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameWonderCubesResponses>(messagePack);
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

    export async function SaveWonderCubesAsync(parameters: GameAPIModels.GameSaveWonderCubesParameters): Promise<GameAPIModels.GameSaveWonderCubesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveWonderCubes`;
        const resultResponse = new GameAPIModels.GameSaveWonderCubesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveWonderCubesResponses>(messagePack);
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

    export async function WonderStoresAsync(parameters: GameAPIModels.GameWonderStoresParameters): Promise<GameAPIModels.GameWonderStoresResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/WonderStores`;
        const resultResponse = new GameAPIModels.GameWonderStoresResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameWonderStoresResponses>(messagePack);
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

    export async function SaveWonderStoresAsync(parameters: GameAPIModels.GameSaveWonderStoresParameters): Promise<GameAPIModels.GameSaveWonderStoresResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveWonderStores`;
        const resultResponse = new GameAPIModels.GameSaveWonderStoresResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveWonderStoresResponses>(messagePack);
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

    export async function EntitlementsAsync(parameters: GameAPIModels.GameEntitlementsParameters): Promise<GameAPIModels.GameEntitlementsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Entitlements`;
        const resultResponse = new GameAPIModels.GameEntitlementsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameEntitlementsResponses>(messagePack);
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

    export async function SaveEntitlementsAsync(parameters: GameAPIModels.GameSaveEntitlementsParameters): Promise<GameAPIModels.GameSaveEntitlementsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveEntitlements`;
        const resultResponse = new GameAPIModels.GameSaveEntitlementsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveEntitlementsResponses>(messagePack);
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

    export async function DeleteEntitlementsAsync(parameters: GameAPIModels.GameDeleteEntitlementsParameters): Promise<GameAPIModels.GameDeleteEntitlementsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteEntitlements`;
        const resultResponse = new GameAPIModels.GameDeleteEntitlementsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteEntitlementsResponses>(messagePack);
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

    export async function FriendsAsync(parameters: GameAPIModels.GameFriendsParameters): Promise<GameAPIModels.GameFriendsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Friends`;
        const resultResponse = new GameAPIModels.GameFriendsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameFriendsResponses>(messagePack);
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

    export async function GlitchStoresAsync(parameters: GameAPIModels.GameGlitchStoresParameters): Promise<GameAPIModels.GameGlitchStoresResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/GlitchStores`;
        const resultResponse = new GameAPIModels.GameGlitchStoresResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameGlitchStoresResponses>(messagePack);
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

    export async function GuideMissionsAsync(parameters: GameAPIModels.GameGuideMissionsParameters): Promise<GameAPIModels.GameGuideMissionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/GuideMissions`;
        const resultResponse = new GameAPIModels.GameGuideMissionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameGuideMissionsResponses>(messagePack);
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

    export async function SaveGuideMissionsAsync(parameters: GameAPIModels.GameSaveGuideMissionsParameters): Promise<GameAPIModels.GameSaveGuideMissionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveGuideMissions`;
        const resultResponse = new GameAPIModels.GameSaveGuideMissionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveGuideMissionsResponses>(messagePack);
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

    export async function GuideMissionProgressRewardAsync(parameters: GameAPIModels.GameGuideMissionProgressRewardParameters): Promise<GameAPIModels.GameGuideMissionProgressRewardResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/GuideMissionProgressReward`;
        const resultResponse = new GameAPIModels.GameGuideMissionProgressRewardResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameGuideMissionProgressRewardResponses>(messagePack);
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

    export async function HeroesAsync(parameters: GameAPIModels.GameHeroesParameters): Promise<GameAPIModels.GameHeroesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Heroes`;
        const resultResponse = new GameAPIModels.GameHeroesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameHeroesResponses>(messagePack);
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

    export async function SkinsAsync(parameters: GameAPIModels.GameSkinsParameters): Promise<GameAPIModels.GameSkinsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Skins`;
        const resultResponse = new GameAPIModels.GameSkinsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSkinsResponses>(messagePack);
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

    export async function SkinPresetsAsync(parameters: GameAPIModels.GameSkinPresetsParameters): Promise<GameAPIModels.GameSkinPresetsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SkinPresets`;
        const resultResponse = new GameAPIModels.GameSkinPresetsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSkinPresetsResponses>(messagePack);
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

    export async function InventoriesAsync(parameters: GameAPIModels.GameInventoriesParameters): Promise<GameAPIModels.GameInventoriesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Inventories`;
        const resultResponse = new GameAPIModels.GameInventoriesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameInventoriesResponses>(messagePack);
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

    export async function SaveInventoriesAsync(parameters: GameAPIModels.GameSaveInventoriesParameters): Promise<GameAPIModels.GameSaveInventoriesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveInventories`;
        const resultResponse = new GameAPIModels.GameSaveInventoriesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveInventoriesResponses>(messagePack);
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

    export async function DeleteInventoriesAsync(parameters: GameAPIModels.GameDeleteInventoriesParameters): Promise<GameAPIModels.GameDeleteInventoriesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteInventories`;
        const resultResponse = new GameAPIModels.GameDeleteInventoriesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteInventoriesResponses>(messagePack);
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

    export async function MailsAsync(parameters: GameAPIModels.GameMailsParameters): Promise<GameAPIModels.GameMailsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Mails`;
        const resultResponse = new GameAPIModels.GameMailsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameMailsResponses>(messagePack);
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

    export async function DeleteMailsAsync(parameters: GameAPIModels.GameDeleteMailsParameters): Promise<GameAPIModels.GameDeleteMailsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteMails`;
        const resultResponse = new GameAPIModels.GameDeleteMailsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteMailsResponses>(messagePack);
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

    export async function MazeRewardBoxesAsync(parameters: GameAPIModels.GameMazeRewardBoxesParameters): Promise<GameAPIModels.GameMazeRewardBoxesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/MazeRewardBoxes`;
        const resultResponse = new GameAPIModels.GameMazeRewardBoxesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameMazeRewardBoxesResponses>(messagePack);
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

    export async function UserBillingsAsync(parameters: GameAPIModels.GameUserBillingsParameters): Promise<GameAPIModels.GameUserBillingsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserBillings`;
        const resultResponse = new GameAPIModels.GameUserBillingsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserBillingsResponses>(messagePack);
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

    export async function IncubationsAsync(parameters: GameAPIModels.GameIncubationsParameters): Promise<GameAPIModels.GameIncubationsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Incubations`;
        const resultResponse = new GameAPIModels.GameIncubationsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameIncubationsResponses>(messagePack);
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

    export async function InstantGuidesAsync(parameters: GameAPIModels.GameInstantGuidesParameters): Promise<GameAPIModels.GameInstantGuidesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/InstantGuides`;
        const resultResponse = new GameAPIModels.GameInstantGuidesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameInstantGuidesResponses>(messagePack);
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

    export async function BillingsAsync(parameters: GameAPIModels.GameBillingsParameters): Promise<GameAPIModels.GameBillingsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Billings`;
        const resultResponse = new GameAPIModels.GameBillingsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameBillingsResponses>(messagePack);
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

    export async function NicePlayersAllAsync(): Promise<GameAPIModels.GameNicePlayersAllResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/NicePlayersAll`;
        const resultResponse = new GameAPIModels.GameNicePlayersAllResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameNicePlayersAllResponses>(messagePack);
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

    export async function NicePlayersAsync(parameters: GameAPIModels.GameNicePlayersParameters): Promise<GameAPIModels.GameNicePlayersResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/NicePlayers`;
        const resultResponse = new GameAPIModels.GameNicePlayersResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameNicePlayersResponses>(messagePack);
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

    export async function PenaltiesAllAsync(parameters: GameAPIModels.GamePenaltiesAllParameters): Promise<GameAPIModels.GamePenaltiesAllResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/PenaltiesAll`;
        const resultResponse = new GameAPIModels.GamePenaltiesAllResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GamePenaltiesAllResponses>(messagePack);
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

    export async function PenaltyAsync(parameters: GameAPIModels.GamePenaltyParameters): Promise<GameAPIModels.GamePenaltyResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Penalty`;
        const resultResponse = new GameAPIModels.GamePenaltyResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GamePenaltyResponses>(messagePack);
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

    export async function PenaltiesAsync(parameters: GameAPIModels.GamePenaltiesParameters): Promise<GameAPIModels.GamePenaltiesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Penalties`;
        const resultResponse = new GameAPIModels.GamePenaltiesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GamePenaltiesResponses>(messagePack);
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

    export async function SavePenaltyAsync(parameters: GameAPIModels.GameSavePenaltyParameters): Promise<GameAPIModels.GameSavePenaltyResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SavePenalty`;
        const resultResponse = new GameAPIModels.GameSavePenaltyResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSavePenaltyResponses>(messagePack);
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

    export async function PetsAsync(parameters: GameAPIModels.GamePetsParameters): Promise<GameAPIModels.GamePetsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Pets`;
        const resultResponse = new GameAPIModels.GamePetsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GamePetsResponses>(messagePack);
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

    export async function PlayRecordGoldClashesAsync(parameters: GameAPIModels.GamePlayRecordGoldClashesParameters): Promise<GameAPIModels.GamePlayRecordGoldClashesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/PlayRecordGoldClashes`;
        const resultResponse = new GameAPIModels.GamePlayRecordGoldClashesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GamePlayRecordGoldClashesResponses>(messagePack);
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

    export async function PlayRecordRpgsAsync(parameters: GameAPIModels.GamePlayRecordRpgsParameters): Promise<GameAPIModels.GamePlayRecordRpgsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/PlayRecordRpgs`;
        const resultResponse = new GameAPIModels.GamePlayRecordRpgsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GamePlayRecordRpgsResponses>(messagePack);
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

    export async function ProfilesAsync(parameters: GameAPIModels.GameProfilesParameters): Promise<GameAPIModels.GameProfilesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Profiles`;
        const resultResponse = new GameAPIModels.GameProfilesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameProfilesResponses>(messagePack);
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

    export async function SaveProfilesAsync(parameters: GameAPIModels.GameSaveProfilesParameters): Promise<GameAPIModels.GameSaveProfilesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveProfiles`;
        const resultResponse = new GameAPIModels.GameSaveProfilesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveProfilesResponses>(messagePack);
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

    export async function DeleteProfilesAsync(parameters: GameAPIModels.GameDeleteProfilesParameters): Promise<GameAPIModels.GameDeleteProfilesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteProfiles`;
        const resultResponse = new GameAPIModels.GameDeleteProfilesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteProfilesResponses>(messagePack);
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

    export async function RankingRewardsAsync(parameters: GameAPIModels.GameRankingRewardsParameters): Promise<GameAPIModels.GameRankingRewardsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/RankingRewards`;
        const resultResponse = new GameAPIModels.GameRankingRewardsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameRankingRewardsResponses>(messagePack);
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

    export async function RpgAttributesAsync(parameters: GameAPIModels.GameRpgAttributesParameters): Promise<GameAPIModels.GameRpgAttributesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/RpgAttributes`;
        const resultResponse = new GameAPIModels.GameRpgAttributesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameRpgAttributesResponses>(messagePack);
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

    export async function SaveRpgAttributesAsync(parameters: GameAPIModels.GameSaveRpgAttributesParameters): Promise<GameAPIModels.GameSaveRpgAttributesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveRpgAttributes`;
        const resultResponse = new GameAPIModels.GameSaveRpgAttributesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveRpgAttributesResponses>(messagePack);
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

    export async function DeleteRpgAttributesAsync(parameters: GameAPIModels.GameDeleteRpgAttributesParameters): Promise<GameAPIModels.GameDeleteRpgAttributesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteRpgAttributes`;
        const resultResponse = new GameAPIModels.GameDeleteRpgAttributesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteRpgAttributesResponses>(messagePack);
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

    export async function SeasonMissionsAsync(parameters: GameAPIModels.GameSeasonMissionsParameters): Promise<GameAPIModels.GameSeasonMissionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SeasonMissions`;
        const resultResponse = new GameAPIModels.GameSeasonMissionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSeasonMissionsResponses>(messagePack);
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

    export async function SaveSeasonMissionsAsync(parameters: GameAPIModels.GameSaveSeasonMissionsParameters): Promise<GameAPIModels.GameSaveSeasonMissionsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveSeasonMissions`;
        const resultResponse = new GameAPIModels.GameSaveSeasonMissionsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveSeasonMissionsResponses>(messagePack);
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

    export async function SeasonPassesAsync(parameters: GameAPIModels.GameSeasonPassesParameters): Promise<GameAPIModels.GameSeasonPassesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SeasonPasses`;
        const resultResponse = new GameAPIModels.GameSeasonPassesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSeasonPassesResponses>(messagePack);
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

    export async function SaveSeasonPassAsync(parameters: GameAPIModels.GameSaveSeasonPassParameters): Promise<GameAPIModels.GameSaveSeasonPassResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveSeasonPass`;
        const resultResponse = new GameAPIModels.GameSaveSeasonPassResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveSeasonPassResponses>(messagePack);
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

    export async function TreasureBoxesAsync(parameters: GameAPIModels.GameTreasureBoxesParameters): Promise<GameAPIModels.GameTreasureBoxesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/TreasureBoxes`;
        const resultResponse = new GameAPIModels.GameTreasureBoxesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameTreasureBoxesResponses>(messagePack);
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

    export async function SaveTreasureBoxesAsync(parameters: GameAPIModels.GameSaveTreasureBoxesParameters): Promise<GameAPIModels.GameSaveTreasureBoxesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveTreasureBoxes`;
        const resultResponse = new GameAPIModels.GameSaveTreasureBoxesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveTreasureBoxesResponses>(messagePack);
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

    export async function DeleteTreasureBoxesAsync(parameters: GameAPIModels.GameDeleteTreasureBoxesParameters): Promise<GameAPIModels.GameDeleteTreasureBoxesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteTreasureBoxes`;
        const resultResponse = new GameAPIModels.GameDeleteTreasureBoxesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteTreasureBoxesResponses>(messagePack);
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

    export async function UserAccountByNickAsync(parameters: GameAPIModels.GameUserAccountByNickParameters): Promise<GameAPIModels.GameUserAccountByNickResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserAccountByNick`;
        const resultResponse = new GameAPIModels.GameUserAccountByNickResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserAccountByNickResponses>(messagePack);
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

    export async function UserAccountByUIDAsync(parameters: GameAPIModels.GameUserAccountByUIDParameters): Promise<GameAPIModels.GameUserAccountByUIDResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserAccountByUID`;
        const resultResponse = new GameAPIModels.GameUserAccountByUIDResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserAccountByUIDResponses>(messagePack);
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

    export async function UserAccountByMemberNoAsync(parameters: GameAPIModels.GameUserAccountByMemberNoParameters): Promise<GameAPIModels.GameUserAccountByMemberNoResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserAccountByMemberNo`;
        const resultResponse = new GameAPIModels.GameUserAccountByMemberNoResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserAccountByMemberNoResponses>(messagePack);
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

    export async function SaveUserAccountAsync(parameters: GameAPIModels.GameSaveUserAccountParameters): Promise<GameAPIModels.GameSaveUserAccountResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveUserAccount`;
        const resultResponse = new GameAPIModels.GameSaveUserAccountResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveUserAccountResponses>(messagePack);
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

    export async function UserBlockByNickAsync(parameters: GameAPIModels.GameUserBlockByNickParameters): Promise<GameAPIModels.GameUserBlockByNickResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserBlockByNick`;
        const resultResponse = new GameAPIModels.GameUserBlockByNickResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserBlockByNickResponses>(messagePack);
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

    export async function UserBlockByMemberNoAsync(parameters: GameAPIModels.GameUserBlockByMemberNoParameters): Promise<GameAPIModels.GameUserBlockByMemberNoResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserBlockByMemberNo`;
        const resultResponse = new GameAPIModels.GameUserBlockByMemberNoResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserBlockByMemberNoResponses>(messagePack);
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

    export async function UserBlockByUIDAsync(parameters: GameAPIModels.GameUserBlockByUIDParameters): Promise<GameAPIModels.GameUserBlockByUIDResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserBlockByUID`;
        const resultResponse = new GameAPIModels.GameUserBlockByUIDResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserBlockByUIDResponses>(messagePack);
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

    export async function SaveUserBlockAsync(parameters: GameAPIModels.GameSaveUserBlockParameters): Promise<GameAPIModels.GameSaveUserBlockResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveUserBlock`;
        const resultResponse = new GameAPIModels.GameSaveUserBlockResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveUserBlockResponses>(messagePack);
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

    export async function DeleteUserBlockAsync(parameters: GameAPIModels.GameDeleteUserBlockParameters): Promise<GameAPIModels.GameDeleteUserBlockResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteUserBlock`;
        const resultResponse = new GameAPIModels.GameDeleteUserBlockResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteUserBlockResponses>(messagePack);
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

    export async function UserDevicesAsync(parameters: GameAPIModels.GameUserDevicesParameters): Promise<GameAPIModels.GameUserDevicesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/UserDevices`;
        const resultResponse = new GameAPIModels.GameUserDevicesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameUserDevicesResponses>(messagePack);
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

    export async function WeaponCategoriesAsync(parameters: GameAPIModels.GameWeaponCategoriesParameters): Promise<GameAPIModels.GameWeaponCategoriesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/WeaponCategories`;
        const resultResponse = new GameAPIModels.GameWeaponCategoriesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameWeaponCategoriesResponses>(messagePack);
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

    export async function SaveWeaponCategoriesAsync(parameters: GameAPIModels.GameSaveWeaponCategoriesParameters): Promise<GameAPIModels.GameSaveWeaponCategoriesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveWeaponCategories`;
        const resultResponse = new GameAPIModels.GameSaveWeaponCategoriesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveWeaponCategoriesResponses>(messagePack);
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

    export async function DeleteWeaponCategoriesAsync(parameters: GameAPIModels.GameDeleteWeaponCategoriesParameters): Promise<GameAPIModels.GameDeleteWeaponCategoriesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteWeaponCategories`;
        const resultResponse = new GameAPIModels.GameDeleteWeaponCategoriesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteWeaponCategoriesResponses>(messagePack);
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

    export async function SearchUserAccountsAsync(parameters: GameAPIModels.GameSearchUserAccountsParameters): Promise<GameAPIModels.GameSearchUserAccountsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SearchUserAccounts`;
        const resultResponse = new GameAPIModels.GameSearchUserAccountsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSearchUserAccountsResponses>(messagePack);
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

    export async function SearchUserAccountsByMemberNoAsync(parameters: GameAPIModels.GameSearchUserAccountsByMemberNoParameters): Promise<GameAPIModels.GameSearchUserAccountsByMemberNoResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SearchUserAccountsByMemberNo`;
        const resultResponse = new GameAPIModels.GameSearchUserAccountsByMemberNoResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSearchUserAccountsByMemberNoResponses>(messagePack);
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

    export async function ImmediatelySendMailAsync(parameters: GameAPIModels.GameImmediatelySendMailParameters): Promise<GameAPIModels.GameImmediatelySendMailResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/ImmediatelySendMail`;
        const resultResponse = new GameAPIModels.GameImmediatelySendMailResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameImmediatelySendMailResponses>(messagePack);
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

    export async function MailWithUsersAsync(parameters: GameAPIModels.GameMailWithUsersParameters): Promise<GameAPIModels.GameMailWithUsersResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/MailWithUsers`;
        const resultResponse = new GameAPIModels.GameMailWithUsersResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameMailWithUsersResponses>(messagePack);
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

    export async function DeleteMailWithUsersAsync(parameters: GameAPIModels.GameDeleteMailWithUsersParameters): Promise<GameAPIModels.GameDeleteMailWithUsersResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteMailWithUsers`;
        const resultResponse = new GameAPIModels.GameDeleteMailWithUsersResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteMailWithUsersResponses>(messagePack);
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

    export async function MaintenancesAsync(): Promise<GameAPIModels.GameMaintenancesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/Maintenances`;
        const resultResponse = new GameAPIModels.GameMaintenancesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameMaintenancesResponses>(messagePack);
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

    export async function SaveMaintenancesAsync(parameters: GameAPIModels.GameSaveMaintenancesParameters): Promise<GameAPIModels.GameSaveMaintenancesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveMaintenances`;
        const resultResponse = new GameAPIModels.GameSaveMaintenancesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveMaintenancesResponses>(messagePack);
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

    export async function DeleteMaintenancesAsync(parameters: GameAPIModels.GameDeleteMaintenancesParameters): Promise<GameAPIModels.GameDeleteMaintenancesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteMaintenances`;
        const resultResponse = new GameAPIModels.GameDeleteMaintenancesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteMaintenancesResponses>(messagePack);
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

    export async function NoticeBannersAsync(): Promise<GameAPIModels.GameNoticeBannersResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/NoticeBanners`;
        const resultResponse = new GameAPIModels.GameNoticeBannersResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameNoticeBannersResponses>(messagePack);
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

    export async function NoticeBannerAsync(parameters: GameAPIModels.GameNoticeBannerParameters): Promise<GameAPIModels.GameNoticeBannerResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/NoticeBanner`;
        const resultResponse = new GameAPIModels.GameNoticeBannerResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameNoticeBannerResponses>(messagePack);
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

    export async function SaveNoticeBannersAsync(parameters: GameAPIModels.GameSaveNoticeBannersParameters): Promise<GameAPIModels.GameSaveNoticeBannersResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveNoticeBanners`;
        const resultResponse = new GameAPIModels.GameSaveNoticeBannersResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveNoticeBannersResponses>(messagePack);
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

    export async function DeleteNoticeBannersAsync(parameters: GameAPIModels.GameDeleteNoticeBannersParameters): Promise<GameAPIModels.GameDeleteNoticeBannersResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteNoticeBanners`;
        const resultResponse = new GameAPIModels.GameDeleteNoticeBannersResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteNoticeBannersResponses>(messagePack);
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

    export async function OpenWorldTimeAsync(): Promise<GameAPIModels.GameOpenWorldTimeResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/OpenWorldTime`;
        const resultResponse = new GameAPIModels.GameOpenWorldTimeResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameOpenWorldTimeResponses>(messagePack);
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

    export async function SaveOpenWorldTimeAsync(parameters: GameAPIModels.GameSaveOpenWorldTimeParameters): Promise<GameAPIModels.GameSaveOpenWorldTimeResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveOpenWorldTime`;
        const resultResponse = new GameAPIModels.GameSaveOpenWorldTimeResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveOpenWorldTimeResponses>(messagePack);
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

    export async function ChattingNoticesAsync(): Promise<GameAPIModels.GameChattingNoticesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/ChattingNotices`;
        const resultResponse = new GameAPIModels.GameChattingNoticesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameChattingNoticesResponses>(messagePack);
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

    export async function ChattingNoticeAsync(parameters: GameAPIModels.GameChattingNoticeParameters): Promise<GameAPIModels.GameChattingNoticeResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/ChattingNotice`;
        const resultResponse = new GameAPIModels.GameChattingNoticeResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameChattingNoticeResponses>(messagePack);
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

    export async function SaveChattingNoticesAsync(parameters: GameAPIModels.GameSaveChattingNoticesParameters): Promise<GameAPIModels.GameSaveChattingNoticesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveChattingNotices`;
        const resultResponse = new GameAPIModels.GameSaveChattingNoticesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveChattingNoticesResponses>(messagePack);
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

    export async function DeleteChattingNoticesAsync(parameters: GameAPIModels.GameDeleteChattingNoticesParameters): Promise<GameAPIModels.GameDeleteChattingNoticesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DeleteChattingNotices`;
        const resultResponse = new GameAPIModels.GameDeleteChattingNoticesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDeleteChattingNoticesResponses>(messagePack);
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

    export async function ImmediatelyChattingNoticeAsync(parameters: GameAPIModels.GameImmediatelyChattingNoticeParameters): Promise<GameAPIModels.GameImmediatelyChattingNoticeResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/ImmediatelyChattingNotice`;
        const resultResponse = new GameAPIModels.GameImmediatelyChattingNoticeResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameImmediatelyChattingNoticeResponses>(messagePack);
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

    export async function GoldClashTimeAsync(): Promise<GameAPIModels.GameGoldClashTimeResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/GoldClashTime`;
        const resultResponse = new GameAPIModels.GameGoldClashTimeResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameGoldClashTimeResponses>(messagePack);
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

    export async function SaveGoldClashTimeAsync(parameters: GameAPIModels.GameSaveGoldClashTimeParameters): Promise<GameAPIModels.GameSaveGoldClashTimeResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveGoldClashTime`;
        const resultResponse = new GameAPIModels.GameSaveGoldClashTimeResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveGoldClashTimeResponses>(messagePack);
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

    export async function SilverMedalStoresAsync(parameters: GameAPIModels.GameSilverMedalStoresParameters): Promise<GameAPIModels.GameSilverMedalStoresResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SilverMedalStores`;
        const resultResponse = new GameAPIModels.GameSilverMedalStoresResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSilverMedalStoresResponses>(messagePack);
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

    export async function LobbyServerTimesAsync(): Promise<GameAPIModels.GameLobbyServerTimesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/LobbyServerTimes`;
        const resultResponse = new GameAPIModels.GameLobbyServerTimesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameLobbyServerTimesResponses>(messagePack);
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

    export async function SaveLobbyServerTimesAsync(parameters: GameAPIModels.GameSaveLobbyServerTimesParameters): Promise<GameAPIModels.GameSaveLobbyServerTimesResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/SaveLobbyServerTimes`;
        const resultResponse = new GameAPIModels.GameSaveLobbyServerTimesResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameSaveLobbyServerTimesResponses>(messagePack);
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

    export async function EventMailsAsync(parameters: GameAPIModels.GameEventMailsParameters): Promise<GameAPIModels.GameEventMailsResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/EventMails`;
        const resultResponse = new GameAPIModels.GameEventMailsResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameEventMailsResponses>(messagePack);
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

    export async function EventMailAsync(parameters: GameAPIModels.GameEventMailParameters): Promise<GameAPIModels.GameEventMailResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/EventMail`;
        const resultResponse = new GameAPIModels.GameEventMailResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameEventMailResponses>(messagePack);
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

    export async function DailyRankingAsync(parameters: GameAPIModels.GameDailyRankingParameters): Promise<GameAPIModels.GameDailyRankingResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Game/DailyRanking`;
        const resultResponse = new GameAPIModels.GameDailyRankingResponses();
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
            const responseData = Helpers.decodeMessagePack<GameAPIModels.GameDailyRankingResponses>(messagePack);
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

}
