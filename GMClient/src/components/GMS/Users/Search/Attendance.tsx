import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, useCallback, useEffect, useState } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';

const AttendanceContents = dynamic(() => import('./AttendanceContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });

export interface AttendanceProps {
    attendances: Models.Attendance[]|null;
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

const Attendance = ({ attendances }: AttendanceProps): ReactElement => {
    const tables = useAppSelector( state => state.tables);
    const attendanceTable = tables.attendanceDataTable;
    const attendancesRewardTable = tables.attendanceRewardDataTable;
    const [attendanceDatas, setAttendanceDatas] = useState<RewardGroup[]>([]);
    
    const [selectRewardGroup, setSelectRewardGroup] = useState<number>(0);
    //2: 모두, 1: 완료, 0: 진행 중
    const [selectRewardViewState, setSelectRewardViewState] = useState<number>(2);

    useEffect(()=> {
        const attendancesTable = tables.attendanceDataTable;

        if(attendances){
            let attendancesOption: RewardGroup[] = [];
            for(let i = 0; i < attendancesTable.length; i++){
                const length = attendancesRewardTable.filter((element) => element.RewardGroup === attendancesTable[i].RewardGroup).length;
                const newArr: string[] = Array.from({length}, ()=> "0");
                const newData : RewardGroup = { 
                    ID: attendancesTable[i].ID, 
                    AttendanceDay: 0,
                    RewardGroupIndex : attendancesTable[i].RewardGroup, 
                    DescriptionStringWithID: attendancesTable[i].DescriptionStringWithID, 
                    RewardState : newArr,
                };
                
                attendancesOption.push(newData);
            }

            if(attendancesOption){
                for(let j = 0; j < attendances.length; j++){
                    const index = attendancesOption.findIndex((item) => item?.ID === attendances[j]?.AttendanceID);

                    if(0 <= index){
                        attendancesOption[index].AttendanceDay = attendances[j]?.AttendanceDay;
                        attendancesOption[index].RewardState = attendances[j]?.RewardState.split("");
                        attendancesOption[index].LastAt = attendances[j]?.LastAttendanceAt;
                        attendancesOption[index].CreateAt = attendances[j]?.CreateAt;
                        attendancesOption[index].UpdateAt = attendances[j]?.UpdateAt;
                    }
                }
                
                setAttendanceDatas(attendancesOption);
            }
        }
    }, [tables, attendances, attendancesRewardTable]);

    const handleSelectRewardGroup = (event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        setSelectRewardGroup(value);
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
        result = (<Select label='패**' value={selectRewardGroup} onChange={handleSelectRewardGroup} size='small' style={{fontSize: "14px"}}>{list}</Select>);
        return result;
    },[ attendanceTable, selectRewardGroup ]);

    return (
        <>
            <Box display='flex' justifyContent='end' sx={{ mt: 5, mb: 1 }}>
                <FormControl>
                    <InputLabel>패**</InputLabel>
                    {AttendanceMenuItems()}
                </FormControl>
            </Box>
            <Box sx={{ width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>출석 **</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} aria-label="relative table">
                        <AttendanceContents datas={attendances ?? []} attendances={attendanceDatas} selectRewardGroup={selectRewardGroup}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default Attendance;