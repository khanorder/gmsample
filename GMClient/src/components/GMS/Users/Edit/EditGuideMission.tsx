import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, MutableRefObject, useCallback, useRef, useState, useEffect } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';
import { DataTableModels } from '@ngel/data/tables/model';

const Paging = dynamic(() => import('@components/ui/Paging'), { ssr: false });
const ListCountSelector = dynamic(() => import('./EditListCountSelector'), { ssr: false });
const GuideMissionContents = dynamic(() => import('./EditGuideMissionContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface GuideMissionProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: PaginatedList<Models.GuideMission>;
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
    onChangePageListCount: (event: SelectChangeEvent<unknown>) => void;
}

const GuideMission = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged, onChangePageListCount }: GuideMissionProps): ReactElement => {
    const [guideMissions, setGuideMissions] = useState<PaginatedList<Models.GuideMission>>(new PaginatedList<Models.GuideMission>([]));
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setGuideMissions(datas);
    }, [datas])

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if (!guideMissions) return;

        const changedDatas = guideMissions.totalItems.filter(_ => _.isChanged);
        if ((0 < changedDatas.length)) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();

        tabContentChanged.current = false;
    }, [loadDatas, tabContentChanged, guideMissions]);

    const onHandleChangeListCount = useCallback((event: SelectChangeEvent<unknown>) => {
        onChangePageListCount(event);
        tabContentChanged.current = false;
    }, [tabContentChanged, onChangePageListCount]);

    const onSave = useCallback(async () => {
        const changedDatas = guideMissions.totalItems.filter(_ => _.isChanged);

        if ((!changedDatas || 1 > changedDatas.length)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else {
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if (0 < changedDatas.length) {
            const response = await GameAPI.SaveGuideMissionsAsync({ guideMissions: changedDatas });
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

        await loadDatas();

    }, [guideMissions, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                    <Box>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave} >저장</Button>
                        <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    </Box>
                    <Box sx={{ marginLeft: 1 }}>
                        <ListCountSelector pageLogs={datas.pageSize} handleChange={onHandleChangeListCount} />
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>가이드미션</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <GuideMissionContents userAccount={userAccount} datas={datas ?? []} />
                    </Table>
                </TableContainer>
            </Box>
            <Paging datas={guideMissions} />
        </>
    );
}

export default GuideMission;