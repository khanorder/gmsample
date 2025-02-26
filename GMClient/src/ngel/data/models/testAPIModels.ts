import { Models } from '@ngeldata/models/index';
import { DataTableModels } from '@ngeldata/tables/model';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngeldata/autoErrors';
import { APIModelInterfaces } from '@ngeldata/models/apiModelInterfaces';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';

export namespace TestAPIModels {

    export class TestSignInParameters {
        name: string = '';
        password: string = '';
    }

    export class TestSignInResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
    }

    export class TestSignOutResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
    }

    export class TestSignUpParameters {
        name: string = '';
        password: string = '';
    }

    export class TestSignUpResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
    }

    export class TestTesterSignInParameters {
        clientId: string = '';
    }

    export class TestTesterSignInResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user: Models.SignInUser|null = null;
    }

}
