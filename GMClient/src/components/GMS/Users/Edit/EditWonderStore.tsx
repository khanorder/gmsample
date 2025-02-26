import { Models } from '@ngel/data/models';
import { TableContainer } from '@mui/material';
import { MutableRefObject, ReactElement, useCallback, useEffect, useState } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';

const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const WonderStoreContents = dynamic(() => import('../Edit/EditWonderStoreContents'), { ssr: false });

export interface WonderStoreProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.WonderStore[];
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
}

const WonderStore = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged }: WonderStoreProps): ReactElement => {
    const [wonderStores, setWonderStores] = useState<Models.WonderStore[]>([]);
    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setWonderStores(datas);
    }, [datas])

    const loadDatas = useCallback(async () => {
        setWonderStores([]);
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])
    
    const onReload = useCallback(async() => {
        const changed = wonderStores.filter(_ => _.isChanged);

        if(changed && 0 < changed.length) {
            if(!confirm("변경된 항목이 있습니다.\n계속 진행하시겠습니까?")) return;
        }

        await loadDatas();
    }, [loadDatas, wonderStores])

    const onSave = useCallback(async() => {
        const changed = wonderStores.filter(_ => _.isChanged);

        if(changed && 0 < changed.length) {
            if(!confirm("변경된 항목이 있습니다.\n계속 진행하시겠습니까?")) return;
        }

        for(let i = 0 ; i < wonderStores.length ; i++) {
            
            if(wonderStores[i].IsSubscription && wonderStores[i].SubscriptionExpireAt == 0){
                alert(`${i+1} 번째 정보의 구독유효 시간이 비어있습니다.`);
                return;
            }
        }

        const response = await GameAPI.SaveWonderStoresAsync({ wonderStores: wonderStores });
        if (response.result) {
            alert("변경된 내용을 저장했습니다.");
            await loadDatas();
        } else {
            alert(`서비스 상태 저장 실패(error: ${Errors[response.error]})`);
        }
    }, [loadDatas, wonderStores])
    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                    <Box>
                        <Button sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave} >저장</Button>
                        <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                    </Box>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>상점</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <WonderStoreContents datas={wonderStores ?? []} setDatas={setWonderStores} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default WonderStore;