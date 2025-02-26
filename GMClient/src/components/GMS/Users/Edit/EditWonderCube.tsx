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

const WonderCubeContents = dynamic(() => import('./EditWonderCubeContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface WonderCubeProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.WonderCube[];
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
}

const EditWonderCube = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged }: WonderCubeProps): ReactElement => {
    const tables = useAppSelector(state => state.tables)
    const wonderCubeTable  = tables.wonderCubeDataTable;
    
    const [wonderCubeOpts, setWonderCubeOpts] = useState<DataTableModels.WonderCubeData[]>([]);
    const [wonderCubes, setWonderCubes] = useState<Models.WonderCube[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setWonderCubes(datas)
    }, [datas]);

    useEffect(() => {
        const wonderCubeOptions:DataTableModels.WonderCubeData[] = [];

        const newData = new DataTableModels.WonderCubeData();
        newData.ID = 0;
        newData.NameStringWithID = "큐브를 선택해주세요(0)";
        wonderCubeOptions.push(newData);

        for(const key in wonderCubeTable){
            const item = new DataTableModels.WonderCubeData(wonderCubeTable[key]);
            wonderCubeOptions.push(item);
        }

        setWonderCubeOpts(wonderCubeOptions);
    }, [wonderCubeTable, setWonderCubeOpts]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if(!wonderCubes) return;

        const changed = wonderCubes.filter(_ => _.isChanged);
        if((0 < changed.length)){
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();
        
    },[loadDatas, wonderCubes]);

    const onSave = useCallback(async() => {
        const changedDatas = wonderCubes.filter(_ => _.isChanged == true);
        
        if ((!changedDatas || 1 > changedDatas.length)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        } else {
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }

        if( 0 < changedDatas.length){
            const response = await GameAPI.SaveWonderCubesAsync({ wonderCubes: changedDatas});
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

    }, [wonderCubes, loadDatas]);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <Box sx={{ padding: '10px 0 20px', display: 'flex' }}>
                    <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                    <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                </Box>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>큐브</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                       <WonderCubeContents userAccount={userAccount} datas={wonderCubes} setDatas={setWonderCubes} opts={wonderCubeOpts}/>
                       
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditWonderCube;