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
    datas: Models.WonderCube[];
}

const WonderCubeContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const cubeTable = tables.wonderCubeDataTable;
    const assetTable = tables.assetDataTable;
    let content: ReactElement = <></>;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const cubeInfo = cubeTable.find(element => element.ID == data.WonderCubeID);
            const assetInfo1 = cubeInfo ? assetTable.find(element => element.ID == cubeInfo.AssetType1) : null;
            const assetInfo2 = cubeInfo ? assetTable.find(element => element.ID == cubeInfo.AssetType2) : null;
            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data.SlotID}</TableCell>
                    <TableCell>{cubeInfo?.NameStringWithID ? cubeInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.WonderCubeID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{`${assetInfo1?.NameStringWithID ? assetInfo1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : cubeInfo?.AssetType1 ? cubeInfo?.AssetType1.toString().replaceAll(/(?<=.{1})./g, '*') : ''}`}</TableCell>
                    <TableCell>{`${cubeInfo?.AssetCount1[0] ? cubeInfo?.AssetCount1[0].toString().replaceAll(/(?<=.{1})./g, '*') : ''} ~ ${cubeInfo?.AssetCount1[cubeInfo?.AssetCount1?.length-1] ? cubeInfo?.AssetCount1[cubeInfo?.AssetCount1?.length-1].toString().replaceAll(/(?<=.{1})./g, '*') : ''}`}</TableCell>
                    <TableCell>{`${assetInfo2?.NameStringWithID ? assetInfo2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : cubeInfo?.AssetType2 ? cubeInfo?.AssetType2.toString().replaceAll(/(?<=.{1})./g, '*') : ''}`}</TableCell>
                    <TableCell>{`${cubeInfo?.AssetCount2[0] ? cubeInfo?.AssetCount2[0].toString().replaceAll(/(?<=.{1})./g, '*') : ''} ~ ${cubeInfo?.AssetCount2[cubeInfo?.AssetCount2?.length-1] ? cubeInfo?.AssetCount2[cubeInfo?.AssetCount2?.length-1].toString().replaceAll(/(?<=.{1})./g, '*') : ''}`}</TableCell>
                    <TableCell>{dayjs(data?.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    <TableCell>{dayjs(data?.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[cubeTable, assetTable, datas]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>슬롯ID</TableCell>
                        <TableCell>큐브ID</TableCell>
                        <TableCell>보상 재화1</TableCell>
                        <TableCell>보상 범위1</TableCell>
                        <TableCell>보상 재화2</TableCell>
                        <TableCell>보상 범위2</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 큐브 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default WonderCubeContents;