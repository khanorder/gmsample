import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { GetStaticProps } from 'next';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import { useRouter } from 'next/router';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: true });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

interface InputGoldClashTime {
    startHour: number;
    startMinute: number;
    endHour: number;
    endMinute: number;
    isChanged: boolean;
}

enum GoldClashDataType {
    StartHour,
    StartMinute,
    EndHour,
    EndMinute,
}

function Page() {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const [data, setData] = useState<InputGoldClashTime>({ startHour: 0, startMinute: 0, endHour: 0, endMinute: 0, isChanged: false });
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setData(prev => prev = { startHour: 0, startMinute: 0, endHour: 0, endMinute: 0, isChanged: false });

        const response = await GameAPI.GoldClashTimeAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            return;
        }

        if (null != response.goldClashTime)
            setData({ startHour: response.goldClashTime.StartHour, startMinute: response.goldClashTime.StartMinute, endHour: response.goldClashTime.EndHour, endMinute: response.goldClashTime.EndMinute, isChanged: false });

    }, [setData]);

    useEffect(() => {
        loadDatas();

    }, [loadDatas]);

    const onReload = useCallback(async () => {
        if (data && data.isChanged) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
    }, [data, loadDatas]);


    const onSaveData = useCallback(async () => {
        if (!data || !data.isChanged) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        } else if (data.isChanged) {
            if (!confirm("이대로 저장하시겠습니까?"))
                return;
        }

        const goldClashTime = new Models.GoldClashTime();
        goldClashTime.StartHour = data.startHour;
        goldClashTime.StartMinute = data.startMinute;
        goldClashTime.EndHour = data.endHour;
        goldClashTime.EndMinute = data.endMinute;

        const response = await GameAPI.SaveGoldClashTimeAsync({ goldClashTime: goldClashTime });

        if (!response) {
            alert(`오류!`);
            return;
        }

        if (!response.result) {
            alert(`오류! (error: ${Errors[response.error]})`);
            return;
        }

        alert(`저장 했습니다.`);
        await loadDatas();
    }, [loadDatas, data]);

    const onChangeData = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, type: GoldClashDataType) => {
        let data: number = 0;
        try {
            data = parseInt(e.target.value);
        } catch (error) {
            if ("production" != process.env.NODE_ENV)
                console.log(error);
        }

        if (isNaN(data))
            data = 0;

        setData(prev => {
            switch (type) {
                case GoldClashDataType.StartHour:
                    if (0 > data || 23 < data)
                    {
                        alert("시작 시(Hour)는 0~23 사이로 입력해 주세요.");
                        return prev;
                    }
                    prev.startHour = data;
                    break;

                case GoldClashDataType.StartMinute:
                    if (0 > data || 59 < data)
                    {
                        alert("시작 분(Minute)은 0~59 사이로 입력해 주세요.");
                        return prev;
                    }
                    prev.startMinute = data;
                    break;

                case GoldClashDataType.EndHour:
                    if (0 > data || 23 < data)
                    {
                        alert("종료 시(Hour)는 0~23 사이로 입력해 주세요.");
                        return prev;
                    }
                    prev.endHour = data;
                    break;

                case GoldClashDataType.EndMinute:
                    if (0 > data || 59 < data)
                    {
                        alert("종료 분(Minute)은 0~59 사이로 입력해 주세요.");
                        return prev;
                    }
                    prev.endMinute = data;
                    break;
            }
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setData]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (data) {
            let rowClass: string = commonUIStyles.row;
            if (data.isChanged)
                rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

            const row = (
                <>
                    <BorderedTableRow className={rowClass}>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.startHour} size='small' placeholder='시작 시(Hour)' onChange={e => onChangeData(e, GoldClashDataType.StartHour)} />
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.startMinute} size='small' placeholder='시간 분(Minute)' onChange={e => onChangeData(e, GoldClashDataType.StartMinute)} />
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.endHour} size='small' placeholder='종료 시(Hour)' onChange={e => onChangeData(e, GoldClashDataType.EndHour)} />
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.endMinute} size='small' placeholder='종료 분(Minute)' onChange={e => onChangeData(e, GoldClashDataType.EndMinute)} />
                            </FormControl>
                        </TableCell>
                    </BorderedTableRow>
                </>
            );

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>시작 시(Hour)</TableCell>
                            <TableCell>시작 분(Minute)</TableCell>
                            <TableCell>종료 시(Hour)</TableCell>
                            <TableCell>종료 분(Minute)</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {row}
                    </TableBody>
                </>
            );
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 골드 클래시 오픈 시간 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [data, onChangeData]);
    
    return (
        <Box sx={{ mt: 5, width: '100%' }}>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>골드 클래시 오픈 시간(UTC+0)</Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Grid sx={{ padding: '20px 0 10px' }}>
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