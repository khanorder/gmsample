import { useAppSelector } from '@hooks/index';
import { MutableRefObject, ReactElement, useCallback, useEffect, useRef, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { Errors } from '@ngel/data/autoErrors';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled, TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from '@ngel/data/tables/model';
import { SelectChangeEvent } from '@mui/material/Select';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import deepmerge from 'deepmerge';
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const FormGroup = dynamic(() => import('@mui/material/FormGroup'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.Attendance[];
    searchDetail: (index: number, accountID: number) => void;
    tabChanged: MutableRefObject<boolean>;
}

interface RewardGroup {
    ID: number;
    AttendanceDay: number;
    RewardGroupIndex : number | undefined;
    DescriptionStringWithID: string;
    RewardState: string[];
    LastAt?: number;
    CreateAt?: Date;
    UpdateAt?: Date;

}

const EditAttendanceContents = ({ userAccount, tabIndex, datas, searchDetail, tabChanged }: ContentsProps): ReactElement => {
    
    let content: ReactElement = <></>;
    const tables = useAppSelector( state => state.tables);
    const assetTable = tables.assetDataTable;
    const itemTable = tables.itemTable;
    const characterTable = tables.characterDataTable;
    const attendanceTable = tables.attendanceDataTable;

    const attendancesRewardTable = tables.attendanceRewardDataTable;
    const firstChanged = useRef<boolean>(false);
    const [selectRewardGroup, setSelectRewardGroup] = useState<number>(0);
    //2: 모두, 1: 완료, 0: 진행 중
    const [selectRewardViewState, setSelectRewardViewState] = useState<number>(2);
    const [attendances, setAttendances] = useState<RewardGroup[]>([]);
    const [reloadAttendances, setReloadAttendances] = useState<RewardGroup[]>([]);
    const filterRewardTables = useRef<DataTableModels.AttendanceRewardData[]>([]);
    
    const isSigned = userAccount.IsSignIn;
    
    useEffect(()=> {
        const attendancesTable = tables.attendanceDataTable;

        let attendancesOption: RewardGroup[] = [];
        for(let i = 0; i < attendancesTable.length; i++){
            const length = attendancesRewardTable.filter((element) => element?.RewardGroup === attendancesTable[i]?.RewardGroup).length;
            const newArr: string[] = Array.from({length}, ()=> "0");
            const newData : RewardGroup = { 
                ID: attendancesTable[i]?.ID ?? 0, 
                AttendanceDay: 0,
                RewardGroupIndex : attendancesTable[i]?.RewardGroup ?? 0, 
                DescriptionStringWithID: attendancesTable[i]?.DescriptionStringWithID ?? "", 
                RewardState : newArr,
            };
            
            attendancesOption.push(newData);
        }
        if(attendancesOption){
            for(let j = 0; j < datas.length; j++){
                const index = attendancesOption.findIndex((item) => item?.ID === datas[j]?.AttendanceID);
                if(0 <= index){
                    attendancesOption[index].AttendanceDay = datas[j]?.AttendanceDay;
                    attendancesOption[index].RewardState = datas[j]?.RewardState.split("");
                    attendancesOption[index].LastAt = datas[j]?.LastAttendanceAt;
                    attendancesOption[index].CreateAt = datas[j]?.CreateAt;
                    attendancesOption[index].UpdateAt = datas[j]?.UpdateAt;
                }
            }
            
            setAttendances(attendancesOption);
            setReloadAttendances(prev => deepmerge([], attendancesOption));
        }
    }, [tables, datas, attendancesRewardTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [userAccount, tabIndex, searchDetail]);

    const onReload = useCallback(async () => {
        if (firstChanged.current) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }
        
        firstChanged.current = false;
        tabChanged.current = false;

        await loadDatas();
    }, [loadDatas, firstChanged, tabChanged]);

    const onSaveRewardData = useCallback(async () => {
        const attendanceID = attendances[selectRewardGroup].ID;
        const rewardState =  attendances[selectRewardGroup].RewardState.join('');

        if(!firstChanged.current){
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if(firstChanged.current){
            const response = await GameAPI.SaveAttendanceAsync({UID: userAccount.UID, AttendanceID: attendanceID, RewardState: rewardState});
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]})`);
                return;
            } 

            if(response){
                firstChanged.current = false;
                tabChanged.current = false;
                alert(`저장 했습니다.`);

                await loadDatas();
            }
        }
    }, [loadDatas, userAccount, tabChanged, firstChanged, attendances, selectRewardGroup]);

    const onChangeRewardState = useCallback((event: React.ChangeEvent<HTMLInputElement>, index: number) => {
        firstChanged.current = true;
        tabChanged.current = true;
        attendances[selectRewardGroup].RewardState[index] = 
        attendances[selectRewardGroup].RewardState[index] === "1" ? "0" : "1";

        setAttendances(prev => deepmerge([], prev));

    },[tabChanged, attendances, selectRewardGroup]);

    const onSelectRewardGroup = useCallback((event: SelectChangeEvent<unknown>) => {
        if(firstChanged.current){
            onReload();
        }

        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10); 
        setSelectRewardGroup(value);
    },[ firstChanged, onReload, setSelectRewardGroup]);

    const onChangeSelectState = (event: React.ChangeEvent<HTMLInputElement>, index: number) =>{
        if([0, 1, 2].includes(index)) setSelectRewardViewState(index);
    }
    const AttendanceMenuItems =  useCallback((): ReactElement =>  {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < attendanceTable.length; i++){
            const data = attendanceTable[i];

            list.push(
                <MenuItem key={`menuitem-${i}`} value={i}>{data.DescriptionStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
            )
        }
        result = (<Select label='패키지' value={selectRewardGroup} onChange={onSelectRewardGroup} style={{fontSize: "14px"}}>{list}</Select>);
        return result;
    },[ attendanceTable, selectRewardGroup, onSelectRewardGroup ]);

    const contents = useCallback((): ReactElement =>  {
        let result: ReactElement = <></>;
        
        if (attendances && 0 < attendances.length) {
            const list: ReactElement[] = [];
            const datas = attendances[selectRewardGroup];
            filterRewardTables.current = attendancesRewardTable.filter((element)=> element.RewardGroup === attendances[selectRewardGroup].RewardGroupIndex);
            
            const isSigned = userAccount.IsSignIn;

            for (let i = datas.RewardState.length-1 ; i >= 0; i--) {
                const curState =  parseInt(datas.RewardState[i]);
                if(selectRewardViewState !== 2){
                    if(selectRewardViewState !== curState) continue;
                }
                const dataInfo = filterRewardTables.current[i];
                const item = itemTable.find((item)=> item?.ID === dataInfo?.ItemID);
                const asset = assetTable.find((asset)=> asset?.ID === dataInfo?.AssetType);
                const hero = characterTable.find((character)=> character?.ID === dataInfo?.HeroID);
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell><Checkbox disabled={isSigned} checked={curState === 1} onChange={(event)=>{onChangeRewardState(event, i)}}/></TableCell>
                        <TableCell>{`${i+1} 일차`}</TableCell>
                        <TableCell colSpan={4}>
                            {item?.NameStringWithID ? <Typography>{item?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            {asset?.NameStringWithID ? <Typography>{asset?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            {hero ? <Typography>{hero?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            </TableCell>
                        <TableCell>
                            {dataInfo?.ItemCount ? <Typography>{dataInfo?.ItemCount}</Typography> : ""}
                            {dataInfo?.AssetCount ? <Typography>{dataInfo?.AssetCount}</Typography> : ""}
                        </TableCell>
                    </BorderedTableRow>
                )
            }
            if(list.length <= 0){
                list.push( 
                <BorderedTableRow key={1}>
                    <TableCell colSpan={7} className={commonUIStyles.noneCell}>진행된 출석 정보가 없습니다.</TableCell>
                </BorderedTableRow>)
            }
            result = (
                <>
                    <TableHead style={{position: "sticky"}}>
                        <BorderedTableRow>
                            <TableCell colSpan={6} style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px"}}></TableCell>
                            <TableCell style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px"}}>
                                <Box display='flex' justifyContent='end'>
                                    <FormControl>
                                        <InputLabel>패키지</InputLabel>
                                        {AttendanceMenuItems()}
                                    </FormControl>
                                </Box>
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell colSpan={4} style={{ borderRight: "0px", borderBottom: "0px", height: "50px"}}>
                                <Typography variant='body1' fontWeight='bold' noWrap>패키지</Typography>
                            </TableCell>
                            <TableCell colSpan={3} style={{ borderLeft: "0px", borderRight: "0px", borderBottom: "0px", height: "50px" }}>
                                <Typography variant='body1' fontWeight='bold' noWrap>진행 상태</Typography>
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell colSpan={4} style={{ borderLeft: "0px", backgroundColor: 'transparent'}}>
                                <Typography variant='body1' noWrap>{attendanceTable[selectRewardGroup].DescriptionStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                            </TableCell>
                            <TableCell colSpan={3} style={{ borderLeft: "0px", backgroundColor: 'transparent'}}>
                                <FormGroup>
                                    <Box display='flex' justifyContent='center'>
                                        <FormControlLabel control={<Checkbox checked={selectRewardViewState=== 2} onChange={(event)=> {onChangeSelectState(event, 2)}}/>} label='모두'/>
                                        <FormControlLabel control={<Checkbox checked={selectRewardViewState=== 1} onChange={(event)=> {onChangeSelectState(event, 1)}}/>} label='완료'/>
                                        <FormControlLabel control={<Checkbox checked={selectRewardViewState=== 0} onChange={(event)=> {onChangeSelectState(event, 0)}}/>} label='진행 중'/>
                                    </Box>
                                </FormGroup>
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell colSpan={2}>
                                <Typography variant='body1' fontWeight='bold' noWrap>
                                    마지막 출석 체크 시간
                                </Typography>
                            </TableCell>
                            <TableCell colSpan={3}>
                                <Typography variant='body1' fontWeight='bold' noWrap>
                                    생성일시({timezoneName})
                                </Typography>
                            </TableCell>
                            <TableCell colSpan={2}>
                                <Typography variant='body1' fontWeight='bold' noWrap>
                                    수정일시({timezoneName})
                                </Typography>
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell colSpan={2} style={{height: 50, background: 'transparent'}}>
                                { datas.LastAt === undefined ?  <Typography></Typography> : <Typography>{dayjs.unix(datas.LastAt).tz().format('YYYY-MM-DD HH:mm:ss')}</Typography>}
                            </TableCell>
                            <TableCell colSpan={3} style={{height: 50, background: 'transparent'}}>
                                { datas.CreateAt === undefined ?  <Typography></Typography> : <Typography>{dayjs(datas.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</Typography> }
                            </TableCell>
                            <TableCell colSpan={2} style={{height: 50, background: 'transparent'}}>
                                { datas.UpdateAt === undefined ? <Typography></Typography> : <Typography>{dayjs(datas.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss')}</Typography> }
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow></BorderedTableRow>
                        <BorderedTableRow>
                            <MinWidthTableCell >보상수령 상태</MinWidthTableCell>
                            <MinWidthTableCell >일차</MinWidthTableCell>
                            <MinWidthTableCell colSpan={4}>보상</MinWidthTableCell>
                            <MinWidthTableCell >보상 수량</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 출석 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
    
        return result;
    }, [userAccount, assetTable, itemTable, attendanceTable, characterTable, attendancesRewardTable, filterRewardTables, AttendanceMenuItems, onChangeRewardState, attendances, selectRewardGroup, selectRewardViewState]);

    content = <>
        <Box display='flex' justifyContent='start' alignItems='center' marginBottom={1}>
            <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSaveRewardData}>저장</Button>
            <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload} sx={{ ml: 1 }}>새로고침</Button>
        </Box>
        <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
            <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                <Typography variant='h6'>출석 체크</Typography>
            </Toolbar>
            <Table className={commonUIStyles.ellipsisTable} aria-label="relative table">
                {contents()}
            </Table>
        </TableContainer>
        </>;
   
    return content;

}

export default EditAttendanceContents;