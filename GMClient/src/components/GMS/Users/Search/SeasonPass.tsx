import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback, useEffect, useState } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';

const SeasonPassContents = dynamic(() => import('./SeasonPassContents'), { ssr: false });
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
    seasonPasses: Models.SeasonPass[]|null;
}

interface PassGroup {
    ID: number[];
    SeasonNum: number;
    NameString: string;
}


const SeasonPass = ({ seasonPasses }: SeasonPassProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    const tables = useAppSelector( state => state.tables);
    const sePassDataTable = tables.seasonPassDataTable;
    const sePassLvTable = tables.seasonPassLevelDataTable;
    const sePassRewardTable = tables.seasonPassRewardDataTable;

    const [selectRewardGroup, setSelectRewardGroup] = useState<number>(0);
    const [seasonPasseGroup, setSeasonPasseGroup] = useState<PassGroup[]>([]);

    useEffect(()=> {
        let seasonPassesOption: PassGroup[] = [];
        for(let i = 0; i < sePassDataTable.length ; i++){
            const overlap = seasonPassesOption.find((element)=> element.SeasonNum === sePassDataTable[i].SeasonNum);
            if(overlap) {
                overlap.ID?.push(sePassDataTable[i].ID);
                continue;
            }

            let SeasonName = sePassDataTable[i].NameStringWithID;
            if(SeasonName.includes("{value}")) {
                SeasonName = SeasonName.replace("{value}", sePassDataTable[i].SeasonNum.toString());
            }
            const newData : PassGroup = {
                ID: [],
                SeasonNum: sePassDataTable[i].SeasonNum,
                NameString: SeasonName
            }
            newData.ID?.push(sePassDataTable[i].ID);

            seasonPassesOption.push(newData);
        }
        
        setSeasonPasseGroup(seasonPassesOption);
        setSelectRewardGroup(seasonPassesOption[0]?.SeasonNum ?? 0);
    }, [tables, sePassDataTable, setSeasonPasseGroup, setSelectRewardGroup]);


    const handleSelectRewardGroup = (event: SelectChangeEvent<unknown>) => {
        const value = typeof event.target.value === "number" ? event.target.value : parseInt(event.target.value as string, 10);
        setSelectRewardGroup(value);
    }

    const AttendanceMenuItems =  useCallback((): ReactElement =>  {
        let result: ReactElement = <></>;
        const list: ReactElement[] = [];

        for(let i = 0; i < seasonPasseGroup.length; i++){
            const data = seasonPasseGroup[i];

            list.push(
                <MenuItem key={`menuitem-${i}`} value={data.SeasonNum}>{data.NameString.toString().replaceAll(/(?<=.{1})./g, '*')}</MenuItem>
            )
        }
        if(list.length >= 1)
            result = (<Select label='시즌**' value={selectRewardGroup.toString().replaceAll(/(?<=.{1})./g, '*')} onChange={handleSelectRewardGroup} size='small' style={{fontSize: "14px", width: "250px"}}>{list}</Select>);
        return result;
    },[ seasonPasseGroup, selectRewardGroup ]);

    return (
        <>
            <Box display='flex' justifyContent='end' sx={{ mt: 5, mb: 1 }}>
                <FormControl>
                    <InputLabel>시즌**</InputLabel>
                    {AttendanceMenuItems()}
                </FormControl>
            </Box>
            <Box sx={{ width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>시즌**</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} aria-label="relative table">
                        <SeasonPassContents datas={seasonPasses ?? []} selectRewardGroup={selectRewardGroup}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default SeasonPass;