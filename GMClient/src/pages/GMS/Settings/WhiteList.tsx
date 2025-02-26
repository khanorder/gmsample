import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { GetStaticProps } from 'next';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { ManagerAPI } from '@ngel/data/apis/managerAPI';
import isEmpty from 'lodash/isEmpty';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

function Page() {
    const firstRender = useRef(true);
    const [datas, setDatas] = useState<Models.WhiteList[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<number[]>([]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await ManagerAPI.WhiteListAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            return;
        }

        setDatas(prev => {
            prev = response.resultData;
            return deepmerge([], prev);
        });

    }, [setDatas, setDeleteDatas]);

    useEffect(() => {
        loadDatas();

    }, [loadDatas]);

    const onReload = useCallback(async () => {
        const changed = datas.filter(_ => _.isChanged);
        if ((changed && changed.length > 0) || (deleteDatas && deleteDatas.length > 0)) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();

    }, [loadDatas, datas, deleteDatas]);


    const onSaveData = useCallback(async () => {
        const changedDatas = datas.filter(_ => _.isChanged);

        if ((!changedDatas || 1 > changedDatas.length) && (!deleteDatas || 1 > deleteDatas.length)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }

        if ((deleteDatas && 0 < deleteDatas.length)) {
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        else {
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if (deleteDatas && 0 < deleteDatas.length) {
            console.log(1);
            const deleteResponse = await ManagerAPI.DeleteWhiteListAsync({ paramData: deleteDatas });

            if (!deleteResponse) {
                alert(`삭제 오류!`);
                return;
            }

            if (!deleteResponse.result) {
                alert(`삭제 오류! (error: ${Errors[deleteResponse.error]}, index: ${deleteResponse.errorIndex})`);
                return;
            }
        }

        if (changedDatas && 0 < changedDatas.length) {
            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];
                if (!data.isChanged)
                    continue;

                if (isEmpty(data.DeviceID) && (isEmpty(data.MemberNo.toString()) || data?.MemberNo <= 0)) {
                    alert(`${i + 1}번째 데이터에 기기 ID 또는 멤버 번호를 입력해 주세요.`);
                    return;
                }

                const existsDatas = datas.filter(_ => _.DeviceID == data.DeviceID && _.MemberNo == data.MemberNo);
                if (null != existsDatas && 1 < existsDatas.length) {
                    alert(`${i + 1}번째 데이터는 중복된 내용입니다.\n기기 ID: ${data.DeviceID}\n멤버 번호: ${data.MemberNo}`);
                    return;
                }

                if (isEmpty(data.Comment)) {
                    alert(`${i + 1}번째 데이터에 코멘트를 입력해 주세요.`);
                    return;
                }
            }

            for (let j = 0; j < changedDatas.length; j++) {
                changedDatas[j].DeviceID = changedDatas[j].DeviceID.trim();
                changedDatas[j].Comment = changedDatas[j].Comment.trim();
            }

            const response = await ManagerAPI.SaveWhiteListAsync({ paramData: changedDatas });

            if (!response) {
                alert(`오류!`);
                return;
            }

            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert(`저장 했습니다.`);
        await loadDatas();
    }, [loadDatas, datas, deleteDatas]);

    const addData = useCallback(() => {
        const newData = new Models.WhiteList();
        newData.DeviceID = "";
        newData.MemberNo = 0;
        newData.Comment = ""
        newData.isChanged = true;
        newData.isNewData = true;

        setDatas(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onDelete = useCallback((index: number) => {
        const deleteData = datas[index];

        if (!deleteData) {
            alert(`${index + 1}번째 데이터가 없습니다.`);
            return;
        }

        if (!deleteData.isNewData) {
            setDeleteDatas(prev => {
                prev.push(deleteData.ID);
                return deepmerge([], prev);
            });
        }

        setDatas(prev => {
            if (prev[index]) {
                prev.splice(index, 1);
            }
            return deepmerge([], prev);
        });
    }, [datas, setDatas]);

    const onChangeDeviceID = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].DeviceID = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);
    const onChangeMemberNumber = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].MemberNo = e.target.value ? (parseInt(e.target.value) < 0 ? 0 : parseInt(e.target.value)) : 0;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeComment = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].Comment = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (0 < datas.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];
                let rowClass: string = commonUIStyles.row;
                if (data.isChanged)
                    rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

                const row = (
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            {data.ID && 0 < data.ID ? data.ID : ''}
                        </TableCell>
                        <TableCell>
                            {data.isNewData ?
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.DeviceID} size='small' placeholder='기기ID' onChange={e => onChangeDeviceID(e, i)} />
                                :
                                data.DeviceID
                            }
                        </TableCell>
                        <TableCell>
                            {data.isNewData ?
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth type='number' value={data.MemberNo} size='small' placeholder='멤버 번호' onChange={e => onChangeMemberNumber(e, i)} />
                                :
                                data.MemberNo
                            }
                        </TableCell>
                        <TableCell>
                            {data.isNewData ?
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.Comment} size='small' placeholder='코멘트' onChange={e => onChangeComment(e, i)} />
                                :
                                data.Comment
                            }
                        </TableCell>
                    </BorderedTableRow>
                );

                list.push(row);
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>ID</TableCell>
                            <TableCell>기기ID</TableCell>
                            <TableCell>멤버 번호</TableCell>
                            <TableCell>코멘트</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 화이트 리스트 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onDelete, onChangeComment, onChangeDeviceID, onChangeMemberNumber]);

    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 점검 및 접속 제한 상황에도 접속 가능한 예외 계정/디바이스 관리 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>화이트 리스트</Typography>
                    {/* <IconButton edge="end" onClick={loadDatas}><RefreshIcon /></IconButton> */}
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Grid sx={{ padding: '20px 0 10px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
        </Box>
    );
};

Page.getLayout = function getLayout(page: ReactElement) {
    return (
        <Layout>
            <ThemeLayout>
                <ManageLayout>{page}</ManageLayout>
            </ThemeLayout>
        </Layout>
    );
}

export const getStaticProps: GetStaticProps = async (context) => {
    return {
        props: {
        }
    };
}

export default Page;