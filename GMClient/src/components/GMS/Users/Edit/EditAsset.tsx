import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import dayjs, { Dayjs } from 'dayjs';
import timezone from 'dayjs/plugin/timezone.js';
import utc from 'dayjs/plugin/utc.js';
// dayjs.extend(timezone);
// dayjs.extend(utc);
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { MutableRefObject, ReactElement, useCallback, useState, useEffect, ChangeEvent } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import EditAssetContents from './EditAssetContents';
import deepmerge from 'deepmerge';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableBody = dynamic(() => import('@mui/material/TableBody'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const TableHead = dynamic(() => import('@mui/material/TableHead'), { ssr: false });
const BorderedTableRow = dynamic(() => import('@components/ui/BorderedTableRow'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });

export interface AssetProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    searchDetail: (index: number, accountID: number) => void;
    assets: Models.Asset[];
    tabContentChanged: MutableRefObject<boolean>;
}

const EditAsset = ({ userAccount, tabIndex, assets, searchDetail, tabContentChanged }: AssetProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const table = useAppSelector(state => state.tables);
    const assetTable = table.assetDataTable;

    const [customDatas, setCustomDatas] = useState<Models.Asset[]>([]);

    const isSigned = userAccount.IsSignIn;
    
    useEffect(()=> {
        const customs:Models.Asset[] = [];
        
        for(let i = 0; i < assetTable.length; i++){
            const data = assets.find((element)=> assetTable[i]?.ItemID == element?.AssetID);
            if(data){
                const custom = new Models.Asset(data);
                custom.isChanged = false;
                custom.isNewData = false;
                customs.push(custom);
            }
            else{
                const custom = new Models.Asset();
                custom.UID = userAccount.UID;
                custom.AssetID = assetTable[i].ItemID;
                custom.isChanged = true;
                custom.isNewData = true;
                customs.push(custom);
            }
        }
        setCustomDatas(customs);
    }, [userAccount, assetTable, assets])

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);

        tabContentChanged.current = false;
    }, [userAccount, tabIndex, searchDetail, tabContentChanged]);

    const onReload = useCallback(async () => {
        const changedDatas : Models.Asset[] = customDatas.filter(_ => _.isChanged);

        if(changedDatas && 0 < changedDatas?.length ){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
    },[customDatas, loadDatas]);


    const onSave = useCallback(async() => {
        const changedDatas = customDatas.filter(_ => _.isChanged);
        if (!changedDatas || 1 > changedDatas.length) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if (changedDatas && 0 < changedDatas.length) {
            const response = await GameAPI.SaveAssetsAsync({ assets : changedDatas });
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert(`저장 했습니다.`);
        await loadDatas();

    }, [customDatas, loadDatas]);

    const onChangeDatas = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, maxValue: number, index: number) => {
        const cleanedValue = Number(e.target.value.replace(/,/g, ''));
        if((!cleanedValue && !(cleanedValue == 0)) || cleanedValue > maxValue) return;
        if(cleanedValue < 0) return;

        setCustomDatas(prev => {
            if(prev[index]){
                prev[index].Count = cleanedValue;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev)
        })

        tabContentChanged.current = true;

    },[tabContentChanged]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Grid sx={{ mb: 1 }}>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                    <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                </Grid>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>재화</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <EditAssetContents userAccount={userAccount} datas={customDatas ?? []} onChangeDatas={onChangeDatas}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditAsset;