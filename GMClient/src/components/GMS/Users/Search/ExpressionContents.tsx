import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { EExpressionCategory, EExpressionType, EMarketCode, EPlatform } from "@ngel/data/models/lobby";
import { Defines } from "@ngel/data/autoDefines";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.Expression[];
}
const ExpressionContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);

    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        if(datas && datas.length){
            const expressionTable = tables?.expressionDataTable;
            const characterTable = tables?.characterDataTable;
            const list: ReactElement[] = [];
            for(let i = 0; i < datas.length; i++){
                const data = expressionTable?.find(element => element.ID == datas[i].ExpressionID) ?? null;
                const character = characterTable?.find(element => element.ID == data?.Character) ?? null;
                list.push(<BorderedTableRow key={`expression-${i}-row`}>
                    <TableCell>{data?.ID ?? datas[i].ExpressionID}</TableCell>
                    <TableCell>{character?.NameStringWithID ? character?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.Character}</TableCell>
                    <TableCell>{data?.ExpressionType ? EExpressionType[data.ExpressionType].toString().replaceAll(/(?<=.{2})./g, '*') : ''}</TableCell>
                    <TableCell>{data?.ExpressionCategory ? EExpressionCategory[data.ExpressionCategory].toString().replaceAll(/(?<=.{2})./g, '*') : ''}</TableCell>
                    <TableCell>{data?.NameStringWithID ? data?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') :  ''}</TableCell>
                    <TableCell>{dayjs(datas[i].CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    <TableCell>{dayjs(datas[i].UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                </BorderedTableRow>)
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>영웅</TableCell>
                            <TableCell>타입</TableCell>
                            <TableCell>카테고리</TableCell>
                            <TableCell>이름</TableCell>
                            <TableCell>생성시간({timezoneName})</TableCell>
                            <TableCell>수정시간({timezoneName})</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        } else {
            result = (
                <>
                    <TableBody>
                        <BorderedTableRow>
                            <TableCell className={commonUIStyles.noneCell}>검색된 감정표현 정보가 없습니다.</TableCell>
                        </BorderedTableRow>
                    </TableBody>
                </>
            );
        }

        return result;
    }, [tables, datas])

    return <>{contents()}</>
}

export default ExpressionContents;