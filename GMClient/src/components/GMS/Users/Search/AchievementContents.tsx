import { useAppSelector } from "@hooks/index";
import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { PaginatedList } from '@helpers/paging';
import { Models } from "@ngel/data/models";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: PaginatedList<Models.Achievement>;
}

const AchievementContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const achievementDataTable = tables.achievementDataTable;
    const achievementGroupDataTable = tables.achievementGroupDataTable;
    let content: ReactElement = <></>;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for (let i = 0; i < datas.items.length; i++) {
            const data = datas.items[i];
            const achInfo = achievementDataTable.find(element => element?.ID === data?.AchievementID) ?? null;
            const achGroupInfo = achievementGroupDataTable.find(element => element?.ID === achInfo?.AchievementGroupID) ?? null;

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{achInfo?.NameStringWithID ? achInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.AchievementID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>{achGroupInfo?.CategoryNameStringWithID ? achGroupInfo?.CategoryNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : achInfo?.AchievementGroupID ? achInfo?.AchievementGroupID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                    <TableCell>{data?.Count ?? ""} / {achInfo?.AchievementCount ?? 0}</TableCell>
                    <TableCell>{data?.CompleteAt ? dayjs.unix(data?.CompleteAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (<>{list}</>);
        return result;
    },[achievementDataTable, achievementGroupDataTable, datas]);

   if(datas && 0 < datas?.items.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>업적 ******</TableCell>
                        <TableCell>업적 ********</TableCell>
                        <TableCell>횟수</TableCell>
                        <TableCell>완료시간({timezoneName})</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 업적 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default AchievementContents;