import { useAppSelector } from "@hooks/index";
import { LayoutProps } from '@defines/index';
import { useCallback, useEffect, useRef } from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import dynamic from 'next/dynamic';
import ConnectionLoading from "@components/commons/connectionLoading";
import { useRouter } from "next/router";
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const CssBaseline = dynamic(() => import('@mui/material/CssBaseline'), { ssr: false });
const DevViewPortInfo = dynamic(() => import('./devViewPortInfo'), { ssr: false });

const mdTheme = createTheme();

function ThemeLayout({ children }: LayoutProps) {
    const gmServerHub = useAppSelector(state => state.GMServerHub);
    const user = useAppSelector(state => state.user);
    const firstRender = useRef(true);
    const router = useRouter();

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    useEffect(() => {
        if (false == firstRender.current && user.signinId && !router.pathname.startsWith(process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS')) {
            router.push(process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS');
        }

    }, [firstRender, user.signinId, router]);

    const childrenContents = useCallback(() => {
        return children;
    }, [children]);

    return (
        <>
            <ThemeProvider theme={mdTheme}>
                <Box sx={{ display: 'flex', height: '100%' }}>
                    <CssBaseline />
                    <ConnectionLoading />
                    {childrenContents()}
                </Box>
            </ThemeProvider>
            <DevViewPortInfo />
        </>
    );
}

export default ThemeLayout;