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
import { TestAPIModels } from '@ngeldata/models/testAPIModels';

export namespace TestAPI {

    export async function SignInAsync(parameters: TestAPIModels.TestSignInParameters): Promise<TestAPIModels.TestSignInResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Test/SignIn`;
        const resultResponse = new TestAPIModels.TestSignInResponses();

        try {
            const response = await fetch(`${routeURI}?name=${parameters.name}&password=${parameters.password}&requestURL=${document.location.pathname}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            return (await response.json()) as TestAPIModels.TestSignInResponses;
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function SignOutAsync(): Promise<TestAPIModels.TestSignOutResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Test/SignOut`;
        const resultResponse = new TestAPIModels.TestSignOutResponses();
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
            const responseData = Helpers.decodeMessagePack<TestAPIModels.TestSignOutResponses>(messagePack);
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

    export async function SignUpAsync(parameters: TestAPIModels.TestSignUpParameters): Promise<TestAPIModels.TestSignUpResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Test/SignUp`;
        const resultResponse = new TestAPIModels.TestSignUpResponses();

        try {
            const response = await fetch(`${routeURI}?name=${parameters.name}&password=${parameters.password}&requestURL=${document.location.pathname}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            return (await response.json()) as TestAPIModels.TestSignUpResponses;
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

    export async function TesterSignInAsync(parameters: TestAPIModels.TestTesterSignInParameters): Promise<TestAPIModels.TestTesterSignInResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/Test/TesterSignIn`;
        const resultResponse = new TestAPIModels.TestTesterSignInResponses();

        try {
            const response = await fetch(`${routeURI}?clientId=${parameters.clientId}&requestURL=${document.location.pathname}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'text/plain',
                },
                credentials: 'include'
            });

            if (401 === response.status) {
                if (confirm(Strings.getString(1)))
                    location.href = '/';

                resultResponse.error = Errors.Common_Forbidden;
                return resultResponse;
            }

            return (await response.json()) as TestAPIModels.TestTesterSignInResponses;
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

}
