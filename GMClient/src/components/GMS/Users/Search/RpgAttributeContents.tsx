import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { useAppSelector } from '@hooks/index';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: PaginatedList<Models.RpgAttribute>;
}

const RpgAttributeContents = ({ datas }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const attributeTable = tables.attributeDataTable;

    if (datas && 0 < datas?.items.length) {
        const list: ReactElement[] = [];

        for (let i = 0; i <  datas.items.length; i++) {
            const data = datas.items[i];
            const attributeInfo = attributeTable.find(element => element?.ID === data?.ID);

            list.push(
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{attributeInfo?.NameStringWithID ? attributeInfo?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*') : data?.ID.toString().replaceAll(/(?<=.{1})./g, '*')}</TableCell>
                    <TableCell>{data?.Level}</TableCell>
                    <TableCell>{attributeInfo?.MaxLevel ?? ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <MinWidthTableCell>특* **</MinWidthTableCell>
                        <MinWidthTableCell>특* **</MinWidthTableCell>
                        <MinWidthTableCell>특* ****</MinWidthTableCell>
                        <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                        <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {list}
                </TableBody>
            </>
        );
    } else {
        result = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell className={commonUIStyles.noneCell}>검색된 특성 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default RpgAttributeContents;