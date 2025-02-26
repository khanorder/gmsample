import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { DataTableModels } from "@ngel/data/tables/model";

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });

interface ContentsProps {
    datas: Models.GlitchStore[];
}

const GlitchStoreContents = ({ datas } : ContentsProps) : ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const storeTable = tables.glitchStoreDataTable;
    const artifactTable = tables.artifactDataTable;
    const petTable = tables.petDataTable;
    let content: ReactElement = <></>;

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < datas.length; i++){
            const data = datas[i];
            const storeInfo = storeTable.find(element => element.ID === data.StoreID);
            let product: string = '';
            if (storeInfo) {
                switch (storeInfo.ProductType) {
                    case 'Artifact':
                        const artifactData = artifactTable.find(element => element.ID === storeInfo?.ProductItem);
                        if (null === artifactData) {
                            product = storeInfo.ProductItem?.toString();
                        } else {
                            product = artifactData?.NameStringWithID ?? storeInfo.ProductItem?.toString();
                        }
                        break;

                    case 'Pet':
                        const petData = petTable.find(element => element.ID === storeInfo?.ProductItem);
                        if (null === petData) {
                            product = storeInfo.ProductItem?.toString();
                        } else {
                            product = petData?.NameStringWithID ?? storeInfo.ProductItem?.toString();
                        }
                        break;

                    default:
                        product = storeInfo.ProductItem?.toString();
                        break;
                }
            }

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{data?.StoreID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>{product.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    },[artifactTable, storeTable, datas, petTable]);

   if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>상점ID</TableCell>
                        <TableCell>아이템</TableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 글리* ** **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return content;
} 

export default GlitchStoreContents;