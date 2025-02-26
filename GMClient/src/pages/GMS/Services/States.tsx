import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { GetStaticProps } from 'next';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import deepmerge from 'deepmerge';
import styles from '@styles/pages/GMS/Services/states.module.sass';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, SelectChangeEvent } from '@mui/material';
import dynamic from 'next/dynamic';
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
    const [datas, setDatas] = useState<Models.Maintenance[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<Models.Maintenance[]>([]);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await GameAPI.MaintenancesAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                    console.log(response.error);

            return;
        }

        if (null != response.maintenances && 0 < response.maintenances.length)
            setDatas(prev => prev = deepmerge([], response.maintenances));

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
            const deleteResponse = await GameAPI.DeleteMaintenancesAsync({ maintenances: deleteDatas });

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

                if (!data.Area) {
                    alert(`${i + 1}번째 데이터에 지역을 입력해 주세요.`);
                    return;
                }

                if (0 > data.Platform) {
                    alert(`${i + 1}번째 데이터에 플랫폼을 선택해 주세요.`);
                    return;
                }

                if (0 > data.State) {
                    alert(`${i + 1}번째 데이터에 서비스 상태를 선택해 주세요.`);
                    return;
                }

                if (!data.StartTime) {
                    alert(`${i + 1}번째 데이터에 점검 시작시간을 선택해 주세요.`);
                    return;
                }

                if (!data.EndTime) {
                    alert(`${i + 1}번째 데이터에 점검 종료시간을 선택해 주세요.`);
                    return;
                }

                if (!data.NoticeStrID) {
                    alert(`${i + 1}번째 데이터에 공지 문구 ID를 입력해 주세요.`);
                    return;
                }

                if (!data.UpdateStrID) {
                    alert(`${i + 1}번째 데이터에 업데이트 문구 ID를 입력해 주세요.`);
                    return;
                }

                if (!data.RecomUpdateStrID) {
                    alert(`${i + 1}번째 데이터에 권장 업데이트 문구 ID를 입력해 주세요.`);
                    return;
                }

                const existsDatas = datas.filter(_ => _.Area == data.Area && _.Platform == data.Platform);
                if (1 < existsDatas.length) {
                    alert(`${i + 1}번째 데이터의 지역(${data.Area}), 플랫폼(${Defines.MaintenancePlatform[data.Platform]})은 이미 존재합니다.`);
                    return;
                }
            }

            const response = await GameAPI.SaveMaintenancesAsync({ maintenances: changedDatas });
    
            if (!response) {
                alert(`저장실패.`);
                return;
            }
    
            if (!response.result) {
                alert(`저장실패. (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert(`저장 했습니다.`);
        await loadDatas();
    }, [loadDatas, datas, deleteDatas]);

    const addData = useCallback(() => {
        const newData = new Models.Maintenance();
        newData.StartTime = dayjs(dayjs().tz().format("YYYY-MM-DD")).toDate();
        newData.EndTime = dayjs(dayjs().tz().format("YYYY-MM-DD") + " 23:59:59").toDate();
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

    const onChangeArea = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].Area = e.target.value ? e.target.value.trim() : "";
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangePlatform = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
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
                prev[index].Platform = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeMaintenanceState = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
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
                prev[index].State = value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeStartTime = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 점검 시작시간을 선택해주세요.`);
            return;
        }
        setDatas(prev => {
            let dayjsDate: Dayjs | null = null;
        
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return prev;
            }

            if (prev[index]) {
                prev[index].StartTime = dayjsDate.toDate();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeEndTime = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 점검 종료시간을 선택해주세요.`);
            return;
        }
        setDatas(prev => {
            let dayjsDate: Dayjs | null = null;
        
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return prev;
            }

            if (prev[index]) {
                prev[index].EndTime = dayjsDate.toDate();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeNoticeStrID = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].NoticeStrID = e.target.value ? e.target.value.trim() : "";
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeUpdateStrID = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].UpdateStrID = e.target.value ? e.target.value.trim() : "";
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeRecomUpdateStrID = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].RecomUpdateStrID = e.target.value ? e.target.value.trim() : "";
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const platformSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(Defines.MaintenancePlatform).length)
            enumSize = Object.values(Defines.MaintenancePlatform).length / 2;

        for (let i = 0; i < enumSize; i++) {
            list.push(<MenuItem key={i} value={i}>{(Defines.MaintenancePlatform.None == i ? "ALL" : Defines.MaintenancePlatform[i])}</MenuItem>);
        }

        return list;
    }

    const maintenanceStateSelectList = () => {
        const list: ReactElement[] = [];
        let enumSize = 0;
        if (0 < Object.values(Defines.MaintenanceState).length)
            enumSize = Object.values(Defines.MaintenanceState).length / 2;

        for (let i = 0; i < enumSize; i++) {
            list.push(<MenuItem key={i} value={i}>{Defines.MaintenanceState[i]}</MenuItem>);
        }

        return list;
    }

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
                        {/* <TableCell>
                            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                        </TableCell> */}
                        <TableCell>
                            {
                                data.isNewData
                                    ?
                                        <FormControl>
                                            <Select className={commonUIStyles.select} value={data.Platform} size='small' onChange={e => onChangePlatform(e, i)}>
                                                {platformSelectList()}
                                            </Select>
                                        </FormControl>
                                    :
                                        (
                                            Defines.MaintenancePlatform.None == data.Platform
                                                ?
                                                    "ALL"
                                                :
                                                    Defines.MaintenancePlatform[data.Platform]
                                        )
                            }
                        </TableCell>
                        <TableCell className={styles.inputCol}>
                            {
                                data.isNewData
                                    ?
                                        <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.Area} size='small' placeholder='지역' onChange={e => onChangeArea(e, i)} />
                                    :
                                        data.Area
                            }
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                <Select className={commonUIStyles.select} value={data.State} size='small' onChange={e => onChangeMaintenanceState(e, i)}>
                                    {maintenanceStateSelectList()}
                                </Select>
                            </FormControl>
                        </TableCell>
                        <TableCell className={styles.timeCol}>
                            <DateTimePicker label={`시작일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs(data.StartTime).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeStartTime(date, i)} />
                        </TableCell>
                        <TableCell className={styles.timeCol}>
                            <DateTimePicker label={`종료일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm"  value={dayjs(data.EndTime).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeEndTime(date, i)} />
                        </TableCell>
                        <TableCell className={styles.inputCol}>
                            <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.NoticeStrID} size='small' placeholder='공지문구 ID' onChange={e => onChangeNoticeStrID(e, i)} />
                        </TableCell>
                        <TableCell className={styles.inputCol}>
                            <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.UpdateStrID} size='small' placeholder='업데이트 문구 ID' onChange={e => onChangeUpdateStrID(e, i)} />
                        </TableCell>
                        <TableCell className={styles.inputCol}>
                            <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.RecomUpdateStrID} size='small' placeholder='권장 업데이트 문구 ID' onChange={e => onChangeRecomUpdateStrID(e, i)} />
                        </TableCell>
                        <TableCell>
                            {
                                data.isNewData
                                    ?
                                        ""
                                    :
                                        dayjs(data.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")
                            }
                        </TableCell>
                        <TableCell>
                            {
                                data.isNewData
                                    ?
                                        ""
                                    :
                                        dayjs(data.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")
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
                            {/* <TableCell>관리</TableCell> */}
                            <TableCell>플랫폼</TableCell>
                            <TableCell>지역</TableCell>
                            <TableCell>서비스 상태</TableCell>
                            <TableCell>시작일시({timezoneName})</TableCell>
                            <TableCell>종료일시({timezoneName})</TableCell>
                            <TableCell>공지 문구ID</TableCell>
                            <TableCell>업데이트 문구ID</TableCell>
                            <TableCell>권장 업데이트 문구ID</TableCell>
                            <TableCell>등록일시({timezoneName})</TableCell>
                            <TableCell>최종수정일시({timezoneName})</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 서비스 상태 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onChangeArea, onChangePlatform, onChangeMaintenanceState, onChangeStartTime, onChangeEndTime, onChangeNoticeStrID, onChangeUpdateStrID, onChangeRecomUpdateStrID]);
    
    return (
        <Box sx={{ mt: 5, width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 게임 서비스(점검/점검문구) 상태 설정 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>서비스 상태</Typography>
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