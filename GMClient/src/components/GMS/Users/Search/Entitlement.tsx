import { Models } from '@ngel/data/models';
import { ReactElement } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer } from '@mui/material';
import dynamic from 'next/dynamic';
const EntitlementContents = dynamic(() => import('./EntitlementContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });

export interface EntitlementProps {
    entitlements: Models.Entitlement[]|null;
}

const Entitlement = ({ entitlements }: EntitlementProps): ReactElement => {

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 500 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>칭호</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <EntitlementContents datas={entitlements ?? []} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default Entitlement;