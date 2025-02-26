import { Models } from '@ngeldata/models/index';
import { DataTableModels } from '@ngeldata/tables/model';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngeldata/autoErrors';
import { APIModelInterfaces } from '@ngeldata/models/apiModelInterfaces';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';

export namespace OAuthAPIModels {

    export class OAuthCheckConfirmEmailParameters {
        id: string = '';
    }

    export class OAuthCheckConfirmEmailResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user: Models.SignInUser|null = null;
    }

    export class OAuthInitEmailPasswordParameters {
        id: string = '';
        password: string = '';
    }

    export class OAuthInitEmailPasswordResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
    }

    export class OAuthSignInParameters {
        provider: Defines.OAuthProvider = 0;
        code: string = '';
        stateCode: string = '';
        clientId: string = '';
    }

    export class OAuthSignInResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        token: string = '';
        user: Models.SignInUser|null = null;
    }

    export class OAuthSignInLDAPParameters {
        clientId: string = '';
        email: string = '';
        password: string = '';
    }

    export class OAuthSignInLDAPResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        token: string = '';
    }

    export class OAuthSignInEmailParameters {
        clientId: string = '';
        email: string = '';
        password: string = '';
    }

    export class OAuthSignInEmailResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        token: string = '';
        countFailedSignin: number = 0;
    }

    export class OAuthCheckAuthenticationParameters {
        token: string = '';
    }

    export class OAuthCheckAuthenticationResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
    }

    export class OAuthSignOutResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

}
