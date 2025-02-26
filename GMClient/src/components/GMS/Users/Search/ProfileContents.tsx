import { useAppSelector } from "@hooks/index";
import { ReactElement, useState, useEffect, useCallback } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from "@ngel/data/tables/model";
import { EProfileType } from "@ngel/data/models/lobby";
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.Profile[];
}

const ProfileContents = ({ datas }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const profileTable = tables.profileDataTable;
    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        for(let i = 0; i < datas.length; i++){
            const data  = datas[i];
            const iconInfo = profileTable.find(element => element?.ID === data?.ProfileID);

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{iconInfo?.NameStringWithID ? iconInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.ProfileID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{EProfileType.Icon == data?.ProfileType ? '아이콘'.toString().replaceAll(/(?<=.{1})./g, '*') : (EProfileType.BG == data?.ProfileType ? '배경'.toString().replaceAll(/(?<=.{1})./g, '*') : data?.ProfileType)}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    }, [profileTable, datas]);

    if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>이*</TableCell>
                        <TableCell>타*********</TableCell>
                        <TableCell>생성시간({timezoneName})</TableCell>
                        <TableCell>수정시간({timezoneName})</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {contents()}
                </TableBody>
            </>
        )
    } else {
        content = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell colSpan={3} className={commonUIStyles.noneCell}>검색된 프** ** **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }
    
    return content;
}

export default ProfileContents;