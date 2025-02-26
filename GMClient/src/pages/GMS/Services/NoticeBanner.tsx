import { ChangeEvent, ReactElement, useEffect, useRef, useState, useCallback, Fragment } from 'react';
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
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: true });
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
    const [datas, setDatas] = useState<Models.NoticeBanner[]>([]);
    const [deleteDatas, setDeleteDatas] = useState<Models.NoticeBanner[]>([]);
    
    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [ firstRender ]);
    //#endregion

    const loadDatas = useCallback(async () => {
        setDatas(prev => prev = []);
        setDeleteDatas(prev => prev = []);

        const response = await GameAPI.NoticeBannersAsync();

        if (!response.result) {
            if ("production" != process.env.NODE_ENV)
                    console.log(response.error);

            return;
        }

        if (null != response.noticeBanners && 0 < response.noticeBanners.length)
            setDatas(prev => prev = deepmerge([], response.noticeBanners));

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
            const deleteResponse = await GameAPI.DeleteNoticeBannersAsync({ noticeBanners: deleteDatas });

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

                if (isEmpty(data.Title)) {
                    alert(`${i + 1}번째 데이터에 제목을 입력해 주세요.`);
                    return;
                }
            }

            const response = await GameAPI.SaveNoticeBannersAsync({ noticeBanners: changedDatas });
    
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
        const newData = new Models.NoticeBanner();
        newData.StartAt = dayjs().unix();
        newData.EndAt = dayjs().add(1, 'day').unix();
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
    
    const onChangeStartAt = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 팝업공지 시작시간을 선택해주세요.`);
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
                prev[index].StartAt = dayjsDate.unix();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeEndAt = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 팝업공지 종료시간을 선택해주세요.`);
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
                prev[index].EndAt = dayjsDate.unix();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeTitle = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].Title = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeImageURL = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].ImageURL = e.target.value;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const onChangeMessage = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        setDatas(prev => {
            if (prev[index]) {
                prev[index].Message = e.target.value;
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
                    <Fragment key={i}>
                        <BorderedTableRow key={`${i}-1`} className={rowClass}>
                            <TableCell rowSpan={2}>
                                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDelete(i)}>삭제</Button>
                            </TableCell>
                            <TableCell>
                                <FormControl fullWidth variant='outlined'>
                                    <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.Title} size='small' placeholder='제목' onChange={e => onChangeTitle(e, i)} />
                                </FormControl>
                            </TableCell>
                            <TableCell>
                                <FormControl fullWidth variant='outlined'>
                                    <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.ImageURL} size='small' placeholder='이미지 경로' onChange={e => onChangeImageURL(e, i)} />
                                </FormControl>
                            </TableCell>
                            <TableCell>
                                <DateTimePicker label={`시작일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs.unix(data.StartAt).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeStartAt(date, i)} />
                            </TableCell>
                            <TableCell>
                                <DateTimePicker label={`종료일시(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs.unix(data.EndAt).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeEndAt(date, i)} />
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow key={`${i}-2`} className={rowClass}>
                            <TableCell colSpan={4} sx={{ borderLeft: "1px solid #d9d9d9 !important" }}>
                                <FormControl fullWidth variant='outlined'>
                                    <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data.Message} size='small' placeholder='공지 내용' multiline onChange={e => onChangeMessage(e, i)} />
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
                            <TableCell sx={{ minWidth: 100 }}>제목</TableCell>
                            <TableCell sx={{ minWidth: 200 }}>이미지 주소</TableCell>
                            <TableCell sx={{ minWidth: 200 }}>시작일시({timezoneName})</TableCell>
                            <TableCell sx={{ minWidth: 200 }}>종료일시({timezoneName})</TableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 팝업공지가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, onChangeStartAt, onChangeEndAt, onChangeTitle, onChangeImageURL, onChangeMessage, onDelete]);
    
    return (
        <Box sx={{ mt: 5, mb: '100px', width: '100%' }}>
            <Box sx={{ mt: 2 }}>
                <Typography variant="body1" sx={{ textAlign: 'center', mb: 2 }}>* 팝업/배너 공지 관리 기능.</Typography>
            </Box>
            <Grid sx={{ padding: '10px 0 20px' }}>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={addData}>추가</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveData} sx={{ ml: 1 }}>저장</Button>
                <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>팝업공지</Typography>
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