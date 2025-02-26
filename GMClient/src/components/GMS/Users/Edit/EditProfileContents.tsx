import { useAppSelector  } from '@hooks/index';
import { ChangeEvent, ReactElement, useCallback, useEffect, useState } from 'react';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled, TextField, Autocomplete, } from '@mui/material';
import dynamic from 'next/dynamic';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';
import { EProfileType } from '@ngel/data/models/lobby';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: Models.Profile[];
    setProfiles: React.Dispatch<React.SetStateAction<Models.Profile[]>>
    opts: DataTableModels.ProfileData[];
    onDelete: (index: number) => void;
}

const EditProfileContents = ({ userAccount, datas, setProfiles, opts, onDelete }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const [profileOpts, setProfileOpts] = useState<DataTableModels.ProfileData[]>([]);
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setProfileOpts(opts)
    }, [opts]);

    const onChangeData = useCallback((e, v: DataTableModels.ProfileData, index: number) => {
        if(!v) return;
        if(isNaN(Number(v?.ID))) return;
        const isOverlap = datas.find((element)=> element.ProfileID == Number(v.ID));
        if(isOverlap){
            alert("중복된 값입니다.");
            return;
        }

        setProfiles(prev => {
            if(prev[index]){
                prev[index].ProfileID = Number(v.ID);
                prev[index].ProfileType = Number(v.Type);
            }
            return deepmerge([], prev);
        })
    }, [datas, setProfiles]);

    const contents = useCallback(() => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        for(let i = 0; i < datas.length; i++){
            const data  = datas[i];
            const iconInfo = profileOpts.find(element => element?.ID === data?.ProfileID);
            let rowClass: string = commonUIStyles.row;
            if (data.isNewData)
                rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`;
            
            list.push(
                <BorderedTableRow key={i} className={rowClass}>
                    <TableCell component="th" scope="row">
                        <Button disabled={isSigned} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant='outlined' size='small' color='error' onClick={() => onDelete(i)}>삭제</Button>
                    </TableCell>
                    <TableCell>
                        <FormControl fullWidth>
                            { data.isNewData ?
                                <Autocomplete disabled={isSigned} options={profileOpts} fullWidth
                                getOptionLabel={(option) => (option as DataTableModels.ProfileData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                value={iconInfo} onChange={(e, v) => onChangeData(e, v as DataTableModels.ProfileData, i)}
                                renderInput={(params) => <TextField {...params} label="프로필 아이템명" />} />
                                :
                                <Typography>{iconInfo?.NameStringWithID ? iconInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : data?.ProfileID ? iconInfo?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : ""}</Typography>
                            }
                        </FormControl>
                    </TableCell>
                    <TableCell>{EProfileType.Icon == data?.ProfileType ? '아이콘' : (EProfileType.BG == data?.ProfileType ? '배경' : data?.ProfileType)}</TableCell>
                    <TableCell>{data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                    <TableCell>{data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""}</TableCell>
                </BorderedTableRow>
            )
        }
        result = (<>{list}</>);
        return result;
    }, [isSigned, datas, profileOpts, onChangeData, onDelete]);

    if(datas && 0 < datas.length){
        content = (
            <>
                <TableHead>
                    <BorderedTableRow>
                        <TableCell>관리</TableCell>
                        <TableCell>이름</TableCell>
                        <TableCell>타입(아이콘/배경)</TableCell>
                        <TableCell>생성시간({timezoneName})</TableCell>
                        <TableCell>수정시간({timezoneName})</TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    {contents()}
                </TableBody>
            </>
        )
    } else {
        content = (
            <TableBody>
                <BorderedTableRow>
                    <TableCell colSpan={3} className={commonUIStyles.noneCell}>검색된 프로필 배경 정보가 없습니다.</TableCell>
                </BorderedTableRow>
            </TableBody>
        );
    }
    
    return content;
}

export default EditProfileContents;