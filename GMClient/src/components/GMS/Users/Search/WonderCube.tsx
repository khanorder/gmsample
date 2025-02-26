import { Models } from '@ngel/data/models';
import { TableContainer } from '@mui/material';
import { ReactElement } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';

const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const WonderCubeContents = dynamic(() => import('./WonderCubeContents'), { ssr: false });

export interface WonderCubeProps {
    datas: Models.WonderCube[]|null;
}

const WonderCube = ({ datas }: WonderCubeProps): ReactElement => {

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>큐브</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <WonderCubeContents datas={datas ?? []} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default WonderCube;