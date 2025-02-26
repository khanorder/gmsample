import { ReactElement } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { useAppSelector } from '@hooks/index';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: Models.Entitlement[];
}

const EntitlementContents = ({ datas }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;
    const entitleTable = useAppSelector(state => state.tables.entitlementDataTable);
    
    if (datas && 0 < datas.length) {
        const list: ReactElement[] = [];

        for (let i = 0; i < datas.length; i++) {
            const data = datas[i];
            const entitleInfo = entitleTable.find(element => element.ID === data?.EntitlementID.toString());
            if(data){
                list.push(
                <BorderedTableRow key={i}>
                    <TableCell component="th" scope="row">{data?.EntitlementID}</TableCell>
                    <TableCell>{entitleInfo?.NameStringWithID ? entitleInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
                )
            }
        }

        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <MinWidthTableCell>칭* **</MinWidthTableCell>
                        <MinWidthTableCell>칭**</MinWidthTableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 칭호 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default EntitlementContents;