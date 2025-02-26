import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, MutableRefObject, useCallback, useRef } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import EditMailContents from './EditMailContents';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './EditListCountSelector';

const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface MailProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas : PaginatedList<Models.Mail>;
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
    onChangePageListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const EditMail = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged, onChangePageListCount }: MailProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const deleteMails = useRef<Models.Mail[]>([]);

    const isSigned = userAccount.IsSignIn;

    const onReload = useCallback(async () => {
        if((0 < deleteMails.current.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await searchDetail(tabIndex, userAccount.UID);
        
        deleteMails.current = [];
        tabContentChanged.current = false;
    },[tabIndex, userAccount, tabContentChanged, searchDetail]);

    const onHandleChangeListCount = useCallback(( event: SelectChangeEvent<unknown> ) => {
        onChangePageListCount(event);
        tabContentChanged.current = false;
    },[tabContentChanged, onChangePageListCount]);

    const onSave = useCallback(async () => {
        if(!(0 < deleteMails.current.length)){
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else{
            if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        
        const response = await GameAPI.DeleteMailsAsync({mails: deleteMails.current});
        if (!response) {
            alert(`오류!`);
            return;
        }
        if (!response.result) {
            alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
            return;
        }

        alert("저장 했습니다.");
        deleteMails.current = [];

        await onReload();
        
    }, [deleteMails, onReload]);
    
    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', mb: 1 }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                        <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    </Box>
                    <Box marginLeft={1}>
                        <ListCountSelector pageLogs={datas.pageSize} handleChange={onHandleChangeListCount}/>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>우편</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <EditMailContents userAccount={userAccount} datas={datas ?? []} tabChanged={tabContentChanged} deleteList={deleteMails}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditMail;