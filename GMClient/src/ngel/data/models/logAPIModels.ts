import { v4 as uuidv4 } from 'uuid';
import { Models } from '@ngeldata/models/index';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngeldata/autoErrors';
import { APIModelInterfaces } from '@ngeldata/models/apiModelInterfaces';
import dayjs, { Dayjs } from 'dayjs';
import timezone from 'dayjs/plugin/timezone';
import utc from 'dayjs/plugin/utc';
dayjs.extend(timezone);
dayjs.extend(utc);

export namespace LogAPIModels {

    export class LogBiskitLogsResponses implements APIModelInterfaces.IAPIResponse {
        result: boolean = false;
        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        biskitLogs: Array<Models.BiskitLog> = [];
    }

}
