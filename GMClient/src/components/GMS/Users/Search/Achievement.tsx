import { Models } from '@ngel/data/models';
import { TableContainer, Paper, Toolbar, Typography } from '@mui/material';
import { ReactElement } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';

const ListCountSelector = dynamic(() => import('./ListCountSelector'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const AchievementContents = dynamic(() => import('./AchievementContents'), { ssr: false });
export interface AchievementProps {
    datas: PaginatedList<Models.Achievement>;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const Achievement = ({ datas, onChangeListCount }: AchievementProps): ReactElement => {

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700, marginTop: 2 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>업적</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <AchievementContents datas={datas} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default Achievement;