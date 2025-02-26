import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
const ProfileContents = dynamic(() => import('./ProfileContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface ProfileBGProps {
    profiles : Models.Profile[]|null;
}

const ProfileBG = ({ profiles }: ProfileBGProps): ReactElement => {

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>프** * **</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <ProfileContents datas={profiles ?? []}/>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default ProfileBG;