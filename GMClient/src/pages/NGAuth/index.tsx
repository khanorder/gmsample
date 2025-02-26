import { ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetServerSideProps } from 'next';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import isEmpty from 'lodash/isEmpty';
import { Helpers } from '@helpers/index';
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Layout = dynamic(() => import('@components/layouts'), { ssr: false });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: false });
const AuthLayout = dynamic(() => import('@components/layouts/auth'), { ssr: false });
const OAuth = dynamic(() => import('@components/OAuth'), { ssr: false });

interface NGAuthProps {
    isLDAP: boolean;
    appId: string;
}

function Page({ isLDAP, appId }: NGAuthProps) {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const GMServerHub = useAppSelector(state => state.GMServerHub);
    const dispatch = useAppDispatch();
    const [serverState, setServerState] = useState<Defines.ServerStateType>(Defines.ServerStateType.Service);
    const [serverVersion, setServerVersion] = useState<string>("");
    const [clientVersion, setClientVersion] = useState<string>("");
    const firstRender = useRef(true);
    
    useEffect(() => {
        if (firstRender.current) {
            let serverState = Defines.ServerStateType.Service;
            try {
                const serverStateString = Helpers.getCookie("SERVER_STATE") ?? Defines.ServerStateType.Service.toString();
                serverState = parseInt(serverStateString);
            } catch (error) {
                if ("production" == process.env.NODE_ENV)
                    console.log(error);
            }
            setServerState(serverState);
        }
    }, [firstRender, setServerState, dispatch]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;
            
    }, [firstRender]);
    //#endregion

    useEffect(() => {
        setServerVersion(isEmpty(GMServerHub.serverVersion) ? "" : `GM Server: v${GMServerHub.serverVersion}`);
        setClientVersion(`GM Client: v${homepageSettings.clientMasterVersion}.${homepageSettings.clientUpdateVersion}.${homepageSettings.clientMaintenanceVersion}`);
    }, [serverVersion, clientVersion, homepageSettings, GMServerHub.connection, GMServerHub.serverVersion]);

    // useEffect(() => {
    //     if (!firstRender.current && hostName.match(/.*192\.168\.{1}[0-9]{1,3}\.{1}[0-9]{1,3}$/))
    //         setMarginTop(2);

    // }, [firstRender, hostName, setMarginTop]);
    
    const onChangeServeState = useCallback((e) => {
        switch (e.target.value) {
            case Defines.ServerStateType.InsideTest:
                if (!process.env.NEXT_PUBLIC_GMTOOL_URL_INSIDETEST) {
                    alert(`${Defines.ServerStateType[e.target.value]} 서버 정보를 없습니다.`);
                    return;
                }

                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_INSIDETEST ?? "");
                break;

            case Defines.ServerStateType.Service:
                if (!process.env.NEXT_PUBLIC_GMTOOL_URL_SERVICE) {
                    alert(`${Defines.ServerStateType[e.target.value]} 서버 정보를 없습니다.`);
                    return;
                }

                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_SERVICE ?? "");
                break;

            default:
                alert("준비중입니다!");
                return;
        }
        setServerState(e.target.value);
    }, [setServerState]);
    
    return (
        <>
            <Box sx={{ marginTop: 13, display: 'flex', flexDirection: 'column', alignItems: 'center', }} >
                <Box sx={{ width: '100%', display: 'flex', flexDirection: 'column', flexWrap: 'wrap' }}>
                    <Typography variant='h3' style={{ width: '100%', marginBottom: 10, textAlign: 'center', fontSize: '2rem' }}>GM.BAEJANGHO.COM</Typography>
                    <Typography variant='h5' style={{ width: '100%', marginBottom: 10, textAlign: 'center' }}>샘플 운영툴</Typography>
                </Box>
                <Box sx={{ width: "100%" }}>
                    <Grid container sx={{ paddingBottom: 2, fontSize: "13px", color: "#6f6f6f", textAlign: "center" }}>
                        {
                            GMServerHub.serverId
                                ? <Grid item xs={12}>GM Server ID: {GMServerHub.serverId}</Grid>
                                : ''
                        }
                        <Grid item xs={12}>{serverVersion}</Grid>
                        <Grid item xs={12}>{clientVersion}</Grid>
                    </Grid>
                </Box>
                <Box sx={{ textAlign: 'center' }}>
                    <OAuth appId={appId} />
                </Box>
            </Box>
        </>
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <ThemeLayout>
                <AuthLayout>{page}</AuthLayout>
            </ThemeLayout>
        </Layout>
    );
}

export const getServerSideProps: GetServerSideProps = async (context) => {
    const appId: string = process.env.GM_APP_ID ?? "";
    if (isEmpty(appId))
        return { notFound: true };

    return {
        props: { isLDAP: ('production' == process.env.NODE_ENV && '0' == process.env.SERVER_STATE), appId: appId }
    };
}

export default Page;