import { useAppSelector } from "@hooks/index";
import { ReactElement, useState, useEffect, useCallback } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import dynamic from 'next/dynamic';
import { DataTableModels } from "@ngel/data/tables/model";
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.Asset[];
}

const AssetContents = ({ datas }: ContentsProps): ReactElement => {

    let content: ReactElement = <></>;
    
    const table = useAppSelector(state => state.tables);
    const [assets, setAssets] = useState<DataTableModels.AssetData[]>([]);

    useEffect(()=> {
        const assetTable = table.assetDataTable;
        setAssets(assetTable);
    }, [table])

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < assets.length; i++){
            const data = datas.find((element)=> element?.AssetID === assets[i]?.ItemID);
            if(data !== undefined && data !== null){
                list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{assets[i]?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>{data?.Count ? data?.Count.toLocaleString().toString().replaceAll(/(?<=.{2})./g, '*') : 0}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
                );
            }
            else{
                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell>{assets[i]?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>0</TableCell>
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                    </BorderedTableRow>
                    )
            }
        }
        result = (
            <>
                {list}
            </>
        );
        return result;
    }, [datas, assets]);
    

    content = (
        <>
            <TableHead>
                <BorderedTableRow>
                    <TableCell>As*****</TableCell>
                    <TableCell>수량</TableCell>
                    <TableCell>생성일시({timezoneName})</TableCell>
                    <TableCell>갱신일시({timezoneName})</TableCell>
                </BorderedTableRow>
            </TableHead>
            <TableBody>
                {contents()}
            </TableBody>
        </>
    );
    
    return content;
}

export default AssetContents;