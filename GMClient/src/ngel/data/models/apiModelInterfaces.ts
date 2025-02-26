import { Errors } from '@ngeldata/autoErrors';
import { Models } from '@ngeldata/models/index';

export namespace APIModelInterfaces
{
    export interface IAPIResponse {
        result: boolean;
        error: Errors;
        user?: Models.SignInUser|null;
    }
}
