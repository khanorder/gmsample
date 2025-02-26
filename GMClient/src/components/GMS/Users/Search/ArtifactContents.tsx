import { useAppSelector } from '@hooks/index';
import { ReactElement, useState, useEffect, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: PaginatedList<Models.Artifact>;
}

const ArtifactContents = ({ datas }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        
        if (datas && 0 < datas?.items.length) {
            const list: ReactElement[] = [];
    
            const artifactTable  = tables.artifactDataTable;
            for (let i = 0; i < datas.items.length; i++) {
                const data = datas.items[i];
                const dataItem = artifactTable.find((element)=> element?.ID == data?.ArtifactID);
                
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">{ dataItem?.NameStringWithID ? dataItem?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID}</TableCell>
                        <TableCell>{data?.Enhance}</TableCell>
                        <TableCell>{data?.Count ?? ""}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>유물******</MinWidthTableCell>
                            <MinWidthTableCell>유물 ****</MinWidthTableCell>
                            <MinWidthTableCell>각성 ****</MinWidthTableCell>
                            <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 유물 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
        return result
    }, [tables, datas]);

    content = ( <>
     {contents()}
     </>);

    return content;
}

export default ArtifactContents;