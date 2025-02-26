import { useCallback, useEffect, useState } from 'react';
import { useAppSelector } from '@hooks/index';
import isEmpty from 'lodash/isEmpty';
import dynamic from 'next/dynamic';
import { HubConnectionState } from '@microsoft/signalr';
const Backdrop = dynamic(() => import('@mui/material/Backdrop'), { ssr: false });
const CircularProgress = dynamic(() => import('@mui/material/CircularProgress'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

function AuthLoading() {
    const user = useAppSelector(state => state.user);
    const GMServerHub = useAppSelector(state => state.GMServerHub);
    const [loadingState, setLoadingState] = useState<boolean>(true);

    useEffect(() => {
        if (HubConnectionState.Connected == GMServerHub.connectionState && isEmpty(user.signinId)) {
            setLoadingState(true);
        } else {
            setLoadingState(false);
        }
    }, [setLoadingState, user, GMServerHub]);

    const loading = useCallback(() => {
        return (
            <Backdrop sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 2 }} open={loadingState}>
                <Grid container>
                    <Grid item xs={12} textAlign='center'>
                        <CircularProgress color='inherit' />
                    </Grid>
                    <Grid item xs={12}>
                        <Box sx={{ margin: '0 auto' }}>
                            <Typography textAlign='center' variant="h6" color="inherit" noWrap mt={5}>인증 정보를 확인중입니다.</Typography>
                        </Box>
                    </Grid>
                </Grid>
            </Backdrop>
        );
    }, [loadingState]);

    return loading();
}

export default AuthLoading;