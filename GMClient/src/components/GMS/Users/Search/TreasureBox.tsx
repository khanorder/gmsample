import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TreasureBoxContents = dynamic(() => import('./TreasureBoxContents'), { ssr: false });

export interface TreasureBoxProps {
    treasures: Models.TreasureBox[]|null;
}

const TreasureBox = ({ treasures }: TreasureBoxProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>트****</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <TreasureBoxContents datas={ treasures ?? [] } />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default TreasureBox;