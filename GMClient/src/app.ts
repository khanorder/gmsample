//#region import
import 'reflect-metadata';
import { Container } from 'inversify';
import { InversifyExpressServer, interfaces } from 'inversify-express-utils';
import express, { Request, Response, NextFunction } from 'express';
import next from 'next';
import http from 'http';
import https from 'https';
import cookieParser from 'cookie-parser';
import fs from 'fs';
import path, { resolve } from 'path';
import morgan from 'morgan';
import { Logger } from 'winston';
import dotenv from 'dotenv';
// import helmet from 'helmet';
import { AuthViewMiddleware } from './server/middlewares/authViewMiddleware.js';
import { AuthMiddleware } from './server/middlewares/authMiddleware.js';
import { ManageMiddleware } from './server/middlewares/manageMiddleware.js';
import { NextServer } from 'next/dist/server/next';
import { LoggerService } from './server/services/logger.js';
import { MainController } from './server/controllers/mainController.js';
import { APIController } from './server/controllers/apiController.js';
import { OAuthController } from './server/controllers/oauthController.js';
import { Defines } from './ngel/data/autoDefines.js';
//#endregion

export class Server {
    private _logger: Logger;
    private container: Container;
    private server: InversifyExpressServer;
    private appNext: NextServer;
    private appExpress: express.Application;

