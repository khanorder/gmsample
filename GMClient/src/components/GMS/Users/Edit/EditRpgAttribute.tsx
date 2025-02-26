import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, MutableRefObject, useCallback, useRef, useState, useEffect  } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './EditListCountSelector';
import { DataTableModels } from '@ngel/data/tables/model';

const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const RpgAttributeContents = dynamic(() => import('./EditRpgAttributeContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface RpgAttributeProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas : PaginatedList<Models.RpgAttribute>;
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
    onChangePageListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const EditRpgAttribute = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged, onChangePageListCount }: RpgAttributeProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const tables = useAppSelector(state => state.tables);
    const attributeTable = tables.attributeDataTable;
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const [attributeOpts, setAttributeOpts] = useState<DataTableModels.AttributeData[]>([]);
    const [rpgAttributes, setRpgAttributes] = useState<PaginatedList<Models.RpgAttribute>>(new PaginatedList<Models.RpgAttribute>([]));
    const deleteDatas = useRef<Models.RpgAttribute[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        const rpgAttributeOptions: DataTableModels.AttributeData[] = [];

        const newData = new DataTableModels.AttributeData();
        newData.NameStringWithID = "특성을 선택해주세요(0)";
        rpgAttributeOptions.push(newData);

        for(const key in attributeTable){
            const item = new DataTableModels.AttributeData(attributeTable[key]);
            rpgAttributeOptions.push(item);
        }

        setAttributeOpts(rpgAttributeOptions);
    }, [attributeTable]);

    useEffect(() => {
        setRpgAttributes(datas)
    }, [datas]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!rpgAttributes) return;

        const changedDatas = rpgAttributes.totalItems.filter(_ => _.isChanged);
        if((0 < changedDatas.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteDatas.current = [];
        tabContentChanged.current = false;
    },[loadDatas, tabContentChanged, rpgAttributes]);

    const onHandleChangeListCount = useCallback(( event: SelectChangeEvent<unknown> ) => {
        if((0 < deleteDatas.current.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 변경 하시겠습니까?")) {
                return;
            }
        }
        onChangePageListCount(event);
        tabContentChanged.current = false;
    },[tabContentChanged, onChangePageListCount]);

    const onAdd = useCallback(() => {
        const newData = new Models.RpgAttribute();
        newData.UID = userAccount.UID;
        newData.Level = 1;
        newData.isChanged = true;
        newData.isNewData = true;
        setRpgAttributes(prev => {
            prev.totalItems.splice((prev.page-1)*prev.pageSize, 0, newData);
            return new PaginatedList<Models.RpgAttribute>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [userAccount, setRpgAttributes])

    const onDelete = useCallback((index: number) => {
        setRpgAttributes(prev => {
            if(!(prev.totalItems[(prev.page-1)*prev.pageSize+index].isNewData)) deleteDatas.current.push(prev.totalItems[(prev.page-1)*prev.pageSize+index]);
            prev.totalItems.splice((prev.page-1)*prev.pageSize+index, 1);
            return new PaginatedList<Models.RpgAttribute>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [setRpgAttributes]);

    const onSave = useCallback(async () => {
        const changedDatas = rpgAttributes.totalItems.filter(_ => _.isChanged && _.ID != 0);
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

            const response = await GameAPI.DeleteRpgAttributesAsync({ rpgAttributes : deleteDatas.current });
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
            const response = await GameAPI.SaveRpgAttributesAsync({rpgAttributes: changedDatas});
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
        
    }, [rpgAttributes, deleteDatas, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', mb: 1 }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onAdd}>추가</Button>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                        <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    </Box>
                    <Box sx={{ ml: 1 }}>
                        <ListCountSelector pageLogs={datas?.pageSize} handleChange={onHandleChangeListCount}/>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>특성</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <RpgAttributeContents userAccount={userAccount} datas={rpgAttributes ?? []} opts={attributeOpts} onDelete={onDelete} />
                    </Table>
                </TableContainer>
            </Box>
            <Paging datas={rpgAttributes} />
        </>
    );
}

export default EditRpgAttribute;