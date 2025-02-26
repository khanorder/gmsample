import { Defines } from './../../ngel/data/autoDefines.js';
import { Models } from './../../ngel/data/models/index.js';
import express, { Request, Response, NextFunction } from 'express';

export function AuthViewMiddleware (req: Request, res: Response, next: NextFunction) {
    // const isDevelopment = "production" != process.env.NODE_ENV;

    // if (false == req.path.startsWith("/_next") && isDevelopment)
    //     console.log(`AuthViewMiddleware: ${req.path}`);

    // const rootPath = process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS";
    // const authPath = process.env.NEXT_PUBLIC_AUTH_PATH ?? "/NGAuth";
    // const viewCookieName = `${process.env.NEXT_PUBLIC_SESSION_COOKIE_NAME}View`;
    // const tokenCookieName = `${process.env.NEXT_PUBLIC_SESSION_COOKIE_NAME}Token`;
    // const sessonSecure = "true" === process.env.SESSION_COOKIE_SECURE;
    // let sessionCookieMaxAge = 180000;
    // if (process.env.SESSION_COOKIE_MAX_AGE) {
    //     try {
    //         sessionCookieMaxAge = parseInt(process.env.SESSION_COOKIE_MAX_AGE);
    //     } catch (error) {
    //         if (isDevelopment)
    //             console.log(`parse SESSION_COOKIE_MAX_AGE: ${error}`);
    //     }
    // }

    // if (req.path.startsWith(rootPath)) {
    //     if (!req.session || !req.session["user"] || !req.session["token"]) {
    //         res.clearCookie(viewCookieName);
    //         res.clearCookie(tokenCookieName);
    //         return res.render("errors/404");
    //     } else {
    //         if (req.session && req.session["user"] && req.session["token"]) {
    //             res.cookie(viewCookieName, req.sessionID, { maxAge: sessionCookieMaxAge, path: '/', secure: sessonSecure, httpOnly: true });
    //             res.cookie(tokenCookieName, req.session["token"], { maxAge: sessionCookieMaxAge, path: '/', secure: sessonSecure, httpOnly: true });
    //             try {
    //                 const menusLinear = req.session["user"]["menusLinear"] as Models.UserMenu[];
    //                 if (rootPath != req.path.replace(/\/$/, '') && null == menusLinear.find(_ => (rootPath + _.path).replace(/\/$/, '') == req.path.replace(/\/$/, ''))) {
    //                     if (isDevelopment)
    //                         console.log(`Not allowed path : ${req.path}`);
    //                     return res.render("errors/404");
    //                 }
    //             } catch (error) {
    //                 if (isDevelopment)
    //                     console.log(`failed to parse menusLinear : ${error}`);
    //                 return res.render("errors/404");
    //             }
    //         } else {
    //             res.clearCookie(viewCookieName);
    //         }
    //     }
    // } else if (req.path.startsWith(authPath) || req.path.startsWith("/oauth/callback") || req.path.startsWith("/authorize/callback")) {
    //     if (req.session && req.session["user"] && req.session["token"]) {
    //         res.cookie(viewCookieName, req.sessionID, { maxAge: sessionCookieMaxAge, path: '/', secure: sessonSecure, httpOnly: true });
    //         res.cookie(tokenCookieName, req.session["token"], { maxAge: sessionCookieMaxAge, path: '/', secure: sessonSecure, httpOnly: true });
    //     } else {
    //         res.clearCookie(viewCookieName);
    //         res.clearCookie(tokenCookieName);
    //     }
    // }

    next();
}