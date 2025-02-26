import isEmpty from 'lodash/isEmpty';
import deepmerge from 'deepmerge';
import download from 'downloadjs';
import Strings from 'src/helpers/strings';
import { Helpers } from 'src/helpers/index';
import { Errors } from '@ngeldata/autoErrors';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';
import { ATestAPIModels } from '@ngeldata/models/atestAPIModels';

export namespace ATestAPI {

    export async function StompTestAsync(parameters: ATestAPIModels.ATestStompTestParameters): Promise<ATestAPIModels.ATestStompTestResponses> {

        const apiHostName = Helpers.getCookie("API_HOST_NAME") ?? 'https://localhost:4101';
        const routeURI = apiHostName + `/ATest/StompTest`;
        const resultResponse = new ATestAPIModels.ATestStompTestResponses();

        try {
            const response = await fetch(`${routeURI}?NoticeMessage=${parameters.NoticeMessage}&requestURL=${document.location.pathname}`, {
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

            return (await response.json()) as ATestAPIModels.ATestStompTestResponses;
        } catch (error) {
            console.error(error);
            resultResponse.error = Errors.Unknown;
        }

        return resultResponse;

    }

}
