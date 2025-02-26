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
import { EContentsType } from "@ngel/data/models/lobby";
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

const ContentsTypes = Object.keys(EContentsType).filter(key => isNaN(Number(key)));
const contentsTranslate = (contentsNum : number) => {
    return ContentsTypes[contentsNum];
}

interface ContentsProps {
    datas: PaginatedList<Models.RankingReward>;
}

const RankingRewardContents = ({ datas } : ContentsProps): ReactElement => {

    const contents = useCallback(() : ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if(datas && 0 < datas.items.length){
            for(let i = 0; i < datas.items.length; i++){
                const data = datas.items[i];
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{contentsTranslate(data?.ContentType).toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                        <TableCell>{data?.RewardedAt ? dayjs(data?.RewardedAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (<>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>컨** **</TableCell>
                        <TableCell>보상일시({timezoneName})</TableCell>
                        <TableCell>생성일시({timezoneName})</TableCell>
                        <TableCell>수정일시({timezoneName})</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {list}
                </TableBody>
            </>)
        }
        else {
            result = (<>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 랭킹 ** **가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>)
        }
        return result;
    }, [datas])

    return contents();
};

export default RankingRewardContents;