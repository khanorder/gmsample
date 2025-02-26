import { ChangeEvent, ReactElement, useCallback, useEffect, useState } from 'react';
import { useAppSelector } from "@hooks/index";
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled, SelectChangeEvent, Typography, TextField } from '@mui/material';
import dynamic from 'next/dynamic';
import { DataTableModels } from '@ngel/data/tables/model';

const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userUID: number;
    datas: Models.UserBlock[];
    onDeleteData: (index: number) => void;
    onChangeStartTime: (date: string | null, index: number) => void;
    onChangeEndTime: (date: string | null, index: number) => void;
    onChangeBlockID: (e: SelectChangeEvent<unknown>, index: number) => void;
    onChangeBlockStrByInput: (e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => void;
}

const UserBlockContents = ({ userUID, datas, onDeleteData, onChangeStartTime, onChangeEndTime, onChangeBlockID, onChangeBlockStrByInput }: ContentsProps): ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const penaltyTable = tables.penaltyDataTable;
    const [penaltyMenuItems, selectPenaltyMenuItems] = useState<ReactElement[]>([]);
    useEffect(() => {
        if(penaltyTable && 0 < penaltyTable.length){
            let list : ReactElement[] = [];
            list.push(
                <MenuItem key={`0-0-menu`} value={0}>{`${0} (직접 입력)`}</MenuItem>
            )
            for(let i = 0 ; i < penaltyTable.length; i++){
                const penaltyData = penaltyTable[i];
                list.push(
                    <MenuItem key={`${i}-${penaltyData.ID}-menu`} value={penaltyData.ID}>
                        {`${penaltyData.ID} ${penaltyData.ReportStateTextStringWithID} (${penaltyData.PenaltyGrade})`}</MenuItem>
                )
            }

            selectPenaltyMenuItems(list);
        }
    }, [penaltyTable])

    const SelectBox = useCallback(({ BlockReasonID, index }) : ReactElement => {
        if(0 < penaltyMenuItems.length){
            return (
                <Select className={commonUIStyles.select} value={BlockReasonID} size='small' onChange={e => onChangeBlockID(e, index)}>
                {penaltyMenuItems}
            </Select>
            )
        }
        return <></>;
    }, [penaltyMenuItems, onChangeBlockID]);

    const contents = useCallback(():ReactElement => {
        let result: ReactElement = <></>;

        if (datas && 0 < datas.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];

                let rowClass: string = commonUIStyles.row;
                    if (data?.isChanged)
                        rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;
                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell ><Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='inherit' onClick={e => onDeleteData(i)}>삭제</Button></TableCell>
                        <TableCell>{ (data?.UID == 0  || data?.UID) ? (userUID ?? "") : data?.UID}</TableCell>
                        <TableCell>{data?.MemberNo === 0 ? 0 : data?.MemberNo}</TableCell>
                        <TableCell>
                            <FormControl>
                                <SelectBox BlockReasonID={data?.BlockReasonID} index={i}></SelectBox>
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            { data?.BlockReasonID == 0 ? 
                            <TextField className={commonUIStyles.input} variant='outlined' fullWidth value={data?.BlockReasonStr} size='small' placeholder='블럭 이유' onChange={e => onChangeBlockStrByInput(e, i)} />
                        : <Typography>{data?.BlockReasonStr}</Typography>}</TableCell>
                        <TableCell><DateTimePicker label="시작일시" enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs(data?.StartTime).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeStartTime(date, i)} /></TableCell>
                        <TableCell><DateTimePicker label="종료일시" enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={dayjs(data?.EndTime).tz().format("YYYY-MM-DD HH:mm")} onChange={(date) => onChangeEndTime(date, i)} /></TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>관리</MinWidthTableCell>
                            <MinWidthTableCell>UID</MinWidthTableCell>
                            <MinWidthTableCell>MemberNo</MinWidthTableCell>
                            <MinWidthTableCell>BlockReasonID</MinWidthTableCell>
                            <MinWidthTableCell>BlockReasonStr</MinWidthTableCell>
                            <MinWidthTableCell>시작일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>종료일시({timezoneName})</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 유저블럭 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [datas, SelectBox, onChangeBlockStrByInput, onChangeEndTime, onChangeStartTime, onDeleteData, userUID])
    return <>{contents()}</>
}

export default UserBlockContents;