import { ReactElement, useEffect, useRef } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import { useRouter } from 'next/router';
import { Helpers } from '@helpers/index';
import * as layoutsActions from '@store/reducers/layouts';
import * as gmServerHubActions from '@ngeldata/hubs/GMServer/reducer';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
import ThemeLayout from '@components/layouts/theme';
import Loading from '@components/commons/loading';
const RedirectLayout = dynamic(() => import('@components/layouts/redirect'), { ssr: true });

interface MicrosoftCallbackProps {
    appId: string;
}

function Page({ appId }: MicrosoftCallbackProps) {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const layouts = useAppSelector(state => state.layouts);
    const user = useAppSelector(state => state.user);
    const gmServerHub = useAppSelector(state => state.GMServerHub);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();

    //#region OnRender
    useEffect(() => {
        if (firstRender.current) {
            dispatch(layoutsActions.startLoadingMessage("로그인 중입니다."));
            firstRender.current = false;
        }

    }, [firstRender, dispatch]);
    //#endregion

    useEffect(() => {
        if (false == firstRender.current) {
            var code = Helpers.getParameterByName("code");
    
            if (!code) {
                console.log("not found code.");
                return;
            }

            if (!appId) {
                console.log("not found appId.");
                return;
            }

            dispatch(gmServerHubActions.signInReq({ clientId: appId, code: code }));
        }

    }, [firstRender, dispatch, gmServerHub.connection, appId]);

    useEffect(() => {
        if (false == firstRender.current) {
            if (user.signinId) {
                dispatch(layoutsActions.stopLoading());
                router.push(process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS");
            }
        }
    }, [firstRender, user.signinId, router, dispatch]);
    
    return (
        <Loading />
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <RedirectLayout>
            <ThemeLayout>
                {page}
            </ThemeLayout>
        </RedirectLayout>
    );
}

export const getServerSideProps: GetServerSideProps = async (context) => {
    const appId = process.env.GM_APP_ID ?? "";
    if (isEmpty(appId))
        return { notFound: true };

    return {
        props: { appId: appId }
    };
}

export default Page;