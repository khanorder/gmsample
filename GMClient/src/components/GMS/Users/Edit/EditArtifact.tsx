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
const EditArtifactContents = dynamic(() => import('./EditArtifactContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface ArtifactProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas : PaginatedList<Models.Artifact>;
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
    onChangePageListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const EditArtifact = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged, onChangePageListCount }: ArtifactProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector(state => state.tables)
    const artifactTable  = tables.artifactDataTable;
    
    const [artifactOpts, setArtifactOpts] = useState<DataTableModels.ArtifactData[]>([]);
    const [artifacts, setArtifacts] = useState<PaginatedList<Models.Artifact>>(new PaginatedList<Models.Artifact>([]));
    const deleteArtifacts = useRef<Models.Artifact[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setArtifacts(datas)
    }, [datas]);

    useEffect(() => {
            const artifactOptions:DataTableModels.ArtifactData[] = [];

            const newData = new DataTableModels.ArtifactData();
            newData.NameStringWithID = "유물을 선택해주세요(0)";
            artifactOptions.push(newData);

            for(const key in artifactTable){
                const item = new DataTableModels.ArtifactData(artifactTable[key]);
                artifactOptions.push(item);
            }

            setArtifactOpts(artifactOptions);
        }, [artifactTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!artifacts) return;

        const changedDatas = artifacts.totalItems.filter(_ => _.isChanged);
        if((0 < changedDatas.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteArtifacts.current = [];
        tabContentChanged.current = false;
    },[loadDatas, tabContentChanged, artifacts]);

    const onHandleChangeListCount = useCallback(( event: SelectChangeEvent<unknown> ) => {
        if((0 < deleteArtifacts.current.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 변경 하시겠습니까?")) {
                return;
            }
        }
        onChangePageListCount(event);
        tabContentChanged.current = false;
    },[tabContentChanged, onChangePageListCount]);

    const onAdd = useCallback(() => {
        const newData = new Models.Artifact();
        newData.UID = userAccount.UID;
        newData.isChanged = true;
        newData.isNewData = true;
        setArtifacts(prev => {
            prev.totalItems.splice((prev.page-1)*prev.pageSize, 0, newData);
            return new PaginatedList<Models.Artifact>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [userAccount, setArtifacts])

    const onDelete = useCallback((index: number) => {
        setArtifacts(prev => {
            if(!(prev.totalItems[(prev.page-1)*prev.pageSize+index].isNewData)) deleteArtifacts.current.push(prev.totalItems[(prev.page-1)*prev.pageSize+index]);
            prev.totalItems.splice((prev.page-1)*prev.pageSize+index, 1);
            return new PaginatedList<Models.Artifact>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [deleteArtifacts, setArtifacts]);

    const onSave = useCallback(async () => {
        const changedDatas = artifacts.totalItems.filter(_ => _.isChanged && _.ArtifactID != 0);
        if ((!changedDatas || 1 > changedDatas.length && deleteArtifacts.current.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        if( deleteArtifacts.current && 0 < deleteArtifacts.current.length){
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
            
            const response = await GameAPI.DeleteArtifactAsync({ artifacts : deleteArtifacts.current });
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
            const response = await GameAPI.SaveArtifactAsync({artifacts: changedDatas});
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
        deleteArtifacts.current = [];

        await loadDatas();
        
    }, [artifacts, deleteArtifacts, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', mb: 1 }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onAdd}>추가</Button>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                        <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    </Box>
                    <Box marginLeft={1}>
                        <ListCountSelector pageLogs={datas.pageSize} handleChange={onHandleChangeListCount}/>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>유물</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <EditArtifactContents userAccount={userAccount} datas={artifacts ?? []} opts={artifactOpts} onDelete={onDelete}/>
                    </Table>
                </TableContainer>
            </Box>
            <Paging datas={artifacts} />
        </>
    );
}

export default EditArtifact;