import { createRef, useCallback, useRef, useEffect } from 'react';
import { useRouter } from 'next/router';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { v4 as uuidv4 } from 'uuid';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { OAuthAPI } from '@ngel/data/apis/oauthAPI';
import { Helpers } from '@helpers/index';
import { Errors } from '@ngel/data/autoErrors';
import * as userActions from '@store/reducers/user';
import isEmpty from 'lodash/isEmpty';
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });

interface OAuthProps {
    appId: string;
}

function OAuth({ appId }: OAuthProps) {
    const user = useAppSelector(state => state.user);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    const testLogin = useCallback(async () => {
        var response = await OAuthAPI.SignInAsync({ code: "test", stateCode: "normal", provider: Defines.OAuthProvider.CustomEmail, clientId: appId });
        if (response && response.token && null != response.user && false == isEmpty(response.user.signinId)) {
            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', response.token);
            dispatch(userActions.signIn(response.user));
            router.push(process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS');
        } else {
            switch (response.error) {
                case Errors.SignIn_AlreadySignIn:
                    alert('이미 로그인 중입니다.');
                    break;

                default:
                    alert(`로그인 실패 (${response.error})`);
            }
        }
    }, [appId, router, dispatch]);

    const testAdminLogin = useCallback(async () => {
        var response = await OAuthAPI.SignInAsync({ code: "test", stateCode: "admin", provider: Defines.OAuthProvider.CustomEmail, clientId: appId });
        if (response && response.token && null != response.user && false == isEmpty(response.user.signinId)) {
            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', response.token);
            dispatch(userActions.signIn(response.user));
            router.push(process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS');
        } else {
            switch (response.error) {
                case Errors.SignIn_AlreadySignIn:
                    alert('이미 로그인 중입니다.');
                    break;

                default:
                    alert(`로그인 실패 (${response.error})`);
            }
        }
    }, [appId, router, dispatch]);

    const login = useCallback(async (provider: Defines.OAuthProvider) => {
        const url = window.location.href;
        const urlArray = url.split('/');
        const protocol = urlArray[0] ?? "https";
        const stateCode = uuidv4().replaceAll('-', '');
        window.localStorage.setItem("stateCode", stateCode);
        switch (provider) {
            case Defines.OAuthProvider.Kakao:
                window.location.href = `https://kauth.kakao.com/oauth/authorize?client_id=${process.env.NEXT_PUBLIC_KAKAO_ID}&redirect_uri=${protocol}//${window.location.host}/oauth/callback/kakao&response_type=code&state=${stateCode}`;
                break;
    
            case Defines.OAuthProvider.Naver:
                window.location.href = `https://nid.naver.com/oauth2.0/authorize?client_id=${process.env.NEXT_PUBLIC_NAVER_ID}&redirect_uri=${protocol}//${window.location.host}/oauth/callback/naver&response_type=code&state=${stateCode}`;
                break;
        }
    }, []);

    const emailInput = createRef<HTMLDivElement>();
    const passwordInput = createRef<HTMLDivElement>();

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    useEffect(() => {
        if (false == firstRender.current) {
            if (user.signinId) {
                router.push(process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS");
            }
        }
    }, [firstRender, user.signinId, router]);

    return (
        <>
            <Box>
                <Grid container sx={{ fontSize: "13px", color: "#6f6f6f", textAlign: "center" }}>
                    <Grid item xs={12}>
                        <Button type="button" color="inherit" variant="outlined" 
                            sx={{
                                width: '100%', 
                                mt: 2, 
                                mb: 0, 
                                fontWeight: 400, 
                                pt: 1, 
                                pb: 1,
                                border: '1px solid rgba(255, 255, 255, 0)', 
                                color: '#ffffff', 
                                backgroundColor: '#494949', 
                                '&:hover': {
                                    color: '#6f6f6f', 
                                    border: '1px solid currentColor'
                                }
                            }} onClick={() => testLogin()}><strong>테스트 계정</strong>&nbsp;<span>로그인</span></Button>
                    </Grid>
                    <Grid item xs={12}>
                        <Button type="button" color="inherit" variant="outlined" 
                            sx={{
                                width: '100%', 
                                mt: 2, 
                                mb: 0, 
                                fontWeight: 400, 
                                pt: 1, 
                                pb: 1, 
                                border: '1px solid rgba(255, 255, 255, 0)', 
                                color: '#3a1d1d', 
                                backgroundColor: '#fee500', 
                                '&:hover': { 
                                    color: '#6f6f6f', 
                                    border: '1px solid currentColor'
                                }
                            }} onClick={() => login(Defines.OAuthProvider.Kakao)}><strong>KAKAO</strong>&nbsp;<span>계정 로그인</span></Button>
                    </Grid>
                    <Grid item xs={12}>
                        <Button type="button" color="inherit" variant="outlined" 
                            sx={{
                                width: '100%', 
                                mt: 2, 
                                mb: 0, 
                                fontWeight: 400, 
                                pt: 1, 
                                pb: 1, 
                                border: '1px solid rgba(255, 255, 255, 0)', 
                                color: '#ffffff', 
                                backgroundColor: '#2db400', 
                                '&:hover': {
                                    color: '#6f6f6f', 
                                    border: '1px solid currentColor'
                                }
                            }} onClick={() => login(Defines.OAuthProvider.Naver)}><strong>NAVER</strong>&nbsp;<span>계정 로그인</span></Button>
                    </Grid>
                </Grid>
                <Grid container sx={{ fontSize: "13px", color: "#6f6f6f", mt: 2, textAlign: "center" }}>
                    <Grid item xs={12}><strong>테스트 계정</strong>으로 <strong>로그인</strong>하면 <strong>전체 기능</strong>을 볼 수 있습니다.</Grid>
                    <Grid item xs={12}><strong>SNS 로그인</strong>은 <strong>이름</strong>, <strong>이메일</strong> 정보만 수집합니다.</Grid>
                    <Grid item xs={12}>Made by <strong>Bae JangHo</strong></Grid>
                    <Grid item xs={12}><a href="mailto:zanghobae@gmail.com" title="배장호에게 메일 발송"><strong>zanghobae@gmail.com</strong></a></Grid>
                    <Grid item xs={12} sx={{ marginTop: 1 }}><a href="tel:010-2070-4274" title="배장호에게 전화 걸기"><strong>010-2070-4274</strong></a></Grid>
                </Grid>
            </Box>
        </>
    );
}

export default OAuth;