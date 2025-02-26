import { useAppSelector } from '@hooks/index';
import { ChangeEvent, ReactElement, MutableRefObject, useCallback, useState, useEffect } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TextField, Autocomplete, styled, SelectChangeEvent } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { DataTableModels } from '@ngel/data/tables/model';
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });


const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

class OptionType {
    ID: number;
    CollectionID: number;
    CollectionType: number;
    Name: string;
    NameString: string;
    NameStringWithID: string;

    constructor(ID?: number, CollectionID?: number, CollectionType?: number, Name?: string, NameString?: string, NameStringWithID?: string) {
        this.ID = ID ?? 0;
        this.CollectionID = CollectionID ?? 0;
        this.CollectionType = CollectionType ?? 0,
            this.Name = Name ?? '';
        this.NameString = NameString ?? '';
        this.NameStringWithID = NameStringWithID ?? '';
    }
}

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: PaginatedList<Models.Collection>;
    opts: OptionType[];
    onDelete: (index: number) => void;
}

const EditCollectionContents = ({ userAccount, datas, opts, onDelete }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const collectionGroupDataTable = tables.collectionGroupDataTable
    const [collectionOpts, setCollectionOpts] = useState<OptionType[]>([]);
    const [collections, setCollections] = useState<PaginatedList<Models.Collection>>(new PaginatedList<Models.Collection>([]));
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setCollections(datas)
    }, [datas]);

    useEffect(() => {
        setCollectionOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: OptionType, index: number) => {
        if (!v) return;

        const isOverlap = collections.totalItems.find((element) => element.CollectionID === v.CollectionID);
        if (isOverlap) {
            alert("중복된 값입니다.");
            return;
        }

        setCollections(prev => {
            if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].CollectionID = v.CollectionID;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].CollectionType = v.CollectionType;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            }
            return new PaginatedList<Models.Collection>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [collections, setCollections]);

    const onChangeRewardType = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        let value = false;
        if (e && e.target && e.target.value) {
            try {
                value = e.target.value as boolean ?? false;
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }

        setCollections(prev => {
            if (prev.totalItems[(prev.page - 1) * prev.pageSize + index]) {
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].IsRewarded = value;
                prev.totalItems[(prev.page - 1) * prev.pageSize + index].isChanged = true;
            }
            return new PaginatedList<Models.Collection>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })

    }, [setCollections]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        if (datas && 0 < collections.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < collections.items.length; i++) {
                const data = collections.items[i];
                const type = collectionGroupDataTable.find(element => element.ID === data?.CollectionType)?.CategoryNameStringWithID ?? "";
                const optInfo = collectionOpts.find(element => element.CollectionID == data?.CollectionID);

                list.push(
                    <BorderedTableRow key={i}>
                        <TableCell component="th" scope="row">
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>{type ? type.toString().replaceAll(/(?<=.{2})./g, '*') : data?.CollectionType.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>{data?.CollectionID.toString().replaceAll(/(?<=.{2})./g, '*')}</TableCell>
                        <TableCell>
                            <FormControl>
                                {
                                    data.isNewData
                                        ?
                                        <Autocomplete disabled={isSigned} options={collectionOpts} sx={{ width: 300 }} size='small'
                                            getOptionLabel={(option) => (option as OptionType)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                            value={optInfo} onChange={(e, v) => onChangeData(e, v as OptionType, i)}
                                            renderInput={(params) => <TextField {...params} label="유물명" />} />
                                        :
                                        <Typography>{optInfo?.NameStringWithID ? optInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.CollectionID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <Select disabled={isSigned} sx={{ color: data?.IsRewarded ? 'blue' : 'red' }} size='small' value={data?.IsRewarded} onChange={(e) => { onChangeRewardType(e, i) }}>
                                <MenuItem sx={{ color: 'red' }} value={false as any}>미수령</MenuItem>
                                <MenuItem sx={{ color: 'blue' }} value={true as any}>수령</MenuItem>
                            </Select>
                        </TableCell>
                        <TableCell>{dayjs(data.CreateAt).format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                        <TableCell>{dayjs(data.UpdateAt).format('YYYY-MM-DD HH:mm:ss')}</TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell>관리</MinWidthTableCell>
                            <MinWidthTableCell>도감 타입</MinWidthTableCell>
                            <MinWidthTableCell>도감 ID</MinWidthTableCell>
                            <MinWidthTableCell>도감 항목 정보</MinWidthTableCell>
                            <MinWidthTableCell>보상 수령</MinWidthTableCell>
                            <MinWidthTableCell>생성일시</MinWidthTableCell>
                            <MinWidthTableCell>수정일시</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 도감 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
        return result;
    }, [collectionGroupDataTable, isSigned, datas, collectionOpts, collections, onChangeData, onChangeRewardType, onDelete])

    content = (<>
        {contents()}
    </>);

    return content;
}

export default EditCollectionContents;