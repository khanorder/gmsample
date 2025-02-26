import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { table } from "console";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

interface ContentsProps {
    datas: Models.TreasureBox[];
}

const TreasureBoxContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const treasureTable = tables.treasureBoxDataTable;
    const itemTable = tables.itemTable;
    const assetTable = tables.assetDataTable;
    let content: ReactElement = <></>;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if(datas){
            for(let i = 0; i < datas[0].BoxList.length; i++){
                const data = datas[0].BoxList[i];
                const boxInfo = treasureTable.find(element => element.ID == data.BoxID);
                console.log(boxInfo);
                list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data?.BoxID}</TableCell>
                    <TableCell>{data?.IsOpened ? "O" : "X"}</TableCell>
                    <TableCell>
                        { boxInfo && boxInfo.RewardItemID[0] != 0 ? 
                            <Typography>
                            {`${
                                boxInfo.RewardItemID.map(itemID => {
                                const itemInfo = itemTable.find(element => element.ID == itemID);
                                return itemInfo?.NameStringWithID ? itemInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : itemID.toString().replaceAll(/(?<=.{1})./g, '*');
                            })}
                            ${
                                boxInfo.RewardItemCount.map(count => { return `  ${count}개`})
                            }`}
                            </Typography>
                        : <></>
                        }
                        { boxInfo && boxInfo.AssetType[0] != 0 ? 
                            <Typography>
                            {`${
                                boxInfo.AssetType.map(assetType => {
                                const assetInfo = assetTable.find(element => element.ID == assetType);
                                return assetInfo?.NameStringWithID ? assetInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : assetType.toString().replaceAll(/(?<=.{1})./g, '*');
                            })}
                            ${
                                boxInfo.AssetCount.map(count => { return `${count}개`})
                            }`}
                            </Typography>
                        : <></>
                        }
                    </TableCell>
                    <TableCell>{datas[0].ExpireAt ? dayjs.unix(datas[0].ExpireAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{datas[0]?.CreateAt ? dayjs(datas[0]?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{datas[0]?.UpdateAt ? dayjs(datas[0]?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
            }
        }
        result = (<>{list}</>);
        return result;
    }, [datas, treasureTable, itemTable, assetTable])

    if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>박***</TableCell>
                        <TableCell>오* **</TableCell>
                        <TableCell>보* **</TableCell>
                        <TableCell>유효시간({timezoneName})</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 트**** **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
}

export default TreasureBoxContents;