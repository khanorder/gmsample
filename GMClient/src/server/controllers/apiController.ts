import statusCode from 'http-status-codes';
import type { NextFunction, Request, Response, Router } from 'express';
import { BaseHttpController, controller, httpGet, httpPost, request, requestParam, response, next } from 'inversify-express-utils';
import { inject } from 'inversify';
import { Logger } from 'winston';
import { LoggerService } from './../services/logger.js';
import dayjs, { Dayjs } from 'dayjs';
import timezone from 'dayjs/plugin/timezone.js';
import utc from 'dayjs/plugin/utc.js';
dayjs.extend(timezone);
dayjs.extend(utc);

@controller('/api')
export class APIController extends BaseHttpController {

    private readonly _logger: Logger;

    constructor(
        @inject(LoggerService.name) private _loggerService: LoggerService,
    ) {
        super();
        this._logger = _loggerService.logger;
    }

    @httpGet('/')
    public async API(@request() req: Request, @response() res: Response, @next() next: NextFunction) {
        res.setHeader("Content-Type", "application/json");
        res.status(statusCode.OK).json([]);
    }

}