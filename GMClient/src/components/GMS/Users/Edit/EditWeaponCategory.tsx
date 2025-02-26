import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, MutableRefObject, useCallback, useState, useRef, useEffect } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { DataTableModels } from '@ngel/data/tables/model';
import { Errors } from '@ngel/data/autoErrors';
import deepmerge from 'deepmerge';
const WeaponCategoryContents = dynamic(() => import('./EditWeaponCategoryContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface WeaponCategoryProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.WeaponCategory[];
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
}

const EditWeaponCategory = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged }: WeaponCategoryProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector(state => state.tables)
    const weaponCategoriesTable  = tables.weaponCategoryDataTable;

    const [weaponCategoriesOpts, setWeaponCategoriesOpts] = useState<DataTableModels.WeaponCategoryData[]>([]);
    const [weaponCategories, setWeaponCategories] = useState<Models.WeaponCategory[]>([]);
    const deleteDatas = useRef<Models.WeaponCategory[]>([]);
    
    const isSigned = userAccount?.IsSignIn ?? true;
    
    useEffect(() => {
        setWeaponCategories(datas)
    }, [datas])

    useEffect(() => {
        const weaponCategoryOptions:DataTableModels.WeaponCategoryData[] = [];

        const newData = new DataTableModels.WeaponCategoryData();
        newData.NameStringWithID = "무기 항목을 선택해주세요(0)";
        weaponCategoryOptions.push(newData);

        for(const key in weaponCategoriesTable){
            const item = new DataTableModels.WeaponCategoryData(weaponCategoriesTable[key]);
            weaponCategoryOptions.push(item);
        }

        setWeaponCategoriesOpts(weaponCategoryOptions);
    }, [weaponCategoriesTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount?.UID);
    }, [tabIndex, userAccount, searchDetail])

    const onReload = useCallback(async () => {
        if(!weaponCategories) return;

        const changedDatas = weaponCategories.filter(_ => _.isChanged);
        if((0 < changedDatas.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteDatas.current = [];
        tabContentChanged.current = false;
    },[loadDatas, tabContentChanged, weaponCategories, deleteDatas]);
    
    const onAdd = useCallback(() => {
        const newData = new Models.WeaponCategory();
        newData.UID = userAccount.UID;
        newData.isChanged = true;
        newData.isNewData = true;
        setWeaponCategories(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        })
    }, [userAccount, setWeaponCategories])

    const onDelete = useCallback((index: number) => {
        setWeaponCategories(prev => {
            if(!(prev[index].isNewData)) deleteDatas.current.push(prev[index]);
            prev.splice(index, 1);
            return deepmerge([], prev);
        })
    }, [deleteDatas, setWeaponCategories]);

    const onSave = useCallback(async() => {
        const changedDatas = weaponCategories.filter(_ => _.isChanged && _.WeaponCategoryID != 0);

        if ((!changedDatas || 1 > changedDatas.length && deleteDatas.current.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        if( deleteDatas.current && 0 < deleteDatas.current.length){
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
            
            const response = await GameAPI.DeleteWeaponCategoriesAsync({ weaponCategories : deleteDatas.current });
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
            const response = await GameAPI.SaveWeaponCategoriesAsync({ weaponCategories: changedDatas});
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
        deleteDatas.current = [];

        await loadDatas();

    }, [weaponCategories, deleteDatas, loadDatas]);
    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', mb: 1 }}>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onAdd}>추가</Button>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                    <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>무기</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <WeaponCategoryContents userAccount={userAccount} opts={weaponCategoriesOpts} weaponCategories={weaponCategories} setWeaponCategories={setWeaponCategories} onDelete={onDelete}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditWeaponCategory;