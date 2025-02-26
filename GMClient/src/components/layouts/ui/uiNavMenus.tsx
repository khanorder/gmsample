import { useCallback, useEffect, useRef } from 'react';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import { useRouter } from 'next/router';
import { Models } from '@ngel/data/models';
import * as userActions from '@store/reducers/user';
import { v4 as uuidv4 } from 'uuid';
import styles from '@styles/layouts/leftBar.module.sass';
import dynamic from 'next/dynamic';
const Link = dynamic(() => import('next/link'), { ssr: false });
const List = dynamic(() => import('@mui/material/List'), { ssr: false });
const ListItem = dynamic(() => import('@mui/material/ListItem'), { ssr: false });
const ListItemButton = dynamic(() => import('@mui/material/ListItemButton'), { ssr: false });
const ListItemText = dynamic(() => import('@mui/material/ListItemText'), { ssr: false });
const Collapse = dynamic(() => import('@mui/material/Collapse'), { ssr: false });
const ExpandMore = dynamic(() => import('@mui/icons-material/ExpandMore'), { ssr: false });
const ExpandLess = dynamic(() => import('@mui/icons-material/ExpandLess'), { ssr: false });

function UINavMenus() {
    const user = useAppSelector(state => state.user);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const rootPath = process.env.NEXT_PUBLIC_MANAGE_PATH ?? "/GMS"

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);

    const selectMenu = useCallback((menu: Models.UserMenu, indexDepth1: number, indexDepth2?: number, indexDepth3?: number) => {
        dispatch(userActions.openMenu([indexDepth1, indexDepth2, indexDepth3]));
        // if (1 > menu.children.length)
        //     router.push(`${rootPath}${menu.path}`, `${rootPath}${menu.path}`, { shallow: true });
    }, [dispatch])

    const menuList = useCallback((): JSX.Element => {
        if (null == user.menus || 1 > user.menus.length)
            return <></>;

        const menuItemList: JSX.Element[][] = [];

        for (let i = 0; i < user.menus.length; i++) {
            const menuDepth1 = user.menus[i];
            if (!menuDepth1)
                continue;

            let selectedDepth1 = 0 < menuDepth1.children.length ? router.pathname.startsWith(`${rootPath}${menuDepth1.path}`) : router.pathname.replace(/\/$/, '') == `${rootPath}${menuDepth1.path}`.replace(/\/$/, '');
            const menuItemDepth1: JSX.Element[] = [];
            let menuItemDepth1Child: JSX.Element|null = null;
            let menuItemDepth1Expend: JSX.Element|null = null;

            if (0 < menuDepth1.children.length) {
                const menuItemDepth2: JSX.Element[] = [];
                for (let j = 0; j < menuDepth1.children.length; j++) {
                    const menuDepth2 = menuDepth1.children[j];
                    if (!menuDepth2)
                        continue;

                    let selectedDepth2 = 0 < menuDepth2.children.length ? router.pathname.startsWith(`${rootPath}${menuDepth2.path}`) : router.pathname.replace(/\/$/, '') == `${rootPath}${menuDepth2.path}`.replace(/\/$/, '');
                    menuItemDepth2.push(
                        <ListItem key={uuidv4()} className={styles.menuItem} sx={{ p: 0 }}>
                            <ListItemButton className={`${styles.menuButton}${(selectedDepth2 ? " " + styles.menuButtonSelected : "")}`} sx={{ pl: 6 }} selected={selectedDepth2} onClick={() => selectMenu(menuDepth2, i + 1, j + 1)}>
                                <ListItemText className={styles.menuItemText} title={menuDepth2.name}>
                                    {
                                        0 < menuDepth2.children.length
                                            ?
                                                menuDepth2.name
                                            :
                                            <Link className={styles.menuLink} href={`${rootPath}${menuDepth2.path}`} title={menuDepth2.name}>{menuDepth2.name}</Link>            
                                    }
                                </ListItemText>
                            </ListItemButton>
                        </ListItem>
                    );
                }

                menuItemDepth1Child = (
                    <ListItem key={uuidv4()} className={styles.menuItem} sx={{ p: 0 }}>
                        <Collapse in={menuDepth1.opened} sx={{ p: 0, width: '100%' }} timeout="auto" unmountOnExit>
                            <List className={styles.menuSubList}>
                                {menuItemDepth2}
                            </List>
                        </Collapse>
                    </ListItem>
                );

                menuItemDepth1Expend = menuDepth1.opened ? <ExpandMore /> : <ExpandLess />;
            }

            menuItemDepth1.push(
                <ListItem key={uuidv4()} className={styles.menuItem} sx={{ p: 0 }}>
                    <ListItemButton className={`${styles.menuButton}${(selectedDepth1 ? " " + styles.menuButtonSelected : "")}`} sx={{ pl: 3 }} selected={selectedDepth1} onClick={() => selectMenu(menuDepth1, i + 1)}>
                        <ListItemText className={styles.menuItemText} title={menuDepth1.name}>
                            {
                                0 < menuDepth1.children.length
                                    ?
                                        menuDepth1.name
                                    :
                                        <Link className={styles.menuLink} href={`${rootPath}${menuDepth1.path}`} title={menuDepth1.name}>{menuDepth1.name}</Link>            
                            }
                        </ListItemText>
                        {(menuItemDepth1Expend ?? <></>)}
                    </ListItemButton>
                </ListItem>
            );

            if (menuItemDepth1Child)
                menuItemDepth1.push(menuItemDepth1Child);

            menuItemList.push(menuItemDepth1);
        }

        return (
            <List className={styles.menuList} sx={{ flexGrow: 1 }}>
                {menuItemList}
            </List>
        );
    }, [user, selectMenu, router, rootPath]);

    return menuList();
}

export default UINavMenus;