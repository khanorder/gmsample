import { useAppSelector  } from '@hooks/index';
import { ChangeEvent, ReactElement, useCallback, useEffect, useState } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled, TextField, Autocomplete, } from '@mui/material';
import dynamic from 'next/dynamic';
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
    datas: Models.Entitlement[];
    setEntitlements: React.Dispatch<React.SetStateAction<Models.Entitlement[]>>
    opts: DataTableModels.EntitlementData[];
    onDelete: (index: number) => void;
}

const EditEntitlementContents = ({ userAccount, datas, setEntitlements, opts, onDelete }: ContentsProps): ReactElement => {
    const [entitlementOpts, setEntitlementOpts] = useState<DataTableModels.EntitlementData[]>([]);
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setEntitlementOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.EntitlementData, index: number) => {
        if(!v) return;
        if(isNaN(Number(v?.ID))) return;
        const isOverlap = datas.find((element)=> element.EntitlementID == Number(v.ID));
        if(isOverlap){
            alert("중복된 값입니다.");
            return;
        }

        setEntitlements(prev => {
            if(prev[index]){
                prev[index].EntitlementID = Number(v.ID);
            }
            return deepmerge([], prev);
        })
    }, [datas, setEntitlements]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (datas && 0 < datas.length) {
            const list: ReactElement[] = [];
    
            for (let i = 0; i < datas.length; i++) {
                const data = datas[i];
                let rowClass: string = commonUIStyles.row;
                if (data.isNewData)
                    rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;

                const entitleInfo = entitlementOpts.find(element => element.ID == data?.EntitlementID.toString());
                if(data){
                    list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell component="th" scope="row">
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>{data?.EntitlementID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>
                            <FormControl>
                                { data.isNewData ?
                                    <Autocomplete disabled={isSigned} options={entitlementOpts} sx={{width: 300}}
                                    getOptionLabel={(option) => (option as DataTableModels.EntitlementData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                    value={entitleInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.EntitlementData, i)}
                                    renderInput={(params) => <TextField {...params} label="칭호명" />} />
                                    :
                                    <Typography>{entitleInfo?.NameStringWithID ? entitleInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.EntitlementID ? data?.EntitlementID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</Typography>
                                }
                            </FormControl>
                        </TableCell>
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
                            <MinWidthTableCell>관리</MinWidthTableCell>
                            <MinWidthTableCell>칭호 ID</MinWidthTableCell>
                            <MinWidthTableCell>칭호명</MinWidthTableCell>
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
    }, [ isSigned, datas, entitlementOpts, onChangeData, onDelete ]);

    return <>{contents()}</>
}

export default EditEntitlementContents;