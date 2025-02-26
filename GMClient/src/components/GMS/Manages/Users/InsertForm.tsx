import { Models } from "@ngel/data/models";
import { ReactElement, useCallback, useEffect, useRef, useState, ChangeEvent } from "react";
import commonUIStyles from '@styles/ui/common.module.sass';
import styles from '@styles/pages/GMS/Manages/users.module.sass';
import { ManagerAPI } from "@ngel/data/apis/managerAPI";
import { useAppDispatch } from "@hooks/index";
import * as layoutsActions from '@store/reducers/layouts';
import { Errors } from "@ngel/data/autoErrors";
import { TableContainer, styled, Toolbar } from '@mui/material';
import dynamic from 'next/dynamic';
import deepmerge from "deepmerge";
import { useRouter } from "next/router";
import isEmpty from 'lodash/isEmpty';
const CopyToClipboard = dynamic(() => import("react-copy-to-clipboard"), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableRow = dynamic(() => import('@mui/material/TableRow'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormGroup = dynamic(() => import('@mui/material/FormGroup'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });
const Link = dynamic(() => import('next/link'), { ssr: false });

const ManageUserInsetForm = (): ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const [data, setData] = useState<Models.UserInfoForAdd>(new Models.UserInfoForAdd());
    const [confirmId, setConfirmId] = useState<string>("");
    const [menus, setMenus] = useState<Models.NavMenu[]>([]);
    const router = useRouter();

    const loadData = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage("메뉴 정보를 불러오는 중입니다."));
        const response = await ManagerAPI.UserMenusAsync();
        setMenus(response.menus ?? []);
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, setMenus]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current) {
            loadData().catch(error => {
                if ('production' != process.env.NODE_ENV)
                    console.log(error);
            });
            firstRender.current = false;
        }

    }, [firstRender, loadData]);
    //#endregion

    const saveData = useCallback(async () => {
        if (!data)
            return;

        if (!data.name) {
            alert("사용자 이름을 입력해주세요.");
            return;
        }

        if (!data.email) {
            alert("사용자 이메일을 입력해주세요.");
            return;
        }

        if (!data.email.match(/^[^\@]+\@smilegate\.com$/g)) {
            alert("스마일게이트 이메일만 사용할 수있습니다.");
            return;
        }

        if (data.roles.includes("최고 권한")) {
            alert(`최고 관리자 권한은 부여할 수 없습니다.`);
            return;
        }

        var response = await ManagerAPI.AddUserAsync({ userInfo: data });
        if (response.result && response.emailConfirmId) {
            alert(`'${data.name}'님을 관리자로 추가했습니다.`);
            setConfirmId(response.emailConfirmId);
        } else {
            switch (response.error) {
                // case Errors.ManagesSaveUserRole_CanNotModifySuperUser:
                //     alert(`최고 관리자 권한은 수정할 수 없습니다.`);
                //     break;

                // case Errors.ManagesSaveUserRole_CanNotAddSuperRole:
                //     alert(`최고 관리자 권한은 부여할 수 없습니다.`);
                //     break;

                default:
                    alert(`관리자를 추가하는데 실패했습니다.`);
            }
        }
    }, [data, setConfirmId]);

    const toggleRole = useCallback((menu: Models.NavMenu) => {
        if (!data)
            return;

        if (!menu.policyName)
            return;

        if ("Administrator" == menu.policyName)
            return;

        if (0 < menu.children.length) {
            for (let i = 0; i < menu.children.length; i++) {
                const subMenu = menu.children[i];
                toggleRole(subMenu);
            }
        } else {
            setData(prev => {
                if (prev.roles.includes(menu.policyName)) {
                    const idx = prev.roles.findIndex(_ => _ == menu.policyName);
                    if (-1 < idx)
                        prev.roles.splice(idx, 1);
                } else {
                    prev.roles.push(menu.policyName);
                }
                return new Models.UserInfoForAdd(prev);
            });
        }

    }, [data, setData]);

    const onChangeName = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setData(prev => {
            if (!prev)
                return prev;

            prev.name = e.target.value ?? "";
            return new Models.UserInfoForAdd(prev);
        });
    }, [setData]);

    const onChangeEmail = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setData(prev => {
            if (!prev)
                return prev;

            prev.email = e.target.value ?? "";
            return new Models.UserInfoForAdd(prev);
        });
    }, [setData]);

    const toggleWholeMenu = useCallback(() => {
        if (!data)
            return;

        if (1 > menus.length) {
            setData(prev => {
                prev.roles = [];
                return new Models.UserInfoForAdd(prev);
            });
            return;
        }

        for (let i = 0; i < menus.length; i++) {
            const menu = menus[i];
            toggleRole(menu);
        }

    }, [data, menus, setData, toggleRole]);

    const menuSelectList = useCallback(() => {
        let menuCount = 0;
        let authorizedMenuCount = 0;

        if (0 < menus.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < menus.length; i++) {
                const menu = menus[i];
                let children: ReactElement[] = [];
                const checked = data && menu.roles.some(role => data.roles.includes(role)) ? true : false;
                menuCount++;
                if (checked)
                    authorizedMenuCount++;

                if (0 < menu.children.length) {
                    for (let j = 0; j < menu.children.length; j++) {
                        const subMenu = menu.children[j];
                        const checkedSub = data && subMenu.roles.some(role => data.roles.includes(role)) ? true : false;
                        menuCount++;
                        if (checkedSub)
                            authorizedMenuCount++;

                        children.push(
                            <FormGroup key={`${i}-${j}`}>
                                <FormControlLabel control={<Checkbox size="small" checked={checkedSub} onClick={() => toggleRole(subMenu)} />} label={subMenu.name} />
                            </FormGroup>
                        );
                    }
                }

                list.push(
                    <TableRow key={i}>
                        <TableCell>
                            <FormGroup>
                                <FormControlLabel control={<Checkbox size="small" checked={checked} onClick={() => toggleRole(menu)} />} label={menu.name} />
                            </FormGroup>
                        </TableCell>
                        <TableCell>{children}</TableCell>
                    </TableRow>
                );
            }

            return (
                <>
                    <TableRow>
                        <TableCell rowSpan={menus.length + 1}>
                            <FormGroup>
                                <FormControlLabel control={<Checkbox size="small" checked={menuCount == authorizedMenuCount} onClick={() => toggleWholeMenu()} />} label="전체" />
                            </FormGroup>
                        </TableCell>
                    </TableRow>
                    {list}
                </>
            );
        } else {
            return <></>;
        }
    }, [data, menus, toggleRole, toggleWholeMenu]);

    const contents = useCallback(() => {
        return (
            <>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell component="th" scope="row">이름</TableCell>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.name} size='small' placeholder='이름' onChange={e => onChangeName(e)} />
                            </FormControl>
                        </TableCell>
                        <TableCell component="th" scope="row">이메일</TableCell>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.email} size='small' placeholder='이메일 ex) abcd@smilegate.com' onChange={e => onChangeEmail(e)} />
                            </FormControl>
                        </TableCell>
                    </BorderedTableRow>
                    <BorderedTableRow>
                        <TableCell component="th" scope="row">권한</TableCell>
                        <TableCell className={styles.menuCol} colSpan={5}>
                            <Table className={styles.menuTable}>
                                <TableBody>
                                    {menuSelectList()}
                                </TableBody>
                            </Table>
                        </TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>
        );
    }, [data, menuSelectList, onChangeEmail, onChangeName]);

    if (confirmId) {
        return (
            <>
                <Grid sx={{ textAlign: 'right', padding: '10px 0' }}>
                    <Link href="/GMS/Manages/Users"><Button variant="outlined" color="inherit" size="small" sx={{ ml: 1 }}>확인</Button></Link>
                </Grid>
                <TableContainer component={Paper} elevation={4}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>계정추가 완료</Typography>
                    </Toolbar>
                    <Table className={styles.formTable}>
                        <TableBody>
                            <BorderedTableRow>
                                <TableCell component="th" scope="row">이름</TableCell>
                                <TableCell>{data.name}</TableCell>
                                <TableCell component="th" scope="row">이메일</TableCell>
                                <TableCell>{data.email}</TableCell>
                                <TableCell>
                                    <CopyToClipboard text={`${window.origin}/NGAuth/AccountConfirm?id=${confirmId}`} onCopy={() => { alert(`'${window.origin}/NGAuth/AccountConfirm?id=${confirmId}' 비밀번호 초기화링크 복사.`) }}>
                                        <Button variant="outlined" color="inherit" size="small">비밀번호 초기화링크</Button>
                                    </CopyToClipboard>
                                </TableCell>
                            </BorderedTableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
                <Grid sx={{ textAlign: 'right', padding: '10px 0' }}>
                    <Link href="/GMS/Manages/Users"><Button variant="outlined" color="inherit" size="small" sx={{ ml: 1 }}>확인</Button></Link>
                </Grid>
            </>
        );
    } else {
        return (
            <>
                <Grid sx={{ textAlign: 'right', padding: '10px 0' }}>
                    <Button onClick={saveData} variant="contained" size="small">저장</Button>
                    <Link href="/GMS/Manages/Users"><Button variant="outlined" color="inherit" size="small" sx={{ ml: 1 }}>취소</Button></Link>
                </Grid>
                <TableContainer component={Paper} elevation={4}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>계정추가</Typography>
                    </Toolbar>
                    <Table className={styles.formTable}>
                        {contents()}
                    </Table>
                </TableContainer>
                <Grid sx={{ textAlign: 'right', padding: '10px 0' }}>
                    <Button onClick={saveData} variant="contained" size="small">저장</Button>
                    <Link href="/GMS/Manages/Users"><Button variant="outlined" color="inherit" size="small" sx={{ ml: 1 }}>취소</Button></Link>
                </Grid>
            </>
        );
    }
}

export default ManageUserInsetForm;