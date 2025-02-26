import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { EItemType } from "@ngel/data/models/lobby";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.EventStore[];
}

const EventStoreContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const itemTable = tables.itemTable;
    const assetTable = tables.assetDataTable;
    const storeTable = tables.eventStoreDataTable;
    let content: ReactElement = <></>;

    const FindItem =  useCallback((ItemType : EItemType, ItemID: number) => {
        let itemInfo = null;
        if(ItemID){
            switch(ItemType){
                case EItemType.Accessory_Head:
                case EItemType.Accessory_Face:
                case EItemType.Accessory_Pelvis:
                case EItemType.Accessory_Back:
                case EItemType.Accessory_Weapon:
                    itemInfo = itemTable.find(element => element.ID === ItemID);
                    break;
                default:
            }
        }
        return itemInfo;
    }, [itemTable]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i< datas.length; i++){
            const data = datas[i];
            const storeInfo = storeTable.find(element => element.ID === data.StoreID);
            let item = null;
            let asset = null;
            if(storeInfo){
                item = itemTable.find(element => element.ID === storeInfo?.ItemID);
                asset = assetTable.find(element => element?.ItemID === storeInfo?.ReqAssetID);
            }

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data?.StoreID}</TableCell>
                    <TableCell>{item?.NameStringWithID ? item?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : storeInfo?.ItemID ?? ""}</TableCell>
                    <TableCell>{storeInfo?.ItemCount ?? ""}</TableCell>
                    <TableCell>{data?.BuyCount}</TableCell>
                    <TableCell>{asset?.NameStringWithID ? asset?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : storeInfo?.ReqAssetID ? storeInfo?.ReqAssetID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                    <TableCell>{storeInfo?.ReqAssetCount}</TableCell>
                    <TableCell>{data?.ExpireAt ? dayjs.unix(data?.ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss'): ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[assetTable, itemTable, storeTable, datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>상점ID</TableCell>
                        <TableCell>아이템</TableCell>
                        <TableCell>개수</TableCell>
                        <TableCell>구매 횟수</TableCell>
                        <TableCell>필요 재화</TableCell>
                        <TableCell>필요 재화 수량</TableCell>
                        <TableCell>종료시각({timezoneName})</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 이벤* ** 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default EventStoreContents;