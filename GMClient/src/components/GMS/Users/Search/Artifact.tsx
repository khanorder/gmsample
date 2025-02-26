import { Models } from '@ngel/data/models';
import { ReactElement } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';

const ListCountSelector = dynamic(() => import('./ListCountSelector'), { ssr: false });
const ArtifactContents = dynamic(() => import('./ArtifactContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface ArtifactProps {
    datas: PaginatedList<Models.Artifact>;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const Artifact = ({ datas, onChangeListCount }: ArtifactProps): ReactElement => {

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700, marginTop: 2 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>유물</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <ArtifactContents datas={datas ?? []} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default Artifact;