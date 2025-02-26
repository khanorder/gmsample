import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

interface ContentsProps {
    datas: Models.Incubation[];
}

const PetIncubationContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const petEggTable = tables.petEggDataTable;
    const incubationTable = tables.incubationDataTable;
    let content: ReactElement = <></>;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const incubationInfo = incubationTable.find(element => element.ID == data.IncubatorID);
            const petEggInfo = petEggTable.find(element => element.ID == data.PetEggID);
            list.push(
                <BorderedTableRow key={`pet-incubation-${i}`}>
                    <TableCell>{incubationInfo?.NameStringWithID ? incubationInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data.IncubatorID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{petEggInfo?.NameStringWithID ? petEggInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data.PetEggID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data.IncubateCount.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.IncubationEndAt ? dayjs.unix(data.IncubationEndAt).tz().format('YYYY-MM-DD HH:mm:ss') : ''}</TableCell>
                    <TableCell>{data.IsDeleted ? <Typography sx={{ color: 'blue' }}>True</Typography> : <Typography sx={{ color: 'red' }}>False</Typography>}</TableCell>
                    <TableCell>{dayjs(data.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    <TableCell>{dayjs(data.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[incubationTable, petEggTable, datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>부**</TableCell>
                        <TableCell>펫*********</TableCell>
                        <TableCell>부** **</TableCell>
                        <TableCell>종료시간({timezoneName})</TableCell>
                        <TableCell>삭제여부</TableCell>
                        <TableCell>생성시간({timezoneName})</TableCell>
                        <TableCell>수정시간({timezoneName})</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {contents()}
                </TableBody>
            </>
        )
    }
    else{
        content = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={commonUIStyles.noneCell}>검색된 펫 부화기 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default PetIncubationContents;