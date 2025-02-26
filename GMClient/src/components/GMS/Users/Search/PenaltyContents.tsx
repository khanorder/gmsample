import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { EPenaltyReportState } from "@ngel/data/models/lobby";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.Penalty[];
}

const PenalyContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const penaltyTable = tables.penaltyDataTable;
    
    let content: ReactElement = <></>;
    
    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        if(datas && 0 < datas.length){
            for(let i = 0; i < datas.length; i++){
                const data = datas[i];

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{EPenaltyReportState[data?.ReportState ?? 0].toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.IsActive ? "True" : "False"}</TableCell>
                        <TableCell>{data?.PenaltyGrade.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.PenaltyPoint.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.PenaltyCount.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.PenaltyEndAt ? dayjs.unix(data?.PenaltyEndAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.ClearPenaltyAt ? dayjs.unix(data?.ClearPenaltyAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }
        }
        result = (<>{list}</>);
        return result;
    },[datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>Re**** *****</TableCell>
                        <TableCell>활성* **</TableCell>
                        <TableCell>패널* **</TableCell>
                        <TableCell>패널* ***</TableCell>
                        <TableCell>패널* **</TableCell>
                        <TableCell>패널* ** **({timezoneName})</TableCell>
                        <TableCell>Cl************({timezoneName})</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 패널* **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default PenalyContents;