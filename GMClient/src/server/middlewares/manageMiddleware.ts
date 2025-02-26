import express, { Request, Response, NextFunction } from 'express';

export function ManageMiddleware (req: Request, res: Response, next: NextFunction) {
    // 로그인한 경우 관리자 시작 화면으로 리디렉션

    next();
}