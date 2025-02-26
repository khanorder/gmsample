import { ReactElement, useCallback } from "react";
import dynamic from "next/dynamic";
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Models } from "@ngel/data/models";
import { useAppSelector } from "@hooks/index";
import { DataTableModels } from "@ngel/data/tables/model";
import { TextField, Autocomplete, styled } from '@mui/material';
import deepmerge from 'deepmerge';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });

export interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: Models.WonderCube[];
    setDatas: React.Dispatch<React.SetStateAction<Models.WonderCube[]>>
    opts: DataTableModels.WonderCubeData[]
}

const WonderCubeContents = ({ userAccount, datas, setDatas, opts }: ContentsProps ): ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const cubeTable = tables.wonderCubeDataTable;
    const assetTable = tables.assetDataTable;

    const onChangeData = useCallback((e, v: DataTableModels.WonderCubeData, index: number) => {
        if(!v) {
            if(datas[index]?.WonderCubeID == 0) return; 

            setDatas(prev => {
                if(prev[index]){
                    prev[index].WonderCubeID = 0;
                    prev[index].isChanged =  true;
                }
                return deepmerge([], prev);
            })
            return;
        }
        if(isNaN(Number(v?.ID))) return;

        setDatas(prev => {
            if(prev[index]){
                prev[index].WonderCubeID = Number(v.ID);
                prev[index].isChanged =  true;
            }
            return deepmerge([], prev);
        })
    }, [datas, setDatas])

    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        if(datas && 0 < datas.length) {
            for(let i = 0; i < datas.length; i++){
                const data = datas[i];

                let rowClass: string = commonUIStyles.row;
                if (data?.isChanged) rowClass = `${commonUIStyles.row} ${commonUIStyles.changed}`

                const cubeInfo = cubeTable.find(element => element.ID == data.WonderCubeID);
                let dataItem = opts.find(element => element.ID == data.WonderCubeID);
                const assetInfo1 = cubeInfo ? assetTable.find(element => element.ID == cubeInfo.AssetType1) : null;
                const assetInfo2 = cubeInfo ? assetTable.find(element => element.ID == cubeInfo.AssetType2) : null;
                list.push(
                    <BorderedTableRow key={i} className={rowClass}>
                        <TableCell>{data.SlotID}</TableCell>
                        <TableCell>
                            <FormControl>
                                { !userAccount.IsSignIn ?
                                    <Autocomplete options={opts} sx={{width: 300}}
                                    getOptionLabel={(option) => (option as DataTableModels.WonderCubeData)?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}
                                    value={dataItem} onChange={(e, v) => {onChangeData(e, v as DataTableModels.WonderCubeData, i)}}
                                    renderInput={(params) => <TextField {...params} label="큐브ID" />} />
                                :
                                <Typography>{dataItem?.NameStringWithID ? dataItem?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : ''}</Typography>
                                }
                            </FormControl>
                        </TableCell>
                        <TableCell>{`${assetInfo1?.NameStringWithID ? assetInfo1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : cubeInfo?.AssetType1 ? cubeInfo?.AssetType1.toString().replaceAll(/(?<=.{2})./g, '*') : ''}`}</TableCell>
                        <TableCell>{`${cubeInfo?.AssetCount1[0] ? cubeInfo?.AssetCount1[0].toString().replaceAll(/(?<=.{2})./g, '*') : ''} ~ ${cubeInfo?.AssetCount1[cubeInfo?.AssetCount1?.length-1] ? cubeInfo?.AssetCount1[cubeInfo?.AssetCount1?.length-1].toString().replaceAll(/(?<=.{2})./g, '*') : ''}`}</TableCell>
                        <TableCell>{`${assetInfo2?.NameStringWithID ? assetInfo2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*') : cubeInfo?.AssetType2 ? cubeInfo?.AssetType2.toString().replaceAll(/(?<=.{2})./g, '*') : ''}`}</TableCell>
                        <TableCell>{`${cubeInfo?.AssetCount2[0] ? cubeInfo?.AssetCount2[0].toString().replaceAll(/(?<=.{2})./g, '*') : ''} ~ ${cubeInfo?.AssetCount2[cubeInfo?.AssetCount2?.length-1] ? cubeInfo?.AssetCount2[cubeInfo?.AssetCount2?.length-1].toString().replaceAll(/(?<=.{2})./g, '*') : ''}`}</TableCell>
                        <TableCell>{dayjs(data?.CreateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                        <TableCell>{dayjs(data?.UpdateAt).tz().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                    </BorderedTableRow>
                )
            }
            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell>슬롯ID</TableCell>
                            <TableCell>큐브ID</TableCell>
                            <TableCell>보상 재화1</TableCell>
                            <TableCell>보상 범위1</TableCell>
                            <TableCell>보상 재화2</TableCell>
                            <TableCell>보상 범위2</TableCell>
                            <TableCell>생성시간({timezoneName})</TableCell>
                            <TableCell>수정시간({timezoneName})</TableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            )
        } else {
            result = (
                <TableBody>
                    <BorderedTableRow>
                        <TableCell className={commonUIStyles.noneCell}>검색된 트레저박스 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            )
        }
        return result;
    }, [assetTable, cubeTable, userAccount, datas, opts, onChangeData])

    return (
        <>
            {contents()}
        </>
    )

}

export default WonderCubeContents;