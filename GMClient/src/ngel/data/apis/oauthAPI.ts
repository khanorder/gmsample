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
import { OAuthAPIModels } from '@ngeldata/models/oauthAPIModels';

export namespace OAuthAPI {

    export async function CheckConfirmEmailAsync(parameters: OAuthAPIModels.OAuthCheckConfirmEmailParameters): Promise<OAuthAPIModels.OAuthCheckConfirmEmailResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/CheckConfirmEmail`;
        const resultResponse = new OAuthAPIModels.OAuthCheckConfirmEmailResponses();

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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthCheckConfirmEmailResponses>(messagePack);
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

    export async function InitEmailPasswordAsync(parameters: OAuthAPIModels.OAuthInitEmailPasswordParameters): Promise<OAuthAPIModels.OAuthInitEmailPasswordResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/InitEmailPassword`;
        const resultResponse = new OAuthAPIModels.OAuthInitEmailPasswordResponses();

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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthInitEmailPasswordResponses>(messagePack);
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

    export async function SignInAsync(parameters: OAuthAPIModels.OAuthSignInParameters): Promise<OAuthAPIModels.OAuthSignInResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/SignIn`;
        const resultResponse = new OAuthAPIModels.OAuthSignInResponses();

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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthSignInResponses>(messagePack);
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

    export async function SignInLDAPAsync(parameters: OAuthAPIModels.OAuthSignInLDAPParameters): Promise<OAuthAPIModels.OAuthSignInLDAPResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/SignInLDAP`;
        const resultResponse = new OAuthAPIModels.OAuthSignInLDAPResponses();

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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthSignInLDAPResponses>(messagePack);
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

    export async function SignInEmailAsync(parameters: OAuthAPIModels.OAuthSignInEmailParameters): Promise<OAuthAPIModels.OAuthSignInEmailResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/SignInEmail`;
        const resultResponse = new OAuthAPIModels.OAuthSignInEmailResponses();

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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthSignInEmailResponses>(messagePack);
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

    export async function CheckAuthenticationAsync(parameters: OAuthAPIModels.OAuthCheckAuthenticationParameters): Promise<OAuthAPIModels.OAuthCheckAuthenticationResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/CheckAuthentication`;
        const resultResponse = new OAuthAPIModels.OAuthCheckAuthenticationResponses();

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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthCheckAuthenticationResponses>(messagePack);
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

    export async function SignOutAsync(): Promise<OAuthAPIModels.OAuthSignOutResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/NGMS/OAuth/SignOut`;
        const resultResponse = new OAuthAPIModels.OAuthSignOutResponses();
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
            const responseData = Helpers.decodeMessagePack<OAuthAPIModels.OAuthSignOutResponses>(messagePack);
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
