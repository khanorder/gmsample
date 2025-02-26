import { ReactElement, useCallback, useEffect, useRef, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Defines } from "@ngeldata/autoDefines";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import * as layoutsActions from '@store/reducers/layouts';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { useAppDispatch, useAppSelector } from '@hooks/index';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { useRouter } from 'next/router';
import { EPenaltyReportState } from '@ngel/data/models/lobby';
import Link from 'next/link';
import { DataTableModels } from '@ngel/data/tables/model';
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const InputAdornment = dynamic(() => import('@mui/material/InputAdornment'), { ssr: false });
const OutlinedInput = dynamic(() => import('@mui/material/OutlinedInput'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const PenaltyList = dynamic(() => import('@components/GMS/Users/Penalties/List'), { ssr: false });
const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Autocomplete = dynamic(() => import('@mui/material/Autocomplete'), { ssr: false });
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });
const FormGroup = dynamic(() => import('@mui/material/FormGroup'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface FormProps {
    page: number;
    searchUid: number;
    searchMemberNo: number;
    searchNick: string;
    startDate: string;
    endDate: string;
    uid: number|null;
    reportState: number|null;
}

const Form = ({ page, searchUid, searchMemberNo, searchNick, uid, reportState, startDate, endDate }: FormProps) : ReactElement => {
    const dispatch = useAppDispatch();
    const firstRender = useRef(true);
    const router = useRouter();
    const tables = useAppSelector(state => state.tables);

    const [parameters, setParameters] = useState<string>('');
    const [data, setData] = useState<Models.PenaltyWithUser|null>(null);
    const [penaltyGrades, setPenaltyGrades] = useState<number[]>([]);

    const loadData = useCallback(async () => {
        

        setData(null);
        setPenaltyGrades([]);

        if (null == uid || 1 > uid)
            return;

        if (null == reportState || 1 > reportState)
            return

        dispatch(layoutsActions.startLoadingMessage("패널티 데이터를 불러오는 중입니다."));

        const parameterPenalty = new Models.Penalty();
        parameterPenalty.UID = uid;
        parameterPenalty.ReportState = reportState;
        const response = await GameAPI.PenaltyAsync({ penalty: parameterPenalty });
        
        if (response.result && null != response.penalty) {
            const penaltyDataTable = tables.penaltyDataTable;
            let penaltyGradeOptions: number[] = [];
            penaltyGradeOptions.push(0);

            for (let i = 0; i < penaltyDataTable.length; i++) {
                if (penaltyDataTable[i].ReportState != response.penalty.ReportState || penaltyDataTable[i].PenaltyGrade > response.penalty.PenaltyGrade)
                    continue;
                    
                penaltyGradeOptions.push(penaltyDataTable[i].PenaltyGrade);
            }
            setPenaltyGrades(penaltyGradeOptions);
            setData(response.penalty);
        }

        dispatch(layoutsActions.stopLoading());
    }, [dispatch, setData, uid, reportState, tables, setPenaltyGrades]);

    const onLoadData = useCallback(async () => {
        if (data?.isChanged) {
            if (false == confirm('작업중인 내용이 있습니다.\n계속 하시겠습니다.'))
                return;
        }
        loadData();
    }, [data, loadData]);

    useEffect(() => {
        if (firstRender.current) {
            setParameters(`page=${page}&searchUid=${searchUid ?? ''}&searchMemberNo=${searchMemberNo ?? ''}&searchNick=${searchNick ?? ''}&startDate=${startDate ?? ''}&endDate=${endDate ?? ''}`);
        }
    }, [firstRender, setParameters, page, searchUid, searchMemberNo, searchNick, startDate, endDate]);

    //#region OnRender
    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;
        
        loadData();
    }, [firstRender, loadData, setParameters]);
    //#endregion

    const onChangeIsActive = useCallback(async () => {
        setData(prev => {
            if (null == prev)
                return null;

            if (false == prev.IsActive) {
                alert('패널티 상태를 활성화 할 수는 없습니다.');
                return prev;
            }

            prev.IsActive = false;
            prev.PenaltyPoint = 0;
            prev.PenaltyCount = 0;
            prev.PenaltyEndAt = 0;
            prev.ClearPenaltyAt = 0;
            prev.isChanged = true;

            return new Models.PenaltyWithUser(prev);
        });
    }, [setData]);

    const onChangeGrade = useCallback(async (e, v: number) => {
        setData(prev => {
            if (null == v)
                v = 0;

            if (null == prev)
                return null;

            if (v > prev.PenaltyGrade)
                return prev;

            prev.IsActive = false;
            prev.PenaltyGrade = v;
            prev.PenaltyPoint = 0;
            prev.PenaltyCount = 0;
            prev.PenaltyEndAt = 0;
            prev.ClearPenaltyAt = 0;
            prev.isChanged = true;

            return new Models.PenaltyWithUser(prev);
        });
    }, [setData]);

    const saveData = useCallback(async () => {
        if (!data?.isChanged) {
            alert(`변경된 내용이 없습니다.`);
            return;
        }

        const response = await GameAPI.SavePenaltyAsync({ penalty: data });
        if (response.result) {
            alert('변경된 내용을 저장했습니다.');
            loadData();
        } else {
            alert(`저장실패 error(${response.error})`);
        }
    }, [data, loadData]);

    const contents = useCallback((): ReactElement => {
        if (null == data) {
            return (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 패널티 데이터가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        } else {
            return (
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell component='th'>UID</TableCell>
                            <TableCell>{data?.UID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell component='th'>MemberNo</TableCell>
                            <TableCell>{data?.MemberNo.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell component='th'>Nick</TableCell>
                            <TableCell>{data?.Nick.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                            <TableCell component='th'>ReportState</TableCell>
                            <TableCell>{EPenaltyReportState[data?.ReportState].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell component='th'>IsActive</TableCell>
                            <TableCell>
                                <FormGroup>
                                    <FormControlLabel control={<Checkbox size="small" checked={data?.IsActive} onClick={onChangeIsActive} />} label={'활성화'} />
                                </FormGroup>
                            </TableCell>
                            <TableCell component='th'>PenaltyGrade</TableCell>
                            <TableCell>
                                {
                                    null != penaltyGrades && penaltyGrades.length > 0
                                        ?
                                            <FormControl fullWidth sx={{ marginLeft: 1 }}>
                                                <Autocomplete 
                                                    id={`penalty_grade`} 
                                                    options={penaltyGrades} 
                                                    size='small' 
                                                    onChange={(e, v, r) => onChangeGrade(e, v as number)} 
                                                    getOptionLabel={(value) => (value as string).toString()} 
                                                    isOptionEqualToValue={(option, value) => option as number == value as number} 
                                                    value={data ? data.PenaltyGrade : 0} 
                                                    renderInput={(params) => <TextField {...params} label={`패널티 등급`} />} 
                                                />
                                            </FormControl>
                                        :
                                            data?.PenaltyGrade
                                }
                            </TableCell>
                            <TableCell component='th'>PenaltyPoint</TableCell>
                            <TableCell>{data?.PenaltyPoint}</TableCell>
                            <TableCell component='th'>PenaltyCount</TableCell>
                            <TableCell>{data?.PenaltyCount}</TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell component='th'>PenaltyEndAt</TableCell>
                            <TableCell>{0 < data.PenaltyEndAt ? dayjs.unix(data.PenaltyEndAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                            <TableCell component='th'>ClearPenaltyAt</TableCell>
                            <TableCell>{0 < data.ClearPenaltyAt ? dayjs.unix(data.ClearPenaltyAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                            <TableCell component='th'>CreateAt</TableCell>
                            <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                            <TableCell component='th'>UpdateAt</TableCell>
                            <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }
    }, [data, onChangeGrade, onChangeIsActive, penaltyGrades]);

    return (
        <Box sx={{ mb: '100px', width: '100%' }}>
            <Grid container justifyContent='space-between' sx={{ margin: '20px 0 0' }}>
                <Grid item sx={{ padding: '10px 0 10px' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ margin: 0.5 }} onClick={saveData}>저장</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ margin: 0.5 }} onClick={onLoadData}>새로고침</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ margin: 0.5 }} onClick={()=> router.push('/GMS/Users/Penalties?' + parameters)}>취소</Button>
                </Grid>
            </Grid>
            <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 600 }}>
                <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                    <Typography variant='h6' noWrap sx={{ flexGrow: 1 }}>패널티 사용자 관리(수정)</Typography>
                </Toolbar>
                <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                    {contents()}
                </Table>
            </TableContainer>
            <Grid container justifyContent='space-between' sx={{ margin: '20px 0 0' }}>
                <Grid item sx={{ padding: '10px 0 10px' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ margin: 0.5 }} onClick={saveData}>저장</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ margin: 0.5 }} onClick={onLoadData}>새로고침</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ margin: 0.5 }} onClick={()=> router.push('/GMS/Users/Penalties?' + parameters)}>취소</Button>
                </Grid>
            </Grid>
        </Box>
    )
}
export default Form;