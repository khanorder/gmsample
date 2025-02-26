import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, MutableRefObject, useCallback, useRef, useState, useEffect  } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import ListCountSelector from './EditListCountSelector';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';

const EntitlementContents = dynamic(() => import('./EditEntitlementContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface EntitlementProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.Entitlement[];
    searchDetail: (index: number, accountID: number) => void;
}

const EditEntitlement = ({ userAccount, tabIndex, datas, searchDetail }: EntitlementProps): ReactElement => {
    const tables = useAppSelector(state => state.tables)
    const entitleTable  = tables.entitlementDataTable;
    
    const [entitlementOpts, setEntitlementOpts] = useState<DataTableModels.EntitlementData[]>([]);
    const [entitlements, setEntitlements] = useState<Models.Entitlement[]>([]);
    const deleteEntitles = useRef<Models.Entitlement[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setEntitlements(datas)
    }, [datas]);

    useEffect(() => {
        const entitleOptions:DataTableModels.EntitlementData[] = [];

        const newData = new DataTableModels.EntitlementData();
        newData.ID = '0';
        newData.NameStringWithID = "칭호을 선택해주세요(0)";
        entitleOptions.push(newData);

        for(const key in entitleTable){
            const item = new DataTableModels.EntitlementData(entitleTable[key]);
            entitleOptions.push(item);
        }

        setEntitlementOpts(entitleOptions);
    }, [entitleTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!entitlements) return;

        const newDatas = entitlements.filter(_ => _.isNewData);
        if((0 < newDatas.length || 0 < deleteEntitles.current.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteEntitles.current = [];
    },[loadDatas, entitlements]);

    const onAdd = useCallback(() => {
        const newData = new Models.Entitlement();
        newData.UID = userAccount.UID;
        newData.isNewData = true;
        setEntitlements(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        })
    }, [userAccount, setEntitlements])

    const onDelete = useCallback((index: number) => {
        setEntitlements(prev => {
            if(!(prev[index].isNewData)) deleteEntitles.current.push(prev[index]);
            prev.splice(index, 1);
            return deepmerge([], prev);
        })
    }, [deleteEntitles, setEntitlements]);

    const onSave = useCallback(async() => {
        const changedDatas = entitlements.filter(_ => _.isNewData && _.EntitlementID != 0);
        
        if ((!changedDatas || 1 > changedDatas.length && deleteEntitles.current.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        if( deleteEntitles.current && 0 < deleteEntitles.current.length){
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
            
            const response = await GameAPI.DeleteEntitlementsAsync({ entitlements : deleteEntitles.current });
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        if( 0 < changedDatas.length){
            const response = await GameAPI.SaveEntitlementsAsync({ entitlements: changedDatas});
            if (!response) {
                alert(`오류!`);
                return;
            }
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        alert("저장 했습니다.");
        deleteEntitles.current = [];

        await loadDatas();

    }, [entitlements, deleteEntitles, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ padding: '10px 0 20px', display: 'flex' }}>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onAdd}>추가</Button>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                    <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>칭호</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <EntitlementContents userAccount={userAccount} opts={entitlementOpts} datas={entitlements} setEntitlements={setEntitlements} onDelete={onDelete} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditEntitlement;