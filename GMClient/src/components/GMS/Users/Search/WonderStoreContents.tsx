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

interface ContentsProps {
    datas: Models.WonderStore[];
}

const WonderStoreContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const seasonPassDataTable = tables.seasonPassDataTable;
    const storeTable = tables.wonderStoreDataTable;
    let content: ReactElement = <></>;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const seasonPassInfo = seasonPassDataTable.find(element => element.ID == data.SeasonPassID)
            const storeInfo = storeTable.find(element => element.ID === data.StoreID);

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data?.StoreID}</TableCell>
                    <TableCell>{storeInfo?.ProductNameStringWithID ? storeInfo?.ProductNameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*') : data?.StoreID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.BuyCount}</TableCell>
                    <TableCell>{seasonPassInfo?.NameStringWithID ? seasonPassInfo?.NameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*') : data?.SeasonPassID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell sx={data?.IsSubscription ? {color: 'rgba(0, 0, 255, 0.8)'} : {color: 'rgba(255, 0, 0, 0.8)'}}>{data?.IsSubscription ? "구독" : "미구독"}</TableCell>
                    <TableCell>{data?.SubscriptionExpireAt ? dayjs.unix(data?.SubscriptionExpireAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.BuyAbleStartAt ? dayjs.unix(data?.BuyAbleStartAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.BuyAbleEndAt ? dayjs.unix(data?.BuyAbleEndAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[storeTable, seasonPassDataTable, datas]);

    if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>상점ID</TableCell>
                        <TableCell>아이템</TableCell>
                        <TableCell>구매수량</TableCell>
                        <TableCell>시즌****</TableCell>
                        <TableCell>구독여부</TableCell>
                        <TableCell>구독유효 시간({timezoneName})</TableCell>
                        <TableCell>구매가능 시작시간({timezoneName})</TableCell>
                        <TableCell>구매가능 종료시간({timezoneName})</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 상점 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default WonderStoreContents;