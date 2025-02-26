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
const EditAchievementContents = dynamic(() => import('./EditAchievementContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface AchievementProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas : PaginatedList<Models.Achievement>;
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
    onChangePageListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const EditAchievement = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged, onChangePageListCount }: AchievementProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector(state => state.tables)
    const achievementTable  = tables.achievementDataTable;
    
    const [achievementOpts, setAchievementOpts] = useState<DataTableModels.AchievementData[]>([]);
    const [achievements, setAchievements] = useState<PaginatedList<Models.Achievement>>(new PaginatedList<Models.Achievement>([]));
    const deleteAchievements = useRef<Models.Achievement[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setAchievements(datas)
    }, [datas]);

    useEffect(() => {
            const achievementOptions:DataTableModels.AchievementData[] = [];

            const newData = new DataTableModels.AchievementData();
            newData.NameStringWithID = "업적을 선택해주세요(0)";
            achievementOptions.push(newData);

            for(const key in achievementTable){
                const item = new DataTableModels.AchievementData(achievementTable[key]);
                achievementOptions.push(item);
            }

            setAchievementOpts(achievementOptions);
        }, [achievementTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!achievements) return;

        const changedDatas = achievements.totalItems.filter(_ => _.isChanged);
        if((0 < changedDatas.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteAchievements.current = [];
        tabContentChanged.current = false;
    },[loadDatas, tabContentChanged, achievements]);

    const onHandleChangeListCount = useCallback(( event: SelectChangeEvent<unknown> ) => {
        onChangePageListCount(event);
        tabContentChanged.current = false;
    },[tabContentChanged, onChangePageListCount]);

    const onAdd = useCallback(() => {
        const newData = new Models.Achievement();
        newData.UID = userAccount.UID;
        newData.Count = 1
        newData.isChanged = true;
        newData.isNewData = true;
        setAchievements(prev => {
            prev.totalItems.splice((prev.page-1)*prev.pageSize, 0, newData);
            return new PaginatedList<Models.Achievement>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [userAccount, setAchievements])

    const onDelete = useCallback((index: number) => {
        setAchievements(prev => {
            if(!(prev.totalItems[(prev.page-1)*prev.pageSize+index].isNewData)) deleteAchievements.current.push(prev.totalItems[(prev.page-1)*prev.pageSize+index]);
            prev.totalItems.splice((prev.page-1)*prev.pageSize+index, 1);
            return new PaginatedList<Models.Achievement>(prev.totalItems, prev.page, prev.parameters, prev.pageSize, prev.pageBlockSize);
        })
    }, [deleteAchievements, setAchievements]);

    const onSave = useCallback(async () => {
        const changedDatas = achievements.totalItems.filter(_ => _.isChanged && _.AchievementID != 0);
        if ((!changedDatas || 1 > changedDatas.length && deleteAchievements.current.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        if( deleteAchievements.current && 0 < deleteAchievements.current.length){
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
            
            const response = await GameAPI.DeleteAchievementsAsync({ achievements : deleteAchievements.current });
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
            const response = await GameAPI.SaveAchievementsAsync({ achievements: changedDatas});
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
        deleteAchievements.current = [];

        await loadDatas();
        
    }, [achievements, deleteAchievements, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', mb: 1 }}>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onAdd}>추가</Button>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                    <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    <Box marginLeft={1}>
                        <ListCountSelector pageLogs={datas.pageSize} handleChange={onHandleChangeListCount}/>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>업적</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <EditAchievementContents userAccount={userAccount} datas={achievements ?? []} opts={achievementOpts} onDelete={onDelete}/>
                    </Table>
                </TableContainer>
            </Box>
            <Paging datas={achievements} />
        </>
    );
}

export default EditAchievement;