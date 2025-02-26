import { Models } from '@ngel/data/models';
import { PaginatedList } from '@helpers/paging';
import { TableContainer } from '@mui/material';
import { ReactElement } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { SelectChangeEvent } from '@mui/material/Select';
import ListCountSelector from './ListCountSelector';
import dynamic from 'next/dynamic';
const FriendsContents = dynamic(() => import('./FriendsContents'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface FriendsProps {
    friends: PaginatedList<Models.Friend>;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const Friends = ({ friends, onChangeListCount }: FriendsProps): ReactElement => {
    
    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <ListCountSelector pageLogs={friends.pageSize} handleChange={onChangeListCount}/>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700, marginTop: 2 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>친구 **</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                       <FriendsContents datas={friends} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default Friends;