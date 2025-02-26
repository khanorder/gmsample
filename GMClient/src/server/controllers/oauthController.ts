import statusCode from 'http-status-codes';
import type { NextFunction, Request, Response, Router } from 'express';
import { BaseHttpController, controller, httpGet, httpPost, request, requestParam, response, next } from 'inversify-express-utils';
import { inject } from 'inversify';
import { Logger } from 'winston';
import { LoggerService } from './../services/logger.js';
import { Defines } from './../../ngel/data/autoDefines.js';

@controller('/oauth')
export class OAuthController extends BaseHttpController {

    private readonly _logger: Logger;

    constructor(
        @inject(LoggerService.name) private _loggerService: LoggerService
    ) {
        super();
        this._logger = _loggerService.logger;
    }

    @httpGet('/')
    public async API(@request() req: Request, @response() res: Response, @next() next: NextFunction) {
        res.setHeader("Content-Type", "application/json");
        res.status(statusCode.OK).json();
    }

    @httpGet('/callback/microsoft')
    public async CallbackMicrosoft(@request() req: Request, @response() res: Response, @next() next: NextFunction) {
        if (!req.query.code)
            return res.render("errors/404");

        return res.render("oauth/signin", { code: req.query.code, state: '', provider: Defines.OAuthProvider.Microsoft, appId: process.env.GM_APP_ID, tokenName: process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK' });
    }

    @httpGet('/callback/kakao')
    public async CallbackKakao(@request() req: Request, @response() res: Response, @next() next: NextFunction) {
        if (!req.query.code)
            return res.render("errors/404");

        if (!req.query.state)
            return res.render("errors/404");

        return res.render("oauth/signin", { code: req.query.code, state: req.query.state,  provider: Defines.OAuthProvider.Kakao, appId: process.env.GM_APP_ID, tokenName: process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK' });
    }

    @httpGet('/callback/naver')
    public async CallbackNaver(@request() req: Request, @response() res: Response, @next() next: NextFunction) {
        if (!req.query.code)
            return res.render("errors/404");

        if (!req.query.state)
            return res.render("errors/404");

        return res.render("oauth/signin", { code: req.query.code, state: req.query.state, provider: Defines.OAuthProvider.Naver, appId: process.env.GM_APP_ID, tokenName: process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK' });
    }

}