import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { PaginatedList } from '@helpers/paging';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './ListCountSelector';
import dynamic from 'next/dynamic';

const RankingRewardContents = dynamic(() => import('./RankingRewardContents'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface RankingRewardProps {
    rankingReward: PaginatedList<Models.RankingReward>;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const RankingReward = ({ rankingReward, onChangeListCount }: RankingRewardProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);
    
    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <ListCountSelector pageLogs={rankingReward.pageSize} handleChange={onChangeListCount}/>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700, marginTop: 2 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>랭킹 **</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <RankingRewardContents datas={rankingReward} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default RankingReward;