import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { PaginatedList } from '@helpers/paging';
import { SelectChangeEvent } from '@mui/material/Select';

const ListCountSelector = dynamic(() => import('./ListCountSelector'), { ssr: false });
const RpgAttributeContents = dynamic(() => import('./RpgAttributeContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface RpgAttributeProps {
    datas: PaginatedList<Models.RpgAttribute>;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const RpgAttribute = ({ datas, onChangeListCount }: RpgAttributeProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
            <ListCountSelector pageLogs={datas.pageSize} handleChange={onChangeListCount}/>
                <TableContainer component={Paper} elevation={4} sx={{ maxHeight: 700, marginTop: 2 }}>
                    <Toolbar variant='dense' className={commonUIStyles.toolBar}>
                        <Typography variant='h6'>특성</Typography>
                    </Toolbar>
                    <Table className={commonUIStyles.ellipsisTable} stickyHeader aria-label="sticky table">
                        <RpgAttributeContents datas={datas ?? []} />
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}

export default RpgAttribute;