import { useAppSelector } from '@hooks/index';
import { ReactElement, useCallback, useEffect, useState } from 'react';
import { PaginatedList } from '@helpers/paging';
import { Models } from '@ngel/data/models';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import commonUIStyles from '@styles/ui/common.module.sass';
import { styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { Defines } from '@ngel/data/autoDefines';
import { SelectChangeEvent } from '@mui/material/Select';
import { DataTableModels } from '@ngel/data/tables/model';

const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Checkbox = dynamic(() => import('@mui/material/Checkbox'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Chip = dynamic(() => import('@mui/material/Chip'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const MinWidthTableCell = styled(TableCell)(({ theme }) => ({
    minWidth: "120px"
}));

interface ContentsProps {
    datas: Models.SeasonPass[];
    selectRewardGroup: number
}

interface PassGroup {
    ID: number[];
    SeasonNum: number;
    NameString: string;
}

const SeasonPassContents = ({ datas, selectRewardGroup }: ContentsProps): ReactElement => {
    let content: ReactElement = <></>;
    const tables = useAppSelector(state => state.tables);
    const itemTable = tables.itemTable;
    const characterTable = tables.characterDataTable;
    const assetTable = tables.assetDataTable;
    const rewardTable = tables.rewardDataTable;
    const sePassDataTable = tables.seasonPassDataTable;
    const sePassLvTable = tables.seasonPassLevelDataTable;
    const sePassRewardTable = tables.seasonPassRewardDataTable;


    const contents = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];
        const index = selectRewardGroup ?? 0;

        const data = datas.find((element) => element.SeasonNum === index);
        if (data) {
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
            let maxCount = rewardGroup[rewardGroup.length - 1].SeasonPassLevel;


            const rewardState = data.RewardState.split("");

            for (let i = 0; i < maxCount; i++) {
                const curState = parseInt(rewardState[i]) ?? 0;
                const hasBasicRewardInfo = basicRewardGroup.find((element) => element?.SeasonPassLevel === i + 1);
                const hasPreRewardInfo = premiumRewardGroup.find(element => element?.SeasonPassLevel === i + 1);

                const basicRewardDataGroup = rewardTable.find(element => element?.ID === hasBasicRewardInfo?.RewardTableID);
                const basicRewardHero = basicRewardDataGroup ? characterTable.find(element => element?.ID === basicRewardDataGroup?.CharacterID) : null;
                const basicCostumeItem1 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.CostumeID1) : null;
                const basicCostumeItem2 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.CostumeID2) : null;
                const basicRewardItem1 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.ItemID1) : null;
                const basicRewardItem2 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.ItemID2) : null;
                const basicRewardItem3 = basicRewardDataGroup ? itemTable.find(element => element?.ID === basicRewardDataGroup?.ItemID3) : null;
                const basicAssetItem1 = basicRewardDataGroup ? assetTable.find(element => element?.ID === basicRewardDataGroup?.AssetID1) : null;
                const basicAssetItem2 = basicRewardDataGroup ? assetTable.find(element => element?.ID === basicRewardDataGroup?.AssetID2) : null;
                const basicAssetItem3 = basicRewardDataGroup ? assetTable.find(element => element?.ID === basicRewardDataGroup?.AssetID3) : null;

                const preRewardDataGroup = rewardTable.find(element => element?.ID === hasPreRewardInfo?.RewardTableID);
                const preRewardHero = preRewardDataGroup ? characterTable.find(element => element?.ID === preRewardDataGroup?.CharacterID) : null;
                const preCostumeItem1 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.CostumeID1) : null;
                const preCostumeItem2 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.CostumeID2) : null;
                const preRewardItem1 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.ItemID1) : null;
                const preRewardItem2 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.ItemID2) : null;
                const preRewardItem3 = preRewardDataGroup ? itemTable.find(element => element?.ID === preRewardDataGroup?.ItemID3) : null;
                const preAssetItem1 = preRewardDataGroup ? assetTable.find(element => element?.ID === preRewardDataGroup?.AssetID1) : null;
                const preAssetItem2 = preRewardDataGroup ? assetTable.find(element => element?.ID === preRewardDataGroup?.AssetID2) : null;
                const preAssetItem3 = preRewardDataGroup ? assetTable.find(element => element?.ID === preRewardDataGroup?.AssetID3) : null;

                list.unshift(
                    <BorderedTableRow key={i}>
                        <TableCell>{`${i + 1} 레벨`}</TableCell>
                        <TableCell>{hasBasicRewardInfo ? <Checkbox checked={curState === 1} onChange={(event) => { event?.preventDefault() }} /> : ""}</TableCell>
                        <TableCell colSpan={2}>
                            {basicRewardHero ? <Typography>{`${basicRewardHero?.NameStringWithID}`.toString().replaceAll(/(?<=.{1})./g, '*')}</Typography> : ""}
                            {basicCostumeItem1 ? <Typography>{`${basicCostumeItem1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')}`}</Typography> : ""}
                            {basicCostumeItem2 ? <Typography>{`${basicCostumeItem2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')}`}</Typography> : ""}
                            {basicRewardItem1 ? <Typography>{`${basicRewardItem1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${basicRewardDataGroup?.ItemCount1}`}</Typography> : ""}
                            {basicRewardItem2 ? <Typography>{`${basicRewardItem2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${basicRewardDataGroup?.ItemCount2}`}</Typography> : ""}
                            {basicRewardItem3 ? <Typography>{`${basicRewardItem3?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${basicRewardDataGroup?.ItemCount3}`}</Typography> : ""}
                            {basicAssetItem1 ? <Typography>{`${basicAssetItem1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${basicRewardDataGroup?.AssetCount1}`}</Typography> : ""}
                            {basicAssetItem2 ? <Typography>{`${basicAssetItem2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${basicRewardDataGroup?.AssetCount2}`}</Typography> : ""}
                            {basicAssetItem3 ? <Typography>{`${basicAssetItem3?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${basicRewardDataGroup?.AssetCount3}`}</Typography> : ""}
                        </TableCell>

                        <TableCell>{hasPreRewardInfo ? <Checkbox checked={curState === 1} onChange={(event) => { event?.preventDefault() }} /> : ""}</TableCell>
                        <TableCell colSpan={2}>
                            {preRewardHero ? <Typography>{`${preRewardHero?.NameStringWithID}`.toString().replaceAll(/(?<=.{1})./g, '*')}</Typography> : ""}
                            {preCostumeItem1 ? <Typography>{`${preCostumeItem1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')}`}</Typography> : ""}
                            {preCostumeItem2 ? <Typography>{`${preCostumeItem2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')}`}</Typography> : ""}
                            {preRewardItem1 ? <Typography>{`${preRewardItem1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${preRewardDataGroup?.ItemCount1}개`}</Typography> : ""}
                            {preRewardItem2 ? <Typography>{`${preRewardItem2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${preRewardDataGroup?.ItemCount2}개`}</Typography> : ""}
                            {preRewardItem3 ? <Typography>{`${preRewardItem3?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${preRewardDataGroup?.ItemCount3}개`}</Typography> : ""}
                            {preAssetItem1 ? <Typography>{`${preAssetItem1?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${preRewardDataGroup?.AssetCount1}`}</Typography> : ""}
                            {preAssetItem2 ? <Typography>{`${preAssetItem2?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${preRewardDataGroup?.AssetCount2}`}</Typography> : ""}
                            {preAssetItem3 ? <Typography>{`${preAssetItem3?.NameStringWithID.toString().replaceAll(/(?<=.{1})./g, '*')} ${preRewardDataGroup?.AssetCount3}`}</Typography> : ""}
                        </TableCell>
                    </BorderedTableRow>
                )
            }

            result = (
                <>
                    <TableHead>
                        <BorderedTableRow>
                            <MinWidthTableCell colSpan={2}>시즌</MinWidthTableCell>
                            <MinWidthTableCell>레벨</MinWidthTableCell>
                            <MinWidthTableCell>경험치</MinWidthTableCell>
                            <MinWidthTableCell>생성일시({timezoneName})</MinWidthTableCell>
                            <MinWidthTableCell>수정일시({timezoneName})</MinWidthTableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell style={{ backgroundColor: 'transparent' }} colSpan={2}>
                                <Typography> {data?.SeasonNum} </Typography>
                            </TableCell>
                            <TableCell style={{ backgroundColor: 'transparent' }}>
                                <Typography> {data?.Level} </Typography>
                            </TableCell>
                            <TableCell style={{ backgroundColor: 'transparent' }}>
                                <Typography> {data?.Exp} </Typography>
                            </TableCell>
                            <TableCell style={{ backgroundColor: 'transparent' }}>
                                <Typography> {data?.CreateAt ? dayjs(data?.CreateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""} </Typography>
                            </TableCell>
                            <TableCell style={{ backgroundColor: 'transparent' }}>
                                <Typography> {data?.UpdateAt ? dayjs(data?.UpdateAt).tz().format('YYYY-MM-DD HH:mm:ss') : ""} </Typography>
                            </TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <TableCell></TableCell>
                            <TableCell colSpan={3}>일반 보상</TableCell>
                            <TableCell colSpan={3}><Grid sx={{ display: 'flex', justifyContent: 'center' }}>{`시즌패스 보상`} <Chip sx={{ marginLeft: '10px' }} color={data.IsPaid ? 'primary' : 'default'} size='small' label={data.IsPaid ? '구매' : '미구매'} /></Grid></TableCell>
                        </BorderedTableRow>
                        <BorderedTableRow>
                            <MinWidthTableCell>레벨</MinWidthTableCell>
                            <MinWidthTableCell>보상** **</MinWidthTableCell>
                            <MinWidthTableCell colSpan={2}>보상</MinWidthTableCell>
                            <MinWidthTableCell>보상** **</MinWidthTableCell>
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
                <TableHead style={{ position: "sticky" }}>
                    <BorderedTableRow>
                        <TableCell colSpan={5} style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px" }}></TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={6} className={commonUIStyles.noneCell}>검색된 시즌** **가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>
            );


        }
        return result;
    }, [characterTable, itemTable, assetTable, rewardTable, sePassRewardTable, datas, selectRewardGroup]);


    if (datas && 0 < datas.length) {
        content = (<>
            {contents()}
        </>)
    } else {
        content = (
            <>
                <TableHead style={{ position: "sticky" }}>
                    <BorderedTableRow>
                        <TableCell style={{ border: "0px", borderBottom: "0px", backgroundColor: 'transparent', height: "50px" }}></TableCell>
                    </BorderedTableRow>
                </TableHead>
                <TableBody>
                    <BorderedTableRow>
                        <TableCell colSpan={3} className={commonUIStyles.noneCell}>검색된 시즌** **가 없습니다.</TableCell>
                    </BorderedTableRow>
                </TableBody>
            </>
        );
    }

    return content;
}

export default SeasonPassContents;