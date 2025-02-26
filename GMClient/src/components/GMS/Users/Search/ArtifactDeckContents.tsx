import { useAppSelector } from "@hooks/index";
import { ReactElement, useCallback, useEffect, useState } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { EItemType } from '@ngel/data/models/lobby';
import { DataTableModels } from '@ngel/data/tables/model';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.ArtifactDeck[];
}

const ArtifactDeckContents = ({ datas } : ContentsProps) : ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        const artifactTable = tables.artifactDataTable;
        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const artifactInfo0 = artifactTable.find(element => element?.ID == data?.ArtifactID0);
            const artifactInfo1 = artifactTable.find(element => element?.ID == data?.ArtifactID1);
            const artifactInfo2 = artifactTable.find(element => element?.ID == data?.ArtifactID2);
            const artifactInfo3 = artifactTable.find(element => element?.ID == data?.ArtifactID3);
            const artifactInfo4 = artifactTable.find(element => element?.ID == data?.ArtifactID4);
            const artifactInfo5 = artifactTable.find(element => element?.ID == data?.ArtifactID5);
            const artifactInfo6 = artifactTable.find(element => element?.ID == data?.ArtifactID6);
            const artifactInfo7 = artifactTable.find(element => element?.ID == data?.ArtifactID7);
            const artifactInfo8 = artifactTable.find(element => element?.ID == data?.ArtifactID8);

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data?.DeckName ?? ""}</TableCell>
                    <TableCell>{data?.SlotID ?? ""}</TableCell>
                    <TableCell>{artifactInfo0?.NameStringWithID ? artifactInfo0?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID0}</TableCell>
                    <TableCell>{artifactInfo1?.NameStringWithID ? artifactInfo1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID1}</TableCell>
                    <TableCell>{artifactInfo2?.NameStringWithID ? artifactInfo2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID2}</TableCell>
                    <TableCell>{artifactInfo3?.NameStringWithID ? artifactInfo3?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID3}</TableCell>
                    <TableCell>{artifactInfo4?.NameStringWithID ? artifactInfo4?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID4}</TableCell>
                    <TableCell>{artifactInfo5?.NameStringWithID ? artifactInfo5?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID5}</TableCell>
                    <TableCell>{artifactInfo6?.NameStringWithID ? artifactInfo6?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID6}</TableCell>
                    <TableCell>{artifactInfo7?.NameStringWithID ? artifactInfo7?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID7}</TableCell>
                    <TableCell>{artifactInfo8?.NameStringWithID ? artifactInfo8?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID8}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                 </BorderedTableRow>)
        }
        result = (<>{list}</>);
        return result;
    },[tables, datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>프** **</TableCell>
                        <TableCell>슬롯 **</TableCell>
                        <TableCell>유물 1번</TableCell>
                        <TableCell>유물 2번</TableCell>
                        <TableCell>유물 3번</TableCell>
                        <TableCell>유물 4번</TableCell>
                        <TableCell>유물 5번</TableCell>
                        <TableCell>유물 6번</TableCell>
                        <TableCell>유물 7번</TableCell>
                        <TableCell>유물 8번</TableCell>
                        <TableCell>유물 9번</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 유물 *** 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default ArtifactDeckContents;