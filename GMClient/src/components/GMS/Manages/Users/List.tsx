import { Models } from "@ngel/data/models";
import { ReactElement, useCallback, useEffect, useRef, useState } from "react";
import commonUIStyles from '@styles/ui/common.module.sass';
import { ManagerAPI } from "@ngel/data/apis/managerAPI";
import { useAppDispatch } from "@hooks/index";
import * as layoutsActions from '@store/reducers/layouts';
import { Errors } from "@ngel/data/autoErrors";
import { TableContainer, styled, Toolbar, Typography, Box } from '@mui/material';
import dynamic from 'next/dynamic';
import { useRouter } from "next/router";
import { Defines } from "@ngel/data/autoDefines";
import { ManagerAPIModels } from "@ngel/data/models/managerAPIModels";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
const CopyToClipboard = dynamic(() => import("react-copy-to-clipboard"), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Link = dynamic(() => import('next/link'), { ssr: false });

const ManageUserList = (): ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const [data, setData] = useState<Models.UserInfoForManage[]>([]);

    const loadData = useCallback(async () => {
        dispatch(layoutsActions.startLoadingMessage("관리자 정보를 불러오는 중입니다."));
        const response = await ManagerAPI.UserInfosAsync();
        setData(response.userInfos);
        dispatch(layoutsActions.stopLoading());
    }, [dispatch, setData])

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

    const onDeleteUser = useCallback(async (user: Models.UserInfo) => {
        if (!user.id) {
            alert("삭제할 계정의 고유정보가 필요합니다.");
            return;
        }

        if (user.isDeleted) {
            if (!confirm(`'${user.name}'님의 계정을 완전히 삭제하시겠습니까?\n완전히 삭제한 계정은 복구할 수 없습니다.`)) {
                return false;
            }
        } else if (!confirm(`'${user.name}'님의 계정을 정말 삭제하시겠습니까?`)) {
            return false;
        }

        const response = await ManagerAPI.DeleteUserAsync({ userId: user.id });
        if (response.result) {
            alert(`'${user.name}'님의 계정을 삭제했습니다.`);
            await loadData();
        } else {
            switch (response.error) {
                case Errors.ManagesDeleteUser_CanNotDelSuperUser:
                    alert('관리자 계정은 삭제할 수 없습니다.');
                    break;

                default:
                    alert('삭제에 실패했습니다.');
            }
        }
    }, [loadData]);

    const onRestoreUser = useCallback(async (user: Models.UserInfo) => {
        if (!user.id) {
            alert("복원 할 계정의 고유정보가 필요합니다.");
            return;
        }

        if (!user.isDeleted) {
            alert("삭제된 계정이 아닙니다.");
            return;
        }

        if (!confirm(`'${user.name}'님의 계정을 복원하시겠습니까?`)) {
            return;
        }

        const response = await ManagerAPI.RestoreUserAsync({ userId: user.id });
        if (response.result) {
            alert(`'${user.name}'님의 계정을 복원했습니다.`);
            await loadData();
        } else {
            alert('복원에 실패했습니다.');
        }
    }, [loadData]);

    const onResetCountFailedSignin = useCallback(async (user: Models.UserInfo) => {
        if (!user.id) {
            alert("로그인 실패 횟수를 초기화 할 계정의 고유정보가 필요합니다.");
            return;
        }

        if (!confirm(`'${user.name}'님의 계정의 로그인 실패 횟수를 초기화 하시겠습니까?`)) {
            return;
        }

        const response = await ManagerAPI.ResetCountFailedSigninAsync({ userId: user.id });
        if (response.result) {
            alert(`'${user.name}'님의 계정의 로그인 실패 횟수를 초기화 했습니다.`);
            await loadData();
        } else {
            alert('초기화에 실패했습니다.');
        }
    }, [loadData]);

    const onResetLatestSignIn = useCallback(async (user: Models.UserInfo) => {
        if (!user.id) {
            alert("휴면 상태를 초기화 할 계정의 고유정보가 필요합니다.");
            return;
        }

        if (!confirm(`'${user.name}'님의 계정의 휴면 상태를 초기화 하시겠습니까?`)) {
            return;
        }

        const response = await ManagerAPI.ResetLatestSignInAsync({ userId: user.id });
        if (response.result) {
            alert(`'${user.name}'님의 계정의 휴면 상태를 해제 했습니다.`);
            await loadData();
        } else {
            alert('초기화에 실패했습니다.');
        }
    }, [loadData]);

    const addUser = useCallback(() => {
        router.push("?mode=insert");
    }, [router]);

    const onResetUserPassword = useCallback(async (userInfo: Models.UserInfoForManage) => {
        if (!confirm(`'${userInfo.name}' 계정의 비밀번호를 초기화 상태로 바꾸시겠습니까?`))
            return;

        var response = new ManagerAPIModels.ManagerSetRefreshPasswordResponses();
        try {
            response = await ManagerAPI.SetRefreshPasswordAsync({ email: userInfo.emails[0] });
        } catch (error) {
            console.log(error);
        }

        if (response.result) {
            alert("비밀번호를 초기화했습니다.");
            await loadData();
        } else {
            alert(`인증실패(error:${response.error})`);
        }
    }, [loadData]);

    const contents = () => {
        if (data && 0 < data.length) {
            const list: ReactElement[] = [];
    
            for (let i = 0; i < data.length; i++) {
                const userInfo = data[i];
                let emails = "";
                let isConfirmed = true;
                let confirmId = "";
                if (userInfo.emails && 0 < userInfo.emails.length) {
                    for (let j = 0; j < userInfo.emails.length; j++) {
                        emails += (emails ? ', ' : '') + userInfo.emails[j];
                    }
                }
        
                let roles = "";
                if (userInfo.roles && 0 < userInfo.roles.length) {
                    for (let j = 0; j < userInfo.roles.length; j++) {
                        roles += (roles ? ', ' : '') + userInfo.roles[j];
                    }
                }
    
                list.push(
                    <BorderedTableRow key={i} className={(userInfo.isDeleted ? commonUIStyles.deletedRow : "")}>
                        <TableCell>{data.length - i}</TableCell>
                        <TableCell>{emails.startsWith("admin@") || emails.startsWith("test@") ? emails : emails.replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{emails.startsWith("admin@") || emails.startsWith("test@") ? userInfo.name : userInfo.name.replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>
                            {
                                Defines.OAuthProvider.CustomEmail == userInfo.provider
                                    ?
                                        `${userInfo.countFailedSignin}/5`
                                    :
                                        ""
                            }
                            {
                                Defines.OAuthProvider.CustomEmail == userInfo.provider && 0 < userInfo.countFailedSignin
                                    ?
                                        <>
                                            <br />
                                            <Button onClick={async (e) => await onResetCountFailedSignin(userInfo)} variant="outlined" color="inherit" size="small" sx={{ ml: 1, lineHeight: '1.2rem' }}>초기화</Button>
                                        </>
                                    :
                                        ""
                            }
                        </TableCell>
                        <TableCell>
                            {dayjs.tz(userInfo.latestSignin).format("YYYY-MM-DD HH:mm:ss")}
                            {
                                7776000 <= dayjs.utc().diff(dayjs.utc(userInfo.latestSignin), 'seconds')
                                    ?
                                        <>
                                            <br />
                                            <Button onClick={async (e) => await onResetLatestSignIn(userInfo)} variant="outlined" color="inherit" size="small" sx={{ ml: 1, lineHeight: '1.2rem' }}>휴면 초기화</Button>
                                        </>
                                    :
                                        <></>
                            }
                        </TableCell>
                        <TableCell>
                            {dayjs(userInfo.latestSignout).format("YYYY-MM-DD HH:mm:ss")}
                        </TableCell>
                        <TableCell>{roles}</TableCell>
                        <TableCell>
                            {
                                userInfo.isDeleted
                                    ?
                                        '삭제'
                                    :
                                        !userInfo.isEmailConfirmed && userInfo.emailConfirmId
                                            ?
                                            <CopyToClipboard text={`${window.origin}/NGAuth/AccountConfirm?id=${userInfo.emailConfirmId}`} onCopy={() => { alert(`'${window.origin}/NGAuth/AccountConfirm?id=${userInfo.emailConfirmId}' 비밀번호 초기화링크 복사.`) }}>
                                                <Button variant="outlined" color="inherit" size="small">비밀번호 초기화링크</Button>
                                            </CopyToClipboard>
                                            :
                                                ""
                            }
                        </TableCell>
                        <TableCell>
                            <Link href={`/GMS/Manages/Users?mode=edit&id=${userInfo.id}`}>
                                <Button variant='outlined' size='small' sx={{ lineHeight: '1.2rem' }}>권한</Button>
                            </Link>
                            <Button onClick={async (e) => await onDeleteUser(userInfo)} variant='outlined' color='error' size='small' sx={{ ml: 1, lineHeight: '1.2rem' }}>삭제</Button>
                            {
                                userInfo.isDeleted
                                    ?
                                        <Button onClick={async (e) => await onRestoreUser(userInfo)} variant='outlined' color='success' size='small' sx={{ ml: 1, lineHeight: '1.2rem' }}>복원</Button>
                                    :
                                        <></>
                            }
                            {
                                Defines.OAuthProvider.CustomEmail == userInfo.provider && userInfo.isEmailConfirmed
                                    ?
                                    <Button onClick={async (e) => await onResetUserPassword(userInfo)} variant='outlined' color='info' size='small' sx={{ ml: 1, lineHeight: '1.2rem' }}>비번 초기화</Button>
                                    :
                                        <></>
                            }
                        </TableCell>
                    </BorderedTableRow>
                );
            }
    
            return (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>순번</TableCell>
                            <TableCell>이메일</TableCell>
                            <TableCell>이름</TableCell>
                            <TableCell>로그인 실패</TableCell>
                            <TableCell>마지막 로그인</TableCell>
                            <TableCell>마지막 로그아웃</TableCell>
                            <TableCell>권한</TableCell>
                            <TableCell>상태</TableCell>
                            <TableCell>관리</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        }
    
        return (
            <>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 관리자 계정이 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>
        );
    };

    return (
        <>
            {/* <Grid sx={{ textAlign: 'right', padding: '10px 0' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addUser}>추가</Button>
            </Grid> */}
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 계정 접근권한 관리 기능.</Typography>
            </Box>
            <TableContainer component={Paper} elevation={4}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6'>관리자 계정</Typography>
                </Toolbar>
                <Table>
                    {contents()}
                </Table>
            </TableContainer>
            {/* <Grid sx={{ textAlign: 'right', padding: '10px 0' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addUser}>추가</Button>
            </Grid> */}
        </>
    );
}

export default ManageUserList;