import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { ERewardType } from "@ngel/data/models/lobby";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.SilverMedalStore[];
}

const SilverMedalContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const heroTable = tables.characterDataTable;
    const skinTable = tables.skinDataTable;
    const itemTable = tables.itemTable;
    const assetTable = tables.assetDataTable;
    const silverMedalTable = tables.silverMedalStoreDataTable;

    let content: ReactElement = <></>;
    const FindItem =  useCallback((rewardType : ERewardType, ItemID: number) => {
        let itemInfo = null;
        if(ItemID){
            switch(rewardType){
                case ERewardType.Character:
                    itemInfo = heroTable.find(element => element.ID === ItemID);
                    break;
                case ERewardType.Costume:
                    itemInfo = skinTable.find(element => element.ID === ItemID);
                    break;
                case ERewardType.Item:
                    itemInfo = itemTable.find(element => element.ID === ItemID);
                    break;
                case ERewardType.Asset:
                    itemInfo = assetTable.find(element => element.ID === ItemID);
                    break;
            }
        }
        return itemInfo;
    }, [heroTable, skinTable, itemTable, assetTable]);
    
    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const silverMedalStoreData = silverMedalTable.find(element => element.ID === data?.StoreID);
            const item = FindItem(silverMedalStoreData?.RewardType ?? 0,  silverMedalStoreData?.ItemID ?? 0);

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data?.StoreID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{item?.NameStringWithID ? item?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : silverMedalStoreData?.ItemID ? silverMedalStoreData?.ItemID .toString().replaceAll(/(?<=.{1})./g, '*') : ""}</TableCell>
                    <TableCell>{data?.BuyCount}</TableCell>
                    <TableCell>{data?.NextResetAt ? dayjs.unix(data?.NextResetAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[silverMedalTable, datas, FindItem]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>상***</TableCell>
                        <TableCell>아**</TableCell>
                        <TableCell>구***</TableCell>
                        <TableCell>초****</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 실*** ** **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default SilverMedalContents;