import { useAppSelector } from '@hooks/index';
import { ReactElement, useCallback, useEffect, useState } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';
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

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: Models.Attendance[];
    attendances: RewardGroup[];
    selectRewardGroup: number;
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
const AttendanceContents = ({ datas, attendances, selectRewardGroup }: ContentsProps): ReactElement => {
    
    const tables = useAppSelector( state => state.tables);
    const assetTable = tables.assetDataTable;
    const itemTable = tables.itemTable;
    const characterTable = tables.characterDataTable;
    const attendanceTable = tables.attendanceDataTable;
    const attendancesRewardTable = tables.attendanceRewardDataTable;
    //2: 모두, 1: 완료, 0: 진행 중
    const [selectRewardViewState, setSelectRewardViewState] = useState<number>(2);

    const handleSelectState = (event: React.ChangeEvent<HTMLInputElement>, index: number) =>{
        if([0, 1, 2].includes(index)) setSelectRewardViewState(index);
    }

    const contents = useCallback((): ReactElement =>  {
        let result: ReactElement = <></>;
        
        if (attendances && 0 < attendances.length) {
            const list: ReactElement[] = [];
            const datas = attendances[selectRewardGroup];
            const filterRewardTables = attendancesRewardTable.filter((element)=> element?.RewardGroup === attendances[selectRewardGroup]?.RewardGroupIndex);
            
            for (let i = datas?.RewardState.length-1 ; i >= 0; i--) {
                const curState =  parseInt(datas?.RewardState[i]);
                if(selectRewardViewState !== 2){
                    if(selectRewardViewState !== curState) continue;
                }
                const dataInfo = filterRewardTables[i];
                const item = itemTable.find((item)=> item?.ID === dataInfo?.ItemID);
                const asset = assetTable.find((asset)=> asset?.ID === dataInfo?.AssetType);
                const hero = characterTable.find((character)=> character?.ID === dataInfo?.HeroID);
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell><Checkbox checked={curState === 1} onChange={(event)=>{event?.preventDefault()}}/></TableCell>
                        <TableCell>{`${i+1} 일차`}</TableCell>
                        <TableCell colSpan={4}>
                            {item?.NameStringWithID ? <Typography>{item?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            {asset?.NameStringWithID ? <Typography>{asset?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            {hero ? <Typography>{hero?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            </TableCell>
                        <TableCell>
                            {dataInfo?.ItemCount ? <Typography>{dataInfo?.ItemCount.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                            {dataInfo?.AssetCount ? <Typography>{dataInfo?.AssetCount.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography> : ""}
                        </TableCell>
                    </BorderedTableRow>
                )
            }

            if (list.length <= 0) {
                list.push( 
                    <BorderedTableRow key={1}>
                        <TableCell colSpan={7} className={commonUIStyles.noneCell}>진행된 출석 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead style={{position: "sticky"}}>
                        <BorderedTableRow>
                            <TableCell colSpan={4} style={{ borderRight: "0px", borderBottom: "0px", height: "50px"}}>
                                <Typography variant='body1' fontWeight='bold' noWrap>패**</Typography>
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
                                        <FormControlLabel control={<Checkbox checked={selectRewardViewState=== 2} onChange={(event)=> {handleSelectState(event, 2)}}/>} label='모두'/>
                                        <FormControlLabel control={<Checkbox checked={selectRewardViewState=== 1} onChange={(event)=> {handleSelectState(event, 1)}}/>} label='완료'/>
                                        <FormControlLabel control={<Checkbox checked={selectRewardViewState=== 0} onChange={(event)=> {handleSelectState(event, 0)}}/>} label='진행 중'/>
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
    }, [assetTable, itemTable, attendanceTable, characterTable, attendancesRewardTable, attendances, selectRewardGroup, selectRewardViewState]);

    return contents();

}

export default AttendanceContents;