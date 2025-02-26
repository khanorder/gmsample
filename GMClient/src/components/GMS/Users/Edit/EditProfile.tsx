import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, MutableRefObject, useCallback, useRef, useState, useEffect  } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';

const ProfileContents = dynamic(() => import('./EditProfileContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface ProfileProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.Profile[];
    searchDetail: (index: number, accountID: number) => void;
}

const EditProfile = ({ userAccount, tabIndex, datas, searchDetail }: ProfileProps): ReactElement => {
    const tables = useAppSelector(state => state.tables)
    const profileTable  = tables.profileDataTable;
    
    const [profileOpts, setProfileOpts] = useState<DataTableModels.ProfileData[]>([]);
    const [profiles, setProfiles] = useState<Models.Profile[]>([]);
    const deleteProfiles = useRef<Models.Profile[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setProfiles(datas)
    }, [datas]);

    useEffect(() => {
        const profileOptions:DataTableModels.ProfileData[] = [];

        const newData = new DataTableModels.ProfileData();
        newData.ID = 0;
        newData.NameStringWithID = "프로필 아이템을 선택해주세요(0)";
        profileOptions.push(newData);

        for(const key in profileTable){
            const item = new DataTableModels.ProfileData(profileTable[key]);
            profileOptions.push(item);
        }

        setProfileOpts(profileOptions);
    }, [profileTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!profiles) return;

        const newDatas = profiles.filter(_ => _.isNewData);
        if((0 < newDatas.length || 0 < deleteProfiles.current.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
        deleteProfiles.current = [];
    },[loadDatas, profiles]);

    const onAdd = useCallback(() => {
        const newData = new Models.Profile();
        newData.UID = userAccount.UID;
        newData.isNewData = true;
        setProfiles(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        })
    }, [userAccount, setProfiles])

    const onDelete = useCallback((index: number) => {
        setProfiles(prev => {
            if(!(prev[index].isNewData)) deleteProfiles.current.push(prev[index]);
            prev.splice(index, 1);
            return deepmerge([], prev);
        })
    }, [deleteProfiles, setProfiles]);

    const onSave = useCallback(async() => {
        const changedDatas = profiles.filter(_ => _.isNewData && _.ProfileID != 0);

        if ((!changedDatas || 1 > changedDatas.length && deleteProfiles.current.length < 1)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        } else {
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        if ( deleteProfiles.current && 0 < deleteProfiles.current.length) {
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
            
            const response = await GameAPI.DeleteProfilesAsync({ profiles : deleteProfiles.current });
            if (!response) {
                alert(`오류!`);
                return;
            }
    
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
                return;
            }
        }

        if ( 0 < changedDatas.length) {
            const response = await GameAPI.SaveProfilesAsync({ profiles: changedDatas});
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
        deleteProfiles.current = [];

        await loadDatas();

    }, [profiles, deleteProfiles, loadDatas]);

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
                        <Typography variant='h6'>프로필 배경</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <ProfileContents userAccount={userAccount} opts={profileOpts} datas={profiles} setProfiles={setProfiles} onDelete={onDelete}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditProfile;