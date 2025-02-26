import statusCode from 'http-status-codes';
import type { NextFunction, Request, Response, Router } from 'express';
import { BaseHttpController, controller, httpGet, httpPost, request, requestParam, response, next } from 'inversify-express-utils';
import { inject } from 'inversify';
import { Logger } from 'winston';
import { LoggerService } from './../services/logger.js';
import { Defines } from './../../ngel/data/autoDefines.js';
import isEmpty from 'lodash/isEmpty.js';

@controller('/')
export class MainController extends BaseHttpController {

    private readonly _logger: Logger;

    constructor(
        @inject(LoggerService.name) private _loggerService: LoggerService
    ) {
        super();
        this._logger = _loggerService.logger;
    }

    @httpGet('/')
    public async Main(@request() req: Request, @response() res: Response, @next() next: NextFunction) {
        const tokenName = process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK';
        if ('undefined' != typeof req.cookies[tokenName] && false == isEmpty(req.cookies[tokenName]))
            return res.redirect('/GMS');
        
        return res.render("oauth/signin", { code: 'test', state: 'normal', provider: Defines.OAuthProvider.CustomEmail, appId: process.env.GM_APP_ID, tokenName: tokenName });
    }

}