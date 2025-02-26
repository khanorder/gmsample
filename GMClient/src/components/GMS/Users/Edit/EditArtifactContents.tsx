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
    datas: PaginatedList<Models.Artifact>;
    opts: DataTableModels.ArtifactData[];
    onDelete: (index: number) => void;
}

const EditArtifactContents = ({ userAccount, datas, opts, onDelete }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const [artifactOpts, setArtifactOpts] = useState<DataTableModels.ArtifactData[]>([]);
    const [artifacts, setArtifacts] = useState<PaginatedList<Models.Artifact>>(new PaginatedList<Models.Artifact>([]));
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setArtifacts(datas)
    }, [datas]);

    useEffect(() => {
        setArtifactOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.ArtifactData, index: number) => {
        if(!v) return;

        const isOverlap = artifacts.totalItems.find((element)=> element.ArtifactID === v.ID);
        if(isOverlap){
            alert("중복된 값입니다.");
            return;
        }

        setArtifacts(prev => {
            if(prev.totalItems[(prev.page-1)*prev.pageSize+index]){
                prev.totalItems[(prev.page-1)*prev.pageSize+index].ArtifactID = v.ID;
                prev.totalItems[(prev.page-1)*prev.pageSize+index].isChanged = true;
            }
            return new PaginatedList<Models.Artifact>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [artifacts, setArtifacts]);

    const onChangeEnhance = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, maxEnhance: number, index: number) => {
        if(!e.target.value || isNaN(Number(e.target.value))) return;

        setArtifacts(prev => {
            if(prev.totalItems[(prev.page-1)*prev.pageSize+index]){
                prev.totalItems[(prev.page-1)*prev.pageSize+index].Enhance = Number(e.target.value) <= maxEnhance ? Number(e.target.value) : maxEnhance;
                prev.totalItems[(prev.page-1)*prev.pageSize+index].isChanged = true;
            }
            return new PaginatedList<Models.Artifact>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    },[setArtifacts]);

    const onChangeCount = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        if(!e.target.value || isNaN(Number(e.target.value))) return;

        setArtifacts(prev => {
            if(prev.totalItems[(prev.page-1)*prev.pageSize+index]){
                prev.totalItems[(prev.page-1)*prev.pageSize+index].Count =  Number(e.target.value);
                prev.totalItems[(prev.page-1)*prev.pageSize+index].isChanged = true;
            }
            return new PaginatedList<Models.Artifact>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    },[setArtifacts]);

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        
        if (artifacts && 0 < artifacts?.items.length) {
            const list: ReactElement[] = [];

            for (let i = 0; i < artifacts.items.length; i++) {
                const data = artifacts.items[i];

                let rowClass: string = commonUIStyles.row;
                if(data.isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`

                let dataItem = artifactOpts.find((element)=> element?.ID == data?.ArtifactID) ?? artifactOpts[0];
                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell component="th" scope="row">
                            <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                        </TableCell>
                        <TableCell>
                            <FormControl>
                                { data.isNewData ?
                                    <Autocomplete disabled={isSigned} options={artifactOpts} sx={{width: 300}}
                                    getOptionLabel={(option) => (option as DataTableModels.ArtifactData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                    value={dataItem} onChange={(e, v) => onChangeData(e, v as DataTableModels.ArtifactData, i)}
                                    renderInput={(params) => <TextField {...params} label="유물명" />} />
                                :
                                <Typography>{dataItem?.NameStringWithID ? dataItem?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ArtifactID.toString().replaceAll(/(?<=.{2})./g, '*')}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>
                            <TextField disabled={isSigned} value={data?.Enhance} onChange={(e) => {onChangeEnhance(e, dataItem?.MaxEnhance ?? 0, i)}}/>
                        </TableCell>
                        <TableCell>
                            <TextField disabled={isSigned} value={data?.Count} onChange={(e) => {onChangeCount(e, i)}}/>
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
                            <MinWidthTableCell>유물명(아이템ID)</MinWidthTableCell>
                            <MinWidthTableCell>유물 강화수치</MinWidthTableCell>
                            <MinWidthTableCell>각성 재료수량</MinWidthTableCell>
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
                        <TableCell className={commonUIStyles.noneCell}>검색된 유물 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            );
        }
        return result
    }, [isSigned, artifactOpts, artifacts, onChangeCount, onChangeData, onChangeEnhance, onDelete]);

    content = ( <>
     {contents()}
     </>);

    return content;
}

export default EditArtifactContents;