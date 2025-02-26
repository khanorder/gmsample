import { useCallback, useEffect, useRef, useState } from 'react';
import { useAppSelector } from "@hooks/index";
import { LayoutProps } from '@defines/index';
import { useRouter } from 'next/router';
import { Models } from '@ngel/data/models';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
const Loading = dynamic(() => import('@components/commons/loading'), { ssr: false });
const AuthLoading = dynamic(() => import('@components/commons/authLoading'), { ssr: false });
const Divider = dynamic(() => import('@mui/material/Divider'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Container = dynamic(() => import('@mui/material/Container'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const UIContainer = dynamic(() => import('./ui/uiContainer'), { ssr: false });

function ManageLayout({ children }: LayoutProps) {
    const user = useAppSelector(state => state.user);
    const firstRender = useRef(true);
    const router = useRouter();
    const [currentMenu, setCurrentMenu] = useState<Models.UserMenu | null>();

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

        const menuPath = user.menusLinear.find(_ => `${(process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS")}${_.path}` == router.pathname);

        if (menuPath) {
            setCurrentMenu(menuPath);
        } else {
            setCurrentMenu(null);
        }

    }, [firstRender, user.menusLinear, router, setCurrentMenu]);
    //#endregion

    const contents = useCallback(() => {
        return (
            router.pathname == (process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS")
                ?
                <>{children}</>
                :
                <>
                    {currentMenu ? <h1>{currentMenu.name}</h1> : ""}
                    <Divider variant="middle" sx={{ m: 0 }} />
                    {currentMenu ? children : ""}
                </>
        );
    }, [currentMenu, children, router]);

    return (
        <>
            {
                isEmpty(user.signinId)
                    ?
                    <AuthLoading />
                    :
                    <></>
            }
            <Loading />
            <UIContainer />
            <Box component="main" sx={{ flexGrow: 1, height: '100vh', overflow: 'auto', }}>
                <Toolbar />
                <Container sx={{ margin: "0px 25px 0px 25px !important", padding: '0px !important', maxWidth: 'calc(100% - 50px) !important', minWidth: 300 }}>
                    {contents()}
                </Container>
            </Box>
        </>
    );
}

export default ManageLayout;