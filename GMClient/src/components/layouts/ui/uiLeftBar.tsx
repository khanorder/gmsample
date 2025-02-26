import styles from '@styles/layouts/leftBar.module.sass';
import { styled } from '@mui/material/styles';
import { useState, useCallback, useEffect, useRef } from 'react';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import * as layoutActions from '@store/reducers/layouts';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
const List = dynamic(() => import('@mui/material/List'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const MuiDrawer = dynamic(() => import('@mui/material/Drawer'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const IconButton = dynamic(() => import('@mui/material/IconButton'), { ssr: false });
const ChevronLeftIcon = dynamic(() => import('@mui/icons-material/ChevronLeft'), { ssr: false });
const Divider = dynamic(() => import('@mui/material/Divider'), { ssr: false });
const UINavMenus = dynamic(() => import('./uiNavMenus'), { ssr: false });

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        '.MuiDrawer-docked': {
            height: '100%'
        },
        '& .MuiDrawer-paper': {
            position: 'relative',
            height: '100%',
            whiteSpace: 'nowrap',
            width: 240,
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen,
            }),
            boxSizing: 'border-box',
            ...(!open && {
                overflowX: 'hidden',
                height: '100%',
                transition: theme.transitions.create('width', {
                    easing: theme.transitions.easing.sharp,
                    duration: theme.transitions.duration.leavingScreen,
                }),
                width: theme.spacing(7),
                [theme.breakpoints.up('sm')]: {
                    width: theme.spacing(9),
                },
            }),
        },
    }),
);

function UILeftBar() {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const layouts = useAppSelector(state => state.layouts);
    const GMServerHub = useAppSelector(state => state.GMServerHub);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const [serverVersion, setServerVersion] = useState<string>("");
    const [clientVersion, setClientVersion] = useState<string>("");

    const toggleDrawer = useCallback(() => {
        dispatch(layoutActions.toggleLeftBar());
    }, [dispatch]);

    useEffect(() => {
        if (firstRender.current && homepageSettings.uaParser?.getDevice()?.type) {
            dispatch(layoutActions.closeLeftBar());
        }

    }, [firstRender, homepageSettings, dispatch]);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);

    useEffect(() => {
        setServerVersion(isEmpty(GMServerHub.serverVersion) ? "" : `GM Server: v${GMServerHub.serverVersion}`);
        setClientVersion(`GM Client: v${homepageSettings.clientMasterVersion}.${homepageSettings.clientUpdateVersion}.${homepageSettings.clientMaintenanceVersion}`);
    }, [serverVersion, clientVersion, homepageSettings, GMServerHub.serverVersion]);

    return (
        <>
            <Drawer variant="permanent" open={layouts.leftBarActive} className={layouts.leftBarClass}>
                <Toolbar sx={{ px: [1], }} className={styles.leftBarTitle}>
                    <Box sx={{ flexGrow: 1 }}>
                        <Typography variant="caption" color="inherit" noWrap className={styles.leftBarTitleInfo}>{serverVersion}</Typography>
                        <Typography variant="caption" color="inherit" noWrap className={styles.leftBarTitleInfo} sx={{ margin: "5px 0 0 0" }}>{clientVersion}</Typography>
                    </Box>
                    <IconButton className={styles.leftBarCloseButton} onClick={toggleDrawer}>
                        <ChevronLeftIcon className={styles.leftBarCloseButtonIcon} />
                    </IconButton>
                </Toolbar>
                <Divider />
                <UINavMenus />
                <Box className={styles.leftBarBottom}>
                    <Divider />
                    <List>
                        <ListItem sx={{ pt: 0, pb: 0 }}><Typography variant='caption'>Made by <strong>Bae JangHo</strong></Typography></ListItem>
                        <ListItem sx={{ pt: '3px', pb: 0 }}><Typography variant='caption'><a href="mailto:zanghobae@gmail.com" title="배장호에게 메일 발송"><strong>zanghobae@gmail.com</strong></a></Typography></ListItem>
                        <ListItem sx={{ pt: '3px', pb: 0 }}><Typography variant='caption'><a href="tel:010-2070-4274" title="배장호에게 전화 걸기"><strong>010-2070-4274</strong></a></Typography></ListItem>
                    </List>
                </Box>
            </Drawer>
        </>
    );
}

export default UILeftBar;