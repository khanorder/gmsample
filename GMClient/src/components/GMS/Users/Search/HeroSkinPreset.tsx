import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { TableContainer, Paper, styled, Toolbar, Typography } from '@mui/material';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import dynamic from 'next/dynamic';

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const HeroSkinPresetContents = dynamic(() => import('./HeroSkinPresetContents'), { ssr: false });
export interface HeroSkinPresetProps {
    datas: Models.HeroSkinPreset[]|null;
}

const HeroSkinPreset = ({ datas }: HeroSkinPresetProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>스킨 ***</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <HeroSkinPresetContents datas={datas ?? []} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default HeroSkinPreset;