import { useAppSelector } from '@hooks/index';
import { Models } from '@ngel/data/models';
import { PaginatedList } from '@helpers/paging';
import { Defines } from '@ngel/data/autoDefines';
import { ReactElement, useCallback } from 'react';
import commonUIStyles from '@styles/ui/common.module.sass';
import { TableContainer, styled } from '@mui/material';
import dynamic from 'next/dynamic';
import { SelectChangeEvent } from '@mui/material/Select';

const PetContents = dynamic(() => import('./PetContents'), { ssr: false });
const Paper = dynamic(() => import('@mui/material/Paper'), { ssr: false });
const Typography = dynamic(() => import('@mui/material/Typography'), { ssr: false });
const Toolbar = dynamic(() => import('@mui/material/Toolbar'), { ssr: false });
const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const Table = dynamic(() => import('@mui/material/Table'), { ssr: false });
const TableCell = dynamic(() => import('@mui/material/TableCell'), { ssr: false });

export interface PetProps {
    datas: PaginatedList<Models.Pet>;
    onTabSearch: (value: number) => void;
    onChangeListCount: ( event: SelectChangeEvent<unknown> ) => void;
}

const Pet = ({ datas, onTabSearch, onChangeListCount }: PetProps): ReactElement => {
    const homepageSettings = useAppSelector(state => state.homepageSettings);
    const user = useAppSelector(state => state.user);
    const layouts = useAppSelector(state => state.layouts);

    return (
        <>
            <Box sx={{ mt: 5, width: '100%' }}>
                <PetContents datas={datas ?? []} onTabSearch={onTabSearch} onChangeListCount={onChangeListCount}/>
            </Box>
        </>
    );
}

export default Pet;