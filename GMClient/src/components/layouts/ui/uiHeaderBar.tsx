import { styled } from '@mui/material/styles';
import { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import { useState, useCallback, useEffect, useRef } from 'react';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import { useRouter } from 'next/router';
import * as layoutActions from '@store/reducers/layouts';
import * as userActions from '@store/reducers/user';
import styles from '@styles/layouts/headerBar.module.sass';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { Helpers } from '@helpers/index';
import { OAuthAPI } from '@ngel/data/apis/oauthAPI';
import { Errors } from '@ngel/data/autoErrors';
const Link = dynamic(() => import('next/link'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const MuiAppBar = dynamic(() => import('@mui/material/AppBar'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const MenuIcon = dynamic(() => import('@mui/icons-material/Menu'), { ssr: false });
const LogoutIcon = dynamic(() => import('@mui/icons-material/Logout'), { ssr: false });
const UINotificationPopper = dynamic(() => import('./uiNotificationPopper'), { ssr: true });

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
}

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
    color: 'dark',
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        marginLeft: 240,
        width: `calc(100% - ${240}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

function UIHeaderBar() {
    const notificationButton = useRef<HTMLButtonElement|null>(null);
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const [serverState, setServerState] = useState<Defines.ServerStateType>(Defines.ServerStateType.Service);

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
            
    }, [firstRender, setServerState]);

    const toggleDrawer = useCallback(() => {
        dispatch(layoutActions.toggleLeftBar());
    }, [dispatch]);

    const logout = useCallback(async () => {
        if (!confirm(`로그아웃 하시겠습니까?`))
            return;

        try {
            const response = await OAuthAPI.SignOutAsync();
            switch (response.error) {
                case Errors.None:
                    Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');
                    dispatch(userActions.signOut());
                    router.push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth');
                    break;

                default:
                    alert(`로그아웃 실패`);
                    break;
            }
        } catch (error) {
            console.error(error);
        }
    }, [dispatch, router]);

    useEffect(() => {
        if (firstRender.current) {
            firstRender.current = false;
        } else {
            
        }
    }, [firstRender]);

    const onChangeServeState = useCallback((e) => {
        switch (e.target.value) {
            case Defines.ServerStateType.InsideTest:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_INSIDETEST ?? "");
                break;

            case Defines.ServerStateType.MainTest:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_MAIN ?? "");
                break;

            case Defines.ServerStateType.BetaTest:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_BETA ?? "");
                break;

            case Defines.ServerStateType.Production:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_PRODUCTION ?? "");
                break;

            case Defines.ServerStateType.QA:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_QA ?? "");
                break;

            case Defines.ServerStateType.Review:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_REVIEW ?? "");
                break;

            case Defines.ServerStateType.Service:
                window.open(process.env.NEXT_PUBLIC_GMTOOL_URL_SERVICE ?? "");
                break;

            default:
                alert("준비중입니다!");
                return;
        }
    }, []);

    return (
        <>
            <AppBar className={styles.appBar} position="absolute" open={layouts.leftBarActive}>
                <Toolbar className={styles.toolBar} sx={{ pr: '36px', minWidth: 300 }}>
                    <IconButton edge="start" color="inherit" aria-label="open drawer" onClick={toggleDrawer} sx={{ marginRight: '26px', ...(layouts.leftBarActive && { display: 'none' }) }}>
                        <MenuIcon />
                    </IconButton>
                    <Typography className={styles.mobileHidden} variant="h6" color="inherit" noWrap>
                        <Link href={process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS"} title="메인 페이지 바로가기">{homepageSettings.siteName}</Link>
                    </Typography>
                    <Typography sx={{ flexGrow: 1 }}></Typography>
                    <Box className={`${styles.userInfo} ${styles.mobileHidden}`}>
                        <Typography className={styles.userInfoDetail} variant="caption" color="inherit" noWrap>{user.email.replace(/@[a-zA-Z\d]+\.com$/, "")}</Typography>
                        <Typography className={styles.userInfoDetail} variant="caption" color="inherit" noWrap sx={{ margin: "5px 0 0 0" }}>{user.name}</Typography>
                    </Box>
                    <UINotificationPopper />
                    <IconButton edge="end" color="inherit" sx={{ marginLeft: '15px' }} onClick={logout}>
                        <LogoutIcon />
                    </IconButton>
                </Toolbar>
            </AppBar>
            
        </>
    );
}

export default UIHeaderBar;