import { useAppSelector } from '@hooks/index';
import { ReactElement, useCallback, useEffect, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TextField, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { SelectChangeEvent } from '@mui/material/Select';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';
import c from 'config';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const FormGroup = dynamic(() => import('@mui/material/FormGroup'), { ssr: false });
const FormControlLabel = dynamic(() => import('@mui/material/FormControlLabel'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    userAccount: Models.UserAccount;
    datas: Models.SeasonPass[];
    setDatas: React.Dispatch<React.SetStateAction<Models.SeasonPass[]>>
    selectRewardGroup: number
}

const EditSeasonPassContents = ({ userAccount, datas, setDatas, selectRewardGroup}: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector( state => state.tables);
    const itemTable = tables.itemTable;
    const characterTable = tables.characterDataTable;
    const assetTable = tables.assetDataTable;
    const rewardTable = tables.rewardDataTable;
    const sePassDataTable = tables.seasonPassDataTable;
    const sePassLvTable = tables.seasonPassLevelDataTable;
    const sePassRewardTable = tables.seasonPassRewardDataTable;

    const onChangeLevelExp = useCallback((e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, isTypeLevel: boolean) => {
        if(!e.target.value || isNaN(Number(e.target.value))) return;
        if(Number(e.target.value) == 0) return;

        const levelInfos  = sePassLvTable.filter(element => element.SeasonPassID == selectRewardGroup).sort((a, b) => b.ID - a.ID);
        if(levelInfos && 0 < levelInfos.length){
            let curInfo :   DataTableModels.SeasonPassLevelData|null = null;

            for(let i = 0; i < levelInfos.length; i++){
                const info = levelInfos[i];
                if((isTypeLevel ? info.SeasonPassLevel : info.ReqExp)<= Number(e.target.value)){
                    curInfo = info;
                    break;
                }
            }

            if(curInfo == null) curInfo = levelInfos[0];

            setDatas(prev => {
                prev.map(element => {
                    if(element.SeasonPassID == selectRewardGroup){
                        if(isTypeLevel){
                            element.Level = Number(e.target.value) < levelInfos[0].SeasonPassLevel ? Number(e.target.value) : levelInfos[0].SeasonPassLevel;
                            element.Exp = curInfo?.ReqExp ?? 0;
                        }
                        else{
                            element.Level = curInfo?.SeasonPassLevel ?? 0;
                            element.Exp = Number(e.target.value) < levelInfos[0].ReqExp ? Number(e.target.value) : levelInfos[0].ReqExp;
                        }
                        element.isChanged = true;
                    }
                })
                return deepmerge([], prev);
            })
        }
    }, [sePassLvTable, setDatas, selectRewardGroup])

    const onChangePaidState = useCallback(() => {
        setDatas(prev => {
            prev.map(element => {
                if(element.SeasonPassID == selectRewardGroup){
                    element.IsPaid = element.IsPaid ? false : true;
                    element.isChanged = true;
                }
            })
            return deepmerge([], prev);
        })
    },[setDatas, selectRewardGroup]);

    const onChangeRewardState = useCallback((event: React.ChangeEvent<HTMLInputElement>, index: number) => {
        setDatas(prev => {
            prev.map(element => {
                if(element.SeasonPassID == selectRewardGroup){
                    const rewardState = element.RewardState.split("");
                    if(rewardState[index]){
                        rewardState[index] = rewardState[index] == '0' ? '1' : '0';
                    }
                    element.RewardState = rewardState.join('');

                    element.isChanged = true;
                }
            })
            return deepmerge([], prev);
        })
    }, [setDatas, selectRewardGroup])

    const contents = useCallback((): ReactElement =>  {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        const data = datas.find((element) => element.SeasonPassID === selectRewardGroup ?? 0);
        if(data){
            const rewardGroup = sePassRewardTable.filter(element => element.SeasonPassID === data?.SeasonPassID)
            const { basicRewardGroup, premiumRewardGroup } = rewardGroup.reduce(
                (result, element) => {
                  if (element.IsPaid) {
                    result.premiumRewardGroup.push(element);
                  } else {
                    result.basicRewardGroup.push(element);
                  }
                  return result;
                },
                { basicRewardGroup: [] as DataTableModels.SeasonPassRewardData[], premiumRewardGroup: [] as DataTableModels.SeasonPassRewardData[] }
              );
        let maxCount = rewardGroup[rewardGroup.length-1].SeasonPassLevel;

        
        const rewardState = data.RewardState.split("");

        for(let i = 0; i < maxCount; i++){
            const curState = parseInt(rewardState[i]) ?? 0;
            const hasBasicRewardInfo = basicRewardGroup.find((element)=> element?.SeasonPassLevel === i+1);
            const hasPreRewardInfo= premiumRewardGroup.find(element => element?.SeasonPassLevel === i+1);
            
            const basicRewardDataGroup = rewardTable.find(element => element?.ID === hasBasicRewardInfo?.RewardTableID);
            const basicRewardHero =  basicRewardDataGroup ? characterTable.find(element => element?.ID === basicRewardDataGroup?.CharacterID) : null;
            const basicCostumeItem1 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.CostumeID1) : null;
            const basicCostumeItem2 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.CostumeID2) : null;
            const [basicRewardItem1, basicRewardItem2, basicRewardItem3] = basicRewardDataGroup ? 
            [basicRewardDataGroup.ItemID1, basicRewardDataGroup.ItemID2, basicRewardDataGroup.ItemID3].map(id => itemTable.find(element => element?.ID === id)) : [null, null, null];
            const [basicAssetItem1, basicAssetItem2, basicAssetItem3] = basicRewardDataGroup ? 
            [basicRewardDataGroup.AssetID1, basicRewardDataGroup.AssetID2, basicRewardDataGroup.AssetID3].map(id => assetTable.find(element => element?.ID === id)) : [null, null, null];
            
            const preRewardDataGroup = rewardTable.find(element => element?.ID === hasPreRewardInfo?.RewardTableID);
            const preRewardHero = preRewardDataGroup ? characterTable.find(element => element?.ID === preRewardDataGroup?.CharacterID) : null;
            const preCostumeItem1 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.CostumeID1) : null;
            const preCostumeItem2 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.CostumeID2) : null;
            const [preRewardItem1, preRewardItem2, preRewardItem3] = preRewardDataGroup ? 
            [preRewardDataGroup?.ItemID1, preRewardDataGroup?.ItemID2, preRewardDataGroup?.ItemID3].map(id => itemTable.find(element => element?.ID === id)) : [null, null, null];
            const [preAssetItem1, preAssetItem2, preAssetItem3] = preRewardDataGroup ? 
            [preRewardDataGroup?.AssetID1, preRewardDataGroup?.AssetID2, preRewardDataGroup?.AssetID3].map(id => assetTable.find(element => element?.ID === id)) : [null, null, null];

            list.unshift(
                <BorderedTableRow key={i}>
                    <TableCell>{`${i+1} 레벨`}</TableCell>
                    <TableCell>{hasBasicRewardInfo ? <Checkbox disabled={data?.Level < i+1 || userAccount?.IsSignIn} checked={curState === 1} onChange={(event)=>{ onChangeRewardState(event, i)}}/> : ""}</TableCell>
                    <TableCell colSpan={2}>
                        {basicRewardHero ? <Typography>{`${basicRewardHero?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}`}</Typography> : ""}
                        {basicCostumeItem1 ? <Typography>{`${basicCostumeItem1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}`}</Typography> : ""}
                        {basicCostumeItem2 ? <Typography>{`${basicCostumeItem2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}`}</Typography> : ""}
                        {basicRewardItem1 ? <Typography>{`${basicRewardItem1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${basicRewardDataGroup?.ItemCount1}`}</Typography> : ""}
                        {basicRewardItem2 ? <Typography>{`${basicRewardItem2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${basicRewardDataGroup?.ItemCount2}`}</Typography> : ""}
                        {basicRewardItem3 ? <Typography>{`${basicRewardItem3?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${basicRewardDataGroup?.ItemCount3}`}</Typography> : ""}
                        {basicAssetItem1 ? <Typography>{`${basicAssetItem1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${basicRewardDataGroup?.AssetCount1}`}</Typography> : ""}
                        {basicAssetItem2 ? <Typography>{`${basicAssetItem2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${basicRewardDataGroup?.AssetCount2}`}</Typography> : ""}
                        {basicAssetItem3 ? <Typography>{`${basicAssetItem3?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${basicRewardDataGroup?.AssetCount3}`}</Typography> : ""}
                    </TableCell>

                    <TableCell>{hasPreRewardInfo ? <Checkbox disabled={data?.Level < i+1 || userAccount?.IsSignIn ||!data?.IsPaid} checked={curState === 1} onChange={(event)=>{ onChangeRewardState(event, i)}}/> : ""}</TableCell>
                    <TableCell colSpan={2}>
                        {preRewardHero ? <Typography>{`${preRewardHero?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}`}</Typography> : ""}
                        {preCostumeItem1 ? <Typography>{`${preCostumeItem1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}`}</Typography> : ""}
                        {preCostumeItem2 ? <Typography>{`${preCostumeItem2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')}`}</Typography> : ""}
                        {preRewardItem1 ? <Typography>{`${preRewardItem1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${preRewardDataGroup?.ItemCount1}개`}</Typography> : ""}
                        {preRewardItem2 ? <Typography>{`${preRewardItem2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${preRewardDataGroup?.ItemCount2}개`}</Typography> : ""}
                        {preRewardItem3 ? <Typography>{`${preRewardItem3?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${preRewardDataGroup?.ItemCount3}개`}</Typography> : ""}
                        {preAssetItem1 ? <Typography>{`${preAssetItem1?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${preRewardDataGroup?.AssetCount1}`}</Typography> : ""}
                        {preAssetItem2 ? <Typography>{`${preAssetItem2?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${preRewardDataGroup?.AssetCount2}`}</Typography> : ""}
                        {preAssetItem3 ? <Typography>{`${preAssetItem3?.NameStringWithID.toString().replaceAll(/(?<=.{2})./g, '*')} ${preRewardDataGroup?.AssetCount3}`}</Typography> : ""}
                    </TableCell>
                </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <TableCell colSpan={5} style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px"}}></TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <MinWidthTableCell colSpan={2}>시즌</MinWidthTableCell>
                            <MinWidthTableCell>레벨</MinWidthTableCell>
                            <MinWidthTableCell>경험치</MinWidthTableCell>
                            <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell style={{backgroundColor: 'transparent'}} colSpan={2}>
                                <Typography> {data?.SeasonNum} </Typography>
                            </TableCell>
                            <TableCell style={{backgroundColor: 'transparent'}}>
                                <TextField disabled={userAccount?.IsSignIn} value={data?.Level} onChange={(e) => { onChangeLevelExp(e, true)}}/>
                            </TableCell>
                            <TableCell style={{backgroundColor: 'transparent'}}>
                                <TextField disabled={userAccount?.IsSignIn} value={data?.Exp} onChange={(e) => { onChangeLevelExp(e, false)}}/> 
                            </TableCell>
                            <TableCell style={{backgroundColor: 'transparent'}}>
                                <Typography> {data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""} </Typography>
                            </TableCell>
                            <TableCell style={{backgroundColor: 'transparent'}}>
                                <Typography> {data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""} </Typography>
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell></TableCell>
                            <TableCell colSpan={3}>일반 보상</TableCell>
                            <TableCell colSpan={3}>{`시즌패스 보상 구매여부`}<Checkbox disabled={userAccount?.IsSignIn} checked={data?.IsPaid} onChange={onChangePaidState}/></TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <MinWidthTableCell>레벨</MinWidthTableCell>
                            <MinWidthTableCell>보상수령 상태</MinWidthTableCell>
                            <MinWidthTableCell colSpan={2}>보상</MinWidthTableCell>
                            <MinWidthTableCell>보상수령 상태</MinWidthTableCell>
                            <MinWidthTableCell colSpan={2}>보상</MinWidthTableCell>
                        </BorderedTableRow>
                    </TableHead>
                    <TableBody>
                        {list}
                    </TableBody>
                </>
            );
        }
        else {
            result = (<>
                <TableHead style={{position: "sticky"}}>
                    <BorderedTableRow>
                        <TableCell colSpan={5} style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px"}}></TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={6} className={commonUIStyles.noneCell}>검색된 시즌패스 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
                </>
            );
        
        
    }
        return result;
    }, [characterTable, itemTable, assetTable, rewardTable, sePassRewardTable, 
        userAccount?.IsSignIn, datas, selectRewardGroup, onChangeLevelExp, onChangePaidState, onChangeRewardState]);


    if (datas && 0 < datas.length) {
        content = (<>
            {contents()}
        </>)
    } else {
        content = (
            <>
                <TableHead style={{position: "sticky"}}>
                        <BorderedTableRow>
                            <TableCell style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px"}}></TableCell>
                        </BorderedTableRow>
                </TableHead>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={3} className={commonUIStyles.noneCell}>검색된 시즌패스 정보가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>
        );
    }

    return content;
}

export default EditSeasonPassContents;