import { Models } from '@ngel/data/models';
import { ChangeEvent, ReactElement, useCallback, useState, useEffect } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import { TableContainer, SelectChangeEvent, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngeldata/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { Defines } from '@ngel/data/autoDefines';
import { dayjs, Dayjs } from '@helpers/localizedDayjs';
import deepmerge from 'deepmerge';
import commonUIStyles from '@styles/ui/common.module.sass';
import { EPenaltyType } from '@ngel/data/models/lobby';

const UserBlockContents = dynamic(() => import('./UserBlockContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const Grid = dynamic(() => import('@mui/material/Grid'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });

export interface UserBlockProps {
    userAccount: Models.Account;
    datas: Models.UserBlock[]|null;
    onSearch: () => void;
}

const PenaltyTypes = Object.keys(EPenaltyType).filter(key => isNaN(Number(key)));
const penaltyTranslate = (penaltyTypeum : number) => {
    return PenaltyTypes[penaltyTypeum];
}

const UserBlock = ({ userAccount, datas, onSearch }: UserBlockProps): ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const penaltyDataTable = tables.penaltyDataTable;
    
    const [userBlocks, setUserBlocks] = useState<Models.UserBlock[]>([]);
    const [deleteUserBlocks, setDeleteUserBlocks] = useState<Models.UserBlock[]>([]);

    useEffect(() => {
        if(datas && 0 < datas?.length){
            setUserBlocks(datas);
        }
    }, [datas])

    const onReload = useCallback(() => {
        if(userBlocks === null || userBlocks === undefined ) return;
    
        const changed = userBlocks.filter(_ => _.isChanged);
        if ((changed && changed.length > 0) || (deleteUserBlocks && deleteUserBlocks.length > 0)) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }
    
        onSearch();
        setDeleteUserBlocks([]);

    }, [onSearch, userBlocks, deleteUserBlocks]);

    const onSaveData = useCallback(async () => {
        if(userBlocks === null || userBlocks === undefined ) return;
    
        const changed = userBlocks.filter(_ => _.isChanged);
        if ((!changed || 1 > changed.length) && (!deleteUserBlocks || 1 > deleteUserBlocks?.length)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
    
        if ((deleteUserBlocks && 0 < deleteUserBlocks?.length)) {
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        else{
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
    
        if ((deleteUserBlocks && 0 < deleteUserBlocks?.length)) {
            const deleteResponse = await GameAPI.DeleteUserBlockAsync({ userBlocks: deleteUserBlocks });
            if (!deleteResponse) {
                alert(`삭제 오류!`);
                return;
            }
    
            if (!deleteResponse.result) {
                alert(`삭제 오류! (error: ${Errors[deleteResponse.error]}, index: ${deleteResponse.errorIndex})`);
                return;
            }
        }
    
        if(changed && 0 < changed?.length ){
            for(let i = 0; i< userBlocks.length; i++){
                const block = userBlocks[i];
    
                if(!block.isChanged)
                    continue;
    
                if(!block.StartTime){
                    alert(`${i + 1}번째 데이터에 시작일시을 입력해 주세요.`);
                    return;
                }
    
                if(!block.EndTime){
                    alert(`${i + 1}번째 데이터에 종료일시을 입력해 주세요.`);
                    return;
                }

                
                if(dayjs.utc(block.EndTime).diff(dayjs.utc(block.StartTime), 'second') <= 0){
                    alert(`종료일이 시작일보다 같거나 이전입니다.`);
                    return;
                }

            }

            const response = await GameAPI.SaveUserBlockAsync({ userBlocks: changed });
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
        onSearch();
        
    }, [onSearch, userBlocks, deleteUserBlocks])

    const onAddData = useCallback(() => {
        const newData = new Models.UserBlock();
        newData.UID =  userAccount?.UID && 0 < userAccount.UID ? userAccount.UID : 0;
        newData.MemberNo = userAccount?.MemberNo && 0 < userAccount?.MemberNo ? userAccount?.MemberNo : 0;
        newData.EndTime =  new Date(dayjs().tz().add(1, 'day').format('YYYY-MM-DD HH:mm:ss'));
        newData.BlockReasonID = 0;
        newData.BlockReasonStr = "";
        newData.isChanged = true;
        newData.isNewData = true;
        
        setUserBlocks(prev => {
            prev?.unshift(newData);
            return deepmerge([], prev);
        });
    }, [userAccount, setUserBlocks]);

    const onDeleteData = useCallback((index: number) => {
        const deleteData = userBlocks[index];
        
        if (!deleteData) {
            alert(`${index + 1}번째 데이터가 없습니다.`);
            return;
        }
        
        if (!deleteData.isNewData) {
            setDeleteUserBlocks(prev => {
                prev.push(deleteData);
                return deepmerge([], prev);
            });
        }
        
        setUserBlocks(prev => {
            if (prev[index]) {
                prev.splice(index, 1);
            }
            return deepmerge([], prev);
        });
    }, [userBlocks, setUserBlocks]);

    const onChangeBlockStartTime = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 시작 일시를 선택해주세요.`);
            return;
        }
        setUserBlocks(prev => {
            let dayjsDate: Dayjs | null = null;
        
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return deepmerge([], prev);
            }
            
            if (prev[index]) {
                prev[index].StartTime = dayjsDate.toDate();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setUserBlocks]);
    
    const onChangeBlockEndTime = useCallback((date: string | null, index: number) => {
        if (!date) {
            alert(`${index + 1}번째 데이터의 종료 일시를 선택해주세요.`);
            return;
        }
        setUserBlocks(prev => {
            let dayjsDate: Dayjs | null = null;
            
            try {
                dayjsDate = dayjs.tz(date);
            } catch (error) {
                return deepmerge([], prev);
            }
            
            if (prev[index]) {
                prev[index].EndTime = dayjsDate.toDate();
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [setUserBlocks]);
    
    const onChangeBlockID = useCallback((e: SelectChangeEvent<unknown>, index: number) => {
        let strValue = "";
        let blockType = 0;
        if (e && e.target && e.target.value) {
            try {
                blockType = Number(e.target.value.toString());
                const penaltyTypeNum = penaltyDataTable.find((element) => element.ID == blockType)?.PenaltyType ?? 0;
                strValue = penaltyTranslate(penaltyTypeNum);
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }
        setUserBlocks(prev => {
            if (prev[index]) {
                prev[index].BlockReasonID = blockType;
                prev[index].BlockReasonStr = strValue;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [ penaltyDataTable, setUserBlocks]);

    const onChangeBlockStrByInput = useCallback((e: ChangeEvent<HTMLInputElement|HTMLTextAreaElement>, index: number) => {
        let strValue = "";
        if (e && e.target && e.target.value) {
            try {
                strValue = e.target.value.toString();
            } catch (error) {
                if ("production" != process.env.NODE_ENV) {
                    console.log(error);
                }
            }
        }
        setUserBlocks(prev => {
            if (prev[index]) {
                prev[index].BlockReasonStr = strValue;
                prev[index].isChanged = true;
            }
            return deepmerge([], prev);
        });
    }, [ setUserBlocks]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Grid sx={{ padding: '10px 0 20px' }}>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onAddData}>추가</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" sx={{ ml: 1 }} onClick={onSaveData}>저장</Button>
                    <Button className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" sx={{ ml: 1 }} onClick={onReload}>새로고침</Button>
                </Grid>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>블럭 정보</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <UserBlockContents userUID={userAccount.UID} datas={userBlocks ?? []}
                        onDeleteData={onDeleteData} onChangeStartTime={onChangeBlockStartTime} onChangeEndTime={onChangeBlockEndTime} 
                        onChangeBlockID={onChangeBlockID} onChangeBlockStrByInput={onChangeBlockStrByInput}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default UserBlock;