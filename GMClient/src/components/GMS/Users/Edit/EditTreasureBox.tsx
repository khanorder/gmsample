import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { ReactElement, MutableRefObject, useCallback, useRef, useState, useEffect } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';
import { DataTableModels } from '@ngel/data/tables/model';
import deepmerge from 'deepmerge';
import { dayjs } from '@helpers/localizedDayjs';

const TreasureBoxContents = dynamic(() => import('./EditTreasureBoxContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Button = dynamic(() => import('@mui/material/Button'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface TreasureBoxProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.TreasureBox[];
    searchDetail: (index: number, accountID: number) => void;
}

const EditTreasureBox = ({ userAccount, tabIndex, datas, searchDetail }: TreasureBoxProps): ReactElement => {
    const tables = useAppSelector(state => state.tables)
    const treasureBoxDataTable = tables.treasureBoxDataTable;

    const [treasureBoxOpts, setTreasureBoxOpts] = useState<DataTableModels.TreasureBoxData[]>([]);
    const [treasureBoxes, setTreasureBoxes] = useState<Models.TreasureBox[]>([]);
    const deleteTreasureBoxes = useRef<Models.TreasureBox[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setTreasureBoxes(datas)
    }, [datas]);

    useEffect(() => {
        const treasureBoxOptions: DataTableModels.TreasureBoxData[] = [];

        const newData = new DataTableModels.TreasureBoxData();
        treasureBoxOptions.push(newData);

        for (const key in treasureBoxDataTable) {
            const item = new DataTableModels.TreasureBoxData(treasureBoxDataTable[key]);
            treasureBoxOptions.push(item);
        }

        setTreasureBoxOpts(treasureBoxOptions);
    }, [treasureBoxDataTable]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        if (!treasureBoxes) return;

        const newDatas = treasureBoxes.filter(_ => _.isNewData);
        if ((0 < newDatas.length || 0 < deleteTreasureBoxes.current.length)) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();

        deleteTreasureBoxes.current = [];
    }, [loadDatas, treasureBoxes]);

    const onAdd = useCallback(() => {
        const newData = new Models.TreasureBox();
        newData.UID = userAccount.UID;
        // newData.OpenAt = dayjs(new Date()).unix();
        newData.isNewData = true;
        newData.isChanged = true;
        setTreasureBoxes(prev => {
            prev.splice(0, 0, newData);
            return deepmerge([], prev);
        })
    }, [userAccount, setTreasureBoxes])

    const onDelete = useCallback((index: number) => {
        setTreasureBoxes(prev => {
            if (!(prev[index].isNewData)) deleteTreasureBoxes.current.push(prev[index]);
            prev.splice(index, 1);
            return deepmerge([], prev);
        })
    }, [deleteTreasureBoxes, setTreasureBoxes]);

    const onSave = useCallback(async () => {
        // const changedDatas = treasureBoxes.filter(_ => _.isNewData && _.BoxID != 0);

        // if ((!changedDatas || 1 > changedDatas.length && deleteTreasureBoxes.current.length < 1)) {
        //     alert("저장할 변경된 내용이 없습니다.");
        //     return;
        // }
        // else {
        //     if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
        //         return;
        //     }
        // }

        // if (deleteTreasureBoxes.current && 0 < deleteTreasureBoxes.current.length) {
        //     if (!confirm("삭제되는 데이터가 있습니다.\n계속 하시겠습니까?")) {
        //         return;
        //     }

        //     const response = await GameAPI.DeleteTreasureBoxesAsync({ treasureBoxes: deleteTreasureBoxes.current });
        //     if (!response) {
        //         alert(`오류!`);
        //         return;
        //     }

        //     if (!response.result) {
        //         alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
        //         return;
        //     }
        // }

        // if (0 < changedDatas.length) {
        //     const response = await GameAPI.SaveTreasureBoxesAsync({ treasureBoxes: changedDatas });
        //     if (!response) {
        //         alert(`오류!`);
        //         return;
        //     }
        //     if (!response.result) {
        //         alert(`오류! (error: ${Errors[response.error]}, index: ${response.errorIndex})`);
        //         return;
        //     }
        // }

        // alert("저장 했습니다.");
        // deleteTreasureBoxes.current = [];

        await loadDatas();

    }, [loadDatas]);
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
                        <Typography variant='h6'>트레저박스</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <TreasureBoxContents userAccount={userAccount} opts={treasureBoxOpts} datas={treasureBoxes} setTreasureBoxes={setTreasureBoxes} onDelete={onDelete} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default EditTreasureBox;