    constructor() {
        const loggerService = new LoggerService();
        this._logger = loggerService.logger;

        try {
            switch (process.env.NODE_ENV) {
                case 'production':
                    dotenv.config({ path: path.join(process.cwd(), '.env') });
                    break;
            
                case 'development':
                    dotenv.config({ path: path.join(process.cwd(), '.env.development') });
                    dotenv.config({ path: path.join(process.cwd(), '.env.development.local') });
                    break;
            
                default:
                    const envError = new Error('Need to set process.env.NODE_ENV.');
                    this._logger.error(envError.message);
                    throw envError;
            }

            if (!process.env.API_HOST_NAME) {
                const apiHostNameError = new Error('Need to set process.env.API_HOST_NAME.');
                this._logger.error(apiHostNameError);
                throw apiHostNameError;
            }
    
            if (!process.env.SERVER_STATE) {
                const serverStateError = new Error('Need to set process.env.SERVER_STATE.');
                this._logger.error(serverStateError);
                throw serverStateError;
            }
    
            if (!process.env.NEXT_PUBLIC_SESSION_COOKIE_NAME) {
                const sessionCookieNameError = new Error('Need to set process.env.NEXT_PUBLIC_SESSION_COOKIE_NAME.');
                this._logger.error(sessionCookieNameError);
                throw sessionCookieNameError;
            }
    
            if (!process.env.SESSION_COOKIE_SECRET) {
                const sessionCookieSecretError = new Error('Need to set process.env.SESSION_COOKIE_SECRET.');
                this._logger.error(sessionCookieSecretError);
                throw sessionCookieSecretError;
            }
    
            if (!process.env.SESSION_COOKIE_SECURE) {
                const sessionCookieSecureError = new Error('Need to set process.env.SESSION_COOKIE_SECURE.');
                this._logger.error(sessionCookieSecureError);
                throw sessionCookieSecureError;
            }
        } catch (error) {
            this._logger.error(error);
            throw error;
        }

        if (!process.env.OAUTH_PATH) {
            const oauthPathError = new Error('Need to set process.env.OAUTH_PATH.');
            this._logger.error(oauthPathError);
            throw oauthPathError;
        }

        if (!process.env.NEXT_PUBLIC_AUTH_PATH) {
            const authPathError = new Error('Need to set process.env.NEXT_PUBLIC_AUTH_PATH.');
            this._logger.error(authPathError);
            throw authPathError;
        }

        if (!process.env.NEXT_PUBLIC_MANAGE_PATH) {
            const managePathError = new Error('Need to set process.env.NEXT_PUBLIC_MANAGE_PATH.');
            this._logger.error(managePathError);
            throw managePathError;
        }

        const options = 'development' === process.env.NODE_ENV ? { protocol: 'https', dev: true, hostname: "", port: 443 } : { protocol: 'https', hostname: "", port: 443 };
        try {
            if (process.env.HOST)
                options.hostname = process.env.HOST;
    
            if ('true' == process.env.HTTPS && process.env.HTTPS_PORT) {
                try {
                    options.port = parseInt(process.env.HTTPS_PORT);
                } catch (error) {
                    this._logger.error(`app.ts - parsePort: ${error}`);
                }
            } else if (process.env.PORT) {
                options.protocol = 'http';
                try {
                    options.port = parseInt(process.env.PORT);
                } catch (error) {
                    this._logger.error(`app.ts - parsePort: ${error}`);
                }
            }
        } catch (error) {
            this._logger.error(error);
            throw error;
        }

        try {
            this.appNext = next(options);
        } catch (error) {
            this._logger.error(error);
            throw error;
        }

        this.container = new Container();

        try {
            this.container.bind(LoggerService.name).to(LoggerService).inSingletonScope();
            this.container.bind(APIController.name).to(APIController);
            this.container.bind(OAuthController.name).to(OAuthController);
        } catch (error) {
            this._logger.error(error);
            throw error;
        }

        this.server = new InversifyExpressServer(this.container);

        const handle = this.appNext.getRequestHandler();

        this.server.setConfig((app: express.Application) => {
            app.set('trust proxy', true);
            app.set('views', 'src/views');
            app.set('view engine', 'pug');
            app.use(express.json());
            app.use(express.urlencoded({ extended: true }));
            app.use(cookieParser());
            app.use((req: Request, res: Response, next: NextFunction) => {
                if (req.url.startsWith('/_next/static')) {
                    next();
                } else if (req.url.startsWith('/_next/webpack')) {
                    next();
                } else if (req.url.startsWith('/css')) {
                    next();
                } else if (req.url.startsWith('/fonts')) {
                    next();
                } else if (req.url.startsWith('/images')) {
                    next();
                } else if (req.url == '/favicon.ico') {
                    next();
                } else {
                    return next();
                }
            });

            switch (process.env.NODE_ENV) {
                case 'production':
                    // app.use(helmet());
                    break;
            
                case 'development':
                    app.use(morgan('dev'));
                    break;
            }
            
            app.use((req: Request, res: Response, next: NextFunction) => {
                res.removeHeader("X-Powered-By");
                res.cookie("API_HOST_NAME", process.env.API_HOST_NAME, { sameSite: 'strict', maxAge: 9000000, secure: process.env.SERVER_STATE != '0' });
                res.cookie("SERVER_STATE", (process.env.SERVER_STATE as string).match(/[a-zA-Z]+/g) ? Defines.ServerStateType[(process.env.SERVER_STATE as string)] : process.env.SERVER_STATE, { sameSite: 'strict', maxAge: 9000000, secure: (process.env.SERVER_STATE != '0' && process.env.SERVER_STATE != 'InsideTest') });
                next();
            });

            app.use('/css', express.static(path.join(process.cwd(), 'public/css')));
            app.use('/js', express.static(path.join(process.cwd(), 'public/js')));
            app.use('/images', express.static(path.join(process.cwd(), 'public/images')));
            app.use('/fonts', express.static(path.join(process.cwd(), 'public/fonts')));
            app.use('/videos', express.static(path.join(process.cwd(), 'public/videos')));
            app.use('/favicon.ico', express.static(path.join(process.cwd(), 'public/favicon.ico')));

            app.use(process.env.NEXT_PUBLIC_AUTH_PATH ?? "", AuthMiddleware);
            app.use(process.env.NEXT_PUBLIC_MANAGE_PATH ?? "", ManageMiddleware);
            app.use(AuthViewMiddleware);

            app.use((req: Request, res: Response, next: NextFunction) => {
                if (req.path.startsWith("/api")) {
                    next();
                } else if (req.path.startsWith("/oauth")) {
                    next();
                } else {
                    return handle(req, res);
                }
            });
        });
        this.appExpress = this.server.build();

        this.appNext.prepare()
            .then(() => {
                const httpPort = this.NormalizePort(process.env.PORT ?? '3000');
                this.appExpress.set('port', httpPort);
        
                const httpServer = http.createServer(this.appExpress);
                httpServer.listen(httpPort);
        
                if (
                    'true' === process.env.HTTPS && process.env.SSL_CRT_FILE && fs.existsSync(process.env.SSL_CRT_FILE) && process.env.SSL_KEY_FILE && fs.existsSync(process.env.SSL_KEY_FILE)
                ) {
                    const httpsPort = this.NormalizePort(process.env.HTTPS_PORT ?? (httpPort + 1).toString());
                    this.appExpress.set('port', httpsPort);
        
                    const httpsServer = https.createServer({ key: fs.readFileSync(process.env.SSL_KEY_FILE), cert: fs.readFileSync(process.env.SSL_CRT_FILE) }, this.appExpress);
                    httpsServer.listen(httpsPort);
                }
            })
            .catch((exception) => {
                this._logger.error(exception.stack);
                process.exit(1);
            });
    }

    private NormalizePort(val: string): number {
        const port = parseInt(val, 10);
    
        if (isNaN(port))
            return 0;
    
        if (port >= 0)
            return port;
    
        return 0;
    }

    public static bootstrap(): Server { return new Server(); }
}

Server.bootstrap();