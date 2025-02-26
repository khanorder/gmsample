import { ReactElement, ReactNode, useEffect, useRef } from 'react';
import { NextPage } from 'next/types';
import { AppProps } from 'next/app';
import wrapper from '@store/index';
import { useRouter } from 'next/router';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import * as GMServerHubActions from '@ngeldata/hubs/GMServer/reducer';
import { Defines } from '@ngel/data/autoDefines';
import { Helpers } from '@helpers/index';
import { ConnectedRouter } from 'connected-next-router';
import isEmpty from 'lodash/isEmpty';
import { dayjs, Dayjs } from '@helpers/localizedDayjs';
import { FingerPrint } from 'src/helpers/FingerPrint';
import { v4 as uuid } from 'uuid';
import '@styles/global.sass';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';

export type NextPageWithLayout = NextPage & {
    getLayout?: (page: ReactElement) => ReactNode
}

export type AppPropsWithLayout = AppProps & {
    Component: NextPageWithLayout;
}

const App = ({ Component, pageProps }: AppPropsWithLayout) => {
    // Use the layout defined at the page level, if available
    const getLayout = Component.getLayout ?? ((page) => page);
    const router = useRouter();
    const user = useAppSelector(state => state.user);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);

    useEffect(() => {
        if (firstRender.current) {
            const apiHostName = Helpers.getCookie("API_HOST_NAME");
            const serverStateString = Helpers.getCookie("SERVER_STATE");
            let serverState = Defines.ServerStateType.Service;
            if (serverStateString) {
                const serverStateInt = parseInt(serverStateString);
                if (!isNaN(serverStateInt))
                    serverState = serverStateInt;
            }

            firstRender.current = false;
            dispatch(GMServerHubActions.initConnection(apiHostName));
        }
    }, [firstRender, dispatch]);

    useEffect(() => {
        if (false == firstRender.current && window) {
            var fingerPrint = new FingerPrint();
            const sessionName = 'viewer';
            const cookies = document.cookie.split(';');
            const userCookie = cookies.find(cookie => cookie.trim().startsWith(`${sessionName}=`));
            let session = uuid();
            if (userCookie) {
                session = userCookie.trim().substring(sessionName.length + 1);
            }
            Helpers.setCookie30Min(sessionName, session);

            const fp = fingerPrint.getFingerprint();
            if (null == fp) {
                return;
            }
            const deviceType = fingerPrint.getDeviceType();
            const deviceVendor = fingerPrint.getDeviceVendor();
            const deviceModel = fingerPrint.getDeviceModel();
            const agent = fingerPrint.getAgent();
            const browser = fingerPrint.getBrowser();
            const browserVersion = fingerPrint.getBrowserVersion();
            const engine = fingerPrint.getEngine();
            const engineVersion = fingerPrint.getEngineVersion();
            const os = fingerPrint.getOS();
            const osVersion = fingerPrint.getOSVersion();
            const host = location.host;
            const path = location.pathname;
            let parameter = '';
            const hrefArr = location.href.split("?", 2);
            if (hrefArr && hrefArr.length > 1 && false == isEmpty(hrefArr[1]))
                parameter = hrefArr[1];

            const localTime = dayjs.utc().toDate();
            ManagerAPI.SaveVisitAsync({ session: session, fp: fp, deviceType: deviceType, deviceVendor: deviceVendor, deviceModel: deviceModel, agent: agent, browser: browser, browserVersion: browserVersion, engine: engine, engineVersion: engineVersion, os: os, osVersion: osVersion, host: host, path: path, parameter: parameter, title: document.title, localTime: localTime });
        }
    }, [firstRender, router]);

    useEffect(() => {
        const rootPath = process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS";
        if (user.signinId && rootPath != router.pathname && router.pathname.startsWith(rootPath) && null == user.menusLinear.find(_ => rootPath + _.path == router.pathname))
            router.push(rootPath);

    }, [router, user.menusLinear, user.signinId]);

    return getLayout(
        <ConnectedRouter>
            <Component {...pageProps} />
        </ConnectedRouter>
    );
}

export default wrapper.withRedux(App);