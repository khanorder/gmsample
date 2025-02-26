import { ReactElement, useCallback, useEffect, useRef, useState } from 'react'
import { NextPageContext } from 'next/types';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { useRouter } from 'next/router';
import dynamic from 'next/dynamic';
import { OAuthAPI } from '@ngel/data/apis/oauthAPI';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';
import { Helpers } from '@helpers/index';
import { Errors } from '@ngel/data/autoErrors';
import * as userActions from '@store/reducers/user';
const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const DefaultLayout = dynamic(() => import('@components/layouts/default'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const CircularProgress = dynamic(() => import('@mui/material/CircularProgress'), { ssr: false });

interface RootPageProps {
    appId: string;
}

function Page({ appId }: RootPageProps) {
    const user = useAppSelector(state => state.user);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const signin = useCallback(() => {
        OAuthAPI.SignInAsync(
            {
                code: "test", 
                stateCode: "normal", 
                provider: Defines.OAuthProvider.CustomEmail, 
                clientId: appId
            }
        )
        .then(response => {
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
        })
        .catch(error => {
            console.log(error);
            router.push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth');
        })
        .finally(() => {
        });
    }, [router, dispatch, appId]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;

            const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');
            if (isEmpty(token)) {
                signin();
            } else {
                OAuthAPI.CheckAuthenticationAsync({token: token})
                .then(response => {
                        console.log(response);
                        switch (response.error) {
                            case Errors.None:
                                break;

                            default:
                                Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');
                                signin();
                        }
                    })
                    .catch(error => {
                        Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');
                        signin();
                    })
                    .finally(() => {
                        router.push(process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS');
                    });
            }
        }

    }, [firstRender, router, user, signin]);
    //#endregion

    const loading = useCallback(() => {
        return (
            isLoading
                ?
                    <>
                        <Grid item xs={12} sx={{ marginTop: 5, marginBottom: 2, fontSize: '18px', textAlign: 'center' }}>
                            운영툴을 로딩중입니다.
                        </Grid>
                        <Grid item xs={12} sx={{ marginBottom: 5, textAlign: 'center' }}>
                            <CircularProgress color='inherit' />
                        </Grid>
                    </>
                :
                    <></>
        );
    }, [isLoading]);

    return (
        <div style={{ width: '100%', height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
            <Grid container>
                <Grid item xs={12} sx={{ fontSize: '2rem', fontWeight: 700, textAlign: 'center', width: '100%' }}>GM.BAEJANGHO.COM</Grid>
                <Grid item xs={12} sx={{ fontSize: '20px', fontWeight: 400, textAlign: 'center' }}>샘플 운영툴</Grid>
            </Grid>
            <Grid container sx={{ fontSize: "13px", color: "#6f6f6f", mt: 2, textAlign: "center" }}>
                {/* <Grid item xs={12}><strong>테스트 계정</strong>으로 <strong>로그인</strong>하면 <strong>전체 기능</strong>을 볼 수 있습니다.</Grid> */}
                <Grid item xs={12}>Made by <strong>Bae JangHo</strong></Grid>
                <Grid item xs={12}><a href="mailto:zanghobae@gmail.com" title="배장호에게 메일 발송"><strong>zanghobae@gmail.com</strong></a></Grid>
                <Grid item xs={12} sx={{ marginTop: 1 }}><a href="tel:010-2070-4274" title="배장호에게 전화 걸기"><strong>010-2070-4274</strong></a></Grid>
                {loading()}
            </Grid>
        </div>
    );
}

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <DefaultLayout>{page}</DefaultLayout>
        </Layout>
    );
}

Page.getInitialProps = ({ res, err }: NextPageContext) => {
    const appId: string = process.env.GM_APP_ID ?? "";
    if (isEmpty(appId))
        return { notFound: true };

    return { appId };
}

export default Page;