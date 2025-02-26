import { ReactElement } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { useAppSelector } from '@hooks/index';
import { EWeaponCategory } from '@ngel/data/models/lobby';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: Models.WeaponCategory[];
}

const WeaponCategoryContents = ({ datas }: ContentsProps): ReactElement => {

    let result: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables)
    const weaponCategoriesTable = tables.weaponCategoryDataTable;
    const weaponUpgradeDataTable  = tables.weaponCategoryUpgradeDataTable;
    
    if (weaponCategoriesTable && 0 < weaponCategoriesTable.length) {
        const list: ReactElement[] = [];

        for (let i = 0; i < weaponCategoriesTable.length; i++) {
            const data = datas.find((data) => data?.WeaponCategoryID === weaponCategoriesTable[i]?.ID);
            
            let level = 0;
            if(data){
                const filterd = weaponUpgradeDataTable.filter(element => element.WeaponCategoryID == data?.WeaponCategoryID).sort((a, b) => b.Exp - a.Exp);
                if(0 < filterd?.length){
                    level = filterd.find(element => element.Exp <= data.Exp)?.Level ?? filterd[0].Level;
                }
            }
            list.push(
                <BorderedTableRow key={i}>
                    <TableCell>{ `${EWeaponCategory[weaponCategoriesTable[i]?.WeaponCategory ?? 0].toString().replaceAll(/(?<=.{3})./g, '*')} (${weaponCategoriesTable[i]?.WeaponCategory ?? 0})` }</TableCell>
                    <TableCell>{ weaponCategoriesTable[i]?.NameStringWithID.toString().replaceAll(/(?<=.{3})./g, '*') }</TableCell>
                    <TableCell>{ level }</TableCell>
                    <TableCell>{ data?.Exp ?? 0 }</TableCell>
                    <TableCell>{ data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{ data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }

        result = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <MinWidthTableCell>무기 **** ****</MinWidthTableCell>
                        <MinWidthTableCell>무기 **** **</MinWidthTableCell>
                        <MinWidthTableCell>무기 **** **</MinWidthTableCell>
                        <MinWidthTableCell>경험치</MinWidthTableCell>
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
                    <TableCell className={commonUIStyles.noneCell}>검색된 무기 **** **가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }

    return result;
}

export default WeaponCategoryContents;