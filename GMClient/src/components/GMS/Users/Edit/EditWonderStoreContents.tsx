import { ReactElement, useCallback, useEffect, useState } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import deepmerge from "deepmerge";
import { SelectChangeEvent } from '@mui/material/Select';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const DateTimePicker = dynamic(() => import('@components/ui/DateTimePicker'), { ssr: false });

interface ContentsProps {
    datas: Models.WonderStore[];
    setDatas: React.Dispatch<React.SetStateAction<Models.WonderStore[]>>;
}

const WonderStoreContents = ({ datas, setDatas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const seasonPassDataTable = tables.seasonPassDataTable;
    const storeTable = tables.wonderStoreDataTable;
    let content: ReactElement = <></>;

    const onChangeSubscription = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        setDatas(prev => {
            prev[index].IsSubscription = e.target.value as boolean;
            prev[index].isChanged = true;
            return deepmerge([], prev);
        })
    }, [setDatas])

    const onChangeSubscriptionExpireAt = useCallback((date: string | null, index: number) => {
        setDatas(prev => {
            let unixTimeDate: number = 0;
        
            try {
                unixTimeDate = dayjs(date).unix();
            } catch (error) {
                return prev;
            }
            
            prev[index].SubscriptionExpireAt = isNaN(unixTimeDate) ? 0 : unixTimeDate;
            prev[index].isChanged = true;
            return deepmerge([], prev);
        });
    }, [setDatas]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const seasonPassInfo = seasonPassDataTable.find(element => element.ID == data.SeasonPassID)
            const storeInfo = storeTable.find(element => element.ID === data.StoreID);

            let rowClass: string = commonUIStyles.row;
            if (data.isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

            list.push(
                <BorderedTableRow key={i} className={rowClass}>
                    <TableCell>{data?.StoreID}</TableCell>
                    <TableCell>{storeInfo?.ProductNameStringWithID ? storeInfo?.ProductNameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.StoreID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>{data?.BuyCount}</TableCell>
                    <TableCell>{seasonPassInfo?.NameStringWithID ? seasonPassInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.SeasonPassID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>
                        <Select size='small' sx={{ color: data?.IsSubscription ? 'blue' : 'red' }} value={data?.IsSubscription} onChange={(e) => {onChangeSubscription(e, i)}}>
                            <MenuItem sx={{ color: 'red' }} value={false as any}>미구독</MenuItem>
                            <MenuItem sx={{ color: 'blue' }} value={true as any}>구독</MenuItem>
                        </Select>
                    </TableCell>
                    <TableCell>
                        <DateTimePicker label={`구독유효 시간(${timezoneName})`} enableTimePicker={true} format="YYYY-MM-DD HH:mm" value={data.SubscriptionExpireAt > 0 ? dayjs.unix(data?.SubscriptionExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : null} onChange={(date) => {onChangeSubscriptionExpireAt(date, i)}} />
                    </TableCell>
                    <TableCell>{data?.BuyAbleStartAt ? dayjs.unix(data?.BuyAbleStartAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.BuyAbleEndAt ? dayjs.unix(data?.BuyAbleEndAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[storeTable, seasonPassDataTable, datas, onChangeSubscription, onChangeSubscriptionExpireAt]);

    if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>상점ID</TableCell>
                        <TableCell>아이템</TableCell>
                        <TableCell>구매수량</TableCell>
                        <TableCell>시즌패스ID</TableCell>
                        <TableCell>구독여부</TableCell>
                        <TableCell>구독유효 시간({timezoneName})</TableCell>
                        <TableCell>구매시작 시간({timezoneName})</TableCell>
                        <TableCell>구매종료 시간({timezoneName})</TableCell>
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