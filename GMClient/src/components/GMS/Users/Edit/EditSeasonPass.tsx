import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import { MutableRefObject, ReactElement, useCallback, useEffect, useState } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { Button, TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';
import { GameAPI } from '@ngel/data/apis/gameAPI';
import { Errors } from '@ngel/data/autoErrors';

const SeasonPassContents = dynamic(() => import('./EditSeasonPassContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const FormControl = dynamic(() => import('@mui/material/FormControl'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });
const Select = dynamic(() => import('@mui/material/Select'), { ssr: false });
const MenuItem = dynamic(() => import('@mui/material/MenuItem'), { ssr: false });
const InputLabel = dynamic(() => import('@mui/material/InputLabel'), { ssr: false });

export interface SeasonPassProps {
    userAccount: Models.UserAccount;
    tabIndex: number;
    datas: Models.SeasonPass[];
    searchDetail: (index: number, accountID: number) => void;
    tabContentChanged: MutableRefObject<boolean>;
}

interface PassGroup {
    SeasonID: number;
    SeasonNum: number;
    NameString: string;
}


const EditSeasonPass = ({ userAccount, tabIndex, datas, searchDetail, tabContentChanged }: SeasonPassProps): ReactElement => {
    const tables = useAppSelector(state => state.tables);
    const sePassDataTable = tables.seasonPassDataTable;

    const [seasonPasses, setSeasonPasses] = useState<Models.SeasonPass[]>([]);
    const [selectRewardGroup, setSelectRewardGroup] = useState<number>(0);
    const [seasonPasseGroup, setSeasonPasseGroup] = useState<PassGroup[]>([]);

    const isSigned = userAccount.IsSignIn;

    useEffect(() => {
        setSeasonPasses(datas);
    }, [datas])

    useEffect(() => {
        let seasonPassesOption: PassGroup[] = [];
        for (let i = 0; i < sePassDataTable.length; i++) {
            let SeasonName = sePassDataTable[i].NameStringWithID;

            if (SeasonName.includes("{value}")) {
                SeasonName = SeasonName.replace("{value}", sePassDataTable[i].SeasonNum.toString());
            }
            const newData: PassGroup = {
                SeasonID: sePassDataTable[i].ID,
                SeasonNum: sePassDataTable[i].SeasonNum,
                NameString: SeasonName
            }

            seasonPassesOption.push(newData);
        }
        setSeasonPasseGroup(seasonPassesOption);
        setSelectRewardGroup(seasonPassesOption[0]?.SeasonID ?? 0);
    }, [tables, sePassDataTable, setSeasonPasseGroup, setSelectRewardGroup]);

    const loadDatas = useCallback(async () => {
        await searchDetail(tabIndex, userAccount.UID);
    }, [tabIndex, userAccount.UID, searchDetail])

    const onReload = useCallback(async () => {
        const changedDatas = seasonPasses.filter(_ => _.isChanged);
        if ((0 < changedDatas.length)) {
            if (!confirm("작업중인 내용이 사라집니다.\n정말 새로고침 하시겠습니까?")) {
                return;
            }
        }

        await loadDatas();

        tabContentChanged.current = false;
    }, [loadDatas, tabContentChanged, seasonPasses]);


    const handleSelectRewardGroup = (event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        setSelectRewardGroup(value);
    }

    const AttendanceMenuItems = useCallback((): ReactElement => {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for (let i = 0; i < seasonPasseGroup.length; i++) {
            const data = seasonPasseGroup[i];

            list.push(
                <MenuItem key={`menuitem-${i}`} value={data.SeasonID}>{data.NameString.toString().replaceAll(/(?<=.{2})./g, '*')}</MenuItem>
            )
        }
        if (list.length >= 1)
            result = (<Select label='시즌패스' value={selectRewardGroup} onChange={handleSelectRewardGroup} size='small' style={{ fontSize: "14px", width: "250px" }}>{list}</Select>);
        return result;
    }, [seasonPasseGroup, selectRewardGroup]);

    const onSave = useCallback(async () => {
        const changed = seasonPasses.find(_ => _.isChanged && _.SeasonPassID == selectRewardGroup);

        if ((!changed)) {
            alert("저장할 변경된 내용이 없습니다.");
            return;
        }
        else {
            if (!confirm("확인을 누르면 내용이 저장됩니다.\n계속 하시겠습니까?")) {
                return;
            }
        }
        if (changed) {
            const response = await GameAPI.SaveSeasonPassAsync({ seasonPass: changed });
            if (!response) {
                alert(`오류!`);
                return;
            }
            if (!response.result) {
                alert(`오류! (error: ${Errors[response.error]}`);
                return;
            }
        }

        alert("저장 했습니다.");

        await loadDatas();

    }, [seasonPasses, selectRewardGroup, loadDatas]);

    return (
        <>
            <Box display='flex' justifyContent='space-between' sx={{ mt: 5, mb: 1 }}>
                <Box sx={{ display: 'flex' }}>
                    <Button disabled={isSigned} sx={{ display: isSigned ? 'none' : 'inline-block' }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                    <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
                </Box>
                <FormControl>
                    <InputLabel>시즌패스</InputLabel>
                    {AttendanceMenuItems()}
                </FormControl>
            </Box>
            <Box sx={{ width: '100%' }}>
                <TableContainer component={Paper} elevation={4}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>시즌패스</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} aria-label="relative table">
                        <SeasonPassContents userAccount={userAccount} datas={seasonPasses} setDatas={setSeasonPasses} selectRewardGroup={selectRewardGroup} />
                    </Table>
                </TableContainer>
            </Box>
            <Box sx={{ padding: '10px 0 10px', display: 'flex', mt: 2 }}>
                <Button sx={{ display: isSigned ? 'none' : 'inline-block', ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="contained" color='primary' size="small" onClick={onSave}>저장</Button>
                <Button sx={{ ml: 1 }} className={`${commonUIStyles.button} ${commonUIStyles.buttonSmall}`} variant="outlined" color='inherit' size="small" onClick={onReload}>새로고침</Button>
            </Box>
        </>
    );
}

export default EditSeasonPass;