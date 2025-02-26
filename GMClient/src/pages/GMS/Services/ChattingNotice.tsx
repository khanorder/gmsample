import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback, Fragment } from 'react';
import { GetStaticProps } from 'next';
import { v4 as uuid } from 'uuid';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import styles from '@styles/pages/GMS/Services/states.module.sass';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, SelectChangeEvent } from '@mui/material';
import dynamic from 'next/dynamic';
import { ENoticeType } from '@ngel/data/models/lobby';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: true });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: true });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: true });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

function Page() {
    const firstRender = useRef(true);
    const [datas, setDatas] = useState<Models.ChattingNotice[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<Models.ChattingNotice[]>([]);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await GameAPI.ChattingNoticesAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                    console.log(response.error);

            return;
        }

        if (null != response.chattingNotices && 0 < response.chattingNotices.length)
            setDatas(prev => prev = deepmerge([], response.chattingNotices));

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
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if (deleteDatas && 0 < deleteDatas.length) {
            const deleteResponse = await GameAPI.DeleteChattingNoticesAsync({ chattingNotices: deleteDatas });

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

                if (!data.message) {
                    alert(`${i + 1}번째 공지내용을 입력해 주세요.`);
                    return;
                }

                if (1 > data.noticeType) {
                    alert(`${i + 1}번째 공지형식을 선택해 주세요.`);
                    return;
                }

                if (1 > data.visibleTime) {
                    alert(`${i + 1}번째 데이터에 공지 지속시간을 입력해 주세요.`);
                    return;
                }

                if (1 > data.visibleCount) {
                    alert(`${i + 1}번째 데이터에 공지 반복횟수를 입력해 주세요.`);
                    return;
                }

                if (!data.noticeTime) {
                    alert(`${i + 1}번째 데이터에 공지시간을 선택해 주세요.`);
                    return;
                }
            }

            const response = await GameAPI.SaveChattingNoticesAsync({ chattingNotices: changedDatas });
    
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
        const newData = new Models.ChattingNotice();
        newData.id = uuid();
        newData.message = "";
        newData.noticeType = ENoticeType.HoldType;
        newData.visibleTime = 3;
        newData.visibleCount = 1;
        newData.noticeTime = dayjs(dayjs().format("YYYY-MM-DD HH:mm")).toDate();
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
                prev.push(deleteData);
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

    const onChangeMessage = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].message = e.target.value ?? "";
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeNoticeType = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        let value = 0;

        if (e && e.target && e.target.value) {
            try {
                value = parseInt(e.target.value.toString());
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setDatas(prev => {
            if (prev[index]) {
                prev[index].noticeType = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeVisibleTime = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        let value = 1;
        if (e.target.value) {
            try {
                const intVisibleTime = parseInt(e.target.value);
                if (!isNaN(intVisibleTime))
                    value = intVisibleTime;
            } catch (error) {
                if ("production" != process.env.NODE_ENV)
                    console.log(error);
            }
        }
        setDatas(prev => {
            if (prev[index]) {
                prev[index].visibleTime = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeVisibleCount = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        let value = 1;
        if (e.target.value) {
            try {
                const intVisibleTime = parseInt(e.target.value);
                if (!isNaN(intVisibleTime))
                    value = intVisibleTime;
            } catch (error) {
                if ("production" != process.env.NODE_ENV)
                    console.log(error);
            }
        }
        setDatas(prev => {
            if (prev[index]) {
                prev[index].visibleCount = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeNoticeTime = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 점검 시작시간을 선택해주세요.`);
            return;
        }
        
        setDatas(prev => {
            let dayjsDate: Dayjs | null = null;
        
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return deepmerge([], prev);
            }

            if (prev[index]) {
                prev[index].noticeTime = dayjsDate.toDate();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const noticeTypeSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(ENoticeType).length)
            enumSize = Object.values(ENoticeType).length / 2;

        for (let i = 0; i < enumSize; i++) {
            list.push(<MenuItem key={i} value={i}>{ENoticeType[i]}</MenuItem>);
        }

        return list;
    }

    const onImmediatelyChattingNotice = useCallback(async (chattingNotice: Models.ChattingNotice) => {
        if (!confirm("선택한 내용이 공지됩니다.\n계속 하시겠습니까?"))
            return;

        var result = await GameAPI.ImmediatelyChattingNoticeAsync({ id: chattingNotice.id });
    }, []);

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
                    <Fragment key={i}>
                        <BorderedTableRow key={`${i}-1`} className={rowClass}>
                            <TableCell rowSpan={2}>
                                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                            </TableCell>
                            <TableCell>
                                {
                                    data.isNewData
                                    ?
                                        ""
                                    :
                                        <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onImmediatelyChattingNotice(data)}>즉시공지</Button>
                                }
                            </TableCell>
                            <TableCell>
                                <FormControl>
                                    <Select className={commonUIStyles.select} value={data.noticeType} size='small' onChange={e => onChangeNoticeType(e, i)}>
                                        {noticeTypeSelectList()}
                                    </Select>
                                </FormControl>
                            </TableCell>
                            <TableCell className={styles.inputCol}>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.visibleTime} size='small' placeholder='내용' onChange={e => onChangeVisibleTime(e, i)} />
                            </TableCell>
                            <TableCell className={styles.inputCol}>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.visibleCount} size='small' placeholder='내용' onChange={e => onChangeVisibleCount(e, i)} />
                            </TableCell>
                            <TableCell className={styles.timeCol}>
                                <DateTimePicker label={`공지일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs(data.noticeTime).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeNoticeTime(date, i)} />
                            </TableCell>
                            <TableCell>
                                {
                                    data.isNewData
                                        ?
                                            ""
                                        :
                                            dayjs(data.regTime).tz().format("YYYY-MM-DD HH:mm:ss")
                                }
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow key={`${i}-2`} className={rowClass}>
                            <TableCell colSpan={7} sx={{ borderLeft: "1px solid #d9d9d9 !important" }}>
                                <FormControl fullWidth variant='outlined'>
                                    <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.message} size='small' placeholder='내용' multiline onChange={e => onChangeMessage(e, i)} />
                                </FormControl>
                            </TableCell>
                        </BorderedTableRow>
                    </Fragment>
                );

                list.push(row);
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>관리</TableCell>
                            <TableCell>즉시공지</TableCell>
                            <TableCell>공지형식</TableCell>
                            <TableCell>지속시간(초)</TableCell>
                            <TableCell>반복횟수</TableCell>
                            <TableCell>공지일시({timezoneName})</TableCell>
                            <TableCell>등록시간({timezoneName})</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 채팅공지 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onChangeMessage, onChangeNoticeType, onChangeVisibleTime, onChangeVisibleCount, onChangeNoticeTime, onDelete, onImmediatelyChattingNotice]);
    
    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 채팅 공지내용 관리 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>채팅공지</Typography>
                </Toolbar>
                <Table className={`${styles.statesTable} ${commonUIStyles.ellipsisTable}`} stickyHeader aria-label="sticky table">
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