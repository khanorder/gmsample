import { useAppSelector } from '@hooks/index';
import { ChangeEvent, ReactElement, useCallback, useEffect, useState } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled, TextField, Autocomplete, } from '@mui/material';
import dynamic from 'next/dynamic';
import { EWeaponCategory } from '@ngel/data/models/lobby';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userAccount: Models.UserAccount;
    weaponCategories: Models.WeaponCategory[];
    setWeaponCategories: React.Dispatch<React.SetStateAction<Models.WeaponCategory[]>>
    opts: DataTableModels.WeaponCategoryData[];
    onDelete: (index: number) => void;
}

const EditWeaponCategoryContents = ({ userAccount, weaponCategories, setWeaponCategories, opts, onDelete }: ContentsProps): ReactElement => {
    const tables = useAppSelector(state => state.tables)
    const weaponUpgradeDataTable = tables.weaponCategoryUpgradeDataTable;

    const [weaponCateroryOpts, setWeaponCateroryOpts] = useState<DataTableModels.WeaponCategoryData[]>([]);
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setWeaponCateroryOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.WeaponCategoryData, index: number) => {
        if (!v) return;

        const isOverlap = weaponCategories.find((element) => element.WeaponCategoryID === v.ID);
        if (isOverlap) {
            alert("중복된 값입니다.");
            return;
        }

        setWeaponCategories(prev => {
            if (prev[index]) {
                prev[index].WeaponCategoryID = v.ID;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        })
    }, [weaponCategories, setWeaponCategories]);

    const onChangeExp = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
        if (isNaN(Number(e.target.value))) return;

        setWeaponCategories(prev => {
            if (prev[index]) {
                const filterd = weaponUpgradeDataTable.filter(element => element.WeaponCategoryID == prev[index].WeaponCategoryID);
                let maxValue = 0;
                if (0 < filterd.length) {
                    maxValue = filterd[filterd.length - 1].Exp;
                }
                prev[index].Exp = Number(e.target.value) < maxValue ? Number(e.target.value) : maxValue;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        })
    }, [weaponUpgradeDataTable, setWeaponCategories]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        if (weaponCategories && 0 < weaponCategories.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < weaponCategories.length; i++) {
                const data = weaponCategories[i];
                let rowClass: string = commonUIStyles.row;
                if (data.isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`

                const filterd = weaponUpgradeDataTable.filter(element => element.WeaponCategoryID == data?.WeaponCategoryID).sort((a, b) => b.Exp - a.Exp);
                let level = 0;
                if (0 < filterd?.length) {
                    level = filterd.find(element => element.Exp <= data.Exp)?.Level ?? filterd[0].Level;
                }
                const weaponCateInfo = weaponCateroryOpts.find((element) => element?.ID === data?.WeaponCategoryID);

                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell component="th" scope="row">
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>{`${EWeaponCategory[weaponCateInfo?.WeaponCategory ?? 0].toString().replaceAll(/(?<=.{2})./g, '*')} (${weaponCateInfo?.WeaponCategory ?? 0})`}</TableCell>
                        <TableCell>
                            <FormControl>
                                {
                                    data.isNewData 
                                        ?
                                        <Autocomplete disabled={isSigned} options={weaponCateroryOpts} sx={{ width: 300 }}
                                            getOptionLabel={(option) => (option as DataTableModels.WeaponCategoryData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                            value={weaponCateInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.WeaponCategoryData, i)}
                                            renderInput={(params) => <TextField {...params} label="유물명" />} />
                                        :
                                        <Typography>{weaponCateInfo?.NameStringWithID ? weaponCateInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.WeaponCategoryID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>{level ?? 0}</TableCell>
                        <TableCell>
                            <TextField disabled={isSigned || data?.WeaponCategoryID == 0} value={data?.Exp} onChange={(e) => { onChangeExp(e, i) }} />
                        </TableCell>
                        <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                        <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>관리</MinWidthTableCell>
                            <MinWidthTableCell>무기 카테고리 (번호)</MinWidthTableCell>
                            <MinWidthTableCell>무기 카테고리 이름</MinWidthTableCell>
                            <MinWidthTableCell>무기 카테고리 레벨</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 무기 카테고리 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }

        return result;
    }, [weaponUpgradeDataTable, isSigned, onChangeData, onChangeExp, onDelete, weaponCategories, weaponCateroryOpts])

    return <>{contents()}</>
}

export default EditWeaponCategoryContents;