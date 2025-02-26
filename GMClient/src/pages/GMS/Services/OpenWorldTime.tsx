import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback } from 'react';
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

interface InputOpenWorldTime {
    minutes: number;
    multiply: string;
    startAt: number;
    isChanged: boolean;
}

function Page() {
    const firstRender = useRef(true);
    const [data, setData] = useState<InputOpenWorldTime>({ minutes: 1, multiply: "0", startAt: 0, isChanged: false });
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setData(prev => prev = { minutes: 1, multiply: "0", startAt: 0, isChanged: false });

        const response = await GameAPI.OpenWorldTimeAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                console.log(response.error);

            return;
        }

        if (null != response.openWorldTime)
            setData({ minutes: response.openWorldTime.Minutes, multiply: response.openWorldTime.Multiply.toString(), startAt: response.openWorldTime.StartAt, isChanged: false });

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

        const openWorldTime = new Models.OpenWorldTime();
        openWorldTime.Minutes = data.minutes;
        openWorldTime.Multiply = parseFloat(data.multiply);
        openWorldTime.StartAt = data.startAt;

        const response = await GameAPI.SaveOpenWorldTimeAsync({ openWorldTime: openWorldTime });

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

    const onChangeMinutes = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>) => {
        let minutes: number = 0;
        try {
            minutes = parseInt(e.target.value);
        } catch (error) {
            if ("production" != process.env.NODE_ENV)
                console.log(error);
        }

        if (isNaN(minutes))
            minutes = 0;

        setData(prev => {
            if (0 > minutes || 1440 < minutes) {
                alert("서버시간(분)은 0~1440 사이로 입력해 주세요.");
                return prev;
            }
            prev.minutes = minutes;
            prev.isChanged = true;
            return deepmerge([], prev);
        });
    }, [setData]);

    const onChangeMultiply = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>) => {
        let multiply: string = "0.1";
        try {
            const multiplyFloat = parseFloat(e.target.value);
            if (isNaN(multiplyFloat)) {
                multiply = "";
            } else {
                if (0.1 > multiplyFloat || 10 < multiplyFloat) {
                    alert("시간 비율은 0.1~10 사이로 입력해 주세요.");
                } else {
                    multiply = e.target.value;
                }
            }
        } catch (error) {
            if ("production" != process.env.NODE_ENV)
                console.log(error);
        }
            
        setData(prev => {
            prev.multiply = multiply;
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
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.minutes} size='small' placeholder='서버시간(분)' onChange={e => onChangeMinutes(e)} />
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <FormControl fullWidth variant='outlined'>
                                <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.multiply} size='small' placeholder='시간 배율' onChange={e => onChangeMultiply(e)} />
                            </FormControl>
                        </TableCell>
                        <TableCell>{dayjs.unix(data.startAt).tz().format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    </BorderedTableRow>
                </>
            );

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>서버시간(분)</TableCell>
                            <TableCell>시간 비율</TableCell>
                            <TableCell>등록시간({timezoneName})</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 오픈월드 시간 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [data, onChangeMinutes, onChangeMultiply]);
    
    return (
        <Box sx={{ mt: 5, width: '100%' }}>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>오픈월드 시간</Typography>
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