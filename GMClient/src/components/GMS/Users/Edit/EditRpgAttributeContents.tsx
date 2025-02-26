import { useAppSelector } from '@hooks/index';
import { ChangeEvent, ReactElement, MutableRefObject, useCallback, useState, useEffect } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TextField, Autocomplete, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from '@ngel/data/tables/model';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: PaginatedList<Models.RpgAttribute>;
    opts: DataTableModels.AttributeData[];
    onDelete: (index: number) => void;
}

const EditRpgAttributeContents = ({ userAccount, datas, opts, onDelete }: ContentsProps): ReactElement => {

    const tables = useAppSelector(state => state.tables);
    const [attributes, setAttributes] = useState<PaginatedList<Models.RpgAttribute>>(new PaginatedList<Models.RpgAttribute>([]));
    const [attributeOpts, setAttributeOpts] = useState<DataTableModels.AttributeData[]>([]);
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setAttributes(datas)
    }, [datas]);

    useEffect(() => {
        setAttributeOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.AttributeData, index: number) => {
        if(!v) return;

        const isOverlap = attributes.totalItems.find((element)=> element.ID === v.ID);
        if(isOverlap){
            alert("중복된 값입니다.");
            return;
        }

        setAttributes(prev => {
            if(prev.totalItems[(prev.page-1)*prev.pageSize+index]){
                prev.totalItems[(prev.page-1)*prev.pageSize+index].ID = v.ID;
                prev.totalItems[(prev.page-1)*prev.pageSize+index].isChanged = true;
            }
            return new PaginatedList<Models.RpgAttribute>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [attributes, setAttributes]);

    const onChangeLevel = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, maxLevel: number, index: number) => {
        if(!e.target.value || isNaN(Number(e.target.value))) return;

        setAttributes(prev => {
            if(prev.totalItems[(prev.page-1)*prev.pageSize+index]){
                prev.totalItems[(prev.page-1)*prev.pageSize+index].Level = Number(e.target.value) <= maxLevel ? Number(e.target.value) : maxLevel;
                prev.totalItems[(prev.page-1)*prev.pageSize+index].isChanged = true;
            }
            return new PaginatedList<Models.RpgAttribute>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    },[setAttributes]);


    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;

        if (attributes && 0 < attributes?.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i <  attributes.items.length; i++) {
                const data = attributes.items[i];
                const attributeInfo = attributeOpts.find(element => element?.ID === data?.ID) ?? attributeOpts[0];;

                let rowClass: string = commonUIStyles.row;
                if(data.isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`


                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell component="th" scope="row">
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                { data.isNewData ?
                                    <Autocomplete disabled={isSigned} options={attributeOpts} sx={{width: 300}}
                                    getOptionLabel={(option) => (option as DataTableModels.AttributeData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                    value={attributeInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.AttributeData, i)}
                                    renderInput={(params) => <TextField {...params} label="유물명" />} />
                                :
                                <Typography>{attributeInfo?.NameStringWithID ? attributeInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <TextField disabled={isSigned} value={data?.Level} onChange={(e) => {onChangeLevel(e, attributeInfo?.MaxLevel ?? 0, i)}}/>
                        </TableCell>
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
                        <MinWidthTableCell>관리</MinWidthTableCell>
                        <MinWidthTableCell>특성 ID</MinWidthTableCell>
                        <MinWidthTableCell>특성 레벨</MinWidthTableCell>
                        <MinWidthTableCell>특성 최대레벨</MinWidthTableCell>
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
    }, [isSigned, attributes, attributeOpts, onChangeData, onChangeLevel, onDelete])

    return <>{contents()}</>
}

export default EditRpgAttributeContents;