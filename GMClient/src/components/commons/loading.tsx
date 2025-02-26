import { useEffect, useState } from 'react';
import { useAppSelector } from '@hooks/index';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
import { HubConnectionState } from '@microsoft/signalr';

const Backdrop = dynamic(() => import('@mui/material/Backdrop'), { ssr: false });
const CircularProgress = dynamic(() => import('@mui/material/CircularProgress'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Link = dynamic(() => import('next/link'), { ssr: false });

function Loading() {
    const gmServerHub = useAppSelector(state => state.GMServerHub);
    const layouts = useAppSelector(state => state.layouts);

    const [loadingState, setLoadingState] = useState<boolean>(true);
    const [loadingMessage, setLoadingMessage] = useState<string>('');

    useEffect(() => {
        if (layouts.loadingActive && HubConnectionState.Connected === gmServerHub.connectionState) {
            setLoadingState(true);
        } else {
            setLoadingState(false);
        }
    }, [layouts.loadingActive, gmServerHub.connectionState, loadingState, setLoadingState]);

    useEffect(() => {
        if (isEmpty(layouts.loadingMessage)) {
            setLoadingMessage('');
        } else {
            setLoadingMessage(layouts.loadingMessage);
        }
    }, [layouts.loadingMessage, loadingMessage, setLoadingMessage]);

    return (
        <>
            <Backdrop sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 2 }} open={loadingState}>
                <Grid container>
                    <Grid item xs={12} textAlign='center'>
                        <CircularProgress color='inherit' />
                    </Grid>
                    <Grid item xs={12}>
                        <Box sx={{ margin: '0 auto' }}>
                            <Typography textAlign='center' variant="h6" color="inherit" noWrap mt={5}>{loadingMessage}</Typography>
                        </Box>
                    </Grid>
                    {
                        layouts.cancelRedirectUrl
                            ?
                            <Grid item xs={12} textAlign='center' sx={{ paddingTop: 2 }}>
                                <Box sx={{ margin: '0 auto' }}>
                                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='contained' sx={{ backgroundColor: 'white', color: '#797979' }}>
                                        <Link href={layouts.cancelRedirectUrl}>{(isEmpty(layouts.cancelString) ? '취소' : layouts.cancelString)}</Link>
                                    </Button>
                                </Box>
                            </Grid>
                            :
                            <></>
                    }
                </Grid>
            </Backdrop>
        </>
    );
}

export default Loading;