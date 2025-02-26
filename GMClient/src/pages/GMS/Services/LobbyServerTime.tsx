import { ReactElement, useEffect, useRef, useState, useCallback } from 'react';
import { GetStaticProps } from 'next';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import isEmpty from 'lodash/isEmpty';
const Layout = dynamic(() => import('@components/layouts'), { ssr: true });
const ThemeLayout = dynamic(() => import('@components/layouts/theme'), { ssr: true });
const ManageLayout = dynamic(() => import('@components/layouts/manage'), { ssr: true });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: true });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: true });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: true });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: true });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: true });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: true });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: true });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: true });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: true });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: true });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: true });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: true });

interface InputServerTime {
    time: Date|null;
    isChanged: boolean;
}

function Page() {
    const firstRender = useRef(true);
    const [datas, setDatas] = useState<Models.LobbyServerTime[]>([]);
    const [allServerTime, setAllServerTime] = useState<InputServerTime>({ time: new Date(), isChanged: false });
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setDatas([]);
        setAllServerTime({ time: new Date(), isChanged: false });
        const response = await GameAPI.LobbyServerTimesAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            return;
        }

        if (null != response.lobbyServerTimes && 0 < response.lobbyServerTimes.length)
            setDatas(response.lobbyServerTimes);

    }, [setDatas, setAllServerTime]);

    useEffect(() => {
        loadDatas();

    }, [loadDatas]);

    const onReload = useCallback(async () => {
        if (allServerTime && allServerTime.isChanged) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
    }, [allServerTime, loadDatas]);


    const onSaveData = useCallback(async () => {
        if (!datas || null == allServerTime.time) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        } else if (allServerTime.isChanged) {
            if (!confirm("이대로 저장하시겠습니까?"))
                return;
        }

        const response = await GameAPI.SaveLobbyServerTimesAsync({ serverTime: allServerTime.time });

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
    }, [loadDatas, datas, allServerTime]);

    const onChangeAllServerTime = useCallback((date: string | null) => {
        if (!date) {
            alert(`전체 로비서버 시간을 선택해주세요.`);
            return;
        }

        setAllServerTime(prev => {
            let dayjsDate: Dayjs | null = null;
        
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return prev;
            }

            if (prev) {
                prev.time = dayjsDate?.toDate();
                prev.isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setAllServerTime]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        const list: ReactElement[] = [];

        if (datas && 0 < datas.length) {
            let rowClass: string = commonUIStyles.row;
            if (allServerTime.isChanged)
                rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

            const allRow = (
                <BorderedTableRow className={rowClass} key={`all`}>
                    <TableCell>전체</TableCell>
                    <TableCell colSpan={2}>
                        <DateTimePicker label={`전체서버 시간(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm:ss" value={dayjs(allServerTime.time).tz().format("YYYY-MM-DD HH:mm:ss")} onChange={(date) => onChangeAllServerTime(date)} />
                    </TableCell>
                </BorderedTableRow>
            );

            list.push(allRow);

            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];
                const row = (
                    <BorderedTableRow className={rowClass} key={i}>
                        <TableCell>{data.lobbyID}</TableCell>
                        <TableCell>{dayjs(data.serverTime).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                        <TableCell>{dayjs().tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    </BorderedTableRow>
                );
                list.push(row);
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow key="header">
                            <TableCell>서버ID(LobbyID)</TableCell>
                            <TableCell>서버시간({timezoneName})</TableCell>
                            <TableCell>갱신시간({timezoneName})</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 로비서버 시간 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, allServerTime, onChangeAllServerTime]);
    
    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>로비서버 시간</Typography>
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