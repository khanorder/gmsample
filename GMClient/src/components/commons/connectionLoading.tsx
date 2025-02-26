import { useEffect, useState, useCallback, useRef } from 'react';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
import { HubConnectionState } from '@microsoft/signalr';
import * as gmServerHubActions from '@ngeldata/hubs/GMServer/reducer';

const Backdrop = dynamic(() => import('@mui/material/Backdrop'), { ssr: false });
const CircularProgress = dynamic(() => import('@mui/material/CircularProgress'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });

function ConnectionLoading() {
    const gmServerHub = useAppSelector(state => state.GMServerHub);
    const dispatch = useAppDispatch();
    const [loadingState, setLoadingState] = useState<boolean>(true);
    const [loadingMessage, setLoadingMessage] = useState<string>('서버와 연결중입니다.');

    const firstRender = useRef(true);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);

    useEffect(() => {
        if (HubConnectionState.Connected !== gmServerHub.connectionState) {
            setLoadingState(true);
        } else {
            setLoadingState(false);
        }
    }, [gmServerHub.connectionState, loadingState, setLoadingState]);

    useEffect(() => {
        if (false == isEmpty(gmServerHub.serverVersion)) {
            setLoadingMessage('서버와 다시 연결중입니다.');
        } else {
            setLoadingMessage('서버와 연결중입니다.');
        }
    }, [gmServerHub.serverVersion, loadingMessage, setLoadingMessage]);

    const startReconnecting = useCallback(() => {
        switch (gmServerHub.connectionState) {
            case HubConnectionState.Disconnected:
                dispatch(gmServerHubActions.startReconnecting());
                break;

            default:
                break;
        }
    }, [gmServerHub.connectionState, dispatch]);

    const loadingContents = useCallback(() => {
        let result: JSX.Element = (
            <Grid container>
                <Grid item xs={12} textAlign='center'>
                    <CircularProgress color='inherit' />
                </Grid>
                <Grid item xs={12}>
                    <Box sx={{ margin: '0 auto' }}>
                        <Typography textAlign='center' variant="h6" color="inherit" noWrap mt={5}>{loadingMessage}</Typography>
                    </Box>
                </Grid>
            </Grid>
        );

        switch (gmServerHub.connectionState) {
            case HubConnectionState.Disconnected:
                result = (
                    <Grid container>
                        <Grid item xs={12} textAlign='center' sx={{ paddingTop: 2 }}>
                            <Box sx={{ margin: '0 auto' }}>
                                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' color='inherit' sx={{ color: '#696969', backgroundColor: 'white', ":hover": { color: 'white', backgroundColor: '#1976d2' } }} onClick={startReconnecting}>서버 재연결</Button>
                            </Box>
                        </Grid>
                    </Grid>
                );
                break;
        }
        
        return result;
    }, [gmServerHub, loadingMessage, startReconnecting]);

    const loading = useCallback(() => {
        return (
            <Backdrop sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 2 }} open={loadingState}>
                {loadingContents()}
            </Backdrop>
        );
    }, [loadingState, loadingContents]);

    return loading();
}

export default ConnectionLoading;