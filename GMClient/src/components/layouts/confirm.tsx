import { useEffect, useRef } from 'react';
import { useAppSelector } from "@hooks/index";
import { LayoutProps } from '@defines/index';
import dynamic from 'next/dynamic';
const Container = dynamic(() => import('@mui/material/Container'), { ssr: false });
const Loading = dynamic(() => import('@components/commons/loading'), { ssr: false });

function ConfirmLayout({ children }: LayoutProps) {
    const layouts = useAppSelector(state => state.layouts);
    const firstRender = useRef(true);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    if (layouts.loadingActive) {
        return (
            <>
                <Loading />
            </>
        );
    } else {
        return (
            <>
                <Container maxWidth="xs" sx={{ paddingTop: '60px' }}>
                    {children}
                </Container>
            </>
        );
    }

}

export default ConfirmLayout;